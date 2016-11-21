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
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Cache;
using EntitySpaces.Interfaces;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class EditCategory : StoreAdminModuleBase
    {
        protected Category category = new Category();
        protected bool isSystemCategory = false;
        protected bool isEditMode = false;

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
               {
                   new AdminBreadcrumbLink() { Text = "Categories", Url = StoreUrls.Admin(ModuleDefs.Admin.Views.Categories) },
                   new AdminBreadcrumbLink() { Text = "Add / Edit Category" }
               };
        }        

        protected int? ParamId
        {
            get { return WA.Parser.ToInt(Request.QueryString["id"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //--- Deletion
                int? deleteId = WA.Parser.ToInt(Request.QueryString["delete"]);
                if(deleteId.HasValue && DeleteCategory(deleteId.Value))
                {
                    Response.Redirect(StoreUrls.Admin(ModuleDefs.Admin.Views.Categories));
                }

                //--- Load for Edit                
                if (ParamId.HasValue && category.LoadByPrimaryKey(ParamId.GetValueOrDefault()))
                {
                    isSystemCategory = (category.Id.Value == Category.GetOrCreateHomeCategoryForStore(StoreContext.CurrentStore).Id);
                    isEditMode = true;
                    PopulateListControls();
                    FillForEdit();
                }
                else
                {
                    PopulateListControls();
                }
            }
        }

        private bool DeleteCategory(int deleteId)
        {
            Category toDelete = new Category();
            if (toDelete.LoadByPrimaryKey(deleteId))
            {                
                toDelete.MarkAsDeleted();
                toDelete.Save();

                return true;
            }
            return false;
        }

        private void PopulateListControls()
        {
            List<Category> parentCategories = CategoryCollection.GetCategoryList(StoreContext.CurrentStore.Id.GetValueOrDefault(-1), true);

            if (isEditMode)
            {
                // remove myself from the parent category list
                parentCategories.RemoveAll(c => c.Id.Value == category.Id.Value);

                // remove my descendant categories from the list
                parentCategories.RemoveAll(c => c.IsDescendantOf(category));
            }

            ddlParentId.Items.Clear();
            ddlParentId.Items.AddRange(parentCategories.ConvertAll(c => new ListItem("....".Repeat(c.GetNestingLevel()) + c.Name, c.Id.Value.ToString())).ToArray());
            ddlParentId.Items.Insert(0, new ListItem(" - none - ", ""));

            // set my parent category as selected
            if (isEditMode && category.ParentId.HasValue)
            {
                ddlParentId.TrySetSelectedValue(category.ParentId.Value.ToString());
            }
        }

        private void FillForEdit()
        {
            txtName.Text = category.Name;
            txtTitle.Text = category.Title;
            txtSlug.Text = category.Slug;
            txtDescription.Text = category.Description;
            chkIsHidden.Checked = !category.IsDisplayed.GetValueOrDefault(true);
            chkIsFeaturedCategory.Checked = category.IsSystemCategory.GetValueOrDefault(false);
            
            txtSeoTitle.Text = category.SeoTitle;
            txtSeoDescription.Text = category.SeoDescription;
            txtSeoKeywords.Text = category.SeoKeywords;            
        }

        private Category SaveCategory()
        {
            string slug = txtSlug.Text.CreateSlug();

            Category toSave = new Category();
            if (!toSave.LoadByPrimaryKey(ParamId.GetValueOrDefault(-1)))
            {

                toSave.StoreId = StoreContext.CurrentStore.Id;
                toSave.SortOrder = Category.GetNextSortOrder(toSave.StoreId.Value);
            }

            if (toSave.Slug != slug)
            {
                if (!SlugFactory.IsSlugAvailable(StoreContext.CurrentStore.Id.Value, slug))
                {
                    ShowFlash(string.Format(@"The URL name ""{0}"" is already in use for this store, please choose another.", slug));
                    return null;
                }
            }

            toSave.ParentId = ddlParentId.SelectedValueAsInt();
            toSave.Name = txtName.Text;
            toSave.Title = txtTitle.Text;
            toSave.Slug = slug;
            toSave.Description = txtDescription.Text;
            toSave.IsDisplayed = !chkIsHidden.Checked;
            toSave.IsSystemCategory = chkIsFeaturedCategory.Checked;

            toSave.SeoTitle = txtSeoTitle.Text;
            toSave.SeoDescription = txtSeoDescription.Text;
            toSave.SeoKeywords = txtSeoKeywords.Text;

            toSave.Save();
            CategoryCollection.UpdateAllNestingLevels();
            
            // clear the cache
            CacheHelper.ClearCache();

            return toSave;
                       
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Category saved = SaveCategory();
            if (saved != null)
            {
                string flashMsg = HttpUtility.UrlEncode(string.Format(@"Category ""{0}"" was saved", saved.Name));
                
                Response.Redirect(StoreUrls.AdminWithFlash(ModuleDefs.Admin.Views.Categories, flashMsg));
            }            
        }

        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            Category saved = SaveCategory();
            if (saved != null)
            {
                string flashMsg = string.Format(@"Category ""{0}"" was saved", saved.Name);

                Response.Redirect(StoreUrls.AdminEditCategory(-1, flashMsg));
            }
        }
    }
}