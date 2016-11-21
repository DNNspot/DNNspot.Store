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
using System.Net;
using System.Text;
using System.Web;
using DNNspot.Store.DataModel;
using WA.Extensions;

namespace DNNspot.Store.PaymentProviders
{
    /// <summary>
    /// Part of the "PayPal Website Payments PRO" Integration Method
    /// </summary>
    public class PayPalExpressCheckoutPaymentProvider : PaymentProvider
    {
        public PayPalExpressCheckoutPaymentProvider(ProviderConfig config)
            : base(config)
        {
        }

        public bool IsSandbox
        {
            get
            {
                if(config.Settings.ContainsKey("isSandbox"))
                {
                    return WA.Parser.ToBool(config.Settings["isSandbox"]).GetValueOrDefault(false);
                }
                return false;
            }
            set { config.Settings["isSandbox"] = value.ToString(); }               
        }

        public string ApiUsername
        {
            get { return config.Settings.ContainsKey("apiUsername") ? config.Settings["apiUsername"] : ""; }
            set { config.Settings["apiUsername"] = value; }            
        }

        public string ApiPassword
        {
            get { return config.Settings.ContainsKey("apiPassword") ? config.Settings["apiPassword"] : ""; }
            set { config.Settings["apiPassword"] = value; }
        }

        public string ApiSignature
        {
            get { return config.Settings.ContainsKey("apiSignature") ? config.Settings["apiSignature"] : ""; }
            set { config.Settings["apiSignature"] = value; }
        }

        public string ProviderUrl
        {
            get { return IsSandbox ? "https://api-3t.sandbox.paypal.com/nvp" : "https://api-3t.paypal.com/nvp"; }
        }

        public string GetExpressCheckoutUrl(string token)
        {
            if(IsSandbox)
            {
                return string.Format("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={0}", token);
            }
            return string.Format("https://www.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={0}", token);
        }

        public override Dictionary<string, string> CreateOffsitePaymentRequestVariables(Order order, StoreUrls storeUrls)
        {
            throw new NotImplementedException();
        }

        public override PaymentStatusName ProcessOffsitePaymentResponse(Order order, HttpRequest request)
        {
            throw new NotImplementedException();
        }

        public override HttpWebResponse SubmitDirectPaymentRequest(Order order, CreditCardInfo creditCardInfo)
        {
            throw new NotImplementedException();
        }

        // Step 1.
        public string SetExpressCheckoutAndGetToken(Order order, string cancelUrl, string returnUrl)
        {
            Dictionary<string,string> vars = new Dictionary<string, string>();

            vars["METHOD"] = "SetExpressCheckout";
            vars["VERSION"] = "60.0";

            vars["USER"] = ApiUsername;
            vars["PWD"] = ApiPassword;
            vars["SIGNATURE"] = ApiSignature;

            vars["RETURNURL"] = returnUrl;
            vars["CANCELURL"] = cancelUrl;

            //vars["CUSTOM"] = order.Id.HasValue ? order.Id.Value.ToString().Left(256) : "";
            //vars["INVNUM"] = order.Id.HasValue ? order.Id.Value.ToString().Left(127) : "";
            vars["CUSTOM"] = order.OrderNumber.Left(256);
            vars["INVNUM"] = order.OrderNumber.Left(127);
            
            vars["PAYMENTACTION"] = "Sale";
            vars["AMT"] = order.Total.Value.ToString("N2");
            vars["CURRENCYCODE"] = order.UpToStoreByStoreId.Currency.Code;
            
            if (order.HasNonEmptyShippingAddress())
            {
                // Address Override (for when user choose PayPal on the Payment Screen, instead of the "check out with paypal" button)
                vars["ADDROVERRIDE"] = "1";

                vars["SHIPTONAME"] = order.ShipRecipientName + (!string.IsNullOrEmpty(order.ShipRecipientBusinessName) ? " " + order.ShipRecipientBusinessName : "");
                vars["SHIPTOSTREET"] = order.ShipAddress1;
                vars["SHIPTOSTREET2"] = order.ShipAddress2;
                vars["SHIPTOCITY"] = order.ShipCity;
                vars["SHIPTOSTATE"] = order.ShipRegion;
                vars["SHIPTOZIP"] = order.ShipPostalCode;
                vars["SHIPTOCOUNTRYCODE"] = order.ShipCountryCode;
            }

            IEnumerable<OrderItem> orderitems = order.OrderItemCollectionByOrderId;
            int itemNumber = 0;
            foreach (OrderItem orderItem in orderitems)
            {
                string orderItemAttributes = orderItem.GetProductFieldDataPlainTextDisplayString();

                vars["L_NAME" + itemNumber] = orderItem.Name.Left(127);
                vars["L_DESC" + itemNumber] = (!string.IsNullOrEmpty(orderItemAttributes) ? " (" + orderItemAttributes + ")" : "").Left(127);
                vars["L_AMT" + itemNumber] = orderItem.PriceForSingleItem.ToString("N2");
                vars["L_QTY" + itemNumber] = orderItem.Quantity.Value.ToString("N0");
                vars["L_NUMBER" + itemNumber] = (!string.IsNullOrEmpty(orderItem.Sku) ? " (" + orderItem.Sku + ")" : "");

                itemNumber++;
            }

            // Send Http POST to PayPal
            HttpWebResponse webResponse = HttpHelper.HttpPost(ProviderUrl, vars);
            string responseString = HttpHelper.WebResponseToString(webResponse);

            // Decode the response fields from PayPal API
            Dictionary<string, string> fields = HttpHelper.DecodeVarsFromHttpString(responseString);

            // parse response fields into variables
            string ack = fields["ACK"];
            AckValues ackType = WA.Enum<AckValues>.TryParseOrDefault(ack, AckValues.Failure);            
            string correlationId = fields.TryGetValueOrEmpty("CORRELATIONID");
            string transactionId = fields.TryGetValueOrEmpty("TOKEN");            

            //string error1Code = fields.TryGetValueOrEmpty("L_ERRORCODE0");
            //string error1ShortMsg = fields.TryGetValueOrEmpty("L_SHORTMESSAGE0");
            //string error1LongMsg = fields.TryGetValueOrEmpty("L_LONGMESSAGE0");
            //string error1Severity = fields.TryGetValueOrEmpty("L_SEVERITYCODE0");

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = ProviderUrl;
            newTransaction.GatewayTransactionId = transactionId;
            newTransaction.GatewayResponse = vars["METHOD"] + ": " + ack;
            newTransaction.GatewayDebugResponse = HttpUtility.UrlDecode(responseString);
            newTransaction.Amount = order.Total;            

            string token = "";
            if (ackType == AckValues.Success || ackType == AckValues.SuccessWithWarning)
            {
                token = transactionId;
            }
            else
            {
                token = "";
                newTransaction.GatewayError = ack + " Invalid / Unknown Response from Payment Provider";                
            }
            newTransaction.Save();

            return token;
        }

        // Step 2.
        private Dictionary<string, string> GetExpressCheckoutDetails(Order order, string payPalToken)
        {
            // TODO - implement this so we can get and update the Tax, Shipping, etc. amounts for the order

            Dictionary<string, string> vars = new Dictionary<string, string>();

            vars["METHOD"] = "GetExpressCheckoutDetails";
            vars["VERSION"] = "60.0";

            vars["USER"] = ApiUsername;
            vars["PWD"] = ApiPassword;
            vars["SIGNATURE"] = ApiSignature;

            vars["TOKEN"] = payPalToken;

            IEnumerable<OrderItem> orderitems = order.OrderItemCollectionByOrderId;
            int itemNumber = 0;
            foreach (OrderItem orderItem in orderitems)
            {
                string orderItemAttributes = orderItem.GetProductFieldDataPlainTextDisplayString();

                vars["L_NAME" + itemNumber] = orderItem.Name.Left(127);
                vars["L_DESC" + itemNumber] = (!string.IsNullOrEmpty(orderItemAttributes) ? " (" + orderItemAttributes + ")" : "").Left(127);
                vars["L_AMT" + itemNumber] = orderItem.PriceForSingleItem.ToString("N2");
                vars["L_QTY" + itemNumber] = orderItem.Quantity.Value.ToString("N0");
                vars["L_NUMBER" + itemNumber] = (!string.IsNullOrEmpty(orderItem.Sku) ? " (" + orderItem.Sku + ")" : "");

                itemNumber++;
            }

            // Send Http POST to PayPal
            HttpWebResponse webResponse = HttpHelper.HttpPost(ProviderUrl, vars);
            string responseString = HttpHelper.WebResponseToString(webResponse);

            // Decode the response fields from PayPal API
            Dictionary<string, string> fields = HttpHelper.DecodeVarsFromHttpString(responseString);

            // parse response fields into variables
            string ack = fields.TryGetValueOrEmpty("ACK");
            AckValues ackType = WA.Enum<AckValues>.TryParseOrDefault(ack, AckValues.Failure);            

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = ProviderUrl;
            newTransaction.GatewayTransactionId = payPalToken;
            newTransaction.GatewayResponse = vars["METHOD"] + ": " + ack;
            newTransaction.GatewayDebugResponse = HttpUtility.UrlDecode(responseString);
            //newTransaction.Amount = order.Total;

            if (ackType == AckValues.Success || ackType == AckValues.SuccessWithWarning)
            {
                // nothing
            }
            else
            {
                newTransaction.GatewayError = ack + " Invalid / Unknown Response from Payment Provider";
            }
            newTransaction.Save();
            

            return fields;
        }

        // Step 2.5
        /// <summary>
        /// Update our Order info with the info sent back to us from PayPal, based on the user's selections on the PayPal site.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="payPalToken"></param>
        public void UpdateOrderWithExpressCheckoutDetails(Order order, string payPalToken)
        {
            Dictionary<string, string> fields = GetExpressCheckoutDetails(order, payPalToken);

            string ack = fields.TryGetValueOrEmpty("ACK");
            AckValues ackType = WA.Enum<AckValues>.TryParseOrDefault(ack, AckValues.Failure);

            if (ackType == AckValues.Success || ackType == AckValues.SuccessWithWarning)
            {
                // parse response fields into variables
                string firstName = fields.TryGetValueOrEmpty("FIRSTNAME");
                string lastName = fields.TryGetValueOrEmpty("LASTNAME");
                string email = fields.TryGetValueOrEmpty("EMAIL");
                string payPalPayerId = fields.TryGetValueOrEmpty("PAYERID");

                string shipToName = fields.TryGetValueOrEmpty("SHIPTONAME");
                string shipAddress1 = fields.TryGetValueOrEmpty("SHIPTOSTREET");
                string shipAddress2 = fields.TryGetValueOrEmpty("SHIPTOSTREET2");
                string shipCity = fields.TryGetValueOrEmpty("SHIPTOCITY");
                string shipRegion = fields.TryGetValueOrEmpty("SHIPTOSTATE");
                string shipPostalCode = fields.TryGetValueOrEmpty("SHIPTOZIP");
                string shipCountryCode = fields.TryGetValueOrEmpty("SHIPTOCOUNTRYCODE");

                //decimal amount = WA.Parser.ToDecimal(fields.TryGetValueOrEmpty("AMT")).Value;
                decimal shippingAmount = WA.Parser.ToDecimal(fields.TryGetValueOrEmpty("SHIPPINGAMT")).GetValueOrDefault(order.ShippingAmount.GetValueOrDefault(0));
                //decimal handlingAmount = WA.Parser.ToDecimal(fields.TryGetValueOrEmpty("HANDLINGAMT")).GetValueOrDefault(0);
                decimal taxAmount = WA.Parser.ToDecimal(fields.TryGetValueOrEmpty("TAXAMT")).GetValueOrDefault(order.TaxAmount.GetValueOrDefault(0));

                // update the order fields with info from PayPal
                order.CustomerFirstName = firstName;
                order.CustomerLastName = lastName;
                order.CustomerEmail = email;

                order.ShipRecipientName = shipToName;
                order.ShipAddress1 = shipAddress1;
                order.ShipAddress2 = shipAddress2;
                order.ShipCity = shipCity;
                order.ShipRegion = shipRegion;
                order.ShipPostalCode = shipPostalCode;
                order.ShipCountryCode = shipCountryCode;

                order.BillAddress1 = shipAddress1;
                order.BillAddress2 = shipAddress2;
                order.BillCity = shipCity;
                order.BillRegion = shipRegion;
                order.BillPostalCode = shipPostalCode;
                order.BillCountryCode = shipCountryCode;

                order.ShippingAmount = shippingAmount;
                order.TaxAmount = taxAmount;               

                order.Save();
            }         
        }

        // Step 3.
        public PaymentStatusName DoExpressCheckoutPayment(Order order, Dictionary<string,string> payPalVariables)
        {
            string token = payPalVariables["token"];
            string payerId = payPalVariables["PayerID"];

            Dictionary<string, string> vars = new Dictionary<string, string>();

            vars["METHOD"] = "DoExpressCheckoutPayment";
            vars["VERSION"] = "60.0";

            vars["USER"] = ApiUsername;
            vars["PWD"] = ApiPassword;
            vars["SIGNATURE"] = ApiSignature;

            vars["TOKEN"] = token;
            vars["PAYERID"] = payerId;

            IEnumerable<OrderItem> orderitems = order.OrderItemCollectionByOrderId;
            int itemNumber = 0;
            foreach (OrderItem orderItem in orderitems)
            {
                string orderItemAttributes = orderItem.GetProductFieldDataPlainTextDisplayString();

                vars["L_NAME" + itemNumber] = orderItem.Name.Left(127);
                vars["L_DESC" + itemNumber] = (!string.IsNullOrEmpty(orderItemAttributes) ? " (" + orderItemAttributes + ")" : "").Left(127);
                vars["L_AMT" + itemNumber] = orderItem.PriceForSingleItem.ToString("N2");
                vars["L_QTY" + itemNumber] = orderItem.Quantity.Value.ToString("N0");
                vars["L_NUMBER" + itemNumber] = (!string.IsNullOrEmpty(orderItem.Sku) ? " (" + orderItem.Sku + ")" : "");

                itemNumber++;
            }

            vars["PAYMENTACTION"] = "Sale";
            vars["AMT"] = order.Total.Value.ToString("N2");
            vars["CURRENCYCODE"] = order.UpToStoreByStoreId.Currency.Code;

            // Send Http POST to PayPal
            HttpWebResponse webResponse = HttpHelper.HttpPost(ProviderUrl, vars);
            string responseString = HttpHelper.WebResponseToString(webResponse);

            // Decode the response fields from PayPal API
            Dictionary<string, string> fields = HttpHelper.DecodeVarsFromHttpString(responseString);

            // parse response fields into variables
            string ack = fields["ACK"];
            AckValues ackType = WA.Enum<AckValues>.TryParseOrDefault(ack, AckValues.Failure);
            string correlationId = fields.TryGetValueOrEmpty("CORRELATIONID");
            string transactionId = fields.TryGetValueOrEmpty("TOKEN");
            decimal? amount = WA.Parser.ToDecimal(fields.TryGetValueOrEmpty("AMT"));
            decimal? taxAmount = WA.Parser.ToDecimal(fields.TryGetValueOrEmpty("TAXAMT"));
            // TODO - do we need shipping amount too?


            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = ProviderUrl;
            newTransaction.GatewayTransactionId = transactionId;
            newTransaction.GatewayResponse = vars["METHOD"] + ": " + ack;
            newTransaction.GatewayDebugResponse = HttpUtility.UrlDecode(responseString);
            
            PaymentStatusName paymentStatus = order.PaymentStatus;
            
            if (ackType == AckValues.Success || ackType == AckValues.SuccessWithWarning)
            {                                
                bool paymentAmountMatches = (amount.GetValueOrDefault(-1) == order.Total.Value);
                if (paymentAmountMatches)
                {                    
                    paymentStatus = PaymentStatusName.Completed;
                    newTransaction.Amount = amount;
                }
                else
                {
                    newTransaction.GatewayError = string.Format("Payment amount does not match Order Total. Order total {0:N2}. Payment amount {1:N2}", order.Total, amount);
                    if (amount >= order.Total)
                    {
                        paymentStatus = PaymentStatusName.Completed;
                        newTransaction.Amount = amount;
                    }
                    else
                    {
                        paymentStatus = PaymentStatusName.Pending;
                    }
                }
            }
            else
            {                
                newTransaction.GatewayError = ack + " Invalid / Unknown Response from Payment Provider";
                paymentStatus = PaymentStatusName.ProviderError;
            }
            newTransaction.Save();

            return paymentStatus;
        }

        public int? GetOrderIdForTransactionToken(string payPalToken)
        {
            PaymentTransaction paymentTransaction = PaymentTransaction.GetMostRecentByTransactionId(payPalToken);
            if(paymentTransaction != null)
            {
                return paymentTransaction.OrderId;
            }
            return null;
        }

        public override PaymentStatusName ProcessDirectPaymentResponse(Order order, HttpWebResponse response)
        {
            string responseString = HttpHelper.WebResponseToString(response);

            // Decode the response fields from PayPal API
            string[] encodedPairs = responseString.Split('&');
            Dictionary<string, string> fields = new Dictionary<string, string>(encodedPairs.Length);            
            foreach (string field in encodedPairs)
            {
                string[] pair = field.Split('=');
                string name = HttpUtility.UrlDecode(pair[0]);
                string value = HttpUtility.UrlDecode(pair[1]);

                fields[name] = value;
            }

            // parse response fields into variables
            string ack = fields["ACK"];
            AckValues ackType = WA.Enum<AckValues>.TryParseOrDefault(ack, AckValues.Failure);
            int? orderId = WA.Parser.ToInt(fields.TryGetValueOrEmpty("CUSTOM"));
            string transactionId = fields.TryGetValueOrEmpty("TRANSACTIONID");
            decimal? amount = WA.Parser.ToDecimal(fields.TryGetValueOrEmpty("AMT"));

            string error1Code = fields.TryGetValueOrEmpty("L_ERRORCODE0");
            string error1ShortMsg = fields.TryGetValueOrEmpty("L_SHORTMESSAGE0");
            string error1LongMsg = fields.TryGetValueOrEmpty("L_LONGMESSAGE0");
            string error1Severity = fields.TryGetValueOrEmpty("L_SEVERITYCODE0");

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = ProviderUrl;
            newTransaction.GatewayTransactionId = transactionId;
            newTransaction.GatewayDebugResponse = HttpUtility.UrlDecode(responseString);
            newTransaction.Amount = amount;

            PaymentStatusName paymentStatus = order.PaymentStatus;
            
            if (ackType == AckValues.Success || ackType == AckValues.SuccessWithWarning)
            {
                bool paymentAmountMatches = (amount.GetValueOrDefault(-1) == order.Total.Value);
                if (paymentAmountMatches)
                {
                    newTransaction.GatewayResponse = ackType.ToString();
                    paymentStatus = PaymentStatusName.Completed;
                }
                else
                {
                    newTransaction.GatewayResponse = ackType.ToString() + " Amount from Payment Provider does not match the Order amount.";
                    paymentStatus = PaymentStatusName.Pending;
                }

                if(ackType == AckValues.SuccessWithWarning)
                {
                    newTransaction.GatewayError = ackType.ToString() + string.Format(@" ErrorCode: {0}, ShortMsg: {1}, LongMsg: {2}, SeverityCode: {3}", error1Code, error1ShortMsg, error1LongMsg, error1Severity);
                }
            }
            else if (ackType == AckValues.Failure || ackType == AckValues.FailureWithWarning || ackType == AckValues.Warning)
            {                
                newTransaction.GatewayError = ackType.ToString() + string.Format(@" ErrorCode: {0}, ShortMsg: {1}, LongMsg: {2}, SeverityCode: {3}", error1Code, error1ShortMsg, error1LongMsg, error1Severity);                
                paymentStatus = PaymentStatusName.ProviderError;
            }
            else
            {
                newTransaction.GatewayError = ack + " Invalid / Unknown Response from Payment Provider";
                paymentStatus = PaymentStatusName.ProviderError;
            }

            newTransaction.Save();

            return paymentStatus;
        }

        private enum AckValues
        {
            Success,
            SuccessWithWarning,
            Failure,
            FailureWithWarning,
            Warning
        }

        private enum TokenStatus
        {
            PaymentActionFailed,
            PaymentActionInProgress,
            PaymentActionCompleted
        }
    }
}
