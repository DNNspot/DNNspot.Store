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
using DNNspot.Store.Shipping;
using WA.Extensions;

namespace DNNspot.Store
{
    public class CheckoutOrderInfo
    {
        public int StoreId
        {
            get { return cart.StoreId.GetValueOrDefault(-1); }
        }
        DataModel.Cart cart;
        List<CheckoutCouponInfo> appliedCoupons;        

        public AddressInfo BillingAddress { get; set; }
        public bool? ShipToBillingAddress { get; set; }

        public AddressInfo StoreAddress { get; private set; }
        public AddressInfo ShippingAddress { get; set; }        
        //public ShippingOption ShippingOption { get; set; }        
        public IShippingRate ShippingRate { get; set; }
        public ShippingProviderType ShippingProvider
        {
            get
            {
                if(ShippingRate.ServiceType.Contains("FedEx",false))
                {
                    return ShippingProviderType.FedEx;
                }
                else if (ShippingRate.ServiceType.Contains("Ups", false))
                {
                    return ShippingProviderType.UPS;
                }
                else 
                {
                    return ShippingProviderType.CustomShipping;
                }
            }
        }
        
        public CreditCardInfo CreditCard { get; set; }
        public PaymentProviderName PaymentProvider { get; set; }                
        public Dictionary<string, string> PayPalVariables { get; private set; }
        
        public decimal SubTotal { get; private set; }
        public decimal DiscountAmount { get; private set; }
        //public decimal ShippingAmount { get; private set; }
        public decimal TaxAmount { get; private set; }        
        public decimal Total { get; private set; }

        public string OrderNotes { get; set; }  

        public DataModel.Cart Cart
        {
            get { return cart; }
            set
            {
                cart = value;
                ReCalculateOrderTotals();
            }
        }

        public List<CheckoutCouponInfo> GetAppliedCoupons()
        {
            return appliedCoupons;
        }

        public void RemoveAllCoupons()
        {
            appliedCoupons.Clear();
            ReCalculateOrderTotals();
        }

        public CheckoutOrderInfo()
        {
            cart = new Cart();
            appliedCoupons = new List<CheckoutCouponInfo>();
            
            BillingAddress = new AddressInfo();
            BillingAddress.PropertyChanged += BillingAddress_PropertyChanged;
            BillingAddress.EnablePropertyChangeEvent = true;

            ShipToBillingAddress = null;

            ShippingAddress = new AddressInfo();
            //ShippingAddress.PropertyChanged += ShippingAddress_PropertyChanged;
            //ShippingOption = new ShippingOption();
            ShippingRate = new ShippingRate();            

            CreditCard = new CreditCardInfo();
            PaymentProvider = PaymentProviderName.UNKNOWN;            
            PayPalVariables = new Dictionary<string, string>();

            SubTotal = 0;
            DiscountAmount = 0;
            //ShippingAmount = 0;
            TaxAmount = 0;            
            Total = 0;

            OrderNotes = String.Empty;
        }

        public bool RequiresPayment
        {
            get { return Total > 0; }
        }

        public bool HasOnlyDownloadableProducts
        {
            get
            {
                return cart.GetCartProducts().ToList().TrueForAll(p => p.DeliveryMethodId == (short)ProductDeliveryMethod.Downloaded);
            }
        }

        //void ShippingAddress_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    // nothing, for now
        //}

        void BillingAddress_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Country" || e.PropertyName == "Region")
            {
                ReCalculateOrderTotals();
            }
        }

        public void ReCalculateOrderTotals()
        {
            var store = DataModel.Store.GetStore(StoreId);

            // SubTotal - add up cart items and quantities
            decimal cartItemSubTotal = 0.0m;
            decimal taxableSubTotal = 0.0m;
            List<vCartItemProductInfo> cartItems = cart.GetCartItemsWithProductInfo();
            foreach (vCartItemProductInfo item in cartItems)
            {
                decimal itemPriceForQuantity = item.GetPriceForQuantity();
                cartItemSubTotal += itemPriceForQuantity;
                if(item.ProductIsTaxable.GetValueOrDefault(true))
                {
                    taxableSubTotal += itemPriceForQuantity;
                }
            }
            SubTotal = cartItemSubTotal;
            
            // Shipping Cost - Provided by ShippingProvider        
            bool calculateShipping = (ShippingAddress != null) && (!string.IsNullOrEmpty(ShippingAddress.PostalCode));
            if (calculateShipping)
            {
                var shippingService = ShippingServiceFactory.Get(StoreId, ShippingProvider, null, cart.Id);
                if (shippingService != null)
                {
                    ShipmentPackagingStrategy shipmentPackagingStrategy = WA.Enum<ShipmentPackagingStrategy>.TryParseOrDefault(store.GetSetting(StoreSettingNames.ShipmentPackagingStrategy), ShipmentPackagingStrategy.SingleBox);
                    var rate = shippingService.GetRate(store.Address.ToPostalAddress(), ShippingAddress.ToPostalAddress(), cart.GetCartItemsAsShipmentPackages(shipmentPackagingStrategy), this.ShippingRate.ServiceType);
                    if (rate != null)
                    {                        
                        this.ShippingRate = rate;
                    }
                }
            }
            else
            {
                this.ShippingRate.Rate = 0;
            }

            // Coupons
            ReApplyAllCoupons();
            
            // Tax Amount            
            string taxCountry = BillingAddress.Country;
            string taxRegion = BillingAddress.Region;
            bool taxShipping = true;

            if (store != null)
            {
                if (store.GetSetting(StoreSettingNames.SalesTaxAddressType) == "Shipping")
                {
                    taxCountry = ShippingAddress.Country;
                    taxRegion = ShippingAddress.Region;
                }

                taxShipping = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.TaxShipping)).GetValueOrDefault(true);
            }
            decimal taxRate = TaxRegion.GetTaxRate(cart.StoreId.GetValueOrDefault(-1), taxCountry, taxRegion);

            if (taxShipping)
            {
                TaxAmount = ((Math.Max(0, taxableSubTotal - DiscountAmount) + ShippingRate.Rate)*taxRate).RoundForMoney();
            }
            else
            {
                TaxAmount = ((Math.Max(0, taxableSubTotal - DiscountAmount)) * taxRate).RoundForMoney();
            }

            // Total
            Total = (SubTotal + ShippingRate.Rate - DiscountAmount + TaxAmount).RoundForMoney();            
        }

        private void ReApplyAllCoupons()
        {
            // grab a copy of the existing coupon codes, then empty the 'applied' list
            string[] couponCodes = appliedCoupons.ConvertAll(c => c.CouponCode).ToArray();
            appliedCoupons.Clear();

            // reset the cart/order total 
            Total = SubTotal;

            // re-apply each coupon code
            foreach (string couponCode in couponCodes)
            {
                CouponStatus couponStatus;

                ApplyCouponCode(couponCode, out couponStatus);
            }

            DiscountAmount = appliedCoupons.Sum(c => c.DiscountAmount);

            //  SANITY CHECK - can't discount more than the SubTotal + Shipping
            if (DiscountAmount > (SubTotal + ShippingRate.Rate))
            {
                DiscountAmount = SubTotal + ShippingRate.Rate;
            }
        }

        private bool ApplyCouponCode(string couponCode, out CouponStatus couponStatus)
        {
            int storeId = this.cart.StoreId.Value;
            Coupon coupon = Coupon.GetCoupon(couponCode, storeId);
            
            bool isValid = CouponController.IsCouponValidForCheckout(coupon, this, out couponStatus);
            if (isValid)
            {
                // calculate discount amount for coupon
                decimal discountAmount = CouponController.CalculateDiscountAmount(coupon, this);
                if (discountAmount > 0)
                {
                    appliedCoupons.Add(new CheckoutCouponInfo() { CouponCode = coupon.Code, IsCombinable = coupon.IsCombinable.Value, DiscountAmount = discountAmount });

                    return true;
                }
            }
            return false;
        }

        public bool ApplyCoupon(string couponCode, out CouponStatus couponStatus)
        {
            bool isValid = ApplyCouponCode(couponCode, out couponStatus);

            ReCalculateOrderTotals();

            return isValid;
        }

        public void SetBillingAddressFromOrder(Order order)
        {
            BillingAddress.EnablePropertyChangeEvent = false;

            BillingAddress.Address1 = order.BillAddress1;
            BillingAddress.Address2 = order.BillAddress2;
            BillingAddress.City = order.BillCity;
            BillingAddress.Region = order.BillRegion;
            BillingAddress.PostalCode = order.BillPostalCode;
            BillingAddress.Country = order.BillCountryCode;

            BillingAddress.EnablePropertyChangeEvent = true;

            // billing address can affect Sales Tax            
            ReCalculateOrderTotals();
        }

        public void SetShippingAddressFromOrder(Order order)
        {
            ShippingAddress.EnablePropertyChangeEvent = false;

            ShippingAddress.Address1 = order.ShipAddress1;
            ShippingAddress.Address2 = order.ShipAddress2;
            ShippingAddress.City = order.ShipCity;
            ShippingAddress.Region = order.ShipRegion;
            ShippingAddress.PostalCode = order.ShipPostalCode;
            ShippingAddress.Country = order.ShipCountryCode;

            ShippingAddress.EnablePropertyChangeEvent = true;
        }
    }
}
