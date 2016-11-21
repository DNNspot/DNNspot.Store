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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DNNspot.Store.DataModel;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using WA;
using WA.Extensions;

namespace DNNspot.Store
{
    public class StoreUrls
    {
        readonly StoreContext storeContext;
        readonly PortalSettings portalSettings;
        readonly TabController tabController = new TabController();
        string webPageFileExtension = ".aspx";

        // URL Paths
        string portalUrlRoot;
        string moduleFolderUrlRoot;
        string productPhotoFolderUrlRoot;
        string productFileFolderUrlRoot;
        string shippingLabelFolderUrlRoot;
        string printShippingLabelsUrlBase;
        string printOrderDetailsUrlBase;


        // File Paths        
        string moduleFolderFileRoot;
        string productPhotoFolderFileRoot;
        string productFileFolderFileRoot;
        string shippingLabelFolderFileRoot;

        // Tabs
        TabInfo tabWithMainDispatch = null;
        int tabIdWithStoreAdmin = -1;

        // Others
        bool forceSslCheckout = false;

        public StoreUrls(StoreContext storeContext)
        {
            this.storeContext = storeContext;

            var portalSettings = PortalController.GetCurrentPortalSettings();
            if (portalSettings == null)
            {
                portalSettings = new PortalSettings(storeContext.CurrentStore.PortalId.Value);
                if (portalSettings.PortalAlias == null)
                {
                    PortalAliasController portalAliasController = new PortalAliasController();
                    var aliases = portalAliasController.GetPortalAliasByPortalID(storeContext.CurrentStore.PortalId.Value);
                    foreach (DictionaryEntry entry in aliases)
                    {
                        portalSettings.PortalAlias = entry.Value as PortalAliasInfo;
                        break;
                    }
                }
            }
            this.portalSettings = portalSettings;

            forceSslCheckout = WA.Parser.ToBool(storeContext.CurrentStore.GetSetting(StoreSettingNames.ForceSslCheckout)).GetValueOrDefault(false)
                                    || portalSettings.SSLEnabled;

            //--- Determine some URLs and Paths
            GrabUrlsAndPaths();

            // Get some info about tabs containing our modules... 
            GrabTabsWithOurModules();
        }

        private void GrabUrlsAndPaths()
        {
            HttpRequest request = HttpContext.Current.Request;

            // File Paths
            //string dnnFilePathRoot = request.PhysicalApplicationPath.EnsureEndsWith(@"\"); // e.g. "D:\Hosting\5541406\html\dotnetnuke\"
            //moduleFolderFileRoot = dnnFilePathRoot + string.Format(@"DesktopModules\{0}\", ModuleDefs.FolderName);            
            moduleFolderFileRoot = GetModuleFolderFileRoot();
            productPhotoFolderFileRoot = GetProductPhotoFolderFileRoot();
            productFileFolderFileRoot = moduleFolderFileRoot + @"ProductFiles\";
            shippingLabelFolderFileRoot = moduleFolderFileRoot + @"ShippingLabels\";
            //Debug.WriteFormat(@"File Paths ::: moduleFolderFileRoot = ""{0}""", moduleFolderFileRoot);
            // Web Paths
            portalUrlRoot = request.Url.GetLeftPart(UriPartial.Scheme) + portalSettings.PortalAlias.HTTPAlias.EnsureEndsWith("/");
            //moduleFolderUrlRoot = (request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath).TrimEnd('/') + string.Format(@"/DesktopModules/{0}/", ModuleDefs.FolderName);            
            moduleFolderUrlRoot = GetModuleFolderUrlRoot();
            productPhotoFolderUrlRoot = moduleFolderUrlRoot + @"ProductPhotos/";
            productFileFolderUrlRoot = moduleFolderUrlRoot + @"ProductFiles/";
            shippingLabelFolderUrlRoot = moduleFolderUrlRoot + @"ShippingLabels/";
            printOrderDetailsUrlBase = moduleFolderUrlRoot + @"Modules/Admin/PrintOrder.aspx?id=";
            printShippingLabelsUrlBase = moduleFolderUrlRoot + @"Modules/Admin/PrintShippingLabels.aspx?id=";

            //Debug.WriteFormat(@"URL Paths ::: portalUrlRoot = ""{0}"" moduleFolderUrlRoot = ""{1}""", portalUrlRoot, moduleFolderUrlRoot);
        }

        public static string GetApplicationUrlRoot()
        {
            HttpRequest request = HttpContext.Current.Request;

            return (request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath).EnsureEndsWith("/");
        }

        public static string GetModuleFolderUrlRoot()
        {
            return GetApplicationUrlRoot().TrimEnd('/') + string.Format(@"/DesktopModules/{0}/", ModuleDefs.FolderName);
        }

        public static string GetModuleFolderFileRoot()
        {
            string dnnFilePathRoot = HttpContext.Current.Request.PhysicalApplicationPath.EnsureEndsWith(@"\"); // e.g. "D:\Hosting\5541406\html\dotnetnuke\"

            return dnnFilePathRoot + string.Format(@"DesktopModules\{0}\", ModuleDefs.FolderName);
        }

        public static string GetProductPhotoFolderFileRoot()
        {
            return GetModuleFolderFileRoot() + @"ProductPhotos\";
        }

        /// <summary>
        /// Load and cache some TabInfo objects that contain our special module definitions
        /// </summary>
        private void GrabTabsWithOurModules()
        {
            // Main Dispatch Tab
            string cacheKeyTabWithMainDispatch = storeContext.CacheKeys.TabWithMainDispatch;
            TabInfo cachedTabWithMainDispatch = CacheHelper.GetCache<TabInfo>(cacheKeyTabWithMainDispatch);
            if (cachedTabWithMainDispatch == null)
            {
                List<TabModuleMatch> dispatchTabs = DnnHelper.GetTabsWithModuleByModuleDefinitionName(portalSettings.PortalId, ModuleDefs.MainDispatch.DefinitionName);
                if (dispatchTabs.Count > 0)
                {
                    tabWithMainDispatch = tabController.GetTab(dispatchTabs[0].TabId, portalSettings.PortalId, false);
                    // cache for 1 minute
                    DataCache.SetCache(cacheKeyTabWithMainDispatch, tabWithMainDispatch, TimeSpan.FromMinutes(1));
                }
            }
            else
            {
                tabWithMainDispatch = cachedTabWithMainDispatch;
            }

            // Store Admin Tab
            string cacheKeyTabWithStoreAdmin = storeContext.CacheKeys.TabWithStoreAdmin;
            int cachedTabIdWithStoreAdmin = CacheHelper.GetCache<int>(cacheKeyTabWithStoreAdmin);
            if (cachedTabIdWithStoreAdmin == null || cachedTabIdWithStoreAdmin <= 0)
            {
                List<TabModuleMatch> adminTabs = DnnHelper.GetTabsWithModuleByModuleDefinitionName(portalSettings.PortalId, ModuleDefs.Admin.DefinitionName);
                List<TabModuleMatch> adminTabs2 = DnnHelper.GetTabsWithModuleByModuleDefinitionName(portalSettings.PortalId, ModuleDefs.Admin.FriendlyName);
                List<TabModuleMatch> tabs = (adminTabs.Count > 0) ? adminTabs : adminTabs2;
                if (tabs.Count > 0)
                {
                    //TabInfo tabWithStoreAdmin = tabController.GetTab(tabs[0].TabId, portalSettings.PortalId, false);
                    //tabIdWithStoreAdmin = tabWithStoreAdmin.TabID;
                    // cache for 1 minute
                    //DataCache.SetCache(cacheKeyTabWithStoreAdmin, tabWithStoreAdmin.TabID, TimeSpan.FromMinutes(1));

                    tabIdWithStoreAdmin = tabs[0].TabId;

                    // cache for 1 minute
                    DataCache.SetCache(cacheKeyTabWithStoreAdmin, tabIdWithStoreAdmin, TimeSpan.FromMinutes(1));
                }
            }
            else
            {
                tabIdWithStoreAdmin = cachedTabIdWithStoreAdmin;
            }
        }

        public string DropDownHandler
        {
            get { return moduleFolderUrlRoot + "Handlers/DropDownHandler.ashx"; }
        }

        public string SlugService
        {
            get { return moduleFolderUrlRoot + "Handlers/SlugService.ashx"; }
        }

        public string AdminUploadifyHandler
        {
            get { return moduleFolderUrlRoot + "Modules/Admin/UploadifyHandler.ashx"; }
        }

        public string AdminAjaxHandler
        {
            get { return moduleFolderUrlRoot + "Modules/Admin/AjaxHandler.ashx"; }
        }

        /// <summary>
        /// URL to the current DNN Portal site (e.g. "http://mywebsite.com/" or "http://mywebsite.com/childportal1/). Will always have a trailing slash.
        /// </summary>
        public string PortalUrlRoot
        {
            get { return portalUrlRoot; }
        }

        public string ModuleFolderUrlRoot
        {
            get { return moduleFolderUrlRoot; }
        }

        public string ProductPhotoFolderUrlRoot
        {
            get { return productPhotoFolderUrlRoot; }
        }

        public string ProductFileFolderUrlRoot
        {
            get { return productFileFolderUrlRoot; }
        }

        public string ShippingLabelFolderUrlRoot
        {
            get { return shippingLabelFolderUrlRoot; }
        }

        public string PrintOrderDetailsUrlBase
        {
            get { return printOrderDetailsUrlBase; }
        }

        public string PrintShippingLabelsUrlBase
        {
            get { return printShippingLabelsUrlBase; }
        }


        public string ModuleFolderFileRoot
        {
            get { return moduleFolderFileRoot; }
        }

        public string ProductPhotoFolderFileRoot
        {
            get { return productPhotoFolderFileRoot; }
        }

        public string ProductFileFolderFileRoot
        {
            get { return productFileFolderFileRoot; }
        }

        public string ShippingLabelFolderFileRoot
        {
            get { return shippingLabelFolderFileRoot; }
        }

        //public string SiteRoot()
        //{
        //    return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        //}

        public string NavigateUrl(int tabId)
        {
            return DotNetNuke.Common.Globals.NavigateURL(tabId);
        }

        public string NavigateUrl(int tabId, string controlKey)
        {
            return DotNetNuke.Common.Globals.NavigateURL(tabId, controlKey);
        }

        public string NavigateUrl(int tabId, string controlKey, params string[] additionalParams)
        {
            return DotNetNuke.Common.Globals.NavigateURL(tabId, controlKey, additionalParams);
        }

        public string CategoryHome()
        {
            //return portalUrlRoot + tabWithMainDispatch.TabName.CreateSlug() + webPageFileExtension;

            return DotNetNuke.Common.Globals.NavigateURL(tabWithMainDispatch.TabID);
        }

        public string Category(int categoryId)
        {
            Category category = new Category();
            category.LoadByPrimaryKey(categoryId);

            return Category(category);
        }

        public string Category(Category category)
        {
            if (!string.IsNullOrEmpty(category.Slug))
            {
                //return string.Format(@"/{0}-t{1}/{2}.aspx", tabWithCatalogMainDispatch.TabName, tabWithCatalogMainDispatch.TabID, category.Slug);

                //List<string> categorySlugs = storeContext.CategoryBreadcrumb.ConvertAll(c => c.Slug);
                //categorySlugs.Remove(category.Slug);

                List<string> categorySlugs = category.GetAllParents().ConvertAll(c => c.Slug);

                return SlugUrl(categorySlugs, category.Slug);
            }
            else
            {
                return CategoryHome();
            }
        }

        public string Category(Category category, ProductSortByField productSortByField)
        {
            return Category(category, productSortByField, 1);
        }

        public string Category(Category category, ProductSortByField productSortByField, int pageNumber)
        {
            if (!string.IsNullOrEmpty(category.Slug))
            {
                //return string.Format(@"/{0}-t{1}/{2}.aspx", tabWithCatalogMainDispatch.TabName, tabWithCatalogMainDispatch.TabID, category.Slug);

                //List<string> categorySlugs = storeContext.CategoryBreadcrumb.ConvertAll(c => c.Slug);
                //categorySlugs.Remove(category.Slug);

                List<string> categorySlugs = category.GetAllParents().ConvertAll(c => c.Slug);

                List<string> otherParams = new List<string>();
                if (!string.IsNullOrEmpty(productSortByField.Field))
                {
                    otherParams.Add("sb=" + productSortByField.GetValueString());
                }
                if (pageNumber > 1)
                {
                    otherParams.Add("pg=" + pageNumber);
                }

                return SlugUrl(categorySlugs, category.Slug, otherParams.ToArray());
            }
            else
            {
                return CategoryHome();
            }
        }

        public string Product(Product product)
        {
            return Product(product.Slug);
        }

        public string Product(string productSlug)
        {
            List<string> categorySlugs = storeContext.CategoryBreadcrumb.ConvertAll(c => c.Slug);

            return SlugUrl(categorySlugs, productSlug);
        }

        private string SlugUrl(string pageNameSlug, params string[] otherQueryParams)
        {
            return SlugUrl(null, pageNameSlug, otherQueryParams);
        }

        private string SlugUrl(IList<string> categorySlugs, string pageNameSlug, params string[] otherQueryParams)
        {
            if (!string.IsNullOrEmpty(pageNameSlug))
            {
                List<string> otherParamList = new List<string>(otherQueryParams);
                string additonalParamString = otherParamList.Count > 0 ? "?" + otherParamList.ToDelimitedString("&") : "";

                string catSlugs = "";
                if (categorySlugs != null && categorySlugs.Count > 0)
                {
                    catSlugs = categorySlugs.ToDelimitedString("/").TrimEnd('/');
                }

                if((categorySlugs != null) && (categorySlugs.Count > 0)) // (!string.IsNullOrEmpty(catSlugs))
                {
                    var catUrl = DotNetNuke.Common.Globals.NavigateURL(tabWithMainDispatch.TabID, false, portalSettings, string.Empty, string.Empty, catSlugs + "/" + pageNameSlug);
                    if (!string.IsNullOrWhiteSpace(additonalParamString))
                    {
                        catUrl += additonalParamString;
                    }
                    return catUrl;
                }
                else
                {
                    //var parms = new List<string>(otherQueryParams);
                    //parms.Insert(0, "slug=" + pageNameSlug);
                    //return DotNetNuke.Common.Globals.NavigateURL(tabWithMainDispatch.TabID, string.Empty, parms.ToArray());

                    var url = DotNetNuke.Common.Globals.NavigateURL(tabWithMainDispatch.TabID, false, portalSettings, string.Empty, string.Empty, pageNameSlug);
                    if(!string.IsNullOrWhiteSpace(additonalParamString))
                    {
                        url += additonalParamString;
                    }
                    return url;
                }
            }
            return "";
        }

        public string ProductFile(string filename)
        {
            return productFileFolderUrlRoot + filename;
        }

        public string ProductPhoto(ProductPhoto photo, int? width, int? height)
        {
            return ProductPhoto(photo != null ? photo.Filename : "", width, height);
        }

        public string ProductPhoto(string photoFilename, int? width, int? height)
        {
            List<string> parms = new List<string>(2);
            if (width.HasValue)
            {
                parms.Add("w=" + width);
            }
            if (height.HasValue)
            {
                parms.Add("h=" + height);
            }
            string queryParams = parms.Count > 0 ? "?" + parms.ToDelimitedString("&") : "";

            if (!string.IsNullOrEmpty(photoFilename))
            {
                return string.Format("{0}{1}.ashx{2}", productPhotoFolderUrlRoot, photoFilename, queryParams);
            }
            return string.Format("{0}images/{1}.ashx{2}", moduleFolderUrlRoot, "photoNotFound.png", queryParams);
        }


        public string AddProductToCart(Product product)
        {
            return SlugUrl("Cart", "add=" + product.Slug);
        }

        public string AddProductToCartRedirectToReferrer(Product product)
        {
            return SlugUrl("Cart", "add=" + product.Slug, "redirect=true");
        }

        public string Cart()
        {
            return SlugUrl("Cart");
        }

        public string Cart(string flashMsg)
        {
            return SlugUrl("Cart", "flash=" + HttpUtility.UrlPathEncode(flashMsg));
        }

        public string CartRemoveCartItem(int cartItemId)
        {
            return SlugUrl("Cart", "remove=" + cartItemId);
        }

        public string LoginPrompt()
        {
            return SlugUrl("LoginPrompt");
        }

        public string Checkout()
        {
            return CheckoutBilling();
        }

        public string CheckoutBilling()
        {
            return forceSslCheckout ? SlugUrl("Checkout-Billing").MakeHttps() : SlugUrl("Checkout-Billing");
        }

        public string CheckoutShipping()
        {
            return forceSslCheckout ? SlugUrl("Checkout-Shipping").MakeHttps() : SlugUrl("Checkout-Shipping");
        }

        public string CheckoutShippingMethod()
        {
            return forceSslCheckout ? SlugUrl("Checkout-ShippingMethod").MakeHttps() : SlugUrl("Checkout-ShippingMethod");
        }

        public string CheckoutPayment()
        {
            return forceSslCheckout ? SlugUrl("Checkout-Payment").MakeHttps() : SlugUrl("Checkout-Payment");
        }

        public string CheckoutReview()
        {
            return forceSslCheckout ? SlugUrl("Checkout-Review").MakeHttps() : SlugUrl("Checkout-Review");
        }

        public string CheckoutCompleteForOrder(Guid completedOrderId)
        {
            return CheckoutCompleteForOrder(completedOrderId, string.Empty);
        }

        public string CheckoutCompleteForOrder(Guid completedOrderId, string flash)
        {
            return CheckoutCompleteForOrder(completedOrderId, flash, null);
        }

        public string CheckoutCompleteForOrder(Guid completedOrderId, string flash, List<string> additionalParams)
        {
            List<string> parms = new List<string>(2);

            parms.Add("dsoid=" + completedOrderId);

            if (!string.IsNullOrEmpty(flash))
            {
                parms.Add("flash=" + HttpUtility.UrlPathEncode(flash));
            }
            if (additionalParams != null)
            {
                parms.AddRange(additionalParams);
            }

            return forceSslCheckout ? SlugUrl("Checkout-Complete", parms.ToArray()).MakeHttps() : SlugUrl("Checkout-Complete", parms.ToArray());
        }

        public string CreateAccount(string returnUrl)
        {
            // DNN 5 only            
            return DotNetNuke.Common.Globals.RegisterURL(returnUrl, string.Empty);

            // DNN 4/5            
            //returnUrl = "returnurl=" + returnUrl.MakeRelative();    // returnUrl MUST BE RELATIVE! or DNN craps out and won't use it...
            //string url = DotNetNuke.Common.Globals.NavigateURL(portalSettings.ActiveTab.TabID, "Register", new string[] { returnUrl });

            //return url;
        }

        public string DispatchView(string viewName)
        {
            return DispatchView(viewName, "");
        }

        public string DispatchView(string viewName, params string[] additionalParams)
        {
            List<string> paramList = new List<string>(additionalParams);
            if (!string.IsNullOrEmpty(viewName))
            {
                paramList.Insert(0, "v=" + viewName);
            }

            return DotNetNuke.Common.Globals.NavigateURL("", paramList.ToArray());
        }

        #region User Urls

        public string UserProfileUrl(int userId)
        {
            // DNN 5.3+ only
            return DotNetNuke.Common.Globals.UserProfileURL(userId);
        }

        #endregion

        #region Admin Urls

        public string Admin(ModuleDefs.Admin.Views adminView)
        {
            return Admin(adminView, "");
        }

        public string AdminWithFlash(ModuleDefs.Admin.Views adminView, string flashMsg)
        {
            return Admin(adminView, "flash=" + HttpUtility.UrlPathEncode(flashMsg));
        }

        public string AdminWithFlash(ModuleDefs.Admin.Views adminView, string flashMsg, params string[] additionalParams)
        {
            List<string> parms = new List<string>(additionalParams);
            parms.Add("flash=" + HttpUtility.UrlPathEncode(flashMsg));

            return Admin(adminView, parms.ToArray());
        }

        public string Admin(ModuleDefs.Admin.Views adminView, params string[] additionalParams)
        {
            List<string> paramList = new List<string>(additionalParams);
            paramList.Insert(0, "v=" + adminView);

            //return DotNetNuke.Common.Globals.NavigateURL("", paramList.ToArray());
            return DotNetNuke.Common.Globals.NavigateURL(tabIdWithStoreAdmin, "", paramList.ToArray());
        }

        public string AdminEditEmailTemplate(short? emailTemplateId)
        {
            List<string> parms = new List<string>();
            parms.Add("id=" + emailTemplateId);

            return Admin(ModuleDefs.Admin.Views.EditEmailTemplate, parms.ToArray());
        }

        public string AdminAddProduct()
        {
            return Admin(ModuleDefs.Admin.Views.EditProduct);
        }

        public string AdminEditProduct(int productId)
        {
            return AdminEditProduct(productId, "");
        }

        public string AdminEditProduct(int productId, string flashMsg)
        {
            List<string> parms = new List<string>();
            parms.Add("id=" + productId);
            if (!string.IsNullOrEmpty(flashMsg))
            {
                parms.Add("flash=" + HttpUtility.UrlPathEncode(flashMsg));
            }

            return Admin(ModuleDefs.Admin.Views.EditProduct, parms.ToArray());
        }

        public string AdminDeleteProductFile(int productId)
        {
            return Admin(ModuleDefs.Admin.Views.EditProduct, "id=" + productId, "deleteFile=true");
        }

        public string AdminDeleteProduct(int productId)
        {
            return Admin(ModuleDefs.Admin.Views.EditProduct, "delete=" + productId);
        }

        public string AdminAddProductField(int productId)
        {
            return Admin(ModuleDefs.Admin.Views.EditProductField, "productId=" + productId);
        }

        public string AdminEditProductField(int productFieldId)
        {
            return Admin(ModuleDefs.Admin.Views.EditProductField, "id=" + productFieldId);
        }

        public string AdminDeleteProductField(int productFieldId)
        {
            return Admin(ModuleDefs.Admin.Views.EditProductField, "delete=" + productFieldId);
        }

        public string AdminAddCategory()
        {
            return Admin(ModuleDefs.Admin.Views.EditCategory);
        }

        public string AdminEditCategory(int categoryId)
        {
            return AdminEditCategory(categoryId, "");
        }

        public string AdminEditCategory(int categoryId, string flashMsg)
        {
            //List<string> parms = new List<string>();
            //parms.Add("id=" + categoryId);
            //if (!string.IsNullOrEmpty(flashMsg))
            //{
            //    parms.Add("flash=" + HttpUtility.UrlPathEncode(flashMsg));
            //}

            //return Admin(ModuleDefs.Admin.Views.EditCategory, parms.ToArray());

            return AdminEditViewWithFlash(ModuleDefs.Admin.Views.EditCategory, categoryId, flashMsg);
        }

        public string AdminDeleteCategory(int categoryId)
        {
            return Admin(ModuleDefs.Admin.Views.EditCategory, "delete=" + categoryId);
        }

        public string AdminAddCoupon()
        {
            return Admin(ModuleDefs.Admin.Views.EditCoupon);
        }

        public string AdminEditCoupon(int couponId)
        {
            return AdminEditCoupon(couponId, string.Empty);
        }

        public string AdminEditCoupon(int couponId, string flashMsg)
        {
            return AdminEditViewWithFlash(ModuleDefs.Admin.Views.EditCoupon, couponId, flashMsg);
        }

        public string AdminDeleteCoupon(int couponId)
        {
            return Admin(ModuleDefs.Admin.Views.EditCoupon, "delete=" + couponId);
        }

        public string AdminAddDiscount()
        {
            return Admin(ModuleDefs.Admin.Views.EditDiscount);
        }

        public string AdminEditDiscount(int discountId)
        {
            return AdminEditDiscount(discountId, string.Empty);
        }

        public string AdminEditDiscount(int discountId, string flashMsg)
        {
            return AdminEditViewWithFlash(ModuleDefs.Admin.Views.EditDiscount, discountId, flashMsg);
        }

        public string AdminDeleteDiscount(int discountId)
        {
            return Admin(ModuleDefs.Admin.Views.EditDiscount, "delete=" + discountId);
        }

        public string AdminViewOrder(int orderId)
        {
            return Admin(ModuleDefs.Admin.Views.ViewOrder, "id=" + orderId);
        }

        public string AdminDeleteOrder(int orderId)
        {
            return Admin(ModuleDefs.Admin.Views.ViewOrder, "delete=" + orderId);
        }

        public string AdminShipping()
        {
            return Admin(ModuleDefs.Admin.Views.Shipping);
        }

        public string AdminShipping(params string[] additionalParams)
        {
            return Admin(ModuleDefs.Admin.Views.Shipping, additionalParams);
        }


        public string ShippingLog(int pageNumber)
        {

            List<string> otherParams = new List<string>();
            otherParams.Add("pg=" + pageNumber);

            return AdminShipping(otherParams.ToArray());
        }

        protected string AdminEditViewWithFlash(ModuleDefs.Admin.Views viewName, int idValue, string flashMsg)
        {
            List<string> parms = new List<string>();
            parms.Add("id=" + idValue);
            if (!string.IsNullOrEmpty(flashMsg))
            {
                parms.Add("flash=" + HttpUtility.UrlPathEncode(flashMsg));
            }

            return Admin(viewName, parms.ToArray());
        }

        #endregion
    }

    internal static class UrlExtensions
    {
        internal static string MakeHttps(this string url)
        {
            if (url.StartsWith("http://"))
            {
                return url.Replace("http://", "https://");
            }
            else if (!url.StartsWith("https://"))
            {
                return "https://" + url;
            }
            return url;
        }

        internal static string MakeRelative(this string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                string relativeUrl = url.Replace("https://", "");
                relativeUrl = relativeUrl.Replace("http://", "");
                int firstSlash = relativeUrl.IndexOf('/');
                if (firstSlash >= 0)
                {
                    relativeUrl = relativeUrl.Substring(firstSlash);
                }
                return relativeUrl;
            }
            return url;
        }
    }
}
