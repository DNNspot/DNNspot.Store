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
using WA.Extensions;

namespace DNNspot.Store.Shipping
{
    public class UpsShippingService : EzShippingService
    {
        EzAccountServerUrls _urls;

        public UpsShippingService(int storeId) : base(storeId, ShippingProviderType.UPS)
        {
            Init();
        }

        public UpsShippingService(int storeId, int? orderId, Guid? cartId) : base(storeId, ShippingProviderType.UPS, orderId, cartId)
        {
            Init();
        }


        private void Init()
        {
            if (isTestGateway)
            {
                // UPS uses differents URLs depending on the service
                _urls = new EzAccountServerUrls()
                {
                    RateUrl = "https://wwwcie.ups.com/ups.app/xml/Rate",
                    ShipUrl = "https://wwwcie.ups.com/ups.app/xml/ShipConfirm",
                    TrackUrl = "https://wwwcie.ups.com/ups.app/xml/Track"
                };
            }
            else
            {
                _urls = new EzAccountServerUrls()
                {
                    RateUrl = "https://onlinetools.ups.com/ups.app/xml/Rate",
                    ShipUrl = "https://onlinetools.ups.com/ups.app/xml/ShipConfirm",
                    //ShipUrl = "https://onlinetools.ups.com/ups.app/xml/ShipAccept",
                    TrackUrl = "https://onlinetools.ups.com/ups.app/xml/Track"
                };
            }

            string accessKey = config.TryGetValueOrEmpty("accessKey");
            string userId = config.TryGetValueOrEmpty("userId");
            string password = config.TryGetValueOrEmpty("password");
            string accountNumber = config.TryGetValueOrEmpty("accountNumber");

            ezrater.Account.AccessKey = accessKey;
            ezrater.Account.UserId = userId;
            ezrater.Account.Password = password;
            ezrater.Account.AccountNumber = accountNumber;

            ezship.Account.AccessKey = accessKey;
            ezship.Account.UserId = userId;
            ezship.Account.Password = password;
            ezship.Account.AccountNumber = accountNumber;
        }
        protected override EzAccountServerUrls ServiceUrls
        {
            get { return _urls; }
        }
    }
}