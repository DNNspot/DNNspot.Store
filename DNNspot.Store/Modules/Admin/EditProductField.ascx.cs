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
using EntitySpaces.Interfaces;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class EditProductField : StoreAdminModuleBase
    {
        protected ProductField productField = new ProductField();        
        protected Product product = new Product();
        //protected bool isEditMode = false;

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Products", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Products) },
                   new AdminBreadcrumbLink() { Text = product.Name, Url = StoreUrls.AdminEditProduct(product.Id.GetValueOrDefault(-1)) },
                   new AdminBreadcrumbLink() { Text = "Variant / Attribute" }
               };
        }        

        protected int? ParamId
        {
            get { return WA.Parser.ToInt(Request.QueryString["id"]); }
        }

        protected int? ParamProductId
        {
            get { return WA.Parser.ToInt(Request.QueryString["productId"]); }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // edit
            int? id = ParamId;
            if (id.HasValue && productField.LoadByPrimaryKey(id.Value))
            {
                product.LoadByPrimaryKey(productField.ProductId.Value);
            }
            else
            {
                if (ParamProductId.HasValue)
                {
                    product.LoadByPrimaryKey(ParamProductId.Value);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // delete
                int? deleteId = WA.Parser.ToInt(Request.QueryString["delete"]);
                if(deleteId.HasValue)
                {
                    ProductField toDelete = new ProductField();
                    if(toDelete.LoadByPrimaryKey(deleteId.Value))
                    {
                        int productId = toDelete.ProductId.Value;
                        toDelete.MarkAsDeleted();
                        toDelete.Save();

                        Response.Redirect(StoreUrls.AdminEditProduct(productId, "Product variant/attribute deleted"));
                    }
                }

                PopulateListControls();
                if(productField.Id.HasValue)
                {
                    FillForEdit();
                }
            }
        }

        private void PopulateListControls()
        {
            ddlWidgetType.DataSource = WA.Enum<ProductFieldWidgetType>.GetNames();
            ddlWidgetType.DataBind();            
        }

        private void FillForEdit()
        {
            liWidgetType.Visible = false;

            txtName.Text = productField.Name;
            //txtLabel.Text = productField.Label;            
            chkIsRequired.Checked = productField.IsRequired.GetValueOrDefault();
            txtPriceAdjustment.Text = productField.PriceAdjustment.HasValue ? productField.PriceAdjustment.Value.ToString("N2") : "";
            txtWeightAdjustment.Text = productField.WeightAdjustment.HasValue ? productField.WeightAdjustment.Value.ToString("N2") : "";
            
            rptFieldChoices.DataSource = productField.GetChoicesInSortOrder();
            rptFieldChoices.DataBind();

            liPrice.Visible = productField.PriceAdjustment.GetValueOrDefault(0) > 0;
            liWeight.Visible = productField.WeightAdjustment.GetValueOrDefault(0) > 0;
            liOptions.Visible = productField.CanContainChoices;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {          
            ProductField toSave = new ProductField();
            if (!toSave.LoadByPrimaryKey(ParamId.GetValueOrDefault(-1)))
            {
                // NEW field
                toSave = new ProductField();                    
                toSave.ProductId = ParamProductId.Value;
                ProductFieldWidgetType widgetType;
                if (!WA.Enum<ProductFieldWidgetType>.TryParse(ddlWidgetType.SelectedValue, out widgetType))
                {
                    throw new ArgumentException("Unable to determine WidgetType");
                }
                toSave.WidgetType = ddlWidgetType.SelectedValue;
                toSave.SortOrder = 99;
                toSave.Slug = txtName.Text.CreateSpecialSlug(true, 100);
                if (ProductField.SlugExistsForProductField(toSave.ProductId.Value, toSave.Slug))
                {
                    toSave.Slug = ProductField.GetNextAvailableSlug(toSave.ProductId.Value, toSave.Slug);
                }
            }

            toSave.Name = txtName.Text;
            toSave.IsRequired = chkIsRequired.Checked;

            switch (toSave.WidgetTypeName)
            {
                case ProductFieldWidgetType.Textbox:
                case ProductFieldWidgetType.Textarea:
                case ProductFieldWidgetType.Checkbox:
                    toSave.PriceAdjustment = WA.Parser.ToDecimal(txtPriceAdjustment.Text);
                    toSave.WeightAdjustment = WA.Parser.ToDecimal(txtWeightAdjustment.Text);                        

                    // no 'choices' for these types
                    toSave.ProductFieldChoiceCollectionByProductFieldId.MarkAllAsDeleted();                        
                    break;
                default:
                    // no field-level adjustments for these types (adjustments are done at the choice-level)
                    toSave.PriceAdjustment = null;
                    toSave.WeightAdjustment = null;

                    // save/update the 'choices'                    
                    List<string> optionNames = WA.Web.WebHelper.GetFormValuesByName("optionName", Request);
                    List<string> optionValues = WA.Web.WebHelper.GetFormValuesByName("optionValue", Request);
                    List<string> optionPriceAdjusts = WA.Web.WebHelper.GetFormValuesByName("optionPriceAdjust", Request);
                    List<string> optionWeightAdjusts = WA.Web.WebHelper.GetFormValuesByName("optionWeightAdjust", Request);
                    if (optionNames.Count == optionPriceAdjusts.Count && optionNames.Count == optionWeightAdjusts.Count)
                    {                            
                        toSave.ProductFieldChoiceCollectionByProductFieldId.MarkAllAsDeleted();                            
                        
                        for(int i = 0; i < optionNames.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(optionNames[i]))
                            {
                                ProductFieldChoice newChoice = toSave.ProductFieldChoiceCollectionByProductFieldId.AddNew();
                                newChoice.Name = optionNames[i];
                                newChoice.Value = string.IsNullOrEmpty(optionValues[i]) ? optionNames[i] : optionValues[i];
                                newChoice.PriceAdjustment = WA.Parser.ToDecimal(optionPriceAdjusts[i]);
                                newChoice.WeightAdjustment = WA.Parser.ToDecimal(optionWeightAdjusts[i]);
                                newChoice.SortOrder = (short)i;
                            }
                        }                            
                    }
                    else
                    {
                        throw new ArgumentException(
                            "Unable to parse/determine values for Product Choice Names and Adjustments");
                    }
                    break;
            }            
            toSave.Save();
  

            // redirect back to Edit Product Url
            Response.Redirect(StoreUrls.AdminEditProduct(toSave.ProductId.Value, "Product variant/attribute saved"));
        }
    }
}