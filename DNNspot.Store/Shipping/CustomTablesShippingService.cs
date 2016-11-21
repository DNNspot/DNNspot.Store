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
using DNNspot.Store.DataModel;

namespace DNNspot.Store.Shipping
{
    public class CustomTablesShippingService : ShippingService, IShippingService
    {
        public CustomTablesShippingService(int storeId) : base(storeId, ShippingProviderType.CustomShipping)
        {            
        }

        public IList<IShippingRate> GetRates(IPostalAddress senderAddress, IPostalAddress recipientAddress, IList<IShipmentPackageDetail> packageDetails)
        {
            var shippingRates = new List<IShippingRate>();
            
            if(packageDetails.Count != 0)
            {

                decimal cartTotalProductWeight = packageDetails.Sum(p => p.Weight);

                var rateTypes = dbShippingService.GetEnabledRateTypes();
                foreach (var rateType in rateTypes)
                {
                    decimal optionCost = 0;

                    //---- Determine cost by total weight of products in the cart
                    List<ShippingServiceRate> ratesByWeight = rateType.GetRates();


                    //AddressInfo shippingAddress = destination;

                    List<ShippingServiceRate> exactRanges = ratesByWeight.FindAll(w =>
                                                                                  cartTotalProductWeight >=
                                                                                  w.WeightMin.GetValueOrDefault(0)
                                                                                  &&
                                                                                  cartTotalProductWeight <=
                                                                                  w.WeightMax.GetValueOrDefault(0));

                    if (exactRanges.Count > 0)
                    {
                        decimal? shipCostByLocation = null;

                        //--- Check for most-specific: country AND region
                        ShippingServiceRate rateWeight =
                            exactRanges.Find(
                                r =>
                                r.CountryCode == recipientAddress.CountryCode && r.Region == recipientAddress.Region);
                        if (rateWeight != null)
                        {
                            shipCostByLocation = rateWeight.Cost.GetValueOrDefault(0);
                        }
                        else
                        {
                            //--- Check for next-specific: country only (empty region)
                            rateWeight =
                                exactRanges.Find(
                                    r => r.CountryCode == recipientAddress.CountryCode && string.IsNullOrEmpty(r.Region));
                            if (rateWeight != null)
                            {
                                shipCostByLocation = rateWeight.Cost.GetValueOrDefault(0);
                            }
                            else
                            {
                                //--- Check for least-specific: empty/default (empty country, empty region)
                                rateWeight =
                                    exactRanges.Find(
                                        r => string.IsNullOrEmpty(r.CountryCode) && string.IsNullOrEmpty(r.Region));
                                if (rateWeight != null)
                                {
                                    shipCostByLocation = rateWeight.Cost.GetValueOrDefault(0);
                                }
                            }
                        }

                        if (shipCostByLocation.HasValue)
                        {
                            //---- Add AdditionalHandlingFee for any products that have additional shipping costs
                            decimal additionalHandlingFee = packageDetails.Sum(p => p.AdditionalHandlingFee);
                            optionCost = shipCostByLocation.Value + additionalHandlingFee;

                            shippingRates.Add(new ShippingRate()
                                                  {
                                                      ServiceType = rateType.Name,
                                                      Rate = optionCost
                                                  });
                        }
                    }

                }

                shippingRates.Sort((left, right) => left.Rate.CompareTo(right.Rate));
            }
            return shippingRates;
        }

        public IShippingRate GetRate(IPostalAddress senderAddress, IPostalAddress recipientAddress, IList<IShipmentPackageDetail> packageDetails, string serviceType)
        {
            var allRates = GetRates(senderAddress, recipientAddress, packageDetails);

            var specificRate = allRates.Where(r => r.ServiceType.Equals(serviceType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            return specificRate ?? allRates.FirstOrDefault();
        }

        public ShipmentLabelResponse GetShipmentLabels(ShipmentLabelRequest shipmentLabelRequest)
        {
            return new ShipmentLabelResponse();
        }
    }
}