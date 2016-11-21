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
using WA.Extensions;

namespace DNNspot.Store.Modules.Catalog
{
    public partial class CatalogCategory : StoreModuleBase
    {
        protected Category category = new Category();
        List<ProductSortByField> sortByFields = GetSortByFields();
        ProductSortByField sortByField = new ProductSortByField();
        protected PagedList<Product> productList = new PagedList<Product>(new Product[] { }, 0, 1);

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            category = StoreContext.Category;

            // Set the META info for the category
            DnnPage.Title = !string.IsNullOrEmpty(category.SeoTitle) ? category.SeoTitle : category.Title;
            if (!string.IsNullOrEmpty(category.SeoDescription))
            {
                DnnPage.Description = category.SeoDescription;
            }
            if (!string.IsNullOrEmpty(category.SeoKeywords))
            {
                DnnPage.KeyWords = category.SeoKeywords;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            RegisterJavascriptFileOnceInBody("js/jquery.equalHeights.js", ModuleRootWebPath + "js/jquery.equalHeights.js");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategoryProducts();
            }

            RenderBreadcrumbs();

        }

        private void LoadCategoryProducts()
        {
            int pageIndex = WA.Parser.ToInt(Request.QueryString["pg"]).GetValueOrDefault(1) - 1;
            int pageSize = WA.Parser.ToInt(StoreContext.CurrentStore.GetSetting(StoreSettingNames.CatalogMaxResultsPerPage)).GetValueOrDefault(100);

            string defaultSortName = StoreContext.CurrentStore.GetSetting(StoreSettingNames.CatalogDefaultSortOrder) ?? "";
            ProductSortByField defaultSortField = sortByFields.Find(f => f.DisplayName == defaultSortName);

            sortByField = ProductSortByField.FromString(Request.QueryString["sb"]) ?? defaultSortField ?? sortByFields[0];


            List<Product> products = category.GetProducts(sortByField);

            if (products.Count > 0)
            {
                productList = new PagedList<Product>(products, pageIndex, pageSize);
                rptCategoryProducts.DataSource = productList;
                rptCategoryProducts.DataBind();

                if (products.Count > pageSize)
                {
                    RenderPaginationLinks(productList);
                }
            }
            else
            {
                pnlCategoryProductResults.Visible = false;
            }
            pnlNoResults.Visible = !pnlCategoryProductResults.Visible;
        }

        private void RenderPaginationLinks(PagedList<Product> pagedList)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<label>Page:</label> <ul class='pager'>");
            for (int i = 1; i <= pagedList.PageCount; i++)
            {
                html.AppendFormat(@"<li class=""{2}""><a href=""{0}"">{1}</a></li>", StoreUrls.Category(category, sortByField, i), i, i == pagedList.PageNumber ? "current" : string.Empty);
            }
            html.Append("</ul>");
            litPaginationLinks.Text = html.ToString();
        }

        private void RenderBreadcrumbs()
        {
            const string crumbSeparator = "&nbsp;&raquo;&nbsp;";

            // we're going to modify the list, make a copy so we don't affect other code
            Category[] crumbs = StoreContext.CategoryBreadcrumb.ToArray();
            if (crumbs.Length > 1)
            {
                //crumbs.RemoveAt(crumbs.Count - 1);                

                StringBuilder html = new StringBuilder();
                //foreach (Category c in crumbs)
                for (int i = 0; i < crumbs.Length - 1; i++)
                {
                    Category c = crumbs[i];
                    html.AppendFormat(@"<a href=""{0}"">{1}</a>{2}", StoreUrls.Category(c), c.Name, crumbSeparator);
                }
                html.AppendFormat(@"{0}", category.Name);

                litBreadcrumb.Text = "<div class='breadcrumbs'>" + html.ToString() + "</div>";
            }
        }

        public static List<ProductSortByField> GetSortByFields()
        {
            List<ProductSortByField> fields = new List<ProductSortByField>();

            fields.Add(new ProductSortByField() { DisplayName = "Sku", Field = ProductMetadata.ColumnNames.Sku, SortDirection = SortDirection.ASC });
            fields.Add(new ProductSortByField() { DisplayName = "Name", Field = ProductMetadata.ColumnNames.Name, SortDirection = SortDirection.ASC });
            fields.Add(new ProductSortByField() { DisplayName = "Bestselling", Field = "NumSold", SortDirection = SortDirection.DESC });
            fields.Add(new ProductSortByField() { DisplayName = "Price - Low to High", Field = ProductMetadata.ColumnNames.Price, SortDirection = SortDirection.ASC });
            fields.Add(new ProductSortByField() { DisplayName = "Price - High to Low", Field = ProductMetadata.ColumnNames.Price, SortDirection = SortDirection.DESC });
            fields.Add(new ProductSortByField() { DisplayName = "Most Recent", Field = ProductMetadata.ColumnNames.CreatedOn, SortDirection = SortDirection.DESC });

            return fields;
        }

        protected List<string> GetSortByFieldLinks()
        {
            return sortByFields.ConvertAll(f => string.Format(@"<a href=""{0}"" title=""{1}"" rel=""nofollow"" {2}>{1}</a>", StoreUrls.Category(category, f), f.DisplayName, sortByField.GetValueString() == f.GetValueString() ? String.Format("class=\"selected {0}\"", f.GetValueString()) : String.Format("{0}", f.GetValueString())));
        }

        //protected void ddlProductSort_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ProductSortByField sortBy = ProductSortByField.FromString(ddlProductSortBy.SelectedValue) ?? sortByFields[0];

        //    Response.Redirect(StoreUrls.Category(category, sortBy));
        //}
    }
}