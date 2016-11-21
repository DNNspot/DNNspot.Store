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
Date Generated       : 4/12/2013 3:32:32 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DNNspot.Store.Shipping;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class Cart : esCart
	{
		public Cart()
		{
		
		}
        public int ItemCount
        {
            get { return this.CartItemCollectionByCartId.Count; }
        }

        public List<vCartItemProductInfo> GetCartItemsWithProductInfo()
        {
            if (this.Id.HasValue)
            {
                vCartItemProductInfoQuery q = new vCartItemProductInfoQuery();
                q.Where(q.CartId == this.Id.Value);
                q.OrderBy(q.CreatedOn.Ascending);

                vCartItemProductInfoCollection collection = new vCartItemProductInfoCollection();
                collection.Load(q);

                return collection.ToList();
            }
            return new List<vCartItemProductInfo>();
        }

        public List<IShipmentPackageDetail> GetCartItemsAsShipmentPackages(ShipmentPackagingStrategy packagingStrategy)
        {
            var cartContents = GetCartItemsWithProductInfo().Where(p => p.DeliveryMethod == ProductDeliveryMethod.Shipped);
            if (cartContents.Count() == 0)
            {
                return new List<IShipmentPackageDetail>();
            }

            if (packagingStrategy == ShipmentPackagingStrategy.SingleBox)
            {
                var cartProducts = cartContents.Select(x => x.GetProduct());

                decimal totalWeight = cartContents.Sum(x => x.GetWeightForQuantity());
                decimal maxLength = cartProducts.Max(x => x.Length.GetValueOrDefault(1));
                decimal maxWidth = cartProducts.Max(x => x.Width.GetValueOrDefault(1));
                decimal maxHeight = cartProducts.Max(x => x.Height.GetValueOrDefault(1));
                decimal additionalHandlingFee = cartProducts.Sum(x => x.ShippingAdditionalFeePerItem).GetValueOrDefault(0);


                return new List<IShipmentPackageDetail>()
                {
                               
                    new ShipmentPackageDetail()
                    {
                        Weight = totalWeight,
                        Length = maxLength,
                        Width = maxWidth,
                        Height = maxHeight,  
                        AdditionalHandlingFee = additionalHandlingFee
                    }
                };
            }
            else if (packagingStrategy == ShipmentPackagingStrategy.BoxPerProductType)
            {
                var cartProducts = cartContents.Select(x => x.GetProduct());
                return (from item in cartContents
                        let product = item.GetProduct()
                        select new ShipmentPackageDetail()
                                   {
                                       Weight = item.GetWeightForQuantity(), Length = product.Length.GetValueOrDefault(1), Width = product.Width.GetValueOrDefault(1), Height = product.Height.GetValueOrDefault(1), AdditionalHandlingFee = product.ShippingAdditionalFeePerItem.GetValueOrDefault(0)*item.Quantity.GetValueOrDefault(1)
                                   }).Cast<IShipmentPackageDetail>().ToList();
            }
            else if (packagingStrategy == ShipmentPackagingStrategy.BoxPerItem)
            {
                var shipmentPackages = new List<IShipmentPackageDetail>();
                foreach (var item in cartContents)
                {
                    var product = item.GetProduct();
                    int qtyIndex = 1;
                    while (qtyIndex <= item.Quantity)
                    {
                        shipmentPackages.Add(new ShipmentPackageDetail()
                        {
                            Weight = product.Weight.GetValueOrDefault(1),
                            Length = product.Length.GetValueOrDefault(1),
                            Width = product.Width.GetValueOrDefault(1),
                            Height = product.Height.GetValueOrDefault(1),
                            AdditionalHandlingFee = product.ShippingAdditionalFeePerItem.GetValueOrDefault(0)
                        });
                        qtyIndex++;
                    }
                }
                return shipmentPackages;
            }
            else
            {
                throw new ArgumentException("Unknown ShipmentPackagingStrategy", "packagingStrategy");
            }
        }

        public IList<Product> GetCartProducts()
        {
            List<int> productIds = ((List<CartItem>)this.CartItemCollectionByCartId.ToList()).ConvertAll(ci => ci.ProductId.Value).ToList();

            return ProductCollection.GetProductsByIds(productIds.ToArray());
        }

        public static Cart GetCart(Guid cartId)
        {
            Cart c = new Cart();
            c.LoadByPrimaryKey(cartId);

            return c;
        }

        //public List<Coupon> GetAppliedCoupons()
        //{
        //    List<Coupon> coupons = new List<Coupon>();
        //    foreach(CartCoupon cc in this.CartCouponCollectionByCartId)
        //    {
        //        coupons.Add(cc.UpToCouponByCouponId);
        //    }
        //    return coupons;
        //}

        public decimal GetTotal()
        {
            SessionKeyHelper sessionKeyHelper = new SessionKeyHelper(this.StoreId.GetValueOrDefault(-1));

            CheckoutOrderInfo checkoutOrderInfo = HttpContext.Current.Session[sessionKeyHelper.CheckoutOrderInfo] as CheckoutOrderInfo;
            if (checkoutOrderInfo != null)
            {
                return checkoutOrderInfo.Total;
            }
            else
            {
                checkoutOrderInfo = new CheckoutOrderInfo() { Cart = this };
                return checkoutOrderInfo.Total;
            }
        }

        public override void Save()
        {
            if (!es.IsDeleted)
            {
                if (es.IsAdded)
                {
                    this.CreatedByIP = HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    this.ModifiedOn = DateTime.Now;
                    this.ModifiedByIP = HttpContext.Current.Request.UserHostAddress;
                }
            }
            base.Save();
        }

	}
}
