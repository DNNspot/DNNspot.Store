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
using System.Web.Services;
using System.Web.SessionState;
using DNNspot.Store.DataModel;
using DNNspot.Store.Shipping;
using DotNetNuke.Common.Utilities;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class AjaxHandler : IHttpHandler, IReadOnlySessionState
    {
        HttpResponse response;
        HttpRequest request;

        public void ProcessRequest(HttpContext context)
        {
            response = context.Response;
            request = context.Request;

            //response.ContentType = "text/plain";
            response.ContentType = "application/json"; // jQuery 1.4.2 requires this now!

            try
            {
                StoreContext storeContext = new StoreContext(request);      
                StoreUrls storeUrls = new StoreUrls(storeContext);

                string action = request.Params["action"];
                int? productId = WA.Parser.ToInt(request.Params["productId"]);
                Product product = null;
                if(productId.HasValue)
                {
                    product = Product.GetProduct(productId.Value);
                }

                switch (action)
                {
                    case "updateShippingRateTypeName":
                        int? shippingRateTypeId = WA.Parser.ToInt(request.Params["shippingServiceRateTypeId"]);
                        if (shippingRateTypeId.HasValue)
                        {
                            string shippingName = request.Params["name"];
                            DataModel.ShippingServiceRateType shippingMethod = DataModel.ShippingServiceRateType.Get(shippingRateTypeId.Value);
                            if (shippingMethod != null)
                            {
                                shippingMethod.DisplayName = shippingName;
                                shippingMethod.Save();

                                RespondWithSuccess();
                            }
                        }
                        break;
                    case "getShippingRatesJson":
                        DataModel.ShippingServiceRateType rateType = DataModel.ShippingServiceRateType.Get(WA.Parser.ToShort(request.Params["shippingServiceRateTypeId"]).GetValueOrDefault(-1));
                        if (rateType != null)
                        {
                            List<ShippingServiceRate> rates = rateType.GetRates();
                            List<JsonShippingServiceRate> jsonRates = rates.ConvertAll(
                                    r =>
                                        new JsonShippingServiceRate()
                                            {
                                                RateTypeId = rateType.Id.Value,
                                                CountryCode = r.CountryCode,
                                                Region = r.Region,
                                                MinWeight = r.WeightMin,
                                                MaxWeight = r.WeightMax,
                                                Cost = r.Cost
                                            }
                                );
                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonRates);
                            response.Write(json);
                        }                        
                        break;
                    //case "applyShippingRate":
                    //    string shippingRate = request.Params["shippingServiceRateTypeId"];
                    //    if (shippingRate != null)
                    //    {

                    //        var store = storeContext.CurrentStore;
                            
                    //        var checkoutOrderInfo = HttpContext.Current.Session[storeContext.SessionKeys.CheckoutOrderInfo] as CheckoutOrderInfo;

                    //        string[] rateParts = shippingRate.Split("||");
                    //        checkoutOrderInfo.ShippingRate.ServiceType = rateParts[0];
                    //        checkoutOrderInfo.ShippingRate.ServiceTypeDescription = rateParts[1];
                    //        checkoutOrderInfo.ShippingRate.Rate = Convert.ToDecimal(rateParts.Length == 3 ? rateParts[2] : rateParts[1]);
                    //        checkoutOrderInfo.ReCalculateOrderTotals();

                    //        Session[StoreContext.SessionKeys.CheckoutOrderInfo] = checkoutOrderInfo;
                    //    }
                    //    break;
                    case "getShippingOptionEstimatesJson":

                        string address1 = request.Params["address1"];
                        string address2 = request.Params["address2"];
                        string city = request.Params["city"];
                        string country = request.Params["country"];
                        string region = request.Params["region"];
                        string postalCode = request.Params["postalCode"];
                        bool isBusiness = Convert.ToBoolean(request.Params["isBusiness"]);

                        var store = storeContext.CurrentStore;
                       
                        var cartController = new CartController(storeContext);                        
                        var checkoutOrderInfo = HttpContext.Current.Session[storeContext.SessionKeys.CheckoutOrderInfo] as CheckoutOrderInfo;
                        if (checkoutOrderInfo == null)
                        {
                            var cart = cartController.GetCart(false);
                            checkoutOrderInfo = new CheckoutOrderInfo() { Cart = cart }; 
                        }
                        //List<string> errors;
                        //var shippingOptionEstimates = store.GetShippingOptionEstimates(origin, destination, checkoutOrderInfo.Cart.GetCartItemsWithProductInfo(), out errors);
                        //var shippingOptions = shippingOptionEstimates.ConvertAll(x => new
                        //                        {
                        //                            Value = x.Cost.GetValueOrDefault(0).ToString("F2"),
                        //                            Name = x.DisplayName,
                        //                            Text = string.Format(@"{0}  :  {1}", x.DisplayName, store.FormatCurrency(x.Cost))
                        //                        });

                        IPostalAddress origin = store.Address.ToPostalAddress();
                        IPostalAddress destination = new PostalAddress() { Address1 = address1, Address2 = address2, City = city, CountryCode = country, Region = region.ToUpper(), PostalCode = postalCode.ToUpper(), IsResidential = !isBusiness };

                        ShipmentPackagingStrategy shipmentPackagingStrategy = WA.Enum<ShipmentPackagingStrategy>.TryParseOrDefault(store.GetSetting(StoreSettingNames.ShipmentPackagingStrategy), ShipmentPackagingStrategy.SingleBox);

                        var shipmentPackages = checkoutOrderInfo.Cart.GetCartItemsAsShipmentPackages(shipmentPackagingStrategy);
                        var shippingRates = new List<IShippingRate>();
                        var shippingServices = store.GetEnabledShippingProviders(null, checkoutOrderInfo.Cart.Id);
                        foreach(var shipper in shippingServices)
                        {
                           shippingRates.AddRange(shipper.GetRates(origin, destination, shipmentPackages));
                        }
                        var shippingOptions = shippingRates.Select(x => new
                                                {
                                                    Value = x.Rate.ToString("F2"),
                                                    Name = x.ServiceType,
                                                    Text = string.Format(@"{0}  :  {1}", x.ServiceType, store.FormatCurrency(x.Rate))
                                                });

                        string shippingOptionsJson = Newtonsoft.Json.JsonConvert.SerializeObject(shippingOptions);
                        response.Write(shippingOptionsJson);
                        break;
                    case "getProductPhotosJson":
                        if (productId.HasValue)
                        {
                            List<ProductPhoto> productPhotos = Product.GetAllPhotosInSortOrder(productId.Value);
                            List<JsonPhoto> jsonProductPhotos =
                                productPhotos.ConvertAll(
                                    p =>
                                    new JsonPhoto()
                                        {
                                            Id = p.Id.Value,
                                            OriginalUrl = storeUrls.ProductPhoto(p, null, null),
                                            ThumbnailUrl = storeUrls.ProductPhoto(p, 120, 90)
                                        });
                            string jsonPhotos = Newtonsoft.Json.JsonConvert.SerializeObject(jsonProductPhotos);

                            response.Write(jsonPhotos);
                        }
                        break;
                    case "deleteProductPhoto":
                        int? photoId = WA.Parser.ToInt(request.Params["photoId"]);
                        if (ProductPhoto.DeletePhoto(photoId.GetValueOrDefault(-1)))
                        {                            
                            RespondWithSuccess();
                        }
                        else
                        {
                            RespondWithError("unable to delete photo: " + photoId);
                        }
                        break;
                    case "updateProductPhotoSortOrder":
                        if (productId.HasValue && request.Params["photoList[]"] != null)
                        {
                            List<string> sortedPhotoList = new List<string>(request.Params.GetValues("photoList[]"));
                            List<int> sortedPhotoIds = sortedPhotoList.ConvertAll(p => Convert.ToInt32(p.Split('-')[1]));

                            ProductPhotoQuery q = new ProductPhotoQuery();
                            q.Where(q.ProductId == productId.Value);
                            ProductPhotoCollection photos = new ProductPhotoCollection();
                            if (photos.Load(q))
                            {
                                foreach (ProductPhoto photo in photos)
                                {
                                    photo.SortOrder = (short) sortedPhotoIds.IndexOf(photo.Id.Value);
                                }
                                photos.Save();
                            }
                            RespondWithSuccess();
                        }
                        break;
                    case "getProductCustomFieldsJson":
                        if(product != null)
                        {
                            List<ProductField> productFields = product.GetProductFieldsInSortOrder();
                            List<JsonProductField> jsonProductFields = productFields.ConvertAll(
                                f => new JsonProductField()
                                     {
                                         Id = f.Id.Value,
                                         WidgetType = f.WidgetType,
                                         Name = f.Name,
                                         Slug = f.Slug,
                                         IsRequired = f.IsRequired.GetValueOrDefault(),
                                         PriceAdjustment = f.PriceAdjustment.GetValueOrDefault(0),
                                         WeightAdjustment = f.WeightAdjustment.GetValueOrDefault(0),
                                         SortOrder = f.SortOrder.GetValueOrDefault(),
                                         Choices = f.GetChoicesInSortOrder().ToList().ConvertAll(
                                            c => new JsonProductFieldChoice()
                                                 {
                                                     Id = c.Id.Value,
                                                     ProductFieldId = f.Id.Value,
                                                     Name = c.Name,
                                                     PriceAdjustment = c.PriceAdjustment.GetValueOrDefault(0),
                                                     WeightAdjustment = c.WeightAdjustment.GetValueOrDefault(0),
                                                     SortOrder = c.SortOrder.GetValueOrDefault()
                                                 })
                                     });
                            string jsonCustomFields = Newtonsoft.Json.JsonConvert.SerializeObject(jsonProductFields);

                            response.Write(jsonCustomFields);
                        }
                        break;
                    case "updateCategorySortOrder":                        
                        string[] sortedCatIdStrings = request.Params.GetValues("sortedCategoryIds");
                        if (sortedCatIdStrings != null)
                        {
                            List<int> sortedCatIds = new List<string>(sortedCatIdStrings).ConvertAll(s => WA.Parser.ToInt(s).GetValueOrDefault(-1));
                            CategoryCollection.SetSortOrderByListPosition(sortedCatIds);
                            CacheHelper.ClearCache();
                            RespondWithSuccess();
                        }
                        else
                        {
                            RespondWithError("no category ids found to update sort order");
                        }
                        break;
                    case "updateProductFieldSortOrder":
                        var sortedProductFieldIdStrings = request.Params.GetValues("sortedProductFieldIds[]");
                        if (sortedProductFieldIdStrings != null)
                        {                            
                            List<int> sortedProductFieldIds = new List<string>(sortedProductFieldIdStrings).ConvertAll(s => WA.Parser.ToInt(s).GetValueOrDefault(-1));                            
                            ProductFieldCollection.SetSortOrderByListPosition(sortedProductFieldIds);
                            RespondWithSuccess();
                        }
                        else
                        {
                            RespondWithError("no product field ids found to update sort order");
                        }
                        break;
                    default:
                        RespondWithError("unknown action");
                        break;
                }

                //response.Write("{ success: true }");
            }
            catch(Exception ex)
            {
                RespondWithError(ex.Message + " Stack Trace:" + ex.StackTrace);
            }

            response.Flush();
        }        

        private void RespondWithSuccess()
        {
            response.Write(@"{ ""success"": ""true"" }");
        }

        private void RespondWithError(string errorMsg)
        {
            response.Write(string.Format(@"{{ ""success"": ""false"", ""error"": ""{0}"" }}", errorMsg.Replace("'", "\'")));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
