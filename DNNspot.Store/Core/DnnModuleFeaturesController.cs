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
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using DNNspot.Store.DataModel;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using EntitySpaces.Interfaces;
using WA.Extensions;

namespace DNNspot.Store
{
    /// <summary>
    /// This class is responsible for implementing the DNN specific interfaces for the module
    /// E.g. ISearchable, IPortable, etc.
    /// </summary>
    public class DnnModuleFeaturesController : ISearchable
    {
        #region ISearchable Members

        /// <summary>
        /// Allow products to be searched via the built-in DNN Search
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <returns></returns>
        public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(ModuleInfo moduleInfo)
        {            
            SearchItemInfoCollection searchItemCollection = new SearchItemInfoCollection();
            
            InitializeEntitySpaces();

            int portalId = moduleInfo.PortalID;
            int moduleId = moduleInfo.ModuleID;

            // We need to always use the same ModuleID because DNN calls this method for each instance of our module that exists.
            // If we did NOT do this, there would be duplicate Products in the search results
            List<TabModuleMatch> moduleTabs = DnnHelper.GetTabsWithModuleByModuleDefinitionName(portalId, ModuleDefs.MainDispatch.DefinitionName);
            if(moduleTabs.Count > 0)
            {
                moduleId = moduleTabs[0].ModuleId;
            }

            DataModel.Store store = DataModel.Store.GetStoreByPortalId(portalId);
            if (store != null)
            {
                List<Product> allProductsInStore = ProductCollection.GetAll(store.Id.Value, true);
                foreach (Product p in allProductsInStore)
                {
                    string description = "";
                    List<ProductDescriptor> descriptors = p.GetProductDescriptors();
                    if (descriptors.Count > 0)
                    {
                        description = descriptors[0].TextHtmlDecoded;
                    }
                    
                    //string searchKey = p.Id.Value.ToString();
                    string searchKey = "DNNspot-Store-Product-" + p.Id.Value.ToString();
                    string searchContent = p.Name + " " + description;

                    SearchItemInfo searchItem = new SearchItemInfo(
                            p.Name,
                            description.StripHtmlTags().ChopAtWithSuffix(200, "..."),
                            Null.NullInteger,
                            p.ModifiedOn.Value,
                            moduleId,
                            searchKey,
                            searchContent,
                            "product=" + p.Id.Value.ToString()
                    );

                    searchItemCollection.Add(searchItem);
                }
            }
            return searchItemCollection;
        }

        #endregion

        #region EntitySpaces

        private static void InitializeEntitySpaces()
        {
            if (esConfigSettings.ConnectionInfo.Default != "SiteSqlServer")
            {
                esConfigSettings connectionInfoSettings = esConfigSettings.ConnectionInfo;
                foreach (esConnectionElement connection in connectionInfoSettings.Connections)
                {
                    //if there is a SiteSqlServer in es connections set it default
                    if (connection.Name == "SiteSqlServer")
                    {
                        esConfigSettings.ConnectionInfo.Default = connection.Name;
                        return;
                    }
                }

                //no SiteSqlServer found grab dnn cnn string and create
                string dnnConnection = ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;

                // Manually register a connection
                esConnectionElement conn = new esConnectionElement();
                conn.ConnectionString = dnnConnection;
                conn.Name = "SiteSqlServer";
                conn.Provider = "EntitySpaces.SqlClientProvider";
                conn.ProviderClass = "DataProvider";
                conn.SqlAccessType = esSqlAccessType.DynamicSQL;
                conn.ProviderMetadataKey = "esDefault";
                conn.DatabaseVersion = "2005";

                // Assign the Default Connection
                esConfigSettings.ConnectionInfo.Connections.Add(conn);
                esConfigSettings.ConnectionInfo.Default = "SiteSqlServer";

                // Register the Loader
                esProviderFactory.Factory = new EntitySpaces.LoaderMT.esDataProviderFactory();
            }
        }

        #endregion
    }
}