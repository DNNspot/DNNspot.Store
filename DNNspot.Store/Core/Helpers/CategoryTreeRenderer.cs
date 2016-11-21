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
using DNNspot.Store.DataModel;
using WA.Extensions;

namespace DNNspot.Store
{
    public class CategoryTreeRenderer
    {
        public delegate IEnumerable<string> GetCssClassesForCategoryDelegate(Category category);
        public delegate string GetNameForCategoryDelegate(Category category);

        GetCssClassesForCategoryDelegate cssCallback;
        GetNameForCategoryDelegate nameCallback;
        const string htmlPrefixCategoryId = "catId-";
        string containingElementTag = "ol";
        string itemElementTag = "li";
        int storeId = -1;

        public string ContainingElementTag
        {
            get { return containingElementTag; }
            set { containingElementTag = value; }
        }

        public string ItemElementTag
        {
            get { return itemElementTag; }
            set { itemElementTag = value; }
        }

        public string CssClassForOuterList { get; set; }
        public short? MaxNestingLevel { get; set; }
        public bool IncludeHiddenCategories { get; set; }

        public CategoryTreeRenderer(int storeId)
        {
            Init(storeId, null, null, "");
        }

        public CategoryTreeRenderer(int storeId, GetNameForCategoryDelegate nameCallback)
        {
            Init(storeId, nameCallback, null, "");
        }

        public CategoryTreeRenderer(int storeId, GetNameForCategoryDelegate nameCallback, GetCssClassesForCategoryDelegate cssCallback)
        {
            Init(storeId, nameCallback, cssCallback, "");
        }

        private void Init(int storeId, GetNameForCategoryDelegate nameCallback, GetCssClassesForCategoryDelegate cssCallback, string cssClass)
        {
            this.storeId = storeId;
            this.nameCallback = nameCallback;
            this.cssCallback = cssCallback;

            this.CssClassForOuterList = cssClass ?? "";
            this.IncludeHiddenCategories = false;
        }

        public string RenderHtmlList()
        {
            StringBuilder html = new StringBuilder();

            AppendChildren(null, ref html);

            return html.ToString();
        }

        private void AppendChildren(Category parent, ref StringBuilder html)
        {
            string rootCss = "";
            List<Category> childCategories;
            if(parent == null)
            {
                //childCategories = RootCategories;
                childCategories = CategoryCollection.GetTopLevelCategories(storeId, IncludeHiddenCategories);
                rootCss = CssClassForOuterList;
            }
            else
            {
                childCategories = parent.GetChildCategoriesInSortedOrder(IncludeHiddenCategories).ToList();
            }
            
            if (childCategories.Count > 0)
            {
                html.AppendFormat("<{0}{1}>", containingElementTag, !string.IsNullOrEmpty(rootCss) ? " class=\"" + rootCss + "\"" : "");
                foreach (Category child in childCategories)
                {
                    html.AppendFormat(@"<{0} id=""{1}""{2}>{3}", itemElementTag, htmlPrefixCategoryId + child.Id, GetCategoryCssAttribute(child), GetCategoryName(child));
                    if (!MaxNestingLevel.HasValue || (child.NestingLevel.Value < MaxNestingLevel.Value))
                    {
                        AppendChildren(child, ref html);
                    }
                    html.AppendFormat("</{0}>", itemElementTag);
                }
                html.AppendFormat("</{0}>", containingElementTag);
            }
        }

        private string GetCategoryCssAttribute(Category cat)
        {
            List<string> cssClasses = new List<string>();
            if (cat.NestingLevel == 0)
            {
                cssClasses.Add("root");
            }
            if (!cat.IsDisplayed.Value)
            {
                cssClasses.Add("hidden");
            }
            if (cssCallback != null)
            {
                cssClasses.AddRange(cssCallback(cat));
            }

            return cssClasses.Count > 0 ? string.Format(@" class=""{0}""", cssClasses.ToDelimitedString(" ")) : "";            
        }

        private string GetCategoryName(Category cat)
        {
            string name = cat.Name;
            if (nameCallback != null)
            {
                name = nameCallback(cat);
            }
            return name;
        }
    }
}
