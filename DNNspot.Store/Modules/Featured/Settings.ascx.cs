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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using WA.Extensions;

namespace DNNspot.Store.Modules.Featured
{
    public partial class Settings : ModuleSettingsBase 
    {
        public override void LoadSettings()
        {
            if (!IsPostBack)
            {
                DataModel.DataModel.Initialize();

                var store = StoreContext.GetCurrentStore(HttpContext.Current.Request);

                string sortByValue = Convert.ToString(ModuleSettings[FeaturedSettings.SortBy]) ?? "Name-ASC";
                var sortByFields = Product.GetSortByFields();
                ddlSortBy.Items.Clear();
                foreach(var f in sortByFields)
                {
                    ddlSortBy.Items.Add(new ListItem() { Value = f.GetValueString(), Text = f.DisplayName, Selected = f.GetValueString() == sortByValue });
                }

                //txtProductsPerPage.Text = WA.Parser.ToInt(ModuleSettings[FeaturedSettings.ProductsPerPage]).GetValueOrDefault(100).ToString();
                txtMaxProducts.Text = WA.Parser.ToInt(ModuleSettings[FeaturedSettings.MaxNumProducts]).GetValueOrDefault(25).ToString();

                rdoCategoryFilterMethod.TrySetSelectedValue(Convert.ToString(ModuleSettings[FeaturedSettings.CategoryFilterMethod]));

                var allCategories = CategoryCollection.GetCategoryList(store.Id.Value, true);
                var selectedCategoryIds = (Convert.ToString(ModuleSettings[FeaturedSettings.CategoryFilterCategoryIds]) ?? string.Empty).Split(',').Select(WA.Parser.ToInt).ToList();
                chkCategoryIds.Items.Clear();
                foreach(var cat in allCategories)
                {
                    string indent = "...".Repeat(cat.NestingLevel.GetValueOrDefault(0));
                    chkCategoryIds.Items.Add(new ListItem() { Value = cat.Id.Value.ToString(), Text = indent + cat.Name, Selected = selectedCategoryIds.Contains(cat.Id) });
                }

                // Templates
                txtTemplateHeader.Text = ModuleSettings[FeaturedSettings.TemplateHeader] != null ? ModuleSettings[FeaturedSettings.TemplateHeader].ToString() : FeaturedSettings.DefaultHeader;
                txtTemplateProduct.Text = ModuleSettings[FeaturedSettings.TemplateProduct] != null ? ModuleSettings[FeaturedSettings.TemplateProduct].ToString() : FeaturedSettings.DefaultProduct;
                txtTemplateFooter.Text = ModuleSettings[FeaturedSettings.TemplateFooter] != null ? ModuleSettings[FeaturedSettings.TemplateFooter].ToString() : FeaturedSettings.DefaultFooter;
                txtTemplateNoResults.Text = ModuleSettings[FeaturedSettings.TemplateNoResults] != null ? ModuleSettings[FeaturedSettings.TemplateNoResults].ToString() : FeaturedSettings.DefaultNoResults;
            }
        }

        public override void UpdateSettings()
        {
            try
            {
                ModuleController settings = new ModuleController();

                // save the setting
                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.SortBy.ToString(), ddlSortBy.SelectedValue);
                //settings.UpdateModuleSetting(ModuleId, FeaturedSettings.ProductsPerPage.ToString(), WA.Parser.ToInt(txtProductsPerPage.Text).GetValueOrDefault(100).ToString());
                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.MaxNumProducts.ToString(), WA.Parser.ToInt(txtMaxProducts.Text).GetValueOrDefault(100).ToString());

                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.CategoryFilterMethod.ToString(), rdoCategoryFilterMethod.SelectedValue);
                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.CategoryFilterCategoryIds.ToString(), chkCategoryIds.GetSelectedValues().ToCsv());

                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.TemplateHeader.ToString(), txtTemplateHeader.Text);
                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.TemplateProduct.ToString(), txtTemplateProduct.Text);
                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.TemplateFooter.ToString(), txtTemplateFooter.Text);
                settings.UpdateModuleSetting(ModuleId, FeaturedSettings.TemplateNoResults.ToString(), txtTemplateNoResults.Text);

                //refresh cache
                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception ex)
            {
                // let DNN handle the error
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}