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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DNNspot.Store.Shipping;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class Orders : StoreAdminModuleBase
    {
        const int maxPageSize = 50;
        protected bool bulkActionsEnabled = false;

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Orders" }
               };
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillListControls();
                chklOrderStatus.Items.FindByText(OrderStatusName.Completed.ToString()).Selected = true;
                chklOrderStatus.Items.FindByText(OrderStatusName.Processing.ToString()).Selected = true;

                ShowRecentOrders();
                
                // NOTE - remove 'bulk shipping actions' for now
                //var enabledShippers = StoreContext.CurrentStore.GetEnabledShippingProviders();
                //if (enabledShippers.Exists(s => (s is FedExShippingService) || (s is UpsShippingService)))
                //{
                //    pnlBulkActions.Visible = true;
                //    bulkActionsEnabled = true;
                //}
                //else
                //{
                //    pnlBulkActions.Visible = false;
                //    bulkActionsEnabled = false;
                //}
            }
        }

        private void ShowRecentOrders()
        {
            processShipmentMessages.Visible = false;

            List<Order> orders = GetFilteredOrders(maxPageSize);

            DisplayOrderResults(orders);

            flash.InnerHtml = string.Format(@"Showing {0} most recent orders", Math.Min(orders.Count, maxPageSize));
        }

        private void FillListControls()
        {
            string[] orderStatuses = WA.Enum<OrderStatusName>.GetNames();
            chklOrderStatus.DataSource = orderStatuses;
            chklOrderStatus.DataBind();
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            processShipmentMessages.Visible = false;

            List<Order> orders = GetFilteredOrders(null);                        

            DisplayOrderResults(orders);
        }
        
        private void DisplayOrderResults(List<Order> orders)
        {
            if (orders.Count > 0)
            {
                flash.InnerHtml = string.Format(@"Found {0} matching orders", orders.Count);

                pnlOrderResults.Visible = true;
                rptOrders.DataSource = orders;
                rptOrders.DataBind();
            }
            else
            {
                flash.InnerHtml = "No orders matched your search criteria.";
                pnlOrderResults.Visible = false;
            }
            flash.Visible = true;            
        }

        private List<Order> GetFilteredOrders(int? maxResults)
        {
            DateTime? fromDate = WA.Parser.ToDateTime(txtDateFrom.Text);
            DateTime? toDate = WA.Parser.ToDateTime(txtDateTo.Text);
            string firstName = txtCustomerFirstName.Text ?? "";
            string lastName = txtCustomerLastName.Text ?? "";
            string customerEmail = txtCustomerEmail.Text;
            
            List<OrderStatusName> orderStatuses = new List<OrderStatusName>();
            List<string> selectedStatuses = (List<string>)chklOrderStatus.GetSelectedValues();
            selectedStatuses.ForEach(s => orderStatuses.Add(WA.Enum<OrderStatusName>.Parse(s)));

            return OrderCollection.FindOrders(StoreContext.CurrentStore.Id.Value, fromDate, toDate, firstName, lastName, customerEmail, orderStatuses, maxResults);
        }

        protected void btnExportToXml_Click(object sender, EventArgs e)
        {                        
            Response.ContentType = "text/xml";            
            Response.AppendHeader("Content-Disposition", "attachment; filename=FilteredOrders.xml");

            List<Order> orders = GetFilteredOrders(null);
            Response.Write(XmlHelper.ToXml(orders));
            Response.Flush();
            Response.End();
        }

        protected void btnBulkProcessShipments_Click(object sender, EventArgs e)
        {
            List<string> bulkErrors = new List<string>();

            // TODO - get the packaging strategy from the user
            ShipmentPackagingStrategy packagingStrategy = ShipmentPackagingStrategy.SingleBox;
            
            string[] orderNos = Request.Form.GetValues("orderNumber");
            if(orderNos != null && orderNos.Length > 0)
            {                                                                
                var orderController = new OrderController(StoreContext);

                List<Order> orders = OrderCollection.FindOrdersByOrderNumber(StoreContext.CurrentStore.Id.Value, new List<string>(orderNos));
                foreach(var order in orders)
                {
                    var orderToShip = Order.GetOrder(order.Id.Value);
                    
                    var result = orderController.GetTrackingNumberAndLabels(orderToShip, packagingStrategy);
                    if (!result.Success)
                    {
                        bulkErrors.AddRange(result.ErrorMessages);
                    }              
                }      
            }
            else
            {
                bulkErrors.Add("No orders selected");
            }

            var flashHtml = new StringBuilder();
            if(bulkErrors.Count == 0)
            {
                flashHtml.AppendFormat("Shipments Processed Successfully");
            }
            else
            {
                bulkErrors.ForEach(s => flashHtml.AppendFormat("{0}<br />", s));
            }
            
            processShipmentMessages.InnerHtml = flashHtml.ToString();
            processShipmentMessages.Visible = true;
            pnlSearch.Visible = false;
            pnlOrderResults.Visible = false;
            flash.Visible = false;            
        }

        protected void btnBulkPrintShippingLabels_Click(object sender, EventArgs e)
        {
            string[] orderNos = Request.Form.GetValues("orderNumber");
            if (orderNos != null && orderNos.Length > 0)
            {
                List<Order> orders = OrderCollection.FindOrdersByOrderNumber(StoreContext.CurrentStore.Id.Value, new List<string>(orderNos));
                orders = orders.FindAll(o => o.HasShippingLabel);

                //string url = StoreUrls.ModuleFolderUrlRoot + "Modules/Admin/BulkPrintShippingLabels.aspx";
                //List<string> urlParams = new List<string>();
                //urlParams.Add("PortalId=" + PortalId);
                //urlParams.Add("ids=" + orders.ConvertAll(o => o.Id.Value).ToCsv());
                //url += "?" + urlParams.ToDelimitedString("&");
                
                //Response.Redirect(url);

                BulkRenderLabelPdfs(orders.ConvertAll(o => o.ShippingServiceLabelFile));
            }
        }

        private void BulkRenderLabelPdfs(List<string> filenames)
        {
            List<string> inputFilepaths = filenames.ConvertAll(x => StoreUrls.ShippingLabelFolderFileRoot + x);
            inputFilepaths.RemoveAll(x => !x.ToLower().EndsWith(".pdf"));

            Response.Clear();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";

            iTextHelper.ConcatenatePdfs(inputFilepaths, Response.OutputStream);

            Response.Flush();
        }
    }
}