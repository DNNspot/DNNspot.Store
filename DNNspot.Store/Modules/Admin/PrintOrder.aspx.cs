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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.PaymentProviders;
using DNNspot.Store.Shipping;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using EntitySpaces.Interfaces;
using WA.Components;
using WA.Extensions;


namespace DNNspot.Store.Modules.Admin
{
    public partial class PrintOrder : System.Web.UI.Page
    {
        public StoreContext storeContext;
        public StoreUrls storeUrls;
        public int PortalId = -1;
        protected Order order = new Order();
        public bool UserIsAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeEntitySpaces();
            PortalSettings portalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings();
            PortalId = portalSettings.PortalId;

            storeContext = new StoreContext(Request);
            storeUrls = new StoreUrls(storeContext);

            pnlOrderDetails.Visible = false;
            RegisterSkinCSS(portalSettings.ActiveTab.SkinPath);
            if (LoadOrder())
            {
                var user = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();

                UserIsAdmin = user.IsSuperUser || user.IsInRole(portalSettings.AdministratorRoleName);

                pnlOrderDetails.Visible = true;


                if (!IsPostBack)
                {
                    IShippingService shippingService = null;
                    if (order.ShippingServiceProvider.Contains("UPS", false))
                    {
                        shippingService = ShippingServiceFactory.Get(order.StoreId.Value, ShippingProviderType.UPS, order.Id, order.CreatedFromCartId);
                    }
                    else if (order.ShippingServiceProvider.Contains("FedEx", false))
                    {
                        shippingService = ShippingServiceFactory.Get(order.StoreId.Value, ShippingProviderType.FedEx, order.Id, order.CreatedFromCartId);
                    }
                    bool shipperSupportsTrackingNumbers = (shippingService != null) && ((shippingService is FedExShippingService) || (shippingService is UpsShippingService));

                    ddlOrderStatus.SelectedValue = order.OrderStatus.ToString();
                    ddlPaymentStatus.SelectedValue = order.PaymentStatus.ToString();

                    txtShippingTrackingNumber.Text = order.ShippingServiceTrackingNumber;
                    if (!string.IsNullOrEmpty(order.ShippingServiceTrackingNumber))
                    {

                        txtShippingTrackingNumber.Visible = false;
                    }


                    rptOrderItems.DataSource = order.OrderItemCollectionByOrderId;
                    rptOrderItems.DataBind();



                    //if(order.PaymentStatus != PaymentStatusName.Completed && StoreContext.CurrentStore.IsPaymentProviderEnabled(PaymentProviderName.OfflinePayment) && order.Total.GetValueOrDefault(0) > 0)



                }

            }

            //HttpContext.Current.User.Identity.Name
        }

        private void RegisterSkinCSS(string portalSkin)
        {

            HtmlHead head = (HtmlHead)Page.Header;
            HtmlLink link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl(portalSkin + "skin.css"));
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);
        }

        private bool LoadOrder()
        {
            Guid? orderId = WA.Parser.ToGuid(Request.QueryString["id"]);

            OrderQuery q = new OrderQuery();
            q.Where(q.CreatedFromCartId == orderId.Value);
            q.es.Top = 1;


            return order.Load(q);
        }
        private static void InitializeEntitySpaces()
        {
            if (esConfigSettings.ConnectionInfo.Default != "SiteSqlServer")
            {
                esConfigSettings connectionInfoSettings = esConfigSettings.ConnectionInfo;
                foreach (esConnectionElement connection in connectionInfoSettings.Connections)
                {
                    //if there is a SiteSqlServer in es connections set it default
                    if (connection.Name == "SiteSqlServer")
                    {
                        esConfigSettings.ConnectionInfo.Default = connection.Name;
                        return;
                    }
                }

                //no SiteSqlServer found grab dnn cnn string and create
                string dnnConnection = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;

                // Manually register a connection
                esConnectionElement conn = new esConnectionElement();
                conn.ConnectionString = dnnConnection;
                conn.Name = "SiteSqlServer";
                conn.Provider = "EntitySpaces.SqlClientProvider";
                conn.ProviderClass = "DataProvider";
                conn.SqlAccessType = esSqlAccessType.DynamicSQL;
                conn.ProviderMetadataKey = "esDefault";
                conn.DatabaseVersion = "2005";

                // Assign the Default Connection
                esConfigSettings.ConnectionInfo.Connections.Add(conn);
                esConfigSettings.ConnectionInfo.Default = "SiteSqlServer";

                // Register the Loader
                esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();
            }
        }
    }
}