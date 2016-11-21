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
using DotNetNuke.Common.Utilities;
using WA.Extensions;

namespace DNNspot.Store.Modules.Admin
{
    public partial class Categories : StoreAdminModuleBase
    {
        protected bool dragDropSupported = false;

        public override List<AdminBreadcrumbLink> GetBreadcrumbs()
        {
            return new List<AdminBreadcrumbLink>()
                       {
                           new AdminBreadcrumbLink() { Text = "Categories" },
                       };
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //dragDropSupported = (BrowserName.Contains("Firefox") || BrowserName.Contains("Chrome"));
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if(dragDropSupported)
            {
                RegisterJavascriptFileOnceInBody("js/AdminCategoriesSortable.js", ModuleRootWebPath + "js/AdminCategoriesSortable.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Category.GetOrCreateHomeCategoryForStore(StoreContext.CurrentStore);

                int? moveIdUp = WA.Parser.ToInt(Request.QueryString["moveUp"]);
                if(moveIdUp.HasValue)
                {
                    Category.MoveCategoryUp(moveIdUp.Value);
                    CacheHelper.ClearCache();
                }
                int? moveIdDown = WA.Parser.ToInt(Request.QueryString["moveDown"]);
                if (moveIdDown.HasValue)
                {
                    Category.MoveCategoryDown(moveIdDown.Value);
                    CacheHelper.ClearCache();
                }

                LoadCategoriesUI();
            }
        }

        private void LoadCategoriesUI()
        {
            StringBuilder html = new StringBuilder();

            CategoryTreeRenderer treeRenderer = new CategoryTreeRenderer(
                StoreContext.CurrentStore.Id.GetValueOrDefault(-1),
                c => string.Format(@"<span> <span class=""edit"">{0}</span> <span class=""name"">{1}</span> <span class=""move"">{2}</span> <span class=""delete"">{3}</span> </span>",
                                    CategoryEditLink(c.Id.Value),
                                    //string.Format(@"<a href=""{0}"" target=""_blank"">{1}</a>", StoreUrls.Category(c), c.Name) + (!c.IsDisplayed.Value ? " [Hidden]" : ""),
                                    // NOTE - removed the link because it crashes if the 'main dispatch' module has not been added to a page yet!
                                    c.Name + (!c.IsDisplayed.Value ? " [Hidden]" : string.Empty),
                                    CategoryMoveHandle(c.Id.Value),
                                    CategoryDeleteLink(c))
                    , CategoryCssClasses
            );
            treeRenderer.CssClassForOuterList = "catDivs";
            treeRenderer.ContainingElementTag = "div";
            treeRenderer.ItemElementTag = "div";
            treeRenderer.IncludeHiddenCategories = true;

            litCategories.Text = treeRenderer.RenderHtmlList();                       
        }

        private List<string> CategoryCssClasses(Category c)
        {
            List<string> cssClasses = new List<string>();

            if(c.NestingLevel.HasValue && c.NestingLevel.Value > 0)
            {
                cssClasses.Add(string.Format("level{0}", c.NestingLevel));
            }

            return cssClasses;
        }

        private string CategoryEditLink(int categoryId)
        {
            return string.Format(@"<a href=""{0}""><img src=""{1}icons/edit.png"" title=""edit"" alt=""edit"" /></a>", StoreUrls.AdminEditCategory(categoryId), ModuleRootImagePath);
        }

        private string CategoryMoveHandle(int categoryId)
        {
            if (dragDropSupported)
            {
                return string.Format(@"<img src=""{0}icons/move.png"" class=""moveHandle"" alt=""move"" title=""move"" />", ModuleRootImagePath);
            }
            else
            {
                string moveUp = string.Format(@"<a href=""{1}"" class=""moveUp""><img src=""{0}icons/arrow_up.png"" alt=""move up"" title=""move up"" /></a>", ModuleRootImagePath, StoreUrls.Admin(ModuleDefs.Admin.Views.Categories, "moveUp=" + categoryId));
                string moveDown = string.Format(@"<a href=""{1}""class=""moveDown""><img src=""{0}icons/arrow_down.png"" alt=""move down"" title=""move down"" /></a>", ModuleRootImagePath, StoreUrls.Admin(ModuleDefs.Admin.Views.Categories, "moveDown=" + categoryId));

                return moveUp + moveDown;
            }
        }

        private string CategoryDeleteLink(Category category)
        {
            if (!category.IsSystemCategory.GetValueOrDefault(false))
            {
                return
                    string.Format(
                        @"<a href=""{0}"" onclick=""return confirm('Deleting a category will also delete all subcategories, are you sure?');""><img src=""{1}icons/delete.png"" title=""delete"" alt=""delete"" /></a>",
                        StoreUrls.AdminDeleteCategory(category.Id.Value), ModuleRootImagePath);
            }
            return "";
        }
    }    
}