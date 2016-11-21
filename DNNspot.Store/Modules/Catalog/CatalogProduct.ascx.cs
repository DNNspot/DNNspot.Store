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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using WA.Extensions;

namespace DNNspot.Store.Modules.Catalog
{
    public partial class CatalogProduct : StoreModuleBase
    {
        protected Product product = new Product();
        protected List<ProductPhoto> photoList = new List<ProductPhoto>();
        protected ProductPhoto mainProductPhoto = new ProductPhoto();
        bool widgetShowTotalProductPrice = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (StoreContext.Product != null)
            {
                product = StoreContext.Product;
            }
            else
            {
                int? productIdFromQueryString = WA.Parser.ToInt(Request.QueryString["product"]);
                product = Product.GetProduct(productIdFromQueryString.GetValueOrDefault(-1)) ?? new Product();
            }

            if (product.IsViewable)
            {
                if (product.Id.HasValue)
                {
                    if (product.IsActive.GetValueOrDefault(false))
                    {
                        // Set the META info
                        if (!string.IsNullOrEmpty(product.SeoTitle))
                        {
                            DnnPage.Title = product.SeoTitle;
                        }
                        if (!string.IsNullOrEmpty(product.SeoDescription))
                        {
                            DnnPage.Description = product.SeoDescription;
                        }
                        if (!string.IsNullOrEmpty(product.SeoKeywords))
                        {
                            DnnPage.KeyWords = product.SeoKeywords;
                        }

                        var relatedProducts = product.GetRelatedProducts();
                        rptRelatedProducts.DataSource = relatedProducts;
                        rptRelatedProducts.DataBind();

                        pnlRelatedProducts.Visible = relatedProducts.Count != 0;
                    }
                    else
                    {
                        pnlProductNotActive.Visible = true;
                        pnlProduct.Visible = !pnlProductNotActive.Visible;
                    }
                }

                else
                {
                    pnlProduct.Visible = false;
                }

            }
            else
            {
                pnlProductNotViewable.Visible = true;
                pnlProduct.Visible = !pnlProductNotViewable.Visible;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            //RegisterJavascriptFileInHeader(ModuleRootWebPath + "js/jquery.popeye-1.1.js");
            RegisterJavascriptFileOnceInBody("js/jquery.fancybox-1.3.1.min.js", ModuleRootWebPath + "js/jquery.fancybox-1.3.1.min.js");

            // jQuery UI - Tabs
            
            bool includeJqueryUI = WA.Parser.ToBool(DataModel.Store.GetStoreByPortalId(PortalId).GetSetting(StoreSettingNames.IncludeJQueryUi)).GetValueOrDefault(true);
            if (includeJqueryUI)
            {
                RegisterJavascriptFileOnceInBody("js/jquery-ui-1.9.1.custom.min.js", ModuleRootWebPath + "js/jquery-ui-1.9.1.custom.min.js");
            }
            RegisterCssFileOnceInHeader(ModuleRootWebPath + "css/jquery.ui.css", string.Empty);

            RegisterJavascriptFileOnceInBody("js/jquery.validate.min.js", ModuleRootWebPath + "js/jquery.validate.min.js");
            RegisterJavascriptFileOnceInBody("js/jquery.equalHeights.js", ModuleRootWebPath + "js/jquery.equalHeights.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //btnAddToCart.ImageUrl = ModuleRootImagePath + "btnAddToCart.png";

            if (!IsPostBack && pnlProduct.Visible)
            {
                LoadPhotos();
                LoadCustomFields();
                LoadDescriptors();
            }
        }

        private void LoadPhotos()
        {
            mainProductPhoto = product.GetMainPhoto();

            photoList = product.GetAllPhotosInSortOrder();

            rptPhotoList.DataSource = photoList;
            rptPhotoList.DataBind();
        }

        private void LoadCustomFields()
        {
            List<ProductField> productFields = product.GetProductFieldsInSortOrder();
            if (productFields.Count > 0)
            {
                litProductFields.Text = GetWidgetHtml(productFields, "li", "productField");
            }
        }

        private void LoadDescriptors()
        {
            List<ProductDescriptor> descriptors = product.GetProductDescriptors();
            if (descriptors.Count > 0)
            {
                rptDescriptorTabNames.DataSource = descriptors;
                rptDescriptorTabNames.DataBind();

                rptDescriptorTabContents.DataSource = descriptors;
                rptDescriptorTabContents.DataBind();

                pnlTabWidget.Visible = true;
            }
        }

        protected List<string> GetProductCategoryLinks()
        {
            return product.GetCategories(false).ConvertAll(c => string.Format(@"<a href=""{0}"">{1}</a>", StoreUrls.Category(c), c.Name));
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            bool IsAvailableForPurchase = product.IsPurchaseableByUser;
            if (IsAvailableForPurchase)
            {
                int productId = product.Id.Value;
                int quantity = WA.Parser.ToInt(txtQuantity.Text).GetValueOrDefault(1);

                if ((product.InventoryIsEnabled == true && quantity < product.InventoryQtyInStock.Value) || product.InventoryIsEnabled == false)
                {
                    string jsonProductFieldData = "";
                    List<JsonProductFieldData> productFieldsData = GetPostedWidgetValues(Request);
                    if (productFieldsData.Count > 0)
                    {
                        jsonProductFieldData = Newtonsoft.Json.JsonConvert.SerializeObject(productFieldsData);
                    }

                    CartController cartController = new CartController(StoreContext);
                    cartController.AddProductToCart(productId, quantity, jsonProductFieldData);

                    //var checkoutOrderInfo = Session[StoreContext.SessionKeys.CheckoutOrderInfo] as CheckoutOrderInfo ?? new CheckoutOrderInfo() { Cart = cartController.GetCart(false) };
                    //checkoutOrderInfo.ReCalculateOrderTotals();
                    //Session[StoreContext.SessionKeys.CheckoutOrderInfo] = checkoutOrderInfo;

                    Response.Redirect(StoreUrls.Cart(string.Format(@"""{0}"" has been added to your cart", product.Name)));
                }
                else
                {
                    ShowFlash("This product is out of stock");
                }
            }
            else
            {
                ShowFlash("This product is no longer available for purchase");
            }
        }

        #region Custom Fields

        static readonly Regex rxInputName = new Regex("productField-(\\d+)-(.*)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private List<JsonProductFieldData> GetPostedWidgetValues(HttpRequest request)
        {
            List<JsonProductFieldData> fieldData = new List<JsonProductFieldData>();

            NameValueCollection postedForm = request.Form;
            foreach (string key in postedForm.Keys)
            {
                Match m = rxInputName.Match(key);
                if (m.Success && m.Groups.Count > 1)
                {
                    int? productFieldId = WA.Parser.ToInt(m.Groups[1].Value);

                    string postedValue = postedForm.Get(key);
                    if (!string.IsNullOrEmpty(postedValue))
                    {
                        ProductField productField = new ProductField();
                        if (productField.LoadByPrimaryKey(productFieldId.GetValueOrDefault(-1)))
                        {
                            decimal priceAdjust = 0;
                            decimal weightAdjust = 0;

                            List<string> postedChoiceValues = new List<string>(postedForm.GetValues(key) ?? new string[] { });
                            // we need to special-case the "Checkbox" type and provide a better "choice value" than "on" (browser default)
                            if (productField.WidgetTypeName == ProductFieldWidgetType.Checkbox)
                            {
                                postedChoiceValues = new List<string>() { "Yes" };
                            }

                            List<ProductFieldChoice> fieldChoices = productField.GetChoicesInSortOrder().ToList();
                            if (fieldChoices.Count > 0)
                            {
                                // widget has choices
                                fieldChoices.RemoveAll(c => !postedChoiceValues.Contains(c.Value));
                                foreach (ProductFieldChoice fieldChoice in fieldChoices)
                                {
                                    priceAdjust += fieldChoice.PriceAdjustment.GetValueOrDefault(0);
                                    weightAdjust += fieldChoice.WeightAdjustment.GetValueOrDefault(0);
                                }
                            }
                            else
                            {
                                priceAdjust = productField.PriceAdjustment.GetValueOrDefault(0);
                                weightAdjust = productField.WeightAdjustment.GetValueOrDefault(0);
                            }

                            JsonProductFieldData jsonProductField = new JsonProductFieldData()
                            {
                                ProductFieldId = productField.Id.Value,
                                Name = productField.Name,
                                Slug = productField.Slug,
                                PriceAdjustment = priceAdjust,
                                WeightAdjustment = weightAdjust,
                                ChoiceValues = postedChoiceValues
                            };
                            fieldData.Add(jsonProductField);
                        }
                    }
                }
            }

            return fieldData;
        }

        private string GetWidgetHtml(List<ProductField> productFields, string htmlTagWrapper, string htmlTagCssClass)
        {
            if (string.IsNullOrEmpty(htmlTagWrapper))
            {
                throw new ArgumentException("htmlTagWrapper must not be empty");
            }
            StringBuilder html = new StringBuilder();
            widgetShowTotalProductPrice = (productFields.Count == 1);

            foreach (ProductField productField in productFields)
            {
                string inputName = string.Format(@"productField-{0}-{1}", productField.Id, productField.Slug);
                string inputId = inputName;
                string label = string.Format(@"<label for=""{0}"">{1}</label>", inputId, productField.Name);
                string attrIdNameCss = string.Format(@"id=""{0}"" name=""{1}"" class=""{2} {3}""", inputId, inputName, productField.WidgetType, productField.IsRequired.GetValueOrDefault() ? "required" : "");
                if (productField.IsRequired.GetValueOrDefault())
                {
                    attrIdNameCss += string.Format(@" title=""{0} is required""", productField.Name);
                }

                html.AppendFormat(@"<{0} class=""{1}"">", htmlTagWrapper, htmlTagCssClass);
                switch (productField.WidgetTypeName)
                {
                    case ProductFieldWidgetType.Textbox:
                        // label
                        html.Append(label);
                        // input
                        html.AppendFormat(@"<span> <input type=""text"" {0} /> </span>", attrIdNameCss);
                        break;
                    case ProductFieldWidgetType.Textarea:
                        // label
                        html.Append(label);
                        // input
                        html.AppendFormat(@"<span> <textarea {0}></textarea> </span>", attrIdNameCss);
                        break;
                    case ProductFieldWidgetType.Checkbox:
                        // label
                        html.Append(label);
                        // input
                        html.AppendFormat(@"<span class=""Checkbox""> <input type=""checkbox"" {0} /> </span>", attrIdNameCss);
                        break;
                    case ProductFieldWidgetType.DropdownList:
                        // label
                        html.Append(label);
                        // input                        
                        html.AppendFormat(@"<span> <select {0}>", attrIdNameCss);
                        List<ProductFieldChoice> dropDownChoices = productField.GetChoicesInSortOrder().ToList();
                        dropDownChoices.ForEach(c => html.AppendFormat(@"<option value=""{0}"">{1}</option>", c.Value, GetChoiceTextHtml(c, widgetShowTotalProductPrice)));
                        html.Append("</select> </span>");
                        break;
                    case ProductFieldWidgetType.RadioButtonList:
                        // label
                        html.Append(label);
                        // input                        
                        html.AppendFormat(@"<span {0}>", attrIdNameCss);

                        List<ProductFieldChoice> radioChoices = productField.GetChoicesInSortOrder().ToList();
                        GenerateRadioCheckboxChoicesInList(html, productField, "radio", radioChoices, inputName);

                        html.Append("</span>");
                        break;
                    case ProductFieldWidgetType.CheckboxList:
                        // label
                        html.Append(label);
                        // input                        
                        html.AppendFormat(@"<span {0}>", attrIdNameCss);

                        List<ProductFieldChoice> checkboxChoices = productField.GetChoicesInSortOrder().ToList();
                        GenerateRadioCheckboxChoicesInList(html, productField, "checkbox", checkboxChoices, inputName);

                        html.Append("</span>");
                        break;
                }
                html.AppendFormat(@"</{0}>", htmlTagWrapper);
                html.Append(Environment.NewLine);
            }
            return html.ToString();
        }

        private void GenerateRadioCheckboxChoicesInList(StringBuilder html, ProductField productField, string inputType, List<ProductFieldChoice> choices, string inputName)
        {
            choices.ForEach(c =>
                html.AppendFormat(@"<span> <input type=""{4}"" id=""{0}"" name=""{1}"" value=""{2}"" {5} /> <label for=""{0}"">{3}</label> </span>"
                    , inputName + "-" + c.Id
                    , inputName
                    , c.Value
                    , GetChoiceTextHtml(c, (widgetShowTotalProductPrice && inputType != "checkbox"))
                    , inputType
                    , productField.IsRequired.GetValueOrDefault() ? string.Format(@"class=""required"" title=""{0} is required""", productField.Name) : ""
                )
            );
        }

        private string GetChoiceTextHtml(ProductFieldChoice choice, bool showTotalProductPrice)
        {
            decimal adjValue = choice.PriceAdjustment.GetValueOrDefault(0);
            if (adjValue != 0)
            {
                if (showTotalProductPrice)
                {
                    // show total product price with adjustment applied
                    return string.Format(@"{0} ({1})", choice.Name, StoreContext.CurrentStore.FormatCurrency(product.GetPrice() + adjValue));
                }
                else
                {
                    // show adjustment amount only e.g. "+ $5.00"
                    return string.Format(@"{0} ({1} {2})", choice.Name, adjValue >= 0 ? "+" : "-", StoreContext.CurrentStore.FormatCurrency(Math.Abs(adjValue)));
                }
            }
            else
            {
                return choice.Name;
            }
        }

        #endregion
    }
}