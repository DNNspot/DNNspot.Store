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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.Shipping;
using DotNetNuke.Entities.Portals;
using EntitySpaces.Interfaces;

namespace DNNspot.Store.Modules.Admin
{
    public partial class PrintShippingLabels : System.Web.UI.Page
    {

        public int PortalId = -1;
        protected Order order = new Order();
        public bool UserIsAdmin = false;
        public string StoreName = String.Empty;
        public string StoreStreet = String.Empty;
        public string StoreCity = String.Empty;
        public string StoreRegion = String.Empty;
        public string StorePostalCode = String.Empty;
        public string StoreCountryCode = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeEntitySpaces();
            PortalSettings portalSettings = DotNetNuke.Entities.Portals.PortalController.GetCurrentPortalSettings();
            PortalId = portalSettings.PortalId;


            pnlOrderDetails.Visible = false;
            RegisterSkinCSS(portalSettings.ActiveTab.SkinPath);

            var user = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo();
            UserIsAdmin = user.IsSuperUser || user.IsInRole(portalSettings.AdministratorRoleName);

            if (LoadOrder() && UserIsAdmin)
            {
                pnlOrderDetails.Visible = true;
                var storeContext = new StoreContext(Request);
                DataModel.Store store = storeContext.CurrentStore;

                StoreName = store.Name;
                StoreStreet = store.GetSetting(StoreSettingNames.StoreAddressStreet);
                StoreCity = store.GetSetting(StoreSettingNames.StoreAddressCity);
                StoreRegion = store.GetSetting(StoreSettingNames.StoreAddressRegion);
                StorePostalCode = store.GetSetting(StoreSettingNames.StoreAddressPostalCode);
                StoreCountryCode = store.GetSetting(StoreSettingNames.StoreAddressCountryCode);
            }
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