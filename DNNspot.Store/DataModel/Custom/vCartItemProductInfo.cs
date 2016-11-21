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
Date Generated       : 4/12/2013 3:32:53 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;
using WA.Extensions;

namespace DNNspot.Store.DataModel
{
	public partial class vCartItemProductInfo : esvCartItemProductInfo
	{
		public vCartItemProductInfo()
		{

        }        private List<JsonProductFieldData> jsonProductFieldDataCache = null;

        public ProductDeliveryMethod DeliveryMethod
        {
            get
            {
                return this.ProductDeliveryMethodId.HasValue
                           ? (ProductDeliveryMethod)this.ProductDeliveryMethodId.Value
                           : ProductDeliveryMethod.UNKNOWN;
            }
        }

        public Product GetProduct()
        {
            Product p = new Product();
            if (this.ProductId.HasValue)
            {
                p.LoadByPrimaryKey(this.ProductId.Value);
            }
            return p;
        }

        public decimal GetPriceForSingleItem()
        {
            return GetProductItemPriceAdjustedForProductFields();
        }

        public decimal GetPriceForQuantity()
        {
            // TODO - implement Quantity Discounts here ??
            decimal pricePerItem = GetProductItemPriceAdjustedForProductFields();
            return (Quantity.Value * pricePerItem);
        }

        private decimal GetProductItemPriceAdjustedForProductFields()
        {
            if (jsonProductFieldDataCache == null)
            {
                GetProductFieldData();
            }

            decimal totalItemPriceAdjust = jsonProductFieldDataCache.Sum(pf => pf.PriceAdjustment);

            Product p = this.GetProduct();

            //return this.ProductItemPrice.Value + totalItemPriceAdjust;
            return p.GetPrice() + totalItemPriceAdjust;
        }

        public decimal GetWeightForQuantity()
        {
            return (Quantity.Value * GetProductItemWeightAdjustedForProductFields());
        }

        private decimal GetProductItemWeightAdjustedForProductFields()
        {
            if (jsonProductFieldDataCache == null)
            {
                GetProductFieldData();
            }

            decimal totalItemWeightAdjust = jsonProductFieldDataCache.Sum(pf => pf.WeightAdjustment);

            return this.ProductWeight.GetValueOrDefault(0) + totalItemWeightAdjust;
        }

        public List<JsonProductFieldData> GetProductFieldData()
        {
            if (jsonProductFieldDataCache == null)
            {
                if (!string.IsNullOrEmpty(this.ProductFieldData))
                {
                    jsonProductFieldDataCache = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonProductFieldData>>(this.ProductFieldData);
                }
                else
                {
                    jsonProductFieldDataCache = new List<JsonProductFieldData>();
                }
            }
            return jsonProductFieldDataCache;
        }

        public string GetProductFieldDataDisplayString()
        {
            List<JsonProductFieldData> fieldData = this.GetProductFieldData();
            if (fieldData.Count > 0)
            {
                StringBuilder html = new StringBuilder("<span class=\"productFields\">");
                foreach (JsonProductFieldData field in fieldData)
                {
                    html.AppendFormat(@"<span>{0}: {1}</span>", field.Name, field.ChoiceValues.ToDelimitedString(", "));
                }
                html.Append("</span>");
                return html.ToString();
            }
            return "";
        }

	}
}
