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
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.Importers;
using WA.Extensions;
using WA.FileHelpers.Csv;

namespace DNNspot.Store.Modules.Admin
{
    public partial class ProductImport : StoreAdminModuleBase
    {
        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
                       {
                           new AdminBreadcrumbLink() { Text = "Products", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Products) },
                           new AdminBreadcrumbLink() { Text = "Import" }
                       };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                
            }
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {            
            if(fupImportFile.HasFile)
            {
                ProductCsvImporter productCsvImporter = new ProductCsvImporter(StoreContext.CurrentStore.Id.Value);
                ProductCsvImportResult result = productCsvImporter.ImportProducts(fupImportFile.FileContent);

                StringBuilder html = new StringBuilder();

                if(result.Messages.Count > 0)
                {
                    html.Append(result.Messages.ToDelimitedString("<br />"));
                }

                foreach(var status in WA.Enum<ProductImportStatus>.GetValues())
                {
                    var lines = result.CsvLines.FindAll(x => x.Status == status);
                    if (lines.Count > 0)
                    {
                        html.AppendFormat(@"<h3>[ {0:N0} ] <a href=""#"" onclick=""jQuery('#lines{1}').toggle(); return false;"">{1}</a></h3>", lines.Count, status);
                        html.AppendFormat(@"<div id=""lines{0}"" style=""display: none;"">", status);
                        html.Append(@"<table class=""grid gridLight""> <thead> <tr> <th style=""text-align: right; width: 82px;"">CSV Line #</th> <th>Name</th> <th>Sku</th> <th>UrlName</th> <th>Import Notes</th> </tr> </thead> <tbody>");
                        foreach (var line in lines)
                        {
                            html.AppendFormat(@"<tr> <td style=""text-align: right;"">{0}</td> <td>{2}</td> <td>{3}</td> <td>{4}</td> <td>{1}</td> </tr>",
                                    line.CsvLineNumber, line.StatusMsg, line.ProductName, line.ProductSku, line.ProductUrlName);

                        }
                        html.Append(@"</tbody> </table>");
                        html.Append("</div>");
                    }
                }

                ShowFlash(html.ToString());
            }
        }
    }

}