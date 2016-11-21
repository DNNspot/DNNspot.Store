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
using System.Data;
using System.Linq;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class ShippingService : esShippingService
	{
		public ShippingService()
		{

        }
        public bool IsEnabled
        {
            get
            {
                var settings = this.GetSettingsDictionary();
                if (settings.ContainsKey("IsEnabled"))
                {
                    return WA.Parser.ToBool(settings["IsEnabled"]).GetValueOrDefault(false);
                }
                return false;
            }
        }

        public static ShippingService Get(int id)
        {
            var service = new ShippingService();
            if (service.LoadByPrimaryKey(id))
            {
                return service;
            }
            return null;
        }

        public static ShippingService Find(int storeId, ShippingProviderType providerType)
        {
            var q = new ShippingServiceQuery();
            q.Where(q.StoreId == storeId, q.ShippingProviderType == (short)providerType);

            var service = new ShippingService();
            if (service.Load(q))
            {
                return service;
            }
            return null;
        }

        public static ShippingService FindOrCreateNew(int storeId, ShippingProviderType providerType)
        {
            var service = Find(storeId, providerType);
            if (service == null)
            {
                service = new ShippingService();
                service.StoreId = storeId;
                service.ShippingProviderType = (short)providerType;
                service.Save();
            }
            return service;
        }

        public List<ShippingServiceRateType> GetAllRateTypes()
        {
            //List<ShippingServiceRateType> rateTypes = this.ShippingServiceRateTypeCollectionByShippingServiceId;
            //rateTypes.Sort((left, right) => left.DisplayName.CompareTo(right.DisplayName));
            //return rateTypes;

            var q = new ShippingServiceRateTypeQuery();
            q.Where(q.ShippingServiceId == this.Id);
            q.OrderBy(q.SortOrder.Ascending, q.DisplayName.Ascending);

            var collection = new ShippingServiceRateTypeCollection();
            collection.Load(q);

            return collection.ToList();
        }

        public List<ShippingServiceRateType> GetEnabledRateTypes()
        {
            return GetAllRateTypes().Where(rt => rt.IsEnabled.GetValueOrDefault()).ToList();
        }

        public Dictionary<string, string> GetSettingsDictionary()
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            ShippingServiceSettingQuery q = new ShippingServiceSettingQuery();
            q.Select(q.Name, q.Value);
            q.Where(q.ShippingServiceId == this.Id.GetValueOrDefault(-1));

            using (IDataReader reader = q.ExecuteReader())
            {
                while (reader.Read())
                {
                    settings[reader.GetString(0)] = reader.GetString(1);
                }
                reader.Close();
            }

            return settings;
        }

        /// <summary>
        /// This will DELETE and then INSERT each setting
        /// </summary>
        /// <param name="shippingServiceId"></param>
        /// <param name="settings"></param>
        public void UpdateSettingsDictionary(Dictionary<string, string> settings)
        {
            using (esTransactionScope transaction = new esTransactionScope())
            {
                // DELETE all existing settings for this service
                ShippingServiceSettingQuery qDelete = new ShippingServiceSettingQuery();
                qDelete.Where(qDelete.ShippingServiceId == this.Id.Value);
                ShippingServiceSettingCollection oldSettings = new ShippingServiceSettingCollection();
                oldSettings.Load(qDelete);
                oldSettings.MarkAllAsDeleted();
                oldSettings.Save();

                // INSERT new settings for this service
                if (settings.Keys.Count > 0)
                {
                    ShippingServiceSettingCollection newSettings = new ShippingServiceSettingCollection();
                    foreach (KeyValuePair<string, string> setting in settings)
                    {
                        ShippingServiceSetting newSetting = newSettings.AddNew();
                        newSetting.ShippingServiceId = this.Id.Value;
                        newSetting.Name = setting.Key;
                        newSetting.Value = setting.Value;
                    }
                    newSettings.Save();
                }

                transaction.Complete();
            }
        }
	}
}
