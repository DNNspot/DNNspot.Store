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
Date Generated       : 4/12/2013 3:32:34 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class ShippingServiceRateType : esShippingServiceRateType
	{
		public ShippingServiceRateType()
		{

        }
        //public string Key
        //{
        //    get { return string.Format(@"{0}-{1}-{2}", this.StoreId, this.ProviderId, this.Name); }
        //}

        public static ShippingServiceRateType Get(int id)
        {
            var shippingMethod = new ShippingServiceRateType();
            if (shippingMethod.LoadByPrimaryKey(id))
            {
                return shippingMethod;
            }
            return null;
        }

        public static ShippingServiceRateType Find(int shippingServiceId, string name)
        {
            var q = new ShippingServiceRateTypeQuery();
            q.Where(q.ShippingServiceId == shippingServiceId, q.Name == name);

            ShippingServiceRateType shippingMethod = new ShippingServiceRateType();
            return shippingMethod.Load(q) ? shippingMethod : null;
        }

        public List<ShippingServiceRate> GetRates()
        {

            //this.ShippingServiceRateCollectionByRateTypeId.Sort =
            //    ShippingServiceRateMetadata.PropertyNames.CountryCode + " ASC, "
            //    + ShippingServiceRateMetadata.PropertyNames.Region + " ASC, "
            //    + ShippingServiceRateMetadata.PropertyNames.Cost + " ASC";

            return this.ShippingServiceRateCollectionByRateTypeId.AsQueryable().OrderBy(x => x.CountryCode).ThenBy(x => x.Region).ThenBy(x => x.Cost).ToList();
        }

        internal void DeleteAllRates()
        {
            var q = new ShippingServiceRateQuery();
            q.Where(q.RateTypeId == this.Id);

            var collection = new ShippingServiceRateCollection();
            collection.Load(q);

            collection.MarkAllAsDeleted();
            collection.Save();
        }
	}
}
