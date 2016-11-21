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

namespace DNNspot.Store.Shipping
{
    public abstract class ShippingService
    {
        protected int storeId;
        protected ShippingProviderType providerType = Store.ShippingProviderType.UNKNOWN;
        protected DataModel.ShippingService dbShippingService = new DataModel.ShippingService();
        protected Dictionary<string, string> config = new Dictionary<string, string>();
        protected Guid? cartId;
        protected int? orderId;

        protected ShippingService(int storeId, ShippingProviderType providerType)
        {
            Init(storeId, providerType);
        }

        protected ShippingService(int storeId, ShippingProviderType providerType, int? orderId, Guid? cartId)
        {
            Init(storeId, providerType);
            this.orderId = orderId;
            this.cartId = cartId;
        }

        private void Init(int storeId, ShippingProviderType providerType)
        {
            this.storeId = storeId;
            this.providerType = providerType;

            dbShippingService = DataModel.ShippingService.Find(storeId, providerType);
            if (dbShippingService == null)
            {
                throw new ApplicationException(string.Format("Unable to find ShippingService for ProviderType '{0}'", providerType));
            }
            this.config = dbShippingService.GetSettingsDictionary();       
        }
    }
}