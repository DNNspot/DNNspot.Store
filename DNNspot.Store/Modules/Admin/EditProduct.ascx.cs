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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DotNetNuke.Security.Roles;
using DotNetNuke.UI.UserControls;
using EntitySpaces.Interfaces;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class EditProduct : StoreAdminModuleBase
    {
        protected Product product = new Product();
        private List<int> productCategoryIds = new List<int>();
        protected bool isEditMode = false;
        protected Dictionary<int, CheckoutRoleInfo> productCheckoutRoleInfos = new Dictionary<int, CheckoutRoleInfo>();
        public string PhotoUploadFolder = String.Empty;

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
                       {
                           new AdminBreadcrumbLink() { Text = "Products", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Products) },
                           new AdminBreadcrumbLink() { Text = "Add / Edit Product" }
                       };
        }

        protected int? ParamId
        {
            get { return WA.Parser.ToInt(Request.QueryString["id"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PhotoUploadFolder = ModuleRootWebPath.MakeRelative();
            if (!IsPostBack)
            {
                //--- Product Deletion
                int? deleteId = WA.Parser.ToInt(Request.QueryString["delete"]);
                if (deleteId.HasValue && product.LoadByPrimaryKey(deleteId.Value))
                {
                    product.MarkAsDeleted();
                    product.RelatedProductCollectionByProductId.MarkAllAsDeleted();
                    product.Save();

                    Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.Products));
                }

                if (ParamId.HasValue && product.LoadByPrimaryKey(ParamId.GetValueOrDefault()))
                {
                    isEditMode = true;

                    if (WA.Parser.ToBool(Request.QueryString["deleteFile"]).GetValueOrDefault(false))
                    {
                        // Delete the digital file for this product
                        string filenameToDelete = product.DigitalFilename;
                        product.DigitalFilename = "";
                        product.DigitalFileDisplayName = "";
                        product.DeliveryMethodId = 1;
                        product.Save();

                        if (!string.IsNullOrEmpty(filenameToDelete))
                        {
                            File.Delete(StoreUrls.ProductFileFolderFileRoot + filenameToDelete);
                        }
                        Response.Redirect(StoreUrls.AdminEditProduct(product.Id.Value, "Digital file was deleted"));
                    }

                    productCategoryIds = product.GetCategories(true).ConvertAll(c => c.Id.Value);

                    PopulateListControls(isEditMode);

                    FillForEdit();
                }
                else
                {
                    product = new Product();
                    isEditMode = false;
                    PopulateListControls(isEditMode);
                }

                LoadProducts();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // jQuery TOOLS - Tabs            
            //RegisterJavascriptFileInHeader(ModuleRootWebPath + "js/jquery.tools.tabs.min.js");

            // jQuery Uploadify
            RegisterJavascriptFileOnceInBody("uploadify/swfobject.js", ModuleRootWebPath + "uploadify/swfobject.js");
            RegisterJavascriptFileOnceInBody("uploadify/jquery.uploadify.v2.1.0.min.js", ModuleRootWebPath + "uploadify/jquery.uploadify.v2.1.0.min.js");

            RegisterCssFileInHeader(ModuleRootWebPath + "uploadify/uploadify.css");

            RegisterJavascriptFileOnceInBody("js/jquery.jtemplates.js", ModuleRootWebPath + "js/jquery.jtemplates.js");
        }

        private void PopulateListControls(bool isEditMode)
        {
            // Delivery Methods
            List<ListItem> deliveryMethods = DeliveryMethodCollection.LoadAllToList().ConvertAll(d => d.ToListItem());
            rdoDeliveryMethod.Items.Clear();
            rdoDeliveryMethod.Items.AddRange(deliveryMethods.ToArray());
            rdoDeliveryMethod.SelectedIndex = 0;

            // Categories
            CategoryTreeRenderer treeRenderer = new CategoryTreeRenderer(
                StoreContext.CurrentStore.Id.GetValueOrDefault(-1),
                c => string.Format(@"<span> <input type=""checkbox"" id=""cat-{0}"" name=""productCategory"" value=""{0}"" {2} /> <label for=""cat-{0}"">{1}</label> </span>", c.Id, c.Name, GetProductCategoryCheckedAttribute(c.Id.Value))
                );
            treeRenderer.CssClassForOuterList = "form";
            treeRenderer.IncludeHiddenCategories = true;
            litCategories.Text = treeRenderer.RenderHtmlList();

            // Descriptors
            List<ProductDescriptor> descriptors = product.GetProductDescriptors();
            if (descriptors.Count >= 1)
            {
                litDescriptorName1.Text = descriptors[0].Name;
                txtDescriptorName1.Text = descriptors[0].Name;
                (txtDescriptorText1 as DotNetNuke.UI.UserControls.TextEditor).Text = descriptors[0].Text;
            }
            if (descriptors.Count >= 2)
            {
                litDescriptorName2.Text = descriptors[1].Name;
                txtDescriptorName2.Text = descriptors[1].Name;
                (txtDescriptorText2 as DotNetNuke.UI.UserControls.TextEditor).Text = descriptors[1].Text;
            }
            if (descriptors.Count >= 3)
            {
                litDescriptorName3.Text = descriptors[2].Name;
                txtDescriptorName3.Text = descriptors[2].Name;
                (txtDescriptorText3 as DotNetNuke.UI.UserControls.TextEditor).Text = descriptors[2].Text;
            }
            if (descriptors.Count >= 4)
            {
                litDescriptorName4.Text = descriptors[3].Name;
                txtDescriptorName4.Text = descriptors[3].Name;
                (txtDescriptorText4 as DotNetNuke.UI.UserControls.TextEditor).Text = descriptors[3].Text;
            }
            if (descriptors.Count >= 5)
            {
                litDescriptorName5.Text = descriptors[4].Name;
                txtDescriptorName5.Text = descriptors[4].Name;
                (txtDescriptorText5 as DotNetNuke.UI.UserControls.TextEditor).Text = descriptors[4].Text;
            }

            //--- DNN roles & Add Role After Checkout
            RoleController roleController = new RoleController();
            List<RoleInfo> roleInfos = roleController.GetPortalRoles(PortalId).ToList<RoleInfo>();
            StringBuilder checkoutRolesUi = new StringBuilder();
            List<CheckoutRoleInfo> checkoutRoleInfos = product.GetCheckoutRoleInfos();

            bool isSelected = false;

            foreach (RoleInfo role in roleInfos)
            {
                CheckoutRoleInfo selectedRole = checkoutRoleInfos.Find(cr => cr.RoleId == role.RoleID);
                isSelected = (selectedRole != null);
                int? expireDays = (selectedRole != null) ? selectedRole.ExpireDays : null;

                checkoutRolesUi.AppendFormat(@"
                    <tr>
                        <td>
                            <input type=""checkbox"" id=""checkoutAssignRole-{0}"" name=""checkoutAssignRole"" value=""{0}"" {1} />
                            <label for=""checkoutAssignRole-{0}"">{2}</label>                                                    
                        </td>
                        <td>
                            <input type=""text"" name=""checkoutAssignRole-{0}-ExpireDays"" style=""width: 50px;"" value=""{3}"" /> <span class=""inputHelp"">days</span>
                        </td>
                    </tr>
                ",
                 role.RoleID,
                 isSelected ? @"checked=""checked""" : "",
                 role.RoleName,
                 expireDays
                );
            }
            litCheckoutRolesPickerUi.Text = checkoutRolesUi.ToString();


            //--- DNN roles -- VIEW PERMISSIONS
            StringBuilder viewPermissionsRolesUi = new StringBuilder();
            List<int> viewPermission = new List<int>();

            if (!String.IsNullOrEmpty(product.ViewPermissions))
            {
                viewPermission = product.ViewPermissions.ToListOfInt(",");
            }

            isSelected = viewPermission.Any(a => a == -1);

            viewPermissionsRolesUi.AppendFormat(@"
                    <tr>
                        <td>
                            <input type=""checkbox"" id=""viewPermissionId-{0}"" name=""viewPermissionRole"" value=""{0}"" {1} />
                            <label for=""viewPermissionId-{0}"">{2}</label>                                                    
                        </td>
                    </tr>
                ",
             -1,
             isSelected ? @"checked=""checked""" : "",
             "All Users"
            );

            foreach (RoleInfo role in roleInfos)
            {
                if (role.RoleType != RoleType.Administrator)
                {
                    isSelected = viewPermission.Any(a => a == role.RoleID);

                    viewPermissionsRolesUi.AppendFormat(
                        @"
                    <tr>
                        <td>
                            <input type=""checkbox"" id=""viewPermissionId-{0}"" name=""viewPermissionRole"" value=""{0}"" {1} />
                            <label for=""viewPermissionId-{0}"">{2}</label>                                                    
                        </td>
                    </tr>
                ",
                        role.RoleID,
                        isSelected ? @"checked=""checked""" : "",
                        role.RoleName
                        );
                }
            }

            litViewPermissions.Text = viewPermissionsRolesUi.ToString();

            //--- DNN roles -- CART PERMISSIONS
            StringBuilder cartPermissionsRolesUi = new StringBuilder();
            List<int> cartPermission = new List<int>();

            if (!String.IsNullOrEmpty(product.CheckoutPermissions))
            {
                cartPermission = product.CheckoutPermissions.ToListOfInt(",");
            }

            isSelected = cartPermission.Any(a => a == -1);

            cartPermissionsRolesUi.AppendFormat(@"
                    <tr>
                        <td>
                            <input type=""checkbox"" id=""cartPermissionId-{0}"" name=""cartPermissionRole"" value=""{0}"" {1} />
                            <label for=""cartPermissionId-{0}"">{2}</label>                                                    
                        </td>
                    </tr>
                ",
                 -1,
                 isSelected ? @"checked=""checked""" : "",
                 "All Users"
                );

            foreach (RoleInfo role in roleInfos)
            {
                if (role.RoleType != RoleType.Administrator)
                {
                    isSelected = cartPermission.Any(a => a == role.RoleID);

                    cartPermissionsRolesUi.AppendFormat(
                        @"
                    <tr>
                        <td>
                            <input type=""checkbox"" id=""cartPermissionId-{0}"" name=""cartPermissionRole"" value=""{0}"" {1} />
                            <label for=""cartPermissionId-{0}"">{2}</label>                                                    
                        </td>
                    </tr>
                ",
                        role.RoleID,
                        isSelected ? @"checked=""checked""" : "",
                        role.RoleName
                        );
                }
            }

            litCartPermissions.Text = cartPermissionsRolesUi.ToString();
        }

        private string GetProductCategoryCheckedAttribute(int categoryId)
        {
            if (productCategoryIds.Contains(categoryId))
            {
                return "checked=\"checked\"";
            }
            return "";
        }

        private void FillForEdit()
        {
            txtName.Text = product.Name;
            txtSlug.Text = product.Slug;
            chkIsActive.Checked = product.IsActive.GetValueOrDefault(false);
            txtSku.Text = product.Sku;
            //txtPrice.Text = product.Price.HasValue ? product.Price.Value.ToString("F2") : "";
            // Fix issue with other languages wanting to use a comma instead of a period
            txtPrice.Text = product.Price.HasValue ? product.Price.Value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US")) : "";

            //txtWeight.Text = product.Weight.Value.ToString("F2");
            txtWeight.Text = product.Weight.HasValue ? product.Weight.Value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US")) : "";
            txtLength.Text = product.Length.HasValue ? product.Length.Value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US")) : "";
            txtWidth.Text = product.Width.HasValue ? product.Width.Value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US")) : "";
            txtHeight.Text = product.Height.HasValue ? product.Height.Value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US")) : "";

            txtSpecialNotes.Text = product.SpecialNotes.BrToNewline();
            chkIsTaxable.Checked = product.IsTaxable.GetValueOrDefault(true);
            chkIsPriceDisplayed.Checked = product.IsPriceDisplayed.GetValueOrDefault(true);
            chkIsAvailableForPurchase.Checked = product.IsAvailableForPurchase.GetValueOrDefault(true);

            rdoDeliveryMethod.TrySetSelectedValue(product.DeliveryMethodId.GetValueOrDefault().ToString());

            txtAdditionalShippingFeePerItem.Text = product.ShippingAdditionalFeePerItem.HasValue ? product.ShippingAdditionalFeePerItem.Value.ToString("F2", CultureInfo.CreateSpecificCulture("en-US")) : "";

            rdoQuantityWidget.SelectedValue = product.QuantityWidget;
            txtQuantityOptions.Text = product.QuantityOptions;

            chkInventoryIsEnabled.Checked = product.InventoryIsEnabled.GetValueOrDefault(false);
            chkInventoryAllowNegativeStockLevel.Checked = product.InventoryAllowNegativeStockLevel.GetValueOrDefault(false);
            txtInventoryQtyInStock.Text = product.InventoryQtyInStock.HasValue ? product.InventoryQtyInStock.Value.ToString() : "";
            txtInventoryQtyLowThreshold.Text = product.InventoryQtyLowThreshold.HasValue ? product.InventoryQtyLowThreshold.Value.ToString() : "";

            txtSeoTitle.Text = product.SeoTitle;
            txtSeoDescription.Text = product.SeoDescription;
            txtSeoKeywords.Text = product.SeoKeywords;

            //---- Photos
            // loaded via ajax

            //---- Custom Fields
            List<ProductField> productFields = product.GetProductFieldsInSortOrder();
            rptCustomFields.DataSource = productFields;
            rptCustomFields.DataBind();

            //--- DNN roles
            // TODO - set the selected values for roles / expirations
            //chklAssignRoleIds.SetSelectedValues(product.CheckoutAssignRoleIds.Split(','));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isNew = false;

            Product toSave = new Product();
            if (!toSave.LoadByPrimaryKey(ParamId.GetValueOrDefault(-1)))
            {
                isNew = true;
                toSave.Name = txtName.Text;
                toSave.StoreId = StoreContext.CurrentStore.Id;
            }

            string slug = txtSlug.Text.CreateSlug();
            if (toSave.Slug != slug)
            {
                if (!SlugFactory.IsSlugAvailable(StoreContext.CurrentStore.Id.Value, slug))
                {
                    ShowFlash(string.Format(@"The URL name ""{0}"" is already in use for this store, please choose another.", slug));
                    return;
                }
            }

            if (toSave.Name.Trim() != txtName.Text.Trim())
            {
                var existingProductByName = Product.GetByName(StoreContext.CurrentStore.Id.Value, txtName.Text.Trim());
                if (existingProductByName != null)
                {
                    ShowFlash(string.Format(@"The product name ""{0}"" already exists in this store, please chose another product name", existingProductByName.Name));
                    return;
                }
            }

            string sku = txtSku.Text;
            if (!string.IsNullOrEmpty(sku))
            {
                if (!RegexPatterns.IsValidSku.IsMatch(sku))
                {
                    ShowFlash(string.Format(@"The Sku '{0}' is invalid, it must match the pattern '{1}'", sku, RegexPatterns.IsValidSku));
                    return;
                }
                sku = sku.Trim();
            }

            toSave.Name = txtName.Text.Trim();
            toSave.Slug = slug;
            toSave.IsActive = chkIsActive.Checked;
            toSave.Price = Convert.ToDecimal(txtPrice.Text, CultureInfo.CreateSpecificCulture("en-US"));
            toSave.Sku = sku;
            toSave.SpecialNotes = txtSpecialNotes.Text.NewlineToBr();
            toSave.IsTaxable = chkIsTaxable.Checked;
            toSave.IsPriceDisplayed = chkIsPriceDisplayed.Checked;
            toSave.IsAvailableForPurchase = chkIsAvailableForPurchase.Checked;
            toSave.DeliveryMethodId = WA.Parser.ToShort(rdoDeliveryMethod.SelectedValue);
            if (toSave.DeliveryMethodId.Value == (short)ProductDeliveryMethod.Shipped)
            {
                toSave.ShippingAdditionalFeePerItem = string.IsNullOrEmpty(txtAdditionalShippingFeePerItem.Text) ? 0 : Convert.ToDecimal(txtAdditionalShippingFeePerItem.Text, CultureInfo.CreateSpecificCulture("en-US"));

                toSave.Weight = String.IsNullOrEmpty(txtWeight.Text) ? 0 : Convert.ToDecimal(txtWeight.Text, CultureInfo.CreateSpecificCulture("en-US"));
                toSave.Length = String.IsNullOrEmpty(txtLength.Text) ? 0 : Convert.ToDecimal(txtLength.Text, CultureInfo.CreateSpecificCulture("en-US"));
                toSave.Width = String.IsNullOrEmpty(txtWidth.Text) ? 0 : Convert.ToDecimal(txtWidth.Text, CultureInfo.CreateSpecificCulture("en-US"));
                toSave.Height = String.IsNullOrEmpty(txtHeight.Text) ? 0 : Convert.ToDecimal(txtHeight.Text, CultureInfo.CreateSpecificCulture("en-US"));
            }
            else if (toSave.DeliveryMethodId.Value == (short)ProductDeliveryMethod.Downloaded)
            {
                toSave.Weight = 0;
                toSave.Length = null;
                toSave.Width = null;
                toSave.Height = null;
                toSave.ShippingAdditionalFeePerItem = 0;
            }

            toSave.QuantityWidget = rdoQuantityWidget.SelectedValue;
            toSave.QuantityOptions = txtQuantityOptions.Text;

            toSave.InventoryIsEnabled = chkInventoryIsEnabled.Checked;
            toSave.InventoryAllowNegativeStockLevel = chkInventoryAllowNegativeStockLevel.Checked;
            toSave.InventoryQtyInStock = WA.Parser.ToInt(txtInventoryQtyInStock.Text);
            toSave.InventoryQtyLowThreshold = WA.Parser.ToInt(txtInventoryQtyLowThreshold.Text);

            toSave.SeoTitle = txtSeoTitle.Text;
            toSave.SeoDescription = txtSeoDescription.Text;
            toSave.SeoKeywords = txtSeoKeywords.Text;

            toSave.CheckoutAssignRoleInfoJson = ParseCheckoutRoleInfoFromPost();
            toSave.ViewPermissions = ParseViewPermissionInfoFromPost();
            toSave.CheckoutPermissions = ParseCheckoutPermissionInfoFromPost();


            toSave.Save();

            //---- Product Categories
            List<string> productCategoryIdStrings = new List<string>(Request.Form.GetValues("productCategory") ?? new string[] { });
            List<int?> productCategoryIds = productCategoryIdStrings.ConvertAll(s => WA.Parser.ToInt(s));
            productCategoryIds.RemoveAll(i => !i.HasValue);
            Product.SetCategories(toSave.Id.Value, productCategoryIds.ConvertAll(i => i.Value));

            //---- Related Products
            DeletePreviousRelatedProducts(toSave);

            var relatedlatedProducts = new RelatedProductCollection();
            foreach (ListItem p in cblRelatedProducts.Items)
            {
                if (!p.Selected) continue;
                RelatedProduct relatedProduct = relatedlatedProducts.AddNew();
                relatedProduct.ProductId = toSave.Id;
                relatedProduct.RelatedProductId = Convert.ToInt32(p.Value);
            }

            relatedlatedProducts.Save();


            //---- Digital File upload);
            if (fupDigitalFile.HasFile)
            {
                string fileUploadDirectory = StoreUrls.ProductFileFolderFileRoot;
                string fileExt = Path.GetExtension(fupDigitalFile.PostedFile.FileName);
                string filenameWithExt = string.Format("{0}_{1}{2}", toSave.Id.Value, Guid.NewGuid(), fileExt);
                string filePath = fileUploadDirectory + filenameWithExt;
                //Debug.WriteFormat(@"fileUploadDirectory = ""{0}""", fileUploadDirectory);
                if (!Directory.Exists(fileUploadDirectory))
                {
                    //Debug.WriteFormat(@"creating fileUploadDirectory = ""{0}""", fileUploadDirectory);
                    Directory.CreateDirectory(fileUploadDirectory);
                }
                fupDigitalFile.PostedFile.SaveAs(filePath);

                toSave.DigitalFilename = filenameWithExt;
                toSave.DigitalFileDisplayName = Path.GetFileNameWithoutExtension(fupDigitalFile.PostedFile.FileName).Left(250);

                toSave.Save();
            }

            //---- Photos are saved via ajax (uploaded via ajax, separately, no need to save here)            

            //---- Descriptor Fields
            using (esTransactionScope transaction = new esTransactionScope())
            {
                toSave.ProductDescriptorCollectionByProductId.MarkAllAsDeleted();
                toSave.Save();

                AddDescriptor(txtDescriptorName1.Text, (txtDescriptorText1 as DotNetNuke.UI.UserControls.TextEditor).Text, 1, toSave);
                AddDescriptor(txtDescriptorName2.Text, (txtDescriptorText2 as DotNetNuke.UI.UserControls.TextEditor).Text, 2, toSave);
                AddDescriptor(txtDescriptorName3.Text, (txtDescriptorText3 as DotNetNuke.UI.UserControls.TextEditor).Text, 3, toSave);
                AddDescriptor(txtDescriptorName4.Text, (txtDescriptorText4 as DotNetNuke.UI.UserControls.TextEditor).Text, 4, toSave);
                AddDescriptor(txtDescriptorName5.Text, (txtDescriptorText5 as DotNetNuke.UI.UserControls.TextEditor).Text, 5, toSave);

                toSave.Save();

                transaction.Complete();
            }

            Response.Redirect(StoreUrls.AdminEditProduct(toSave.Id.Value, "Product Saved" + (isNew ? ", you can now add Photos, Descriptions, and Custom Attributes" : "")));
        }

        private static void DeletePreviousRelatedProducts(Product product)
        {
            RelatedProductCollection relatedProducts = new RelatedProductCollection();
            RelatedProductQuery q = new RelatedProductQuery();

            q.Where(q.ProductId == product.Id);
            relatedProducts.Load(q);
            relatedProducts.MarkAllAsDeleted();
            relatedProducts.Save();
        }

        private string ParseCheckoutRoleInfoFromPost()
        {
            //toSave.CheckoutAssignRoleIds = chklAssignRoleIds.GetSelectedValues().ToCsv();     
            Dictionary<int, CheckoutRoleInfo> checkoutRoles = new Dictionary<int, CheckoutRoleInfo>();

            string[] checkedRoleIds = Request.Form.GetValues("checkoutAssignRole") ?? new string[] { };
            foreach (string roleIdStr in checkedRoleIds)
            {
                int roleId = WA.Parser.ToInt(roleIdStr).Value;
                checkoutRoles[roleId] = new CheckoutRoleInfo() { RoleId = roleId };
            }

            NameValueCollection postedForm = Request.Form;
            foreach (string key in postedForm.Keys)
            {
                if (key.StartsWith("checkoutAssignRole-") && key.EndsWith("-ExpireDays"))
                {
                    string[] keyParts = key.Split('-');
                    int? roleId = roleId = WA.Parser.ToInt(keyParts[1]);
                    int? expireDays = WA.Parser.ToInt(postedForm[key]);

                    if (checkoutRoles.ContainsKey(roleId.Value))
                    {
                        checkoutRoles[roleId.Value].ExpireDays = expireDays;
                    }
                }
            }

            List<CheckoutRoleInfo> checkoutRoleInfos = new List<CheckoutRoleInfo>();
            foreach (var pair in checkoutRoles)
            {
                checkoutRoleInfos.Add(pair.Value);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(checkoutRoleInfos);
        }

        private string ParseViewPermissionInfoFromPost()
        {
            //toSave.CheckoutAssignRoleIds = chklAssignRoleIds.GetSelectedValues().ToCsv();     

            string[] checkedRoleIds = Request.Form.GetValues("viewPermissionRole") ?? new string[] { };

            return string.Join(",", checkedRoleIds.ToArray());
        }

        private string ParseCheckoutPermissionInfoFromPost()
        {
            //toSave.CheckoutAssignRoleIds = chklAssignRoleIds.GetSelectedValues().ToCsv();     
            Dictionary<int, CheckoutRoleInfo> checkoutRoles = new Dictionary<int, CheckoutRoleInfo>();

            string[] checkedRoleIds = Request.Form.GetValues("cartPermissionRole") ?? new string[] { };

            return string.Join(",", checkedRoleIds.ToArray());
        }

        private void AddDescriptor(string name, string text, short sortOrder, Product p)
        {
            // always save the 1st tab, check if other tab's text is empty or the FCKEditor "empty" crap
            if ((sortOrder == 1) || (!string.IsNullOrEmpty(text) && text != "&lt;p&gt;&amp;#160;&lt;/p&gt;"))
            {
                ProductDescriptor desc = p.ProductDescriptorCollectionByProductId.AddNew();
                desc.ProductId = p.Id;
                desc.Name = string.IsNullOrEmpty(name) ? "Tab " + sortOrder : name;
                desc.Text = text;
                desc.SortOrder = sortOrder;
            }
        }

        private void LoadProducts()
        {
            DataModel.ProductQuery q = new ProductQuery();
            q.Where(q.StoreId == StoreContext.CurrentStore.Id).OrderBy(q.Name.Ascending);

            ProductCollection products = new ProductCollection();
            if (products.Load(q))
            {
                var relatedProductIds = product.RelatedProductCollectionByProductId.Select(p => Convert.ToString(p.RelatedProductId.Value));
            
                cblRelatedProducts.DataSource = products;
                cblRelatedProducts.DataTextField = "Name";
                cblRelatedProducts.DataValueField = "Id";

                cblRelatedProducts.DataBind();

                foreach (ListItem p in cblRelatedProducts.Items)
                {
                    p.Selected = relatedProductIds.Contains(p.Value);
                }
            }
        }

        protected void rptCustomFields_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ProductField productField = e.Item.DataItem as ProductField;
                Repeater rptFieldChoices = e.Item.FindControl("rptFieldChoices") as Repeater;
                if (productField != null && rptFieldChoices != null)
                {
                    rptFieldChoices.DataSource = productField.GetChoicesInSortOrder();
                    rptFieldChoices.DataBind();

                    HtmlGenericControl liOptions = e.Item.FindControl("liOptions") as HtmlGenericControl;
                    if (liOptions != null && rptFieldChoices.Items.Count == 0)
                    {
                        liOptions.Visible = false;
                    }
                }
            }
        }
    }
}