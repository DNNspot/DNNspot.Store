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
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DNNspot.Store.Shipping;
using WA.Components;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class ViewOrder : StoreAdminModuleBase
    {
        protected Order order = new Order();
        protected PaymentProviderName orderPaymentProvider = PaymentProviderName.UNKNOWN;

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Orders", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Orders) },
                   new AdminBreadcrumbLink() { Text = "View Order" }
               };
        }             

        private bool LoadOrder()
        {
            int? orderId = WA.Parser.ToInt(Request.QueryString["id"]);

            return order.LoadByPrimaryKey(orderId.GetValueOrDefault(-1));
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if(!IsPostBack)
            {
                int? deleteId = WA.Parser.ToInt(Request.QueryString["delete"]);
                if(deleteId.HasValue)
                {
                    Order toDelete = new Order();
                    if(toDelete.LoadByPrimaryKey(deleteId.Value))
                    {
                        //--- SOFT Delete
                        toDelete.IsDeleted = true;
                        toDelete.OrderStatus = OrderStatusName.Deleted;
                        toDelete.Save();

                        Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.Orders));
                    }
                }

                if(LoadOrder())
                {
                    FillListControls();

                    IShippingService shippingService = null;
                    if (order.ShippingServiceProvider.Contains("UPS", false))
                    {
                        shippingService = ShippingServiceFactory.Get(order.StoreId.Value, ShippingProviderType.UPS, order.Id, order.CreatedFromCartId);
                    }
                    else if (order.ShippingServiceProvider.Contains("FedEx", false))
                    {
                        shippingService = ShippingServiceFactory.Get(order.StoreId.Value, ShippingProviderType.FedEx, order.Id, order.CreatedFromCartId);
                    }
                    bool shipperSupportsTrackingNumbers = (shippingService != null) && ((shippingService is FedExShippingService) || (shippingService is UpsShippingService));

                    ddlOrderStatus.SelectedValue = order.OrderStatus.ToString();
                    ddlPaymentStatus.SelectedValue = order.PaymentStatus.ToString();

                    txtShippingTrackingNumber.Text = order.ShippingServiceTrackingNumber;
                    if (!string.IsNullOrEmpty(order.ShippingServiceTrackingNumber))
                    {
                        btnSaveTrackingNumber.Visible = false;
                        txtShippingTrackingNumber.Visible = false;
                    }

                    rptShippingLog.DataSource = order.GetShippingLogEntries();
                    rptShippingLog.DataBind();
                    rptShippingLog.Visible = (rptShippingLog.Items.Count > 0);

                    rptPaymentTransactions.DataSource = order.GetPaymentTransactionsOldestFirst();
                    rptPaymentTransactions.DataBind();
                    rptPaymentTransactions.Visible = (rptPaymentTransactions.Items.Count > 0);

                    rptOrderItems.DataSource = order.OrderItemCollectionByOrderId;
                    rptOrderItems.DataBind();

                    //if(order.PaymentStatus != PaymentStatusName.Completed && StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.OfflinePayment) && order.Total.GetValueOrDefault(0) > 0)
                    if (order.PaymentStatus != PaymentStatusName.Completed && order.Total.GetValueOrDefault(0) > 0)
                    {
                        btnMarkOfflinePaid.Visible = true;
                    }

                    btnGetShippingTrackingLabels.Visible = false;                    
                    btnSendShippingEmail.Visible = false;
                    btnMarkShipped.Visible = false;

                    if(!string.IsNullOrEmpty(order.ShippingServiceTrackingNumber))
                    {
                        btnGetShippingTrackingLabels.CssClass += " ok";
                    }

                    switch(order.OrderStatus)
                    {
                        case OrderStatusName.Processing:
                            btnMarkShipped.Visible = true;
                            btnGetShippingTrackingLabels.Visible = order.HasShippableItems;                            
                            break;
                        case OrderStatusName.Completed:
                            btnSendShippingEmail.Visible = order.HasShippableItems;
                            if(string.IsNullOrEmpty(order.ShippingServiceTrackingNumber))
                            {
                                btnGetShippingTrackingLabels.Visible = order.HasShippableItems;
                            }
                            break;
                    }                 

                    if(!shipperSupportsTrackingNumbers)
                    {
                        btnGetShippingTrackingLabels.Visible = false;                        
                    }
                    divShipmentPackaging.Visible = btnGetShippingTrackingLabels.Visible;

                    ShipmentPackagingStrategy shipmentPackagingStrategy = WA.Enum<ShipmentPackagingStrategy>.TryParseOrDefault(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShipmentPackagingStrategy), ShipmentPackagingStrategy.SingleBox);
                    ddlPackageGrouping.SelectedValue = shipmentPackagingStrategy.ToString();                                        
                }
            }
        }

        private void FillListControls()
        {
            var orderStatuses = new List<string>(WA.Enum<OrderStatusName>.GetNames());
            orderStatuses.Remove(OrderStatusName.Deleted.ToString());
            ddlOrderStatus.DataSource = orderStatuses;
            ddlOrderStatus.DataBind();

            var paymentStatuses = new List<string>(WA.Enum<PaymentStatusName>.GetNames());
            paymentStatuses.Remove(PaymentStatusName.ProviderError.ToString());
            ddlPaymentStatus.DataSource = paymentStatuses;
            ddlPaymentStatus.DataBind();
        }

        protected void btnMarkPaid_Click(object sender, EventArgs e)
        {            
            if(LoadOrder())
            {
                order.MarkAsPaid();

                Response.Redirect(StoreUrls.AdminViewOrder(order.Id.Value));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(LoadOrder())
            {
                order.ShippingServiceTrackingNumber = txtShippingTrackingNumber.Text;
                order.Save();

                //OrderController controller = new OrderController(StoreContext);
                //controller.UpdateOrderStatus(order, WA.Enum<OrderStatusName>.Parse(ddlOrderStatus.SelectedValue), WA.Enum<PaymentStatusName>.Parse(ddlPaymentStatus.SelectedValue));

                Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.ViewOrder, "Order saved", "id=" + order.Id.Value));
            }
        }

        protected void btnSaveTrackingNumber_Click(object sender, EventArgs e)
        {
            if (LoadOrder())
            {
                order.ShippingServiceTrackingNumber = txtShippingTrackingNumber.Text;
                order.Save();

                Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.ViewOrder, "Tracking Number Saved", "id=" + order.Id.Value));
            }
        }
        
        //protected void btnProcessShipment_Click(object sender, EventArgs e)
        //{
        //    if (LoadOrder())
        //    {
        //        var orderController = new OrderController(StoreContext);
        //        var shipResult = orderController.ProcessShipment(order);
        //        string flashMsg = shipResult.Success ? "Shipment Processed Successfully<br />Shipping Update email sent to customer" : shipResult.ErrorMessages.ToDelimitedString("<br />");

        //        Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.ViewOrder, "id=" + order.Id, "flash=" + HttpUtility.UrlPathEncode(flashMsg)));
        //    }
        //}

        //protected void btnViewShippingLabel_Click(object sender, EventArgs e)
        //{
        //    if(LoadOrder() && !string.IsNullOrEmpty(order.ShippingServiceLabelFile))
        //    {
        //        Response.Redirect(StoreUrls.ShippingLabelFolderUrlRoot + order.ShippingServiceLabelFile);
        //    }
        //}

        protected void btnSendShippingEmail_Click(object sender, EventArgs e)
        {
            if(LoadOrder())
            {
                TokenHelper tokenHelper = new TokenHelper(StoreContext);
                var tokens = tokenHelper.GetOrderTokens(order, true);

                var tokenizer = new TokenProcessor(Constants.TemplateTokenStart, Constants.TemplateTokenEnd);
                vStoreEmailTemplate emailTemplate = StoreContext.CurrentStore.GetStoreEmailTemplate(EmailTemplateNames.ShippingUpdate);
                string subject = tokenizer.ReplaceTokensInString(HttpUtility.HtmlDecode(emailTemplate.SubjectTemplate), tokens);
                string body = tokenizer.ReplaceTokensInString(HttpUtility.HtmlDecode(emailTemplate.BodyTemplate), tokens);

                Session["from"] = StoreContext.CurrentStore.GetSetting(StoreSettingNames.CustomerServiceEmailAddress);
                Session["to"] = order.CustomerEmail;
                Session["subject"] = subject;
                Session["body"] = body;

                Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.SendCustomerEmail));
            }
        }

        protected void btnEmailCustomer_Click(object sender, EventArgs e)
        {
            if (LoadOrder())
            {
                TokenHelper tokenHelper = new TokenHelper(StoreContext);
                var tokens = tokenHelper.GetOrderTokens(order, true);

                var tokenizer = new TokenProcessor(Constants.TemplateTokenStart, Constants.TemplateTokenEnd);
                vStoreEmailTemplate emailTemplate = StoreContext.CurrentStore.GetStoreEmailTemplate(EmailTemplateNames.OrderReceived);                
                string body = tokenizer.ReplaceTokensInString(HttpUtility.HtmlDecode(emailTemplate.BodyTemplate), tokens);

                Session["from"] = StoreContext.CurrentStore.GetSetting(StoreSettingNames.CustomerServiceEmailAddress);
                Session["to"] = order.CustomerEmail;                
                Session["body"] = body;

                Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.SendCustomerEmail));
            }
        }

        protected void btnGetShippingTrackingLabels_Click(object sender, EventArgs e)
        {
            if (LoadOrder())
            {
                ShipmentPackagingStrategy packagingStrategy = WA.Enum<ShipmentPackagingStrategy>.TryParseOrDefault(ddlPackageGrouping.SelectedValue, ShipmentPackagingStrategy.SingleBox);
                var orderController = new OrderController(StoreContext);
                var shipResult = orderController.GetTrackingNumberAndLabels(order, packagingStrategy);
                string flashMsg = shipResult.Success ? "Tracking Number and Shipping Labels updated" : shipResult.ErrorMessages.ToDelimitedString("<br />");

                Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.ViewOrder, "id=" + order.Id, "flash=" + HttpUtility.UrlPathEncode(flashMsg)));
            }
        }

        protected void btnMarkShipped_Click(object sender, EventArgs e)
        {
            if (LoadOrder())
            {
                var orderController = new OrderController(StoreContext);

                // update order status to 'Completed'
                orderController.UpdateOrderStatus(order, OrderStatusName.Completed, order.PaymentStatus);

                string flashMsg = "Order Complete!";

                if(order.HasShippableItems)
                {
                    // Send shipping email
                    orderController.SendShippingEmail(order);
                    flashMsg += " Shipping notification email sent to Customer.";
                }

                Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.ViewOrder, "id=" + order.Id, "flash=" + HttpUtility.UrlPathEncode(flashMsg)));
            }
        }
    }
}