
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
	/// Encapsulates the 'DNNspot_Store_ShippingService' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ShippingService))]	
	[XmlType("ShippingService")]
	[Table(Name="ShippingService")]
	public partial class ShippingService : esShippingService
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ShippingService();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ShippingService();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ShippingService();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int32? Id
		{
			get { return base.Id;  }
			set { base.Id = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int32? StoreId
		{
			get { return base.StoreId;  }
			set { base.StoreId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? ShippingProviderType
		{
			get { return base.ShippingProviderType;  }
			set { base.ShippingProviderType = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ShippingServiceCollection")]
	public partial class ShippingServiceCollection : esShippingServiceCollection, IEnumerable<ShippingService>
	{
		public ShippingService FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ShippingService))]
		public class ShippingServiceCollectionWCFPacket : esCollectionWCFPacket<ShippingServiceCollection>
		{
			public static implicit operator ShippingServiceCollection(ShippingServiceCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ShippingServiceCollectionWCFPacket(ShippingServiceCollection collection)
			{
				return new ShippingServiceCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ShippingServiceQuery : esShippingServiceQuery
	{
		public ShippingServiceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ShippingServiceQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ShippingServiceQuery query)
		{
			return ShippingServiceQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ShippingServiceQuery(string query)
		{
			return (ShippingServiceQuery)ShippingServiceQuery.SerializeHelper.FromXml(query, typeof(ShippingServiceQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esShippingService : esEntity
	{
		public esShippingService()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 id)
		{
			ShippingServiceQuery query = new ShippingServiceQuery();
			query.Where(query.Id == id);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id", id);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingService.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ShippingServiceMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingServiceMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ShippingServiceMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingService.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(ShippingServiceMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingServiceMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(ShippingServiceMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingService.ShippingProviderType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? ShippingProviderType
		{
			get
			{
				return base.GetSystemInt16(ShippingServiceMetadata.ColumnNames.ShippingProviderType);
			}
			
			set
			{
				if(base.SetSystemInt16(ShippingServiceMetadata.ColumnNames.ShippingProviderType, value))
				{
					OnPropertyChanged(ShippingServiceMetadata.PropertyNames.ShippingProviderType);
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
						case "Id": this.str().Id = (string)value; break;							
						case "StoreId": this.str().StoreId = (string)value; break;							
						case "ShippingProviderType": this.str().ShippingProviderType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ShippingServiceMetadata.PropertyNames.Id);
							break;
						
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(ShippingServiceMetadata.PropertyNames.StoreId);
							break;
						
						case "ShippingProviderType":
						
							if (value == null || value is System.Int16)
								this.ShippingProviderType = (System.Int16?)value;
								OnPropertyChanged(ShippingServiceMetadata.PropertyNames.ShippingProviderType);
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
			public esStrings(esShippingService entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int32? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt32(value);
				}
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
				
			public System.String ShippingProviderType
			{
				get
				{
					System.Int16? data = entity.ShippingProviderType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingProviderType = null;
					else entity.ShippingProviderType = Convert.ToInt16(value);
				}
			}
			

			private esShippingService entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ShippingServiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ShippingServiceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ShippingServiceQuery query;		
	}



	[Serializable]
	abstract public partial class esShippingServiceCollection : esEntityCollection<ShippingService>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ShippingServiceCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ShippingServiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ShippingServiceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ShippingServiceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ShippingServiceQuery)query);
		}

		#endregion
		
		private ShippingServiceQuery query;
	}



	[Serializable]
	abstract public partial class esShippingServiceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "StoreId": return this.StoreId;
				case "ShippingProviderType": return this.ShippingProviderType;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ShippingServiceMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, ShippingServiceMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem ShippingProviderType
		{
			get { return new esQueryItem(this, ShippingServiceMetadata.ColumnNames.ShippingProviderType, esSystemType.Int16); }
		} 
		
		#endregion
		
	}


	
	public partial class ShippingService : esShippingService
	{

		#region ShippingServiceRateTypeCollectionByShippingServiceId - Zero To Many
		
		static public esPrefetchMap Prefetch_ShippingServiceRateTypeCollectionByShippingServiceId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.ShippingService.ShippingServiceRateTypeCollectionByShippingServiceId_Delegate;
				map.PropertyName = "ShippingServiceRateTypeCollectionByShippingServiceId";
				map.MyColumnName = "ShippingServiceId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ShippingServiceRateTypeCollectionByShippingServiceId_Delegate(esPrefetchParameters data)
		{
			ShippingServiceQuery parent = new ShippingServiceQuery(data.NextAlias());

			ShippingServiceRateTypeQuery me = data.You != null ? data.You as ShippingServiceRateTypeQuery : new ShippingServiceRateTypeQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ShippingServiceId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ShippingServiceRateType_DNNspot_Store_ShippingService
		/// </summary>

		[XmlIgnore]
		public ShippingServiceRateTypeCollection ShippingServiceRateTypeCollectionByShippingServiceId
		{
			get
			{
				if(this._ShippingServiceRateTypeCollectionByShippingServiceId == null)
				{
					this._ShippingServiceRateTypeCollectionByShippingServiceId = new ShippingServiceRateTypeCollection();
					this._ShippingServiceRateTypeCollectionByShippingServiceId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ShippingServiceRateTypeCollectionByShippingServiceId", this._ShippingServiceRateTypeCollectionByShippingServiceId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ShippingServiceRateTypeCollectionByShippingServiceId.Query.Where(this._ShippingServiceRateTypeCollectionByShippingServiceId.Query.ShippingServiceId == this.Id);
							this._ShippingServiceRateTypeCollectionByShippingServiceId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ShippingServiceRateTypeCollectionByShippingServiceId.fks.Add(ShippingServiceRateTypeMetadata.ColumnNames.ShippingServiceId, this.Id);
					}
				}

				return this._ShippingServiceRateTypeCollectionByShippingServiceId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ShippingServiceRateTypeCollectionByShippingServiceId != null) 
				{ 
					this.RemovePostSave("ShippingServiceRateTypeCollectionByShippingServiceId"); 
					this._ShippingServiceRateTypeCollectionByShippingServiceId = null;
					
				} 
			} 			
		}
			
		
		private ShippingServiceRateTypeCollection _ShippingServiceRateTypeCollectionByShippingServiceId;
		#endregion

		#region ShippingServiceSettingCollectionByShippingServiceId - Zero To Many
		
		static public esPrefetchMap Prefetch_ShippingServiceSettingCollectionByShippingServiceId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.ShippingService.ShippingServiceSettingCollectionByShippingServiceId_Delegate;
				map.PropertyName = "ShippingServiceSettingCollectionByShippingServiceId";
				map.MyColumnName = "ShippingServiceId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ShippingServiceSettingCollectionByShippingServiceId_Delegate(esPrefetchParameters data)
		{
			ShippingServiceQuery parent = new ShippingServiceQuery(data.NextAlias());

			ShippingServiceSettingQuery me = data.You != null ? data.You as ShippingServiceSettingQuery : new ShippingServiceSettingQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ShippingServiceId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ShippingServiceSetting_DNNspot_Store_ShippingService
		/// </summary>

		[XmlIgnore]
		public ShippingServiceSettingCollection ShippingServiceSettingCollectionByShippingServiceId
		{
			get
			{
				if(this._ShippingServiceSettingCollectionByShippingServiceId == null)
				{
					this._ShippingServiceSettingCollectionByShippingServiceId = new ShippingServiceSettingCollection();
					this._ShippingServiceSettingCollectionByShippingServiceId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ShippingServiceSettingCollectionByShippingServiceId", this._ShippingServiceSettingCollectionByShippingServiceId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ShippingServiceSettingCollectionByShippingServiceId.Query.Where(this._ShippingServiceSettingCollectionByShippingServiceId.Query.ShippingServiceId == this.Id);
							this._ShippingServiceSettingCollectionByShippingServiceId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ShippingServiceSettingCollectionByShippingServiceId.fks.Add(ShippingServiceSettingMetadata.ColumnNames.ShippingServiceId, this.Id);
					}
				}

				return this._ShippingServiceSettingCollectionByShippingServiceId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ShippingServiceSettingCollectionByShippingServiceId != null) 
				{ 
					this.RemovePostSave("ShippingServiceSettingCollectionByShippingServiceId"); 
					this._ShippingServiceSettingCollectionByShippingServiceId = null;
					
				} 
			} 			
		}
			
		
		private ShippingServiceSettingCollection _ShippingServiceSettingCollectionByShippingServiceId;
		#endregion

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ShippingService_DNNspot_Store_Store
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
		

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "ShippingServiceRateTypeCollectionByShippingServiceId":
					coll = this.ShippingServiceRateTypeCollectionByShippingServiceId;
					break;
				case "ShippingServiceSettingCollectionByShippingServiceId":
					coll = this.ShippingServiceSettingCollectionByShippingServiceId;
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
			
			props.Add(new esPropertyDescriptor(this, "ShippingServiceRateTypeCollectionByShippingServiceId", typeof(ShippingServiceRateTypeCollection), new ShippingServiceRateType()));
			props.Add(new esPropertyDescriptor(this, "ShippingServiceSettingCollectionByShippingServiceId", typeof(ShippingServiceSettingCollection), new ShippingServiceSetting()));
		
			return props;
		}
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
		
		/// <summary>
		/// Called by ApplyPostSaveKeys 
		/// </summary>
		/// <param name="coll">The collection to enumerate over</param>
		/// <param name="key">"The column name</param>
		/// <param name="value">The column value</param>
		private void Apply(esEntityCollectionBase coll, string key, object value)
		{
			foreach (esEntity obj in coll)
			{
				if (obj.es.IsAdded)
				{
					obj.SetProperty(key, value);
				}
			}
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
			if(this._ShippingServiceRateTypeCollectionByShippingServiceId != null)
			{
				Apply(this._ShippingServiceRateTypeCollectionByShippingServiceId, "ShippingServiceId", this.Id);
			}
			if(this._ShippingServiceSettingCollectionByShippingServiceId != null)
			{
				Apply(this._ShippingServiceSettingCollectionByShippingServiceId, "ShippingServiceId", this.Id);
			}
		}
		
	}
	



	[Serializable]
	public partial class ShippingServiceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ShippingServiceMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ShippingServiceMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingServiceMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceMetadata.ColumnNames.StoreId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingServiceMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceMetadata.ColumnNames.ShippingProviderType, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ShippingServiceMetadata.PropertyNames.ShippingProviderType;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ShippingServiceMetadata Meta()
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
			 public const string StoreId = "StoreId";
			 public const string ShippingProviderType = "ShippingProviderType";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string StoreId = "StoreId";
			 public const string ShippingProviderType = "ShippingProviderType";
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
			lock (typeof(ShippingServiceMetadata))
			{
				if(ShippingServiceMetadata.mapDelegates == null)
				{
					ShippingServiceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ShippingServiceMetadata.meta == null)
				{
					ShippingServiceMetadata.meta = new ShippingServiceMetadata();
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


				meta.AddTypeMap("Id", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StoreId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ShippingProviderType", new esTypeMap("smallint", "System.Int16"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_ShippingService";
					meta.Destination = objectQualifier + "DNNspot_Store_ShippingService";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ShippingServiceInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ShippingServiceUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ShippingServiceDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ShippingServiceLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ShippingServiceLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ShippingService";
					meta.Destination = "DNNspot_Store_ShippingService";
									
					meta.spInsert = "proc_DNNspot_Store_ShippingServiceInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ShippingServiceUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ShippingServiceDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ShippingServiceLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ShippingServiceLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ShippingServiceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
