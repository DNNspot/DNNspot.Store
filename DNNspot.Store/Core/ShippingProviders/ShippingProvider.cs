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

namespace DNNspot.Store.ShippingProviders
{
    public abstract partial class ShippingProvider : esShippingService, IShippingProvider
    {
        protected readonly int storeId;
        protected readonly ShippingProviderType providerType = Store.ShippingProviderType.UNKNOWN;
        protected readonly ShippingService shippingService = new ShippingService();
        protected readonly Dictionary<string, string> settings = new Dictionary<string, string>();

        public bool IsEnabled
        {
            get
            {
                return settings.ContainsKey("IsEnabled")
                    ? WA.Parser.ToBool(settings["IsEnabled"]).GetValueOrDefault(false)
                    : false;
            }            
        }

        public List<string> ErrorMessages { get; protected set; }

        protected ShippingProvider(int storeId, ShippingProviderType providerType)
        {
            this.storeId = storeId;
            this.providerType = providerType;
            this.ErrorMessages = new List<string>();

            shippingService = ShippingService.Find(storeId, providerType);
            if(shippingService == null)
            {
                throw new ApplicationException(string.Format("Unable to find ShippingService for ProviderType '{0}'", providerType));
            }
            this.settings = shippingService.GetSettingsDictionary();
        }

        public abstract ProcessShipmentResult ProcessShipment(Order order, ShippingLabelType shippingLabelType);
        public abstract List<ShippingOption> GetShippingOptions();
        public abstract List<ShippingOption> GetShippingOptions(AddressInfo origin, AddressInfo destination);
        public abstract List<ShippingOption> GetShippingOptionEstimates(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts);
        public abstract decimal GetShippingOptionCost(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts, ShippingOption shippingOption);
    }
}