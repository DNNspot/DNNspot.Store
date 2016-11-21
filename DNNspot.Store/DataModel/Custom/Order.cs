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

/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/12/2013 3:32:33 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using DNNspot.Store.PaymentProviders;
using DNNspot.Store.Shipping;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;
using WA.Extensions;

namespace DNNspot.Store.DataModel
{
	public partial class Order : esOrder
	{
		public Order()
		{

        }
        const string CreditCardEncryptionPassword = "7075D5ED-C53B-2F77-9439-49993DC13E5E";

        public OrderStatusName OrderStatus
        {
            get { return (OrderStatusName)this.OrderStatusId; }
            set { this.OrderStatusId = (short)value; }
        }

        public PaymentStatusName PaymentStatus
        {
            get { return (PaymentStatusName)this.PaymentStatusId; }
            set { this.PaymentStatusId = (short)value; }
        }

        public bool HasShippingTrackingNumber
        {
            get { return !string.IsNullOrEmpty(this.ShippingServiceTrackingNumber); }
        }

        public List<string> TrackingNumbers
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ShippingServiceTrackingNumber))
                {
                    string[] numbers = this.ShippingServiceTrackingNumber.Split(',');

                    return new List<string>(numbers);
                }
                return new List<string>();
            }
        }

        public bool HasShippingLabel
        {
            get { return ShippingLabels.Count > 0; }
        }

        public List<string> ShippingLabels
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ShippingServiceLabelFile))
                {
                    string[] labels = this.ShippingServiceLabelFile.Split(',');

                    return new List<string>(labels);
                }
                return new List<string>();
            }
        }

        public bool HasShippableItems
        {
            get
            {
                return this.OrderItemCollectionByOrderId.ToList().Exists(x => !x.UpToProductByProductId.IsDigitalDelivery);
            }
        }

        public bool HasNonEmptyShippingAddress()
        {
            if (!string.IsNullOrEmpty(ShipAddress1))
                return true;
            if (!string.IsNullOrEmpty(ShipCity))
                return true;
            if (!string.IsNullOrEmpty(ShipRegion))
                return true;
            if (!string.IsNullOrEmpty(ShipPostalCode))
                return true;
            if (!string.IsNullOrEmpty(ShipCountryCode))
                return true;

            return false;
        }
        public string PaymentSummary
        {
            get { return this.GetPaymentSummary(); }
        }

        public string GetPaymentSummary(bool showFullCreditCard = true)
        {
            string providerName = this.GetPaymentProviderName();
            if (providerName.ToLower() == "paylater")
            {
                PayLaterPaymentProvider payLaterProvider = new PayLaterPaymentProvider(this.UpToStoreByStoreId.GetPaymentProviderConfig(PaymentProviderName.PayLater));
                return string.Format("{0}<br />{1}", payLaterProvider.DisplayText, payLaterProvider.CustomerInstructions.NewlineToBr());
            }
            else if (providerName.ToLower() == "cardcaptureonly" && (this.PaymentStatus != PaymentStatusName.Completed) && showFullCreditCard)
            {
                return string.Format("{0}<br />{1}<br />{2}<br />CVV: {3}<br />{4}", this.CreditCardType, this.DecryptCreditCardNumber(), this.CreditCardExpiration, this.CreditCardSecurityCode, this.CreditCardNameOnCard);
            }
            else
            {
                return string.Format("{0}<br />xxxx-{1}", this.CreditCardType, this.CreditCardNumberLast4);
            }

        }

        public string PaymentSummaryForEmail
        {
            get
            {
                string providerName = this.GetPaymentProviderName();
                if (providerName.ToLower() == "paylater")
                {
                    PayLaterPaymentProvider payLaterProvider = new PayLaterPaymentProvider(this.UpToStoreByStoreId.GetPaymentProviderConfig(PaymentProviderName.PayLater));
                    return string.Format("{0}<br />{1}", payLaterProvider.DisplayText, payLaterProvider.CustomerInstructions.NewlineToBr());
                }
                else
                {
                    return string.Format("{0}<br />xxxx-{1}", this.CreditCardType, this.CreditCardNumberLast4);
                }
            }
        }

        public static Order GetOrder(int orderId)
        {
            Order order = new Order();
            if (order.LoadByPrimaryKey(orderId))
            {
                return order;
            }
            return null;
        }

        public static Order FindOrder(int? storeId, string orderNumber, string customerEmail)
        {
            OrderQuery q = new OrderQuery();
            q.es.Top = 1;
            q.Where(q.OrderNumber == orderNumber.Trim(), q.CustomerEmail == customerEmail.Trim());
            if (storeId.HasValue)
            {
                q.Where(q.StoreId == storeId);
            }

            Order order = new Order();
            if (order.Load(q))
            {
                return order;
            }
            return null;
        }

        public static Order GetOrderByOrderNumber(int? storeId, string orderNumber)
        {
            OrderQuery q = new OrderQuery();
            q.es.Top = 1;
            q.Where(q.OrderNumber == orderNumber.Trim());
            if (storeId.HasValue)
            {
                q.Where(q.StoreId == storeId);
            }

            Order order = new Order();
            if (order.Load(q))
            {
                return order;
            }
            return null;
        }

        public static Order GetOrderByCartId(Guid cartId)
        {
            OrderQuery q = new OrderQuery();
            q.es.Top = 1;
            q.Where(q.CreatedFromCartId == cartId);

            Order order = new Order();
            if (order.Load(q))
            {
                return order;
            }
            return null;
        }

        /// <summary>
        /// Get the Product entities associated with this order's order items.
        /// If products were deleted since the order was placed, this product count
        /// will not match the order item count.
        /// </summary>
        /// <returns></returns>
        public List<Product> GetOrderItemProducts()
        {
            List<OrderItem> orderItems = this.OrderItemCollectionByOrderId.ToList();
            List<int> productIds = orderItems.ConvertAll(oi => oi.ProductId.Value);

            ProductQuery q = new ProductQuery();
            q.Where(q.Id.In(productIds.ToArray()));

            ProductCollection collection = new ProductCollection();
            collection.Load(q);

            return collection.ToList();
        }

        public List<IShipmentPackageDetail> GetOrderItemsAsShipmentPackages(ShipmentPackagingStrategy packagingStrategy)
        {
            var orderItems = this.OrderItemCollectionByOrderId;

            if (packagingStrategy == ShipmentPackagingStrategy.SingleBox)
            {
                decimal maxWeight = orderItems.Max(x => x.WeightTotal.GetValueOrDefault(1));
                decimal maxLength = orderItems.Max(x => x.Length.GetValueOrDefault(1));
                decimal maxWidth = orderItems.Max(x => x.Width.GetValueOrDefault(1));
                decimal maxHeight = orderItems.Max(x => x.Height.GetValueOrDefault(1));

                return new List<IShipmentPackageDetail>()
                {
                    new ShipmentPackageDetail()
                    {
                        Weight = maxWeight,
                        Length = maxLength,
                        Width = maxWidth,
                        Height = maxHeight,
                        ReferenceCode = this.OrderNumber
                    }
                };
            }
            else if (packagingStrategy == ShipmentPackagingStrategy.BoxPerProductType)
            {
                var shipmentPackages = new List<IShipmentPackageDetail>();
                foreach (var item in orderItems)
                {
                    shipmentPackages.Add(new ShipmentPackageDetail()
                    {
                        Weight = item.WeightTotal.GetValueOrDefault(1),
                        Length = item.Length.GetValueOrDefault(1),
                        Width = item.Width.GetValueOrDefault(1),
                        Height = item.Height.GetValueOrDefault(1),
                        ReferenceCode = string.Format(@"{0}-{1}", this.OrderNumber, item.Sku)
                    });
                }
                return shipmentPackages;
            }
            else if (packagingStrategy == ShipmentPackagingStrategy.BoxPerItem)
            {
                var shipmentPackages = new List<IShipmentPackageDetail>();
                int itemNum = 1;
                foreach (var item in orderItems)
                {
                    decimal itemWeight = item.WeightTotal.GetValueOrDefault(0) / item.Quantity.GetValueOrDefault(1);
                    int qtyIndex = 1;
                    while (qtyIndex <= item.Quantity)
                    {
                        shipmentPackages.Add(new ShipmentPackageDetail()
                        {
                            Weight = itemWeight,
                            Length = item.Length.GetValueOrDefault(1),
                            Width = item.Width.GetValueOrDefault(1),
                            Height = item.Height.GetValueOrDefault(1),
                            ReferenceCode = string.Format(@"{0}-{1}-{2}", this.OrderNumber, item.Sku, qtyIndex)
                        });
                        qtyIndex++;
                    }
                    itemNum++;
                }
                return shipmentPackages;
            }
            else
            {
                throw new ArgumentException("Unknown ShipmentPackagingStrategy", "packagingStrategy");
            }
        }

        public string GetPaymentProviderName()
        {
            List<string> namesFromTransactionHistory = GetPaymentProviderNamesFromTransactionHistory();
            if (namesFromTransactionHistory.Count > 0)
            {
                return namesFromTransactionHistory.ToDelimitedString(", ");
            }
            return "None";
        }

        public List<string> GetPaymentProviderNamesFromTransactionHistory()
        {
            List<PaymentTransaction> transactions = GetPaymentTransactionsMostRecentFirst();
            transactions.RemoveDuplicates((left, right) => left.PaymentProviderId.Value.CompareTo(right.PaymentProviderId.Value));

            List<string> distinctProviders = transactions.ConvertAll(t => t.UpToPaymentProviderByPaymentProviderId.Name);

            return distinctProviders;
        }

        public PaymentTransaction GetMostRecentPaymentTransaction()
        {
            List<PaymentTransaction> transactions = GetPaymentTransactionsMostRecentFirst();
            if (transactions.Count > 0)
            {
                return transactions[0];
            }
            return null;
        }

        public List<PaymentTransaction> GetPaymentTransactionsMostRecentFirst()
        {
            //this.PaymentTransactionCollectionByOrderId.Filter = PaymentTransactionMetadata.ColumnNames.CreatedOn + " DESC";

            return this.PaymentTransactionCollectionByOrderId.AsQueryable().OrderByDescending(x => x.CreatedOn).ToList();
        }

        public List<PaymentTransaction> GetPaymentTransactionsOldestFirst()
        {
            //this.PaymentTransactionCollectionByOrderId.Sort = PaymentTransactionMetadata.ColumnNames.CreatedOn + " ASC";

            return this.PaymentTransactionCollectionByOrderId.AsQueryable().OrderBy(x => x.CreatedOn).ToList();
        }

        public void EncryptCreditCardNumber(string plainTextCreditCardNumber)
        {
            this.CreditCardNumberEncrypted = CryptoHelper.EncryptString(plainTextCreditCardNumber, CreditCardEncryptionPassword);
        }

        public string DecryptCreditCardNumber()
        {
            return CryptoHelper.DecryptString(this.CreditCardNumberEncrypted, CreditCardEncryptionPassword);
        }

        public List<OrderCoupon> GetCoupons()
        {
            return this.OrderCouponCollectionByOrderId.ToList();
        }

        internal void MarkAsPaid()
        {
            Store store = this.UpToStoreByStoreId;

            if (this.GetPaymentProviderName().ToLower() == "paylater")
            {
                PayLaterPaymentProvider payLaterProvider = new PayLaterPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.PayLater));
                payLaterProvider.MarkOrderAsPaid(this.Id.Value);
            }
            else
            {
                CardCaptureOnlyPaymentProvider cardCaptureOnlyPaymentProvider = new CardCaptureOnlyPaymentProvider(store.GetPaymentProviderConfig(PaymentProviderName.CardCaptureOnly));
                cardCaptureOnlyPaymentProvider.MarkOrderAsPaid(this.Id.Value);
            }

            PostCheckoutController.AddUserToDnnRoles(this);
        }

        internal ShippingLogCollection GetShippingLogEntries()
        {
            ShippingLogCollection log = new ShippingLogCollection();
            ShippingLogQuery q = new ShippingLogQuery();
            q.Where(q.CartId == this.CreatedFromCartId);

            log.Load(q);

            return log;
        }
	}
}
