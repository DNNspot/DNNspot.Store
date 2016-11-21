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
using DNNspot.Store.Shipping;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class EditCoupon : StoreAdminModuleBase
    {
        protected Coupon coupon = new Coupon();

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Coupons", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Coupons) },
                   new AdminBreadcrumbLink() { Text = "Add / Edit Coupon" }
               };
        }        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillListControls();

                int? id = WA.Parser.ToInt(Request.QueryString["id"]);
                if(coupon.LoadByPrimaryKey(id.GetValueOrDefault(-1)))
                {
                    FillEditForm();
                }

                int? deleteId = WA.Parser.ToInt(Request.QueryString["delete"]);
                if (deleteId.HasValue)
                {
                    Coupon toDelete = new Coupon();
                    if(toDelete.LoadByPrimaryKey(deleteId.Value))
                    {
                        toDelete.MarkAsDeleted(); ;
                        toDelete.Save();

                        Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.Coupons, "Coupon Deleted"));
                    }
                }
            }
        }

        private void FillListControls()
        {
            List<Product> products = ProductCollection.GetAll(StoreContext.CurrentStore.Id.Value);
            chkAppliesToProducts.DataSource = products;
            chkAppliesToProducts.DataValueField = ProductMetadata.PropertyNames.Id;
            chkAppliesToProducts.DataTextField = ProductMetadata.PropertyNames.Name;
            chkAppliesToProducts.DataBind();

            //var shippingOptions = StoreContext.CurrentStore.GetShippingOptions();
            //chkAppliesToShipping.Items.Clear();
            //chkAppliesToShipping.Items.AddRange(HtmlHelper.GetShippingOptionsAsListItems(shippingOptions).ToArray());

            var store = StoreContext.CurrentStore;
            IPostalAddress origin = store.Address.AddressFieldsAreEmpty ? new PostalAddress() { PostalCode = "48375" } : StoreContext.CurrentStore.Address.ToPostalAddress(); 
            // ---Throwing error that no destination was set when UPS shipping was turned on
            //IPostalAddress destination = new PostalAddress() { PostalCode = origin.PostalCode };
            IPostalAddress destination = origin;
            var shipmentPackages = new List<IShipmentPackageDetail>() { new ShipmentPackageDetail() { Weight = 1, Length = 8, Width = 4, Height = 4 } };
            var shippingRates = new List<IShippingRate>();
            var shippingServices = StoreContext.CurrentStore.GetEnabledShippingProviders();
            foreach (var shipper in shippingServices)
            {
                shippingRates.AddRange(shipper.GetRates(origin, destination, shipmentPackages));
            }
            var options = new List<ListItem>();            
            shippingRates.ForEach(rt => options.Add(new ListItem()
            {
                Value = rt.ServiceType,
                Text = rt.ServiceType
            }
            ));
            chkAppliesToShipping.Items.Clear();
            chkAppliesToShipping.Items.AddRange(options.ToArray());
        }

        private void FillEditForm()
        {
            chkIsActive.Checked = coupon.IsActive.GetValueOrDefault(false);
            txtCode.Text = coupon.Code;
            txtDescriptionForCustomer.Text = coupon.DescriptionForCustomer;
            chkIsCombinable.Checked = coupon.IsCombinable.GetValueOrDefault(false);

            txtDiscountNumber.Text = coupon.IsPercentOff ? coupon.PercentOff.Value.ToString("N2") : coupon.AmountOff.Value.ToString("N2");
            if(coupon.IsPercentOff)
            {
                rdoDiscountNumberType.TrySetSelectedValue("percent");
            }
            else if(coupon.IsAmountOff)
            {
                rdoDiscountNumberType.TrySetSelectedValue("amount");
            }

            if (coupon.DiscountTypeName == CouponDiscountType.Product)
            {                
                chkAppliesToProducts.SetSelectedValues(coupon.GetProductIds());
            }
            if (coupon.DiscountTypeName == CouponDiscountType.Shipping)
            {
                chkAppliesToShipping.SetSelectedValues(coupon.GetShippingRateTypes());
            }

            txtValidDateFrom.Text = coupon.ValidFromDate.HasValue ? coupon.ValidFromDate.Value.ToShortDateString() : "";
            txtValidDateTo.Text = coupon.ValidToDate.HasValue ? coupon.ValidToDate.Value.ToShortDateString() : "";

            txtMinOrderAmount.Text = coupon.MinOrderAmount.HasValue ? coupon.MinOrderAmount.Value.ToString("N2") : "";
            txtMaxUsesPerUser.Text = coupon.MaxUsesPerUser.HasValue ? coupon.MaxUsesPerUser.Value.ToString() : "";
            txtMaxUsesLifetime.Text = coupon.MaxUsesLifetime.HasValue ? coupon.MaxUsesLifetime.Value.ToString() : "";
            txtMaxDiscountAmountPerOrder.Text = coupon.MaxDiscountAmountPerOrder.HasValue ? coupon.MaxDiscountAmountPerOrder.Value.ToString("N2") : "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? id = WA.Parser.ToInt(Request.QueryString["id"]);

            Coupon toSave = new Coupon();
            if (!toSave.LoadByPrimaryKey(id.GetValueOrDefault(-1)))
            {                    
             
                toSave.StoreId = StoreContext.CurrentStore.Id.Value;
            }

            toSave.IsActive = chkIsActive.Checked;
            toSave.Code = txtCode.Text;
            toSave.DescriptionForCustomer = txtDescriptionForCustomer.Text;
            toSave.IsCombinable = chkIsCombinable.Checked;

            decimal discountNumber = WA.Parser.ToDecimal(txtDiscountNumber.Text).GetValueOrDefault(0);
            if(rdoDiscountNumberType.SelectedValue == "percent")
            {
                toSave.PercentOff = discountNumber;
                toSave.AmountOff = null;
            }
            else if(rdoDiscountNumberType.SelectedValue == "amount")
            {
                toSave.PercentOff = null;
                toSave.AmountOff = discountNumber;
            }

            toSave.DiscountTypeName = WA.Enum<CouponDiscountType>.TryParseOrDefault(Request.Form["rdoAppliesTo"], CouponDiscountType.UNKNOWN);
            if(toSave.DiscountTypeName == CouponDiscountType.SubTotal || toSave.DiscountTypeName == CouponDiscountType.SubTotalAndShipping)
            {
                toSave.AppliesToProductIds = "";
                toSave.AppliesToShippingRateTypes = "";
            }
            else if (toSave.DiscountTypeName == CouponDiscountType.Product)
            {                    
                toSave.SetProductIds(chkAppliesToProducts.GetSelectedValues());
                toSave.AppliesToShippingRateTypes = "";
            }
            else if (toSave.DiscountTypeName == CouponDiscountType.Shipping)
            {
                toSave.AppliesToProductIds = "";
                toSave.SetShippingRateTypes(chkAppliesToShipping.GetSelectedValues().ToList());
            }
            
            toSave.ValidFromDate = WA.Parser.ToDateTime(txtValidDateFrom.Text);
            toSave.ValidToDate = WA.Parser.ToDateTime(txtValidDateTo.Text);

            toSave.MinOrderAmount = WA.Parser.ToDecimal(txtMinOrderAmount.Text);                               
            toSave.MaxUsesPerUser = WA.Parser.ToInt(txtMaxUsesPerUser.Text);
            toSave.MaxUsesLifetime = WA.Parser.ToInt(txtMaxUsesLifetime.Text);
            toSave.MaxDiscountAmountPerOrder = WA.Parser.ToDecimal(txtMaxDiscountAmountPerOrder.Text);                                

            toSave.Save();
            
            Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.Coupons, "Coupon Saved Successfully"));
        }
    }
}