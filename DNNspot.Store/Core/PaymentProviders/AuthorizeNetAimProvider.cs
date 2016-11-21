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
    public class AuthorizeNetAimProvider : PaymentProvider
    {
        const char fieldDelimiterChar = '|';
        //const char fieldEncapsulationChar = '"';

        public AuthorizeNetAimProvider(ProviderConfig config)
            : base(config)
        {            
        }

        public string ApiLoginId
        {
            get { return config.Settings.ContainsKey("apiLogin") ? config.Settings["apiLogin"] : ""; }
            set { config.Settings["apiLogin"] = value; }
        }

        public string TransactionKey
        {
            get { return config.Settings.ContainsKey("transactionKey") ? config.Settings["transactionKey"] : ""; }
            set { config.Settings["transactionKey"] = value; }
        }

        public bool IsTestGateway
        {
            get
            {
                if (config.Settings.ContainsKey("isTestGateway"))
                {
                    return WA.Parser.ToBool(config.Settings["isTestGateway"]).GetValueOrDefault(false);
                }
                return false;
            }
            set { config.Settings["isTestGateway"] = value.ToString(); }
        }

        public bool IsTestTransactions
        {
            get
            {
                if (config.Settings.ContainsKey("isTestTransactions"))
                {
                    return WA.Parser.ToBool(config.Settings["isTestTransactions"]).GetValueOrDefault(false);
                }
                return false;
            }
            set { config.Settings["isTestTransactions"] = value.ToString(); }
        }

        private string ProviderUrl
        {
            get { return IsTestGateway ? "https://test.authorize.net/gateway/transact.dll" : "https://secure.authorize.net/gateway/transact.dll"; }
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
            Dictionary<string, string> vars = new Dictionary<string, string>();

            vars["x_login"] = ApiLoginId;
            vars["x_tran_key"] = TransactionKey;
            vars["x_test_request"] = IsTestTransactions ? "TRUE" : "FALSE";
            
            vars["x_version"] = "3.1";                        
            vars["x_relay_response"] = "FALSE";
            vars["x_delim_data"] = "TRUE";
            vars["x_delim_char"] = fieldDelimiterChar.ToString();
            //vars["x_encap_char"] = fieldEncapsulationChar.ToString();
            vars["x_type"] = TransactionType.AUTH_CAPTURE.ToString();

            // Order Info
            //vars["x_invoice_num"] = order.Id.Value.ToString().Left(20);
            vars["x_invoice_num"] = order.OrderNumber.Left(20);
            vars["x_amount"] = order.Total.Value.ToString("N2");

            vars["x_first_name"] = order.CustomerFirstName.Left(50);
            vars["x_last_name"] = order.CustomerLastName.Left(50);
            vars["x_company"] = order.ShipRecipientBusinessName.Left(50);
            vars["x_address"] = order.BillAddress1.Left(60);
            vars["x_city"] = order.BillCity.Left(40);
            vars["x_state"] = order.BillRegion.Left(40);
            vars["x_zip"] = order.BillPostalCode.Left(20);
            vars["x_country"] = order.BillCountryCode.Left(60);
            vars["x_phone"] = order.BillTelephone.Left(25);
            vars["x_email"] = order.CustomerEmail.Left(255);
            vars["x_cust_id"] = order.UserId.HasValue ? order.UserId.Value.ToString().Left(20) : "";
            vars["x_customer_ip"] = order.CreatedByIP.Left(15);

            // TODO - shipping address ??
            // TODO - shipping cost ??

            // Credit Card Info
            vars["x_method"] = "CC";
            vars["x_card_num"] = creditCardInfo.CardNumber.Trim();
            vars["x_exp_date"] = string.Format("{0}{1}", creditCardInfo.ExpireMonth2Digits, creditCardInfo.ExpireYear);
            if (!string.IsNullOrEmpty(creditCardInfo.SecurityCode))
            {
                vars["x_card_code"] = creditCardInfo.SecurityCode.Trim();
            }

            // Custom merchant-defined fields
            vars["originator"] = "DNNspot-Store";
            
            // TODO - Send line items per API ??

            HttpWebResponse webResponse = HttpHelper.HttpPost(ProviderUrl, vars, "application/x-www-form-urlencoded");

            return webResponse;
        }

        public override PaymentStatusName ProcessDirectPaymentResponse(Order order, HttpWebResponse response)
        {
            // returned values are returned as a stream, then read into a string
            string responseString = HttpHelper.WebResponseToString(response);

            // the response string is broken into an array
            // The split character specified here must match the delimiting character specified above
            string[] fields = responseString.Split(fieldDelimiterChar);

            ResponseCode responseCode = WA.Enum<ResponseCode>.TryParseOrDefault(fields[0], ResponseCode.Error);
            string responseSubcode = fields[1];
            int? responseReasonCode = WA.Parser.ToInt(fields[2]);
            string responseReasonText = fields[3];  // could show this to the customer
            string authorizationCode = fields[4];
            string avsResponse = fields[5];
            string transactionId = fields[6];
            int? orderId = WA.Parser.ToInt(fields[7]); // "Invoice Number" in Auth.Net docs
            string description = fields[8];
            decimal? amount = WA.Parser.ToDecimal(fields[9]);
            string method = fields[10];
            string transactionType = fields[11];
            string customerId = fields[12];
            //.... fields omitted here....
            string md5Hash = fields[37];
            string cardCodeResponse = fields[38];   // result of CCV verification
            string cavvResponse = fields[39];   // Cardholder Authentication Verification Response
            // Custom Merchant-Defined Fields (Pg. 36/37 of Auth.Net Docs)
            if(fields.Length >= 69) // custom fields start at position 69
            {
                // grab any add'l merchant-defined fields here...
            }

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = ProviderUrl;
            newTransaction.GatewayTransactionId = transactionId;
            newTransaction.GatewayDebugResponse = responseString;
            newTransaction.Amount = amount;

            PaymentStatusName paymentStatus = order.PaymentStatus;
            
            if (responseCode == ResponseCode.Approved)
            {                
                bool paymentAmountMatches = (amount.GetValueOrDefault(-1) == order.Total.Value);

                if (paymentAmountMatches)
                {
                    newTransaction.GatewayResponse = "Approved";
                    paymentStatus = PaymentStatusName.Completed;
                }
                else
                {
                    newTransaction.GatewayResponse = "Payment amount does not match the order amount.";
                    paymentStatus = PaymentStatusName.Pending;                    
                }
            }
            else if(responseCode == ResponseCode.Declined)
            {
                newTransaction.GatewayError = string.Format("Payment was declined. {1} (Response Reason Code: {0}).", responseReasonCode, responseReasonText);
                paymentStatus = PaymentStatusName.Denied;                
            }
            else if(responseCode == ResponseCode.Error)
            {
                newTransaction.GatewayError = string.Format("{1} (Response Reason Code: {0}).", responseReasonCode, responseReasonText);
                paymentStatus = PaymentStatusName.ProviderError;                             
            }
            else if (responseCode == ResponseCode.HeldForReview)
            {
                newTransaction.GatewayError = string.Format("Payment has been held for review by Authorize.Net. {1} (Response Reason Code: {0}).", responseReasonCode, responseReasonText);
                paymentStatus = PaymentStatusName.Pending;
            }
            else
            {
                newTransaction.GatewayError = "Invalid / Unknown Response from Payment Provider";                
                paymentStatus = PaymentStatusName.ProviderError;
            }

            newTransaction.Save();

            return paymentStatus;
        }

        private enum TransactionType
        {
            AUTH_CAPTURE, AUTH_ONLY, CAPTURE_ONLY, CREDIT, PRIOR_AUTH_CAPTURE, VOID
        }

        private enum ResponseCode : short
        {
            Approved = 1,
            Declined = 2,
            Error = 3,
            HeldForReview = 4
        }                
    }
}
