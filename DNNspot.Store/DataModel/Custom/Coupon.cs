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
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;
using WA.Extensions;

namespace DNNspot.Store.DataModel
{
	public partial class Coupon : esCoupon
	{
		public Coupon()
		{

        }
        const string idSeparator = ",";

        public decimal PercentOffDecimal
        {
            get { return (PercentOff.GetValueOrDefault(0) / 100.0m); }
        }

        public CouponDiscountType DiscountTypeName
        {
            get { return WA.Enum<CouponDiscountType>.TryParseOrDefault(this.DiscountType, CouponDiscountType.UNKNOWN); }
            set { this.DiscountType = value.ToString(); }
        }

        public bool IsPercentOff
        {
            get { return PercentOff.HasValue; }
        }

        public bool IsAmountOff
        {
            get { return !IsPercentOff; }
        }

        public string ValidDateDisplayString
        {
            get
            {
                if (this.ValidFromDate.HasValue && this.ValidToDate.HasValue)
                {
                    return string.Format("{0} to {1}", this.ValidFromDate.Value.ToShortDateString(), this.ValidToDate.Value.ToShortDateString());
                }
                else if (this.ValidFromDate.HasValue)
                {
                    return "from " + this.ValidFromDate.Value.ToShortDateString();
                }
                else if (this.ValidToDate.HasValue)
                {
                    return "until " + this.ValidToDate.Value.ToShortDateString();
                }
                return string.Empty;
            }
        }

        public List<string> GetProductIds()
        {
            if (!string.IsNullOrEmpty(AppliesToProductIds))
            {
                return AppliesToProductIds.ToList(idSeparator);
            }
            return new List<string>();
        }

        public void SetProductIds(IList<string> idList)
        {
            this.AppliesToProductIds = idList.ToCsv();
        }

        public List<string> GetShippingRateTypes()
        {
            if (!string.IsNullOrEmpty(AppliesToShippingRateTypes))
            {
                return AppliesToShippingRateTypes.ToList(idSeparator);
            }
            return new List<string>();
        }

        public void SetShippingRateTypes(IList<string> rateTypeList)
        {
            this.AppliesToShippingRateTypes = rateTypeList.ToCsv();
        }

        public static Coupon GetCoupon(string couponCode, int storeId)
        {
            CouponQuery q = new CouponQuery();
            q.es.Top = 1;
            q.Where(q.StoreId == storeId, q.Code.ToUpper() == couponCode.ToUpper());

            Coupon coupon = new Coupon();
            if (coupon.Load(q))
            {
                return coupon;
            }
            return null;
        }

        public int GetNumberOfRedemptions()
        {
            //SELECT            
            //COUNT(oc.CouponCode) as NumRedemptions
            //FROM DNNspot_Store_OrderCoupon oc
            //INNER JOIN DNNspot_Store_Order o ON o.Id = oc.OrderId
            //WHERE o.StoreId = @storeId AND CouponCode = @couponCode
            //GROUP BY oc.CouponCode

            OrderCouponQuery oc = new OrderCouponQuery("oc");
            OrderQuery o = new OrderQuery("o");

            oc.Select(oc.CouponCode.Count().As("NumRedemptions"));
            oc.InnerJoin(o).On(o.Id == oc.OrderId);
            oc.Where(o.StoreId == this.StoreId.Value, oc.CouponCode.ToUpper() == this.Code.ToUpper());
            oc.GroupBy(oc.CouponCode);

            object scalar = oc.ExecuteScalar();
            if (scalar != null)
            {
                return (int)scalar;
            }
            return 0;
        }
	}
}
