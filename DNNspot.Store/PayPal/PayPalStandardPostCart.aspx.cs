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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;

namespace DNNspot.Store.PayPal
{
    public partial class PayPalStandardPostCart : System.Web.UI.Page
    {
        protected string formPostAction = "";
        protected StoreContext storeContext;
        protected StoreUrls storeUrls;
        PayPalStandardProvider payPalStandard;

        protected void Page_Load(object sender, EventArgs e)
        {            
            storeContext = new StoreContext(Request);
            storeUrls = new StoreUrls(storeContext);

            string shippingOption = Request.Params["shippingOption"];

            payPalStandard = new PayPalStandardProvider(storeContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayPalStandard));

            if(!IsPostBack)
            {
                //CartController cartController = new CartController(storeContext);
                //Cart cart = cartController.GetCart(false);

                //Order pendingOrder = ConvertCartToPendingOrder(cart);

                decimal? shippingCost = WA.Parser.ToDecimal(Request.Params["s"]);
                string shippingOptionName = Request.Params["sn"] ?? string.Empty;

                CheckoutOrderInfo checkoutOrderInfo = Session[storeContext.SessionKeys.CheckoutOrderInfo] as CheckoutOrderInfo;
                if (checkoutOrderInfo != null)
                {
                    OrderController orderController = new OrderController(storeContext);
                    Order pendingOrder = orderController.CreateOrder(checkoutOrderInfo, OrderStatusName.PendingOffsite);
                    if (shippingCost.HasValue)
                    {
                        pendingOrder.ShippingAmount = shippingCost.Value;
                        pendingOrder.ShippingServiceOption = shippingOptionName;
                        pendingOrder.ShippingServiceProvider = ShippingProviderType.CustomShipping.ToString();
                        pendingOrder.Save();
                    }                    
                    GeneratePayPalForm(pendingOrder);
                }
            }
        }

        private Order ConvertCartToPendingOrder(Cart cart)
        {            
            CheckoutOrderInfo checkoutOrderInfo = new CheckoutOrderInfo() { Cart = cart };            

            OrderController orderController = new OrderController(storeContext);

            return orderController.CreateOrder(checkoutOrderInfo, OrderStatusName.PendingOffsite);
        }

        private void GeneratePayPalForm(Order order)
        {                        
            formPostAction = payPalStandard.ProviderUrl;

            Dictionary<string, string> payPalVariables = payPalStandard.CreateOffsitePaymentRequestVariables(order, storeUrls);

            StringBuilder html = new StringBuilder();
            foreach (KeyValuePair<string, string> variable in payPalVariables)
            {
                html.AppendFormat(@"<input type=""hidden"" name=""{0}"" value=""{1}"" /> {2}", variable.Key, HttpUtility.HtmlEncode(variable.Value), Environment.NewLine);
            }
            litFormFields.Text = html.ToString();
        }
    }
}
