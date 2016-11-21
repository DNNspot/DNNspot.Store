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
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using DNNspot.Store.DataModel;
using DotNetNuke.Services.Cache;
using DotNetNuke.Common.Utilities;
using WA.Extensions;

namespace DNNspot.Store.Modules.Catalog
{
    public partial class CatalogCategoryMenu : StoreModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }

            RenderCategoryTree();
        }

        //private void RenderCategoryTree()
        //{
        //    //string cacheKey = string.Format("{0}CategoryTreeRenderer::Portal::{1}::CategorySlug::{2}", Constants.CacheKeyPrefix, PortalId, StoreContext.Category.Slug); 

        //    // NOTE: CACHING TURNED OFF IN ORDER TO ALLOW CATEGORY TREE TO UPDATE...PARTICULARLY WHEN CATEGORY MODULE IS SHOWN ON A PAGE OTHER THAN A PAGE WITH THE DISPATCH MODULE. ACTIVEROOT CLASS...
        //    // NOTE: ...GETS ADDED EVEN WHEN THE ACTIVE PAGE ISN'T THAT PARTICULAR CATEGORY.
        //    //string cacheKey = StoreContext.CacheKeys.Custom("TreeRenderHtml-CatSlug-" + StoreContext.Category.Slug);

        //    //string cachedHtml = CacheHelper.GetCache<string>(cacheKey);
        //    //if (!string.IsNullOrEmpty(cachedHtml))
        //    //{
        //    //    litCategoriesTree.Text = cachedHtml;
        //    //}
        //    //else
        //    //{
        //    CategoryTreeRenderer treeRenderer = new CategoryTreeRenderer(StoreContext.CurrentStore.Id.GetValueOrDefault(-1), GetTextForCategory, GetCssClassesForCategory);
        //    //treeRenderer.MaxNestingLevel = 2;
        //    string treeHtml = treeRenderer.RenderHtmlList();

        //    // cache it                
        //    //DataCache.SetCache(cacheKey, treeHtml, TimeSpan.FromMinutes(1));
        //    //DataCache.SetCache(cacheKey, treeHtml TimeSpan.FromSeconds(10));

        //    litCategoriesTree.Text = treeHtml;
        //    //}
        //}

        private void RenderCategoryTree()
        {
            //string cacheKey = string.Format("DNNspotStore:CategoryMenu:Portal:{0}:Module:{1}:Slug:{2}", PortalId, ModuleId, StoreContext.Category.Slug);
            string cacheKey = string.Format("DNNspotStore:CategoryMenu:Portal:{0}:Module:{1}", PortalId, ModuleId); 

            // NOTE: CACHING TURNED OFF IN ORDER TO ALLOW CATEGORY TREE TO UPDATE...PARTICULARLY WHEN CATEGORY MODULE IS SHOWN ON A PAGE OTHER THAN A PAGE WITH THE DISPATCH MODULE. ACTIVEROOT CLASS...
            // NOTE: ...GETS ADDED EVEN WHEN THE ACTIVE PAGE ISN'T THAT PARTICULAR CATEGORY.
            // string cacheKey = StoreContext.CacheKeys.Custom("TreeRenderHtml-CatSlug-" + StoreContext.Category.Slug);

            string cachedHtml = CacheHelper.GetCache<string>(cacheKey);
            if (!string.IsNullOrEmpty(cachedHtml))
            {
                litCategoriesTree.Text = cachedHtml;
            }
            else
            {
                CategoryTreeRenderer treeRenderer = new CategoryTreeRenderer(StoreContext.CurrentStore.Id.GetValueOrDefault(-1), GetTextForCategory, GetCssClassesForCategory);
                //treeRenderer.MaxNestingLevel = 2;
                string treeHtml = treeRenderer.RenderHtmlList();

                // cache it                
                DataCache.SetCache(cacheKey, treeHtml, TimeSpan.FromMinutes(60));

                litCategoriesTree.Text = treeHtml;
            }
        }

        private string GetTextForCategory(Category category)
        {
            //if(category.Id.Value == StoreContext.Category.Id.Value)
            //{
            //    return string.Format(@"<span>{0}</span>", category.Name);
            //}
            //else
            //{
            //    return string.Format(@"<a href=""{0}"">{1}</a>", StoreUrls.Category(category), category.Name);    
            //}
            return string.Format(@"<a href=""{0}"">{1}</a>", StoreUrls.Category(category), category.Name);
        }

        private IEnumerable<string> GetCssClassesForCategory(Category category)
        {

            //Bug: ACTIVENODE ISSUE - IF CATEGORY MODULE ISN'T ON THE DISPATCH PAGE, IT WILL INCORRECTLY SHOW THE TOP CATEGORY AS THE ACTIVE CATEGORY
            bool isOnDispatchPage = false;

            List<TabModuleMatch> dispatchTabs = DnnHelper.GetTabsWithModuleByModuleDefinitionName(PortalId, ModuleDefs.MainDispatch.DefinitionName);
            if (dispatchTabs.Count > 0)
            {
                foreach(TabModuleMatch tab in dispatchTabs)
                {
                    if(tab.TabId == TabId)
                    {
                        isOnDispatchPage = true;
                        break;
                    }
                }
            }

            //if (category.Id.Value == StoreContext.Category.Id.Value && isOnDispatchPage)
            //{
            //    return new[] {"activeNode"};
            //}
            //else if (StoreContext.CategoryBreadcrumb.Exists(c => c.Id.Value == category.Id.Value))
            //{
            //    return new[] { "activePath" };
            //}
            return new string[] { };
        }
    }
}