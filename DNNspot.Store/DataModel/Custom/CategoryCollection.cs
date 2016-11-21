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
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class CategoryCollection : esCategoryCollection
	{
		public CategoryCollection()
		{

        }
        public static List<Category> GetTopLevelCategories(int storeId, bool includeHidden)
        {
            CategoryQuery qry = new CategoryQuery();
            qry.Where(qry.StoreId == storeId);
            qry.Where(qry.ParentId.IsNull());
            if (!includeHidden)
            {
                qry.Where(qry.IsDisplayed == true);
            }
            qry.OrderBy(qry.SortOrder.Ascending, qry.Name.Ascending);

            CategoryCollection rootCategories = new CategoryCollection();
            rootCategories.Load(qry);

            List<Category> catList = new List<Category>(rootCategories.Count);
            foreach (var c in rootCategories)
            {
                catList.Add(c);
            }

            //return rootCategories;
            return catList;
        }

        //public static List<Category> GetCategoriesInSortOrder()
        //{
        //    CategoryCollection categories = new CategoryCollection();
        //    categories.Query.OrderBy(categories.Query.SortOrder.Ascending);
        //    categories.Query.Load();

        //    return categories;
        //}

        public static List<Category> GetCategoryList(int storeId, bool includeHidden)
        {
            List<Category> list = new List<Category>();

            List<Category> rootCats = GetTopLevelCategories(storeId, includeHidden);
            foreach (Category root in rootCats)
            {
                list.Add(root);
                AddChildCategoriesToList(root, ref list, includeHidden);
            }

            return list;
        }

        private static void AddChildCategoriesToList(Category parent, ref List<Category> list, bool includeHidden)
        {
            CategoryCollection childCats = parent.GetChildCategoriesInSortedOrder(includeHidden);
            foreach (Category child in childCats)
            {
                list.Add(child);
                AddChildCategoriesToList(child, ref list, includeHidden);
            }
        }

        //public static List<CategoryNode> GetCategoryNodeTree()
        //{
        //    List<CategoryNode> cats = new List<CategoryNode>();
        //    cats.AddRange(GetTopLevelCategories().ConvertAll(c => new CategoryNode() { Category = c }));

        //    foreach (CategoryNode parentInfo in cats)
        //    {
        //        AddChildCategoryNodesRecursive(parentInfo);
        //    }

        //    return cats;
        //}

        //private static void AddChildCategoryNodesRecursive(CategoryNode parentInfo)
        //{
        //    parentInfo.SubCategories = ((List<Category>)parentInfo.Category.GetChildCategoriesInSortedOrder()).ConvertAll(c => new CategoryNode() { Category = c });

        //    foreach (CategoryNode child in parentInfo.SubCategories)
        //    {
        //        AddChildCategoryNodesRecursive(child);
        //    }
        //}

        internal static void UpdateAllNestingLevels()
        {
            CategoryCollection allCategories = new CategoryCollection();
            allCategories.LoadAll();

            foreach (Category cat in allCategories)
            {
                cat.NestingLevel = cat.GetNestingLevel();
            }
            allCategories.Save();
        }

        public static void SetSortOrderByListPosition(List<int> categoryIdsInSortOrder)
        {
            CategoryQuery q = new CategoryQuery();
            q.Where(q.Id.In(categoryIdsInSortOrder.ToArray()));

            CategoryCollection collection = new CategoryCollection();
            if (collection.Load(q))
            {
                for (short i = 0; i < categoryIdsInSortOrder.Count; i++)
                {
                    Category c = collection.FindByPrimaryKey(categoryIdsInSortOrder[i]);
                    if (c != null)
                    {
                        c.SortOrder = i;
                    }
                }
                collection.Save();
            }
        }
    }

    //public class CategoryNode
    //{
    //    public Category Category { get; set; }
    //    public List<CategoryNode> SubCategories { get; set; }

    //    public CategoryNode()
    //    {
    //        this.Category = new Category();
    //        this.SubCategories = new List<CategoryNode>();
    //    }
    //}
}
