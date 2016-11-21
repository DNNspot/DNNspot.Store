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
using System.Net;
using System.Web;
using DNNspot.Store.DataModel;
using WA.Extensions;
using FluentValidation.Results;

namespace DNNspot.Store.PaymentProviders
{
    public class CardCaptureOnlyPaymentProvider: PaymentProvider
    {
        public CardCaptureOnlyPaymentProvider(ProviderConfig config)
            : base(config)
        {
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
            order.EncryptCreditCardNumber(creditCardInfo.CardNumber);
            order.CreditCardSecurityCode = creditCardInfo.SecurityCode;
            order.PaymentStatus = PaymentStatusName.Pending;

            order.Save();

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = "";
            newTransaction.GatewayTransactionId = "";
            newTransaction.GatewayResponse = "Payment Pending";
            newTransaction.GatewayDebugResponse = "";

            if (order.Total > 0)
            {
                // check for valid Credit Card
                CreditCardInfoValidator validator = new CreditCardInfoValidator();
                ValidationResult results = validator.Validate(creditCardInfo);
                if (!results.IsValid && results.Errors.Count > 0)
                {
                    newTransaction.GatewayResponse = "Credit Card Validation Error";
                    newTransaction.GatewayError = results.Errors.ToList().ConvertAll(e => e.ErrorMessage).ToDelimitedString(", ");
                }
            }

            newTransaction.Save();

            return null;
        }

        public override PaymentStatusName ProcessDirectPaymentResponse(Order order, HttpWebResponse response)
        {
            return order.PaymentStatus;          
        }

        public void MarkOrderAsPaid(int orderId)
        {
            Order order = Order.GetOrder(orderId);

            OrderController orderController = new OrderController(new StoreContext(HttpContext.Current.Request, order.StoreId.Value));
            orderController.UpdateOrderStatus(order, order.OrderStatus, PaymentStatusName.Completed);

            //--- Delete the stored Credit Card number, even though it's encrypted we don't need the full # anymore
            order.CreditCardNumberEncrypted = string.Empty;
            order.CreditCardExpiration = string.Empty;
            order.CreditCardSecurityCode = string.Empty;
            order.Save();

            PaymentTransaction newTransaction = new PaymentTransaction();
            newTransaction.OrderId = order.Id;
            newTransaction.PaymentProviderId = this.ProviderId;
            newTransaction.GatewayUrl = "";
            newTransaction.GatewayTransactionId = "";
            newTransaction.GatewayResponse = "Marked as Paid";
            newTransaction.GatewayDebugResponse = "";
            newTransaction.Amount = order.Total;
            newTransaction.Save();
        }
    }
}
