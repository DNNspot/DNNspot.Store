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
using WA.Extensions;

namespace DNNspot.Store.Modules.MyOrders
{
    public partial class MyOrders : StoreModuleBase
    {
        const int defaultMaxResults = 15;
        internal const string SessionKeyOrderEmail = "MyOrders-CustomerEmail";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bool showMore = WA.Parser.ToBool(Request.QueryString["more"]).GetValueOrDefault(false);
                int maxResults = showMore ? int.MaxValue : defaultMaxResults;
                LoadRecentOrders(maxResults);

                if(UserId > 0)
                {
                    txtOrderEmail.Text = UserInfo.Email;
                }
            }
        }

        private void LoadRecentOrders(int maxResults)
        {
            if(UserId > 0)
            {
                List<Order> userOrders = OrderCollection.GetOrdersForUser(UserId, StoreContext.CurrentStore.Id.GetValueOrDefault(-1), false);
                if (userOrders.Count > 0)
                {
                    bool isMore = (userOrders.Count > maxResults);
                    spnShowMore.Visible = isMore;

                    rptRecentOrders.DataSource = userOrders.Take(maxResults);
                    rptRecentOrders.DataBind();
                }
                else
                {
                    pnlNoRecentOrders.Visible = true;
                }
                pnlRecentOrders.Visible = true;                
            }
        }

        protected void btnFindOrder_Click(object sender, EventArgs e)
        {
            string orderNumber = txtOrderNumber.Text;
            string orderEmail = txtOrderEmail.Text;

            Session[StoreContext.SessionKeys.Custom(SessionKeyOrderEmail)] = orderEmail;

            pnlSearchResults.Visible = true;
            Order order = Order.FindOrder(StoreContext.CurrentStore.Id.GetValueOrDefault(-1), orderNumber, orderEmail);
            if(order != null)
            {
                rptSearchResults.DataSource = new List<Order>() { order };
                rptSearchResults.DataBind();
                
                rptSearchResults.Visible = true;
                pnlNoResults.Visible = false;

                pnlRecentOrders.Visible = false;
            }
            else
            {                
                rptSearchResults.Visible = false;
                pnlNoResults.Visible = true;

                pnlRecentOrders.Visible = false;
            }
        }
    }
}