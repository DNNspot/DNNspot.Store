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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DNNspot.Store.Shipping;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Log.EventLog;
using FluentValidation.Results;
using WA.Extensions;
using EntitySpaces.Interfaces;
using PaymentProvider=DNNspot.Store.PaymentProviders.PaymentProvider;

namespace DNNspot.Store
{
    public class CheckoutResult
    {        
        public DataModel.Order SubmittedOrder { get; set; }
        public List<string> Warnings { get; set; }
        public List<string> Errors { get; set; }

        public CheckoutResult()
        {
            Warnings = new List<string>();
            Errors = new List<string>();
        }
    }

    public class OrderController
    {
        //const string orderItemsTokenDelim = "|";
        StoreContext storeContext;
        StoreUrls storeUrls;

        public OrderController(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            this.storeUrls = new StoreUrls(storeContext);
        }

        /// <summary>
        /// Create a "PendingOffsite" Order by copying the CheckoutOrderInfo into an actual Order in the database.
        /// </summary>
        /// <param name="checkoutOrderInfo"></param>
        /// <returns></returns>
        public Order CreateOrder(CheckoutOrderInfo checkoutOrderInfo, OrderStatusName orderStatus)
        {
            using (esTransactionScope transaction = new esTransactionScope())
            {
                Order pendingOrder = new Order();

                if (checkoutOrderInfo.PaymentProvider != PaymentProviderName.CardCaptureOnly)
                {
                    //--- check if we have an existing pending order for this Cart....
                    Order existingOrderByCartId = Order.GetOrderByCartId(checkoutOrderInfo.Cart.Id.Value);
                    if (existingOrderByCartId != null)
                    {
                        //existingOrderByCartId.MarkAsDeleted();
                        existingOrderByCartId.OrderStatus = OrderStatusName.Failed;
                        existingOrderByCartId.Save();
                    }
                }
                                                                
                //pendingOrder.OrderStatus = OrderStatusName.PendingOffsite;
                pendingOrder.OrderStatus = orderStatus;
                pendingOrder.PaymentStatus = PaymentStatusName.Pending;                
                
                //---- copy the Checkout Order Info into our Order database object
                pendingOrder.StoreId = storeContext.CurrentStore.Id.Value;
                pendingOrder.UserId = storeContext.UserId;
                pendingOrder.CreatedFromCartId = checkoutOrderInfo.Cart.Id;
                pendingOrder.CreatedByIP = HttpContext.Current.Request.UserHostAddress;
                pendingOrder.OrderNumber = "";  // we'll update it later

                pendingOrder.CustomerFirstName = checkoutOrderInfo.BillingAddress.FirstName;
                pendingOrder.CustomerLastName = checkoutOrderInfo.BillingAddress.LastName;
                pendingOrder.CustomerEmail = checkoutOrderInfo.BillingAddress.Email;

                pendingOrder.BillAddress1 = checkoutOrderInfo.BillingAddress.Address1;
                pendingOrder.BillAddress2 = !string.IsNullOrEmpty(checkoutOrderInfo.BillingAddress.Address2) ? checkoutOrderInfo.BillingAddress.Address2 : String.Empty;
                pendingOrder.BillCity = checkoutOrderInfo.BillingAddress.City;
                pendingOrder.BillRegion = checkoutOrderInfo.BillingAddress.Region;
                pendingOrder.BillPostalCode = checkoutOrderInfo.BillingAddress.PostalCode;
                pendingOrder.BillCountryCode = checkoutOrderInfo.BillingAddress.Country;
                pendingOrder.BillTelephone = checkoutOrderInfo.BillingAddress.Telephone;

                pendingOrder.ShipRecipientName = string.Format("{0} {1}", checkoutOrderInfo.ShippingAddress.FirstName, checkoutOrderInfo.ShippingAddress.LastName);
                pendingOrder.ShipRecipientBusinessName = checkoutOrderInfo.ShippingAddress.BusinessName ?? "";
                pendingOrder.ShipAddress1 = checkoutOrderInfo.ShippingAddress.Address1;
                pendingOrder.ShipAddress2 = checkoutOrderInfo.ShippingAddress.Address2;
                pendingOrder.ShipCity = checkoutOrderInfo.ShippingAddress.City;
                pendingOrder.ShipRegion = checkoutOrderInfo.ShippingAddress.Region;
                pendingOrder.ShipPostalCode = checkoutOrderInfo.ShippingAddress.PostalCode;
                pendingOrder.ShipCountryCode = checkoutOrderInfo.ShippingAddress.Country;
                pendingOrder.ShipTelephone = checkoutOrderInfo.ShippingAddress.Telephone;

                //--- Shipping Provider Stuff                
                pendingOrder.ShippingServiceProvider = checkoutOrderInfo.ShippingProvider.ToString();
                pendingOrder.ShippingServiceOption = checkoutOrderInfo.ShippingRate.ServiceTypeDescription ?? "";
                pendingOrder.ShippingServiceType = checkoutOrderInfo.ShippingRate.ServiceType ?? "";
                pendingOrder.ShippingServicePrice = checkoutOrderInfo.ShippingRate.Rate;

                //--- Order Notes
                pendingOrder.OrderNotes = checkoutOrderInfo.OrderNotes ?? "";

                //---- Cart Items
                List<vCartItemProductInfo> cartItems = checkoutOrderInfo.Cart.GetCartItemsWithProductInfo();
                foreach (vCartItemProductInfo cartItem in cartItems)
                {
                    Product product = cartItem.GetProduct();

                    OrderItem newItem = pendingOrder.OrderItemCollectionByOrderId.AddNew();
                    newItem.ProductId = product.Id;
                    newItem.Name = product.Name;
                    newItem.Sku = product.Sku;
                    if (product.DeliveryMethodId == 2)
                    {
                        newItem.DigitalFilename = product.DigitalFilename;
                        newItem.DigitalFileDisplayName = product.DigitalFileDisplayName;
                    }
                    newItem.ProductFieldData = cartItem.ProductFieldData;
                    newItem.Quantity = cartItem.Quantity;
                    newItem.WeightTotal = cartItem.GetWeightForQuantity();
                    newItem.PriceTotal = cartItem.GetPriceForQuantity();
                }

                pendingOrder.SubTotal = checkoutOrderInfo.SubTotal;
                pendingOrder.ShippingAmount = checkoutOrderInfo.ShippingRate.Rate;
                pendingOrder.DiscountAmount = checkoutOrderInfo.DiscountAmount;                
                pendingOrder.TaxAmount = checkoutOrderInfo.TaxAmount;
                pendingOrder.Total = checkoutOrderInfo.Total;

                //--- Coupons
                foreach(CheckoutCouponInfo checkoutCoupon in checkoutOrderInfo.GetAppliedCoupons())
                {
                    OrderCoupon orderCoupon = pendingOrder.OrderCouponCollectionByOrderId.AddNew();
                    orderCoupon.CouponCode = checkoutCoupon.CouponCode;
                    orderCoupon.DiscountAmount = checkoutCoupon.DiscountAmount;
                }

                //--- Save limited Credit Card info to order
                pendingOrder.CreditCardType = checkoutOrderInfo.CreditCard.CardType.ToString();
                // the full card number is not saved here for security
                pendingOrder.CreditCardNumberLast4 = checkoutOrderInfo.CreditCard.CardNumber.Right(4);
                pendingOrder.CreditCardExpiration = string.Format("{0} / {1}", checkoutOrderInfo.CreditCard.ExpireMonth2Digits, checkoutOrderInfo.CreditCard.ExpireYear);
                // Credit Card CVV not saved here for security
                pendingOrder.CreditCardNameOnCard = checkoutOrderInfo.CreditCard.NameOnCard;
                                
                pendingOrder.Save();

                // update the order number
                pendingOrder.OrderNumber = storeContext.CurrentStore.GetSetting(StoreSettingNames.OrderNumberPrefix) + pendingOrder.Id;
                pendingOrder.Save();

                transaction.Complete();

                int orderId = pendingOrder.Id.Value;
                pendingOrder.LoadByPrimaryKey(orderId);

                return pendingOrder;
            }                    
        }

        public CheckoutResult CheckoutWithOnSitePayment(CheckoutOrderInfo checkoutOrderInfo)
        {
            if(!checkoutOrderInfo.RequiresPayment)
            {
                checkoutOrderInfo.PaymentProvider = PaymentProviderName.None;
            }

            CheckoutOrderValidator checkoutOrderValidator = new CheckoutOrderValidator();
            ValidationResult validationResult = checkoutOrderValidator.Validate(checkoutOrderInfo);
            if (validationResult.IsValid)
            {
                Order pendingOrder = CreateOrder(checkoutOrderInfo, OrderStatusName.Processing);

                if (checkoutOrderInfo.PaymentProvider != PaymentProviderName.None)
                {
                    PaymentStatusName paymentStatus = pendingOrder.PaymentStatus;
                    if (pendingOrder.PaymentStatus != PaymentStatusName.Completed)
                    {
                        PaymentProvider paymentProcessor = PaymentProviderFactory.GetProvider(checkoutOrderInfo.PaymentProvider, storeContext.CurrentStore);

                        HttpWebResponse response = paymentProcessor.SubmitDirectPaymentRequest(pendingOrder, checkoutOrderInfo.CreditCard);
                        paymentStatus = paymentProcessor.ProcessDirectPaymentResponse(pendingOrder, response);
                    }

                    OrderStatusName orderStatus = (paymentStatus == PaymentStatusName.Completed) ? OrderStatusName.Processing : pendingOrder.OrderStatus;
                    UpdateOrderStatus(pendingOrder, orderStatus, paymentStatus);

                    pendingOrder.Save();
                }
                else
                {
                    // does not require payment (free order / order total == 0)
                    UpdateOrderStatus(pendingOrder, OrderStatusName.Processing, PaymentStatusName.Completed);
                }

                return DoPostCheckoutProcessing(pendingOrder, true);                
            }
            else
            {
                // failed validation
                return new CheckoutResult()
                           {
                               SubmittedOrder = null,
                               Errors = validationResult.Errors.ToList().ConvertAll(e => e.ErrorMessage)
                           };
            }
        }

        public CheckoutResult CheckoutWithPayPalExpressCheckout(Dictionary<string, string> payPalVariables)
        {
            string token = payPalVariables["token"];
            string payerId = payPalVariables["PayerID"];

            PayPalExpressCheckoutPaymentProvider payPalExpress = new PayPalExpressCheckoutPaymentProvider(storeContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalExpressCheckout));

            int? orderId = payPalExpress.GetOrderIdForTransactionToken(token);
            Order pendingOrder = Order.GetOrder(orderId.GetValueOrDefault(-1));
            if (pendingOrder != null)
            {
                PaymentStatusName paymentStatus = payPalExpress.DoExpressCheckoutPayment(pendingOrder, payPalVariables);
                
                OrderStatusName orderStatus = pendingOrder.OrderStatus;
                if(paymentStatus == PaymentStatusName.Completed)
                {
                    orderStatus = OrderStatusName.Completed;
                    pendingOrder.CreditCardType = CreditCardType.PayPal.ToString();

                }
                UpdateOrderStatus(pendingOrder, orderStatus, paymentStatus);                                
            }

            return DoPostCheckoutProcessing(pendingOrder, true);
        }

        public CheckoutResult CheckoutWithPayPalStandardCheckout(HttpRequest request)
        {
            Order order = null;
            PayPalStandardProvider payPalStandard = new PayPalStandardProvider(storeContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalStandard));
            int? orderId;
            if(payPalStandard.IsIpnResponse(request, out orderId))
            {
                // user arrived here through a redirect from PayPal Checkout Page
                // so we have "IPN" variables in the Request that we can process
                order = new Order();
                if (order.LoadByPrimaryKey(orderId.GetValueOrDefault(-1)))
                {
                    Exceptions.LogException(new Exception("Loaded order"));
                    PaymentStatusName paymentStatus = payPalStandard.ProcessOffsitePaymentResponse(order, request);
                    Exceptions.LogException(new Exception("paymentStatus:" + paymentStatus));
                    OrderStatusName orderStatus = order.OrderStatus;
                    Exceptions.LogException(new Exception("orderStatus:" + orderStatus));
                    if (paymentStatus == PaymentStatusName.Completed)
                    {
                        orderStatus = OrderStatusName.Processing;
                        order.CreditCardType = CreditCardType.PayPal.ToString();
                    }
                    if (order.PaymentStatus != paymentStatus || order.OrderStatus != orderStatus)
                    {
                        Exceptions.LogException(new Exception("Updating Order Status"));
                        UpdateOrderStatus(order, orderStatus, paymentStatus);
                        Exceptions.LogException(new Exception("Updated Order Status"));
                        return DoPostCheckoutProcessing(order, true);
                    }
                    return DoPostCheckoutProcessing(order, false);
                }                
            }
            else
            {
                Exceptions.LogException(new Exception("NOT AN IPN RESPONSE!"));
            }
            return null;
        }

        private CheckoutResult DoPostCheckoutProcessing(Order submittedOrder, bool sendEmails)
        {
            PostCheckoutController postCheckoutController = new PostCheckoutController(storeContext);

            return postCheckoutController.DoPostCheckoutProcessing(submittedOrder, sendEmails);
        }

        internal void UpdatePaymentStatus(Order order, PaymentStatusName paymentStatus)
        {
            UpdateOrderStatus(order, order.OrderStatus, paymentStatus);
        }

        internal void UpdateOrderStatus(Order order, OrderStatusName orderStatusName, PaymentStatusName paymentStatusName)
        {
            OrderStatusName oldOrderStatus = order.OrderStatus;
            PaymentStatusName oldPaymentStatus = order.PaymentStatus;
             
            order.PaymentStatus = paymentStatusName;
            order.OrderStatus = orderStatusName;

            //if (order.PaymentStatus == PaymentStatusName.Completed && !(order.OrderStatus == OrderStatusName.Paid || order.OrderStatus == OrderStatusName.Completed))
            //{
            //    order.OrderStatus = OrderStatusName.Paid;
            //}
            
            order.Save();

            if (oldPaymentStatus != paymentStatusName)
            {
                //--- Payment Status has changed...
                if (paymentStatusName == PaymentStatusName.Completed && WA.Parser.ToBool(storeContext.CurrentStore.GetSetting(StoreSettingNames.SendPaymentCompleteEmail)).GetValueOrDefault(true))
                {
                    // send "PaymentCompleted" email
                    EmailController emailController = new EmailController();
                    TokenHelper tokenHelper = new TokenHelper(storeContext);
                    Dictionary<string, string> emailTokens = tokenHelper.GetOrderTokens(order, true);

                    string emailResponse = emailController.SendEmailTemplate(EmailTemplateNames.PaymentCompleted, emailTokens, order.CustomerEmail, order.UpToStoreByStoreId);
                }
            }
            if (oldOrderStatus != orderStatusName)
            {
                // TODO Order Status has changed... send emails ??
            }
        }

        internal ProcessShipmentResult GetTrackingNumberAndLabels(Order order, ShipmentPackagingStrategy packagingStrategy)
        {
            IShippingService shippingService = null;
            if (order.ShippingServiceProvider == ShippingProviderType.FedEx.ToString())
            {                
                shippingService = ShippingServiceFactory.Get(storeContext.CurrentStore.Id.Value, ShippingProviderType.FedEx, order.Id, order.CreatedFromCartId);
            }
            else if (order.ShippingServiceProvider == ShippingProviderType.UPS.ToString())
            {
                shippingService = ShippingServiceFactory.Get(storeContext.CurrentStore.Id.Value, ShippingProviderType.UPS, order.Id, order.CreatedFromCartId);
            }
            else if (order.ShippingServiceProvider == ShippingProviderType.CustomShipping.ToString())
            {
                shippingService = ShippingServiceFactory.Get(storeContext.CurrentStore.Id.Value, ShippingProviderType.CustomShipping);
            }
            if (shippingService == null)
            {
                return new ProcessShipmentResult()
                           {
                               Success = false,
                               ErrorMessages = new List<string>()
                                                   {
                                                       string.Format(@"{0} was not processed because '{1}' is an un-supported shipping provider.", order.OrderNumber, order.ShippingServiceProvider)
                                                   }
                           };
            }

            var store = storeContext.CurrentStore;
            var shipmentLabelRequest = new ShipmentLabelRequest();
            shipmentLabelRequest.SenderAddress = store.Address.ToPostalAddress();
            shipmentLabelRequest.SenderContact = new Contact()
                                                     {
                                                         FirstName = store.Address.FirstName,
                                                         LastName = store.Address.LastName,
                                                         CompanyName = store.Name,
                                                         Phone = store.GetSetting(StoreSettingNames.StorePhoneNumber)
                                                     };
            shipmentLabelRequest.RecipientAddress = new PostalAddress()
                                                        {
                                                            Address1 = order.ShipAddress1,
                                                            Address2 = order.ShipAddress2,
                                                            City = order.ShipCity,
                                                            Region = order.ShipRegion,
                                                            PostalCode = order.ShipPostalCode,
                                                            CountryCode = order.ShipCountryCode
                                                        };
            shipmentLabelRequest.RecipientContact = new Contact()
                                                        {
                                                            FirstName = order.CustomerFirstName,
                                                            LastName = order.CustomerLastName,
                                                            CompanyName = order.ShipRecipientBusinessName,
                                                            Phone = order.ShipTelephone
                                                        };
            shipmentLabelRequest.ServiceType = order.ShippingServiceType;
            shipmentLabelRequest.Packages = order.GetOrderItemsAsShipmentPackages(packagingStrategy);

            var response = shippingService.GetShipmentLabels(shipmentLabelRequest);            
            if (response.Labels.Count > 0)
            {
                // save tracking #
                if (!string.IsNullOrEmpty(response.MasterTrackingNumber))
                {
                    order.ShippingServiceTrackingNumber = response.MasterTrackingNumber;
                }

                List<string> trackingNumbers = new List<string>();
                List<string> labelFilenames = new List<string>();                
                foreach(var label in response.Labels)
                {                    
                    // save shipping label file(s)
                    if(label.LabelFile != null && label.LabelFile.Length > 0)
                    {
                        string labelDirPath = storeUrls.ShippingLabelFolderFileRoot;
                        if (!Directory.Exists(labelDirPath))
                        {
                            Directory.CreateDirectory(labelDirPath);
                        }

                        string fileExt = "." + label.LabelFileType.ToString().ToLower();
                        string filename = Guid.NewGuid().ToString();
                        if(label.PackageDetail != null && !string.IsNullOrEmpty(label.PackageDetail.ReferenceCode))
                        {
                            filename = label.PackageDetail.ReferenceCode;
                        }
                        string filePath = Path.Combine(labelDirPath, filename + fileExt);  
                        if(File.Exists(filePath) && label.PackageDetail != null && !string.IsNullOrEmpty(label.PackageDetail.ReferenceCode))
                        {
                            filename = label.PackageDetail.ReferenceCode + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmssZ");
                            filePath = Path.Combine(labelDirPath, filename + fileExt);
                        }

                        File.WriteAllBytes(filePath, label.LabelFile);                        
                        labelFilenames.Add(Path.GetFileName(filePath));
                        if(!string.IsNullOrEmpty(label.TrackingNumber))
                        {
                            trackingNumbers.Add(label.TrackingNumber);
                        }
                    }                    
                }
                order.ShippingServiceLabelFile = string.Join(",", labelFilenames.ToArray());
                if(trackingNumbers.Count > 0)
                {
                    order.ShippingServiceTrackingNumber = string.Join(",", trackingNumbers.ToArray());
                }
                order.Save();

                //// update order status to 'Completed'
                //UpdateOrderStatus(order, OrderStatusName.Completed, order.PaymentStatus);
            }

            return new ProcessShipmentResult()
            {
                Success = true,
                ErrorMessages = new List<string>(),
                ShippingLabels = response
            };
        }

        internal void SendShippingEmail(Order order)
        {
            // send "shipping update" email...
            try
            {
                TokenHelper tokenHelper = new TokenHelper(this.storeContext);
                var tokens = tokenHelper.GetOrderTokens(order, true);

                EmailController emailer = new EmailController();
                emailer.SendEmailTemplate(EmailTemplateNames.ShippingUpdate, tokens, order.CustomerEmail, this.storeContext.CurrentStore);
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
            }            
        }
    }
}
