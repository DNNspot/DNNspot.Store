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
using DotNetNuke.Services.Exceptions;
using WA.Extensions;

namespace DNNspot.Store.PaymentProviders
{
    public class PayPalStandardProvider: PaymentProvider
    {
        public PayPalStandardProvider(ProviderConfig config)
            : base(config)
        {            
        }

        public string EmailAddress
        {
            get { return config.Settings.ContainsKey("Email") ? config.Settings["Email"] : ""; }
            set { config.Settings["Email"] = value; }
        }

        public string ShippingLogic
        {
            get { return config.Settings.ContainsKey("ShippingLogic") ? config.Settings["ShippingLogic"] : ""; }
            set { config.Settings["ShippingLogic"] = value; }
        }

        public bool IsSandbox
        {
            get
            {
                if(config.Settings.ContainsKey("IsSandbox"))
                {
                    return WA.Parser.ToBool(config.Settings["IsSandbox"]).GetValueOrDefault(false);
                }
                return false;
            }
            set { config.Settings["IsSandbox"] = value.ToString(); }
        }

        public string ProviderUrl
        {
            get { return IsSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr"; }
        }

        public override Dictionary<string, string> CreateOffsitePaymentRequestVariables(Order order, StoreUrls storeUrls)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();

            fields["cmd"] = "_cart";
            fields["upload"] = "1";
            fields["business"] = EmailAddress;
            fields["custom"] = order.Id.Value.ToString();
            //fields["custom"] = order.OrderNumber;

            string siteWebRoot = storeUrls.PortalUrlRoot;
            string moduleRootWebPath = storeUrls.ModuleFolderUrlRoot;
            fields["shopping_url"] = storeUrls.CategoryHome();
            fields["return"] = storeUrls.CheckoutCompleteForOrder(order.CreatedFromCartId.Value, string.Empty, new List<string>() { "ppstdreturn=true" }); // URL the user is returned to after PayPal Checkout is complete            
            // BEFORE 7/1/2011 FIX
            //fields["notify_url"] = string.Format("{0}DesktopModules/DNNspot-Store/PayPal/PayPalIpnHandler.aspx", moduleRootWebPath);

            fields["notify_url"] = string.Format("{0}PayPal/PayPalIpnHandler.aspx", moduleRootWebPath);

            fields["currency_code"] = order.UpToStoreByStoreId.Currency.Code;

            // populate the Cart/Order Items
            IEnumerable<OrderItem> orderitems = order.OrderItemCollectionByOrderId;
            int itemNumber = 1;

            foreach (OrderItem orderItem in orderitems)
            {
                string orderItemAttributes = orderItem.GetProductFieldDataPlainTextDisplayString();

                if(!orderItem.UpToProductByProductId.IsTaxable.GetValueOrDefault(true))
                {
                    fields["tax_rate_" + itemNumber] = "0.00";
                }
                else
                {

                    //fields["tax_rate_" + itemNumber] = TaxRegion.GetTaxRate(cart.StoreId.GetValueOrDefault(-1), taxCountry, taxRegion);
                }

                fields["item_name_" + itemNumber] = orderItem.Name + (!string.IsNullOrEmpty(orderItemAttributes) ? " (" + orderItemAttributes + ")" : "");
                //fields["amount_" + itemNumber] = orderItem.PriceTotal.Value.ToString("N2"); // WRONG! "PriceTotal" includes the Qty. multiplier!
                fields["amount_" + itemNumber] = orderItem.PriceForSingleItem.ToString("N2");
                fields["quantity_" + itemNumber] = orderItem.Quantity.Value.ToString("N0");
                fields["item_number_" + itemNumber] = orderItem.Sku;

                itemNumber++;
            }
            if (order.DiscountAmount.GetValueOrDefault(0) > 0)
            {
                fields["discount_amount_cart"] = order.DiscountAmount.GetValueOrDefault(0).ToString("F2");
            }
            
            if(order.ShippingAmount.HasValue && ShippingLogic == "Store")
            {
                //fields["shipping"] = order.ShippingAmount.Value.ToString("N2"); // doesn't seem to be honored by PayPal for cart uploads...
                fields["handling_cart"] = order.ShippingAmount.Value.ToString("N2");
            }

            if(order.HasNonEmptyShippingAddress())
            {
                fields["address1"] = order.ShipAddress1.ChopAt(100);
                fields["address2"] = order.ShipAddress2.ChopAt(100);
                fields["city"] = order.ShipCity.ChopAt(40);
                fields["state"] = order.ShipRegion;
                fields["zip"] = order.ShipPostalCode.ChopAt(32);
                fields["country"] = order.ShipCountryCode.ChopAt(2);
            }

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = ProviderUrl;
            newTransaction.GatewayTransactionId = "";
            newTransaction.GatewayResponse = "CreateOffsitePaymentRequest";
            newTransaction.GatewayDebugResponse = fields.ImplodeToList("=").ToDelimitedString(", ");
            newTransaction.Save();

            return fields;
        }

        public bool IsIpnResponse(HttpRequest request)
        {
            int? orderId;
            return IsIpnResponse(request, out orderId);
        }

        public bool IsIpnResponse(HttpRequest request, out int? orderId)
        {
            NameValueCollection fields = request.Params;

            orderId = WA.Parser.ToInt(fields["custom"]);
            string receiverEmail = fields["receiver_email"] ?? "";
            string businessEmail = fields["business"] ?? "";

            Exceptions.LogException(new Exception(string.Format("IsIpnResponse(): orderId: {0}, receiverEmail: {1}, businessEmail: {2}, EmailAddress: {3}", orderId, receiverEmail, businessEmail, EmailAddress)));

            if (string.IsNullOrEmpty(receiverEmail) && string.IsNullOrEmpty(businessEmail))
            {
                return false;
            }
            else
            {
                return (orderId.HasValue && ((receiverEmail == EmailAddress) || (businessEmail == EmailAddress)));
            }
        }
        
        public override PaymentStatusName ProcessOffsitePaymentResponse(Order order, HttpRequest request)
        {
            //---- Need to validate the incoming request with PayPal so we know it's authentic            
            Dictionary<string, string> requestParams = HttpHelper.DecodeParamsFromHttpRequest(request);
            string requestDataString = HttpHelper.EncodeVarsForHttpPostString(requestParams);
            
            // send our modified request back to PayPal for validation
            string postData = requestDataString + "&cmd=_notify-validate";
            HttpWebResponse payPalResponse = HttpHelper.HttpPost(ProviderUrl, postData);

            string payPalResponseString = HttpHelper.WebResponseToString(payPalResponse);

            // Parse response into some variables
            NameValueCollection fields = request.Params;
            string paymentStatus = fields["payment_status"];
            string transactionId = fields["txn_id"];
            string receiverEmail = fields["receiver_email"];
            string businessEmail = fields["business"];

            int? orderId = WA.Parser.ToInt(fields["custom"]);
            decimal? paymentAmount = WA.Parser.ToDecimal(fields["mc_gross"]);   // amount before PayPal fee is subtracted
            decimal? payPalTransactionFee = WA.Parser.ToDecimal(fields["mc_fee"]);
            decimal shippingAmount = WA.Parser.ToDecimal(fields["mc_shipping"]).GetValueOrDefault(0);
            decimal handlingAmount = WA.Parser.ToDecimal(fields["mc_handling"]).GetValueOrDefault(0);
            decimal taxAmount = WA.Parser.ToDecimal(fields["tax"]).GetValueOrDefault(0);

            string firstName = fields["first_name"] ?? "";
            string lastName = fields["last_name"] ?? "";
            string email = fields["payer_email"] ?? "";
            string phone = fields["contact_phone"] ?? "";

            string shipName = fields["address_name"] ?? "";
            string shipAddress1 = fields["address_street"] ?? "";
            string shipCity = fields["address_city"] ?? "";
            string shipRegion = fields["address_state"] ?? "";
            string shipPostalCode = fields["address_zip"] ?? "";
            string shipCountryCode = fields["address_country_code"] ?? "";

            PaymentStatusName paymentStatusName = order.PaymentStatus;
            bool isValidReceiverEmail = (receiverEmail == EmailAddress) || (businessEmail == EmailAddress);

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = orderId;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = ProviderUrl;
            newTransaction.GatewayTransactionId = transactionId;
            newTransaction.GatewayResponse = paymentStatus;
            newTransaction.GatewayDebugResponse = requestDataString;
            newTransaction.Amount = paymentAmount;

            if(payPalResponseString != "VERIFIED")
            {
                newTransaction.GatewayError = "Invalid/Unknown response or response could not be validated as authentic";
                newTransaction.Save();

                return PaymentStatusName.ProviderError;                                
            }
            if(!isValidReceiverEmail)
            {
                newTransaction.GatewayError = "Receiver Email does not match PayPal Account";
                newTransaction.Save();

                return PaymentStatusName.ProviderError;
            }

            //Order pendingOrder = Order.GetOrder(orderId);
            if (payPalResponseString == "VERIFIED")
            {
                // check if we have already processed this transaction successfully
                // we might already have processed the IPN callback, and the user clicks "return to store"
                // which would trigger another transaction
                PaymentTransaction priorTransaction = PaymentTransaction.GetMostRecentByTransactionId(transactionId);
                if(priorTransaction != null && priorTransaction.OrderId == orderId)
                {
                    // we have a duplicate transaction, we should NOT process this new one
                    return order.PaymentStatus;
                }

                //check the payment_status is Completed                    
                bool paymentStatusComplete = (paymentStatus.ToLower() == "completed");
        
                if(paymentAmount.GetValueOrDefault(-1) < order.Total.Value)
                {
                    newTransaction.GatewayError = string.Format("Payment amount does not match Order Total. Order total {0:N2}. Payment amount {1:N2}", order.Total, paymentAmount);
                    newTransaction.Save();
                    return PaymentStatusName.ProviderError;
                }
                
                //if (paymentStatusComplete && paymentAmountMatches && receiverEmailMatches)
                if (paymentStatusComplete)
                {
                    newTransaction.GatewayResponse = "Verified Payment Completed";
                    if(request.RawUrl.Contains("PayPalIpnHandler.aspx"))
                    {
                        newTransaction.GatewayResponse += " via IPN";
                    }
                    else if (request.RawUrl.Contains("Checkout-Complete.aspx"))
                    {
                        newTransaction.GatewayResponse += " via return button from PayPal";
                    }
                    paymentStatusName = PaymentStatusName.Completed;

                    //---- Update the Order
                    order.CustomerFirstName = firstName;
                    order.CustomerLastName = lastName;
                    order.CustomerEmail = email;

                    order.ShipRecipientName = shipName;
                    order.ShipAddress1 = shipAddress1;
                    //order.ShipAddress2 = shipAddress2;
                    order.ShipCity = shipCity;
                    order.ShipRegion = shipRegion;
                    order.ShipPostalCode = shipPostalCode;
                    order.ShipCountryCode = shipCountryCode;
                    order.ShipTelephone = phone;

                    order.BillAddress1 = shipAddress1;
                    //order.BillAddress2 = shipAddress2;
                    order.BillCity = shipCity;
                    order.BillRegion = shipRegion;
                    order.BillPostalCode = shipPostalCode;
                    order.BillCountryCode = shipCountryCode;
                    order.BillTelephone = phone;

                    // PayPal 'paymentAmount' is the total amount paid by customer
                    // so we need to do some basic math to get the other order amounts...
                    order.ShippingAmount = shippingAmount + handlingAmount;
                    order.TaxAmount = taxAmount;
                    order.SubTotal = paymentAmount - (order.ShippingAmount + order.TaxAmount);
                    order.Total = paymentAmount;

                    order.Save();
                }
                newTransaction.Save();
            }         

            return paymentStatusName;
        }

        public override HttpWebResponse SubmitDirectPaymentRequest(Order order, CreditCardInfo creditCardInfo)
        {
            throw new NotImplementedException();
        }

        public override PaymentStatusName ProcessDirectPaymentResponse(Order order, HttpWebResponse response)
        {
            throw new NotImplementedException();
        }
    }
}