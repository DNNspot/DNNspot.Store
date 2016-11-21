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
using DNNspot.Store.PaymentProviders;
using FluentValidation.Results;
using WA.Extensions;

namespace DNNspot.Store.Modules.Checkout
{
    public partial class CheckoutPayment : StoreCheckoutModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadResourceFileSettings();
                FillListControls();
            }
        }

        private void FillListControls()
        {
            DataModel.Store store = StoreContext.CurrentStore;
            checkoutOrderInfo.ReCalculateOrderTotals();

            //---- CC Types
            string creditCardsAccepted = store.GetSetting(StoreSettingNames.AcceptedCreditCards);
            if(!string.IsNullOrEmpty(creditCardsAccepted))
            {
                string[] cardTypes = creditCardsAccepted.Split(',');
                ddlCCType.Items.Clear();
                foreach(var card in cardTypes)
                {
                    string[] pair = card.Split('-');
                    if (pair.Length == 1)
                    {
                        ddlCCType.Items.Add(new ListItem() { Value = pair[0], Text = pair[0] });
                    }
                    else if(pair.Length == 2)
                    {
                        ddlCCType.Items.Add(new ListItem() { Value = pair[0], Text = pair[1] });
                    }                    
                }
                ddlCCType.Items.Insert(0, "");
            }
            else
            {
                //List<string> cardTypes = new List<string>(WA.Enum<CreditCardType>.GetNames());
                //cardTypes.Remove(CreditCardType.UNKNOWN.ToString());
                //cardTypes.Remove(CreditCardType.PayPal.ToString());                
                //ddlCCType.Items.Clear();
                //ddlCCType.Items.AddRange(cardTypes.ConvertAll(s => new ListItem() { Text = s, Value = s }).ToArray());
                //ddlCCType.Items.Insert(0, "");
                msgFlash.InnerHtml = "<ul><li>No credit cards accepted. Please choose a different payment method.</li></ul>";
                msgFlash.Visible = true;
            }

            //---- CC Expire Year
            ddlCCExpireYear.Items.Clear();
            ddlCCExpireYear.Items.Add("");
            int maxYear = DateTime.Now.Year + 12;
            for (int y = DateTime.Now.Year; y <= maxYear; y++)
            {
                ddlCCExpireYear.Items.Add(new ListItem() { Text = y.ToString(), Value = y.ToString() });
            }

            //---- Payment Methods            
            PayLaterPaymentProvider billMeLaterProvider = new PayLaterPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayLater));
            liBillMeLater.Visible = billMeLaterProvider.IsEnabled;
            lblPayLater.InnerHtml = billMeLaterProvider.DisplayText;
            payLaterInstructions.InnerHtml = billMeLaterProvider.CustomerInstructions.NewlineToBr();
            liPayPalExpressCheckoutPaymentMethod.Visible = store.IsPaymentProviderEnabled(PaymentProviderName.PayPalExpressCheckout);
            liCreditCardCapture.Visible = store.IsPaymentProviderEnabled(PaymentProviderName.CardCaptureOnly)
                                                || store.IsPaymentProviderEnabled(PaymentProviderName.AuthorizeNetAim)
                                                || store.IsPaymentProviderEnabled(PaymentProviderName.PayPalDirectPayment);
        }

        protected void btnReviewOrder_Click(object sender, EventArgs e)
        {
            // user-selected payment method (credit card or PayPal)
            string userSelectedPaymentMethod = Request.Form["paymentMethod"] ?? "";

            if (userSelectedPaymentMethod == "payLater")
            {
                checkoutOrderInfo.PaymentProvider = StoreContext.CurrentStore.GetOnsitePaymentProviders().Where(p => p == PaymentProviderName.PayLater).First();
                if (checkoutOrderInfo.PaymentProvider == PaymentProviderName.UNKNOWN)
                {
                    throw new ApplicationException("Unable to determine PaymentProvider for CheckoutOrderInfo!");
                }
                UpdateCheckoutSession(checkoutOrderInfo);

                Response.Redirect(StoreUrls.CheckoutReview());
            }
            else if(userSelectedPaymentMethod == "payPalExpressCheckout")
            {
                OrderController orderController = new OrderController(StoreContext);
                Order pendingOrder = orderController.CreateOrder(checkoutOrderInfo, OrderStatusName.PendingOffsite);

                PayPalExpressCheckoutPaymentProvider payPalExpressCheckout = new PayPalExpressCheckoutPaymentProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalExpressCheckout));
                string cancelUrl = StoreUrls.Cart();
                string returnUrl = StoreUrls.CheckoutReview();
                string token = payPalExpressCheckout.SetExpressCheckoutAndGetToken(pendingOrder, cancelUrl, returnUrl);

                if (!string.IsNullOrEmpty(token))
                {
                    //Session[SessionKeys.CheckoutOrderInfo] = checkoutOrderInfo;                
                    //string payPalUrl = string.Format("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={0}", token);

                    string payPalUrl = payPalExpressCheckout.GetExpressCheckoutUrl(token);

                    Response.Redirect(payPalUrl);
                }
                else
                {
                    // ERROR
                    throw new ApplicationException("PayPal Express Token is Null/Empty!");
                }                
            }
            else if (userSelectedPaymentMethod == "creditCard")
            {
                checkoutOrderInfo.PaymentProvider = StoreContext.CurrentStore.GetOnsitePaymentProviders().Where(p => p != PaymentProviderName.PayLater).First();
                if (checkoutOrderInfo.PaymentProvider == PaymentProviderName.UNKNOWN)
                {
                    throw new ApplicationException("Unable to determine PaymentProvider for CheckoutOrderInfo!");
                }

                //---- Credit Card Info
                CreditCardInfo creditCard = new CreditCardInfo()
                {
                    CardType = WA.Enum<CreditCardType>.TryParseOrDefault(ddlCCType.SelectedValue, CreditCardType.UNKNOWN),
                    CardNumber = txtCCNumber.Text,
                    ExpireMonth = WA.Parser.ToShort(ddlCCExpireMonth.SelectedValue),
                    ExpireYear = WA.Parser.ToShort(ddlCCExpireYear.SelectedValue),
                    NameOnCard = txtCCNameOnCard.Text,
                    SecurityCode = txtCCSecurityCode.Text.Trim()
                };
                checkoutOrderInfo.CreditCard = creditCard;

                UpdateCheckoutSession(checkoutOrderInfo);
                
                bool doRedirect = true;
                if (checkoutOrderInfo.Total > 0)
                {
                    CreditCardInfoValidator validator = new CreditCardInfoValidator();
                    ValidationResult results = validator.Validate(checkoutOrderInfo.CreditCard);
                    if (!results.IsValid && results.Errors.Count > 0)
                    {
                        doRedirect = false;

                        msgFlash.InnerHtml = "<ul>" + results.Errors.ToList().ConvertAll(err => "<li>" + err.ErrorMessage + "</li>").ToDelimitedString(" ") + "</ul>";
                        msgFlash.Visible = true;
                    }
                }

                if(doRedirect)
                {
                    Response.Redirect(StoreUrls.CheckoutReview());
                }
            }
            else
            {
                throw new ApplicationException("Unable to determine PaymentProvider for CheckoutOrderInfo!");
            }                        
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
    }
}