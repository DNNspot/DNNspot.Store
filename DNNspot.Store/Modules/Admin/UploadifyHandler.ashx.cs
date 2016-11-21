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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using DNNspot.Store.DataModel;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class UploadifyHandler : IHttpHandler
    {
        HttpContext context;
        HttpResponse response;
        HttpRequest request;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            this.response = context.Response;
            this.request = context.Request;

            try
            {
                string folderParam = request.Params["folder"];
                int? idParam = WA.Parser.ToInt(request.Params["productId"]);
                string typeParam = (request.Params["type"] ?? "").ToLower();

                if (!idParam.HasValue)
                {
                    throw new ArgumentException("'productId' must be set in Request.Params[]");
                }
                if (string.IsNullOrEmpty(typeParam))
                {
                    throw new ArgumentException("'type' must be set in Request.Params[]");
                }
                
                string fileUploadDirectory = StoreUrls.GetProductPhotoFolderFileRoot();
                //Debug.WriteFormat(@"photo fileUploadDirectory = ""{0}""", fileUploadDirectory);
                if (!Directory.Exists(fileUploadDirectory))
                {
                    //Debug.WriteFormat(@"creating photo fileUploadDirectory = ""{0}""", fileUploadDirectory);
                    Directory.CreateDirectory(fileUploadDirectory);
                }                

                //--- Save Files/Photos for product
                Product product = new Product();
                if (product.LoadByPrimaryKey(idParam.Value))
                {
                    for (int i = 0; i < request.Files.Count; i++)
                    {
                        HttpPostedFile postedFile = request.Files[i];
                        //context.Trace.Write(string.Format(@"Saving uploaded file ""{0}""", postedFile.FileName));

                        //string fileExt = Path.GetExtension(postedFile.FileName);
                        //string filenameWithExt = string.Format("{0}_{1}{2}", idParam.Value, Guid.NewGuid(), fileExt);
                        //string filePath = fileUploadDirectory + filenameWithExt;
                        string filePath = fileUploadDirectory + Path.GetFileName(postedFile.FileName).CreateUniqueSequentialFileNameInDir(fileUploadDirectory);
                        postedFile.SaveAs(filePath);

                        // Digital Files are NOT handled here anymore (uploaded via ASP.NET FileUpload control in EditProduct.ascx)
                        // Only product photos are "uploadified"
                        if (typeParam == "photo")
                        {
                            ProductPhoto newPhoto = product.ProductPhotoCollectionByProductId.AddNew();
                            newPhoto.Filename = Path.GetFileName(filePath);
                            newPhoto.DisplayName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                            newPhoto.SortOrder = 99;
                        }
                        product.Save();
                    }
                }

                response.Write("1");
            }
            catch(Exception ex)
            {
                context.Trace.Write("", "Error in UploadifyHandler.ashx - " + ex.Message, ex);
                response.Write("ERROR: " + ex.Message);
            }
            response.Flush();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}