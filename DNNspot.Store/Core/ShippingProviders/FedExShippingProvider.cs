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
using System.IO;
using System.Linq;
using System.Web;
using DNNspot.Store.DataModel;
using WA.Extensions;
using WA.Shipping;
using WA.Shipping.FedEx;
using WA.Shipping.FedExRateService;
using WA.Shipping.FedExShipService;
using NotificationSeverityType = WA.Shipping.FedExRateService.NotificationSeverityType;
using ReturnedRateType = WA.Shipping.FedExRateService.ReturnedRateType;
using ServiceType = WA.Shipping.FedExRateService.ServiceType;
using ShippingDocumentImageType = WA.Shipping.FedExShipService.ShippingDocumentImageType;
using TrackingId = WA.Shipping.FedExShipService.TrackingId;

namespace DNNspot.Store.ShippingProviders
{
    public class FedExShippingProvider : ShippingProvider
    {
        FedExApi fedExApi;
        FedExApi fedExApiNoSmartPost = null;
        bool isSmartPostEnabled = false;

        public FedExShippingProvider(int storeId, ShippingProviderType providerType) : base(storeId, providerType)
        {
            bool isTestGateway = WA.Parser.ToBool(settings.TryGetValueOrEmpty("isTestGateway")).GetValueOrDefault(false);

            fedExApi = new FedExApi(
                settings.TryGetValueOrEmpty("apiKey"),
                settings.TryGetValueOrEmpty("apiPassword"),
                settings.TryGetValueOrEmpty("accountNumber"),
                settings.TryGetValueOrEmpty("meterNumber"),
                settings.TryGetValueOrEmpty("smartPostHubId"),
                isTestGateway
            );
            isSmartPostEnabled = !string.IsNullOrEmpty(settings.TryGetValueOrEmpty("smartPostHubId"));
            if(isSmartPostEnabled)
            {
                fedExApiNoSmartPost = new FedExApi(
                    settings.TryGetValueOrEmpty("apiKey"),
                    settings.TryGetValueOrEmpty("apiPassword"),
                    settings.TryGetValueOrEmpty("accountNumber"),
                    settings.TryGetValueOrEmpty("meterNumber"),
                    string.Empty,
                    isTestGateway
                );
            }
        }

        private string ServiceTypeToDisplayName(ServiceType serviceType)
        {
            return "FedEx - " + serviceType.ToString().Replace('_', ' ').ToLower().Replace("fedex", "").ToTitleCase();
        }

        public override List<ShippingOption> GetShippingOptions()
        {
            ErrorMessages.Clear();

            List<ServiceType> allServiceTypes = WA.Enum<ServiceType>.GetValues().ToList();            

            return allServiceTypes.ConvertAll(s => new ShippingOption()
                 {
                     ProviderType = providerType,
                     Name = s.ToString(),
                     DisplayName = ServiceTypeToDisplayName(s),
                     Cost = null
                 });
        }

        public override List<ShippingOption> GetShippingOptions(AddressInfo origin, AddressInfo destination)
        {            
            ErrorMessages.Clear();
            return GetShippingOptionEstimates(origin, destination, new List<vCartItemProductInfo>());
        }

        public override List<ShippingOption> GetShippingOptionEstimates(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts)
        {
            ErrorMessages.Clear();

            List<ShippingOption> options = new List<ShippingOption>();

            WA.Shipping.AddressInfo fedExOrigin = StoreAddressToFedExAddress(origin);

            WA.Shipping.AddressInfo fedExDestination = StoreAddressToFedExAddress(destination);
            if (string.IsNullOrEmpty(fedExDestination.CountryCode))
            {
                fedExDestination.CountryCode = origin.Country;
            }

            List<PackageInfo> fedExPackages = StoreCartToPackageList(cartProducts);            
            
            options.AddRange(GetAvailableRates(fedExApi, fedExOrigin, fedExDestination, fedExPackages));
            if(isSmartPostEnabled && fedExApiNoSmartPost != null)
            {
                // call FedEx API again WITHOUT the SmartPost Hub ID to append the regular rates too...
                options.AddRange(GetAvailableRates(fedExApiNoSmartPost, fedExOrigin, fedExDestination, fedExPackages));
            }
            options.Sort((left, right) => left.Cost.Value.CompareTo(right.Cost.Value));

            return options;
        }

        private List<ShippingOption> GetAvailableRates(FedExApi api, WA.Shipping.AddressInfo fedExOrigin, WA.Shipping.AddressInfo fedExDestination, List<PackageInfo> fedExPackages)
        {
            List<ShippingOption> options = new List<ShippingOption>();

            RateReply reply = api.GetAvailableRates(fedExOrigin, fedExDestination, fedExPackages);
            if (reply.HighestSeverity == NotificationSeverityType.SUCCESS || reply.HighestSeverity == NotificationSeverityType.NOTE) // || reply.HighestSeverity == NotificationSeverityType.WARNING) // check if the call was successful
            {
                if (reply.RateReplyDetails.Length > 0)
                {
                    foreach (RateReplyDetail rateDetail in reply.RateReplyDetails)
                    {
                        //Console.WriteLine("ServiceType: " + rateDetail.ServiceType);
                        foreach (RatedShipmentDetail shipmentDetail in rateDetail.RatedShipmentDetails)
                        {
                            //Console.WriteLine("RateType : " + shipmentDetail.ShipmentRateDetail.RateType);
                            //Console.WriteLine("Total Billing Weight : " + shipmentDetail.ShipmentRateDetail.TotalBillingWeight.Value);
                            //Console.WriteLine("Total Base Charge : " + shipmentDetail.ShipmentRateDetail.TotalBaseCharge.Amount);
                            //Console.WriteLine("Total Discount : " + shipmentDetail.ShipmentRateDetail.TotalFreightDiscounts.Amount);
                            //Console.WriteLine("Total Surcharges : " + shipmentDetail.ShipmentRateDetail.TotalSurcharges.Amount);
                            //Console.WriteLine("Net Charge : " + shipmentDetail.ShipmentRateDetail.TotalNetCharge.Amount);
                            //Console.WriteLine("*********");                                                        

                            if (shipmentDetail.ShipmentRateDetail.RateType == ReturnedRateType.PAYOR_ACCOUNT_PACKAGE)
                            {
                                options.Add(new ShippingOption()
                                {
                                    ProviderType = providerType,
                                    Name = rateDetail.ServiceType.ToString(),
                                    DisplayName = ServiceTypeToDisplayName(rateDetail.ServiceType),
                                    Cost = shipmentDetail.ShipmentRateDetail.TotalNetCharge.Amount
                                });
                            }
                        }
                        //if (rateDetail.DeliveryTimestampSpecified)
                        //{
                        //    Console.WriteLine("Delivery timestamp " + rateDetail.DeliveryTimestamp.ToString());
                        //}
                        //Console.WriteLine("Transit Time: " + rateDetail.TransitTime);
                    }
                }
            }
            else
            {
                foreach (var error in reply.Notifications)
                {
                    if (error.Severity == NotificationSeverityType.ERROR || error.Severity == NotificationSeverityType.FAILURE || error.Severity == NotificationSeverityType.WARNING)
                    {
                        ErrorMessages.Add(string.Format(@"Code: {0}, Error: {1}", error.Code, error.Message));
                    }
                }
            }
            
            return options;
        }

        public override decimal GetShippingOptionCost(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts, ShippingOption shippingOption)
        {
            ErrorMessages.Clear();

            // NOTE is there a better / faster way to get this?  maybe cache the previous results for same origin/destination/cart or something?

            var estimates = GetShippingOptionEstimates(origin, destination, cartProducts);
            var selectedOption = estimates.Find(x => x.Name == shippingOption.Name && x.ProviderType == shippingOption.ProviderType);
            if(selectedOption == null)
            {
                throw new ApplicationException(string.Format("Unable to find valid Shipping Option for '{0}'", shippingOption.Name));
            }
            return selectedOption.Cost.GetValueOrDefault(0);   
        }

        public override ProcessShipmentResult ProcessShipment(Order order, ShippingLabelType shippingLabelType)
        {
            ErrorMessages.Clear();

            ProcessShipmentResult result = new ProcessShipmentResult();

            FedExServiceType serviceType;
            if (!WA.Enum<FedExServiceType>.TryParse(order.ShippingServiceOption, out serviceType))
            {
                throw new ApplicationException(string.Format("Unable to determine FedEx service type for '{0}'", order.ShippingServiceOption));
            }

            ContactInfo sender = GetSender();

            ContactInfo recipient = new ContactInfo()
            {
                FirstName = order.CustomerFirstName,
                LastName = order.CustomerLastName,
                Email = order.CustomerEmail,
                Phone = order.ShipTelephone,
                CompanyName = order.ShipRecipientBusinessName,
                Address = new WA.Shipping.AddressInfo()
                {
                    StreetLines = new List<string>() { order.ShipAddress1 },
                    City = order.ShipCity,
                    RegionCode = order.ShipRegion,
                    PostalCode = order.ShipPostalCode,
                    CountryCode = order.ShipCountryCode
                }
            };            
            if (!string.IsNullOrEmpty(order.ShipAddress2))
            {
                recipient.Address.StreetLines.Add(order.ShipAddress2);
            }

            List<PackageInfo> packages = StoreOrderItemsToPackageList(order.OrderItemCollectionByOrderId).ToList();

            ShippingDocumentImageType fedexLabelType = ShippingDocumentImageType.PDF;
            switch(shippingLabelType)
            {
                case ShippingLabelType.PNG:
                    fedexLabelType = ShippingDocumentImageType.PNG;
                    break;
                case ShippingLabelType.PDF:
                    fedexLabelType = ShippingDocumentImageType.PDF;
                    break;
            }


            ProcessShipmentReply reply = fedExApi.ProcessShipment(sender, recipient, packages, serviceType, fedexLabelType);
            if ((reply.HighestSeverity != WA.Shipping.FedExShipService.NotificationSeverityType.ERROR) && (reply.HighestSeverity != WA.Shipping.FedExShipService.NotificationSeverityType.FAILURE))
            {
                List<TrackingId> trackingIds = new List<TrackingId>();
                foreach (CompletedPackageDetail pkg in reply.CompletedShipmentDetail.CompletedPackageDetails)
                {
                    //---- Tracking #'s
                    trackingIds.AddRange(pkg.TrackingIds);
                    //foreach (WA.Shipping.FedExShipService.TrackingId trackingId in pkg.TrackingIds)
                    //{
                    //    //Console.WriteLine(string.Format(@"Tracking #: {0}", trackingId.TrackingNumber));                        
                    //    trackingNums.Add(trackingId.TrackingNumber);
                    //}

                    //---- Package Info/Weights
                    //foreach (WA.Shipping.FedExShipService.PackageRateDetail ratedPackage in pkg.PackageRating.PackageRateDetails)
                    //{
                    //    Console.WriteLine("\nRate details");
                    //    Console.WriteLine("\nRate Type: " + ratedPackage.RateType);
                    //    if (ratedPackage.BillingWeight != null)
                    //        Console.WriteLine("Billing weight {0} {1}", ratedPackage.BillingWeight.Value, ratedPackage.BillingWeight.Units);
                    //    if (ratedPackage.BaseCharge != null)
                    //        Console.WriteLine("Base charge {0} {1}", ratedPackage.BaseCharge.Amount, ratedPackage.BaseCharge.Currency);
                    //    if (ratedPackage.NetCharge != null)
                    //        Console.WriteLine("Net charge {0} {1}", ratedPackage.NetCharge.Amount, ratedPackage.NetCharge.Currency);
                    //    if (ratedPackage.Surcharges != null)
                    //    {
                    //        // Individual surcharge for each package
                    //        foreach (WA.Shipping.FedExShipService.Surcharge surcharge in ratedPackage.Surcharges)
                    //        {
                    //            Console.WriteLine("{0} surcharge {1} {2}", surcharge.SurchargeType, surcharge.Amount.Amount, surcharge.Amount.Currency);
                    //        }
                    //    }
                    //    if (ratedPackage.TotalSurcharges != null)
                    //    {
                    //        Console.WriteLine("Total surcharge {0} {1}", ratedPackage.TotalSurcharges.Amount, ratedPackage.TotalSurcharges.Currency);
                    //    }
                    //}

                    //---- Route / Transit Details
                    //Console.WriteLine("\nRouting details");
                    //Console.WriteLine("URSA prefix {0} suffix {1}", reply.CompletedShipmentDetail.RoutingDetail.UrsaPrefixCode, reply.CompletedShipmentDetail.RoutingDetail.UrsaSuffixCode);
                    //Console.WriteLine("Service commitment {0} Airport ID {1}", reply.CompletedShipmentDetail.RoutingDetail.DestinationLocationId, reply.CompletedShipmentDetail.RoutingDetail.AirportId);

                    //if (reply.CompletedShipmentDetail.RoutingDetail.DeliveryDaySpecified)
                    //{
                    //    Console.WriteLine("Delivery day " + reply.CompletedShipmentDetail.RoutingDetail.DeliveryDay);
                    //}
                    //if (reply.CompletedShipmentDetail.RoutingDetail.DeliveryDateSpecified)
                    //{
                    //    Console.WriteLine("Delivery date " + reply.CompletedShipmentDetail.RoutingDetail.DeliveryDate.ToShortDateString());
                    //}
                    //Console.WriteLine("Transit time " + reply.CompletedShipmentDetail.RoutingDetail.TransitTime);

                    //---- Label
                    //WA.Shipping.FedExShipService.CompletedShipmentDetail completedShipmentDetail = reply.CompletedShipmentDetail;
                    if (pkg.Label.Parts[0].Image != null)
                    {
                        result.ShippingLabelBytes = pkg.Label.Parts[0].Image;
                        result.LabelType = WA.Enum<ShippingLabelType>.TryParseOrDefault(fedexLabelType.ToString(), ShippingLabelType.PDF);

                        //string labelFilename = string.Format(@"{0}.pdf", pkg.TrackingIds[0].TrackingNumber);

                        // Save label buffer to file
                        //using (FileStream labelFile = new FileStream(labelFilename, FileMode.Create))
                        //{
                        //    labelFile.Write(pkg.Label.Parts[0].Image, 0, pkg.Label.Parts[0].Image.Length);
                        //    labelFile.Close();
                        //}
                        //Console.WriteLine(@"Label file written to ""{0}""", labelFilename);
                    }
                }
                if(trackingIds.Count > 0)
                {
                    result.TrackingNumber = trackingIds.Select(t => string.Format("{0}: {1}", t.TrackingIdType, t.TrackingNumber)).ToList().ToDelimitedString(", ");
                }
                else
                {
                    result.TrackingNumber = trackingIds.Select(t => t.TrackingNumber).First();
                }
                
                result.Success = true;
            }
            else
            {                
                foreach (var error in reply.Notifications)
                {
                    if (error.Severity == WA.Shipping.FedExShipService.NotificationSeverityType.ERROR || error.Severity == WA.Shipping.FedExShipService.NotificationSeverityType.FAILURE)
                    {
                        ErrorMessages.Add(string.Format(@"Code: {0}, Error: {1}", error.Code, error.Message));
                    }
                }

                result.ErrorMessages.AddRange(ErrorMessages);
            }

            return result;
        }

        private WA.Shipping.AddressInfo StoreAddressToFedExAddress(AddressInfo address)
        {
            var a = new WA.Shipping.AddressInfo()
                       {
                           City = address.City,
                           CountryCode = address.Country,
                           PostalCode = address.PostalCode,
                           RegionCode = address.Region,                           
                           StreetLines = new List<string>()
                       };
            if (!string.IsNullOrEmpty(address.BusinessName))
            {
                a.StreetLines.Add(address.BusinessName);
            }
            a.StreetLines.Add(address.Address1);
            if(!string.IsNullOrEmpty(address.Address2))
            {
                a.StreetLines.Add(address.Address2);
            }
            return a;
        }

        private List<PackageInfo> StoreCartToPackageList(List<vCartItemProductInfo> cartProducts)
        {
            List<PackageInfo> packages = new List<PackageInfo>();

            int i = 1;
            foreach(var cartItem in cartProducts)
            {
                decimal itemWeight = cartItem.ProductWeight.GetValueOrDefault(0);
                decimal packageWeight = cartItem.Quantity.Value * itemWeight;
                packageWeight = Math.Max(packageWeight, 1); // FedEx requires a minimum weight of 1 lb.

                packages.Add(new PackageInfo()
                {
                    SequenceNumber = i.ToString(),
                    WeightUnit = WeightUnit.LB,
                    Weight = packageWeight,
                    Length = 1, // TODO real dimensions
                    Width = 1,
                    Height = 1,
                    DimensionUnit = LinearUnit.IN
                });  
                i++;
            }

            return packages;
        }

        private IList<PackageInfo> StoreOrderItemsToPackageList(IList<OrderItem> orderItems)
        {
            List<PackageInfo> packages = new List<PackageInfo>();

            int i = 1;
            foreach (var orderItem in orderItems)
            {
                packages.Add(new PackageInfo()
                {
                    SequenceNumber = i.ToString(),
                    WeightUnit = WeightUnit.LB,
                    Weight = Math.Max(orderItem.WeightTotal.GetValueOrDefault(0), 1),
                    Length = 1, // TODO real dimensions
                    Width = 1,
                    Height = 1,
                    DimensionUnit = LinearUnit.IN
                });
                i++;
            }

            return packages;            
        }

        private WA.Shipping.ContactInfo GetSender()
        {
            var store = DataModel.Store.GetStore(storeId);
            AddressInfo storeAddress = store.Address;

            return new WA.Shipping.ContactInfo()
                              {
                                  CompanyName = store.Name,
                                  Address = new WA.Shipping.AddressInfo()
                                  {
                                      StreetLines = new List<string>() { storeAddress.Address1 },
                                      City = storeAddress.City,
                                      RegionCode = storeAddress.Region,
                                      PostalCode = storeAddress.PostalCode,
                                      CountryCode = storeAddress.Country,
                                  },
                                  Phone = store.GetSetting(StoreSettingNames.StorePhoneNumber),
                              };
        }
    }
}
