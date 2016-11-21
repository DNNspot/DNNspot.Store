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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using EntitySpaces.Interfaces;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class Shipping : StoreAdminModuleBase
    {
        protected string countryRegionOptionsHtml = "";
        
        protected DataModel.ShippingService shippingTablesService;        
        protected DataModel.ShippingService fedExService;
        protected DataModel.ShippingService upsService;
        protected PagedList<ShippingLog> shippingLogList = new PagedList<ShippingLog>(new ShippingLog[] { }, 0, 1);

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Shipping" }
               };
        }

        protected override void OnInit(EventArgs e)
        {            
            base.OnInit(e);

            int storeId = StoreContext.CurrentStore.Id.Value;

            ShipmentPackagingStrategy shipmentPackagingStrategy = WA.Enum<ShipmentPackagingStrategy>.TryParseOrDefault(StoreContext.CurrentStore.GetSetting(StoreSettingNames.ShipmentPackagingStrategy), ShipmentPackagingStrategy.SingleBox);
            ddlShipmentPackaging.SelectedValue = shipmentPackagingStrategy.ToString();

            //---- Custom Shipping
            shippingTablesService = DataModel.ShippingService.FindOrCreateNew(storeId, ShippingProviderType.CustomShipping);
            if(shippingTablesService.ShippingServiceRateTypeCollectionByShippingServiceId.Count == 0)
            {
                var defaultRateType = shippingTablesService.ShippingServiceRateTypeCollectionByShippingServiceId.AddNew();
                defaultRateType.Name = "FREE Shipping";
                defaultRateType.DisplayName = "FREE Shipping";
                defaultRateType.IsEnabled = true;
                defaultRateType.SortOrder = 1;

                var defaultRate = defaultRateType.ShippingServiceRateCollectionByRateTypeId.AddNew();
                defaultRate.WeightMin = 0;
                defaultRate.WeightMax = 999999;
                defaultRate.Cost = 0;
                
                shippingTablesService.Save();
            }
            //shippingTablesProvider = ShippingProviderFactory.Get(storeId, ShippingProviderType.CustomShipping);

            //--- FedEx
            fedExService = DataModel.ShippingService.FindOrCreateNew(storeId, ShippingProviderType.FedEx);
            //fedExProvider = ShippingProviderFactory.Get(storeId, ShippingProviderType.FedEx);
            // FedEx - Add RateTypes for this service if they don't exist
            List<ShippingServiceRateType> rateTypes = fedExService.GetAllRateTypes();
            string[] allRateTypes = WA.Enum<WA.Shipping.FedExServiceType>.GetNames();
            foreach (string rateType in allRateTypes)
            {
                if (!rateTypes.Exists(x => x.Name == rateType))
                {
                    var newRateType = fedExService.ShippingServiceRateTypeCollectionByShippingServiceId.AddNew();
                    newRateType.Name = rateType;
                    newRateType.DisplayName = rateType;
                    newRateType.IsEnabled = true;
                    newRateType.SortOrder = 99;
                }
            }
            fedExService.Save();

            //--- UPS
            upsService = DataModel.ShippingService.FindOrCreateNew(storeId, ShippingProviderType.UPS);
            upsService.Save();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            RegisterJavascriptFileOnceInBody("js/jquery.editable-1.3.3.min.js", ModuleRootWebPath + "js/jquery.editable-1.3.3.min.js");
            RegisterJavascriptFileOnceInBody("js/jquery.jtemplates.js", ModuleRootWebPath + "js/jquery.jtemplates.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FillCountryRegionOptionsHtml();

            if(!IsPostBack)
            {
                LoadShippingTransactions();

                int? deleteRateId = WA.Parser.ToInt(Request.QueryString["deleteRate"]);
                if (deleteRateId.HasValue)
                {
                    ShippingServiceRate toDelete = new ShippingServiceRate();
                    if (toDelete.LoadByPrimaryKey(deleteRateId.Value))
                    {
                        toDelete.MarkAsDeleted();
                        toDelete.Save();
                    }
                }
                short? deleteId = WA.Parser.ToShort(Request.QueryString["delete"]);
                if (deleteId.HasValue)
                {
                    DataModel.ShippingServiceRateType toDelete = new DataModel.ShippingServiceRateType();
                    if (toDelete.LoadByPrimaryKey(deleteId.Value))
                    {
                        toDelete.MarkAsDeleted();
                        toDelete.Save();
                    }
                }
                
                chkShippingTablesEnabled.Checked = shippingTablesService.IsEnabled;

                // FEDEX
                chkFedExEnabled.Checked = fedExService.IsEnabled;
                Dictionary<string, string> fedexSettings = fedExService.GetSettingsDictionary();
                txtFedExAccountNum.Text = fedexSettings.TryGetValueOrEmpty("accountNumber");
                txtFedExMeterNum.Text = fedexSettings.TryGetValueOrEmpty("meterNumber");
                txtFedExSmartPostHub.Text = fedexSettings.TryGetValueOrEmpty("smartPostHubId");
                txtFedExApiKey.Text = fedexSettings.TryGetValueOrEmpty("apiKey");
                txtFedExApiPassword.Text = fedexSettings.TryGetValueOrEmpty("apiPassword");
                chkFedExIsTestGateway.Checked = WA.Parser.ToBool(fedexSettings.TryGetValueOrEmpty("isTestGateway")).GetValueOrDefault(false);
                ddlLabelStockType.SelectedValue = fedexSettings.TryGetValueOrDefault("labelStockType", "0");
                
                // UPS
                chkUpsEnabled.Checked = upsService.IsEnabled;
                Dictionary<string, string> upsSettings = upsService.GetSettingsDictionary();
                txtUpsUserId.Text = upsSettings.TryGetValueOrEmpty("userId");
                txtUpsPassword.Text = upsSettings.TryGetValueOrEmpty("password");
                txtUpsAccountNumber.Text = upsSettings.TryGetValueOrEmpty("accountNumber");
                txtUpsAccessKey.Text = upsSettings.TryGetValueOrEmpty("accessKey");                
                chkUpsIsTestGateway.Checked = WA.Parser.ToBool(upsSettings.TryGetValueOrEmpty("isTestGateway")).GetValueOrDefault(false);

                LoadShippingRates();
            }
        }

        private void FillCountryRegionOptionsHtml()
        {
            const string cacheKey = "DNNspot-Store-CountryRegionOptionsHtml";

            string cachedHtml = CacheHelper.GetCacheOrDefault(cacheKey, "");
            if (!string.IsNullOrEmpty(cachedHtml))
            {
                countryRegionOptionsHtml = cachedHtml;
            }
            else
            {
                // drop-down values of country-region
                List<CountryInfo> countries = DnnHelper.GetCountryListAdoNet();
                StringBuilder html = new StringBuilder(@"<option value=""none"">Any/All</option>");
                foreach(var country in countries)
                {
                    if(country.Regions.Count > 0)
                    {
                        html.AppendFormat(@"<option value=""{0}"">{1}</option>", country.CountryCode, country.Name);
                        foreach (var region in country.Regions.Values)
                        {
                            html.AppendFormat(@"<option value=""{0}|{1}"">{2} | {3}</option>", country.CountryCode, region.RegionCode, country.Name, region.Name);
                        }                        
                    }
                    else
                    {
                        html.AppendFormat(@"<option value=""{0}"">{1}</option>", country.CountryCode, country.Name);
                    }
                }
                countryRegionOptionsHtml = html.ToString();

                CacheHelper.SetCache(cacheKey, countryRegionOptionsHtml, TimeSpan.FromMinutes(10));
            }
        }

        private void LoadShippingRates()
        {
            //rptShippingRateTypes.DataSource = StoreContext.CurrentStore.GetAllShippingRateTypes();
            rptShippingRateTypes.DataSource = shippingTablesService.GetAllRateTypes();
            rptShippingRateTypes.DataBind();
        }

        private void LoadShippingTransactions()
        {
            int pageIndex = WA.Parser.ToInt(Request.QueryString["pg"]).GetValueOrDefault(1) - 1;
            int pageSize = 100;

            List<ShippingLog> shippingLog = ShippingLog.LoadAll();

            if (shippingLog.Count > 0)
            {
                shippingLogList = new PagedList<ShippingLog>(shippingLog, pageIndex, pageSize);
                rptShippingLogs.DataSource = shippingLogList;
                rptShippingLogs.DataBind();

                if (shippingLog.Count > pageSize)
                {
                    RenderPaginationLinks(shippingLogList);
                }
            }
            else
            {
                rptShippingLogs.Visible = false;
            }
        }

        private void RenderPaginationLinks(PagedList<ShippingLog> pagedList)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<label>Page:</label> <ul class='pager'>");
            for (int i = 1; i <= pagedList.PageCount; i++)
            {
                html.AppendFormat(@"<li class=""{2}""><a href=""{0}"">{1}</a></li>", StoreUrls.ShippingLog(i), i, i == pagedList.PageNumber ? "current" : string.Empty);
            }
            html.Append("</ul>");
            litPaginationLinks.Text = html.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            StoreContext.CurrentStore.UpdateSetting(StoreSettingNames.ShipmentPackagingStrategy, ddlShipmentPackaging.SelectedValue);

            //--- Custom Shipping
            Dictionary<string, string> settings = shippingTablesService.GetSettingsDictionary();
            settings["IsEnabled"] = chkShippingTablesEnabled.Checked.ToString();
            shippingTablesService.UpdateSettingsDictionary(settings);
            SaveShippingRows();

            //--- FedEx
            settings = fedExService.GetSettingsDictionary();
            settings["IsEnabled"] = chkFedExEnabled.Checked.ToString();
            settings["accountNumber"] = txtFedExAccountNum.Text;
            settings["meterNumber"] = txtFedExMeterNum.Text;
            settings["smartPostHubId"] = txtFedExSmartPostHub.Text;
            settings["apiKey"] = txtFedExApiKey.Text;
            settings["apiPassword"] = txtFedExApiPassword.Text;
            settings["isTestGateway"] = chkFedExIsTestGateway.Checked.ToString();
            settings["labelStockType"] = ddlLabelStockType.SelectedValue;
            fedExService.UpdateSettingsDictionary(settings);

            // UPS
            settings = upsService.GetSettingsDictionary();
            settings["IsEnabled"] = chkUpsEnabled.Checked.ToString();
            settings["userId"] = txtUpsUserId.Text;
            settings["password"] = txtUpsPassword.Text;
            settings["accountNumber"] = txtUpsAccountNumber.Text;
            settings["accessKey"] = txtUpsAccessKey.Text;            
            settings["isTestGateway"] = chkUpsIsTestGateway.Checked.ToString();
            upsService.UpdateSettingsDictionary(settings);

            Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.Shipping, "Shipping options saved"));
        }

        private void SaveShippingRows()
        {
            const string inputPrefix = "shippingRow";
            Regex rxInputName = new Regex(@"shippingRow\[(\d+)\]\[(.*?)\]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            Dictionary<int, Dictionary<string, string[]>> shipMethodIdToNameValues = new Dictionary<int, Dictionary<string, string[]>>();
            foreach (string key in Request.Form)
            {
                Match m = rxInputName.Match(key);
                if (m.Success)
                {
                    int? shipMethodId = WA.Parser.ToInt(m.Groups[1].Value);
                    string fieldName = m.Groups[2].Value;
                    if (shipMethodId.HasValue)
                    {
                        if (!shipMethodIdToNameValues.ContainsKey(shipMethodId.Value))
                        {
                            shipMethodIdToNameValues[shipMethodId.Value] = new Dictionary<string, string[]>();
                        }
                        shipMethodIdToNameValues[shipMethodId.Value][fieldName] = Request.Form.GetValues(key);
                    }
                }
            }

            // delete existing rates for this service (we'll re-add them below)
            shippingTablesService.GetAllRateTypes().ForEach(rt => rt.DeleteAllRates());

            //--- Save the dictionary structure to the database                        
            foreach(var methods in shipMethodIdToNameValues)
            {
                int rateTypeId = methods.Key;
                
                //ShippingServiceRateCollection.DeleteAllRates(shippingMethodId);
                
                string[] countryRegionValues = methods.Value["ddlCountryRegion"];
                string[] minWeightValues = methods.Value["minWeight"];
                string[] maxWeightValues = methods.Value["maxWeight"];
                string[] costValues = methods.Value["cost"];

                for(int i = 0; i < countryRegionValues.Length; i++)
                {
                    string countryValue = "";
                    string regionValue = "";
                    decimal? minWeight = null;
                    decimal? maxWeight = null;
                    decimal? cost = null;

                    string[] parts = countryRegionValues[i].Split('|');
                    if(parts.Length == 2)
                    {
                        // Country AND Region
                        countryValue = parts[0];
                        regionValue = parts[1];
                    }
                    else if(parts.Length == 1)
                    {
                        // Country only
                        countryValue = parts[0];
                    }
                    if(countryValue.ToLower() == "none")
                    {
                        countryValue = "";
                    }
                    minWeight = WA.Parser.ToDecimal(minWeightValues[i]);
                    maxWeight = WA.Parser.ToDecimal(maxWeightValues[i]);
                    cost = WA.Parser.ToDecimal(costValues[i]);

                    if(cost.HasValue)
                    {
                        // add the rate to the db
                        ShippingServiceRate newRate = new ShippingServiceRate();
                        newRate.RateTypeId = (short)rateTypeId;
                        newRate.CountryCode = countryValue;
                        newRate.Region = regionValue;
                        newRate.WeightMin = minWeight.GetValueOrDefault(0);
                        newRate.WeightMax = maxWeight.GetValueOrDefault(99999999);
                        newRate.Cost = cost;
                        newRate.Save();
                    }
                }
            }
        }

        protected void btnCreateShippingRateType_Click(object sender, EventArgs e)
        {
            DataModel.ShippingServiceRateType toSave = new DataModel.ShippingServiceRateType();
            toSave.ShippingServiceId = shippingTablesService.Id;
            toSave.Name = txtNewShippingRateType.Text;
            toSave.DisplayName = toSave.Name;
            toSave.IsEnabled = true;
            toSave.Save();

            // NOTE - implement UI drag-drop sorting to update SortOrder field ??

            SaveShippingRows();

            Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.Shipping, "Created new shipping option"));
        }
    }
}