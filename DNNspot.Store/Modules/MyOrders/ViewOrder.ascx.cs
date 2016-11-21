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
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using WA.Extensions;

namespace DNNspot.Store.Modules.MyOrders
{
    public partial class ViewOrder : StoreModuleBase
    {
        protected Order order = new Order();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                int? id = WA.Parser.ToInt(Request.QueryString["id"]);
                if (id.HasValue)
                {
                    LoadOrder(id.Value);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(order.Id.HasValue)
            {
                if(order.GetPaymentProviderName().ToLower() == "paylater")
                {
                    PayLaterPaymentProvider payLaterProvider = new PayLaterPaymentProvider(StoreContext.CurrentStore.GetPaymentProviderConfig(PaymentProviderName.PayLater));
                    litPaymentSummary.Text = string.Format("{0}<br />{1}", payLaterProvider.DisplayText, payLaterProvider.CustomerInstructions.NewlineToBr());
                }
                else
                {
                    litPaymentSummary.Text = string.Format("{0}<br />xxxx-{1}", order.CreditCardType, order.CreditCardNumberLast4);    
                }                
            }
        }

        private void LoadOrder(int orderId)
        {
            if(order.LoadByPrimaryKey(orderId))
            {
                string sessionMyOrderEmail = Convert.ToString(Session[StoreContext.SessionKeys.Custom(MyOrders.SessionKeyOrderEmail)] ?? "");
                if (IsEditable || order.CustomerEmail == sessionMyOrderEmail || (order.UserId.HasValue && order.UserId.Value == UserId))
                {
                    // load order items
                    rptOrderItems.DataSource = order.OrderItemCollectionByOrderId;
                    rptOrderItems.DataBind();
                }
                else
                {
                    pnlViewOrder.Visible = false;
                    litMsg.Text = string.Format(@"You do not have permission to view this order.");
                }
            }
        }
    }
}