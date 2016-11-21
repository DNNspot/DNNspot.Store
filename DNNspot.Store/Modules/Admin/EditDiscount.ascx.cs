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
using DotNetNuke.Security.Roles;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class EditDiscount : StoreAdminModuleBase
    {
        protected Discount discount = new Discount();

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Discounts", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Discounts) },
                   new AdminBreadcrumbLink() { Text = "Add / Edit Discount" }
               };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillListControls();

                int? id = WA.Parser.ToInt(Request.QueryString["id"]);
                if (discount.LoadByPrimaryKey(id.GetValueOrDefault(-1)))
                {
                    FillEditForm();
                }

                int? deleteId = WA.Parser.ToInt(Request.QueryString["delete"]);
                if (deleteId.HasValue)
                {
                    Discount toDelete = new Discount();
                    if (toDelete.LoadByPrimaryKey(deleteId.Value))
                    {
                        toDelete.MarkAsDeleted(); ;
                        toDelete.Save();

                        Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.Discounts, "Discount Deleted"));
                    }
                }
            }
        }

        private void FillListControls()
        {
            RoleController roleController = new RoleController();
            List<RoleInfo> roleInfos = roleController.GetPortalRoles(PortalId).ToList<RoleInfo>();
            ddlDnnRole.DataSource = roleInfos;
            ddlDnnRole.DataValueField = "RoleID";
            ddlDnnRole.DataTextField = "RoleName";
            ddlDnnRole.DataBind();
            ddlDnnRole.Items.Insert(0, new ListItem() { Value = "-1", Text = "All Users" });
            ddlDnnRole.Items.Insert(0, "");

            List<Product> products = ProductCollection.GetAll(StoreContext.CurrentStore.Id.Value);
            chkAppliesToProducts.DataSource = products;
            chkAppliesToProducts.DataValueField = ProductMetadata.PropertyNames.Id;
            chkAppliesToProducts.DataTextField = ProductMetadata.PropertyNames.Name;
            chkAppliesToProducts.DataBind();

            List<Category> categories = CategoryCollection.GetCategoryList(StoreContext.CurrentStore.Id.Value, true);
            chkAppliesToCategories.Items.Clear();
            foreach(var c in categories)
            {
                string indent = "...".Repeat(c.NestingLevel.GetValueOrDefault(0));
                chkAppliesToCategories.Items.Add(new ListItem() { Value = c.Id.ToString(), Text = indent + c.Name });
            }
        }

        private void FillEditForm()
        {
            chkIsActive.Checked = discount.IsActive.GetValueOrDefault(true);
            txtName.Text = discount.Name;
            ddlDnnRole.TrySetSelectedValue(discount.DnnRoleId.HasValue ? discount.DnnRoleId.ToString() : "-1");
            chkIsCombinable.Checked = discount.IsCombinable.GetValueOrDefault(false);

            txtDiscountNumber.Text = discount.IsPercentOff ? discount.PercentOff.Value.ToString("N2") : discount.AmountOff.Value.ToString("N2");
            if (discount.IsPercentOff)
            {
                rdoDiscountNumberType.TrySetSelectedValue("percent");
            }
            else if (discount.IsAmountOff)
            {
                rdoDiscountNumberType.TrySetSelectedValue("amount");
            }

            if (discount.DiscountTypeName == DiscountDiscountType.Product)
            {
                chkAppliesToProducts.SetSelectedValues(discount.GetProductIds());
            }
            if (discount.DiscountTypeName == DiscountDiscountType.Category)
            {
                chkAppliesToCategories.SetSelectedValues(discount.GetCategoryIds());
            }

            txtValidDateFrom.Text = discount.ValidFromDate.HasValue ? discount.ValidFromDate.Value.ToShortDateString() : "";
            txtValidDateTo.Text = discount.ValidToDate.HasValue ? discount.ValidToDate.Value.ToShortDateString() : "";
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            int? id = WA.Parser.ToInt(Request.QueryString["id"]);

            Discount toSave = new Discount();
            if (!toSave.LoadByPrimaryKey(id.GetValueOrDefault(-1)))
            {
                toSave.StoreId = StoreContext.CurrentStore.Id.Value;
            }

            toSave.IsActive = chkIsActive.Checked;
            toSave.Name = txtName.Text;
            toSave.DnnRoleId = ddlDnnRole.SelectedValueAsInt();
            if(toSave.DnnRoleId == -1) // special case for the "All Users" Role, which doesn't actually exist in the DNN Roles table
            {
                toSave.DnnRoleId = null;
            }
            toSave.IsCombinable = chkIsCombinable.Checked;

            decimal discountNumber = WA.Parser.ToDecimal(txtDiscountNumber.Text).GetValueOrDefault(0);
            if (rdoDiscountNumberType.SelectedValue == "percent")
            {
                toSave.PercentOff = discountNumber;
                toSave.AmountOff = null;
            }
            else if (rdoDiscountNumberType.SelectedValue == "amount")
            {
                toSave.PercentOff = null;
                toSave.AmountOff = discountNumber;
            }

            toSave.DiscountTypeName = WA.Enum<DiscountDiscountType>.TryParseOrDefault(Request.Form["rdoAppliesTo"], DiscountDiscountType.UNKNOWN);
            if (toSave.DiscountTypeName == DiscountDiscountType.AllProducts)
            {
                toSave.AppliesToProductIds = "";
                toSave.AppliesToCategoryIds = "";
            }
            else if (toSave.DiscountTypeName == DiscountDiscountType.Product)
            {
                toSave.SetProductIds(chkAppliesToProducts.GetSelectedValues());
                toSave.AppliesToCategoryIds = "";
            }
            else if (toSave.DiscountTypeName == DiscountDiscountType.Category)
            {
                toSave.AppliesToProductIds = "";
                toSave.SetCategoryIds(chkAppliesToCategories.GetSelectedValues());
            }

            toSave.ValidFromDate = WA.Parser.ToDateTime(txtValidDateFrom.Text);
            toSave.ValidToDate = WA.Parser.ToDateTime(txtValidDateTo.Text);

            toSave.Save();

            Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.Discounts, "Discount Saved Successfully"));
        }
    }
}