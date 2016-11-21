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
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.Shipping;
using DotNetNuke.Services.Exceptions;
using WA.Extensions;

namespace DNNspot.Store.Modules.Checkout
{
    public partial class CheckoutShippingMethod : StoreCheckoutModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadResourceFileSettings();
                FillListControls();
            }
        }

        private void FillListControls()
        {
            //---- Shipping Options
            IPostalAddress origin = StoreContext.CurrentStore.Address.ToPostalAddress();
            IPostalAddress destination = checkoutOrderInfo.ShippingAddress.ToPostalAddress();
            ShipmentPackagingStrategy shipmentPackagingStrategy = WA.Enum<ShipmentPackagingStrategy>.TryParseOrDefault(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShipmentPackagingStrategy), ShipmentPackagingStrategy.SingleBox);

            var shipmentPackages = checkoutOrderInfo.Cart.GetCartItemsAsShipmentPackages(shipmentPackagingStrategy);
            var shippingRates = new List<IShippingRate>();
            var shippingServices = StoreContext.CurrentStore.GetEnabledShippingProviders(null, checkoutOrderInfo.Cart.Id);
            foreach (var shipper in shippingServices)
            {
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
            ddlShippingOption.Items.Clear();
            shippingRates.ForEach(x => ddlShippingOption.Items.Add(new ListItem()
            {
                Value = string.Format(@"{0}||{1}||{2}", x.ServiceType, x.ServiceTypeDescription, x.Rate),
                Text = string.Format(@"{0} - {1}", ((ShippingRate)x).DisplayName, HttpUtility.HtmlDecode(StoreContext.CurrentStore.FormatCurrency(x.Rate)))
            }));

            var rate = shippingRates.Where(x => checkoutOrderInfo.ShippingRate.ServiceType == x.ServiceType).FirstOrDefault();
            string selectedShippingRate = String.Empty;
            if (rate != null)
            {
                selectedShippingRate = string.Format(@"{0}||{1}||{2}", rate.ServiceType, rate.ServiceTypeDescription, rate.Rate);
            }

            ddlShippingOption.TrySetSelectedValue(selectedShippingRate);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string[] rateParts = ddlShippingOption.SelectedValue.Split("||");
            checkoutOrderInfo.ShippingRate.ServiceType = rateParts[0];
            checkoutOrderInfo.ShippingRate.ServiceTypeDescription = rateParts[1];
            checkoutOrderInfo.ShippingRate.Rate = Convert.ToDecimal(rateParts.Length == 3 ? rateParts[2] : rateParts[1]);
            checkoutOrderInfo.ReCalculateOrderTotals();

            UpdateCheckoutSession(checkoutOrderInfo);

            if (checkoutOrderInfo.RequiresPayment)
            {
                Response.Redirect(StoreUrls.CheckoutPayment());
            }
            else
            {
                Response.Redirect(StoreUrls.CheckoutReview());
            }
        }

        private void LoadResourceFileSettings()
        {
            litBillingBreadcrumb.Text = ResourceString("litBillingBreadcrumb.Text");
            litShippingBreadcrumb.Text = ResourceString("litShippingBreadcrumb.Text");
            litShippingMethodBreadcrumb.Text = ResourceString("litShippingMethodBreadcrumb.Text");
            litPaymentBreadcrumb.Text = ResourceString("litPaymentBreadcrumb.Text");
            litReviewOrderBreadcrumb.Text = ResourceString("litReviewOrderBreadcrumb.Text");
            
            //btnCheckoutOnsite.Text = ResourceString("CheckoutOnSite.Text");
        }
    }
}