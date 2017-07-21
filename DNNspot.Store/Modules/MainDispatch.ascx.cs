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
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Search;
using DNNspot.Store.DataModel;
using EntitySpaces.Interfaces;
using WA.Extensions;

namespace DNNspot.Store.Modules
{
    public partial class MainDispatch : PortalModuleBase //StoreModuleBase
    {
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            DataModel.DataModel.Initialize();            

            string controlPath = GetCustomControlToLoad();
            if (!string.IsNullOrEmpty(controlPath))
            {                
                StoreModuleBase module = (StoreModuleBase)LoadControl(controlPath);
                if (module != null)
                {
                    // load the control into the placeholder
                    module.ModuleConfiguration = ModuleConfiguration;
                    module.ID = System.IO.Path.GetFileNameWithoutExtension(controlPath);                    

                    plhUserControl.Controls.Add(module);

                    //--- Site Credit Check
                    //bool showSiteCredit = module.StoreContext.CurrentStore.GetSettingBool(StoreSettingNames.DisplaySiteCredit).GetValueOrDefault(true);
                    //if(showSiteCredit)
                    //{
                    //    string resxFile = string.Format("/DesktopModules/{0}/App_GlobalResources/Global.resx", ModuleConfiguration.FolderName);
                    //    string siteCreditHtml = DotNetNuke.Services.Localization.Localization.GetString("SiteCredit", resxFile);
                    //    plhUserControl.Controls.Add(new LiteralControl(siteCreditHtml));                        
                    //}
                }
            }
            else
            {
                throw new ModuleLoadException("Empty 'controlPath' variable, Unknown View Control for DNNspot Store");
            }
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if(!IsPostBack)
        //    {
                
        //    }
        //}

        protected string GetCustomControlToLoad()
        {            
            string slug = RequestHelper.GetSlug();            

            // Check for Page slugs
            //switch (storeContext.PageSlug)            
            switch (slug)
            {
                case "Cart":
                    return "Cart/Cart.ascx";
                case "LoginPrompt":
                    return "Checkout/LoginPrompt.ascx";
                case "Checkout-Billing":
                    return "Checkout/CheckoutBilling.ascx";
                case "Checkout-Shipping":
                    return "Checkout/CheckoutShipping.ascx";
                case "Checkout-ShippingMethod":
                    return "Checkout/CheckoutShippingMethod.ascx";
                case "Checkout-Payment":
                    return "Checkout/CheckoutPayment.ascx";
                case "Checkout-Review":
                    return "Checkout/CheckoutReview.ascx";
                case "Checkout-Complete":
                    return "Checkout/CheckoutComplete.ascx";
                case "Catalog-Product":
                    return "Catalog/CatalogProduct.ascx";
            }

            DataModel.Store currentStore = StoreContext.GetCurrentStore(Request);            
            //if (storeContext.Product != null || WA.Parser.ToInt(Request.QueryString["product"]).HasValue)
            if (Product.SlugExists(currentStore.Id.Value, slug) || WA.Parser.ToInt(Request.QueryString["product"]).HasValue)
            {
                return "Catalog/CatalogProduct.ascx";
            }

            ////if (storeContext.Category != null)
            //if (!string.IsNullOrEmpty(categorySlug))
            //{
            //    return "Catalog/CatalogCategory.ascx";
            //}

            return "Catalog/CatalogCategory.ascx";
        }

        #region IActionable Members

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection moduleActions = new ModuleActionCollection();
                //moduleActions.Add(GetNextActionID(), "Administration", ModuleActionType.EditContent, "", "", UrlHelper.ViewUrl(ViewNames.AdminDefault), false, SecurityAccessLevel.Edit, true, false);

                return moduleActions;
            }
        }

        #endregion


    }
}