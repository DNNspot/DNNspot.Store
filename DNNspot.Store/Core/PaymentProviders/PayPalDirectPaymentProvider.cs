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
    public class PayPalDirectPaymentProvider : PaymentProviders.PaymentProvider
    {
        public PayPalDirectPaymentProvider(ProviderConfig config)
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
            Dictionary<string,string> vars = new Dictionary<string, string>();
            
            vars["METHOD"] = "DoDirectPayment";
            vars["VERSION"] = "60.0";

            vars["USER"] = ApiUsername;
            vars["PWD"] = ApiPassword;
            vars["SIGNATURE"] = ApiSignature;

            // Add request-specific fields to the request.
            vars["PAYMENTACTION"] = "Sale";
            vars["IPADDRESS"] = order.CreatedByIP;

            vars["AMT"] = order.Total.Value.ToString("N2");

            //vars["CURRENCYCODE"] = "USD";
            //vars["CURRENCYCODE"] = "GBP"; // British Pounds
            //vars["CURRENCYCODE"] = "AUD"; // Australian Dollars
            vars["CURRENCYCODE"] = order.UpToStoreByStoreId.Currency.Code;

            vars["CREDITCARDTYPE"] = creditCardInfo.CardType.ToString().Left(10);
            vars["ACCT"] = creditCardInfo.CardNumber;
            vars["EXPDATE"] = string.Format("{0}{1}", creditCardInfo.ExpireMonth2Digits, creditCardInfo.ExpireYear);
            vars["CVV2"] = creditCardInfo.SecurityCode.ToString();

            //vars["BUSINESS"] = "";
            vars["FIRSTNAME"] = order.CustomerFirstName.Left(25);
            vars["LASTNAME"] = order.CustomerLastName.Left(25);
            vars["STREET"] = order.BillAddress1.Left(100);
            vars["STREET2"] = order.BillAddress2.Left(100);
            vars["CITY"] = order.BillCity.Left(40);
            vars["STATE"] = order.BillRegion.Left(40);
            vars["ZIP"] = order.BillPostalCode.Left(20);
            vars["COUNTRYCODE"] = order.BillCountryCode.Left(2).ToUpper();
            //vars["EMAIL"] = order.CustomerEmail.Left(127);    // email was causing problems if it happens to be an actual PayPal account!!
            vars["PHONENUM"] = order.BillTelephone.Left(20);

            //vars["CUSTOM"] = order.Id.Value.ToString().Left(256);
            //vars["INVNUM"] = order.Id.Value.ToString().Left(127);
            vars["CUSTOM"] = order.OrderNumber.Left(256);
            vars["INVNUM"] = order.OrderNumber.Left(127);

            // TODO - individual line items.. ??

            HttpWebResponse webResponse = HttpHelper.HttpPost(ProviderUrl, vars);

            return webResponse;
        }

        public override PaymentStatusName ProcessDirectPaymentResponse(Order order, HttpWebResponse response)
        {
            // Decode the response fields from PayPal API            
            string responseString = HttpHelper.WebResponseToString(response);
            Dictionary<string, string> fields = HttpHelper.DecodeVarsFromHttpString(responseString);

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
    }
}
