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
using EntitySpaces.Interfaces;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class TaxRegions : StoreAdminModuleBase
    {
        protected Dictionary<string,string> countryNameHash = new Dictionary<string, string>();
        protected Dictionary<string,string> regionNameHash = new Dictionary<string, string>();

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Tax Regions" }
               };
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                FillListControls();
            }
        }

        private void FillListControls()
        {
            // drop-down values of country-region
            //List<ListItem> countries = DnnHelper.GetCountryListItems();
            //StringBuilder html = new StringBuilder("<option></option>");
            //foreach(ListItem countryItem in countries)
            //{
            //    List<ListItem> regionsForCountry = DnnHelper.GetRegionListItems(countryItem.Value);
            //    if(regionsForCountry.Count > 0)
            //    {
            //        foreach(ListItem regionItem in regionsForCountry)
            //        {
            //            html.AppendFormat(@"<option value=""{0}-{1}"">{2} | {3}</option>", countryItem.Value, regionItem.Value, countryItem.Text, regionItem.Text);
            //            regionNameHash[regionItem.Value] = regionItem.Text;
            //        }
            //    }
            //    else
            //    {
            //        html.AppendFormat(@"<option value=""{0}"">{1}</option>", countryItem.Value, countryItem.Text);
            //    }
            //    countryNameHash[countryItem.Value] = countryItem.Text;
            //}            
            //litCountryRegionOptions.Text = html.ToString();

            try
            {
                rdoChargeTaxBasedOn.SelectedValue = StoreContext.CurrentStore.GetSetting(StoreSettingNames.SalesTaxAddressType);
            }
            catch (Exception ex)
            { }

            List<CountryInfo> countries = DnnHelper.GetCountryListAdoNet();
            StringBuilder html = new StringBuilder(@"<option></option>");
            foreach (var country in countries)
            {
                if (country.Regions.Count > 0)
                {
                    //html.AppendFormat(@"<option value=""{0}"">{1}</option>", country.CountryCode, country.Name);
                    foreach (var region in country.Regions.Values)
                    {
                        html.AppendFormat(@"<option value=""{0}-{1}"">{2} | {3}</option>", country.CountryCode, region.RegionCode, country.Name, region.Name);
                        regionNameHash[region.RegionCode] = region.Name;
                    }
                }
                else
                {
                    html.AppendFormat(@"<option value=""{0}"">{1}</option>", country.CountryCode, country.Name);
                }
                countryNameHash[country.CountryCode] = country.Name;
            }
            litCountryRegionOptions.Text = html.ToString();


            // databind the existing tax regions
            rptTaxRates.DataSource = TaxRegionCollection.GetTaxRegions(StoreContext.CurrentStore.Id.Value);
            rptTaxRates.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            StoreContext.CurrentStore.UpdateSetting(StoreSettingNames.SalesTaxAddressType, rdoChargeTaxBasedOn.SelectedValue);

            List<string> countryRegionCodes = WA.Web.WebHelper.GetFormValuesByName("ddlCountryRegion", Request);
            List<string> taxRates = WA.Web.WebHelper.GetFormValuesByName("taxRate", Request);

            if(countryRegionCodes.Count != taxRates.Count)
            {
                throw new ApplicationException("Mismatched item count for countryRegionCodes and taxRates");
            }
                
            List<TaxRateInfo> taxRateInfos = new List<TaxRateInfo>();
            for (int i = 0; i < countryRegionCodes.Count; i++)
            {
                decimal taxRate = WA.Parser.ToDecimal(taxRates[i]).GetValueOrDefault(0);
                string countryCode = "";
                string regionName = "";
                string[] parts = countryRegionCodes[i].Split('-');
                if (parts.Length == 2)
                {
                    countryCode = parts[0];
                    regionName = parts[1];
                }
                else if (parts.Length == 1)
                {
                    countryCode = parts[0];
                }

                if (!string.IsNullOrEmpty(countryCode))
                {
                    taxRateInfos.Add(new TaxRateInfo() { CountryCode = countryCode, Region = regionName, TaxRate = taxRate });
                }
            }
            UpdateTaxRates(taxRateInfos);

            Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.TaxRegions, "Tax Regions saved successfully"));
        }

        private void UpdateTaxRates(List<TaxRateInfo> taxRateInfos)
        {
            using (esTransactionScope transaction = new esTransactionScope())
            {
                int storeId = StoreContext.CurrentStore.Id.Value;

                //---- delete all tax regions for this store
                TaxRegionQuery q = new TaxRegionQuery();
                q.Where(q.StoreId == storeId);
                TaxRegionCollection taxRegions = new TaxRegionCollection();
                taxRegions.Load(q);
                taxRegions.MarkAllAsDeleted();
                taxRegions.Save();

                //---- and re-insert them
                // remove duplicate entries
                taxRateInfos.RemoveDuplicates((left, right) => (left.CountryCode == right.CountryCode && left.Region == right.Region) ? 1 : -1);
                foreach(TaxRateInfo taxRate in taxRateInfos)
                {
                    TaxRegion newTaxRegion = taxRegions.AddNew();
                    newTaxRegion.StoreId = storeId;
                    newTaxRegion.CountryCode = taxRate.CountryCode;
                    newTaxRegion.Region = taxRate.Region;
                    newTaxRegion.TaxRate = taxRate.TaxRate;
                }
                taxRegions.Save();

                transaction.Complete();
            }
        }

        private class TaxRateInfo
        {
            public string CountryCode { get; set; }
            public string Region { get; set; }
            public decimal TaxRate { get; set; }
        }
    }
}