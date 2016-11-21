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
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DNNspot.Store.DataModel;
using EntitySpaces.Interfaces;
using WA.Extensions;
using WA.FileHelpers.Csv;

namespace DNNspot.Store.Importers
{    
    internal class ProductCsvImporter
    {
        private enum ImportActionType { UNKNOWN, Create, Replace, Update, Delete }

        readonly int storeId;
        ImportActionType importAction = ImportActionType.UNKNOWN;
        readonly string productPhotoFolderFileRoot;
        readonly string productFilesFolderFileRoot;
        readonly string importFilesFolderFileRoot;
        ProductCsvImportLine csvLine;
        List<string> filesInImportDirectory;

        public ProductCsvImporter(int storeId)
        {
            this.storeId = storeId;
            
            this.productPhotoFolderFileRoot = StoreUrls.GetProductPhotoFolderFileRoot();
            CreateDirIfNotExists(productPhotoFolderFileRoot);
            this.productFilesFolderFileRoot = StoreUrls.GetModuleFolderFileRoot() + @"ProductFiles\";
            CreateDirIfNotExists(productFilesFolderFileRoot);

            this.importFilesFolderFileRoot = StoreUrls.GetModuleFolderFileRoot() + @"ImportFiles\";
            CreateDirIfNotExists(importFilesFolderFileRoot);
            filesInImportDirectory = new List<string>(Directory.GetFiles(importFilesFolderFileRoot));

            csvLine = new ProductCsvImportLine();
        }

        private void CreateDirIfNotExists(string directory)
        {
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        internal void ExportProducts(IList<CsvProductInfo> products, Stream outputStream)
        {
            using (var writer = new StreamWriter(outputStream))
            {
                var csv = new CsvWriter(writer, new CsvWriterOptions { HasHeaderRecord = true });

                csv.WriteRecords(products);
            }
        }

        internal ProductCsvImportResult ImportProducts(Stream fileStream)
        {
            ProductCsvImportResult result = new ProductCsvImportResult();

            try
            {
                IList<CsvProductInfo> csvProducts;
                using (StreamReader csvStreamReader = new StreamReader(fileStream))
                {
                    var csvParser = new CsvParser(csvStreamReader);
                    var csvOptions = new CsvReaderOptions() {HasHeaderRecord = true, Strict = false};
                    using (CsvReader csvReader = new CsvReader(csvParser, csvOptions))
                    {
                        csvProducts = csvReader.GetRecords<CsvProductInfo>();
                    }
                }

                if (csvProducts.Count == 0)
                {
                    result.Messages.Add("Did not find any products in import file");
                }
                else
                {
                    result.Messages.Add(string.Format(@"Read {0:N0} products from CSV file", csvProducts.Count));

                    ImportProductsToDatabase(csvProducts, ref result);
                }
            }
            catch(Exception ex)
            {
                result.Messages.Add(string.Format(@"ERROR: {0}. {1}", ex.Message, ex.StackTrace));
            }

            return result;
        }

        private void ImportProductsToDatabase(IEnumerable<CsvProductInfo> csvProducts, ref ProductCsvImportResult result)
        {           
            int lineNo = 1;
            foreach(var csvProduct in csvProducts)
            {
                lineNo++;
                csvLine = new ProductCsvImportLine()
                {
                    CsvLineNumber = lineNo,
                    ProductName = csvProduct.Name,
                    ProductSku = csvProduct.Sku,
                    ProductUrlName = csvProduct.UrlName,
                    StatusMsg = ""
                };

                string actionString = (csvProduct.ImportAction ?? "").ToUpper();
                importAction = ImportActionType.UNKNOWN;
                if (actionString == "C") importAction = ImportActionType.Create;
                if (actionString == "R") importAction = ImportActionType.Replace;
                if (actionString == "U") importAction = ImportActionType.Update;
                if (actionString == "D") importAction = ImportActionType.Delete;

                //--- Check for missing/invalid fields
                if(!HasRequiredFields(csvLine, csvProduct))
                {
                    result.CsvLines.Add(csvLine);
                    continue;
                }

                // try to find the product
                Product p = null;
                if (csvProduct.ProductId.HasValue)
                {
                    p = Product.GetProduct(csvProduct.ProductId.Value);
                }
                else
                {
                    p = Product.GetBySlug(storeId, csvProduct.UrlName) ??
                        Product.GetByName(storeId, csvProduct.Name) ?? Product.GetBySku(storeId, csvProduct.Sku);
                }
                bool productExists = (p != null);

                try
                {
                    switch (importAction)
                    {
                        case ImportActionType.Create:
                            if(productExists)
                            {
                                csvLine.Status = ProductImportStatus.Skipped;
                                csvLine.StatusMsg = "Product already exists. Specify 'R' or 'U' to replace/update this product.";
                            }
                            else
                            {
                                csvLine.Status = ProductImportStatus.Created;
                                CreateProduct(csvProduct);
                            }
                            break;
                        case ImportActionType.Replace:
                            if(productExists)
                            {
                                csvLine.Status = ProductImportStatus.Replaced;
                                ReplaceProduct(p, csvProduct);
                            }
                            else
                            {
                                csvLine.Status = ProductImportStatus.Created;
                                CreateProduct(csvProduct);
                            }
                            break;
                        case ImportActionType.Update:
                            if(productExists)
                            {
                                csvLine.Status = ProductImportStatus.Updated;
                                UpdateProduct(p, csvProduct);
                            }
                            else
                            {
                                csvLine.Status = ProductImportStatus.Created;
                                CreateProduct(csvProduct);
                            }
                            break;
                        case ImportActionType.Delete:
                            if (productExists)
                            {
                                csvLine.Status = ProductImportStatus.Deleted;
                                DeleteProduct(p);
                            }
                            else
                            {
                                csvLine.Status = ProductImportStatus.Skipped;
                                csvLine.StatusMsg = "Unable to find Product to delete";
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    csvLine.Status = ProductImportStatus.Error;
                    csvLine.StatusMsg += " ERROR: " + ex.ToString();
                }

                result.CsvLines.Add(csvLine);       
            }
        }
        
        private bool HasRequiredFields(ProductCsvImportLine csvLine, CsvProductInfo csvProduct)
        {
            if (string.IsNullOrEmpty(csvProduct.ImportAction))
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg = "ImportAction is required";
                return false;
            }
            else if (importAction == ImportActionType.UNKNOWN)
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg = "Unknown ImportAction. Valid values are: 'C', 'R', 'U', 'D'";
                return false;
            }
            if (string.IsNullOrEmpty(csvProduct.Name))
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg = "Name is required";
                return false;
            }
            if (string.IsNullOrEmpty(csvProduct.UrlName))
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg = "UrlName is required";
                return false;
            }
            return true;
        }

        private void CreateProduct(CsvProductInfo csvProduct)
        {
            var p = new Product();
            p.StoreId = storeId;
            
            ReplaceProduct(p, csvProduct);            
        }

        /// <summary>
        /// Replaces/overwrites fields on an existing Product.
		/// ALL matching fields are replaced/overwritten with the values from the CSV file (including empty fields).
		/// Existing Product Categories are first DELETED, and then re-added from the CSV file.
		/// Existing Product Photos are DELETED, and then re-added from the CSV.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="csvProduct"></param>
        private void ReplaceProduct(Product p, CsvProductInfo csvProduct)
        {
            p.Name = csvProduct.Name;
            p.Sku = csvProduct.Sku;
            p.Slug = csvProduct.UrlName.IsValidSlug() ? csvProduct.UrlName : csvProduct.UrlName.CreateSlug();
            p.Price = csvProduct.Price;
            p.Weight = csvProduct.Weight;
            p.SeoTitle = csvProduct.SeoTitle;
            p.SeoDescription = csvProduct.SeoDescription;
            p.SeoKeywords = csvProduct.SeoKeywords;
            p.IsActive = csvProduct.IsActive.GetValueOrDefault(true);
            p.IsTaxable = csvProduct.TaxableItem.GetValueOrDefault(true);
            p.IsPriceDisplayed = csvProduct.ShowPrice.GetValueOrDefault(true);
            p.IsAvailableForPurchase = csvProduct.AvailableForPurchase.GetValueOrDefault(true);
            p.InventoryIsEnabled = csvProduct.EnableInventoryManagement.GetValueOrDefault(false);
            p.InventoryQtyInStock = csvProduct.StockLevel;
            p.InventoryAllowNegativeStockLevel = csvProduct.AllowNegativeStock.GetValueOrDefault(false);

            if (!p.Slug.IsValidSlug())
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg += string.Format(@"Invalid Slug '{0}' must match the pattern '{1}'", p.Slug, RegexPatterns.IsValidSlug);
                return;
            }
            if (!string.IsNullOrEmpty(p.Sku) && !p.Sku.IsValidSku())
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg += string.Format(@"Invalid Sku '{0}' must match the pattern '{1}'", p.Sku, RegexPatterns.IsValidSku);
                return;
            }

            // save now so we can get an ID for new products
            p.Save();

            //--- Digital File            
            if(NotEmpty(p.DigitalFilename))
            {
                File.Delete(Path.Combine(productFilesFolderFileRoot, p.DigitalFilename));
                p.DigitalFilename = "";
                p.DigitalFileDisplayName = "";
                p.DeliveryMethodId = (short)ProductDeliveryMethod.Shipped;
            }
            ImportDigitalFile(p, csvProduct.DigitalFilename);

            //--- Descriptors (delete and re-add)
            using (esTransactionScope transaction = new esTransactionScope())
            {
                p.ProductDescriptorCollectionByProductId.MarkAllAsDeleted();
                AddProductDescriptor(p, 1, csvProduct.Desc1Name, csvProduct.Desc1Html); // always add the 1st tab
                if (!string.IsNullOrEmpty(csvProduct.Desc2Name)) AddProductDescriptor(p, 2, csvProduct.Desc2Name, csvProduct.Desc2Html);
                if (!string.IsNullOrEmpty(csvProduct.Desc3Name)) AddProductDescriptor(p, 3, csvProduct.Desc3Name, csvProduct.Desc3Html);
                if (!string.IsNullOrEmpty(csvProduct.Desc4Name)) AddProductDescriptor(p, 4, csvProduct.Desc4Name, csvProduct.Desc4Html);
                if (!string.IsNullOrEmpty(csvProduct.Desc5Name)) AddProductDescriptor(p, 5, csvProduct.Desc5Name, csvProduct.Desc5Html);

                transaction.Complete();
            }

            //--- Categories (delete and then re-associate/add)
            p.ProductCategoryCollectionByProductId.MarkAllAsDeleted(); // remove existing product categories
            //p.ProductCategoryCollectionByProductId.Save();
            List<string> newCategoryNames = csvProduct.CategoryNames.ToList(",", true);
            foreach (string newCat in newCategoryNames)
            {
                var c = Category.GetByName(storeId, newCat) ?? CreateCategory(newCat);
                p.AddCategory(c);
            }            
            
            //--- Photos (delete and then re-add)
            p.DeleteAllPhotos(productPhotoFolderFileRoot);
            IEnumerable<string> importPhotoFiles = GetPhotoFilePathsForImport(p, csvProduct);
            foreach(string filepath in importPhotoFiles)
            {
                AddPhoto(p, filepath);
            }

            p.Save();
        }

        /// <summary>
        /// Updates an existing product.
		/// A field is only updated if the CSV field is non-empty.
		/// Existing Product Categories are kept, and new Categories are added from the CSV file.
		/// Existing Product Photos are kept, and new Photos are added.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="csvProduct"></param>
        private void UpdateProduct(Product p, CsvProductInfo csvProduct)
        {
            if(NotEmpty(csvProduct.Name)) p.Name = csvProduct.Name;
            if (NotEmpty(csvProduct.Sku)) p.Sku = csvProduct.Sku;
            if (NotEmpty(csvProduct.UrlName)) p.Slug = csvProduct.UrlName.IsValidSlug() ? csvProduct.UrlName : csvProduct.UrlName.CreateSlug();
            if (csvProduct.Price.HasValue) p.Price = csvProduct.Price;
            if (csvProduct.Weight.HasValue) p.Weight = csvProduct.Weight;
            if (NotEmpty(csvProduct.SeoTitle)) p.SeoTitle = csvProduct.SeoTitle;
            if (NotEmpty(csvProduct.SeoDescription)) p.SeoDescription = csvProduct.SeoDescription;
            if (NotEmpty(csvProduct.SeoKeywords)) p.SeoKeywords = csvProduct.SeoKeywords;
            if (csvProduct.IsActive.HasValue) p.IsActive = csvProduct.IsActive;
            if (csvProduct.TaxableItem.HasValue) p.IsTaxable = csvProduct.TaxableItem;
            if (csvProduct.ShowPrice.HasValue) p.IsPriceDisplayed = csvProduct.ShowPrice;
            if (csvProduct.AvailableForPurchase.HasValue) p.IsAvailableForPurchase = csvProduct.AvailableForPurchase;
            if (csvProduct.EnableInventoryManagement.HasValue) p.InventoryIsEnabled = csvProduct.EnableInventoryManagement;
            p.InventoryQtyInStock = csvProduct.StockLevel.HasValue ? csvProduct.StockLevel : null;
            
            if (csvProduct.AllowNegativeStock.HasValue) p.InventoryAllowNegativeStockLevel = csvProduct.AllowNegativeStock;
            
            if (!p.Slug.IsValidSlug())
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg += string.Format(@"Invalid Slug '{0}' must match the pattern '{1}'", p.Slug, RegexPatterns.IsValidSlug);
                return;
            }
            if (!string.IsNullOrEmpty(p.Sku) && !p.Sku.IsValidSku())
            {
                csvLine.Status = ProductImportStatus.Error;
                csvLine.StatusMsg += string.Format(@"Invalid Sku '{0}' must match the pattern '{1}'", p.Sku, RegexPatterns.IsValidSku);
                return;
            }

            //--- Digital File            
            if (NotEmpty(csvProduct.DigitalFilename))
            {
                if (NotEmpty(p.DigitalFilename))
                {
                    File.Delete(Path.Combine(productFilesFolderFileRoot, p.DigitalFilename));
                }
                ImportDigitalFile(p, csvProduct.DigitalFilename);
            }            

            //--- Descriptors (update/add if different, but preserve existing)
            using (esTransactionScope transaction = new esTransactionScope())
            {
                var existingDescriptors = p.GetProductDescriptors();
                var newDescriptors = new Dictionary<int, DescriptorInfo>();
                for (short i = 0; i < 5; i++)
                {
                    //var descriptor = (i < existingDescriptors.Count) ? existingDescriptors[i] : new ProductDescriptor();
                    //descriptor.SortOrder = i;
                    //newDescriptors[i] = descriptor;
                    
                    if(i < existingDescriptors.Count)
                    {
                        newDescriptors[i] = new DescriptorInfo() { Name = existingDescriptors[i].Name, Text = existingDescriptors[i].Text};
                    }
                    else
                    {
                        newDescriptors[i] = new DescriptorInfo();
                    }
                }
                if (NotEmpty(csvProduct.Desc1Name)) newDescriptors[0].Name = csvProduct.Desc1Name;
                if (NotEmpty(csvProduct.Desc2Name)) newDescriptors[1].Name = csvProduct.Desc2Name;
                if (NotEmpty(csvProduct.Desc3Name)) newDescriptors[2].Name = csvProduct.Desc3Name;
                if (NotEmpty(csvProduct.Desc4Name)) newDescriptors[3].Name = csvProduct.Desc4Name;
                if (NotEmpty(csvProduct.Desc5Name)) newDescriptors[4].Name = csvProduct.Desc5Name;
                if (NotEmpty(csvProduct.Desc1Html)) newDescriptors[0].Text = csvProduct.Desc1Html;
                if (NotEmpty(csvProduct.Desc2Html)) newDescriptors[1].Text = csvProduct.Desc2Html;
                if (NotEmpty(csvProduct.Desc3Html)) newDescriptors[2].Text = csvProduct.Desc3Html;
                if (NotEmpty(csvProduct.Desc4Html)) newDescriptors[3].Text = csvProduct.Desc4Html;
                if (NotEmpty(csvProduct.Desc5Html)) newDescriptors[4].Text = csvProduct.Desc5Html;

                p.ProductDescriptorCollectionByProductId.MarkAllAsDeleted();
                for (short i = 0; i < newDescriptors.Count; i++)
                {
                    var descr = newDescriptors[i];
                    if (NotEmpty(descr.Name) || NotEmpty(descr.Text))
                    {
                        AddProductDescriptor(p, i, descr.Name, descr.Text);
                    }
                }

                transaction.Complete();
            }

            //--- Categories (add new, but don't delete existing)
            List<Category> existingProductCategories = p.GetCategories(true);
            List<string> newCategoryNames = csvProduct.CategoryNames.ToList(",", true);
            newCategoryNames.RemoveAll(x => existingProductCategories.Exists(c => c.Name == x));
            foreach (string newCat in newCategoryNames)
            {
                var c = Category.GetByName(storeId, newCat) ?? CreateCategory(newCat);
                p.AddCategory(c);
            }            
            
            //--- Photos (add new, but don't delete existing)            
            IEnumerable<string> importPhotoFiles = GetPhotoFilePathsForImport(p, csvProduct);
            foreach (string filepath in importPhotoFiles)
            {
                AddPhoto(p, filepath);
            }

            p.Save();
        }

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="p"></param>
        private void DeleteProduct(Product p)
        {
            p.MarkAsDeleted();
            p.Save();            
        }

        private bool NotEmpty(string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        private void AddProductDescriptor(Product p, short tabNumber, string tabName, string tabHtml)
        {            
            ProductDescriptor desc = p.ProductDescriptorCollectionByProductId.AddNew();
            desc.ProductId = p.Id;
            desc.Name = string.IsNullOrEmpty(tabName) ? "Tab " + tabNumber : tabName;
            desc.Text = tabHtml;
            desc.SortOrder = tabNumber;
        }

        private Category CreateCategory(string categoryName)
        {
            var c = new Category();
            c.StoreId = storeId;
            c.ParentId = null;
            c.NestingLevel = 0;
            c.Slug = categoryName.CreateSlug();
            c.Name = categoryName;
            c.Title = categoryName;
            c.IsDisplayed = true;
            c.SortOrder = 999;
            c.Save();

            return c;
        }

        /// <summary>
        /// Get the file paths for Product Photos to be imported.
        /// Will grab photos from the CSV field and also photos with matching SKUs (StartsWith) from the ImportFiles directory.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="csvProduct"></param>
        /// <returns></returns>
        private IEnumerable<string> GetPhotoFilePathsForImport(Product p, CsvProductInfo csvProduct)
        {
            List<string> filenames = new List<string>();

            // CSV field
            filenames.AddRange(csvProduct.PhotoFilenames.ToList(",", true));

            // ImportFiles Directory - match by SKU
            if(!string.IsNullOrEmpty(p.Sku))
            {
                filenames.AddRange(filesInImportDirectory.FindAll(f =>
                        f.StartsWith(p.Sku)
                        && (f.EndsWith(".jpg") || f.EndsWith(".gif") || f.EndsWith(".png"))
                    ));
            }

            return filenames.ConvertAll(f => Path.Combine(importFilesFolderFileRoot, f));
        }

        private void AddPhoto(Product p, string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                if (File.Exists(filepath))
                {
                    string destDir = this.productPhotoFolderFileRoot;

                    string filename = filepath.CreateUniqueSequentialFileNameInDir(destDir);
                    string newPath = Path.Combine(destDir, filename);
                    File.Move(filepath, newPath);

                    var photo = p.ProductPhotoCollectionByProductId.AddNew();
                    photo.Filename = filename;
                    photo.DisplayName = Path.GetFileNameWithoutExtension(filename);
                    photo.SortOrder = 99;
                }
                else
                {
                    csvLine.StatusMsg += string.Format(@" Photo '{0}' not found in import directory.", Path.GetFileName(filepath));
                }
            }
        }

        private void ImportDigitalFile(Product p, string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                string importDir = this.importFilesFolderFileRoot;
                string destDir = this.productFilesFolderFileRoot;

                string filepath = Path.Combine(importDir, filename);
                if (File.Exists(filepath))
                {
                    try
                    {
                        string newFilename = filepath.CreateUniqueSequentialFileNameInDir(destDir);

                        string newPath = Path.Combine(destDir, newFilename);
                        File.Move(filepath, newPath);

                        p.DigitalFilename = newFilename;
                        p.DigitalFileDisplayName = Path.GetFileNameWithoutExtension(newFilename);
                        p.DeliveryMethodId = (short) ProductDeliveryMethod.Downloaded;
                    }
                    catch(Exception ex)
                    {
                        csvLine.StatusMsg += string.Format(@" ERROR: {0}", ex);
                    }
                }
                else
                {
                    csvLine.StatusMsg += string.Format(@" File '{0}' not found in import directory.", filename);
                }
            }
        }

        class DescriptorInfo
        {
            public string Name { get; set; }
            public string Text { get; set; }
        }
    }    
}
