
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
	/// Encapsulates the 'DNNspot_Store_StorePaymentProviderSetting' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(StorePaymentProviderSetting))]	
	[XmlType("StorePaymentProviderSetting")]
	[Table(Name="StorePaymentProviderSetting")]
	public partial class StorePaymentProviderSetting : esStorePaymentProviderSetting
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new StorePaymentProviderSetting();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 storeId, System.Int16 paymentProviderId, System.String name)
		{
			var obj = new StorePaymentProviderSetting();
			obj.StoreId = storeId;
			obj.PaymentProviderId = paymentProviderId;
			obj.Name = name;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 storeId, System.Int16 paymentProviderId, System.String name, esSqlAccessType sqlAccessType)
		{
			var obj = new StorePaymentProviderSetting();
			obj.StoreId = storeId;
			obj.PaymentProviderId = paymentProviderId;
			obj.Name = name;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int32? StoreId
		{
			get { return base.StoreId;  }
			set { base.StoreId = value; }
		}

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int16? PaymentProviderId
		{
			get { return base.PaymentProviderId;  }
			set { base.PaymentProviderId = value; }
		}

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Value
		{
			get { return base.Value;  }
			set { base.Value = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("StorePaymentProviderSettingCollection")]
	public partial class StorePaymentProviderSettingCollection : esStorePaymentProviderSettingCollection, IEnumerable<StorePaymentProviderSetting>
	{
		public StorePaymentProviderSetting FindByPrimaryKey(System.Int32 storeId, System.Int16 paymentProviderId, System.String name)
		{
			return this.SingleOrDefault(e => e.StoreId == storeId && e.PaymentProviderId == paymentProviderId && e.Name == name);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(StorePaymentProviderSetting))]
		public class StorePaymentProviderSettingCollectionWCFPacket : esCollectionWCFPacket<StorePaymentProviderSettingCollection>
		{
			public static implicit operator StorePaymentProviderSettingCollection(StorePaymentProviderSettingCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator StorePaymentProviderSettingCollectionWCFPacket(StorePaymentProviderSettingCollection collection)
			{
				return new StorePaymentProviderSettingCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class StorePaymentProviderSettingQuery : esStorePaymentProviderSettingQuery
	{
		public StorePaymentProviderSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "StorePaymentProviderSettingQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(StorePaymentProviderSettingQuery query)
		{
			return StorePaymentProviderSettingQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator StorePaymentProviderSettingQuery(string query)
		{
			return (StorePaymentProviderSettingQuery)StorePaymentProviderSettingQuery.SerializeHelper.FromXml(query, typeof(StorePaymentProviderSettingQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esStorePaymentProviderSetting : esEntity
	{
		public esStorePaymentProviderSetting()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 storeId, System.Int16 paymentProviderId, System.String name)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, paymentProviderId, name);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, paymentProviderId, name);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 storeId, System.Int16 paymentProviderId, System.String name)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, paymentProviderId, name);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, paymentProviderId, name);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 storeId, System.Int16 paymentProviderId, System.String name)
		{
			StorePaymentProviderSettingQuery query = new StorePaymentProviderSettingQuery();
			query.Where(query.StoreId == storeId, query.PaymentProviderId == paymentProviderId, query.Name == name);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 storeId, System.Int16 paymentProviderId, System.String name)
		{
			esParameters parms = new esParameters();
			parms.Add("StoreId", storeId);			parms.Add("PaymentProviderId", paymentProviderId);			parms.Add("Name", name);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_StorePaymentProviderSetting.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(StorePaymentProviderSettingMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(StorePaymentProviderSettingMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(StorePaymentProviderSettingMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StorePaymentProviderSetting.PaymentProviderId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? PaymentProviderId
		{
			get
			{
				return base.GetSystemInt16(StorePaymentProviderSettingMetadata.ColumnNames.PaymentProviderId);
			}
			
			set
			{
				if(base.SetSystemInt16(StorePaymentProviderSettingMetadata.ColumnNames.PaymentProviderId, value))
				{
					this._UpToPaymentProviderByPaymentProviderId = null;
					this.OnPropertyChanged("UpToPaymentProviderByPaymentProviderId");
					OnPropertyChanged(StorePaymentProviderSettingMetadata.PropertyNames.PaymentProviderId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StorePaymentProviderSetting.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(StorePaymentProviderSettingMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(StorePaymentProviderSettingMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(StorePaymentProviderSettingMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StorePaymentProviderSetting.Value
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Value
		{
			get
			{
				return base.GetSystemString(StorePaymentProviderSettingMetadata.ColumnNames.Value);
			}
			
			set
			{
				if(base.SetSystemString(StorePaymentProviderSettingMetadata.ColumnNames.Value, value))
				{
					OnPropertyChanged(StorePaymentProviderSettingMetadata.PropertyNames.Value);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected PaymentProvider _UpToPaymentProviderByPaymentProviderId;
		[CLSCompliant(false)]
		internal protected Store _UpToStoreByStoreId;
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
						case "StoreId": this.str().StoreId = (string)value; break;							
						case "PaymentProviderId": this.str().PaymentProviderId = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "Value": this.str().Value = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(StorePaymentProviderSettingMetadata.PropertyNames.StoreId);
							break;
						
						case "PaymentProviderId":
						
							if (value == null || value is System.Int16)
								this.PaymentProviderId = (System.Int16?)value;
								OnPropertyChanged(StorePaymentProviderSettingMetadata.PropertyNames.PaymentProviderId);
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
			public esStrings(esStorePaymentProviderSetting entity)
			{
				this.entity = entity;
			}
			
	
			public System.String StoreId
			{
				get
				{
					System.Int32? data = entity.StoreId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StoreId = null;
					else entity.StoreId = Convert.ToInt32(value);
				}
			}
				
			public System.String PaymentProviderId
			{
				get
				{
					System.Int16? data = entity.PaymentProviderId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentProviderId = null;
					else entity.PaymentProviderId = Convert.ToInt16(value);
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
				
			public System.String Value
			{
				get
				{
					System.String data = entity.Value;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Value = null;
					else entity.Value = Convert.ToString(value);
				}
			}
			

			private esStorePaymentProviderSetting entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return StorePaymentProviderSettingMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public StorePaymentProviderSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StorePaymentProviderSettingQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StorePaymentProviderSettingQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(StorePaymentProviderSettingQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private StorePaymentProviderSettingQuery query;		
	}



	[Serializable]
	abstract public partial class esStorePaymentProviderSettingCollection : esEntityCollection<StorePaymentProviderSetting>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StorePaymentProviderSettingMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "StorePaymentProviderSettingCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public StorePaymentProviderSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StorePaymentProviderSettingQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StorePaymentProviderSettingQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StorePaymentProviderSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(StorePaymentProviderSettingQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((StorePaymentProviderSettingQuery)query);
		}

		#endregion
		
		private StorePaymentProviderSettingQuery query;
	}



	[Serializable]
	abstract public partial class esStorePaymentProviderSettingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StorePaymentProviderSettingMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StoreId": return this.StoreId;
				case "PaymentProviderId": return this.PaymentProviderId;
				case "Name": return this.Name;
				case "Value": return this.Value;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, StorePaymentProviderSettingMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem PaymentProviderId
		{
			get { return new esQueryItem(this, StorePaymentProviderSettingMetadata.ColumnNames.PaymentProviderId, esSystemType.Int16); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, StorePaymentProviderSettingMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Value
		{
			get { return new esQueryItem(this, StorePaymentProviderSettingMetadata.ColumnNames.Value, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class StorePaymentProviderSetting : esStorePaymentProviderSetting
	{

				
		#region UpToPaymentProviderByPaymentProviderId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_PaymentProcessor
		/// </summary>

		[XmlIgnore]
					
		public PaymentProvider UpToPaymentProviderByPaymentProviderId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToPaymentProviderByPaymentProviderId == null && PaymentProviderId != null)
				{
					this._UpToPaymentProviderByPaymentProviderId = new PaymentProvider();
					this._UpToPaymentProviderByPaymentProviderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToPaymentProviderByPaymentProviderId", this._UpToPaymentProviderByPaymentProviderId);
					this._UpToPaymentProviderByPaymentProviderId.Query.Where(this._UpToPaymentProviderByPaymentProviderId.Query.Id == this.PaymentProviderId);
					this._UpToPaymentProviderByPaymentProviderId.Query.Load();
				}	
				return this._UpToPaymentProviderByPaymentProviderId;
			}
			
			set
			{
				this.RemovePreSave("UpToPaymentProviderByPaymentProviderId");
				

				if(value == null)
				{
					this.PaymentProviderId = null;
					this._UpToPaymentProviderByPaymentProviderId = null;
				}
				else
				{
					this.PaymentProviderId = value.Id;
					this._UpToPaymentProviderByPaymentProviderId = value;
					this.SetPreSave("UpToPaymentProviderByPaymentProviderId", this._UpToPaymentProviderByPaymentProviderId);
				}
				
			}
		}
		#endregion
		

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
					
		public Store UpToStoreByStoreId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToStoreByStoreId == null && StoreId != null)
				{
					this._UpToStoreByStoreId = new Store();
					this._UpToStoreByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToStoreByStoreId", this._UpToStoreByStoreId);
					this._UpToStoreByStoreId.Query.Where(this._UpToStoreByStoreId.Query.Id == this.StoreId);
					this._UpToStoreByStoreId.Query.Load();
				}	
				return this._UpToStoreByStoreId;
			}
			
			set
			{
				this.RemovePreSave("UpToStoreByStoreId");
				

				if(value == null)
				{
					this.StoreId = null;
					this._UpToStoreByStoreId = null;
				}
				else
				{
					this.StoreId = value.Id;
					this._UpToStoreByStoreId = value;
					this.SetPreSave("UpToStoreByStoreId", this._UpToStoreByStoreId);
				}
				
			}
		}
		#endregion
		

		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
			if(!this.es.IsDeleted && this._UpToStoreByStoreId != null)
			{
				this.StoreId = this._UpToStoreByStoreId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class StorePaymentProviderSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StorePaymentProviderSettingMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StorePaymentProviderSettingMetadata.ColumnNames.StoreId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StorePaymentProviderSettingMetadata.PropertyNames.StoreId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StorePaymentProviderSettingMetadata.ColumnNames.PaymentProviderId, 1, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = StorePaymentProviderSettingMetadata.PropertyNames.PaymentProviderId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StorePaymentProviderSettingMetadata.ColumnNames.Name, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = StorePaymentProviderSettingMetadata.PropertyNames.Name;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 500;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StorePaymentProviderSettingMetadata.ColumnNames.Value, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = StorePaymentProviderSettingMetadata.PropertyNames.Value;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public StorePaymentProviderSettingMetadata Meta()
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
			 public const string StoreId = "StoreId";
			 public const string PaymentProviderId = "PaymentProviderId";
			 public const string Name = "Name";
			 public const string Value = "Value";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StoreId = "StoreId";
			 public const string PaymentProviderId = "PaymentProviderId";
			 public const string Name = "Name";
			 public const string Value = "Value";
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
			lock (typeof(StorePaymentProviderSettingMetadata))
			{
				if(StorePaymentProviderSettingMetadata.mapDelegates == null)
				{
					StorePaymentProviderSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StorePaymentProviderSettingMetadata.meta == null)
				{
					StorePaymentProviderSettingMetadata.meta = new StorePaymentProviderSettingMetadata();
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


				meta.AddTypeMap("StoreId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PaymentProviderId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Value", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_StorePaymentProviderSetting";
					meta.Destination = objectQualifier + "DNNspot_Store_StorePaymentProviderSetting";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderSettingInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderSettingUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderSettingDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderSettingLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderSettingLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_StorePaymentProviderSetting";
					meta.Destination = "DNNspot_Store_StorePaymentProviderSetting";
									
					meta.spInsert = "proc_DNNspot_Store_StorePaymentProviderSettingInsert";				
					meta.spUpdate = "proc_DNNspot_Store_StorePaymentProviderSettingUpdate";		
					meta.spDelete = "proc_DNNspot_Store_StorePaymentProviderSettingDelete";
					meta.spLoadAll = "proc_DNNspot_Store_StorePaymentProviderSettingLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_StorePaymentProviderSettingLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StorePaymentProviderSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
