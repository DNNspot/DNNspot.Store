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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using WA.Extensions;

namespace DNNspot.Store.Modules.Checkout
{
    public partial class CheckoutComplete : StoreModuleBase //StoreCheckoutModuleBase
    {
        protected DataModel.Order order = new Order();

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlOrderDetails.Visible = false;
            LoadResourceFileSettings();

            if (!IsPostBack)
            {
                PayPalStandardProvider payPalStandard =
                new PayPalStandardProvider(
                StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalStandard));
                bool isPayPalIpn = false;
                bool isPayPalReferrer = false;
                bool isPayPalStandardReturn = false;

                if (payPalStandard.IsEnabled)
                {
                    isPayPalIpn = payPalStandard.IsIpnResponse(Request);
                    isPayPalReferrer = (Request.UrlReferrer != null)
                                           ? Request.UrlReferrer.ToString().Contains("paypal.com")
                                           : false;
                    isPayPalStandardReturn = Request.Params["ppstdreturn"] != null;
                }

                Guid? completedOrderId = WA.Parser.ToGuid(Request.Params["dsoid"]);
                if (completedOrderId.HasValue && !(isPayPalReferrer || isPayPalIpn))
                {
                    // load an on-site order
                    OrderQuery q = new OrderQuery();
                    q.Where(q.CreatedFromCartId == completedOrderId.Value);
                    q.es.Top = 1;
                    order.Load(q);
                }
                else if (isPayPalReferrer || isPayPalIpn)
                {
                    OrderController orderController = new OrderController(StoreContext);
                    CheckoutResult checkoutResult = orderController.CheckoutWithPayPalStandardCheckout(Request);
                    if (checkoutResult != null)
                    {
                        order = checkoutResult.SubmittedOrder;
                    }
                }

                //bool clientHasCartIdCookie = order.CreatedFromCartId.Value == StoreContext.CartId;

                //if (UserInfo.IsSuperUser || UserInfo.IsInRole("Administrator") || UserInfo.UserID == order.UserId)
                //if (UserInfo.IsSuperUser || UserInfo.IsInRole("Administrator") || clientHasCartIdCookie)
                //{
                pnlOrderDetails.Visible = true;
                pnlOrderReceipt.Visible = true;



                if (order.Id.HasValue)
                {
                    ShowDigitalDownloads();

                    if (order.PaymentStatus == PaymentStatusName.Pending)
                    {
                        pnlDigitalDownloads.Visible = false;
                    }

                    if (isPayPalStandardReturn)
                    {
                        pnlOrderReceipt.Visible = false;
                    }
                    else
                    {
                        LoadOrderReceipt();
                    }
                }
                else
                {
                    //ShowFlash("Unable to find order");
                    //flash.Text = "Unable to find order";
                    ShowFlash("Could not load Order from DB");
                    //flash.Text = "Could not load Order from DB";
                    pnlOrderReceipt.Visible = false;
                    pnlDigitalDownloads.Visible = false;
                }
                //}
            }
        }

        private void ShowDigitalDownloads()
        {
            List<OrderItem> orderItems = order.OrderItemCollectionByOrderId.ToList();
            List<OrderItem> digitalOrderItems = orderItems.FindAll(oi => !string.IsNullOrEmpty(oi.DigitalFilename));
            if (digitalOrderItems.Count > 0)
            {
                pnlDigitalDownloads.Visible = true;

                rptDigitalFiles.DataSource = digitalOrderItems;
                rptDigitalFiles.DataBind();
            }
        }

        private void LoadOrderReceipt()
        {
            pnlOrderReceipt.Visible = true;

            rptOrderItems.DataSource = order.OrderItemCollectionByOrderId;
            rptOrderItems.DataBind();

            if (order.GetPaymentProviderName().ToLower() == "paylater")
            {
                PayLaterPaymentProvider payLaterProvider = new PayLaterPaymentProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayLater));
                litPaymentSummary.Text = string.Format("{0}<br />{1}", payLaterProvider.DisplayText, payLaterProvider.CustomerInstructions.NewlineToBr());
            }
            else
            {
                litPaymentSummary.Text = string.Format("{0}<br />xxxx-{1}", order.CreditCardType, order.CreditCardNumberLast4);
            }
        }

        private void LoadResourceFileSettings()
        {
            litCheckoutComplete.Text = ResourceString("litCheckoutComplete.Text");
            litOrderReceived.Text = ResourceString("litOrderReceived.Text");
            litYouWillReceiveAnEmail.Text = ResourceString("litYouWillReceiveAnEmail.Text");
            litOrderReceipt.Text = ResourceString("litOrderReceipt.Text");
            litOrderNumber.Text = ResourceString("litOrderNumber.Text");
            litBillingInformation.Text = ResourceString("litBillingInformation.Text");
            litNoShippableItems.Text = ResourceString("litNoShippableItems.Text");
            litPrintOrderDetails.Text = ResourceString("litPrintOrderDetails.Text");
            

        }
    }
}