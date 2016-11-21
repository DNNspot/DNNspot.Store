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
using System.Web;
using DNNspot.Store.DataModel;
using WA.Extensions;

namespace DNNspot.Store
{
    public class CouponController
    {
        public static decimal CalculateDiscountAmount(Coupon coupon, CheckoutOrderInfo checkoutOrderInfo)
        {
            // we're assuming it's a valid coupon when this is called, maybe not a good idea??

            decimal discountAmount = 0.0m;

            CouponDiscountType discountType = coupon.DiscountTypeName;

            if(discountType == CouponDiscountType.SubTotal)
            {
                if(coupon.IsAmountOff)
                {
                    discountAmount = coupon.AmountOff.Value;
                }
                else if(coupon.IsPercentOff)
                {
                    discountAmount = coupon.PercentOffDecimal * checkoutOrderInfo.SubTotal;
                }
            }
            else if (discountType == CouponDiscountType.Product)
            {
                List<int> couponProductIds = coupon.GetProductIds().ConvertAll(s => Convert.ToInt32(s));
                List<vCartItemProductInfo> cartProductInfos = checkoutOrderInfo.Cart.GetCartItemsWithProductInfo();
                List<int> cartProductIds = cartProductInfos.ConvertAll(p => p.ProductId.Value);
                
                List<int> intersectedProductIds = cartProductIds.Intersect(couponProductIds).ToList();
                foreach(int productIdToDiscount in intersectedProductIds)
                {
                    vCartItemProductInfo cartItem = cartProductInfos.Find(pi => pi.ProductId.Value == productIdToDiscount);
                    if(cartItem != null)
                    {
                        if(coupon.IsAmountOff)
                        {
                            discountAmount += (coupon.AmountOff.Value * cartItem.Quantity.Value);
                        }
                        else if(coupon.IsPercentOff)
                        {
                            discountAmount += (coupon.PercentOffDecimal * cartItem.GetPriceForQuantity());
                        }
                    }
                }
            }
            else if (discountType == CouponDiscountType.Shipping)
            {
                if (coupon.IsAmountOff)
                {
                    discountAmount = coupon.AmountOff.Value;
                }
                else if (coupon.IsPercentOff)
                {
                    discountAmount = coupon.PercentOffDecimal * checkoutOrderInfo.ShippingRate.Rate;
                }
            }
            else if (discountType == CouponDiscountType.SubTotalAndShipping)
            {
                if (coupon.IsAmountOff)
                {
                    discountAmount = coupon.AmountOff.Value;
                }
                else if (coupon.IsPercentOff)
                {
                    discountAmount = coupon.PercentOffDecimal * (checkoutOrderInfo.SubTotal + checkoutOrderInfo.ShippingRate.Rate);
                }                
            }
            else
            {
                throw new NotImplementedException(string.Format(@"""{0}"" is an unknown CouponDiscountType", discountType));
            }

            //--- check we didn't exceed the "Max Discount Amount Per Order"
            if(coupon.MaxDiscountAmountPerOrder.HasValue && (discountAmount > coupon.MaxDiscountAmountPerOrder.Value))
            {
                discountAmount = coupon.MaxDiscountAmountPerOrder.Value;
            }

            return discountAmount.RoundForMoney();
        }

        public static bool IsCouponValidForCheckout(Coupon coupon, CheckoutOrderInfo checkoutOrderInfo, out CouponStatus couponStatus)
        {
            couponStatus = CouponStatus.NotFound;

            //Coupon coupon = Coupon.GetCoupon(couponCode, storeId);
            if (coupon != null)
            {
                //--- "Active" status?
                if (!coupon.IsActive.Value)
                {
                    couponStatus = CouponStatus.NotActive;
                    return false;
                }

                //--- Active Dates?
                if (coupon.ValidFromDate.HasValue)
                {
                    if (DateTime.Today < coupon.ValidFromDate.Value)
                    {
                        couponStatus = CouponStatus.ActiveDateInvalidFrom;
                        return false;
                    }
                }
                if (coupon.ValidToDate.HasValue)
                {
                    if (DateTime.Today > coupon.ValidToDate.Value)
                    {
                        couponStatus = CouponStatus.ActiveDateInvalidTo;
                        return false;
                    }
                }

                List<CheckoutCouponInfo> checkoutCouponInfos = checkoutOrderInfo.GetAppliedCoupons();
                //--- already been applied?
                if (checkoutCouponInfos.Exists(c => c.CouponCode == coupon.Code))
                {
                    couponStatus = CouponStatus.AlreadyApplied;
                    return false;
                }
                //--- combinable?
                if (!coupon.IsCombinable.Value && checkoutCouponInfos.Count > 0)
                {
                    // this coupon is not combinable and user is trying to combine it
                    couponStatus = CouponStatus.NotCombinable;
                    return false;
                }
                if(checkoutCouponInfos.Count > 1 && checkoutCouponInfos.Exists(cc => cc.IsCombinable == false))
                {
                    // there's multiple coupons applied but at least 1 of them is NOT combinable
                    couponStatus = CouponStatus.NonCombinableCouponAlreadyInUse;
                    return false;
                }

                CouponDiscountType discountType = coupon.DiscountTypeName;
                //--- Applies to products?
                if (discountType == CouponDiscountType.Product)
                {
                    // make sure the Product(s) the coupon applies to are in the User's Cart
                    List<int> couponProductIds = coupon.GetProductIds().ConvertAll(s => Convert.ToInt32(s));
                    List<int> cartProductIds = checkoutOrderInfo.Cart.GetCartProducts().ToList().ConvertAll(p => p.Id.Value);

                    // 'cartProductIds' must contain at least 1 of the 'couponProductIds'
                    List<int> intersectedIds = cartProductIds.Intersect(couponProductIds).ToList();
                    if (intersectedIds.Count == 0)
                    {
                        couponStatus = CouponStatus.NoEligibleProduct;
                        return false;
                    }
                }

                //--- Applies to shipping?
                if (discountType == CouponDiscountType.Shipping)
                {
                    // make sure one of the Shipping Option(s) the coupon applies to has been selected by the User     
                    if (checkoutOrderInfo.ShippingProvider != ShippingProviderType.UNKNOWN)
                    {
                        if (!coupon.GetShippingRateTypes().Contains(checkoutOrderInfo.ShippingRate.ServiceType))
                        {
                            couponStatus = CouponStatus.NoEligibleShipping;
                            return false;
                        }
                    }
                }

                //--- min. cart total
                if (coupon.MinOrderAmount.HasValue && (checkoutOrderInfo.Total < coupon.MinOrderAmount.Value))
                {
                    couponStatus = CouponStatus.MinOrderAmountNotReached;
                    return false;
                }

                // TODO - Max # redemptions per user
                // Probably need to implement some kind of "User GUID" cookie value at the store level to track unique users/visitors
                // since we can't reliably use UserId (won't work for anonymous checkout) or IP address (different users behind same IP)

                //--- max. # redemptions - lifetime
                if (coupon.MaxUsesLifetime.HasValue)
                {
                    int numLifetimeRedemptions = coupon.GetNumberOfRedemptions();
                    if (numLifetimeRedemptions >= coupon.MaxUsesLifetime.Value)
                    {
                        couponStatus = CouponStatus.ExceededMaxLifetimeRedemptions;
                        return false;
                    }
                }

                couponStatus = CouponStatus.Valid;
                return true;
            }
            return false;
        }
    }
}