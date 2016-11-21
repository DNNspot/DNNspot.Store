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
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class OrderCollection : esOrderCollection
	{
		public OrderCollection()
		{
		
		}

        internal static List<Order> GetOrdersForUser(int userId, int? storeId, bool includeDeleted)
        {
            OrderQuery q = new OrderQuery();
            q.Where(q.UserId == userId);
            if (storeId.HasValue)
            {
                q.Where(q.StoreId == storeId);
            }
            if (!includeDeleted)
            {
                q.Where(q.IsDeleted == false);
            }
            q.OrderBy(q.CreatedOn.Descending);

            OrderCollection collection = new OrderCollection();
            if (collection.Load(q))
            {
                return collection.ToList();
            }
            return new List<Order>();
        }

        internal static List<Order> GetOrdersByIds(List<int> orderIds)
        {
            var q = new OrderQuery();
            q.Where(q.Id.In(orderIds.ToArray()));
            q.OrderBy(q.OrderNumber.Ascending);

            var collection = new OrderCollection();
            collection.Load(q);

            return collection.ToList();
        }

        internal static List<Order> FindOrdersByOrderNumber(int storeId, List<string> orderNumbers)
        {
            var q = new OrderQuery();
            q.Where(q.StoreId == storeId, q.OrderNumber.In(orderNumbers.ToArray()));
            q.OrderBy(q.OrderNumber.Ascending);

            var collection = new OrderCollection();
            collection.Load(q);

            return collection.ToList();
        }

        internal static List<Order> FindOrders(int storeId, DateTime? fromDate, DateTime? toDate, string customerFirstName, string customerLastName, string customerEmail, List<OrderStatusName> orderStatuses, int? maxResults)
        {
            OrderQuery q = new OrderQuery();
            q.Where(q.StoreId == storeId);

            if (maxResults.HasValue)
            {
                q.es.Top = maxResults.Value;
            }

            if (fromDate.HasValue)
            {
                q.Where(q.CreatedOn >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                q.Where(q.CreatedOn <= toDate.Value);
            }
            if (!string.IsNullOrEmpty(customerFirstName))
            {
                q.Where(q.CustomerFirstName.Like("%" + customerFirstName + "%"));
            }
            if (!string.IsNullOrEmpty(customerLastName))
            {
                q.Where(q.CustomerLastName.Like("%" + customerLastName + "%"));
            }
            if (!string.IsNullOrEmpty(customerEmail))
            {
                q.Where(q.CustomerEmail.Like("%" + customerEmail + "%"));
            }
            if (orderStatuses.Count > 0)
            {
                short[] orderStatusIds = orderStatuses.ConvertAll(x => (short)x).ToArray();
                q.Where(q.OrderStatusId.In(orderStatusIds));
            }
            q.OrderBy(q.CreatedOn.Descending);

            OrderCollection collection = new OrderCollection();
            collection.Load(q);

            return collection.ToList();
        }

        internal static int GetPendingOrderCount(int storeId)
        {
            OrderQuery q = new OrderQuery();
            q.es.CountAll = true;
            q.Where(q.StoreId == storeId);
            q.Where(q.OrderStatusId != (short)OrderStatusName.Completed);
            q.Where(q.IsDeleted == false);

            return (int)q.ExecuteScalar();
        }

        internal static List<Order> GetPendingOrders(int storeId)
        {
            OrderQuery q = new OrderQuery();
            q.Where(q.StoreId == storeId);
            q.Where(q.OrderStatusId != (short)OrderStatusName.Completed);
            q.Where(q.IsDeleted == false);
            q.OrderBy(q.CreatedOn.Descending);

            OrderCollection collection = new OrderCollection();
            collection.Load(q);

            return collection.ToList();
        }

        internal static int GetCompletedOrderCount(int storeId)
        {
            OrderQuery q = new OrderQuery();
            q.es.CountAll = true;
            q.Where(q.StoreId == storeId);
            q.Where(q.OrderStatusId == (short)OrderStatusName.Completed);
            q.Where(q.IsDeleted == false);

            return (int)q.ExecuteScalar();
        }

        internal static List<Order> GetCompletedOrders(int storeId)
        {
            OrderQuery q = new OrderQuery();
            q.Where(q.StoreId == storeId);
            q.Where(q.OrderStatusId == (short)OrderStatusName.Completed);
            q.Where(q.IsDeleted == false);
            q.OrderBy(q.CreatedOn.Descending);

            OrderCollection collection = new OrderCollection();
            collection.Load(q);

            return collection.ToList();
        }

        public static List<Order> GetAllOrders(int? storeId)
        {
            OrderQuery q = new OrderQuery();
            q.Where(q.StoreId == storeId);
            q.Where(q.IsDeleted == false);
            q.OrderBy(q.CreatedOn.Descending);

            OrderCollection collection = new OrderCollection();
            collection.Load(q);

            return collection.ToList();
        }
	}
}
