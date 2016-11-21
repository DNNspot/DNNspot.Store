
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
	/// Encapsulates the 'DNNspot_Store_StorePaymentProvider' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(StorePaymentProvider))]	
	[XmlType("StorePaymentProvider")]
	[Table(Name="StorePaymentProvider")]
	public partial class StorePaymentProvider : esStorePaymentProvider
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new StorePaymentProvider();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 storeId, System.Int16 paymentProviderId)
		{
			var obj = new StorePaymentProvider();
			obj.StoreId = storeId;
			obj.PaymentProviderId = paymentProviderId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 storeId, System.Int16 paymentProviderId, esSqlAccessType sqlAccessType)
		{
			var obj = new StorePaymentProvider();
			obj.StoreId = storeId;
			obj.PaymentProviderId = paymentProviderId;
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


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("StorePaymentProviderCollection")]
	public partial class StorePaymentProviderCollection : esStorePaymentProviderCollection, IEnumerable<StorePaymentProvider>
	{
		public StorePaymentProvider FindByPrimaryKey(System.Int32 storeId, System.Int16 paymentProviderId)
		{
			return this.SingleOrDefault(e => e.StoreId == storeId && e.PaymentProviderId == paymentProviderId);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(StorePaymentProvider))]
		public class StorePaymentProviderCollectionWCFPacket : esCollectionWCFPacket<StorePaymentProviderCollection>
		{
			public static implicit operator StorePaymentProviderCollection(StorePaymentProviderCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator StorePaymentProviderCollectionWCFPacket(StorePaymentProviderCollection collection)
			{
				return new StorePaymentProviderCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class StorePaymentProviderQuery : esStorePaymentProviderQuery
	{
		public StorePaymentProviderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "StorePaymentProviderQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(StorePaymentProviderQuery query)
		{
			return StorePaymentProviderQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator StorePaymentProviderQuery(string query)
		{
			return (StorePaymentProviderQuery)StorePaymentProviderQuery.SerializeHelper.FromXml(query, typeof(StorePaymentProviderQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esStorePaymentProvider : esEntity
	{
		public esStorePaymentProvider()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 storeId, System.Int16 paymentProviderId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, paymentProviderId);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, paymentProviderId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 storeId, System.Int16 paymentProviderId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, paymentProviderId);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, paymentProviderId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 storeId, System.Int16 paymentProviderId)
		{
			StorePaymentProviderQuery query = new StorePaymentProviderQuery();
			query.Where(query.StoreId == storeId, query.PaymentProviderId == paymentProviderId);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 storeId, System.Int16 paymentProviderId)
		{
			esParameters parms = new esParameters();
			parms.Add("StoreId", storeId);			parms.Add("PaymentProviderId", paymentProviderId);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_StorePaymentProvider.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(StorePaymentProviderMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(StorePaymentProviderMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(StorePaymentProviderMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StorePaymentProvider.PaymentProviderId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? PaymentProviderId
		{
			get
			{
				return base.GetSystemInt16(StorePaymentProviderMetadata.ColumnNames.PaymentProviderId);
			}
			
			set
			{
				if(base.SetSystemInt16(StorePaymentProviderMetadata.ColumnNames.PaymentProviderId, value))
				{
					this._UpToPaymentProviderByPaymentProviderId = null;
					this.OnPropertyChanged("UpToPaymentProviderByPaymentProviderId");
					OnPropertyChanged(StorePaymentProviderMetadata.PropertyNames.PaymentProviderId);
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
					}
				}
				else
				{
					switch (name)
					{	
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(StorePaymentProviderMetadata.PropertyNames.StoreId);
							break;
						
						case "PaymentProviderId":
						
							if (value == null || value is System.Int16)
								this.PaymentProviderId = (System.Int16?)value;
								OnPropertyChanged(StorePaymentProviderMetadata.PropertyNames.PaymentProviderId);
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
			public esStrings(esStorePaymentProvider entity)
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
			

			private esStorePaymentProvider entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return StorePaymentProviderMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public StorePaymentProviderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StorePaymentProviderQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StorePaymentProviderQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(StorePaymentProviderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private StorePaymentProviderQuery query;		
	}



	[Serializable]
	abstract public partial class esStorePaymentProviderCollection : esEntityCollection<StorePaymentProvider>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StorePaymentProviderMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "StorePaymentProviderCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public StorePaymentProviderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StorePaymentProviderQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StorePaymentProviderQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StorePaymentProviderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(StorePaymentProviderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((StorePaymentProviderQuery)query);
		}

		#endregion
		
		private StorePaymentProviderQuery query;
	}



	[Serializable]
	abstract public partial class esStorePaymentProviderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StorePaymentProviderMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StoreId": return this.StoreId;
				case "PaymentProviderId": return this.PaymentProviderId;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, StorePaymentProviderMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem PaymentProviderId
		{
			get { return new esQueryItem(this, StorePaymentProviderMetadata.ColumnNames.PaymentProviderId, esSystemType.Int16); }
		} 
		
		#endregion
		
	}


	
	public partial class StorePaymentProvider : esStorePaymentProvider
	{

				
		#region UpToPaymentProviderByPaymentProviderId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_PaymentProcessor
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
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_Store
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
	public partial class StorePaymentProviderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StorePaymentProviderMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StorePaymentProviderMetadata.ColumnNames.StoreId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StorePaymentProviderMetadata.PropertyNames.StoreId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StorePaymentProviderMetadata.ColumnNames.PaymentProviderId, 1, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = StorePaymentProviderMetadata.PropertyNames.PaymentProviderId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public StorePaymentProviderMetadata Meta()
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
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StoreId = "StoreId";
			 public const string PaymentProviderId = "PaymentProviderId";
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
			lock (typeof(StorePaymentProviderMetadata))
			{
				if(StorePaymentProviderMetadata.mapDelegates == null)
				{
					StorePaymentProviderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StorePaymentProviderMetadata.meta == null)
				{
					StorePaymentProviderMetadata.meta = new StorePaymentProviderMetadata();
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
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_StorePaymentProvider";
					meta.Destination = objectQualifier + "DNNspot_Store_StorePaymentProvider";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_StorePaymentProviderLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_StorePaymentProvider";
					meta.Destination = "DNNspot_Store_StorePaymentProvider";
									
					meta.spInsert = "proc_DNNspot_Store_StorePaymentProviderInsert";				
					meta.spUpdate = "proc_DNNspot_Store_StorePaymentProviderUpdate";		
					meta.spDelete = "proc_DNNspot_Store_StorePaymentProviderDelete";
					meta.spLoadAll = "proc_DNNspot_Store_StorePaymentProviderLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_StorePaymentProviderLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StorePaymentProviderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
