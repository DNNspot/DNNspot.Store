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
using System.Xml;
using System.Xml.Linq;
using System.Web;
using System.Web.Services;
using DNNspot.Store.DataModel;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using WA.Extensions;

namespace DNNspot.Store
{
    public static class XmlHelper
    {
        public static string ToXml(Order order)
        {
            return ToXml(new List<Order>() { order });
        }

        public static string ToXml(List<Order> orders)
        {
            XElement xml = new XElement("orders");
            foreach(Order o in orders)
            {
                try
                {
                    XElement xOrder =
                        new XElement("order",
                            new XAttribute("orderId", o.Id),
                            new XAttribute("storeId", o.StoreId),
                            new XElement("userId", o.UserId),
                            new XElement("orderNumber", o.OrderNumber),
                            new XElement("orderStatus", o.OrderStatus),
                            new XElement("paymentStatus", o.PaymentStatus),
                            new XElement("firstName", o.CustomerFirstName),
                            new XElement("lastName", o.CustomerLastName),
                            new XElement("email", o.CustomerEmail),
                            new XElement("shippingServiceProvider", o.ShippingServiceProvider),
                            new XElement("shippingServiceOption", o.ShippingServiceOption),
                            new XElement("addresses",
                            new XElement("address",
                                    new XAttribute("type", "billing"),
                                    new XElement("address1", o.BillAddress1),
                                    new XElement("address2", o.BillAddress2),
                                    new XElement("city", o.BillCity),
                                    new XElement("region", o.BillRegion),
                                    new XElement("postalCode", o.BillPostalCode),
                                    new XElement("country", o.BillCountryCode),
                                    new XElement("telephone", o.BillTelephone)
                                ),
                            new XElement("address",
                                    new XAttribute("type", "shipping"),
                                    new XElement("recipientName", o.ShipRecipientName),
                                    new XElement("recipientBusinessName", o.ShipRecipientBusinessName),
                                    new XElement("address1", o.ShipAddress1),
                                    new XElement("address2", o.ShipAddress2),
                                    new XElement("city", o.ShipCity),
                                    new XElement("region", o.ShipRegion),
                                    new XElement("postalCode", o.ShipPostalCode),
                                    new XElement("country", o.ShipCountryCode),
                                    new XElement("telephone", o.ShipTelephone)
                                )
                            ),
                            new XElement("creditcard",
                                    new XAttribute("type", o.CreditCardType),
                                    new XElement("last4digits", o.CreditCardNumberLast4),
                                    new XElement("expiration", o.CreditCardExpiration)
                                )
                        );

                    try
                    {
                        //--- Order Items
                        XElement xOrderItems = new XElement("orderItems");
                        foreach (OrderItem oi in o.OrderItemCollectionByOrderId)
                        {
                            XElement xOrderItem =
                                    new XElement("orderItem",
                                            new XAttribute("productId", oi.ProductId.HasValue ? oi.ProductId.Value.ToString() : string.Empty),
                                            new XElement("name", oi.Name),
                                            new XElement("sku", oi.Sku),
                                            new XElement("quantity", oi.Quantity.GetValueOrDefault(1)),
                                            new XElement("digitalFilename", oi.DigitalFilename ?? string.Empty),
                                            new XElement("digitalFileDisplayName", oi.DigitalFileDisplayName ?? string.Empty),
                                            new XElement("weightTotal", oi.WeightTotal.GetValueOrDefault(0).ToString("N2")),
                                            new XElement("priceTotal", oi.PriceTotal.GetValueOrDefault(0).ToString("N2"))
                                        );
                            try
                            {
                                //--- Order Item Attributes (Product Field Data)
                                XElement xOrderItemAttributes = new XElement("attributes");
                                List<JsonProductFieldData> attributes = oi.GetProductFieldData();
                                foreach (JsonProductFieldData attr in attributes)
                                {
                                    XElement xOrderItemAttribute =
                                            new XElement("attribute",
                                                    new XAttribute("slug", attr.Slug ?? ""),
                                                    new XElement("name", attr.Name),
                                                    new XElement("value", new XCData(attr.ChoiceValues.ToCsv()))
                                                );

                                    xOrderItemAttributes.Add(xOrderItemAttribute);
                                }
                                xOrderItem.Add(xOrderItemAttributes);
                            }
                            catch(Exception ex)
                            {
                                throw new ModuleLoadException("Error generating XML for Order Item Attributes for Order ID: " + o.Id, ex);
                            }
                            xOrderItems.Add(xOrderItem);
                        }
                        xOrder.Add(xOrderItems);
                    }
                    catch(Exception ex)
                    {
                        throw new ModuleLoadException("Error generating XML for Order Items for OrderID: " + o.Id, ex);
                    }

                    try
                    {
                        //---- Order Coupons
                        XElement xCoupons = new XElement("coupons");
                        foreach (OrderCoupon coupon in o.OrderCouponCollectionByOrderId)
                        {
                            xCoupons.Add(
                                    new XElement("coupon",
                                            new XAttribute("code", coupon.CouponCode),
                                            new XElement("discountAmount", coupon.DiscountAmount.GetValueOrDefault(0).ToString("N2"))
                                        )
                                );
                        }
                        xOrder.Add(xCoupons);
                    }
                    catch(Exception ex)
                    {
                        throw new ModuleLoadException("Error generating XML for Order Coupons for Order ID: " + o.Id, ex);
                    }

                    try
                    {
                        //---- Order Totals
                        xOrder.Add(
                                new XElement("subtotal", o.SubTotal.GetValueOrDefault(0).ToString("N2")),
                                new XElement("shippingAmount", o.ShippingAmount.GetValueOrDefault(0).ToString("N2")),
                                new XElement("discountAmount", o.DiscountAmount.GetValueOrDefault(0).ToString("N2")),
                                new XElement("taxAmount", o.TaxAmount.GetValueOrDefault(0).ToString("N2")),
                                new XElement("total", o.Total.GetValueOrDefault(0).ToString("N2")),
                                new XElement("createdByIp", o.CreatedByIP),
                                new XElement("createdOn", o.CreatedOn)
                                );

                        xml.Add(xOrder);
                    }
                    catch(Exception ex)
                    {
                        throw new ModuleLoadException("Error generating XML for Order Totals for Order ID: " + o.Id, ex);
                    }
                }
                catch(Exception ex)
                {
                    throw new ModuleLoadException("Error generating XML for Order ID: " + o.Id, ex);
                }
            }

            return xml.ToString();
        }
    }
}
