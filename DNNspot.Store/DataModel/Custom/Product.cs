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
using System.IO;
using System.Linq;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class Product : esProduct
	{
		public Product()
		{
		
		}

        List<string> deletePhotos = null;
        string deleteDigitalFile = "";
        List<Discount> activeDiscounts = null;

        public bool HasDigitalFile
        {
            get { return !string.IsNullOrEmpty(this.DigitalFilename); }
        }

	    public bool HasVariants
	    {
            get { return this.GetProductFieldsInSortOrder().Count > 0; }
	    }


        public bool IsDigitalDelivery
        {
            get { return this.DeliveryMethodId == 2; }
        }

        public string InventoryQtyInStockForDisplay
        {
            get
            {
                if (this.InventoryQtyInStock.HasValue)
                {
                    return this.InventoryQtyInStock.Value > 0 ? this.InventoryQtyInStock.Value.ToString("N0") : "Out of stock";
                }
                return "";
            }
        }

        public bool IsInStock
        {
            get
            {
                if (this.InventoryIsEnabled.GetValueOrDefault(false))
                {
                    int qtyOnHand = this.InventoryQtyInStock.GetValueOrDefault(0);
                    if ((qtyOnHand > 0) || (qtyOnHand <= 0 && this.InventoryAllowNegativeStockLevel.GetValueOrDefault(false)))
                    {
                        return true;
                    }
                    return false;
                }
                return true;
            }
        }

        public bool IsPurchaseableByUser
        {
            get
            {
                RoleController roleController = new RoleController();
                UserInfo userInfo = UserController.GetCurrentUserInfo();
                IEnumerable<RoleInfo> userRoles =
                    (RoleInfo[])roleController.GetUserRoles(userInfo.PortalID, userInfo.UserID).ToArray(typeof(RoleInfo));//.ToList<RoleInfo>();
                List<int> roleIds = userRoles.Select(r => r.RoleID).ToList();
                bool checkoutAble = true;

                var store = Store.GetStore(this.StoreId.GetValueOrDefault(1));

                if (!WA.Parser.ToBool(store.GetSetting(StoreSettingNames.EnableCheckout)).GetValueOrDefault(true))
                {
                    return false;
                }

                if (!this.IsAvailableForPurchase.GetValueOrDefault(true))
                {
                    return false;
                }

                if (!String.IsNullOrEmpty(this.CheckoutPermissions))
                {
                    var checkoutPermissions = this.CheckoutPermissions.Split(',');

                    checkoutAble =
                        userRoles.Any(userRole => checkoutPermissions.Any(a => a == userRole.RoleID.ToString())) ||
                        checkoutPermissions.Any(a => a == "-1");

                }
                else
                {
                    return false;
                }

                if (userRoles.Any(a => a.RoleType == RoleType.Administrator) || userInfo.IsSuperUser)
                {
                    return true;
                }


                return checkoutAble;
            }

        }

        public bool IsViewable
        {
            get
            {
  
                RoleController roleController = new RoleController();
                UserInfo userInfo = UserController.GetCurrentUserInfo();
                IEnumerable<RoleInfo> userRoles = (RoleInfo[])roleController.GetUserRoles(userInfo.PortalID, userInfo.UserID).ToArray(typeof(RoleInfo)); //.ToList<RoleInfo>();
                IEnumerable<int> roleIds = userRoles.Select(r => r.RoleID);

                if (userRoles.Any(a => a.RoleType == RoleType.Administrator) || userInfo.IsSuperUser)
                {
                    return true;
                }

                if (!String.IsNullOrEmpty(this.ViewPermissions))
                {




                    var viewPermissions = this.ViewPermissions.Split(',');

                    bool viewable = userRoles.Any(userRole => viewPermissions.Any(a => a == userRole.RoleID.ToString())) || viewPermissions.Any(a => a == "-1");
                    return viewable;
                }


                return false;
            }
        }

        public bool HasActiveDiscounts
        {
            get
            {
                if (activeDiscounts == null)
                {
                    activeDiscounts = GetActiveDiscounts();
                }
                return (activeDiscounts.Count > 0);
            }
        }

        private List<Discount> GetActiveDiscounts()
        {
            List<Discount> userDiscounts = DiscountCollection.GetActiveDiscountsForCurrentUser(this.StoreId.Value);

            List<Discount> discounts = new List<Discount>();
            foreach (Discount d in userDiscounts)
            {
                if (d.DiscountTypeName == DiscountDiscountType.AllProducts)
                {
                    discounts.Add(d);
                }
                else if (d.DiscountTypeName == DiscountDiscountType.Product)
                {
                    var productIdInts = d.GetProductIds().Select(WA.Parser.ToInt).ToList();
                    if (productIdInts.Contains(this.Id.Value))
                    {
                        discounts.Add(d);
                    }
                }
                else if (d.DiscountTypeName == DiscountDiscountType.Category)
                {
                    var categoryIdInts = d.GetCategoryIds().Select(WA.Parser.ToInt);
                    var productCategoryIds = this.GetCategories(true).Select(c => c.Id);
                    if (categoryIdInts.Intersect(productCategoryIds).Count() > 0)
                    {
                        discounts.Add(d);
                    }
                }
            }
            return discounts;
        }

        public decimal GetPrice()
        {
            return this.Price.GetValueOrDefault(0) - GetPriceDiscountAmount();
        }

        public string GetPriceForDisplay()
        {
            var store = Store.GetStore(this.StoreId.Value);

            if (IsPriceDisplayed.GetValueOrDefault(true) && WA.Parser.ToBool(store.GetSetting(StoreSettingNames.ShowPrices)).GetValueOrDefault(true))
            {
                return store.FormatCurrency(GetPrice());
            }
            else
            {
                return String.Empty;
            }
        }

        private decimal GetPriceDiscountAmount()
        {
            if (activeDiscounts == null)
            {
                activeDiscounts = GetActiveDiscounts();
            }


            decimal combinableDiscountAmount = activeDiscounts
                                                .Where(d => d.IsCombinable.GetValueOrDefault(false))
                                                .Sum(d =>
                                                {
                                                    if (d.IsPercentOff)
                                                    {
                                                        return (this.Price.GetValueOrDefault(0) * d.PercentOffDecimal);
                                                    }
                                                    else if (d.IsAmountOff)
                                                    {
                                                        return d.AmountOff.GetValueOrDefault(0);
                                                    }
                                                    return 0;
                                                });

            decimal nonCombinableDiscountAmount = 0;
            var nonCombinableDiscount = activeDiscounts.FirstOrDefault(d => d.IsCombinable.GetValueOrDefault(false) == false);
            if (nonCombinableDiscount != null)
            {
                if (nonCombinableDiscount.IsPercentOff)
                {
                    nonCombinableDiscountAmount = (this.Price.GetValueOrDefault(0) * nonCombinableDiscount.PercentOffDecimal);
                }
                else if (nonCombinableDiscount.IsAmountOff)
                {
                    nonCombinableDiscountAmount = nonCombinableDiscount.AmountOff.GetValueOrDefault(0);
                }
            }

            //--- keep the greater of either the non-combinable discount, or the sum of the combinable discounts
            return Math.Max(combinableDiscountAmount, nonCombinableDiscountAmount);

            //decimal discountAmount = 0;
            //foreach (var d in activeDiscounts)
            //{
            //    if (d.IsPercentOff)
            //    {
            //        discountAmount += (this.Price.GetValueOrDefault(0) * d.PercentOffDecimal);
            //    }
            //    else if (d.IsAmountOff)
            //    {
            //        discountAmount += d.AmountOff.GetValueOrDefault(0);
            //    }
            //}
            //return discountAmount;
        }

        public decimal GetPriceWithoutDiscount()
        {
            return this.Price.GetValueOrDefault(0);
        }

        public static Product GetProduct(int productId)
        {
            Product p = new Product();
            if (p.LoadByPrimaryKey(productId))
            {
                return p;
            }
            return null;
        }

        public List<CheckoutRoleInfo> GetCheckoutRoleInfos()
        {
            if (!string.IsNullOrEmpty(this.CheckoutAssignRoleInfoJson))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<CheckoutRoleInfo>>(this.CheckoutAssignRoleInfoJson) ?? new List<CheckoutRoleInfo>();
            }
            return new List<CheckoutRoleInfo>();
        }

        public ProductPhoto GetMainPhoto()
        {
            ProductPhotoQuery q = new ProductPhotoQuery();
            q.es.Top = 1;
            q.Where(q.ProductId == this.Id.Value);
            q.OrderBy(q.SortOrder.Ascending, q.DisplayName.Ascending);

            ProductPhoto mainPhoto = new ProductPhoto();
            if (mainPhoto.Load(q))
            {
                return mainPhoto;
            }
            return new ProductPhoto();
        }

        public List<ProductField> GetProductFieldsInSortOrder()
        {
            //ProductFieldQuery q = new ProductFieldQuery();
            //q.OrderBy(q.SortOrder.Ascending);

            //ProductFieldCollection collection = new ProductFieldCollection();
            //collection.Load(q);

            //return collection;

            //this.ProductFieldCollectionByProductId.Sort = ProductFieldMetadata.PropertyNames.SortOrder + " ASC";

            return this.ProductFieldCollectionByProductId.AsQueryable().OrderBy(x => x.SortOrder).ToList();
        }

        public static List<ProductPhoto> GetAllPhotosInSortOrder(int productId)
        {
            ProductPhotoQuery q = new ProductPhotoQuery();
            q.Where(q.ProductId == productId);
            q.OrderBy(q.SortOrder.Ascending, q.DisplayName.Ascending);

            ProductPhotoCollection collection = new ProductPhotoCollection();
            if (collection.Load(q))
            {
                return collection.ToList();
            }
            return new List<ProductPhoto>();
        }

        public List<ProductPhoto> GetAllPhotosInSortOrder()
        {
            //string sort = string.Format("{0} ASC, {1} ASC", ProductPhotoMetadata.PropertyNames.SortOrder, ProductPhotoMetadata.PropertyNames.DisplayName);
            //this.ProductPhotoCollectionByProductId.Sort = sort;

            //return this.ProductPhotoCollectionByProductId;

            return GetAllPhotosInSortOrder(this.Id.GetValueOrDefault(-1));
        }

        public bool DeleteAllPhotos(string directoryPath)
        {
            this.ProductPhotoCollectionByProductId.MarkAllAsDeleted();
            this.ProductPhotoCollectionByProductId.Save();

            if (Directory.Exists(directoryPath))
            {
                List<ProductPhoto> photos = GetAllPhotosInSortOrder();
                foreach (var photo in photos)
                {
                    string path = Path.Combine(directoryPath, photo.Filename);
                    File.Delete(path);
                }
                return true;
            }
            return false;
        }

        public List<ProductDescriptor> GetProductDescriptors()
        {
            //this.ProductDescriptorCollectionByProductId.Sort = ProductDescriptorMetadata.ColumnNames.SortOrder + " ASC";

            return this.ProductDescriptorCollectionByProductId.AsQueryable().OrderBy(x => x.SortOrder).ToList();
        }

        public bool LoadBySlug(int storeId, string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return false;
            }
            return this.Load(ProductQuery.FindSingleBySlug(storeId, slug));
        }

        public static bool SlugExists(int storeId, string slug)
        {
            slug = slug.ToLower();

            // method for testing 'exists' as recommended here: http://community.entityspaces.net/forums/16358/ShowThread.aspx#16358
            ProductQuery q = new ProductQuery();
            q.es.CountAll = true;
            q.Where(q.StoreId == storeId, q.Slug == slug);
            int count = (int)q.ExecuteScalar();

            return (count == 1);
        }

        public static Product GetBySlug(int storeId, string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return null;
            }
            Product p = new Product();
            return p.LoadBySlug(storeId, slug) ? p : null;
        }

        public static Product GetBySku(int storeId, string sku)
        {
            if (string.IsNullOrEmpty(sku))
            {
                return null;
            }
            ProductQuery q = new ProductQuery();
            q.Where(q.StoreId == storeId, q.Sku == sku.Trim());

            Product p = new Product();
            return p.Load(q) ? p : null;
        }

        public static Product GetByName(int storeId, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            ProductQuery q = new ProductQuery();
            q.Where(q.StoreId == storeId, q.Name == name.Trim());

            Product p = new Product();
            return p.Load(q) ? p : null;
        }

        /// <summary>
        /// Load a Product by matching AT LEAST ONE of: name, sku, slug
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="name"></param>        
        /// <param name="slug"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public static Product GetByNameOrSlugOrSku(int storeId, string name, string slug, string sku)
        {
            ProductQuery q = new ProductQuery();
            q.es.DefaultConjunction = esConjunction.Or;
            q.Where(q.And(q.StoreId == storeId, q.Name == name.Trim()));
            q.Where(q.And(q.StoreId == storeId, q.Slug == slug.Trim()));
            // SKU is not required in the db, so only check if non-empty
            if (!string.IsNullOrEmpty(sku))
            {
                q.Where(q.And(q.StoreId == storeId, q.Sku == sku.Trim()));
            }

            Product p = new Product();
            return p.Load(q) ? p : null;
        }

        public static void SetCategories(int productId, List<int> categoryIds)
        {
            Product p = new Product();
            if (p.LoadByPrimaryKey(productId))
            {
                using (esTransactionScope transaction = new esTransactionScope())
                {
                    p.ProductCategoryCollectionByProductId.MarkAllAsDeleted();
                    p.ProductCategoryCollectionByProductId.Save();

                    foreach (int catId in categoryIds)
                    {
                        ProductCategory pc = p.ProductCategoryCollectionByProductId.AddNew();
                        pc.CategoryId = catId;
                    }
                    p.ProductCategoryCollectionByProductId.Save();

                    transaction.Complete();
                }
            }
        }

        public List<Category> GetCategories(bool includeHiddenCategories)
        {
            // This is correct, but there's a bug in ES that doesn't honor the DNN Object Qualifier for M2M collections :(
            //List<Category> categories = this.UpToCategoryCollection;
            //if (!includeHiddenCategories)
            //{
            //    categories.RemoveAll(c => !c.IsDisplayed.GetValueOrDefault());
            //}
            //return categories;

            //SELECT
            //c.*
            //FROM DNNspot_Store_Category c
            //INNER JOIN DNNspot_Store_ProductCategory pc ON pc.CategoryId = c.Id
            //WHERE pc.ProductId = 21

            CategoryQuery c = new CategoryQuery("c");
            ProductCategoryQuery pc = new ProductCategoryQuery("pc");

            c.Select(c).InnerJoin(pc).On(c.Id == pc.CategoryId);
            c.Where(pc.ProductId == this.Id.Value);

            CategoryCollection collection = new CategoryCollection();
            collection.Load(c);

            return collection.ToList();
        }

        public List<Product> GetRelatedProducts()
        {

            ProductQuery p = new ProductQuery("p");
            RelatedProductQuery rp = new RelatedProductQuery("rp");

            p.Select(p).InnerJoin(rp).On(p.Id == rp.RelatedProductId);
            p.Where(rp.ProductId == this.Id.Value);

            ProductCollection collection = new ProductCollection();
            collection.Load(p);

            return collection.ToList();
        }

        public void AddCategory(Category category)
        {
            if (this.StoreId == category.StoreId)
            {
                ProductCategory pc = new ProductCategory();
                pc.ProductId = this.Id.Value;
                pc.CategoryId = category.Id.Value;
                pc.Save();
            }
        }

        public static List<ProductSortByField> GetSortByFields()
        {
            List<ProductSortByField> fields = new List<ProductSortByField>();

            fields.Add(new ProductSortByField() { DisplayName = "Name", Field = ProductMetadata.ColumnNames.Name, SortDirection = SortDirection.ASC });
            fields.Add(new ProductSortByField() { DisplayName = "Bestselling", Field = "NumSold", SortDirection = SortDirection.DESC });
            fields.Add(new ProductSortByField() { DisplayName = "Price - Low to High", Field = ProductMetadata.ColumnNames.Price, SortDirection = SortDirection.ASC });
            fields.Add(new ProductSortByField() { DisplayName = "Price - High to Low", Field = ProductMetadata.ColumnNames.Price, SortDirection = SortDirection.DESC });
            //fields.Add(new ProductSortByField() { DisplayName = "Date Added - Oldest to Newest", Field = ProductMetadata.ColumnNames.CreatedOn, SortDirection = SortDirection.ASC });
            //fields.Add(new ProductSortByField() { DisplayName = "Date Added - Newest to Oldest", Field = ProductMetadata.ColumnNames.CreatedOn, SortDirection = SortDirection.DESC });
            fields.Add(new ProductSortByField() { DisplayName = "Most Recent", Field = ProductMetadata.ColumnNames.CreatedOn, SortDirection = SortDirection.DESC });
            fields.Add(new ProductSortByField() { DisplayName = "SKU", Field = ProductMetadata.ColumnNames.Sku, SortDirection = SortDirection.ASC });

            return fields;
        }

        /// <summary>
        /// Override to handle automatic deletion of Photo files and Digital file on disk
        /// </summary>
        public override void MarkAsDeleted()
        {
            deletePhotos = this.GetAllPhotosInSortOrder().ConvertAll(x => x.Filename);
            deleteDigitalFile = this.DigitalFilename;

            base.MarkAsDeleted();
        }

        /// <summary>
        /// Override to handle automatic deletion of Photo files and Digital file on disk
        /// </summary>
        public override void Save()
        {
            bool isDeleted = this.es.IsDeleted;

            base.Save();

            if (isDeleted)
            {
                // Delete Product Photos from disk                
                string photoDir = StoreUrls.GetProductPhotoFolderFileRoot();
                foreach (var photoFile in deletePhotos)
                {
                    if (!string.IsNullOrEmpty(photoFile))
                    {
                        File.Delete(Path.Combine(photoDir, photoFile));
                    }
                }

                // Delete Digital File from disk
                if (!string.IsNullOrEmpty(deleteDigitalFile))
                {
                    string filesDir = StoreUrls.GetModuleFolderFileRoot() + @"ProductFiles\";
                    File.Delete(Path.Combine(filesDir, deleteDigitalFile));
                }
            }
        }
	}
}
