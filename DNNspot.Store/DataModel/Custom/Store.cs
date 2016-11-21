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
using System.Globalization;
using System.Linq;
using DNNspot.Store.Shipping;
using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;
using WA.Extensions;

namespace DNNspot.Store.DataModel
{
	public partial class Store : esStore
	{
		public Store()
		{
		
		}

        Dictionary<string, string> settings = null;
        Currency currency = null;

        public AddressInfo Address
        {
            get
            {
                var address = new AddressInfo()
                {
                    Address1 = GetSetting(StoreSettingNames.StoreAddressStreet),
                    City = GetSetting(StoreSettingNames.StoreAddressCity),
                    Region = GetSetting(StoreSettingNames.StoreAddressRegion),
                    PostalCode = GetSetting(StoreSettingNames.StoreAddressPostalCode),
                    Country = GetSetting(StoreSettingNames.StoreAddressCountryCode),
                };

                return address;
            }
        }

        public Currency Currency
        {
            get
            {
                if (currency == null)
                {
                    string code = this.GetSetting(StoreSettingNames.CurrencyCode);
                    if (string.IsNullOrEmpty(code))
                    {
                        currency = Currency.Get("USD") ?? new Currency() { Code = "USD", Description = "United States of America, Dollars", Symbol = "$", SymbolPosition = "prefix", GroupSeparator = ",", DecimalSeparator = "." };
                    }
                    else
                    {
                        currency = Currency.Get(code);
                    }
                }
                return currency;
            }
        }

        public string FormatCurrency(decimal? amount)
        {
            if (amount.HasValue)
            {
                if (!string.IsNullOrEmpty(Currency.CultureName))
                {
                    // use built-in .NET formatting
                    var culture = new CultureInfo(Currency.CultureName);
                    return amount.Value.ToString("C", culture.NumberFormat);
                }
                else
                {
                    // roll-our-own (use the US culture as a template)
                    var usCulture = new CultureInfo("en-US");
                    string usValueFormat = amount.Value.ToString("C", usCulture.NumberFormat);
                    string temp = usValueFormat.Replace(",", currency.GroupSeparator).Replace(".", currency.DecimalSeparator);
                    if (currency.SymbolPosition.ToLower() == "prefix")
                    {
                        temp = temp.Replace("$", currency.Symbol);
                    }
                    else if (currency.SymbolPosition.ToLower() == "suffix")
                    {
                        temp = temp.Replace("$", string.Empty);
                        temp += " " + currency.Symbol;
                    }
                    else
                    {
                        temp = temp.Replace("$", string.Empty);
                    }
                    return temp;
                }
            }
            return string.Empty;
        }

        public static Store GetStore(int storeId)
        {
            Store store = new Store();
            if (store.LoadByPrimaryKey(storeId))
            {
                return store;
            }
            return null;
        }

        public static Store GetStoreByPortalId(int portalId)
        {
            StoreQuery q = new StoreQuery();
            q.es.Top = 1;
            q.Where(q.PortalId == portalId);

            Store store = new Store();
            if (store.Load(q))
            {
                return store;
            }
            return null;
        }

        private void LoadSettings()
        {
            var q = new StoreSettingQuery();
            q.Select(q.Name, q.Value);
            q.Where(q.StoreId == this.Id.Value);

            Dictionary<string, string> dict = new Dictionary<string, string>();

            using (IDataReader reader = q.ExecuteReader())
            {
                while (reader.Read())
                {
                    dict[reader.GetString(0)] = reader.GetString(1);
                }
                reader.Close();
            }

            this.settings = dict;
        }

        public string GetSetting(StoreSettingNames settingName)
        {
            if (this.settings == null)
            {
                LoadSettings();
            }

            return settings.TryGetValueOrEmpty(settingName.ToString());
        }

        public bool? GetSettingBool(StoreSettingNames settingName)
        {
            return WA.Parser.ToBool(GetSetting(settingName));
        }

        public int? GetSettingInt(StoreSettingNames settingName)
        {
            return WA.Parser.ToInt(GetSetting(settingName));
        }

        public void UpdateSetting(StoreSettingNames settingName, string value)
        {
            StoreSetting setting = new StoreSetting();
            if (setting.LoadByPrimaryKey(this.Id.Value, settingName.ToString()))
            {
                setting.Value = value;
            }
            else
            {
                setting.StoreId = this.Id.Value;
                setting.Name = settingName.ToString();
                setting.Value = value;
            }
            setting.Save();
        }

        public List<vStoreEmailTemplate> GetAllEmailTemplates()
        {
            vStoreEmailTemplateQuery q = new vStoreEmailTemplateQuery();
            q.Where(q.StoreId == this.Id);
            q.OrderBy(q.NameKey.Ascending);

            vStoreEmailTemplateCollection collection = new vStoreEmailTemplateCollection();
            collection.Load(q);

            return collection.ToList();
        }

        public vStoreEmailTemplate GetStoreEmailTemplate(EmailTemplateNames templateName)
        {
            vStoreEmailTemplateQuery q = new vStoreEmailTemplateQuery();
            q.es.Top = 1;
            q.Where(q.StoreId == this.Id, q.NameKey == templateName.ToString());

            vStoreEmailTemplate template = new vStoreEmailTemplate();
            if (template.Load(q))
            {
                return template;
            }
            return null;
        }

        //public List<ShippingProductWeightOption> GetShippingMethods()
        //{
        //    return ShippingProductWeightOptionCollection.GetList(this.Id.GetValueOrDefault(-1));
        //}

        public List<IShippingService> GetEnabledShippingProviders()
        {
            var enabledServices = new List<IShippingService>();

            foreach (var service in this.ShippingServiceCollectionByStoreId)
            {
                if (service.IsEnabled)
                {
                    enabledServices.Add(ShippingServiceFactory.Get(this.Id.Value, (ShippingProviderType)service.ShippingProviderType.Value));
                }
            }

            return enabledServices;

            //IList<ShippingProviderType> shippingProviderTypes = WA.Enum<ShippingProviderType>.GetValues();
            //shippingProviderTypes.Remove(ShippingProviderType.UNKNOWN);
            //foreach (ShippingProviderType providerType in shippingProviderTypes)
            //{

            //    var shippingProvider = ShippingProviderFactory.Get(this.Id.Value, providerType);
            //    if (shippingProvider.IsEnabled)
            //    {
            //        enabledProviders.Add(shippingProvider);
            //    }
            //}
            //return enabledProviders;
        }

        public List<IShippingService> GetEnabledShippingProviders(int? orderId, Guid? cartId)
        {
            var enabledServices = new List<IShippingService>();

            foreach (var service in this.ShippingServiceCollectionByStoreId)
            {
                if (service.IsEnabled)
                {
                    enabledServices.Add(ShippingServiceFactory.Get(this.Id.Value, (ShippingProviderType)service.ShippingProviderType.Value, orderId, cartId));
                }
            }

            return enabledServices;

            //IList<ShippingProviderType> shippingProviderTypes = WA.Enum<ShippingProviderType>.GetValues();
            //shippingProviderTypes.Remove(ShippingProviderType.UNKNOWN);
            //foreach (ShippingProviderType providerType in shippingProviderTypes)
            //{

            //    var shippingProvider = ShippingProviderFactory.Get(this.Id.Value, providerType);
            //    if (shippingProvider.IsEnabled)
            //    {
            //        enabledProviders.Add(shippingProvider);
            //    }
            //}
            //return enabledProviders;
        }
        //public List<ShippingOption> GetShippingOptions()
        //{
        //    List<ShippingOption> options = new List<ShippingOption>();

        //    GetEnabledShippingProviders().ForEach(p => options.AddRange(p.GetShippingOptions()));

        //    return options;
        //}

        //public ShippingOption GetShippingOptionByListKey(string shippingOptionListKey)
        //{
        //    List<ShippingOption> options = GetShippingOptions();

        //    ShippingOption o = options.Find(x => x.ListKey == shippingOptionListKey);            

        //    return o ?? new ShippingOption();
        //}

        //public List<ShippingOption> GetShippingOptions(AddressInfo origin, AddressInfo destination)
        //{
        //    List<ShippingOption> options = new List<ShippingOption>();

        //    GetEnabledShippingProviders().ForEach(p => options.AddRange(p.GetShippingOptions(origin, destination)));

        //    return options;
        //}

        //public List<ShippingOption> GetShippingOptionEstimates(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts)
        //{
        //    List<string> errors;
        //    return GetShippingOptionEstimates(origin, destination, cartProducts, out errors);
        //}

        //public List<ShippingOption> GetShippingOptionEstimates(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts, out List<string> errorMessages)
        //{
        //    errorMessages = new List<string>();

        //    List<ShippingOption> estimates = new List<ShippingOption>();

        //    var providers = GetEnabledShippingProviders();
        //    foreach(var p in providers)
        //    {
        //        estimates.AddRange(p.GetShippingOptionEstimates(origin, destination, cartProducts));
        //        if(p.ErrorMessages.Count > 0)
        //        {
        //            errorMessages.AddRange(p.ErrorMessages.ConvertAll(s => string.Format(@"ShippingProvider ""{0}"". {1}", p.ToString(), s)));
        //            //p.ErrorMessages.ForEach(s => Exceptions.LogException(new ModuleLoadException(string.Format(@"ShippingProvider: {0} {1}", p.ToString(), s))));
        //        }
        //    }

        //    errorMessages.ForEach(s => Exceptions.LogException(new ModuleLoadException(s)));

        //    estimates.Sort((left,right) => left.Cost.GetValueOrDefault(0).CompareTo(right.Cost.GetValueOrDefault(0)));

        //    return estimates;
        //}

        //public decimal GetShippingOptionCost(AddressInfo origin, AddressInfo destination, List<vCartItemProductInfo> cartProducts, string shippingOptionListKey)
        //{
        //    var option = GetShippingOptionByListKey(shippingOptionListKey);
        //    if(option == null)
        //    {
        //        throw new ApplicationException("Unable to find ShippingOption with ListKey '" + shippingOptionListKey + "'");
        //    }

        //    var shippingProvider = ShippingProviderFactory.Get(this.Id.Value, option.ProviderType);

        //    return shippingProvider.GetShippingOptionCost(origin, destination, cartProducts, option);             
        //}

        public bool IsPaymentProviderEnabled(PaymentProviderName paymentProviderName)
        {
            var config = this.GetPaymentProviderConfig(paymentProviderName);

            return config.Settings.ContainsKey("IsEnabled")
                ? WA.Parser.ToBool(config.Settings["IsEnabled"]).GetValueOrDefault(false)
                : false;
        }

        public ProviderConfig GetPaymentProviderConfig(PaymentProviderName paymentProviderName)
        {
            List<ProviderConfig> configs = GetPaymentProviderConfigs();
            ProviderConfig config = configs.Find(c => c.ProviderId == (short)paymentProviderName);
            if (config == null)
            {
                config = new ProviderConfig();
                //config.IsEnabled = true;
                config.ProviderId = (short)paymentProviderName;
            }

            return config;
        }

        public List<ProviderConfig> GetPaymentProviderConfigs()
        {
            List<ProviderConfig> paymentConfigs = new List<ProviderConfig>();

            foreach (StorePaymentProvider spp in this.StorePaymentProviderCollectionByStoreId)
            {
                ProviderConfig config = new ProviderConfig();
                config.ProviderId = spp.UpToPaymentProviderByPaymentProviderId.Id;
                //config.IsEnabled = spp.IsEnabled.GetValueOrDefault();
                config.Settings = StorePaymentProviderSettingCollection.GetSettingsDictionary(spp.StoreId.Value, spp.PaymentProviderId.Value);

                paymentConfigs.Add(config);
            }

            return paymentConfigs;
        }

        public void UpdatePaymentProviderConfig(ProviderConfig config)
        {
            int storeId = this.Id.Value;

            PaymentProvider provider = PaymentProvider.Get(config.ProviderId.GetValueOrDefault(-1));

            StorePaymentProvider storePaymentProvider = StorePaymentProvider.GetOrCreate(storeId, provider.Id.Value);
            //storePaymentProvider.IsEnabled = config.IsEnabled;
            storePaymentProvider.Save();

            StorePaymentProviderSettingCollection.UpdateSettingsDictionary(storeId, provider.Id.Value, config.Settings);
        }

        public List<PaymentProviderName> GetOnsitePaymentProviders()
        {
            List<PaymentProviderName> providers = new List<PaymentProviderName>();
            if (IsPaymentProviderEnabled(PaymentProviderName.AuthorizeNetAim))
            {
                providers.Add(PaymentProviderName.AuthorizeNetAim);
            }
            if (IsPaymentProviderEnabled(PaymentProviderName.PayPalDirectPayment))
            {
                providers.Add(PaymentProviderName.PayPalDirectPayment);
            }
            if (IsPaymentProviderEnabled(PaymentProviderName.CardCaptureOnly))
            {
                providers.Add(PaymentProviderName.CardCaptureOnly);
            }
            if (IsPaymentProviderEnabled(PaymentProviderName.PayLater))
            {
                providers.Add(PaymentProviderName.PayLater);
            }
            return providers;
        }

        public List<PaymentProviderName> GetOffsitePaymentProviders()
        {
            List<PaymentProviderName> providers = new List<PaymentProviderName>();

            if (IsPaymentProviderEnabled(PaymentProviderName.PayPalStandard))
            {
                providers.Add(PaymentProviderName.PayPalStandard);
            }
            if (IsPaymentProviderEnabled(PaymentProviderName.PayPalExpressCheckout))
            {
                providers.Add(PaymentProviderName.PayPalExpressCheckout);
            }
            return providers;
        }

        public void AddMissingEmailTemplates()
        {
            List<vStoreEmailTemplate> storeEmails = GetAllEmailTemplates();

            EmailTemplateCollection allEmailCollection = new EmailTemplateCollection();
            allEmailCollection.LoadAll();
            List<EmailTemplate> allEmails = allEmailCollection.ToList();
            allEmails.RemoveAll(e => storeEmails.Exists(se => se.EmailTemplateId == e.Id));
            List<EmailTemplate> missingEmails = allEmails;

            foreach (EmailTemplate missingEmail in missingEmails)
            {
                StoreEmailTemplate template = this.StoreEmailTemplateCollectionByStoreId.AddNew();
                template.EmailTemplateId = missingEmail.Id;
                template.SubjectTemplate = missingEmail.DefaultSubject;
                template.BodyTemplate = missingEmail.DefaultBody;
            }
            this.Save();
        }

        public static List<CustomerInfo> GetCustomers(int storeId)
        {
            //OrderQuery q = new OrderQuery();
            //q.es.Distinct = true;
            //q.Select(q.UserId);
            //q.Where(q.StoreId == storeId, q.UserId.IsNotNull());            
            //OrderCollection orderUsers = new OrderCollection();
            //orderUsers.Load(q);

            //Dictionary<int,bool> orderUserIdHash = new Dictionary<int, bool>(orderUsers.Count);
            //((List<Order>)orderUsers).ForEach(o => orderUserIdHash[o.UserId.Value] = true);            

            //Store theStore = GetStore(storeId);
            //ArrayList userArray = UserController.GetUsers(theStore.PortalId.Value); // Normal Users
            //userArray.AddRange(UserController.GetUsers(Null.NullInteger));  // Super Users

            //List<UserInfo> userInfos = userArray.ToList<UserInfo>();
            //userInfos.RemoveAll(ui => !orderUserIdHash.ContainsKey(ui.UserID));

            //return userInfos;

            OrderQuery q = new OrderQuery();
            q.es.Distinct = true;
            q.Select(
                q.UserId,
                q.CustomerFirstName,
                q.CustomerLastName,
                q.CustomerEmail,
                q.BillAddress1,
                q.BillAddress2,
                q.BillCity,
                q.BillRegion,
                q.BillPostalCode,
                q.BillCountryCode,
                q.BillTelephone,
                q.ShipRecipientName,
                q.ShipRecipientBusinessName,
                q.ShipAddress1,
                q.ShipAddress2,
                q.ShipCity,
                q.ShipRegion,
                q.ShipPostalCode,
                q.ShipCountryCode,
                q.ShipTelephone
                );
            q.Where(q.StoreId == storeId);

            OrderCollection collection = new OrderCollection();
            collection.Load(q);
            List<Order> orders = collection.ToList();

            List<CustomerInfo> customerInfos = orders.ConvertAll(c => new CustomerInfo()
            {
                UserId = c.UserId,
                FirstName = c.CustomerFirstName,
                LastName = c.CustomerLastName,
                Email = c.CustomerEmail,
                BillAddress1 = c.BillAddress1,
                BillAddress2 = c.BillAddress2,
                BillCity = c.BillCity,
                BillRegion = c.BillRegion,
                BillPostalCode = c.BillPostalCode,
                BillCountryCode = c.BillCountryCode,
                BillTelephone = c.BillTelephone,
                ShipRecipientName = c.ShipRecipientName,
                ShipRecipientBusinessName = c.ShipRecipientBusinessName,
                ShipAddress1 = c.ShipAddress1,
                ShipAddress2 = c.ShipAddress2,
                ShipCity = c.ShipCity,
                ShipRegion = c.ShipRegion,
                ShipPostalCode = c.ShipPostalCode,
                ShipCountryCode = c.ShipCountryCode,
                ShipTelephone = c.ShipTelephone
            });

            return customerInfos;
        }

        public bool DiscountsActive
        {
            get { return DiscountCollection.GetActiveDiscountsForCurrentUser(this.Id.Value).Count > 0; }
        }
	}
}
