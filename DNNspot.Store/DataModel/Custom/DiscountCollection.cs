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
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;
using WA.Extensions;

namespace DNNspot.Store.DataModel
{
	public partial class DiscountCollection : esDiscountCollection
	{
		public DiscountCollection()
		{
		
		}

        public static List<Discount> GetAll(int storeId)
        {
            DiscountQuery q = new DiscountQuery();
            q.Where(q.StoreId == storeId);
            q.OrderBy(q.DnnRoleId.Ascending);

            DiscountCollection collection = new DiscountCollection();
            collection.Load(q);

            return collection.ToList();
        }

        internal static List<Discount> GetActiveDiscountsForCurrentUser(int storeId)
        {
            UserInfo userInfo = UserController.GetCurrentUserInfo();

            RoleController roleController = new RoleController();
            List<RoleInfo> userRoles = roleController.GetUserRoles(userInfo.PortalID, userInfo.UserID).ToList<RoleInfo>();

            List<int> roleIds = userRoles.ConvertAll(r => r.RoleID);

            DiscountQuery q = new DiscountQuery();
            q.Where(q.StoreId == storeId);
            q.Where(q.IsActive == true);
            if (roleIds.Count > 0)
            {
                q.Where(q.Or(
                        q.DnnRoleId.IsNull(), q.DnnRoleId.In(roleIds.ToArray())
                    ));
            }
            else
            {
                q.Where(q.Or(q.DnnRoleId.IsNull()));
            }

            DateTime now = DateTime.Now;
            q.Where(q.Or(
                    q.ValidFromDate.IsNull(), now >= q.ValidFromDate
                ));
            q.Where(q.Or(
                    q.ValidToDate.IsNull(), now <= q.ValidToDate
                ));

            DiscountCollection discounts = new DiscountCollection();
            discounts.Load(q);

            return discounts.ToList();
        }
	}
}
