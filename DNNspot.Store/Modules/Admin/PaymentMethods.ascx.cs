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
using DNNspot.Store.PaymentProviders;

namespace DNNspot.Store.Modules.Admin
{
    public partial class PaymentMethods : StoreAdminModuleBase
    {
        List<CheckBox> onSiteCheckboxes = new List<CheckBox>();
        List<CheckBox> offSiteCheckboxes = new List<CheckBox>();

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Payment Processors" }
               };
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            onSiteCheckboxes.AddRange(new List<CheckBox>() { chkPayLater, chkCardCaptureOnly, chkAuthorizeNetAim, chkPayPalDirect });
            offSiteCheckboxes.AddRange(new List<CheckBox>() { chkPayPalStandard, chkPayPalExpress });

            if(!IsPostBack)
            {
                LoadPaymentProviders();
            }
        }

        private void LoadPaymentProviders()
        {
            DataModel.Store store = StoreContext.CurrentStore;

            // StoreContext.CurrentStore.GetOnsiteCreditCardPaymentProvider() == PaymentProviderName.OfflinePayment
            // StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.AuthorizeNetAim)

            // ON-SITE
            chkPayLater.Checked = store.IsPaymentProviderEnabled(PaymentProviderName.PayLater);
            chkCardCaptureOnly.Checked = store.IsPaymentProviderEnabled(PaymentProviderName.CardCaptureOnly);
            chkAuthorizeNetAim.Checked = store.IsPaymentProviderEnabled(PaymentProviderName.AuthorizeNetAim);
            chkPayPalDirect.Checked = store.IsPaymentProviderEnabled(PaymentProviderName.PayPalDirectPayment);

            PayLaterPaymentProvider billMeLaterProvider = new PayLaterPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayLater));
            txtPayLaterDisplayText.Text = billMeLaterProvider.DisplayText;
            txtPayLaterCustomerInstructions.Text = billMeLaterProvider.CustomerInstructions;

            AuthorizeNetAimProvider authorizeNetAimProvider = new AuthorizeNetAimProvider(store.GetPaymentProviderConfig(PaymentProviderName.AuthorizeNetAim));            
            chkAuthorizeNetAimTestGateway.Checked = authorizeNetAimProvider.IsTestGateway;
            chkAuthorizeNetAimTestTransactions.Checked = authorizeNetAimProvider.IsTestTransactions;
            txtAuthorizeNetAimApiLoginId.Text = authorizeNetAimProvider.ApiLoginId;
            txtAuthorizeNetAimTransactionKey.Text = authorizeNetAimProvider.TransactionKey;

            PayPalDirectPaymentProvider payPalDirectPayment = new PayPalDirectPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayPalDirectPayment));            
            chkPayPalDirectPaymentIsSandbox.Checked = payPalDirectPayment.IsSandbox;
            txtPayPalDirectPaymentApiUsername.Text = payPalDirectPayment.ApiUsername;
            txtPayPalDirectPaymentApiPassword.Text = payPalDirectPayment.ApiPassword;
            txtPayPalDirectPaymentApiSignature.Text = payPalDirectPayment.ApiSignature;

            // OFF-SITE
            chkPayPalStandard.Checked = store.IsPaymentProviderEnabled(PaymentProviderName.PayPalStandard);
            chkPayPalExpress.Checked = store.IsPaymentProviderEnabled(PaymentProviderName.PayPalExpressCheckout);

            PayPalStandardProvider payPalStandard = new PayPalStandardProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayPalStandard));            
            chkPayPalStandardIsSandbox.Checked = payPalStandard.IsSandbox;
            rdoPayPalStandardShippingLogic.SelectedValue = payPalStandard.ShippingLogic;
            txtPayPalStandardEmail.Text = payPalStandard.EmailAddress;

            PayPalExpressCheckoutPaymentProvider payPalExpressCheckout = new PayPalExpressCheckoutPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayPalExpressCheckout));            
            chkPayPalExpressCheckoutIsSandbox.Checked = payPalExpressCheckout.IsSandbox;
            txtPayPalExpressCheckoutApiUsername.Text = payPalExpressCheckout.ApiUsername;
            txtPayPalExpressCheckoutApiPassword.Text = payPalExpressCheckout.ApiPassword;
            txtPayPalExpressCheckoutApiSignature.Text = payPalExpressCheckout.ApiSignature;      
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataModel.Store store = StoreContext.CurrentStore;

            //PaymentProviderName onSitePaymentProvider = WA.Enum<PaymentProviderName>.TryParseOrDefault(Request.Form["enabledOnSiteProvider"], PaymentProviderName.UNKNOWN);
            //PaymentProviderName offSitePaymentProvider = WA.Enum<PaymentProviderName>.TryParseOrDefault(Request.Form["enabledOffSiteProvider"], PaymentProviderName.UNKNOWN);

            if(!onSiteCheckboxes.Exists(x => x.Checked) && !offSiteCheckboxes.Exists(x => x.Checked))
            {
                ShowFlash("<span style='color: Red;'>ERROR - Please choose at least one payment provider.</span>");
                return;
            }

            if(chkAuthorizeNetAim.Checked && chkPayPalDirect.Checked)
            {
                ShowFlash("<span style='color: Red;'>ERROR - Please choose either Authorize.Net OR PayPal Pro (both cannot be selected)</span>");
                return;                
            }
            if (chkPayPalStandard.Checked && chkPayPalExpress.Checked)
            {
                ShowFlash("<span style='color: Red;'>ERROR - Please choose either PayPal Standard OR PayPal Express Checkout (both cannot be selected)</span>");
                return;
            }

            //--- ON-Site Payment Providers

            PayLaterPaymentProvider billMeLaterPaymentProvider = new PayLaterPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayLater));
            billMeLaterPaymentProvider.IsEnabled = chkPayLater.Checked;
            billMeLaterPaymentProvider.DisplayText = txtPayLaterDisplayText.Text;
            billMeLaterPaymentProvider.CustomerInstructions = txtPayLaterCustomerInstructions.Text;
            store.UpdatePaymentProviderConfig(billMeLaterPaymentProvider.GetConfiguration());

            CardCaptureOnlyPaymentProvider cardCaptureOnlyPaymentProvider = new CardCaptureOnlyPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.CardCaptureOnly));
            cardCaptureOnlyPaymentProvider.IsEnabled = chkCardCaptureOnly.Checked;
            store.UpdatePaymentProviderConfig(cardCaptureOnlyPaymentProvider.GetConfiguration());

            AuthorizeNetAimProvider authorizeNetAimProvider = new AuthorizeNetAimProvider(store.GetPaymentProviderConfig(PaymentProviderName.AuthorizeNetAim));
            authorizeNetAimProvider.IsEnabled = chkAuthorizeNetAim.Checked;
            authorizeNetAimProvider.IsTestGateway = chkAuthorizeNetAimTestGateway.Checked;
            authorizeNetAimProvider.IsTestTransactions = chkAuthorizeNetAimTestTransactions.Checked;
            authorizeNetAimProvider.ApiLoginId = txtAuthorizeNetAimApiLoginId.Text;
            authorizeNetAimProvider.TransactionKey = txtAuthorizeNetAimTransactionKey.Text;
            store.UpdatePaymentProviderConfig(authorizeNetAimProvider.GetConfiguration());

            PayPalDirectPaymentProvider payPalDirectPayment = new PayPalDirectPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayPalDirectPayment));
            payPalDirectPayment.IsEnabled = chkPayPalDirect.Checked;
            payPalDirectPayment.IsSandbox = chkPayPalDirectPaymentIsSandbox.Checked;
            payPalDirectPayment.ApiUsername = txtPayPalDirectPaymentApiUsername.Text;
            payPalDirectPayment.ApiPassword = txtPayPalDirectPaymentApiPassword.Text;
            payPalDirectPayment.ApiSignature = txtPayPalDirectPaymentApiSignature.Text;
            store.UpdatePaymentProviderConfig(payPalDirectPayment.GetConfiguration());


            //--- OFF-Site Payment Providers

            PayPalStandardProvider payPalStandard = new PayPalStandardProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayPalStandard));
            payPalStandard.IsEnabled = chkPayPalStandard.Checked;
            payPalStandard.IsSandbox = chkPayPalStandardIsSandbox.Checked;
            payPalStandard.ShippingLogic = rdoPayPalStandardShippingLogic.SelectedValue;
            payPalStandard.EmailAddress = txtPayPalStandardEmail.Text;
            store.UpdatePaymentProviderConfig(payPalStandard.GetConfiguration());

            PayPalExpressCheckoutPaymentProvider payPalExpressCheckout = new PayPalExpressCheckoutPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayPalExpressCheckout));
            payPalExpressCheckout.IsEnabled = chkPayPalExpress.Checked;
            payPalExpressCheckout.IsSandbox = chkPayPalExpressCheckoutIsSandbox.Checked;
            payPalExpressCheckout.ApiUsername = txtPayPalExpressCheckoutApiUsername.Text;
            payPalExpressCheckout.ApiPassword = txtPayPalExpressCheckoutApiPassword.Text;
            payPalExpressCheckout.ApiSignature = txtPayPalExpressCheckoutApiSignature.Text;
            store.UpdatePaymentProviderConfig(payPalExpressCheckout.GetConfiguration());

            Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.PaymentMethods, "Payment Settings Saved Successfully"));            
        }
    }
}