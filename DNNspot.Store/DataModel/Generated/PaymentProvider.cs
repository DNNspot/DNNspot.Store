
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using EntitySpaces.Core;
using EntitySpaces.Interfaces;
using EntitySpaces.DynamicQuery;


using DotNetNuke.Framework.Providers;


namespace DNNspot.Store.DataModel
{
	/// <summary>
	/// Encapsulates the 'DNNspot_Store_PaymentProvider' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(PaymentProvider))]	
	[XmlType("PaymentProvider")]
	[Table(Name="PaymentProvider")]
	public partial class PaymentProvider : esPaymentProvider
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new PaymentProvider();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int16 id)
		{
			var obj = new PaymentProvider();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int16 id, esSqlAccessType sqlAccessType)
		{
			var obj = new PaymentProvider();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int16? Id
		{
			get { return base.Id;  }
			set { base.Id = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("PaymentProviderCollection")]
	public partial class PaymentProviderCollection : esPaymentProviderCollection, IEnumerable<PaymentProvider>
	{
		public PaymentProvider FindByPrimaryKey(System.Int16 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(PaymentProvider))]
		public class PaymentProviderCollectionWCFPacket : esCollectionWCFPacket<PaymentProviderCollection>
		{
			public static implicit operator PaymentProviderCollection(PaymentProviderCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator PaymentProviderCollectionWCFPacket(PaymentProviderCollection collection)
			{
				return new PaymentProviderCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class PaymentProviderQuery : esPaymentProviderQuery
	{
		public PaymentProviderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "PaymentProviderQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(PaymentProviderQuery query)
		{
			return PaymentProviderQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator PaymentProviderQuery(string query)
		{
			return (PaymentProviderQuery)PaymentProviderQuery.SerializeHelper.FromXml(query, typeof(PaymentProviderQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esPaymentProvider : esEntity
	{
		public esPaymentProvider()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int16 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int16 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int16 id)
		{
			PaymentProviderQuery query = new PaymentProviderQuery();
			query.Where(query.Id == id);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int16 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id", id);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentProvider.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? Id
		{
			get
			{
				return base.GetSystemInt16(PaymentProviderMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt16(PaymentProviderMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(PaymentProviderMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentProvider.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(PaymentProviderMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(PaymentProviderMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(PaymentProviderMetadata.PropertyNames.Name);
				}
			}
		}		
		
		#endregion	

		#region .str() Properties
		
		public override void SetProperties(IDictionary values)
		{
			foreach (string propertyName in values.Keys)
			{
				this.SetProperty(propertyName, values[propertyName]);
			}
		}
		
		public override void SetProperty(string name, object value)
		{
			esColumnMetadata col = this.Meta.Columns.FindByPropertyName(name);
			if (col != null)
			{
				if(value == null || value is System.String)
				{				
					// Use the strongly typed property
					switch (name)
					{							
						case "Id": this.str().Id = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int16)
								this.Id = (System.Int16?)value;
								OnPropertyChanged(PaymentProviderMetadata.PropertyNames.Id);
							break;
					

						default:
							break;
					}
				}
			}
            else if (this.ContainsColumn(name))
            {
                this.SetColumn(name, value);
            }
			else
			{
				throw new Exception("SetProperty Error: '" + name + "' not found");
			}
		}		

		public esStrings str()
		{
			if (esstrings == null)
			{
				esstrings = new esStrings(this);
			}
			return esstrings;
		}

		sealed public class esStrings
		{
			public esStrings(esPaymentProvider entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int16? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt16(value);
				}
			}
				
			public System.String Name
			{
				get
				{
					System.String data = entity.Name;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Name = null;
					else entity.Name = Convert.ToString(value);
				}
			}
			

			private esPaymentProvider entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return PaymentProviderMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public PaymentProviderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentProviderQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PaymentProviderQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(PaymentProviderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private PaymentProviderQuery query;		
	}



	[Serializable]
	abstract public partial class esPaymentProviderCollection : esEntityCollection<PaymentProvider>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PaymentProviderMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "PaymentProviderCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public PaymentProviderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentProviderQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PaymentProviderQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PaymentProviderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(PaymentProviderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((PaymentProviderQuery)query);
		}

		#endregion
		
		private PaymentProviderQuery query;
	}



	[Serializable]
	abstract public partial class esPaymentProviderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PaymentProviderMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "Name": return this.Name;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, PaymentProviderMetadata.ColumnNames.Id, esSystemType.Int16); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, PaymentProviderMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class PaymentProvider : esPaymentProvider
	{

		#region PaymentTransactionCollectionByPaymentProviderId - Zero To Many
		
		static public esPrefetchMap Prefetch_PaymentTransactionCollectionByPaymentProviderId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.PaymentProvider.PaymentTransactionCollectionByPaymentProviderId_Delegate;
				map.PropertyName = "PaymentTransactionCollectionByPaymentProviderId";
				map.MyColumnName = "PaymentProviderId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void PaymentTransactionCollectionByPaymentProviderId_Delegate(esPrefetchParameters data)
		{
			PaymentProviderQuery parent = new PaymentProviderQuery(data.NextAlias());

			PaymentTransactionQuery me = data.You != null ? data.You as PaymentTransactionQuery : new PaymentTransactionQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.PaymentProviderId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_PaymentTransaction_DNNspot_Store_PaymentProvider
		/// </summary>

		[XmlIgnore]
		public PaymentTransactionCollection PaymentTransactionCollectionByPaymentProviderId
		{
			get
			{
				if(this._PaymentTransactionCollectionByPaymentProviderId == null)
				{
					this._PaymentTransactionCollectionByPaymentProviderId = new PaymentTransactionCollection();
					this._PaymentTransactionCollectionByPaymentProviderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("PaymentTransactionCollectionByPaymentProviderId", this._PaymentTransactionCollectionByPaymentProviderId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._PaymentTransactionCollectionByPaymentProviderId.Query.Where(this._PaymentTransactionCollectionByPaymentProviderId.Query.PaymentProviderId == this.Id);
							this._PaymentTransactionCollectionByPaymentProviderId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._PaymentTransactionCollectionByPaymentProviderId.fks.Add(PaymentTransactionMetadata.ColumnNames.PaymentProviderId, this.Id);
					}
				}

				return this._PaymentTransactionCollectionByPaymentProviderId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._PaymentTransactionCollectionByPaymentProviderId != null) 
				{ 
					this.RemovePostSave("PaymentTransactionCollectionByPaymentProviderId"); 
					this._PaymentTransactionCollectionByPaymentProviderId = null;
					
				} 
			} 			
		}
			
		
		private PaymentTransactionCollection _PaymentTransactionCollectionByPaymentProviderId;
		#endregion

		#region UpToStoreCollectionByStorePaymentProvider - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_PaymentProcessor
		/// </summary>

		[XmlIgnore]
		public StoreCollection UpToStoreCollectionByStorePaymentProvider
		{
			get
			{
				if(this._UpToStoreCollectionByStorePaymentProvider == null)
				{
					this._UpToStoreCollectionByStorePaymentProvider = new StoreCollection();
					this._UpToStoreCollectionByStorePaymentProvider.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToStoreCollectionByStorePaymentProvider", this._UpToStoreCollectionByStorePaymentProvider);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						StoreQuery m = new StoreQuery("m");
						StorePaymentProviderQuery j = new StorePaymentProviderQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.StoreId);
                        m.Where(j.PaymentProviderId == this.Id);

						this._UpToStoreCollectionByStorePaymentProvider.Load(m);
					}
				}

				return this._UpToStoreCollectionByStorePaymentProvider;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToStoreCollectionByStorePaymentProvider != null) 
				{ 
					this.RemovePostSave("UpToStoreCollectionByStorePaymentProvider"); 
					this._UpToStoreCollectionByStorePaymentProvider = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_PaymentProcessor
		/// </summary>
		public void AssociateStoreCollectionByStorePaymentProvider(Store entity)
		{
			if (this._StorePaymentProviderCollection == null)
			{
				this._StorePaymentProviderCollection = new StorePaymentProviderCollection();
				this._StorePaymentProviderCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderCollection", this._StorePaymentProviderCollection);
			}

			StorePaymentProvider obj = this._StorePaymentProviderCollection.AddNew();
			obj.PaymentProviderId = this.Id;
			obj.StoreId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_PaymentProcessor
		/// </summary>
		public void DissociateStoreCollectionByStorePaymentProvider(Store entity)
		{
			if (this._StorePaymentProviderCollection == null)
			{
				this._StorePaymentProviderCollection = new StorePaymentProviderCollection();
				this._StorePaymentProviderCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderCollection", this._StorePaymentProviderCollection);
			}

			StorePaymentProvider obj = this._StorePaymentProviderCollection.AddNew();
			obj.PaymentProviderId = this.Id;
            obj.StoreId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private StoreCollection _UpToStoreCollectionByStorePaymentProvider;
		private StorePaymentProviderCollection _StorePaymentProviderCollection;
		#endregion

		#region StorePaymentProviderCollectionByPaymentProviderId - Zero To Many
		
		static public esPrefetchMap Prefetch_StorePaymentProviderCollectionByPaymentProviderId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.PaymentProvider.StorePaymentProviderCollectionByPaymentProviderId_Delegate;
				map.PropertyName = "StorePaymentProviderCollectionByPaymentProviderId";
				map.MyColumnName = "PaymentProviderId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void StorePaymentProviderCollectionByPaymentProviderId_Delegate(esPrefetchParameters data)
		{
			PaymentProviderQuery parent = new PaymentProviderQuery(data.NextAlias());

			StorePaymentProviderQuery me = data.You != null ? data.You as StorePaymentProviderQuery : new StorePaymentProviderQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.PaymentProviderId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_PaymentProcessor
		/// </summary>

		[XmlIgnore]
		public StorePaymentProviderCollection StorePaymentProviderCollectionByPaymentProviderId
		{
			get
			{
				if(this._StorePaymentProviderCollectionByPaymentProviderId == null)
				{
					this._StorePaymentProviderCollectionByPaymentProviderId = new StorePaymentProviderCollection();
					this._StorePaymentProviderCollectionByPaymentProviderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("StorePaymentProviderCollectionByPaymentProviderId", this._StorePaymentProviderCollectionByPaymentProviderId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._StorePaymentProviderCollectionByPaymentProviderId.Query.Where(this._StorePaymentProviderCollectionByPaymentProviderId.Query.PaymentProviderId == this.Id);
							this._StorePaymentProviderCollectionByPaymentProviderId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._StorePaymentProviderCollectionByPaymentProviderId.fks.Add(StorePaymentProviderMetadata.ColumnNames.PaymentProviderId, this.Id);
					}
				}

				return this._StorePaymentProviderCollectionByPaymentProviderId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._StorePaymentProviderCollectionByPaymentProviderId != null) 
				{ 
					this.RemovePostSave("StorePaymentProviderCollectionByPaymentProviderId"); 
					this._StorePaymentProviderCollectionByPaymentProviderId = null;
					
				} 
			} 			
		}
			
		
		private StorePaymentProviderCollection _StorePaymentProviderCollectionByPaymentProviderId;
		#endregion

		#region UpToStoreCollectionByStorePaymentProviderSetting - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_PaymentProcessor
		/// </summary>

		[XmlIgnore]
		public StoreCollection UpToStoreCollectionByStorePaymentProviderSetting
		{
			get
			{
				if(this._UpToStoreCollectionByStorePaymentProviderSetting == null)
				{
					this._UpToStoreCollectionByStorePaymentProviderSetting = new StoreCollection();
					this._UpToStoreCollectionByStorePaymentProviderSetting.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToStoreCollectionByStorePaymentProviderSetting", this._UpToStoreCollectionByStorePaymentProviderSetting);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						StoreQuery m = new StoreQuery("m");
						StorePaymentProviderSettingQuery j = new StorePaymentProviderSettingQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.StoreId);
                        m.Where(j.PaymentProviderId == this.Id);

						this._UpToStoreCollectionByStorePaymentProviderSetting.Load(m);
					}
				}

				return this._UpToStoreCollectionByStorePaymentProviderSetting;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToStoreCollectionByStorePaymentProviderSetting != null) 
				{ 
					this.RemovePostSave("UpToStoreCollectionByStorePaymentProviderSetting"); 
					this._UpToStoreCollectionByStorePaymentProviderSetting = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_PaymentProcessor
		/// </summary>
		public void AssociateStoreCollectionByStorePaymentProviderSetting(Store entity)
		{
			if (this._StorePaymentProviderSettingCollection == null)
			{
				this._StorePaymentProviderSettingCollection = new StorePaymentProviderSettingCollection();
				this._StorePaymentProviderSettingCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderSettingCollection", this._StorePaymentProviderSettingCollection);
			}

			StorePaymentProviderSetting obj = this._StorePaymentProviderSettingCollection.AddNew();
			obj.PaymentProviderId = this.Id;
			obj.StoreId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_PaymentProcessor
		/// </summary>
		public void DissociateStoreCollectionByStorePaymentProviderSetting(Store entity)
		{
			if (this._StorePaymentProviderSettingCollection == null)
			{
				this._StorePaymentProviderSettingCollection = new StorePaymentProviderSettingCollection();
				this._StorePaymentProviderSettingCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderSettingCollection", this._StorePaymentProviderSettingCollection);
			}

			StorePaymentProviderSetting obj = this._StorePaymentProviderSettingCollection.AddNew();
			obj.PaymentProviderId = this.Id;
            obj.StoreId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private StoreCollection _UpToStoreCollectionByStorePaymentProviderSetting;
		private StorePaymentProviderSettingCollection _StorePaymentProviderSettingCollection;
		#endregion

		#region StorePaymentProviderSettingCollectionByPaymentProviderId - Zero To Many
		
		static public esPrefetchMap Prefetch_StorePaymentProviderSettingCollectionByPaymentProviderId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.PaymentProvider.StorePaymentProviderSettingCollectionByPaymentProviderId_Delegate;
				map.PropertyName = "StorePaymentProviderSettingCollectionByPaymentProviderId";
				map.MyColumnName = "PaymentProviderId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void StorePaymentProviderSettingCollectionByPaymentProviderId_Delegate(esPrefetchParameters data)
		{
			PaymentProviderQuery parent = new PaymentProviderQuery(data.NextAlias());

			StorePaymentProviderSettingQuery me = data.You != null ? data.You as StorePaymentProviderSettingQuery : new StorePaymentProviderSettingQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.PaymentProviderId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_PaymentProcessor
		/// </summary>

		[XmlIgnore]
		public StorePaymentProviderSettingCollection StorePaymentProviderSettingCollectionByPaymentProviderId
		{
			get
			{
				if(this._StorePaymentProviderSettingCollectionByPaymentProviderId == null)
				{
					this._StorePaymentProviderSettingCollectionByPaymentProviderId = new StorePaymentProviderSettingCollection();
					this._StorePaymentProviderSettingCollectionByPaymentProviderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("StorePaymentProviderSettingCollectionByPaymentProviderId", this._StorePaymentProviderSettingCollectionByPaymentProviderId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._StorePaymentProviderSettingCollectionByPaymentProviderId.Query.Where(this._StorePaymentProviderSettingCollectionByPaymentProviderId.Query.PaymentProviderId == this.Id);
							this._StorePaymentProviderSettingCollectionByPaymentProviderId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._StorePaymentProviderSettingCollectionByPaymentProviderId.fks.Add(StorePaymentProviderSettingMetadata.ColumnNames.PaymentProviderId, this.Id);
					}
				}

				return this._StorePaymentProviderSettingCollectionByPaymentProviderId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._StorePaymentProviderSettingCollectionByPaymentProviderId != null) 
				{ 
					this.RemovePostSave("StorePaymentProviderSettingCollectionByPaymentProviderId"); 
					this._StorePaymentProviderSettingCollectionByPaymentProviderId = null;
					
				} 
			} 			
		}
			
		
		private StorePaymentProviderSettingCollection _StorePaymentProviderSettingCollectionByPaymentProviderId;
		#endregion

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "PaymentTransactionCollectionByPaymentProviderId":
					coll = this.PaymentTransactionCollectionByPaymentProviderId;
					break;
				case "StorePaymentProviderCollectionByPaymentProviderId":
					coll = this.StorePaymentProviderCollectionByPaymentProviderId;
					break;
				case "StorePaymentProviderSettingCollectionByPaymentProviderId":
					coll = this.StorePaymentProviderSettingCollectionByPaymentProviderId;
					break;	
			}

			return coll;
		}		
		/// <summary>
		/// Used internally by the entity's hierarchical properties.
		/// </summary>
		protected override List<esPropertyDescriptor> GetHierarchicalProperties()
		{
			List<esPropertyDescriptor> props = new List<esPropertyDescriptor>();
			
			props.Add(new esPropertyDescriptor(this, "PaymentTransactionCollectionByPaymentProviderId", typeof(PaymentTransactionCollection), new PaymentTransaction()));
			props.Add(new esPropertyDescriptor(this, "StorePaymentProviderCollectionByPaymentProviderId", typeof(StorePaymentProviderCollection), new StorePaymentProvider()));
			props.Add(new esPropertyDescriptor(this, "StorePaymentProviderSettingCollectionByPaymentProviderId", typeof(StorePaymentProviderSettingCollection), new StorePaymentProviderSetting()));
		
			return props;
		}
		
	}
	



	[Serializable]
	public partial class PaymentProviderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PaymentProviderMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PaymentProviderMetadata.ColumnNames.Id, 0, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = PaymentProviderMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentProviderMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentProviderMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public PaymentProviderMetadata Meta()
		{
			return meta;
		}	
		
		public Guid DataID
		{
			get { return base.m_dataID; }
		}	
		
		public bool MultiProviderMode
		{
			get { return false; }
		}		

		public esColumnMetadataCollection Columns
		{
			get	{ return base.m_columns; }
		}
		
		#region ColumnNames
		public class ColumnNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
		}
		#endregion	

		public esProviderSpecificMetadata GetProviderMetadata(string mapName)
		{
			MapToMeta mapMethod = mapDelegates[mapName];

			if (mapMethod != null)
				return mapMethod(mapName);
			else
				return null;
		}
		
		#region MAP esDefault
		
		static private int RegisterDelegateesDefault()
		{
			// This is only executed once per the life of the application
			lock (typeof(PaymentProviderMetadata))
			{
				if(PaymentProviderMetadata.mapDelegates == null)
				{
					PaymentProviderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PaymentProviderMetadata.meta == null)
				{
					PaymentProviderMetadata.meta = new PaymentProviderMetadata();
				}
				
				MapToMeta mapMethod = new MapToMeta(meta.esDefault);
				mapDelegates.Add("esDefault", mapMethod);
				mapMethod("esDefault");
			}
			return 0;
		}			

		private esProviderSpecificMetadata esDefault(string mapName)
		{
			if(!m_providerMetadataMaps.ContainsKey(mapName))
			{
				esProviderSpecificMetadata meta = new esProviderSpecificMetadata();			


				meta.AddTypeMap("Id", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_PaymentProvider";
					meta.Destination = objectQualifier + "DNNspot_Store_PaymentProvider";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_PaymentProviderInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_PaymentProviderUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_PaymentProviderDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_PaymentProviderLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_PaymentProviderLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_PaymentProvider";
					meta.Destination = "DNNspot_Store_PaymentProvider";
									
					meta.spInsert = "proc_DNNspot_Store_PaymentProviderInsert";				
					meta.spUpdate = "proc_DNNspot_Store_PaymentProviderUpdate";		
					meta.spDelete = "proc_DNNspot_Store_PaymentProviderDelete";
					meta.spLoadAll = "proc_DNNspot_Store_PaymentProviderLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_PaymentProviderLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PaymentProviderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
