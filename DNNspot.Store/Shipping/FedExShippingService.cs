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
using System.Globalization;
using System.Linq;
using System.Web;
using WA.Extensions;
using nsoftware.InShip;

namespace DNNspot.Store.Shipping
{
    public class FedExShippingService : EzShippingService
    {
        EzAccountServerUrls _urls;

        public FedExShippingService(int storeId) : base(storeId, ShippingProviderType.FedEx)
        {
            Init();
        }


        public FedExShippingService(int storeId, int? orderId, Guid? cartId) : base(storeId, ShippingProviderType.FedEx, orderId, cartId)
        {
            Init();
        }

        
        private void Init()
        {
            string url = isTestGateway ? "https://gatewaybeta.fedex.com:443/xml" : "https://gateway.fedex.com:443/xml";
            // FedEx uses the same URL for all types of requests                
            _urls = new EzAccountServerUrls()
            {
                RateUrl = url,
                ShipUrl = url,
                TrackUrl = url,
            };

            string developerKey = config.TryGetValueOrEmpty("apiKey");
            string meterNumber = config.TryGetValueOrEmpty("meterNumber");
            string accountNumber = config.TryGetValueOrEmpty("accountNumber");
            string password = config.TryGetValueOrEmpty("apiPassword");
            string smartPostHub = config.TryGetValueOrEmpty("smartPostHubId");
            string labelStockType = config.TryGetValueOrEmpty("labelStockType");
            string labelImageType = config.TryGetValueOrEmpty("labelImageType");

            ezrater.Account.DeveloperKey = developerKey;
            ezrater.Account.MeterNumber = meterNumber;
            ezrater.Account.AccountNumber = accountNumber;
            ezrater.Account.Password = password;
            if (!string.IsNullOrEmpty(smartPostHub))
            {
                ezrater.Config("SmartPostHubId=" + smartPostHub);
                ezrater.Config("SmartPostIndicia=1");
                ezrater.Config("SmartPostAncillaryEndorsement=2");
            }

            ezship.Account.DeveloperKey = developerKey;
            ezship.Account.MeterNumber = meterNumber;
            ezship.Account.AccountNumber = accountNumber;
            ezship.Account.Password = password;
            if (!string.IsNullOrEmpty(smartPostHub))
            {
                ezship.Config("SmartPostHubId=" + smartPostHub);
                ezship.Config("SmartPostIndicia=1");
                ezship.Config("SmartPostAncillaryEndorsement=2");
            }

            if (!string.IsNullOrEmpty(labelStockType))
            {
                ezship.Config("LabelStockType=" + labelStockType);
            }

            if (!string.IsNullOrEmpty(labelImageType))
            {

            }
        }

        protected override EzAccountServerUrls ServiceUrls
        {
            get { return _urls; }
        }
    }
}