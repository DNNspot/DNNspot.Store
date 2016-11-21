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
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace DNNspot.Store
{
    public static class HttpHelper
    {
        public static HttpWebResponse HttpPost(string url, Dictionary<string, string> postVars)
        {
            return HttpPost(url, EncodeVarsForHttpPostString(postVars), string.Empty);
        }

        public static HttpWebResponse HttpPost(string url, string postData)
        {
            return HttpPost(url, postData, string.Empty);
        }

        public static HttpWebResponse HttpPost(string url, Dictionary<string, string> postVars, string httpContentType)
        {
            return HttpPost(url, EncodeVarsForHttpPostString(postVars), httpContentType);
        }

        private static HttpWebResponse HttpPost(string url, string postData, string httpContentType)
        {
            //HttpWebRequest webRequest = CreatePostRequest(url, postData, httpContentType);

            // METHOD #1
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentLength = postData.Length;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            if (!string.IsNullOrEmpty(httpContentType))
            {
                webRequest.ContentType = httpContentType;
            }

            // post data is sent as a stream
            using (StreamWriter myWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                myWriter.Write(postData);
                myWriter.Close();
            }
                                                
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

            // METHOD #2
            //WebClient webClient = new WebClient();
            //webClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            //byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postData);
            //webClient.UploadData(url, "POST", postBytes);            

            return webResponse;
        }

        public static string WebResponseToString(HttpWebResponse webResponse)
        {
            string responseString = "";
            using (StreamReader responseStream = new StreamReader(webResponse.GetResponseStream()))
            {
                responseString = responseStream.ReadToEnd();
                responseStream.Close();
            }

            return responseString;
        }

        public static string EncodeVarsForHttpPostString(Dictionary<string, string> postVars)
        {
            if (postVars != null)
            {
                StringBuilder postValues = new StringBuilder(postVars.Keys.Count);
                foreach (KeyValuePair<string, string> pair in postVars)
                {
                    postValues.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(pair.Key), HttpUtility.UrlEncode(pair.Value));
                }
                string postString = postValues.ToString().TrimEnd('&');

                return postString;
            }
            return "";
        }

        public static Dictionary<string, string> DecodeParamsFromHttpRequest(HttpRequest request)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>(request.Params.Keys.Count);

            NameValueCollection postedForm = request.Params;
            foreach (string key in postedForm.Keys)
            {
                string[] values = postedForm.GetValues(key);
                if (values != null)
                {
                    fields[key] = string.Join(",", values);
                }
                else
                {
                    fields[key] = "";
                }
            }

            return fields;
        }

        public static Dictionary<string, string> DecodeVarsFromHttpString(string webResponse)
        {
            if (!string.IsNullOrEmpty(webResponse))
            {
                string[] encodedPairs = webResponse.Split('&');
                Dictionary<string, string> fields = new Dictionary<string, string>(encodedPairs.Length);
                foreach (string field in encodedPairs)
                {
                    string[] pair = field.Split('=');
                    string name = HttpUtility.UrlDecode(pair[0]);
                    string value = (pair.Length >= 2) ? HttpUtility.UrlDecode(pair[1]) : "";

                    fields[name] = value;
                }
                return fields;
            }
            return new Dictionary<string, string>();
        }
    }
}
