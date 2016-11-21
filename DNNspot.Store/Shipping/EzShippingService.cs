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
using DNNspot.Store.DataModel;
using nsoftware.InShip;
using WA.Extensions;

namespace DNNspot.Store.Shipping
{
    public abstract class EzShippingService : ShippingService, IShippingService
    {
        protected Ezrates ezrater;
        protected Ezship ezship;
        protected bool isTestGateway;
        protected abstract EzAccountServerUrls ServiceUrls { get; }
        protected CultureInfo UnitedStatesCulture;

        protected EzShippingService(int storeId, ShippingProviderType providerType)
            : base(storeId, providerType)
        {
            Init();
        }

        protected EzShippingService(int storeId, ShippingProviderType providerType, int? orderId, Guid? cartId)
            : base(storeId, providerType, orderId, cartId)
        {
            Init();
        }

        private void Init()
        {
            isTestGateway = WA.Parser.ToBool(config.TryGetValueOrEmpty("isTestGateway")).GetValueOrDefault(false);
            UnitedStatesCulture = CultureInfo.CreateSpecificCulture("en-US");

            ezrater = new Ezrates();
            ezship = new Ezship();

            if (providerType == ShippingProviderType.FedEx)
            {
                ezrater.Provider = EzratesProviders.pFedEx;
                ezship.Provider = EzshipProviders.pFedEx;
                //DataModel.ShippingService fedExService = DataModel.ShippingService.FindOrCreateNew(storeId, ShippingProviderType.FedEx);
                //Dictionary<string, string> settings = fedExService.GetSettingsDictionary();
                //ezship.Config("LabelStockType=" + settings.TryGetValueOrDefault("labelStockType", "0")); 
            }
            else if (providerType == ShippingProviderType.UPS)
            {
                ezrater.Provider = EzratesProviders.pUPS;
                ezship.Provider = EzshipProviders.pUPS;
            }
        }



        public IList<IShippingRate> GetRates(IPostalAddress senderAddress, IPostalAddress recipientAddress, IList<IShipmentPackageDetail> packageDetails)
        {
            // return rates for ALL service types
            return GetRates(senderAddress, recipientAddress, packageDetails, ServiceTypes.stUnspecified);
        }

        public IShippingRate GetRate(IPostalAddress senderAddress, IPostalAddress recipientAddress, IList<IShipmentPackageDetail> packageDetails, string serviceType)
        {
            ServiceTypes serviceTypeEnum;
            if (WA.Enum<ServiceTypes>.TryParse(serviceType, out serviceTypeEnum))
            {
                var rates = GetRates(senderAddress, recipientAddress, packageDetails, serviceTypeEnum);

                return rates[0];
            }
            else
            {
                throw new ArgumentException("Unknown Shipping ServiceType argument", "serviceType");
            }
        }

        private IList<IShippingRate> GetRates(IPostalAddress senderAddress, IPostalAddress recipientAddress, IList<IShipmentPackageDetail> packageDetails, ServiceTypes serviceType)
        {
            ezrater.Account.Server = ServiceUrls.RateUrl;

            ezrater.RequestedService = serviceType;

            SetSenderAddress(senderAddress);
            SetRecipientAddress(recipientAddress);

            foreach (var package in packageDetails)
            {
                ezrater.Packages.Add(new PackageDetail()
                {
                    Weight = Math.Max(package.Weight, 1).ToString("F1", UnitedStatesCulture),
                    Length = (int)Math.Round(Math.Max(package.Length, 1)),
                    Width = (int)Math.Round(Math.Max(package.Width, 1)),
                    Height = (int)Math.Round(Math.Max(package.Height, 1)),
                    PackagingType = TPackagingTypes.ptYourPackaging
                });

            }
            ezrater.TotalWeight = packageDetails.Sum(p => Math.Max(1, p.Weight)).ToString("F1", UnitedStatesCulture);

            try
            {
                ezrater.GetRates();
                var shippingLog = new ShippingLog
                                      {
                                          RequestSent = ezrater.Config("FullRequest"),
                                          ShippingRequestType = "Rates",
                                          ResponseReceived = ezrater.Config("FullResponse"),
                                          CartId = cartId,
                                          OrderId = orderId
                                      };
                shippingLog.Save();
            }
            catch (InShipEzratesException ratesEx)
            {
                ShippingLog shippingLog = new ShippingLog
                                              {
                                                  RequestSent = ezrater.Config("FullRequest"),
                                                  ShippingRequestType = "Request",
                                                  ResponseReceived = ezrater.Config("FullResponse"),
                                                  CartId = cartId,
                                                  OrderId = orderId
                                              };
                shippingLog.Save();

                try
                {
                    if (recipientAddress.IsResidential)
                    {
                        ezship.RecipientAddress.AddressFlags = 0x00000002; // Residential
                    }
                    ezrater.GetRates();

                }
                catch (InShipEzratesException ratesEx2)
                {
                    throw new InShipEzratesException("Ezrates Exception. Full Request = " + ezrater.Config("FullRequest") + " - FullResponse = " + ezrater.Config("FullResponse"), ratesEx2);

                }
            }

            decimal additionalHandlingFee = packageDetails.Sum(p => p.AdditionalHandlingFee);


            var shippingRates = new List<IShippingRate>();
            foreach (ServiceDetail serviceDetail in ezrater.Services)
            {
                var rate = ConvertServiceDetailToShippingRate(serviceDetail);
                rate.Rate = rate.Rate + additionalHandlingFee;
                shippingRates.Add(rate);
            }
            shippingRates.Sort((left, right) => left.Rate.CompareTo(right.Rate));

            return shippingRates;
        }

        public ShipmentLabelResponse GetShipmentLabels(ShipmentLabelRequest shipmentLabelRequest)
        {
            ezship.Account.Server = ServiceUrls.ShipUrl;
            ezship.LabelImageType = (ezship.Provider == EzshipProviders.pFedEx) ? EzshipLabelImageTypes.itPNG : EzshipLabelImageTypes.itGIF;

            ServiceTypes serviceType;
            WA.Enum<ServiceTypes>.TryParse(shipmentLabelRequest.ServiceType, out serviceType);
            ezship.ServiceType = serviceType;

            //---- SENDER
            var senderContact = shipmentLabelRequest.SenderContact;
            ezship.SenderContact.FirstName = senderContact.FirstName;
            ezship.SenderContact.LastName = senderContact.LastName;
            ezship.SenderContact.Phone = senderContact.Phone;
            ezship.SenderContact.Company = senderContact.CompanyName;

            var senderAddress = shipmentLabelRequest.SenderAddress;
            if (!string.IsNullOrEmpty(senderAddress.Address1))
                ezship.SenderAddress.Address1 = senderAddress.Address1;

            if (!string.IsNullOrEmpty(senderAddress.Address2))
                ezship.SenderAddress.Address2 = senderAddress.Address2;

            if (!string.IsNullOrEmpty(senderAddress.City))
                ezship.SenderAddress.City = senderAddress.City;

            if (!string.IsNullOrEmpty(senderAddress.Region))
                ezship.SenderAddress.State = senderAddress.Region;

            if (!string.IsNullOrEmpty(senderAddress.PostalCode))
                ezship.SenderAddress.ZipCode = senderAddress.PostalCode;

            if (!string.IsNullOrEmpty(senderAddress.CountryCode))
                ezship.SenderAddress.CountryCode = senderAddress.CountryCode;

            //---- RECIPIENT
            var recipientContact = shipmentLabelRequest.RecipientContact;
            ezship.RecipientContact.FirstName = recipientContact.FirstName;
            ezship.RecipientContact.LastName = recipientContact.LastName;
            ezship.RecipientContact.Phone = recipientContact.Phone;
            // FedEx and UPS both require the 'Company' field for the recipient, so we'll set it to LastName if it's not already set
            ezship.RecipientContact.Company = string.IsNullOrEmpty(recipientContact.CompanyName) ? recipientContact.LastName : recipientContact.CompanyName;

            var recipientAddress = shipmentLabelRequest.RecipientAddress;
            if (recipientAddress.IsResidential)
            {
                ezship.RecipientAddress.AddressFlags = 0x00000002; // Residential
            }
            else
            {
                ezship.RecipientAddress.AddressFlags = 0; // not set (defaults to Commercial)
            }
            if (!string.IsNullOrEmpty(recipientAddress.Address1))
                ezship.RecipientAddress.Address1 = recipientAddress.Address1;

            if (!string.IsNullOrEmpty(recipientAddress.Address2))
                ezship.RecipientAddress.Address2 = recipientAddress.Address2;

            if (!string.IsNullOrEmpty(recipientAddress.City))
                ezship.RecipientAddress.City = recipientAddress.City;

            if (!string.IsNullOrEmpty(recipientAddress.Region))
                ezship.RecipientAddress.State = recipientAddress.Region;

            if (!string.IsNullOrEmpty(recipientAddress.PostalCode))
                ezship.RecipientAddress.ZipCode = recipientAddress.PostalCode;

            if (!string.IsNullOrEmpty(recipientAddress.CountryCode))
                ezship.RecipientAddress.CountryCode = recipientAddress.CountryCode;

            //EzshipLabelImageTypes labelImageType = new EzshipLabelImageTypes();

            //if(ezship.Provider == EzshipProviders.pFedEx)
            //{
            //    var fedExService = DataModel.ShippingService.FindOrCreateNew(storeId, ShippingProviderType.FedEx);
            //    Dictionary<string, string> fedexSettings = fedExService.GetSettingsDictionary();
            //    var labelImageTypeSetting = fedexSettings.TryGetValueOrDefault("labelImageType", "itGIF");
            //     labelImageType = WA.Enum<EzshipLabelImageTypes>.Parse(labelImageTypeSetting);
            //}
            //else
            //{
            //    ezship.LabelImageType = EzshipLabelImageTypes.itGIF;
            //}


            LabelFileType labelFileType = (ezship.Provider == EzshipProviders.pFedEx) ? LabelFileType.Png : LabelFileType.Gif;

            if (ezship.Provider == EzshipProviders.pUPS)
            {
                ezship.LabelImageType = EzshipLabelImageTypes.itGIF;
            }

            const TPackagingTypes packagingType = TPackagingTypes.ptYourPackaging;

            foreach (var packageDetail in shipmentLabelRequest.Packages)
            {
                ezship.Packages.Add(new PackageDetail()
                                        {
                                            PackagingType = packagingType,
                                            Weight = Math.Max(1, packageDetail.Weight).ToString("F1", UnitedStatesCulture),
                                            Length = Convert.ToInt32(Math.Max(1, packageDetail.Length)),
                                            Width = Convert.ToInt32(Math.Max(1, packageDetail.Width)),
                                            Height = Convert.ToInt32(Math.Max(1, packageDetail.Height)),
                                            Reference = string.Format(@"PO:{0}", packageDetail.ReferenceCode)
                                        });
            }

            try
            {
                ezship.GetShipmentLabels();
            }
            catch (InShipEzshipException ex)
            {
                ezship.RecipientAddress.AddressFlags = 0;
                ezship.GetShipmentLabels();
            }

            var shippingLog = new ShippingLog
            {
                RequestSent = ezrater.Config("FullRequest"),
                ShippingRequestType = "ShippingLabels",
                ResponseReceived = ezrater.Config("FullResponse"),
                CartId = cartId,
                OrderId = orderId
            };
            shippingLog.Save();


            var response = new ShipmentLabelResponse();

            response.MasterTrackingNumber = ezship.MasterTrackingNumber;
            bool copyPackageDetails = (ezship.Packages.Count == shipmentLabelRequest.Packages.Count);
            for (int i = 0; i < ezship.Packages.Count; i++)
            {
                var package = ezship.Packages[i];
                response.Labels.Add(new ShipmentLabel()
                                        {
                                            TrackingNumber = package.TrackingNumber,
                                            LabelFile = package.ShippingLabelB,
                                            LabelFileType = labelFileType,

                                            PackageDetail = copyPackageDetails ? shipmentLabelRequest.Packages[i] : null
                                        });
            }

            return response;

            //if (ezship1.Provider == EzshipProviders.pFedEx)
            //{
            //    ezship1.LabelImageType = EzshipLabelImageTypes.itPNG;
            //    ezship1.Packages[0].ShippingLabelFile = "test_label.png";
            //}
            //else
            //{
            //    ezship1.LabelImageType = EzshipLabelImageTypes.itGIF;
            //    ezship1.Packages[0].ShippingLabelFile = "test_label.gif";
            //}

            //ezship1.GetShipmentLabels();

            //lblLabelFile.Text = ezship1.Packages[0].ShippingLabelFile;
            //lblTrackingNumber.Text = ezship1.Packages[0].TrackingNumber;
        }

        protected virtual void SetSenderAddress(IPostalAddress senderAddress)
        {
            // SENDER
            if (!string.IsNullOrEmpty(senderAddress.Address1))
                ezrater.SenderAddress.Address1 = senderAddress.Address1;

            if (!string.IsNullOrEmpty(senderAddress.Address2))
                ezrater.SenderAddress.Address2 = senderAddress.Address2;

            if (!string.IsNullOrEmpty(senderAddress.City))
                ezrater.SenderAddress.City = senderAddress.City;

            if (!string.IsNullOrEmpty(senderAddress.Region))
                ezrater.SenderAddress.State = senderAddress.Region;

            if (!string.IsNullOrEmpty(senderAddress.PostalCode))
                ezrater.SenderAddress.ZipCode = senderAddress.PostalCode;

            if (!string.IsNullOrEmpty(senderAddress.CountryCode))
                ezrater.SenderAddress.CountryCode = senderAddress.CountryCode;
        }

        protected virtual void SetRecipientAddress(IPostalAddress recipientAddress)
        {
            // RECIPIENT
            if (recipientAddress.IsResidential)
            {
                // Per DOCS: Residential - Whether or not the address is a residential address.
                // This flag is only relevant for UPS and FedEx, and if this flag is not set, the address is assumed to be commercial. 
                ezrater.RecipientAddress.AddressFlags = 0x00000002;
            }
            else
            {
                ezrater.RecipientAddress.AddressFlags = 0;
            }
            if (!string.IsNullOrEmpty(recipientAddress.Address1))
                ezrater.RecipientAddress.Address1 = recipientAddress.Address1;

            if (!string.IsNullOrEmpty(recipientAddress.Address2))
                ezrater.RecipientAddress.Address2 = recipientAddress.Address2;

            if (!string.IsNullOrEmpty(recipientAddress.City))
                ezrater.RecipientAddress.City = recipientAddress.City;

            if (!string.IsNullOrEmpty(recipientAddress.Region))
                ezrater.RecipientAddress.State = recipientAddress.Region;

            if (!string.IsNullOrEmpty(recipientAddress.PostalCode))
                ezrater.RecipientAddress.ZipCode = recipientAddress.PostalCode;

            if (!string.IsNullOrEmpty(recipientAddress.CountryCode))
                ezrater.RecipientAddress.CountryCode = recipientAddress.CountryCode;
        }

        protected virtual IShippingRate ConvertServiceDetailToShippingRate(ServiceDetail rate)
        {
            // check if there's a "negotiated rate"
            decimal cost = !string.IsNullOrEmpty(rate.AccountNetCharge) ? Convert.ToDecimal(rate.AccountNetCharge) : Convert.ToDecimal(rate.ListNetCharge);

            return new ShippingRate()
            {
                ServiceType = rate.ServiceType.ToString(),
                ServiceTypeDescription = rate.ServiceTypeDescription,
                Rate = cost
            };
        }
    }
}