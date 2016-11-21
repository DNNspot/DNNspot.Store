
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
	/// Encapsulates the 'DNNspot_Store_ShippingServiceSetting' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ShippingServiceSetting))]	
	[XmlType("ShippingServiceSetting")]
	[Table(Name="ShippingServiceSetting")]
	public partial class ShippingServiceSetting : esShippingServiceSetting
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ShippingServiceSetting();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 shippingServiceId, System.String name)
		{
			var obj = new ShippingServiceSetting();
			obj.ShippingServiceId = shippingServiceId;
			obj.Name = name;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 shippingServiceId, System.String name, esSqlAccessType sqlAccessType)
		{
			var obj = new ShippingServiceSetting();
			obj.ShippingServiceId = shippingServiceId;
			obj.Name = name;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int32? ShippingServiceId
		{
			get { return base.ShippingServiceId;  }
			set { base.ShippingServiceId = value; }
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
	[XmlType("ShippingServiceSettingCollection")]
	public partial class ShippingServiceSettingCollection : esShippingServiceSettingCollection, IEnumerable<ShippingServiceSetting>
	{
		public ShippingServiceSetting FindByPrimaryKey(System.Int32 shippingServiceId, System.String name)
		{
			return this.SingleOrDefault(e => e.ShippingServiceId == shippingServiceId && e.Name == name);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ShippingServiceSetting))]
		public class ShippingServiceSettingCollectionWCFPacket : esCollectionWCFPacket<ShippingServiceSettingCollection>
		{
			public static implicit operator ShippingServiceSettingCollection(ShippingServiceSettingCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ShippingServiceSettingCollectionWCFPacket(ShippingServiceSettingCollection collection)
			{
				return new ShippingServiceSettingCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ShippingServiceSettingQuery : esShippingServiceSettingQuery
	{
		public ShippingServiceSettingQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ShippingServiceSettingQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ShippingServiceSettingQuery query)
		{
			return ShippingServiceSettingQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ShippingServiceSettingQuery(string query)
		{
			return (ShippingServiceSettingQuery)ShippingServiceSettingQuery.SerializeHelper.FromXml(query, typeof(ShippingServiceSettingQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esShippingServiceSetting : esEntity
	{
		public esShippingServiceSetting()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 shippingServiceId, System.String name)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(shippingServiceId, name);
			else
				return LoadByPrimaryKeyStoredProcedure(shippingServiceId, name);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 shippingServiceId, System.String name)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(shippingServiceId, name);
			else
				return LoadByPrimaryKeyStoredProcedure(shippingServiceId, name);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 shippingServiceId, System.String name)
		{
			ShippingServiceSettingQuery query = new ShippingServiceSettingQuery();
			query.Where(query.ShippingServiceId == shippingServiceId, query.Name == name);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 shippingServiceId, System.String name)
		{
			esParameters parms = new esParameters();
			parms.Add("ShippingServiceId", shippingServiceId);			parms.Add("Name", name);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceSetting.ShippingServiceId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ShippingServiceId
		{
			get
			{
				return base.GetSystemInt32(ShippingServiceSettingMetadata.ColumnNames.ShippingServiceId);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingServiceSettingMetadata.ColumnNames.ShippingServiceId, value))
				{
					this._UpToShippingServiceByShippingServiceId = null;
					this.OnPropertyChanged("UpToShippingServiceByShippingServiceId");
					OnPropertyChanged(ShippingServiceSettingMetadata.PropertyNames.ShippingServiceId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceSetting.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ShippingServiceSettingMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ShippingServiceSettingMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ShippingServiceSettingMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceSetting.Value
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Value
		{
			get
			{
				return base.GetSystemString(ShippingServiceSettingMetadata.ColumnNames.Value);
			}
			
			set
			{
				if(base.SetSystemString(ShippingServiceSettingMetadata.ColumnNames.Value, value))
				{
					OnPropertyChanged(ShippingServiceSettingMetadata.PropertyNames.Value);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected ShippingService _UpToShippingServiceByShippingServiceId;
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
						case "ShippingServiceId": this.str().ShippingServiceId = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "Value": this.str().Value = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ShippingServiceId":
						
							if (value == null || value is System.Int32)
								this.ShippingServiceId = (System.Int32?)value;
								OnPropertyChanged(ShippingServiceSettingMetadata.PropertyNames.ShippingServiceId);
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
			public esStrings(esShippingServiceSetting entity)
			{
				this.entity = entity;
			}
			
	
			public System.String ShippingServiceId
			{
				get
				{
					System.Int32? data = entity.ShippingServiceId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServiceId = null;
					else entity.ShippingServiceId = Convert.ToInt32(value);
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
			

			private esShippingServiceSetting entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceSettingMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ShippingServiceSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceSettingQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceSettingQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ShippingServiceSettingQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ShippingServiceSettingQuery query;		
	}



	[Serializable]
	abstract public partial class esShippingServiceSettingCollection : esEntityCollection<ShippingServiceSetting>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceSettingMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ShippingServiceSettingCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ShippingServiceSettingQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceSettingQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceSettingQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ShippingServiceSettingQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ShippingServiceSettingQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ShippingServiceSettingQuery)query);
		}

		#endregion
		
		private ShippingServiceSettingQuery query;
	}



	[Serializable]
	abstract public partial class esShippingServiceSettingQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceSettingMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ShippingServiceId": return this.ShippingServiceId;
				case "Name": return this.Name;
				case "Value": return this.Value;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ShippingServiceId
		{
			get { return new esQueryItem(this, ShippingServiceSettingMetadata.ColumnNames.ShippingServiceId, esSystemType.Int32); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ShippingServiceSettingMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Value
		{
			get { return new esQueryItem(this, ShippingServiceSettingMetadata.ColumnNames.Value, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class ShippingServiceSetting : esShippingServiceSetting
	{

				
		#region UpToShippingServiceByShippingServiceId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ShippingServiceSetting_DNNspot_Store_ShippingService
		/// </summary>

		[XmlIgnore]
					
		public ShippingService UpToShippingServiceByShippingServiceId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToShippingServiceByShippingServiceId == null && ShippingServiceId != null)
				{
					this._UpToShippingServiceByShippingServiceId = new ShippingService();
					this._UpToShippingServiceByShippingServiceId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToShippingServiceByShippingServiceId", this._UpToShippingServiceByShippingServiceId);
					this._UpToShippingServiceByShippingServiceId.Query.Where(this._UpToShippingServiceByShippingServiceId.Query.Id == this.ShippingServiceId);
					this._UpToShippingServiceByShippingServiceId.Query.Load();
				}	
				return this._UpToShippingServiceByShippingServiceId;
			}
			
			set
			{
				this.RemovePreSave("UpToShippingServiceByShippingServiceId");
				

				if(value == null)
				{
					this.ShippingServiceId = null;
					this._UpToShippingServiceByShippingServiceId = null;
				}
				else
				{
					this.ShippingServiceId = value.Id;
					this._UpToShippingServiceByShippingServiceId = value;
					this.SetPreSave("UpToShippingServiceByShippingServiceId", this._UpToShippingServiceByShippingServiceId);
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
			if(!this.es.IsDeleted && this._UpToShippingServiceByShippingServiceId != null)
			{
				this.ShippingServiceId = this._UpToShippingServiceByShippingServiceId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class ShippingServiceSettingMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ShippingServiceSettingMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ShippingServiceSettingMetadata.ColumnNames.ShippingServiceId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingServiceSettingMetadata.PropertyNames.ShippingServiceId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceSettingMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingServiceSettingMetadata.PropertyNames.Name;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 300;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceSettingMetadata.ColumnNames.Value, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingServiceSettingMetadata.PropertyNames.Value;
			c.CharacterMaxLength = 1000;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ShippingServiceSettingMetadata Meta()
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
			 public const string ShippingServiceId = "ShippingServiceId";
			 public const string Name = "Name";
			 public const string Value = "Value";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ShippingServiceId = "ShippingServiceId";
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
			lock (typeof(ShippingServiceSettingMetadata))
			{
				if(ShippingServiceSettingMetadata.mapDelegates == null)
				{
					ShippingServiceSettingMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ShippingServiceSettingMetadata.meta == null)
				{
					ShippingServiceSettingMetadata.meta = new ShippingServiceSettingMetadata();
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


				meta.AddTypeMap("ShippingServiceId", new esTypeMap("int", "System.Int32"));
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
					meta.Source = objectQualifier + "DNNspot_Store_ShippingServiceSetting";
					meta.Destination = objectQualifier + "DNNspot_Store_ShippingServiceSetting";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ShippingServiceSettingInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ShippingServiceSettingUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ShippingServiceSettingDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ShippingServiceSettingLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ShippingServiceSettingLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ShippingServiceSetting";
					meta.Destination = "DNNspot_Store_ShippingServiceSetting";
									
					meta.spInsert = "proc_DNNspot_Store_ShippingServiceSettingInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ShippingServiceSettingUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ShippingServiceSettingDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ShippingServiceSettingLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ShippingServiceSettingLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ShippingServiceSettingMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
