
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
	/// Encapsulates the 'DNNspot_Store_TaxRegion' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(TaxRegion))]	
	[XmlType("TaxRegion")]
	[Table(Name="TaxRegion")]
	public partial class TaxRegion : esTaxRegion
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new TaxRegion();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 storeId, System.String countryCode, System.String region)
		{
			var obj = new TaxRegion();
			obj.StoreId = storeId;
			obj.CountryCode = countryCode;
			obj.Region = region;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 storeId, System.String countryCode, System.String region, esSqlAccessType sqlAccessType)
		{
			var obj = new TaxRegion();
			obj.StoreId = storeId;
			obj.CountryCode = countryCode;
			obj.Region = region;
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
		public override System.String CountryCode
		{
			get { return base.CountryCode;  }
			set { base.CountryCode = value; }
		}

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.String Region
		{
			get { return base.Region;  }
			set { base.Region = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? TaxRate
		{
			get { return base.TaxRate;  }
			set { base.TaxRate = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("TaxRegionCollection")]
	public partial class TaxRegionCollection : esTaxRegionCollection, IEnumerable<TaxRegion>
	{
		public TaxRegion FindByPrimaryKey(System.Int32 storeId, System.String countryCode, System.String region)
		{
			return this.SingleOrDefault(e => e.StoreId == storeId && e.CountryCode == countryCode && e.Region == region);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(TaxRegion))]
		public class TaxRegionCollectionWCFPacket : esCollectionWCFPacket<TaxRegionCollection>
		{
			public static implicit operator TaxRegionCollection(TaxRegionCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator TaxRegionCollectionWCFPacket(TaxRegionCollection collection)
			{
				return new TaxRegionCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class TaxRegionQuery : esTaxRegionQuery
	{
		public TaxRegionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "TaxRegionQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(TaxRegionQuery query)
		{
			return TaxRegionQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator TaxRegionQuery(string query)
		{
			return (TaxRegionQuery)TaxRegionQuery.SerializeHelper.FromXml(query, typeof(TaxRegionQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esTaxRegion : esEntity
	{
		public esTaxRegion()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 storeId, System.String countryCode, System.String region)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, countryCode, region);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, countryCode, region);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 storeId, System.String countryCode, System.String region)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, countryCode, region);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, countryCode, region);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 storeId, System.String countryCode, System.String region)
		{
			TaxRegionQuery query = new TaxRegionQuery();
			query.Where(query.StoreId == storeId, query.CountryCode == countryCode, query.Region == region);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 storeId, System.String countryCode, System.String region)
		{
			esParameters parms = new esParameters();
			parms.Add("StoreId", storeId);			parms.Add("CountryCode", countryCode);			parms.Add("Region", region);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_TaxRegion.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(TaxRegionMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(TaxRegionMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(TaxRegionMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_TaxRegion.CountryCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CountryCode
		{
			get
			{
				return base.GetSystemString(TaxRegionMetadata.ColumnNames.CountryCode);
			}
			
			set
			{
				if(base.SetSystemString(TaxRegionMetadata.ColumnNames.CountryCode, value))
				{
					OnPropertyChanged(TaxRegionMetadata.PropertyNames.CountryCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_TaxRegion.Region
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Region
		{
			get
			{
				return base.GetSystemString(TaxRegionMetadata.ColumnNames.Region);
			}
			
			set
			{
				if(base.SetSystemString(TaxRegionMetadata.ColumnNames.Region, value))
				{
					OnPropertyChanged(TaxRegionMetadata.PropertyNames.Region);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_TaxRegion.TaxRate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? TaxRate
		{
			get
			{
				return base.GetSystemDecimal(TaxRegionMetadata.ColumnNames.TaxRate);
			}
			
			set
			{
				if(base.SetSystemDecimal(TaxRegionMetadata.ColumnNames.TaxRate, value))
				{
					OnPropertyChanged(TaxRegionMetadata.PropertyNames.TaxRate);
				}
			}
		}		
		
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
						case "CountryCode": this.str().CountryCode = (string)value; break;							
						case "Region": this.str().Region = (string)value; break;							
						case "TaxRate": this.str().TaxRate = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(TaxRegionMetadata.PropertyNames.StoreId);
							break;
						
						case "TaxRate":
						
							if (value == null || value is System.Decimal)
								this.TaxRate = (System.Decimal?)value;
								OnPropertyChanged(TaxRegionMetadata.PropertyNames.TaxRate);
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
			public esStrings(esTaxRegion entity)
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
				
			public System.String CountryCode
			{
				get
				{
					System.String data = entity.CountryCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CountryCode = null;
					else entity.CountryCode = Convert.ToString(value);
				}
			}
				
			public System.String Region
			{
				get
				{
					System.String data = entity.Region;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Region = null;
					else entity.Region = Convert.ToString(value);
				}
			}
				
			public System.String TaxRate
			{
				get
				{
					System.Decimal? data = entity.TaxRate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxRate = null;
					else entity.TaxRate = Convert.ToDecimal(value);
				}
			}
			

			private esTaxRegion entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return TaxRegionMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public TaxRegionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TaxRegionQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(TaxRegionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(TaxRegionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private TaxRegionQuery query;		
	}



	[Serializable]
	abstract public partial class esTaxRegionCollection : esEntityCollection<TaxRegion>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return TaxRegionMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "TaxRegionCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public TaxRegionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new TaxRegionQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(TaxRegionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new TaxRegionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(TaxRegionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((TaxRegionQuery)query);
		}

		#endregion
		
		private TaxRegionQuery query;
	}



	[Serializable]
	abstract public partial class esTaxRegionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return TaxRegionMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StoreId": return this.StoreId;
				case "CountryCode": return this.CountryCode;
				case "Region": return this.Region;
				case "TaxRate": return this.TaxRate;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, TaxRegionMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem CountryCode
		{
			get { return new esQueryItem(this, TaxRegionMetadata.ColumnNames.CountryCode, esSystemType.String); }
		} 
		
		public esQueryItem Region
		{
			get { return new esQueryItem(this, TaxRegionMetadata.ColumnNames.Region, esSystemType.String); }
		} 
		
		public esQueryItem TaxRate
		{
			get { return new esQueryItem(this, TaxRegionMetadata.ColumnNames.TaxRate, esSystemType.Decimal); }
		} 
		
		#endregion
		
	}


	
	public partial class TaxRegion : esTaxRegion
	{

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_RegionTax_DNNspot_Store_Store
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
	public partial class TaxRegionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected TaxRegionMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(TaxRegionMetadata.ColumnNames.StoreId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = TaxRegionMetadata.PropertyNames.StoreId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TaxRegionMetadata.ColumnNames.CountryCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = TaxRegionMetadata.PropertyNames.CountryCode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 2;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TaxRegionMetadata.ColumnNames.Region, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = TaxRegionMetadata.PropertyNames.Region;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
			c = new esColumnMetadata(TaxRegionMetadata.ColumnNames.TaxRate, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = TaxRegionMetadata.PropertyNames.TaxRate;
			c.NumericPrecision = 10;
			c.NumericScale = 6;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public TaxRegionMetadata Meta()
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
			 public const string CountryCode = "CountryCode";
			 public const string Region = "Region";
			 public const string TaxRate = "TaxRate";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StoreId = "StoreId";
			 public const string CountryCode = "CountryCode";
			 public const string Region = "Region";
			 public const string TaxRate = "TaxRate";
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
			lock (typeof(TaxRegionMetadata))
			{
				if(TaxRegionMetadata.mapDelegates == null)
				{
					TaxRegionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (TaxRegionMetadata.meta == null)
				{
					TaxRegionMetadata.meta = new TaxRegionMetadata();
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
				meta.AddTypeMap("CountryCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Region", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("TaxRate", new esTypeMap("decimal", "System.Decimal"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_TaxRegion";
					meta.Destination = objectQualifier + "DNNspot_Store_TaxRegion";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_TaxRegionInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_TaxRegionUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_TaxRegionDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_TaxRegionLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_TaxRegionLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_TaxRegion";
					meta.Destination = "DNNspot_Store_TaxRegion";
									
					meta.spInsert = "proc_DNNspot_Store_TaxRegionInsert";				
					meta.spUpdate = "proc_DNNspot_Store_TaxRegionUpdate";		
					meta.spDelete = "proc_DNNspot_Store_TaxRegionDelete";
					meta.spLoadAll = "proc_DNNspot_Store_TaxRegionLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_TaxRegionLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private TaxRegionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
