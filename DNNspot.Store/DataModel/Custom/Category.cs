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

/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/12/2013 3:32:32 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class Category : esCategory
	{
		public Category()
		{

        }

        public override void MarkAsDeleted()
        {
            this.CategoryCollectionByParentId.MarkAllAsDeleted();

            base.MarkAsDeleted();
        }

        public bool HasChildren
        {
            get { return CategoryCollectionByParentId.Count > 0; }
        }

        public bool LoadBySlug(int storeId, string slug)
        {
            if (!string.IsNullOrEmpty(slug))
            {
                return this.Load(CategoryQuery.FindSingleBySlug(storeId, slug));
            }
            return false;
        }

        public static bool SlugExists(int storeId, string slug)
        {
            slug = slug.ToLower();

            // method for testing 'exists' as recommended here: http://community.entityspaces.net/forums/16358/ShowThread.aspx#16358
            CategoryQuery q = new CategoryQuery();
            q.es.CountAll = true;
            q.Where(q.StoreId == storeId, q.Slug == slug);
            int count = (int)q.ExecuteScalar();

            return (count == 1);
        }

        public static Category GetByName(int storeId, string name)
        {
            var q = new CategoryQuery();
            q.Where(q.StoreId == storeId, q.Name == name.Trim());

            var c = new Category();
            return c.Load(q) ? c : null;
        }

        public static Category GetBySlug(int storeId, string slug)
        {
            Category category = new Category();
            return category.LoadBySlug(storeId, slug) ? category : null;
        }

        public void SortChildCategories()
        {
            this.CategoryCollectionByParentId.Filter = this.CategoryCollectionByParentId.AsQueryable().OrderBy(x => x.SortOrder).ThenBy(x => x.Name);
            //this.CategoryCollectionByParentId.Sort = string.Format(@"{0} ASC, {1} ASC", CategoryMetadata.PropertyNames.SortOrder, CategoryMetadata.PropertyNames.Name);
        }

        public CategoryCollection GetChildCategoriesInSortedOrder(bool includeHidden)
        {
            //SortChildCategories();
            //return this.CategoryCollectionByParentId;

            CategoryQuery q = new CategoryQuery();
            q.Where(q.ParentId == this.Id.Value);
            if (!includeHidden)
            {
                q.Where(q.IsDisplayed == true);
            }
            q.OrderBy(q.SortOrder.Ascending, q.Name.Ascending);

            CategoryCollection collection = new CategoryCollection();
            collection.Load(q);

            return collection;
        }

        public static Category GetOrCreateHomeCategoryForStore(Store store)
        {
            CategoryQuery q = new CategoryQuery();
            q.es.Top = 1;
            q.Where(q.StoreId == store.Id.Value, q.ParentId.IsNull(), q.IsSystemCategory == true);

            Category homeCategory = new Category();
            if (!homeCategory.Load(q))
            {
                homeCategory.StoreId = store.Id.Value;
                homeCategory.Name = "Store";
                homeCategory.Title = store.Name;
                homeCategory.Slug = "store";
                homeCategory.Description = "Welcome to our online store!";
                homeCategory.ParentId = null;
                homeCategory.NestingLevel = 0;
                homeCategory.IsDisplayed = true;
                homeCategory.IsSystemCategory = true;
                homeCategory.Save();
            }
            return homeCategory;
        }

        //public static short GetNestingLevelByParentId(int? parentId)
        //{
        //    if (!parentId.HasValue)
        //    {
        //        // this is a top-level/root node
        //        return 0;
        //    }

        //    // walk up the parent hierarchy...
        //    short level = 0;
        //    Category parent = new Category();
        //    if (parent.LoadByPrimaryKey(parentId.Value))
        //    {
        //        while (parent != null)
        //        {
        //            level++;
        //            parent = parent.UpToCategoryByParentId;
        //        }
        //    }
        //    return level;            
        //}

        public short GetNestingLevel()
        {
            if (!this.ParentId.HasValue)
            {
                // this is a top-level/root node
                return 0;
            }

            // walk up the parent hierarchy...
            short level = 0;
            Category parent = this.UpToCategoryByParentId;
            while (parent != null)
            {
                level++;
                parent = parent.UpToCategoryByParentId;
            }

            return level;
        }

        public List<Category> GetAllParents()
        {
            if (!this.ParentId.HasValue)
            {
                // this is a top-level/root node, it does not have parents
                return new List<Category>();
            }

            // walk up the parent hierarchy...
            List<Category> parentCats = new List<Category>();
            Category parent = this.UpToCategoryByParentId;
            while (parent != null)
            {
                parentCats.Insert(0, parent);
                parent = parent.UpToCategoryByParentId;
            }
            return parentCats;
        }

        public bool IsDescendantOf(Category category)
        {
            if (this.Id.Value == category.Id.Value)
            {
                // can't be a descendant of yourself
                return false;
            }

            if (!this.ParentId.HasValue)
            {
                // this is a top-level/root node, so it can't be a descendant of anyone
                return false;
            }

            if (this.ParentId.Value == category.Id.Value)
            {
                // this is a direct child
                return true;
            }

            // look through the list of parent categories
            List<Category> allParents = GetAllParents();
            if (allParents.Exists(c => c.Id.Value == category.Id.Value))
            {
                return true;
            }
            return false;
        }

        public List<Product> GetProducts()
        {
            return this.UpToProductCollectionByProductCategory.ToList();
        }

        public List<Product> GetProducts(ProductSortByField productSortByField)
        {
            if (this.Id.HasValue)
            {
                return ProductCollection.FindProductsByCategory(this.Id.Value, productSortByField);
            }
            else
            {
                return null;
            }
        }

	    public static short GetNextSortOrder(int storeId)
        {
            //SELECT
            //MAX(SortOrder)
            //FROM DNNspot_Store_Category
            //WHERE StoreId = 1            

            var q = new CategoryQuery();
            q.es.Top = 1;
            q.Select(q.SortOrder.Max().As("MaxSortValue")).Where(q.StoreId == storeId);

            var x = new Category();
            if (x.Load(q))
            {
                short next = WA.Parser.ToShort(x.GetColumn("MaxSortValue")).Value;
                return (short)(next + 1);
            }
            return 9999;
        }

        public static void MoveCategoryUp(int categoryId)
        {
            Category source = new Category();
            if (source.LoadByPrimaryKey(categoryId))
            {
                //SELECT TOP 1 *
                //FROM DNNspot_Store_Category WHERE StoreId = 1 AND ParentId IS NULL AND SortOrder <= 6 AND Id <> 34
                //ORDER BY SortOrder DESC
                CategoryQuery q = new CategoryQuery();
                q.es.Top = 1;
                q.Where(q.StoreId == source.StoreId);
                if (source.ParentId.HasValue)
                {
                    q.Where(q.ParentId == source.ParentId.Value);
                }
                else
                {
                    q.Where(q.ParentId.IsNull());
                }
                q.Where(q.SortOrder <= source.SortOrder);
                q.Where(q.Id != categoryId);
                q.OrderBy(q.SortOrder.Descending);

                Category dest = new Category();
                if (dest.Load(q))
                {
                    SwapAndReNumberSortOrder(source, dest);
                }
            }
        }

        private static void SwapAndReNumberSortOrder(Category source, Category dest)
        {
            List<Category> allCats = CategoryCollection.GetCategoryList(source.StoreId.Value, true);



            int sourceIndex = allCats.FindIndex(x => x.Id == source.Id);
            int destIndex = allCats.FindIndex(x => x.Id == dest.Id);
            if (sourceIndex > -1 && destIndex > -1)
            {
                var swap = source;
                allCats[sourceIndex] = allCats[destIndex];
                allCats[destIndex] = swap;

                for (short i = 0; i < allCats.Count; i++)
                {
                    var c = new Category();
                    if (c.LoadByPrimaryKey(allCats[i].Id.Value))
                    {
                        c.SortOrder = i;
                        c.Save();
                    }
                    //allCats[i].SortOrder = i;
                    //allCats[i].Save();
                }
            }
        }

        public static void MoveCategoryDown(int categoryId)
        {
            Category source = new Category();
            if (source.LoadByPrimaryKey(categoryId))
            {
                //SELECT TOP 1 *
                //FROM DNNspot_Store_Category WHERE StoreId = 1 AND ParentId IS NULL AND SortOrder >= 3 AND Id <> 33
                //ORDER BY SortOrder ASC
                CategoryQuery q = new CategoryQuery();
                q.es.Top = 1;
                q.Where(q.StoreId == source.StoreId);
                if (source.ParentId.HasValue)
                {
                    q.Where(q.ParentId == source.ParentId.Value);
                }
                else
                {
                    q.Where(q.ParentId.IsNull());
                }
                q.Where(q.SortOrder >= source.SortOrder);
                q.Where(q.Id != categoryId);
                q.OrderBy(q.SortOrder.Ascending);

                Category dest = new Category();
                if (dest.Load(q))
                {
                    SwapAndReNumberSortOrder(source, dest);
                }
            }
        }
	}
}
