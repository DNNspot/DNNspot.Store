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
using WA.Extensions;

namespace DNNspot.Store.UserControls
{
    public partial class AddressForm : System.Web.UI.UserControl
    {
        protected string ControlPrefix { get; set; }
        protected bool showEmailField = true;
        protected bool showBusinessNameField = false;
        protected AddressInfo addressInfo = new AddressInfo();

        //public AddressForm()
        //{
        //    ControlPrefix = this.ID + "_";
        //    ShowEmailField = true;
        //    ShowBusinessNameField = false;
        //}        

        public string ShowEmail
        {
            get { return showEmailField.ToString(); }
            set { showEmailField = WA.Parser.ToBool(value).GetValueOrDefault(true); }
        }

        public string ShowBusinessName
        {
            get { return showBusinessNameField.ToString(); }
            set { showBusinessNameField = WA.Parser.ToBool(value).GetValueOrDefault(false); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ControlPrefix = this.ID + "_";

            if (!IsPostBack)
            {                
                //ddlRegion.Items.Clear();
                //ddlRegion.Items.Add(new ListItem() { Text = "-- Choose a Country --", Value = "" });
                litRegionCodeOptions.Text = @"<option value="""">--- Choose a Country ---</option>";

                SetCountryListItems(DnnHelper.GetCountryListItems());
            }
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        SetRegionListItems(DnnHelper.GetRegionListItems());
        //        SetCountryListItems(DnnHelper.GetCountryListItems());                
        //    }
        //}

        public AddressInfo GetAddressInfoFromPost()
        {
            return new AddressInfo()
                       {
                           IsResidential = !WA.Parser.ToBool(GetFormValue("isBusinessAddress")).GetValueOrDefault(false),
                           FirstName = GetFormValue("firstName"),
                           LastName = GetFormValue("lastName"),
                           Email = GetFormValue("email"),
                           BusinessName = GetFormValue("businessName"),
                           Telephone = GetFormValue("telephone"),
                           Address1 = GetFormValue("address1"),
                           Address2 = GetFormValue("address2"),
                           City = GetFormValue("city"),
                           //Region = GetFormValue("region"),
                           //Region = ddlRegion.SelectedValue,
                           //Region = !string.IsNullOrEmpty(ddlRegion.SelectedValue) ? ddlRegion.SelectedValue : txtRegion.Text,
                           Region = GetFormValueForRegion(),
                           PostalCode = GetFormValue("postalCode"),
                           //Country = GetFormValue("country"),
                           Country = ddlCountry.SelectedValue
                       };
        }

        public void SetAddressInfo(AddressInfo addressInfo)
        {
            this.addressInfo = addressInfo;

            ddlCountry.TrySetSelectedValue(addressInfo.Country);

            if(!string.IsNullOrEmpty(ddlCountry.SelectedValue))
            {
                // try to fill the regions drop-down using the country code
                SetRegionListItems(DnnHelper.GetRegionListItems(ddlCountry.SelectedValue), addressInfo.Region);
            }

            //ddlRegion.TrySetSelectedValue(addressInfo.Region);
            //txtRegion.Text = addressInfo.Region;
            //if (!string.IsNullOrEmpty(addressInfo.Region) && string.IsNullOrEmpty(ddlRegion.SelectedValue))
            //{
            //    txtRegion.Style.Clear();

            //    ddlRegion.Style.Clear();
            //    ddlRegion.Style.Add("display", "none");
            //}            
        }

        private void SetCountryListItems(List<ListItem> listItems)
        {
            ddlCountry.Items.Clear();
            ddlCountry.Items.AddRange(listItems.ToArray());
            ddlCountry.Items.Insert(0, "");            
        }

        private void SetRegionListItems(List<ListItem> listItems, string selectedRegionCode)
        {
            //ddlRegion.Items.Clear();
            //ddlRegion.Items.AddRange(listItems.ToArray());
            //ddlRegion.Items.Insert(0, "");

            listItems.Insert(0, new ListItem("", ""));

            StringBuilder html = new StringBuilder();
            listItems.ForEach(option => html.AppendFormat(@"<option value=""{0}"" {2}>{1}</option>", option.Value, option.Text, option.Value == selectedRegionCode ? "selected='selected'" : ""));
            litRegionCodeOptions.Text = html.ToString();
        }

        private string GetFormValue(string name)
        {
            return Request.Form.Get(ControlPrefix + name);
        }

        private string GetFormValueForRegion()
        {
            //string ddlValue = Request.Form.Get(ddlRegion.UniqueID);
            //string textValue = txtRegion.Text;
            string ddlValue = GetFormValue("regionCode");
            string textValue = GetFormValue("regionName");

            return !string.IsNullOrEmpty(ddlValue) ? ddlValue : textValue;
        }

        //public string FirstName { get { return Request.Form.Get(ControlPrefix + "firstName"); } }
        //public string LastName { get { return Request.Form.Get(ControlPrefix + "lastName"); } }
        //public string Email { get { return Request.Form.Get(ControlPrefix + "email"); } }
        //public string BusinessName { get { return Request.Form.Get(ControlPrefix + "businessName"); } }
        //public string Telephone { get { return Request.Form.Get(ControlPrefix + "telephone"); } }
        //public string Address1 { get { return Request.Form.Get(ControlPrefix + "address1"); } }
        //public string Address2 { get { return Request.Form.Get(ControlPrefix + "address2"); } }
        //public string City { get { return Request.Form.Get(ControlPrefix + "city"); } }
        //public string Region { get { return Request.Form.Get(ControlPrefix + "region"); } }
        //public string PostalCode { get { return Request.Form.Get(ControlPrefix + "postalCode"); } }
        //public string Country { get { return Request.Form.Get(ControlPrefix + "country"); } }
    }
}