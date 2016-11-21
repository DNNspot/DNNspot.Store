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
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.Importers;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class ProductExport : StoreAdminModuleBase
    {
        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
                       {
                           new AdminBreadcrumbLink() { Text = "Products", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Products) },
                           new AdminBreadcrumbLink() { Text = "Export" }
                       };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnDownloadCsv_Click(object sender, EventArgs e)
        {
            var productList = DataModel.ProductCollection.GetAll(StoreContext.CurrentStore.Id.Value);
            productList.Sort((left, right) => left.Id.Value.CompareTo(right.Id.Value));

            List<CsvProductInfo> csvProducts = new List<CsvProductInfo>();
            foreach (Product p in productList)
            {
                var csv = new CsvProductInfo()
                              {
                                  ImportAction = "",
                                  ProductId = p.Id.Value,
                                  Name = p.Name,
                                  Sku = p.Sku,
                                  UrlName = p.Slug,
                                  Price = p.Price,
                                  Weight = p.Weight,
                                  SeoTitle = p.SeoTitle,
                                  SeoDescription = p.SeoDescription,
                                  SeoKeywords = p.SeoKeywords,
                                  IsActive = p.IsActive,
                                  CategoryNames = p.GetCategories(true).ConvertAll(c => c.Name).ToDelimitedString(", "),
                                  PhotoFilenames = p.GetAllPhotosInSortOrder().ConvertAll(x => x.Filename).ToDelimitedString(", "),
                                  DigitalFilename = p.DigitalFilename,
                                  TaxableItem = p.IsTaxable,
                                  ShowPrice = p.IsPriceDisplayed,
                                  AvailableForPurchase = p.IsAvailableForPurchase,
                                  EnableInventoryManagement = p.InventoryIsEnabled,
                                  StockLevel = p.InventoryQtyInStock,
                                  AllowNegativeStock = p.InventoryAllowNegativeStockLevel
                              };
                var descriptors = p.GetProductDescriptors();
                if (descriptors.Count >= 1)
                {
                    csv.Desc1Name = descriptors[0].Name;
                    csv.Desc1Html = descriptors[0].TextHtmlDecoded;
                }
                if (descriptors.Count >= 2)
                {
                    csv.Desc2Name = descriptors[1].Name;
                    csv.Desc2Html = descriptors[1].TextHtmlDecoded;
                }
                if (descriptors.Count >= 3)
                {
                    csv.Desc3Name = descriptors[2].Name;
                    csv.Desc3Html = descriptors[2].TextHtmlDecoded;
                }
                if (descriptors.Count >= 4)
                {
                    csv.Desc4Name = descriptors[3].Name;
                    csv.Desc4Html = descriptors[3].TextHtmlDecoded;
                }
                if (descriptors.Count >= 5)
                {
                    csv.Desc5Name = descriptors[4].Name;
                    csv.Desc5Html = descriptors[4].TextHtmlDecoded;
                }

                csvProducts.Add(csv);
            }

            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment; filename=Product-Export.csv");

            var exporter = new ProductCsvImporter(StoreContext.CurrentStore.Id.Value);
            exporter.ExportProducts(csvProducts, Response.OutputStream);

            Response.Flush();
            Response.End();
        }
    }
}