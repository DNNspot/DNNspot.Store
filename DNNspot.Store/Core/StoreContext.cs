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
using System.IO;
using System.Linq;
using System.Web;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Host;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.HttpModules.Config;
using DotNetNuke.Services.Log.EventLog;

namespace DNNspot.Store
{
    public class StoreContext
    {
        public DataModel.Store CurrentStore { get; private set; }
        public string PageSlug { get; private set; }
        public Category Category { get; private set; }
        public Product Product { get; private set; }
        public List<Category> CategoryBreadcrumb { get; private set; }
        public Guid CartId { get; private set; }
        public int? UserId { get; private set; }
        private HttpCookie userCookie;
        public CacheKeyHelper CacheKeys { get; private set; }
        public SessionKeyHelper SessionKeys { get; private set; }

        public StoreContext(HttpRequest httpRequest)
        {
            Init(httpRequest, null);
        }

        public StoreContext(HttpRequest httpRequest, int storeId)
        {
            Init(httpRequest, storeId);
        }

        private void Init(HttpRequest httpRequest, int? storeId)
        {
            if (storeId.HasValue)
            {
                CurrentStore = DataModel.Store.GetStore(storeId.Value);
            }
            else
            {
                CurrentStore = GetCurrentStore(httpRequest);
            }
                        
            CacheKeys = new CacheKeyHelper(CurrentStore.Id.GetValueOrDefault(-1));
            SessionKeys = new SessionKeyHelper(CurrentStore.Id.GetValueOrDefault(-1));

            //---- Get the current User
            UserInfo userInfo = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
            if (userInfo.UserID > 0)
            {
                this.UserId = userInfo.UserID;
            }

            //---- Cookie Handling
            userCookie = GetOrCreateUserCookie();
            Guid? cookieCartId = WA.Parser.ToGuid(userCookie.Values["CartId"]);
            if (cookieCartId.HasValue)
            {
                this.CartId = cookieCartId.Value;
            }
            else
            {
                // set a new CartId
                this.CartId = Guid.NewGuid();
                UpdateCookieValue("CartId", this.CartId.ToString());
            }            

            //---- Slug parsing
            ParseSlugs();

            //---- Category Breadcrumbs
            CategoryBreadcrumb = new List<Category>();
            if (this.Category != null)
            {
                CategoryBreadcrumb = this.Category.GetAllParents();
                if (!this.Category.IsSystemCategory.Value)
                {
                    CategoryBreadcrumb.Add(this.Category);
                }
            }              
        }

        private HttpCookie GetOrCreateUserCookie()
        {
            string cookieName = string.Format(@"DNNspot-Store-{0}", CurrentStore.Id.Value);

            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                // create the cookie
                cookie = new HttpCookie(cookieName);
                cookie.Values["CartId"] = Guid.NewGuid().ToString();
            }

            // update the expiration date for new/existing cookie
            cookie.Expires = DateTime.Now.AddDays(Constants.CookieExpireDays);

            HttpContext.Current.Response.Cookies.Add(cookie);

            return cookie;
        }

        private void UpdateCookieValue(string key, string value)
        {
            HttpCookie cookie = GetOrCreateUserCookie();
            if (cookie != null)
            {
                cookie.Values[key] = value;

                HttpContext.Current.Response.Cookies.Add(cookie);
            }             
        }

        public void RemoveCookieCartId()
        {
            UpdateCookieValue("CartId", "");         
        }

        private void ParseSlugs()
        {
            int storeId = CurrentStore.Id.Value;
            string categorySlug = RequestHelper.GetCategorySlug();
            string slug = RequestHelper.GetSlug();
            this.PageSlug = slug;

            if (string.IsNullOrEmpty(slug))
            {
                this.Category = Category.GetOrCreateHomeCategoryForStore(CurrentStore);
            }
            else
            {
                this.Product = Product.GetBySlug(storeId, slug);
                if (this.Product != null)
                {
                    // we loaded a Product by the slug, now check for the category slug
                    this.Category = Category.GetBySlug(storeId, categorySlug) ?? Category.GetOrCreateHomeCategoryForStore(CurrentStore);
                }
                else
                {
                    this.Category = Category.GetBySlug(storeId, slug) ?? Category.GetOrCreateHomeCategoryForStore(CurrentStore);
                }
            }

            //string msg = string.Format(@"StoreContext-ParseSlugs : StoreId = '{0}' | Slug = '{1}' | CategorySlug = '{2}' | CategoryId = '{3}' | ProductId = '{4}'", storeId, slug, categorySlug, this.Category.Id, this.Product != null ? this.Product.Id.GetValueOrDefault(-1).ToString() : "");
            //Debug.Write(msg);
        }

        internal static DataModel.Store GetCurrentStore(HttpRequest httpRequest)
        {
            PortalSettings portalSettings = null;

            if(httpRequest != null)
            {
                int? queryPortalId = WA.Parser.ToInt(httpRequest.Params.Get("PortalId"));
                if(queryPortalId.HasValue)
                {
                    // DNN 5 only
                    portalSettings = new PortalSettings(queryPortalId.Value);          
                }
            }

            if (portalSettings == null)
            {
                portalSettings = PortalController.GetCurrentPortalSettings();    
            }

            List<DataModel.Store> portalStores = DataModel.StoreCollection.GetStoresByPortalId(portalSettings.PortalId);           
            if (portalStores.Count > 0)
            {
                return portalStores[0];
            }
            else
            {
                return CreateInitialStoreForPortal(portalSettings);
            }
        }

        internal void SetCurrentStore(int storeId)
        {
            this.CurrentStore = DataModel.Store.GetStore(storeId);
        }

        private static DataModel.Store CreateInitialStoreForPortal(PortalSettings portalSettings)
        {
            //--- Create the Store for this Portal
            DataModel.Store newStore = new DataModel.Store();
            newStore.PortalId = portalSettings.PortalId;
            newStore.Name = portalSettings.PortalName;
            newStore.Save();

            //--- Set some sensible default settings
            newStore.UpdateSetting(StoreSettingNames.OrderCompletedEmailRecipient, portalSettings.Email);
            newStore.UpdateSetting(StoreSettingNames.CustomerServiceEmailAddress, portalSettings.Email);

            //--- Copy over the default email templates
            newStore.AddMissingEmailTemplates();

            //--- Set the default payment processor to 'CardCaptureOnly'
            CardCaptureOnlyPaymentProvider cardCapturePaymentProvider = new CardCaptureOnlyPaymentProvider(newStore.GetPaymentProviderConfig(PaymentProviderName.CardCaptureOnly));
            cardCapturePaymentProvider.IsEnabled = true;
            newStore.UpdatePaymentProviderConfig(cardCapturePaymentProvider.GetConfiguration());
            

            //--- Add a default shipping service and rate type
            var newShippingService = new DataModel.ShippingService();
            newShippingService.StoreId = newStore.Id.Value;
            newShippingService.ShippingProviderType = (short)ShippingProviderType.CustomShipping;
            newShippingService.Save();
            Dictionary<string, string> settings = newShippingService.GetSettingsDictionary();
            settings["IsEnabled"] = true.ToString();
            newShippingService.UpdateSettingsDictionary(settings);
            var newRateType = new DataModel.ShippingServiceRateType();
            newRateType.ShippingServiceId = newShippingService.Id.Value;
            newRateType.Name = "Standard";
            newRateType.DisplayName = "Standard";
            newRateType.Save();

            var fedexService = new DataModel.ShippingService();
            fedexService.StoreId = newStore.Id.Value;
            fedexService.ShippingProviderType = (short)ShippingProviderType.FedEx;
            fedexService.Save();
            Dictionary<string, string> fedexSettings = fedexService.GetSettingsDictionary();
            fedexSettings["IsEnabled"] = false.ToString();
            fedexService.UpdateSettingsDictionary(fedexSettings);

            return newStore;            
        }

        /// <summary>
        /// Tries to update the DNN FriendlyURL Config with our Store URL patterns
        /// </summary>
        /// <returns>true if success, false otherwise</returns>
        internal static bool UpdateDnnHostSiteUrlConfig()
        {
            const string rxMatch = @".*?-t(?<tabid>\d+)/(?<cat>[\w-_]*/)*(?<slug>.*)\.aspx";
            const string rxReplace = @"~/Default.aspx?TabId=$1&cat=$2&slug=$3";

            RewriterConfiguration urlConfig = DotNetNuke.HttpModules.Config.RewriterConfiguration.GetConfig();

            bool ruleExistsInConfig = false;
            foreach (RewriterRule rule in urlConfig.Rules)
            {
                if (rule.LookFor == rxMatch)
                {
                    ruleExistsInConfig = true;
                    break;
                }
            }

            if (!ruleExistsInConfig)
            {
                RewriterRule storeUrlRule = new RewriterRule();
                storeUrlRule.LookFor = rxMatch;
                storeUrlRule.SendTo = rxReplace;
                urlConfig.Rules.Add(storeUrlRule);

                RewriterConfiguration.SaveConfig(urlConfig.Rules);
                ruleExistsInConfig = true;
            }

            return ruleExistsInConfig;
        }
    }
}
