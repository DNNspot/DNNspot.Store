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
using DotNetNuke.Entities.Portals;
using DotNetNuke.Security.Roles;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;
using WA.Extensions;

namespace DNNspot.Store.DataModel
{
	public partial class Discount : esDiscount
	{
		public Discount()
		{
		
		}

        const string idSeparator = ",";

        public DiscountDiscountType DiscountTypeName
        {
            get { return WA.Enum<DiscountDiscountType>.TryParseOrDefault(this.DiscountType, DiscountDiscountType.UNKNOWN); }
            set { this.DiscountType = value.ToString(); }
        }

        public bool IsPercentOff
        {
            get { return PercentOff.GetValueOrDefault(0) > AmountOff.GetValueOrDefault(0); }
        }

        public bool IsAmountOff
        {
            get { return !IsPercentOff; }
        }

        public decimal PercentOffDecimal
        {
            get { return (PercentOff.GetValueOrDefault(0) / 100.0m); }
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

        public string GetRoleName()
        {
            if (this.DnnRoleId.HasValue)
            {
                int portalId = PortalController.GetCurrentPortalSettings().PortalId;

                RoleController roleController = new RoleController();
                RoleInfo roleInfo = roleController.GetRole(this.DnnRoleId.Value, portalId);
                if (roleInfo != null)
                {
                    return roleInfo.RoleName;
                }
                return string.Empty;
            }
            else
            {
                return "All Users";
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

        public List<string> GetCategoryIds()
        {
            if (!string.IsNullOrEmpty(AppliesToCategoryIds))
            {
                return AppliesToCategoryIds.ToList(idSeparator);
            }
            return new List<string>();
        }

        public void SetCategoryIds(IList<string> idList)
        {
            this.AppliesToCategoryIds = idList.ToCsv();
        }
	}
}
