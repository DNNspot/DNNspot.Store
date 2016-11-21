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

namespace DNNspot.Store.Modules.Checkout
{
    public partial class CheckoutBilling : StoreCheckoutModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadResourceFileSettings();

            if (!IsPostBack)
            {
                PopulateForm();
            }
        }

        private void PopulateForm()
        {
            AddressInfo address = checkoutOrderInfo.BillingAddress;

            if (address.NameFieldsAreEmpty && UserId > 0)
            {
                address.FirstName = UserInfo.FirstName;
                address.LastName = UserInfo.LastName;
                address.Email = UserInfo.Email;
            }
            if (address.AddressFieldsAreEmpty && UserId > 0)
            {
                address.Telephone = UserInfo.Profile.Telephone;

                address.Address1 = UserInfo.Profile.Street;
                address.Address2 = UserInfo.Profile.Unit;
                address.City = UserInfo.Profile.City;
                address.Region = UserInfo.Profile.Region;   // DNN seems to use full region name instead of abbreviation
                address.PostalCode = UserInfo.Profile.PostalCode;
                address.Country = UserInfo.Profile.Country;     // DNN seems to use full country name here

                // try to look up the Country Code based on the Country Name
                List<ListItem> countryListItems = DnnHelper.GetCountryListItems();
                ListItem countryItem = countryListItems.Find(c => c.Text == UserInfo.Profile.Country);
                if (countryItem != null)
                {
                    address.Country = countryItem.Value;
                }

                // try to look up the Region Code based on the Region Name
                List<ListItem> regionListItems = DnnHelper.GetRegionListItems(address.Country);
                ListItem regionItem = regionListItems.Find(r => r.Text == UserInfo.Profile.Region);
                if (regionItem != null)
                {
                    address.Region = regionItem.Value;
                }
            }

            //--- Set a default country
            string defaultCountry = StoreContext.CurrentStore.GetSetting(StoreSettingNames.DefaultCountryCode);
            if (string.IsNullOrEmpty(address.Country) && !string.IsNullOrEmpty(defaultCountry))
            {
                address.Country = defaultCountry;
            }

            chkCopyBillingAddress.Checked = checkoutOrderInfo.ShipToBillingAddress.GetValueOrDefault(false);
            chkCopyBillingAddress.Visible = !checkoutOrderInfo.HasOnlyDownloadableProducts;
            lblCopyBilling.Visible = chkCopyBillingAddress.Visible;

            //--- Set the fields on the form
            billingAddressForm.SetAddressInfo(address);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string nextUrl = StoreUrls.CheckoutShipping();

            checkoutOrderInfo.BillingAddress = billingAddressForm.GetAddressInfoFromPost();

            bool copyBillingAddress = chkCopyBillingAddress.Checked;
            if (copyBillingAddress)
            {
                //checkoutOrderInfo.ShippingAddress = billingAddressForm.GetAddressInfoFromPost();
                //nextUrl = StoreUrls.CheckoutShippingMethod();
                checkoutOrderInfo.ShippingAddress.FirstName = checkoutOrderInfo.BillingAddress.FirstName;
                checkoutOrderInfo.ShippingAddress.LastName = checkoutOrderInfo.BillingAddress.LastName;
                checkoutOrderInfo.ShippingAddress.Address1 = checkoutOrderInfo.BillingAddress.Address1;
                checkoutOrderInfo.ShippingAddress.Address2 = checkoutOrderInfo.BillingAddress.Address2;
                checkoutOrderInfo.ShippingAddress.City = checkoutOrderInfo.BillingAddress.City;
                checkoutOrderInfo.ShippingAddress.Region = checkoutOrderInfo.BillingAddress.Region;
                checkoutOrderInfo.ShippingAddress.PostalCode = checkoutOrderInfo.BillingAddress.PostalCode;
                checkoutOrderInfo.ShippingAddress.Country = checkoutOrderInfo.BillingAddress.Country;
                checkoutOrderInfo.ShippingAddress.Telephone = checkoutOrderInfo.BillingAddress.Telephone;
            }
            checkoutOrderInfo.ShipToBillingAddress = copyBillingAddress;


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
        }

        private void LoadResourceFileSettings()
        {
            litBillingBreadcrumb.Text = ResourceString("litBillingBreadcrumb.Text");
            litShippingBreadcrumb.Text = ResourceString("litShippingBreadcrumb.Text");
            litShippingMethodBreadcrumb.Text = ResourceString("litShippingMethodBreadcrumb.Text");
            litPaymentBreadcrumb.Text = ResourceString("litPaymentBreadcrumb.Text");
            litReviewOrderBreadcrumb.Text = ResourceString("litReviewOrderBreadcrumb.Text");

            litCheckoutHeader.Text = ResourceString("litCheckoutHeader.Text");
            litBillingInformation.Text = ResourceString("litBillingInformation.Text");
            
            //btnCheckoutOnsite.Text = ResourceString("CheckoutOnSite.Text");
        }
    }
}