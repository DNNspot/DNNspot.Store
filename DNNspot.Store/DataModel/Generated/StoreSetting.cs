
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
	/// Encapsulates the 'DNNspot_Store_StoreSetting' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(StoreSetting))]	
	[XmlType("StoreSetting")]
	[Table(Name="StoreSetting")]
	public partial class StoreSetting : esStoreSetting
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new StoreSetting();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 storeId, System.String name)
		{
			var obj = new StoreSetting();
			obj.StoreId = storeId;
			obj.Name = name;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 storeId, System.String name, esSqlAccessType sqlAccessType)
		{
			var obj = new StoreSetting();
			obj.StoreId = storeId;
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
	[XmlType("StoreSettingCollection")]
	public partial class StoreSettingCollection : esStoreSettingCollection, IEnumerable<StoreSetting>
	{
		public StoreSetting FindByPrimaryKey(System.Int32 storeId, System.String name)
		{
			return this.SingleOrDefault(e => e.StoreId == storeId && e.Name == name);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(StoreSetting))]
		public class StoreSettingCollectionWCFPacket : esCollectionWCFPacket<StoreSettingCollection>
		{
			public static implicit operator StoreSettingCollection(StoreSettingCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator StoreSettingCollectionWCFPacket(StoreSettingCollection collection)
			{
				return new StoreSettingCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class StoreSettingQuery : esStoreSettingQuery
	{
		public StoreSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "StoreSettingQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(StoreSettingQuery query)
		{
			return StoreSettingQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator StoreSettingQuery(string query)
		{
			return (StoreSettingQuery)StoreSettingQuery.SerializeHelper.FromXml(query, typeof(StoreSettingQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esStoreSetting : esEntity
	{
		public esStoreSetting()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 storeId, System.String name)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, name);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, name);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 storeId, System.String name)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, name);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, name);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 storeId, System.String name)
		{
			StoreSettingQuery query = new StoreSettingQuery();
			query.Where(query.StoreId == storeId, query.Name == name);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 storeId, System.String name)
		{
			esParameters parms = new esParameters();
			parms.Add("StoreId", storeId);			parms.Add("Name", name);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_StoreSetting.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(StoreSettingMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(StoreSettingMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(StoreSettingMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StoreSetting.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(StoreSettingMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(StoreSettingMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(StoreSettingMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StoreSetting.Value
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Value
		{
			get
			{
				return base.GetSystemString(StoreSettingMetadata.ColumnNames.Value);
			}
			
			set
			{
				if(base.SetSystemString(StoreSettingMetadata.ColumnNames.Value, value))
				{
					OnPropertyChanged(StoreSettingMetadata.PropertyNames.Value);
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
								OnPropertyChanged(StoreSettingMetadata.PropertyNames.StoreId);
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
			public esStrings(esStoreSetting entity)
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
			

			private esStoreSetting entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return StoreSettingMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public StoreSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StoreSettingQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StoreSettingQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(StoreSettingQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private StoreSettingQuery query;		
	}



	[Serializable]
	abstract public partial class esStoreSettingCollection : esEntityCollection<StoreSetting>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StoreSettingMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "StoreSettingCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public StoreSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StoreSettingQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StoreSettingQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StoreSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(StoreSettingQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((StoreSettingQuery)query);
		}

		#endregion
		
		private StoreSettingQuery query;
	}



	[Serializable]
	abstract public partial class esStoreSettingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StoreSettingMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StoreId": return this.StoreId;
				case "Name": return this.Name;
				case "Value": return this.Value;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, StoreSettingMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, StoreSettingMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Value
		{
			get { return new esQueryItem(this, StoreSettingMetadata.ColumnNames.Value, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class StoreSetting : esStoreSetting
	{

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_StoreSetting_DNNspot_Store_Store
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
	public partial class StoreSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StoreSettingMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StoreSettingMetadata.ColumnNames.StoreId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StoreSettingMetadata.PropertyNames.StoreId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreSettingMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = StoreSettingMetadata.PropertyNames.Name;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 300;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreSettingMetadata.ColumnNames.Value, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = StoreSettingMetadata.PropertyNames.Value;
			c.CharacterMaxLength = 2000;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public StoreSettingMetadata Meta()
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
			 public const string Name = "Name";
			 public const string Value = "Value";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StoreId = "StoreId";
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
			lock (typeof(StoreSettingMetadata))
			{
				if(StoreSettingMetadata.mapDelegates == null)
				{
					StoreSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StoreSettingMetadata.meta == null)
				{
					StoreSettingMetadata.meta = new StoreSettingMetadata();
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
					meta.Source = objectQualifier + "DNNspot_Store_StoreSetting";
					meta.Destination = objectQualifier + "DNNspot_Store_StoreSetting";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_StoreSettingInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_StoreSettingUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_StoreSettingDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_StoreSettingLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_StoreSettingLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_StoreSetting";
					meta.Destination = "DNNspot_Store_StoreSetting";
									
					meta.spInsert = "proc_DNNspot_Store_StoreSettingInsert";				
					meta.spUpdate = "proc_DNNspot_Store_StoreSettingUpdate";		
					meta.spDelete = "proc_DNNspot_Store_StoreSettingDelete";
					meta.spLoadAll = "proc_DNNspot_Store_StoreSettingLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_StoreSettingLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StoreSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
