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
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DotNetNuke.Services.Exceptions;
using WA.Extensions;

namespace DNNspot.Store
{
    public class TokenHelper
    {
        const string orderItemsTokenDelim = "|";
        readonly StoreContext storeContext;
        readonly StoreUrls storeUrls;

        public TokenHelper(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            storeUrls = new StoreUrls(storeContext);
        }

        internal Dictionary<string, string> GetOrderTokens(Order order, bool isEmail)
        {
            //StoreContext fakeContext = new StoreContext(HttpContext.Current.Request);
            //StoreUrls storeUrls = new StoreUrls(fakeContext);

            Dictionary<string, string> tokens = new Dictionary<string, string>();

            DataModel.Store store = order.UpToStoreByStoreId;

            tokens["store.name"] = store.Name;
            string customerServiceEmail = store.GetSetting(StoreSettingNames.CustomerServiceEmailAddress);
            if (!string.IsNullOrEmpty(customerServiceEmail))
            {
                tokens["store.contactemail"] = customerServiceEmail;
            }

            tokens["customer.userid"] = order.UserId.HasValue ? order.UserId.Value.ToString() : "anonymous";

            tokens["customer.firstname"] = order.CustomerFirstName;
            tokens["customer.lastname"] = order.CustomerLastName;
            tokens["customer.email"] = order.CustomerEmail;

            tokens["order.number"] = order.OrderNumber;
            tokens["order.status"] = order.OrderStatus.ToString();
            tokens["order.payment.status"] = order.PaymentStatus.ToString();
            tokens["order.payment.summary"] = isEmail ? order.PaymentSummaryForEmail : order.PaymentSummary;

            tokens["order.date"] = order.CreatedOn.Value.ToString();
            tokens["order.date.monthname"] = order.CreatedOn.Value.ToString("MMMM");
            tokens["order.date.day"] = order.CreatedOn.Value.Day.ToString();
            tokens["order.date.year"] = order.CreatedOn.Value.Year.ToString();

            tokens["order.subtotal"] = storeContext.CurrentStore.FormatCurrency(order.SubTotal.GetValueOrDefault(0));
            tokens["order.shippingcost"] = storeContext.CurrentStore.FormatCurrency(order.ShippingAmount.GetValueOrDefault(0));
            tokens["order.discount"] = storeContext.CurrentStore.FormatCurrency(order.DiscountAmount.GetValueOrDefault(0));
            tokens["order.couponcodes"] = order.GetCoupons().ToDelimitedString(", ", t => t.CouponCode);
            tokens["order.tax"] = storeContext.CurrentStore.FormatCurrency(order.TaxAmount.GetValueOrDefault(0));
            tokens["order.total"] = storeContext.CurrentStore.FormatCurrency(order.Total.GetValueOrDefault(0));

            tokens["order.billing.address1"] = order.BillAddress1;
            tokens["order.billing.address2"] = order.BillAddress2;
            tokens["order.billing.city"] = order.BillCity;
            tokens["order.billing.region"] = order.BillRegion;
            tokens["order.billing.postalcode"] = order.BillPostalCode;
            tokens["order.billing.countrycode"] = order.BillCountryCode;
            tokens["order.billing.telephone"] = order.BillTelephone;

            tokens["order.billing.creditcardtype"] = order.CreditCardType;
            tokens["order.billing.creditcardlast4"] = order.CreditCardNumberLast4;
            tokens["order.billing.creditcardexpiration"] = order.CreditCardExpiration;

            tokens["order.shipping.recipientname"] = order.ShipRecipientName + (!string.IsNullOrEmpty(order.ShipRecipientBusinessName) ? "<br />" + order.ShipRecipientBusinessName : "");
            tokens["order.shipping.address1"] = order.ShipAddress1;
            tokens["order.shipping.address2"] = order.ShipAddress2;
            tokens["order.shipping.city"] = order.ShipCity;
            tokens["order.shipping.region"] = order.ShipRegion;
            tokens["order.shipping.postalcode"] = order.ShipPostalCode;
            tokens["order.shipping.countrycode"] = order.ShipCountryCode;
            tokens["order.shipping.telephone"] = order.ShipTelephone;

            tokens["order.shipping.option"] = order.ShippingServiceOption;
            tokens["order.shipping.cost"] = order.ShippingAmount.GetValueOrDefault(0).ToString("C2");
            tokens["order.shipping.trackingnumber"] = order.TrackingNumbers.ToDelimitedString(", ");

            tokens["order.ordernotes"] = order.OrderNotes;

            List<OrderItem> orderItems = order.OrderItemCollectionByOrderId.ToList();
            if (orderItems.Count > 0)
            {
                tokens["order.itemsdelim"] = GetOrderItemsDelimitedString(orderItems);
                tokens["order.itemstable"] = GetOrderItemsTable(order);
                tokens["order.itemstablenoprice"] = GetOrderItemsTable(order, false);
                try
                {
                    tokens["order.itemsjson"] = GetOrderItemsJson(orderItems);
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                    if (tokens.ContainsKey("order.itemsjson"))
                    {
                        tokens.Remove("order.itemsjson");
                    }
                }
            }

            tokens["order.xml"] = XmlHelper.ToXml(order);

            return tokens;
        }

        private string GetOrderItemsDelimitedString(IEnumerable<OrderItem> orderItems)
        {
            List<string> itemStrings = new List<string>();

            foreach (OrderItem item in orderItems)
            {
                string itemName = item.Name;
                if (item.HasDigitalFile)
                {
                    itemName = string.Format(@"<a href=""{0}"">{1}</a>", storeUrls.ProductFile(item.DigitalFilename), item.Name);
                }

                string skuString = !string.IsNullOrEmpty(item.Sku) ? string.Format("#{0}", item.Sku) : "";

                StringBuilder sbFieldData = new StringBuilder();
                List<JsonProductFieldData> fieldData = item.GetProductFieldData();
                if (fieldData.Count > 0)
                {
                    foreach (JsonProductFieldData field in fieldData)
                    {
                        sbFieldData.AppendFormat(@"{0}: {1}, ", field.Name, field.ChoiceValues.ToCsv());
                    }
                }
                string fieldDataString = sbFieldData.ToString().TrimEnd(", ");

                itemStrings.Add(string.Format("{0} {1}{4}, Qty = {2:N0}, Total = {3}", itemName, skuString, item.Quantity, storeContext.CurrentStore.FormatCurrency(item.PriceTotal), fieldDataString.Length > 0 ? " " + fieldDataString : ""));
            }

            return itemStrings.ToDelimitedString(orderItemsTokenDelim);
        }

        private string GetOrderItemsTable(Order order, bool includePrice = true)
        {
            List<OrderItem> orderItems = order.OrderItemCollectionByOrderId.ToList();
            if (orderItems.Count > 0)
            {
                StringBuilder itemsTable = new StringBuilder();
                itemsTable.Append(@"<table cellspacing=""0"" cellpadding=""0"" border=""0"" class=""orderItems box"" width=""100%"" style=""width: 100%;"">
                                <thead>
                                    <tr>
                                        <th align=""left"">Item</th> <th align=""left"">Sku</th> <th align=""right"">Qty.</th> ");
                if (includePrice)
                {
                    itemsTable.Append(@"<th align=""right"">Total</th>");
                }
                itemsTable.Append(@"</tr>
                                </thead>
                                <tbody>");

                foreach (OrderItem item in orderItems)
                {
                    string itemName = item.Name;
                    if (item.HasDigitalFile)
                    {
                        itemName = string.Format(@"<a href=""{0}"">{1}</a>", storeUrls.ProductFile(item.DigitalFilename), item.Name);
                    }

                    string skuString = !string.IsNullOrEmpty(item.Sku) ? string.Format("#{0}", item.Sku) : "";

                    StringBuilder sbFieldData = new StringBuilder();
                    List<JsonProductFieldData> fieldData = item.GetProductFieldData();
                    if (fieldData.Count > 0)
                    {
                        foreach (JsonProductFieldData field in fieldData)
                        {
                            sbFieldData.AppendFormat(@"{0}: {1}, ", field.Name, field.ChoiceValues.ToCsv());
                        }
                    }
                    string fieldDataString = sbFieldData.ToString().TrimEnd(", ");
                    itemsTable.AppendFormat(@"<tr> <td>{0}{1}</td> <td>{2}</td> <td align=""right"">{3:N0}</td></tr>", itemName, fieldDataString.Length > 0 ? string.Format(@"<br /><span class=""productFieldData"">{0}</span>", fieldDataString) : "", skuString, item.Quantity);

                    if (includePrice)
                    {
                        itemsTable.AppendFormat(@" <td align=""right"">{0}</td> ", storeContext.CurrentStore.FormatCurrency(item.PriceTotal));
                    }
                }
                itemsTable.Append("</tbody>");

                if (includePrice)
                {
                    // table - summary lines
                    itemsTable.Append("<tfoot>");
                    itemsTable.AppendFormat(@"<tr> <td colspan=""3"" align=""right"" style=""text-align: right;"">Subtotal</td> <td align=""right"" style=""text-align: right;"">{0}</td> </tr>", storeContext.CurrentStore.FormatCurrency(order.SubTotal));
                    itemsTable.AppendFormat(@"<tr> <td colspan=""3"" align=""right"" style=""text-align: right;"">Shipping & Handling</td> <td align=""right"" style=""text-align: right;"">{0}</td> </tr>", storeContext.CurrentStore.FormatCurrency(order.ShippingAmount));
                    if (order.DiscountAmount > 0)
                    {
                        itemsTable.AppendFormat(@"<tr> <td colspan=""3"" align=""right"" style=""text-align: right;"">Discount</td> <td align=""right"" style=""text-align: right;"">({0})</td> </tr>", storeContext.CurrentStore.FormatCurrency(order.DiscountAmount));
                    }
                    itemsTable.AppendFormat(@"<tr> <td colspan=""3"" align=""right"" style=""text-align: right;"">Tax</td> <td align=""right"" style=""text-align: right;"">{0}</td> </tr>", storeContext.CurrentStore.FormatCurrency(order.TaxAmount));
                    itemsTable.AppendFormat(@"<tr> <td colspan=""3"" align=""right"" style=""text-align: right;""><strong>Total</strong></td> <td align=""right"" style=""text-align: right;""><strong>{0}</strong></td> </tr>", storeContext.CurrentStore.FormatCurrency(order.Total));
                    itemsTable.Append(@"</tfoot>");
                }

                itemsTable.Append("</table>");

                return itemsTable.ToString();
            }
            return "";
        }

        private static string GetOrderItemsJson(List<OrderItem> orderItems)
        {
            List<JsonOrderItem> jsonOrderItems = orderItems.ConvertAll(oi =>
                    new JsonOrderItem()
                        {
                            Id = oi.Id.Value,
                            OrderId = oi.OrderId.Value,
                            ProductId = oi.ProductId,
                            Name = oi.Name,
                            Sku = oi.Sku,
                            Quantity = oi.Quantity.Value,
                            ProductFieldData = oi.GetProductFieldData(),
                            DigitalFileDisplayName = oi.DigitalFileDisplayName,
                            DigitalFilename = oi.DigitalFilename,
                            WeightTotal = oi.WeightTotal.Value,
                            PriceTotal = oi.PriceTotal.Value
                        }
                );

            return Newtonsoft.Json.JsonConvert.SerializeObject(jsonOrderItems);
        }
    }
}
