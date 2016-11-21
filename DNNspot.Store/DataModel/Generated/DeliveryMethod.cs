
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
	/// Encapsulates the 'DNNspot_Store_DeliveryMethod' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(DeliveryMethod))]	
	[XmlType("DeliveryMethod")]
	[Table(Name="DeliveryMethod")]
	public partial class DeliveryMethod : esDeliveryMethod
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new DeliveryMethod();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int16 id)
		{
			var obj = new DeliveryMethod();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int16 id, esSqlAccessType sqlAccessType)
		{
			var obj = new DeliveryMethod();
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
	[XmlType("DeliveryMethodCollection")]
	public partial class DeliveryMethodCollection : esDeliveryMethodCollection, IEnumerable<DeliveryMethod>
	{
		public DeliveryMethod FindByPrimaryKey(System.Int16 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(DeliveryMethod))]
		public class DeliveryMethodCollectionWCFPacket : esCollectionWCFPacket<DeliveryMethodCollection>
		{
			public static implicit operator DeliveryMethodCollection(DeliveryMethodCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator DeliveryMethodCollectionWCFPacket(DeliveryMethodCollection collection)
			{
				return new DeliveryMethodCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class DeliveryMethodQuery : esDeliveryMethodQuery
	{
		public DeliveryMethodQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "DeliveryMethodQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(DeliveryMethodQuery query)
		{
			return DeliveryMethodQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator DeliveryMethodQuery(string query)
		{
			return (DeliveryMethodQuery)DeliveryMethodQuery.SerializeHelper.FromXml(query, typeof(DeliveryMethodQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esDeliveryMethod : esEntity
	{
		public esDeliveryMethod()
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
			DeliveryMethodQuery query = new DeliveryMethodQuery();
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
		/// Maps to DNNspot_Store_DeliveryMethod.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? Id
		{
			get
			{
				return base.GetSystemInt16(DeliveryMethodMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt16(DeliveryMethodMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(DeliveryMethodMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_DeliveryMethod.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(DeliveryMethodMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(DeliveryMethodMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(DeliveryMethodMetadata.PropertyNames.Name);
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
								OnPropertyChanged(DeliveryMethodMetadata.PropertyNames.Id);
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
			public esStrings(esDeliveryMethod entity)
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
			

			private esDeliveryMethod entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return DeliveryMethodMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public DeliveryMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DeliveryMethodQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(DeliveryMethodQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(DeliveryMethodQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private DeliveryMethodQuery query;		
	}



	[Serializable]
	abstract public partial class esDeliveryMethodCollection : esEntityCollection<DeliveryMethod>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DeliveryMethodMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "DeliveryMethodCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public DeliveryMethodQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DeliveryMethodQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(DeliveryMethodQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DeliveryMethodQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(DeliveryMethodQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((DeliveryMethodQuery)query);
		}

		#endregion
		
		private DeliveryMethodQuery query;
	}



	[Serializable]
	abstract public partial class esDeliveryMethodQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DeliveryMethodMetadata.Meta();
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
			get { return new esQueryItem(this, DeliveryMethodMetadata.ColumnNames.Id, esSystemType.Int16); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, DeliveryMethodMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class DeliveryMethod : esDeliveryMethod
	{

		#region ProductCollectionByDeliveryMethodId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductCollectionByDeliveryMethodId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.DeliveryMethod.ProductCollectionByDeliveryMethodId_Delegate;
				map.PropertyName = "ProductCollectionByDeliveryMethodId";
				map.MyColumnName = "DeliveryMethodId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductCollectionByDeliveryMethodId_Delegate(esPrefetchParameters data)
		{
			DeliveryMethodQuery parent = new DeliveryMethodQuery(data.NextAlias());

			ProductQuery me = data.You != null ? data.You as ProductQuery : new ProductQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.DeliveryMethodId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Product_DNNspot_Store_DeliveryMethod
		/// </summary>

		[XmlIgnore]
		public ProductCollection ProductCollectionByDeliveryMethodId
		{
			get
			{
				if(this._ProductCollectionByDeliveryMethodId == null)
				{
					this._ProductCollectionByDeliveryMethodId = new ProductCollection();
					this._ProductCollectionByDeliveryMethodId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductCollectionByDeliveryMethodId", this._ProductCollectionByDeliveryMethodId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductCollectionByDeliveryMethodId.Query.Where(this._ProductCollectionByDeliveryMethodId.Query.DeliveryMethodId == this.Id);
							this._ProductCollectionByDeliveryMethodId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductCollectionByDeliveryMethodId.fks.Add(ProductMetadata.ColumnNames.DeliveryMethodId, this.Id);
					}
				}

				return this._ProductCollectionByDeliveryMethodId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductCollectionByDeliveryMethodId != null) 
				{ 
					this.RemovePostSave("ProductCollectionByDeliveryMethodId"); 
					this._ProductCollectionByDeliveryMethodId = null;
					
				} 
			} 			
		}
			
		
		private ProductCollection _ProductCollectionByDeliveryMethodId;
		#endregion

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "ProductCollectionByDeliveryMethodId":
					coll = this.ProductCollectionByDeliveryMethodId;
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
			
			props.Add(new esPropertyDescriptor(this, "ProductCollectionByDeliveryMethodId", typeof(ProductCollection), new Product()));
		
			return props;
		}
		
	}
	



	[Serializable]
	public partial class DeliveryMethodMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DeliveryMethodMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DeliveryMethodMetadata.ColumnNames.Id, 0, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = DeliveryMethodMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DeliveryMethodMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = DeliveryMethodMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public DeliveryMethodMetadata Meta()
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
			lock (typeof(DeliveryMethodMetadata))
			{
				if(DeliveryMethodMetadata.mapDelegates == null)
				{
					DeliveryMethodMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DeliveryMethodMetadata.meta == null)
				{
					DeliveryMethodMetadata.meta = new DeliveryMethodMetadata();
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
				meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_DeliveryMethod";
					meta.Destination = objectQualifier + "DNNspot_Store_DeliveryMethod";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_DeliveryMethodInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_DeliveryMethodUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_DeliveryMethodDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_DeliveryMethodLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_DeliveryMethodLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_DeliveryMethod";
					meta.Destination = "DNNspot_Store_DeliveryMethod";
									
					meta.spInsert = "proc_DNNspot_Store_DeliveryMethodInsert";				
					meta.spUpdate = "proc_DNNspot_Store_DeliveryMethodUpdate";		
					meta.spDelete = "proc_DNNspot_Store_DeliveryMethodDelete";
					meta.spLoadAll = "proc_DNNspot_Store_DeliveryMethodLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_DeliveryMethodLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DeliveryMethodMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
