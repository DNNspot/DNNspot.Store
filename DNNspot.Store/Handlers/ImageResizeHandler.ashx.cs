/*
* This software is licensed under the GNU General Public License, version 2
* You may copy, distribute and modify the software as long as you track changes/dates of in source files and keep all modifications under GPL. You can distribute your application using a GPL library commercially, but you must also provide the source code.

* DNNspot Software (http://www.dnnspot.com)
* Copyright (C) 2013 Atriage Software LLC
* Authors: Kevin Southworth, Matthew Hall, Ryan Doom

* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

* Full license viewable here: http://www.gnu.org/licenses/gpl-2.0.txt
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Services;

namespace DNNspot.Store.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ImageResizeHandler : IHttpHandler
    {
        private HttpContext context;

        #region Configuration Options
        private readonly bool aspNetCacheEnabled = WA.Parser.ToBool(ConfigurationManager.AppSettings["DynamicResizer.AspNetCache.Enabled"]).GetValueOrDefault(false);
        private readonly int aspNetCacheDurationMinutes = WA.Parser.ToInt(ConfigurationManager.AppSettings["DynamicResizer.AspNetCache.DurationMinutes"]).GetValueOrDefault(60);

        private readonly bool diskCacheEnabled = WA.Parser.ToBool(ConfigurationManager.AppSettings["DynamicResizer.DiskCache.Enabled"]).GetValueOrDefault(false);
        private readonly string diskCacheDirectory = ConfigurationManager.AppSettings["DynamicResizer.DiskCache.Directory"] ?? "";

        private readonly bool notFoundImageEnabled = WA.Parser.ToBool(ConfigurationManager.AppSettings["DynamicResizer.NotFoundImage.Enabled"]).GetValueOrDefault(false);
        private readonly string notFoundImagePath = ConfigurationManager.AppSettings["DynamicResizer.NotFoundImage.Path"] ?? "";

        private readonly int jpgQuality = WA.Parser.ToInt(ConfigurationManager.AppSettings["DynamicResizer.JpgQuality"]).GetValueOrDefault(90);
        private readonly bool allowUpscaling = WA.Parser.ToBool(ConfigurationManager.AppSettings["DynamicResizer.AllowUpscaling"]).GetValueOrDefault(false);
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;

            int? requestedWidth = WA.Parser.ToInt(context.Request.Params["w"]);
            int? requestedHeight = WA.Parser.ToInt(context.Request.Params["h"]);

            try
            {
                string requestedPhysicalPath = Regex.Replace(context.Request.PhysicalPath, "\\.ashx.*", "");
                //Debug.WriteFormat(@"ImageResizeHandler :: requestedPhysicalPath = ""{0}""", requestedPhysicalPath);

                if (!File.Exists(requestedPhysicalPath))
                {
                    //Debug.Write("ImageResizeHandler :: file not found ");

                    string notFoundImageServerPath = context.Server.MapPath(notFoundImagePath);
                    if (notFoundImageEnabled && File.Exists(notFoundImageServerPath))
                    {
                        context.Response.ContentType = GetContentTypeFromFilePath(notFoundImageServerPath);

                        //context.Response.WriteFile(notFoundImageServerPath);
                        byte[] imgBytes;
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            // resize the image
                            ResizeToStream(notFoundImageServerPath, requestedWidth, requestedHeight, imgStream);
                            imgBytes = imgStream.GetBuffer();  
                        }
                        if (imgBytes != null)
                        {
                            context.Response.BinaryWrite(imgBytes);
                        }
                    }
                    else
                    {
                        context.Response.ClearHeaders();
                        context.Response.ClearContent();

                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        context.Response.Write("File Not Found");
                    }
                }
                else
                {
                    string requestedVirtualPath = Regex.Replace(context.Request.Path, "\\.ashx.*", "");

                    string virtualPathUnderscored = Regex.Replace(requestedVirtualPath, "/", "_").TrimStart('_');
                    string resizedFileName = string.Format("{0}_{1}x{2}{3}",
                                                           Path.GetFileNameWithoutExtension(virtualPathUnderscored),
                                                           requestedWidth.HasValue
                                                               ? requestedWidth.Value.ToString()
                                                               : "",
                                                           requestedHeight.HasValue
                                                               ? requestedHeight.Value.ToString()
                                                               : "", Path.GetExtension(virtualPathUnderscored));

                    if (aspNetCacheEnabled)
                    {
                        byte[] imgBytes = context.Cache[resizedFileName] as byte[];
                        if (imgBytes == null)
                        {
                            // not in the cache yet
                            TraceLine(string.Format(@"""{0}"" not found in ASP.NET Cache...adding to cache", resizedFileName));
                            //Debug.WriteFormat(@"ImageResizeHandler ::: ""{0}"" not found in ASP.NET Cache...adding to cache", resizedFileName);
                            using (MemoryStream imgStream = new MemoryStream())
                            {
                                // resize the image
                                ResizeToStream(requestedPhysicalPath, requestedWidth, requestedHeight, imgStream);
                                imgBytes = imgStream.GetBuffer();

                                // put the resized image into the ASP.NET Cache
                                context.Cache.Insert(resizedFileName, imgBytes, null, Cache.NoAbsoluteExpiration,
                                                     TimeSpan.FromMinutes(aspNetCacheDurationMinutes));
                            }
                        }
                        else
                        {
                            TraceLine(string.Format(@"Serving ""{0}"" from the ASP.NET Cache.", resizedFileName));
                            //Debug.WriteFormat(@"ImageResizeHandler ::: Serving ""{0}"" from the ASP.NET Cache.", resizedFileName);
                        }

                        string contentType = GetContentTypeFromFilePath(requestedPhysicalPath);
                        if (!string.IsNullOrEmpty(contentType))
                        {
                            context.Response.ContentType = contentType;
                        }

                        context.Response.BinaryWrite(imgBytes);                        
                    }
                    else if (diskCacheEnabled && !string.IsNullOrEmpty(diskCacheDirectory))
                    {
                        string resizeCacheDirectoryPath = context.Server.MapPath(diskCacheDirectory);
                        string resizedFilePath = System.IO.Path.Combine(resizeCacheDirectoryPath, resizedFileName);

                        if (!Directory.Exists(resizeCacheDirectoryPath))
                        {
                            Directory.CreateDirectory(resizeCacheDirectoryPath);
                        }

                        string contentType = GetContentTypeFromFilePath(resizedFilePath);
                        if (!string.IsNullOrEmpty(contentType))
                        {
                            context.Response.ContentType = contentType;
                        }

                        if (File.Exists(resizedFilePath))
                        {
                            //------ output the cached file from disk             
                            TraceLine(string.Format(@"Serving ""{0}"" from Disk Cache", resizedFileName));
                            //Debug.WriteFormat(@"ImageResizeHandler ::: Serving ""{0}"" from Disk Cache", resizedFileName);

                            context.Response.WriteFile(resizedFilePath);
                        }
                        else
                        {
                            //------ resize the image and save to disk               
                            TraceLine(string.Format(@"""{0}"" not found in Disk Cache...adding to cache", resizedFileName));
                            //Debug.WriteFormat(@"ImageResizeHandler ::: ""{0}"" not found in Disk Cache...adding to cache", resizedFileName);

                            using (FileStream fileStream = new FileStream(resizedFilePath, FileMode.Create))
                            {
                                ResizeToStream(requestedPhysicalPath, requestedWidth, requestedHeight, fileStream);
                            }
                            //------ output the image from disk                            
                            context.Response.WriteFile(resizedFilePath);
                        }
                    }
                    else
                    {
                        //--- resize cache NOT enabled, do everything in memory
                        TraceLine(string.Format(@"Serving ""{0}"" from Memory Stream (no caching)", resizedFileName));
                        //Debug.WriteFormat(@"ImageResizeHandler ::: Serving ""{0}"" from Memory Stream (no caching)", resizedFileName);

                        string contentType = GetContentTypeFromFilePath(requestedPhysicalPath);
                        if (!string.IsNullOrEmpty(contentType))
                        {
                            context.Response.ContentType = contentType;
                        }

                        ResizeToStream(requestedPhysicalPath, requestedWidth, requestedHeight,
                                       context.Response.OutputStream);                        
                    }
                }

                context.Response.Flush();
            }
            catch(Exception ex)
            {
                context.Response.ClearHeaders();
                context.Response.ClearContent();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                context.Response.Write("ERROR: " + ex.Message + " " + Environment.NewLine + ex.StackTrace);
                //Debug.Write("ImageResizeHandler ::: ERROR: " + ex.Message + " " + Environment.NewLine + ex.StackTrace);
            }
        }

        private void Trace(string s)
        {
            context.Trace.Write(s);
        }

        private void TraceLine(string s)
        {
            context.Trace.Write(s + Environment.NewLine);
        }

        private void ResizeToStream(string requestedPhysicalPath, int? requestedWidth, int? requestedHeight, Stream outputStream)
        {
            //Debug.Write("ImageResizeHandler ::: ResizeToStream");

            try
            {
                System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(requestedPhysicalPath);
                using (Bitmap sourceBitmap = new Bitmap(requestedPhysicalPath))
                {
                    int? newWidth = requestedWidth;
                    int? newHeight = requestedHeight;

                    if (newWidth.HasValue && !newHeight.HasValue)
                    {
                        //The user only set the width, calculate the new height
                        newHeight = (int) Math.Floor(sourceBitmap.Height/(sourceBitmap.Width/(double) newWidth));
                    }

                    if (newHeight.HasValue && !newWidth.HasValue)
                    {
                        //The user only set the height, calculate the width
                        newWidth = (int) Math.Floor(sourceBitmap.Width/(sourceBitmap.Height/(double) newHeight));
                    }

                    if (!newWidth.HasValue && !newHeight.HasValue)
                    {
                        // width and height were not set, use source dimensions
                        newWidth = sourceBitmap.Width;
                        newHeight = sourceBitmap.Height;
                    }                   
                    
                    if(!allowUpscaling && (newWidth.Value > sourceBitmap.Width || newHeight.Value > sourceBitmap.Height))
                    {
                        newWidth = sourceBitmap.Width;
                        newHeight = sourceBitmap.Height;
                    }

                    using (Bitmap resizedBitmap = new Bitmap(sourceBitmap, newWidth.Value, newHeight.Value))
                    {
                        //resizedBitmap.SetResolution(72F, 72F);
                        resizedBitmap.SetResolution(sourceBitmap.HorizontalResolution, sourceBitmap.VerticalResolution);

                        Graphics newGraphic = Graphics.FromImage(resizedBitmap);
                        newGraphic.Clear(Color.Transparent);
                        newGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        newGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        newGraphic.DrawImage(sourceBitmap, 0, 0, newWidth.Value, newHeight.Value);

                        // Save the image as the appropriate type                    
                        if (imageFormat == ImageFormat.Jpeg)
                        {
                            SaveAsJpg(resizedBitmap, outputStream);
                        }
                        else
                        {
                            resizedBitmap.Save(outputStream, imageFormat);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //Debug.WriteFormat(@"ImageResizeHandler ::: ResizeToStream ::: EXCEPTION - ""{0}""", ex.Message + ex.StackTrace);
            }
        }

        private void SaveAsJpg(Bitmap bitmap, Stream outputStream)
        {                        
            EncoderParameters codecParameters = new EncoderParameters(1);
            codecParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpgQuality);
            ImageCodecInfo codecInfo = FindEncoder(ImageFormat.Jpeg);

            bitmap.Save(outputStream, codecInfo, codecParameters);
        }

        private static ImageCodecInfo FindEncoder(ImageFormat fmt)
        {
            ImageCodecInfo[] infoArray1 = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo[] infoArray2 = infoArray1;
            for (int num1 = 0; num1 < infoArray2.Length; num1++)
            {
                ImageCodecInfo info1 = infoArray2[num1];
                if (info1.FormatID.Equals(fmt.Guid))
                {
                    return info1;
                }
            }
            return null;
        }

        private static string GetContentTypeFromFilePath(string filePath)
        {
            string fileExt = Path.GetExtension(filePath);
            switch(fileExt.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";                    
                case ".gif":
                    return "image/gif";                    
                case ".png":
                    return "image/png";                    
                default:
                    return "";
            }
        }

        private static ImageFormat GetImageFormat(string requestedPhysicalPath)
        {
            string fileExt = Path.GetExtension(requestedPhysicalPath);
            switch (fileExt.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;                    
                case ".gif":
                    return ImageFormat.Gif;                    
                case ".png":
                    return ImageFormat.Png;                    
                default:
                    return ImageFormat.Jpeg;
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}