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
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DNNspot.Store.Shipping;
using DotNetNuke.Services.Exceptions;
using WA.Geocoding;
using WA.Geocoding.Google;
using PaymentProvider = DNNspot.Store.DataModel.PaymentProvider;
using WA.Extensions;

namespace DNNspot.Store.Modules.Cart
{
    public partial class Cart : StoreModuleBase
    {
        protected CartController cartController;
        protected CheckoutOrderInfo checkoutOrderInfo;
        protected const string cartItemQtyInputNamePrefix = "cartItemQty-";
        protected bool collectPayPalStandardShipping = false;
        protected string errors;

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            RegisterJavascriptFileOnceInBody("js/jquery.fancybox-1.2.5.min.js", ModuleRootWebPath + "js/jquery.fancybox-1.2.5.min.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadResourceFileSettings();

            cartController = new CartController(StoreContext);

            checkoutOrderInfo = Session[StoreContext.SessionKeys.CheckoutOrderInfo] as CheckoutOrderInfo ?? new CheckoutOrderInfo() { Cart = cartController.GetCart(true) };
            checkoutOrderInfo.Cart = cartController.GetCart(true);
            checkoutOrderInfo.ReCalculateOrderTotals();

            var payPalStandard = new PayPalStandardProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalStandard));
            collectPayPalStandardShipping = payPalStandard.ShippingLogic == "Store" && !checkoutOrderInfo.HasOnlyDownloadableProducts;

            if (!IsPostBack)
            {
                int? removeCartItem = WA.Parser.ToInt(Request.QueryString["remove"]);
                if (removeCartItem.HasValue)
                {
                    RemoveCartItemFromCart(removeCartItem.Value);
                    UpdateCheckoutSession();

                    // redirect so that the "mini-cart" updates on the page also...
                    Response.Redirect(StoreUrls.Cart());
                }
                // FEATURE: Add product to the cart via URL/QueryString
                string addProductSlug = Request.QueryString["add"] ?? string.Empty;
                if (!string.IsNullOrEmpty(addProductSlug))
                {
                    var productToAdd = Product.GetBySlug(StoreContext.CurrentStore.Id.Value, addProductSlug);
                    if (productToAdd != null)
                    {
                        //bool IsAvailableForPurchase = productToAdd.IsAvailableForPurchase.GetValueOrDefault(true) && WA.Parser.ToBool(StoreContext.CurrentStore.GetSetting(StoreSettingNames.EnableCheckout)).GetValueOrDefault(true);
                        bool IsAvailableForPurchase = productToAdd.IsPurchaseableByUser;
                        if (IsAvailableForPurchase)
                        {
                            int quantityToAdd = Request.QueryString["q"] != null ? Convert.ToInt32(Request.QueryString["q"]) : 1;
                            cartController.AddProductToCart(productToAdd.Id.Value, quantityToAdd, string.Empty);
                        }
                        checkoutOrderInfo.Cart = cartController.GetCart(false);
                        checkoutOrderInfo.ReCalculateOrderTotals();

                        bool redirectBackToReferrer = WA.Parser.ToBool(Request.QueryString["redirect"]).GetValueOrDefault(false);
                        if (redirectBackToReferrer && (Request.UrlReferrer != null))
                        {
                            string redirectUrl = Request.UrlReferrer.ToString();


                            // Remove previous flash message from querystring when redirecting
                            if (redirectUrl.ToLower().Contains("flash"))
                            {
                                int flashIndex = redirectUrl.IndexOf("flash");
                                redirectUrl = redirectUrl.Substring(0, flashIndex - 1);
                            }

                            bool referrerIsOnsite = (Request.UrlReferrer.Host == Request.Url.Host);
                            if (referrerIsOnsite)
                            {
                                // redirect and add a 'flash' message to notify customer that product was added to cart
                                if (IsAvailableForPurchase)
                                {
                                    redirectUrl = redirectUrl.AddUrlParam("flash", HttpUtility.UrlPathEncode(string.Format(@"""{0}"" has been added to your cart", productToAdd.Name)));
                                }
                                else
                                {
                                    redirectUrl = redirectUrl.AddUrlParam("flash", HttpUtility.UrlPathEncode(string.Format(@"""{0}"" is not available for purchase.", productToAdd.Name)));
                                }
                            }
                            //Response.Redirect(redirectUrl, true);
                            Response.Redirect(redirectUrl);
                        }
                        else
                        {
                            if (IsAvailableForPurchase)
                            {
                                flash.InnerHtml = string.Format(@"""{0}"" has been added to your cart", productToAdd.Name);
                            }
                            else
                            {
                                flash.InnerHtml = string.Format(@"""{0}"" is not available for purchase", productToAdd.Name);
                            }
                            flash.Visible = true;
                        }
                    }
                }

                DataBindCartItems();

                var store = StoreContext.CurrentStore;

                //---- checkout buttons                
                bool payLaterPaymentEnabled = StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.PayLater);
                bool cardCaptureOnlyPaymentEnabled = StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.CardCaptureOnly);
                bool authorizeNetAimEnabled = StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.AuthorizeNetAim);
                bool payPalDirectEnabled = StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.PayPalDirectPayment);
                bool onsitePaymentProviderEnabled = payLaterPaymentEnabled || cardCaptureOnlyPaymentEnabled || authorizeNetAimEnabled || payPalDirectEnabled;

                btnCheckoutOnsite.Visible = onsitePaymentProviderEnabled;
                btnCheckoutPayPalStandard.Visible = StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.PayPalStandard);
                ibtnPayPalExpressCheckout.Visible = StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.PayPalExpressCheckout);
                if (btnCheckoutOnsite.Visible && (btnCheckoutPayPalStandard.Visible || ibtnPayPalExpressCheckout.Visible))
                {
                    spnOr.Visible = true;
                }

                //collectPayPalStandardShipping = payPalStandard.ShippingLogic == "Store";
                //payPalStandard = new PayPalStandardProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalStandard));)
                if (collectPayPalStandardShipping)
                {
                    string storeCountry = store.GetSetting(StoreSettingNames.DefaultCountryCode);
                    var countries = DnnHelper.GetCountryListAdoNet();
                    StringBuilder countriesOptionsHtml = new StringBuilder(countries.Count);
                    foreach (var c in countries)
                    {
                        countriesOptionsHtml.AppendFormat(@"<option value=""{0}"" {2}>{1}</option>", c.CountryCode, c.Name, c.CountryCode == storeCountry ? "selected=selected" : string.Empty);
                    }
                    litCountryOptionsHtml.Text = countriesOptionsHtml.ToString();
                }

                //IShippingProvider fedExProvider = ShippingProviderFactory.Get(StoreContext.CurrentStore.Id.Value, ShippingProviderType.FedEx);
                //pnlShippingQuoteForm.Visible = fedExProvider.IsEnabled;
                var shippingServices = StoreContext.CurrentStore.GetEnabledShippingProviders(null, checkoutOrderInfo.Cart.Id);
                if (shippingServices.Count > 0)
                {
                    pnlShippingQuoteForm.Visible = true;
                }

                //----------Show / Hide Coupon Box and Shipping Estimate Boxes

                pnlShippingQuoteForm.Visible = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ShowShippingEstimate)).GetValueOrDefault(true);
                pnlCouponCodeForm.Visible = WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ShowCouponBox)).GetValueOrDefault(true);
            }


        }

        private void LoadResourceFileSettings()
        {
            litNameOfCart.Text = ResourceString("NameOfCart.Text");
            btnCheckoutOnsite.Text = ResourceString("CheckoutOnSite.Text");
        }

        private void DataBindCartItems()
        {
            List<vCartItemProductInfo> cartItems = checkoutOrderInfo.Cart.GetCartItemsWithProductInfo();
            rptCartItems.DataSource = cartItems;
            rptCartItems.DataBind();

            pnlEmptyCart.Visible = (cartItems.Count <= 0);
            pnlCart.Visible = !pnlEmptyCart.Visible;
        }

        private void RemoveCartItemFromCart(int cartItemId)
        {
            cartController.RemoveCartItemFromCart(cartItemId);

            DataModel.Cart cart = cartController.GetCart(false);
            if (cart.CartItemCollectionByCartId.Count == 0)
            {
                // remove all coupons if we have an empty cart
                checkoutOrderInfo.RemoveAllCoupons();
            }
        }

        private void UpdateProductQuantities()
        {
            Regex rxNameKeyMatch = new Regex(cartItemQtyInputNamePrefix + @"(\d+)");
            Dictionary<string, string> cartItemQtyDict = WA.Web.WebHelper.GetFormValueDictionaryByNameMatch(rxNameKeyMatch, rxNameKeyMatch, Request);

            foreach (var cartItemQty in cartItemQtyDict)
            {
                int? cartItemId = WA.Parser.ToInt(cartItemQty.Key);
                int? quantity = WA.Parser.ToInt(cartItemQty.Value);
                if (cartItemId.HasValue && quantity.HasValue)
                {
                    cartController.UpdateCartItemQuantity(cartItemId.Value, quantity.Value);
                }
            }
        }

        protected void lbtnUpdateQty_Click(object sender, EventArgs e)
        {
            UpdateProductQuantities();
            UpdateCheckoutSession();

            // Do a redirect so the mini-cart gets updated
            Response.Redirect(StoreUrls.Cart());
        }

        protected void btnApplyCouponCode_Click(object sender, EventArgs e)
        {
            CouponStatus couponStatus;
            bool couponApplied = checkoutOrderInfo.ApplyCoupon(txtCouponCode.Text, out couponStatus);
            if (couponApplied)
            {
                // update the Session and redirect                
                UpdateCheckoutSession();

                Response.Redirect(StoreUrls.Cart());
            }
            else
            {
                couponStatusMessage.Visible = true;
                string couponResxMsg = ResourceString("CouponStatus." + couponStatus);
                couponStatusMessage.InnerHtml = !string.IsNullOrEmpty(couponResxMsg) ? couponResxMsg : couponStatus.ToString();
            }
        }

        protected void btnCheckoutOnsite_Click(object sender, EventArgs e)
        {
            UpdateProductQuantities();
            UpdateCheckoutSession();

            if (CartIsValid())
            {
                if (UserId > 0)
                {
                    Response.Redirect(StoreUrls.Checkout());
                }
                else
                {
                    Response.Redirect(StoreUrls.LoginPrompt());
                }
            }
            else
            {
                Response.Redirect(StoreUrls.Cart(errors));
            }
        }

        protected void btnCheckoutPayPalStandard_Click(object sender, EventArgs e)
        {
            UpdateProductQuantities();
            UpdateCheckoutSession();

            if (CartIsValid())
            {
                Response.Redirect(string.Format("{0}PayPal/PayPalStandardPostCart.aspx?PortalId={1}", ModuleRootWebPath, PortalId));
            }
            else
            {
                Response.Redirect(StoreUrls.Cart(errors));
            }
        }

        protected void btnCheckoutPayPalStandardWithShipping_Click(object sender, EventArgs e)
        {
            string shippingParam = string.Empty;

            string shipOption = Request.Params["paypalStdShippingCost"];
            if (!string.IsNullOrEmpty(shipOption))
            {
                string[] parts = shipOption.Split('|');
                decimal? shippingCost = WA.Parser.ToDecimal(parts[0]);
                if (shippingCost.HasValue)
                {
                    shippingParam = "&s=" + shippingCost.Value.ToString("F2");
                    shippingParam += "&sn=" + HttpUtility.UrlPathEncode(parts[1]);
                }
            }
            checkoutOrderInfo.ShippingAddress = new AddressInfo()
                                                    {
                                                        Address1 = Request.Params["paypalStdAddress1"],
                                                        Address2 = Request.Params["paypalStdAddress2"],
                                                        City = Request.Params["paypalStdCity"],
                                                        Region = Request.Params["paypalStdRegion"],
                                                        PostalCode = Request.Params["paypalStdPostalCode"],
                                                        Country = Request.Params["paypalStdCountry"]
                                                    };

            UpdateProductQuantities();
            UpdateCheckoutSession();

            if (CartIsValid())
            {
                Response.Redirect(string.Format("{0}PayPal/PayPalStandardPostCart.aspx?PortalId={1}{2}", ModuleRootWebPath, PortalId, shippingParam));
            }
            else
            {
                Response.Redirect(StoreUrls.Cart(errors));
            }

            //CheckoutCartValidator cartValidator = new CheckoutCartValidator();
            //var validationResult = cartValidator.Validate(checkoutOrderInfo.Cart);

            //if (validationResult.IsValid)
            //{
            //    Response.Redirect(string.Format("{0}PayPal/PayPalStandardPostCart.aspx?PortalId={1}{2}",
            //                                    ModuleRootWebPath, PortalId, shippingParam));
            //}
            //else
            //{

            //    flash.InnerHtml = String.Join(",", validationResult.Errors.ToList().ConvertAll(e => e.ErrorMessage).ToArray());
            //    flash.Visible = true;
            //}
        }

        private bool CartIsValid()
        {
            CheckoutCartValidator cartValidator = new CheckoutCartValidator();
            var validationResult = cartValidator.Validate(checkoutOrderInfo.Cart);

            if (validationResult.IsValid)
            {
                return true;
            }
            else
            {
                errors = String.Join(",", validationResult.Errors.ToList().ConvertAll(e => e.ErrorMessage).ToArray());
                return false;
            }
        }

        protected void ibtnPayPalExpressCheckout_Click(object sender, ImageClickEventArgs e)
        {
            UpdateProductQuantities();
            UpdateCheckoutSession();

            OrderController orderController = new OrderController(StoreContext);
            Order pendingOrder = orderController.CreateOrder(checkoutOrderInfo, OrderStatusName.PendingOffsite);

            PayPalExpressCheckoutPaymentProvider payPalExpressCheckout = new PayPalExpressCheckoutPaymentProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalExpressCheckout));
            string cancelUrl = StoreUrls.Cart();
            string returnUrl = StoreUrls.CheckoutReview();
            string token = payPalExpressCheckout.SetExpressCheckoutAndGetToken(pendingOrder, cancelUrl, returnUrl);


            if (!string.IsNullOrEmpty(token))
            {
                string payPalUrl = payPalExpressCheckout.GetExpressCheckoutUrl(token);

                Response.Redirect(payPalUrl);
            }
            else
            {
                // ERROR
                throw new ApplicationException("PayPal Express Token is Null/Empty!");
            }

        }

        protected void UpdateCheckoutSession()
        {
            Session[StoreContext.SessionKeys.CheckoutOrderInfo] = checkoutOrderInfo;
        }

        protected void btnEstimateShipping_Click(object sender, EventArgs e)
        {
            ShipmentPackagingStrategy shipmentPackagingStrategy = WA.Enum<ShipmentPackagingStrategy>.TryParseOrDefault(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShipmentPackagingStrategy), ShipmentPackagingStrategy.SingleBox);

            IPostalAddress origin = StoreContext.CurrentStore.Address.ToPostalAddress();
            IPostalAddress destination = new PostalAddress() { PostalCode = txtShippingEstimateZip.Text.Trim(), IsResidential = !chkShippingAddressIsBusiness.Checked };

            var shipmentPackages = checkoutOrderInfo.Cart.GetCartItemsAsShipmentPackages(shipmentPackagingStrategy);
            var shippingRates = new List<IShippingRate>();
            var shippingServices = StoreContext.CurrentStore.GetEnabledShippingProviders(null, checkoutOrderInfo.Cart.Id);
            foreach (var shipper in shippingServices)
            {
                //if(shipper is UpsShippingService)
                //{
                // NOTE: UPS is silly and requires PostalCode AND State/Region - Now being used for all shipping estimates for a more accurate estimate
                IGeocodeRequest request = new GoogleGeocodeRequest(destination.PostalCode);
                IGeocodeService service = new GoogleGeocoderV3();
                var response = service.Geocode(request) as GoogleGeocodeResponse;
                if (response.Success && response.Results.Length > 0)
                {
                    string region = response.Results[0].Address_Components.Where(x => x.Types.Contains(AddressComponentType.administrative_area_level_1)).Select(x => x.Short_Name).FirstOrDefault();
                    if (!string.IsNullOrEmpty(region))
                    {
                        destination.Region = region;
                    }

                    string countryCode = response.Results[0].Address_Components.Where(x => x.Types.Contains(AddressComponentType.country)).Select(x => x.Short_Name).FirstOrDefault();
                    if (!string.IsNullOrEmpty(countryCode))
                    {
                        destination.CountryCode = countryCode;
                    }
                }
                //}
                try
                {
                    shippingRates.AddRange(shipper.GetRates(origin, destination, shipmentPackages));
                }
                catch (Exception ex)
                {
                    Exceptions.LogException(ex);
                    ShowFlash(ex.Message);
                }
            }

            //rptShippingRateEstimates.DataSource = shippingRates;
            //rptShippingRateEstimates.DataBind();

            rblShippingRateEstimates.Items.Clear();
            shippingRates.ForEach(x => rblShippingRateEstimates.Items.Add(new ListItem()
            {
                Value = string.Format(@"{0}||{1}||{2}", x.ServiceType, x.ServiceTypeDescription, x.Rate),
                Text = string.Format(@"{0} - {1}", ((ShippingRate)x).DisplayName, HttpUtility.HtmlDecode(StoreContext.CurrentStore.FormatCurrency(x.Rate)))
            }));
        }

        protected void rblShippingRateEstimates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] rateParts = rblShippingRateEstimates.SelectedValue.Split("||");
            checkoutOrderInfo.ShippingRate.ServiceType = rateParts[0];
            checkoutOrderInfo.ShippingRate.ServiceTypeDescription = rateParts[1];
            checkoutOrderInfo.ShippingRate.Rate = Convert.ToDecimal(rateParts.Length == 3 ? rateParts[2] : rateParts[1]);

            //checkoutOrderInfo.BillingAddress.PostalCode = txtShippingEstimateZip.Text;
            //checkoutOrderInfo.ShippingAddress.PostalCode = txtShippingEstimateZip.Text;
            //checkoutOrderInfo.ShippingAddress.IsResidential = !chkShippingAddressIsBusiness.Checked;

            checkoutOrderInfo.ReCalculateOrderTotals();

            UpdateProductQuantities();
            UpdateCheckoutSession();
        }
    }
}