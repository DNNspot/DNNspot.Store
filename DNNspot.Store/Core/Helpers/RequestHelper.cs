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
using System.Linq;
using System.Web;

namespace DNNspot.Store
{
    internal static class RequestHelper
    {
        public static string GetCategorySlug()
        {
            string cat = GetCategoryFromQueryString();
            string slug = GetSlugFromQueryString();

            if (!string.IsNullOrEmpty(cat))
            {
                return cat.TrimEnd('/');
            }
            return slug;
        }

        public static string GetSlug()
        {
            return GetSlugFromQueryString();
        }

        private static string GetSlugFromQueryString()
        {
            //return HttpContext.Current.Request.QueryString["slug"] ?? "";

            // http://dnn7/en-us/DNNspot/Store/test-product

            var request = HttpContext.Current.Request;

            var parts = request.RawUrl.Split('/');
            return parts.Last().Split('?').First();

            string raw = request.RawUrl;
            string url = request.Url.ToString();
            string path = request.Url.AbsolutePath;
            string uri = request.Url.AbsoluteUri;

            return string.Empty;
        }

        private static string GetCategoryFromQueryString()
        {
            return HttpContext.Current.Request.QueryString["cat"] ?? "";
        }
    }
}
