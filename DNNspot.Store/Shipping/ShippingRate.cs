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
using WA.Extensions;

namespace DNNspot.Store.Shipping
{
    public class ShippingRate : IShippingRate
    {
        public string ServiceType { get; set; }
        public string ServiceTypeDescription { get; set; }

        public decimal Rate { get; set; }

        public ShippingRate()
        {
            ServiceType = string.Empty;
            ServiceTypeDescription = string.Empty;
            Rate = 0;
        }

        public string DisplayName
        {
            get
            {
                string providerName = string.Empty;
                if (ServiceType.Contains("FedEx"))
                    providerName = "FedEx";
                else if (ServiceType.Contains("Ups") || ServiceType.Contains("UPS"))
                    providerName = "UPS";

                string description = ServiceType;
                if(!string.IsNullOrEmpty(ServiceTypeDescription))
                {
                    description = ServiceTypeDescription.Replace('_', ' ');
                    if(!string.IsNullOrEmpty(providerName))
                    {
                        description = description.Replace(providerName, string.Empty).Replace(providerName.ToUpper(),string.Empty);
                    }
                    //description = description.ToLower().ToTitleCase();
                }                

                if(!string.IsNullOrEmpty(providerName))
                {
                    return string.Format(@"{0} - {1}", providerName, description);
                }
                else
                {
                    return description;
                }
            }
        }
    }
}