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
Date Generated       : 4/12/2013 3:32:33 PM
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
	public partial class ProductCollection : esProductCollection
	{
		public ProductCollection()
		{
		
		}

        public static List<Product> GetAll(int storeId)
        {
            return GetAll(storeId, false);
        }

        public static List<Product> GetAll(int storeId, bool includeActiveProductsOnly)
        {
            ProductQuery q = new ProductQuery();
            q.Where(q.StoreId == storeId);
            if (includeActiveProductsOnly)
            {
                q.Where(q.IsActive == true);
            }
            q.OrderBy(q.Name.Ascending);

            ProductCollection collection = new ProductCollection();
            collection.Load(q);

            return collection.ToList();
        }

        public static List<Product> FindProductsByCategory(int categoryId, ProductSortByField sortBy)
        {
            ProductQuery p = new ProductQuery("p");
            ProductCategoryQuery pc = new ProductCategoryQuery("pc");
            vProductsSoldCountsQuery productsSold = new vProductsSoldCountsQuery("productsSold");

            p.Select(p);
            p.InnerJoin(pc).On(p.Id == pc.ProductId);
            p.LeftJoin(productsSold).On(p.Id == productsSold.ProductId);
            p.Where(p.IsActive == true);
            p.Where(pc.CategoryId == categoryId);
            if (!string.IsNullOrEmpty(sortBy.Field))
            {
                p.OrderBy(sortBy.Field,
                          sortBy.SortDirection == SortDirection.ASC
                              ? esOrderByDirection.Ascending
                              : esOrderByDirection.Descending);
            }

            //string sql = p.Parse();

            ProductCollection products = new ProductCollection();
            products.Load(p);

            return products.Where(z => z.IsViewable == true).ToList();
        }

        public static List<Product> FindProductsByCategories(IList<int> categoryIds, ProductSortByField sortBy, bool matchAllCategories)
        {
            if (categoryIds.Count > 0)
            {
                ProductQuery p = new ProductQuery("p");
                ProductCategoryQuery pc = new ProductCategoryQuery("pc");
                vProductsSoldCountsQuery productsSold = new vProductsSoldCountsQuery("productsSold");

                //p.es.CountAll = true;
                //p.es.CountAllAlias = "TotalResultCount";
                //p.es.PageNumber = 1;
                //p.es.PageSize = 2;

                p.es.Distinct = true;
                p.Select(p);
                p.InnerJoin(pc).On(p.Id == pc.ProductId);
                p.LeftJoin(productsSold).On(p.Id == productsSold.ProductId);
                p.Where(p.IsActive == true);
                if (matchAllCategories)
                {
                    ProductCategoryQuery pcAllCats = new ProductCategoryQuery("pcAll");
                    pcAllCats.Select(pcAllCats.ProductId)
                        .Where(pcAllCats.CategoryId.In(categoryIds.ToArray()))
                        .GroupBy(pcAllCats.ProductId)
                        .Having(pcAllCats.ProductId.Count() == categoryIds.Count);

                    p.Where(p.Id.In(pcAllCats));
                }
                else
                {
                    p.Where(pc.CategoryId.In(categoryIds.ToArray()));
                }
                if (!string.IsNullOrEmpty(sortBy.Field))
                {
                    p.OrderBy(sortBy.Field,
                              sortBy.SortDirection == SortDirection.ASC
                                  ? esOrderByDirection.Ascending
                                  : esOrderByDirection.Descending);
                }

                string sql = p.Parse();

                ProductCollection products = new ProductCollection();
                products.Load(p);

                return products.Where(z => z.IsViewable == true).ToList();
            }
            return new List<Product>();
        }

        public static IList<Product> GetProductsByIds(int[] productIds)
        {
            if (productIds.Length > 0)
            {
                ProductQuery q = new ProductQuery();
                q.Where(q.Id.In(productIds));
                q.OrderBy(q.Name.Ascending);

                ProductCollection collection = new ProductCollection();
                collection.Load(q);


                return collection;
            }
            else
            {
                return new List<Product>();
            }
        }
	}
}
