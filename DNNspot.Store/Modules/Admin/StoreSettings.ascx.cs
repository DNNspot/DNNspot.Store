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
using DotNetNuke.Security.Roles;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class StoreSettings : StoreAdminModuleBase
    {
        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Store Settings" }
               };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStoreSettings();
            }
        }

        private void LoadStoreSettings()
        {
            DataModel.Store store = StoreContext.CurrentStore;

            txtStoreName.Text = store.Name;
            txtOrderCompletedEmailRecipient.Text = store.GetSetting(StoreSettingNames.OrderCompletedEmailRecipient);
            txtCustomerServiceEmail.Text = store.GetSetting(StoreSettingNames.CustomerServiceEmailAddress);
            txtOrderNumberPrefix.Text = store.GetSetting(StoreSettingNames.OrderNumberPrefix);

            ddlDefaultCountryCode.Items.Clear();
            ddlDefaultCountryCode.Items.AddRange(DnnHelper.GetCountryListItems().ToArray());
            ddlDefaultCountryCode.Items.Insert(0, "");
            ddlDefaultCountryCode.TrySetSelectedValue(store.GetSetting(StoreSettingNames.DefaultCountryCode));

            ddlCurrency.Items.Clear();
            ddlCurrency.Items.AddRange(CurrencyCollection.All().Select(x => new ListItem() { Value = x.Code, Text = string.Format("{0} - {1}", x.Code, x.Description) }).ToArray());
            ddlCurrency.Items.Insert(0, new ListItem());
            ddlCurrency.SelectedValue = store.GetSetting(StoreSettingNames.CurrencyCode);

            string ccTypes = store.GetSetting(StoreSettingNames.AcceptedCreditCards) ?? string.Empty;
            if (string.IsNullOrEmpty(ccTypes))
            {
                chklAcceptedCreditCards.SetAllSelected();
            }
            else
            {
                string[] ccTypesArray = ccTypes.Split(',');
                chklAcceptedCreditCards.SetSelectedValues(ccTypesArray);
            }

            chkLoadJQueryUi.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.IncludeJQueryUi)).GetValueOrDefault(true);

            // Store Location / Address
            txtStorePhone.Text = store.GetSetting(StoreSettingNames.StorePhoneNumber);
            txtStoreStreet.Text = store.GetSetting(StoreSettingNames.StoreAddressStreet);
            txtStoreCity.Text = store.GetSetting(StoreSettingNames.StoreAddressCity);
            txtStoreRegion.Text = store.GetSetting(StoreSettingNames.StoreAddressRegion);
            txtStorePostalCode.Text = store.GetSetting(StoreSettingNames.StoreAddressPostalCode);
            ddlStoreCountryCode.Items.Clear();
            ddlStoreCountryCode.Items.AddRange(DnnHelper.GetCountryListItems().ToArray());
            ddlStoreCountryCode.Items.Insert(0, "");
            ddlStoreCountryCode.TrySetSelectedValue(store.GetSetting(StoreSettingNames.StoreAddressCountryCode));

            chkCheckoutAllowAnonymous.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.CheckoutAllowAnonymous)).GetValueOrDefault(true);
            chkCheckoutShowCreateAccountLink.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.CheckoutShowCreateAccountLink)).GetValueOrDefault(true);

            chkTaxShipping.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.TaxShipping)).GetValueOrDefault(true);

            chkSendPaymentCompleteEmail.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.SendPaymentCompleteEmail)).GetValueOrDefault(true);
            chkSendOrderReceivedEmail.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.SendOrderReceivedEmail)).GetValueOrDefault(true);

            chkShowShippingEstimateBox.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ShowShippingEstimate)).GetValueOrDefault(true);
            chkShowCouponBox.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ShowCouponBox)).GetValueOrDefault(true);
            chkQuantityAndPrice.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ShowPriceAndQuantityInCatalog)).GetValueOrDefault(false);

            chkShowPrices.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ShowPrices)).GetValueOrDefault(true);
            chkEnableCheckout.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.EnableCheckout)).GetValueOrDefault(true);
            chkRequireOrderNotes.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.RequireOrderNotes)).GetValueOrDefault(false);

            chkForceSslCheckout.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ForceSslCheckout)).GetValueOrDefault(false);
            chkDisplaySiteCredit.Checked = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.DisplaySiteCredit)).GetValueOrDefault(true);
            txtUrlToPostOrder.Text = store.GetSetting(StoreSettingNames.UrlToPostCompletedOrder);
        }

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveStoreSettings();
        }

        private void SaveStoreSettings()
        {
            DataModel.Store store = new DataModel.Store();
            if (store.LoadByPrimaryKey(StoreContext.CurrentStore.Id.Value))
            {
                store.Name = txtStoreName.Text;
                store.Save();


                store.UpdateSetting(StoreSettingNames.IncludeJQueryUi, chkLoadJQueryUi.Checked.ToString());

                store.UpdateSetting(StoreSettingNames.OrderCompletedEmailRecipient, txtOrderCompletedEmailRecipient.Text);
                store.UpdateSetting(StoreSettingNames.CustomerServiceEmailAddress, txtCustomerServiceEmail.Text);
                store.UpdateSetting(StoreSettingNames.OrderNumberPrefix, txtOrderNumberPrefix.Text);
                store.UpdateSetting(StoreSettingNames.DefaultCountryCode, ddlDefaultCountryCode.SelectedValue);
                
                store.UpdateSetting(StoreSettingNames.CurrencyCode, ddlCurrency.SelectedValue);
                store.UpdateSetting(StoreSettingNames.ShowPrices, chkShowPrices.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.AcceptedCreditCards, chklAcceptedCreditCards.GetSelectedValues().ToDelimitedString(","));

                store.UpdateSetting(StoreSettingNames.StorePhoneNumber, txtStorePhone.Text);
                store.UpdateSetting(StoreSettingNames.StoreAddressStreet, txtStoreStreet.Text);
                store.UpdateSetting(StoreSettingNames.StoreAddressCity, txtStoreCity.Text);
                store.UpdateSetting(StoreSettingNames.StoreAddressRegion, txtStoreRegion.Text);
                store.UpdateSetting(StoreSettingNames.StoreAddressPostalCode, txtStorePostalCode.Text);
                store.UpdateSetting(StoreSettingNames.StoreAddressCountryCode, ddlStoreCountryCode.SelectedValue);
                
                store.UpdateSetting(StoreSettingNames.EnableCheckout, chkEnableCheckout.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.CheckoutAllowAnonymous, chkCheckoutAllowAnonymous.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.CheckoutShowCreateAccountLink, chkCheckoutShowCreateAccountLink.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.ShowPriceAndQuantityInCatalog, chkQuantityAndPrice.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.RequireOrderNotes, chkRequireOrderNotes.Checked.ToString());

                store.UpdateSetting(StoreSettingNames.ShowCouponBox, chkShowCouponBox.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.ShowShippingEstimate, chkShowShippingEstimateBox.Checked.ToString());

                store.UpdateSetting(StoreSettingNames.SendPaymentCompleteEmail, chkSendPaymentCompleteEmail.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.SendOrderReceivedEmail, chkSendOrderReceivedEmail.Checked.ToString());

                store.UpdateSetting(StoreSettingNames.TaxShipping, chkTaxShipping.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.ForceSslCheckout, chkForceSslCheckout.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.DisplaySiteCredit, chkDisplaySiteCredit.Checked.ToString());
                store.UpdateSetting(StoreSettingNames.UrlToPostCompletedOrder, txtUrlToPostOrder.Text);

                // re-load the current store from the DB
                StoreContext.SetCurrentStore(store.Id.Value);

                Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.StoreSettings, "Store settings saved successfully"));
            }
        }
    }
}