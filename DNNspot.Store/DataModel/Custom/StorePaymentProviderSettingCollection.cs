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
Date Generated       : 4/12/2013 3:32:34 PM
===============================================================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;

namespace DNNspot.Store.DataModel
{
	public partial class StorePaymentProviderSettingCollection : esStorePaymentProviderSettingCollection
	{
		public StorePaymentProviderSettingCollection()
		{
		
		}

        public static Dictionary<string, string> GetSettingsDictionary(int storeId, short paymentProviderId)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            StorePaymentProviderSettingQuery q = new StorePaymentProviderSettingQuery();
            q.Select(q.Name, q.Value);
            q.Where(q.StoreId == storeId, q.PaymentProviderId == paymentProviderId);

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

        public static void UpdateSettingsDictionary(int storeId, short paymentProviderId, Dictionary<string, string> settings)
        {
            using (esTransactionScope transaction = new esTransactionScope())
            {
                // DELETE all existing settings for this store/provider
                StorePaymentProviderSettingQuery qDelete = new StorePaymentProviderSettingQuery();
                qDelete.Where(qDelete.StoreId == storeId, qDelete.PaymentProviderId == paymentProviderId);
                StorePaymentProviderSettingCollection oldSettings = new StorePaymentProviderSettingCollection();
                oldSettings.Load(qDelete);
                oldSettings.MarkAllAsDeleted();
                oldSettings.Save();

                // INSERT new settings for this store/provider
                if (settings.Keys.Count > 0)
                {
                    StorePaymentProviderSettingCollection newSettings = new StorePaymentProviderSettingCollection();
                    foreach (KeyValuePair<string, string> setting in settings)
                    {
                        StorePaymentProviderSetting newSetting = newSettings.AddNew();
                        newSetting.StoreId = storeId;
                        newSetting.PaymentProviderId = paymentProviderId;
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
