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
    public class CustomShippingProvider : ShippingProvider
    {
        public CustomShippingProvider(int storeId, ShippingProviderType providerType) : base(storeId, providerType)
        {
        }

        public override ProcessShipmentResult ProcessShipment(Order order, ShippingLabelType shippingLabelType)
        {
            //return new ProcessShipmentResult()
            //                 {
            //                     Success = false,
            //                     ErrorMessages = new List<string>() { "CustomShipping does not process shipments" }
            //                 };

            return new ProcessShipmentResult()
            {
                Success = true
            };            
        }

        public override List<ShippingOption> GetShippingOptions()
        {
            return shippingService.GetEnabledRateTypes().ConvertAll(x => new ShippingOption()
            {
                ProviderType = providerType,
                Name = x.Name,
                DisplayName = x.DisplayName,
                Cost = null
            });
        }

        public override List<ShippingOption> GetShippingOptions(AddressInfo origin, AddressInfo destination)
        {
            // this provider doesn't depend on origin or destination address for available options
            return GetShippingOptions();
        }

        public override List<ShippingOption> GetShippingOptionEstimates(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts)
        {
            var rateTypes = shippingService.GetEnabledRateTypes();

            return GetShippingOptionEstimates(origin, destination, cartProducts, rateTypes);
        }


        public override decimal GetShippingOptionCost(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts, ShippingOption shippingOption)
        {
            var rateType = shippingService.GetEnabledRateTypes().Find(x => x.Name == shippingOption.Name);
            if(rateType == null)
            {
                throw new ApplicationException("Unable to find a ShippingServiceRateType for ShippingOption '" + shippingOption.Name + "'");
            }

            var estimates = GetShippingOptionEstimates(origin, destination, cartProducts, new[] { rateType });
            if (estimates.Count == 0)
            {
                //throw new ApplicationException("Unable to get a ShippingOptionEstimate for RateType '" + rateType.Name + "'");
                return 0;
            }
            else
            {
                return estimates[0].Cost.GetValueOrDefault(0);
            }
        }

        private List<ShippingOption> GetShippingOptionEstimates(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts, IEnumerable<ShippingServiceRateType> rateTypes)
        {
            List<ShippingOption> shippingOptions = new List<ShippingOption>();

            //var rateTypes = service.GetEnabledRateTypes();
            foreach(var rateType in rateTypes)
            {
                decimal optionCost = 0;

                // special case: we have a cart with only Downloadable items, so no shipping cost
                if (!cartProducts.TrueForAll(cp => cp.DeliveryMethod == ProductDeliveryMethod.Downloaded))
                {
                    //---- Determine cost by total weight of products in the cart
                    List<ShippingServiceRate> ratesByWeight = rateType.GetRates();
                    decimal cartTotalProductWeight = cartProducts.Sum(p => p.GetWeightForQuantity());
                    
                    AddressInfo shippingAddress = destination;

                    List<ShippingServiceRate> exactRanges = ratesByWeight.FindAll(w =>
                        cartTotalProductWeight >= w.WeightMin.GetValueOrDefault(0)
                        && cartTotalProductWeight <= w.WeightMax.GetValueOrDefault(0));

                    if (exactRanges.Count > 0)
                    {
                        decimal? shipCostByLocation = null;

                        //--- Check for most-specific: country AND region
                        ShippingServiceRate rateWeight = exactRanges.Find(r => r.CountryCode == shippingAddress.Country && r.Region == shippingAddress.Region);
                        if (rateWeight != null)
                        {
                            shipCostByLocation = rateWeight.Cost.GetValueOrDefault(0);
                        }
                        else
                        {
                            //--- Check for next-specific: country only (empty region)
                            rateWeight = exactRanges.Find(r => r.CountryCode == shippingAddress.Country && string.IsNullOrEmpty(r.Region));
                            if (rateWeight != null)
                            {
                                shipCostByLocation = rateWeight.Cost.GetValueOrDefault(0);
                            }
                            else
                            {
                                //--- Check for least-specific: empty/default (empty country, empty region)
                                rateWeight = exactRanges.Find(r => string.IsNullOrEmpty(r.CountryCode) && string.IsNullOrEmpty(r.Region));
                                if (rateWeight != null)
                                {
                                    shipCostByLocation = rateWeight.Cost.GetValueOrDefault(0);
                                }
                            }
                        }

                        if (shipCostByLocation.HasValue)
                        {
                            //---- Add amounts for any products that have additional shipping costs
                            decimal additionalShipCost = cartProducts.Sum(p => (p.ProductShippingAdditionalFeePerItem * p.Quantity)).GetValueOrDefault(0);

                            optionCost = shipCostByLocation.Value + additionalShipCost;

                            shippingOptions.Add(new ShippingOption()
                            {
                                ProviderType = providerType,
                                Name = rateType.Name,
                                DisplayName = rateType.DisplayName,
                                Cost = optionCost
                            });
                        }
                    }
                }
            }

            shippingOptions.Sort((left,right) => left.Cost.Value.CompareTo(right.Cost.Value));
            return shippingOptions;
        }

    }
}