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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DotNetNuke.Services.Exceptions;
using WA.Extensions;

namespace DNNspot.Store.Modules.Checkout
{
    public partial class CheckoutReview : StoreCheckoutModuleBase
    {
        protected bool RequireOrderNotes;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadResourceFileSettings();

            if (!IsPostBack)
            {
                HandlePayPalExpressCallback();

                ShowReviewOrder();
            }
        }

        /// <summary>
        /// Check for the PayPal Express Checkout return page variables and update the Order if needed.
        /// </summary>
        private void HandlePayPalExpressCallback()
        {
            Dictionary<string, string> payPalVariables = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(Request.QueryString["token"]) && !string.IsNullOrEmpty(Request.QueryString["PayerID"]))
            {
                string payPalToken = Request.QueryString["token"];
                payPalVariables["token"] = payPalToken;
                payPalVariables["PayerID"] = Request.QueryString["PayerID"];

                checkoutOrderInfo.PayPalVariables.Merge(payPalVariables);
                checkoutOrderInfo.PaymentProvider = PaymentProviderName.PayPalExpressCheckout;

                PayPalExpressCheckoutPaymentProvider payPalExpressCheckout = new PayPalExpressCheckoutPaymentProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalExpressCheckout));
                Order pendingOrder = Order.GetOrder(payPalExpressCheckout.GetOrderIdForTransactionToken(payPalToken).GetValueOrDefault(-1));
                if (pendingOrder != null)
                {
                    payPalExpressCheckout.UpdateOrderWithExpressCheckoutDetails(pendingOrder, payPalToken);

                    checkoutOrderInfo.SetBillingAddressFromOrder(pendingOrder);
                    checkoutOrderInfo.SetShippingAddressFromOrder(pendingOrder);

                    //checkoutOrderInfo.ReCalculateOrderTotals();
                }

                UpdateCheckoutSession(checkoutOrderInfo);
            }
        }

        private void ShowReviewOrder()
        {
            //--- DataBind the cart items
            List<vCartItemProductInfo> cartItems = checkoutOrderInfo.Cart.GetCartItemsWithProductInfo();
            rptCheckoutItems.DataSource = cartItems;
            rptCheckoutItems.DataBind();

            //--- Billing/Shipping Summaries
            litBillToSummary.Text = checkoutOrderInfo.BillingAddress.ToHumanFriendlyString("<br />");
            litShipToSummary.Text = checkoutOrderInfo.ShippingAddress.ToHumanFriendlyString("<br />", false, false);

            //--- Payment Summary
            if (checkoutOrderInfo.Total == 0)
            {
                litPaymentSummary.Text = "N / A";
            }
            else if (checkoutOrderInfo.PaymentProvider == PaymentProviderName.PayPalExpressCheckout)
            {
                litPaymentSummary.Text = "PayPal Express Checkout";
            }
            else if (checkoutOrderInfo.PaymentProvider == PaymentProviderName.PayLater)
            {
                PayLaterPaymentProvider billMeLaterProvider = new PayLaterPaymentProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayLater));
                litPaymentSummary.Text = string.Format("{0}<br />{1}", billMeLaterProvider.DisplayText, billMeLaterProvider.CustomerInstructions);
            }
            else
            {
                CreditCardInfo cardInfo = checkoutOrderInfo.CreditCard;
                litPaymentSummary.Text = string.Format(@"{0}<br />xxxx-xxxx-xxxx-{1}<br />{2}/{3}", cardInfo.CardType, cardInfo.CardNumber.Right(4), cardInfo.ExpireMonth2Digits, cardInfo.ExpireYear);
            }

            //--- Order Notes
            RequireOrderNotes = StoreContext.CurrentStore.GetSettingBool(StoreSettingNames.RequireOrderNotes).GetValueOrDefault(false);
        
            txtOrderNotes.Text = checkoutOrderInfo.OrderNotes;

        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            PlaceOrder();
        }

        private void PlaceOrder()
        {
            if (checkoutOrderInfo != null)
            {
                bool doSuccessRedirect = false;
                string nextPageFlashMsg = "";
                CheckoutResult checkoutResult = null;

                checkoutOrderInfo.OrderNotes = txtOrderNotes.Text;

                try
                {
                    OrderController orderController = new OrderController(StoreContext);

                    if (checkoutOrderInfo.PaymentProvider == PaymentProviderName.PayPalExpressCheckout)
                    {
                        //---- PayPal Express Checkout                        
                        checkoutResult = orderController.CheckoutWithPayPalExpressCheckout(checkoutOrderInfo.PayPalVariables);
                    }
                    else
                    {
                        //---- Non-PayPal Process
                        checkoutResult = orderController.CheckoutWithOnSitePayment(checkoutOrderInfo);
                    }

                    if (checkoutResult.Errors.Count > 0)
                    {
                        StringBuilder errorList = new StringBuilder();
                        errorList.Append("<ul>");
                        checkoutResult.Errors.ForEach(e => errorList.AppendFormat("<li>{0}</li>", e));
                        errorList.Append("</ul>");

                        msgFlash.Visible = true;
                        msgFlash.InnerHtml = string.Format(@"Oh no! Something went wrong when we tried to process your order: {0}", errorList);

                        LogToDnnEventLog(string.Format(@"Checkout ERROR. PortalId: {0} StoreId: {1} OrderId : {2}. Errors: {3}", PortalId, StoreContext.CurrentStore.Id, (checkoutResult.SubmittedOrder != null) ? checkoutResult.SubmittedOrder.Id.ToString() : "", checkoutResult.Errors.ToCsv()));

                        doSuccessRedirect = false;
                    }
                    else if (checkoutResult.Warnings.Count > 0)
                    {
                        nextPageFlashMsg = string.Format(@"We received your order successfully, but we encountered some problems during processing: {0}", checkoutResult.Warnings.ToCsv());

                        LogToDnnEventLog(string.Format(@"Checkout WARNING. PortalId: {0} StoreId: {1} OrderId : {2}. Errors: {3}", PortalId, StoreContext.CurrentStore.Id, (checkoutResult.SubmittedOrder != null) ? checkoutResult.SubmittedOrder.Id.ToString() : "", checkoutResult.Errors.ToCsv()));

                        // still show "complete" page if we have warnings
                        doSuccessRedirect = true;
                    }
                    else if (checkoutResult.SubmittedOrder != null)
                    {
                        if (checkoutResult.SubmittedOrder.OrderStatus == OrderStatusName.Completed || checkoutResult.SubmittedOrder.OrderStatus == OrderStatusName.Processing)
                        {
                            doSuccessRedirect = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);

                    msgFlash.Visible = true;
                    msgFlash.InnerHtml = string.Format(@"Oops! An unexpected error has occurred while trying to process your order. Error: {0} {1}", ex.Message, ex.StackTrace);
                }

                if (checkoutResult != null)
                {
                    // We need to Response.Redirect() outside of the try/catch block to avoid throwing a "Thread was being aborted exception"
                    if (doSuccessRedirect)
                    {
                        Response.Redirect(StoreUrls.CheckoutCompleteForOrder(checkoutResult.SubmittedOrder.CreatedFromCartId.Value, nextPageFlashMsg));
                    }
                }
                else
                {
                    Exceptions.LogException(new ApplicationException("CheckoutResult is NULL"));

                    msgFlash.Visible = true;
                    msgFlash.InnerHtml = string.Format(@"Oops! An unexpected error has occurred while trying to process your order. Error: CheckoutResult is NULL");
                }
            }
            else
            {
                Exceptions.LogException(new ApplicationException("CheckoutOrderInfo is NULL in the Session!"));

                msgFlash.Visible = true;
                msgFlash.InnerHtml = string.Format(@"Oops! An unexpected error has occurred while trying to process your order. Error: CheckoutOrderInfo is NULL");
            }
        }

        private void LoadResourceFileSettings()
        {
            litBillingBreadcrumb.Text = ResourceString("litBillingBreadcrumb.Text");
            litShippingBreadcrumb.Text = ResourceString("litShippingBreadcrumb.Text");
            litShippingMethodBreadcrumb.Text = ResourceString("litShippingMethodBreadcrumb.Text");
            litPaymentBreadcrumb.Text = ResourceString("litPaymentBreadcrumb.Text");
            litReviewOrderBreadcrumb.Text = ResourceString("litReviewOrderBreadcrumb.Text");

            litBillTo.Text = ResourceString("litBillTo.Text");
            btnPlaceOrder.Text = ResourceString("btnPlaceOrder.Text");
            lblOrderNotes.Text = ResourceString("lblOrderNotes.Text");
            litReviewOrder.Text = ResourceString("litReviewOrder.Text");
            litProcessingOrder.Text = ResourceString("litProcessingOrder.Text");

            
            
        }
    }
}