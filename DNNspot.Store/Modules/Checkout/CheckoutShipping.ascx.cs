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
using WA.Extensions;

namespace DNNspot.Store.Modules.Checkout
{
    public partial class CheckoutShipping : StoreCheckoutModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadResourceFileSettings();
                FillListControls();
                PopulateForm();
            }
        }

        private void FillListControls()
        {
            ////---- Shipping Options
            //AddressInfo origin = StoreContext.CurrentStore.Address;
            //AddressInfo destination = checkoutOrderInfo.ShippingAddress;

            //List<ShippingOption> shippingOptions = StoreContext.CurrentStore.GetShippingOptionEstimates(origin, destination, checkoutOrderInfo.Cart.GetCartItemsWithProductInfo());
            //ddlShippingOption.Items.Clear();
            //shippingOptions.ForEach(x => ddlShippingOption.Items.Add(new ListItem()
            //                                                             {
            //                                                                 Value = x.ListKey, 
            //                                                                 Text = string.Format(@"{0} - {1}", x.DisplayName, StoreContext.CurrentStore.FormatCurrency(x.Cost.GetValueOrDefault(0)))
            //                                                             }));
            //ddlShippingOption.TrySetSelectedValue(checkoutOrderInfo.ShippingOption.ListKey);

            ////if(shippingOptions.Count == 0)
            ////{
            ////    msgFlash.InnerHtml = "We are unable to ship to the specified Country / Region";
            ////    msgFlash.Visible = true;
            ////}
        }

        private void PopulateForm()
        {
            AddressInfo address = checkoutOrderInfo.ShippingAddress;

            string defaultCountry = StoreContext.CurrentStore.GetSetting(StoreSettingNames.DefaultCountryCode);
            bool haveDefaultCountry = !string.IsNullOrEmpty(defaultCountry);

            bool shippingCountryIsEmpty = string.IsNullOrEmpty(address.Country);
            if (shippingCountryIsEmpty && haveDefaultCountry)
            {
                address.Country = defaultCountry;
            }

            if(string.IsNullOrEmpty(address.FirstName) && UserId> 0)
            {
                address.FirstName = UserInfo.FirstName;
            }
            if(string.IsNullOrEmpty(address.LastName) && UserId > 0)
            {
                address.LastName = UserInfo.LastName;
            }

            shippingAddressForm.SetAddressInfo(address);
            txtExtraShipToRecipientName.Text = address.BusinessName;

            //if(checkoutOrderInfo.ShipToBillingAddress.GetValueOrDefault(false))
            //{
            //    shippingAddressForm.Visible = false;
            //    pnlExtraShippingFields.Visible = true;
            //}
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //if (checkoutOrderInfo.ShipToBillingAddress.GetValueOrDefault(false))
            //{
            //    checkoutOrderInfo.ShippingAddress.BusinessName = txtExtraShipToRecipientName.Text ?? "";
            //}
            //else
            //{
            //    checkoutOrderInfo.ShippingAddress = shippingAddressForm.GetAddressInfoFromPost();
            //}
            checkoutOrderInfo.ShippingAddress.BusinessName = txtExtraShipToRecipientName.Text ?? "";
            checkoutOrderInfo.ShippingAddress = shippingAddressForm.GetAddressInfoFromPost();

            //checkoutOrderInfo.ShippingOption = StoreContext.CurrentStore.GetShippingOptionByListKey(ddlShippingOption.SelectedValue);

            checkoutOrderInfo.ReCalculateOrderTotals();

            string nextUrl = StoreUrls.CheckoutShippingMethod();

            if (checkoutOrderInfo.HasOnlyDownloadableProducts)
            {
                if (checkoutOrderInfo.RequiresPayment)
                {
                    nextUrl = StoreUrls.CheckoutPayment();
                }
                else
                {
                    nextUrl = StoreUrls.CheckoutReview();
                }
            }

            UpdateCheckoutSession(checkoutOrderInfo);

            Response.Redirect(nextUrl);

            //if (checkoutOrderInfo.RequiresPayment)
            //{                
            //    Response.Redirect(StoreUrls.CheckoutPayment());
            //}
            //else
            //{
            //    Response.Redirect(StoreUrls.CheckoutReview());                
            //}
        }

        private void LoadResourceFileSettings()
        {
            litBillingBreadcrumb.Text = ResourceString("litBillingBreadcrumb.Text");
            litShippingBreadcrumb.Text = ResourceString("litShippingBreadcrumb.Text");
            litShippingMethodBreadcrumb.Text = ResourceString("litShippingMethodBreadcrumb.Text");
            litPaymentBreadcrumb.Text = ResourceString("litPaymentBreadcrumb.Text");
            litReviewOrderBreadcrumb.Text = ResourceString("litReviewOrderBreadcrumb.Text");

            //btnCheckoutOnsite.Text = ResourceString("CheckoutOnSite.Text");
        }

        //protected void lbtnEditShippingAddress_Click(object sender, EventArgs e)
        //{
        //    checkoutOrderInfo.ShipToBillingAddress = false;
        //    checkoutOrderInfo.ShippingAddress.BusinessName = txtExtraShipToRecipientName.Text ?? "";
        //    //checkoutOrderInfo.ShippingOption = StoreContext.CurrentStore.GetShippingOptionByListKey(ddlShippingOption.SelectedValue);

        //    UpdateCheckoutSession(checkoutOrderInfo);

        //    Response.Redirect(StoreUrls.CheckoutShipping());
        //}
    }
}