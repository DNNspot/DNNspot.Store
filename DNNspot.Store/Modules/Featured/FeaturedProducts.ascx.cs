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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using WA.Extensions;

namespace DNNspot.Store.Modules.Featured
{
    public partial class FeaturedProducts : StoreModuleBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Settings[FeaturedSettings.SortBy] == null)
                {
                    litTemplateOutput.Text = "<strong>Please configure this module via Module Settings</strong>";
                }
                else
                {
                    LoadFeaturedProducts();
                }
            }
        }

        private void LoadFeaturedProducts()
        {
            var sortField = ProductSortByField.FromString(Convert.ToString(Settings[FeaturedSettings.SortBy])) ?? new ProductSortByField() { Field = "Name", SortDirection = SortDirection.ASC };
            var categoryIds = Convert.ToString(Settings[FeaturedSettings.CategoryFilterCategoryIds]).Split(',').Select(x => WA.Parser.ToInt(x).GetValueOrDefault(-1)).ToList();
            bool matchAll = Convert.ToString(Settings[FeaturedSettings.CategoryFilterMethod]).ToLower() == "all";

            var products = ProductCollection.FindProductsByCategories(categoryIds, sortField, matchAll);
            int maxProducts = WA.Parser.ToInt(Settings[FeaturedSettings.MaxNumProducts]).GetValueOrDefault(25);

            if (products.Count > maxProducts)
            {
                products = products.Take(maxProducts).ToList();
            }

            string tplHeader = Settings[FeaturedSettings.TemplateHeader].ToString();
            string tplProduct = Settings[FeaturedSettings.TemplateProduct].ToString();
            string tplFooter = Settings[FeaturedSettings.TemplateFooter].ToString();
            string tplNoResults = Settings[FeaturedSettings.TemplateNoResults].ToString();

            if (products.Count == 0)
            {
                litTemplateOutput.Text = tplHeader + tplNoResults + tplFooter;
            }
            else
            {
                var productTokenizer = new ProductTokenValueProvider(StoreContext);
                var templateProcessor = new TemplateProcessor(productTokenizer);
                StringBuilder productsHtml = new StringBuilder(products.Count);
                foreach (var p in products)
                {
                    productTokenizer.Product = p;
                    productsHtml.Append(templateProcessor.ProcessTemplate(tplProduct));
                }
                litTemplateOutput.Text = tplHeader + productsHtml.ToString() + tplFooter;
            }
        }
    }

    internal class ProductTokenValueProvider : ITokenValueProvider
    {
        StoreContext storeContext;
        StoreUrls storeUrls;
        Product product;
        List<ProductDescriptor> descriptors;

        public Product Product
        {
            get { return this.product; }
            set
            {
                this.product = value;
                this.descriptors = this.product.GetProductDescriptors();
            }
        }

        public ProductTokenValueProvider(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            this.storeUrls = new StoreUrls(storeContext);
        }

        public string GetTokenValue(string token, string property, Dictionary<string, string> attributes)
        {
            if (token.Equals("Product"))
            {
                attributes = attributes ?? new Dictionary<string, string>();
                List<Category> parentCategories = null;
                List<Category> productCategories = null;

                switch (property)
                {
                    case "Id":
                        return this.Product.Id.ToString();
                        break;
                    case "Name":
                        return this.Product.Name;
                        break;
                    case "Slug":
                    case "UrlName":
                        return this.Product.Slug;
                        break;
                    case "Price":
                        return this.Product.GetPriceForDisplay();
                        break; ;
                    case "Sku":
                    case "SKU":
                        return this.Product.Sku;
                        break;
                    case "Url":
                    case "URL":
                        return storeUrls.Product(this.product);
                        break;
                    case "AddToCartUrl":
                        return storeUrls.AddProductToCart(this.product);
                        break;
                    case "AddToCartUrlAndBackToPage":
                        return storeUrls.AddProductToCartRedirectToReferrer(this.product);
                        break;
                    case "ParentCategory":
                        productCategories = this.Product.GetCategories(false);
                        parentCategories = null;
                        foreach (var category in productCategories)
                        {
                            parentCategories = category.GetAllParents();

                            if (parentCategories.Any())
                            {
                                break;
                            }
                        }

                        if (parentCategories != null && parentCategories.Any())
                        {
                            return parentCategories.First().Name;
                        }
                        else
                        {
                            return string.Empty;
                        }

                        return this.Product.GetCategories(false).First().GetAllParents().First().Name;
                        break;
                    case "ParentCategoryUrl":

                        productCategories = this.Product.GetCategories(false);
                        parentCategories = null;
                        foreach (var category in productCategories)
                        {
                            parentCategories = category.GetAllParents();

                            if (parentCategories.Any())
                            {
                                break;
                            }
                        }

                        if (parentCategories != null && parentCategories.Any())
                        {
                            return storeUrls.Category(parentCategories.First());
                        }
                        else
                        {
                            return string.Empty;
                        }
                        break;
                    case "Photo":
                        return string.Format(@"<img src=""{0}"" alt=""{1}"" />", GetPhotoUrl(attributes), this.Product.Name);
                        break;
                    case "PhotoUrl":
                        return GetPhotoUrl(attributes);
                        break;
                    case "Description1":
                        return GetDescriptor(1, attributes);
                        break;
                    case "Description2":
                        return GetDescriptor(2, attributes);
                        break;
                    case "Description3":
                        return GetDescriptor(3, attributes);
                        break;
                    case "Description4":
                        return GetDescriptor(4, attributes);
                        break;
                    case "Description5":
                        return GetDescriptor(5, attributes);
                        break;
                    default:
                        return string.Empty;
                }
            }
            return string.Empty;
        }

        private string GetPhotoUrl(Dictionary<string, string> attributes)
        {
            int? w = attributes.ContainsKey("Width") ? WA.Parser.ToInt(attributes["Width"]) : null;
            int? h = attributes.ContainsKey("Height") ? WA.Parser.ToInt(attributes["Height"]) : null;

            return storeUrls.ProductPhoto(this.Product.GetMainPhoto(), w, h);
        }

        private string GetDescriptor(int descriptorNum, Dictionary<string, string> attributes)
        {
            int descriptorIndex = descriptorNum - 1;
            int? chopAt = attributes.ContainsKey("MaxChars") ? WA.Parser.ToInt(attributes["MaxChars"]) : null;

            if (descriptors.Count >= descriptorNum)
            {
                if (chopAt.HasValue)
                {
                    return descriptors[descriptorIndex].TextHtmlDecoded.ChopAtWithSuffix(chopAt.Value, "...");
                }
                else
                {
                    return descriptors[descriptorIndex].TextHtmlDecoded;
                }
            }
            return string.Empty;
        }
    }
}