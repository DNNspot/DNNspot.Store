
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
	/// Encapsulates the 'DNNspot_Store_ProductPhoto' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ProductPhoto))]	
	[XmlType("ProductPhoto")]
	[Table(Name="ProductPhoto")]
	public partial class ProductPhoto : esProductPhoto
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ProductPhoto();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ProductPhoto();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ProductPhoto();
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
		public override System.Int32? ProductId
		{
			get { return base.ProductId;  }
			set { base.ProductId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DisplayName
		{
			get { return base.DisplayName;  }
			set { base.DisplayName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Filename
		{
			get { return base.Filename;  }
			set { base.Filename = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? SortOrder
		{
			get { return base.SortOrder;  }
			set { base.SortOrder = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ProductPhotoCollection")]
	public partial class ProductPhotoCollection : esProductPhotoCollection, IEnumerable<ProductPhoto>
	{
		public ProductPhoto FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ProductPhoto))]
		public class ProductPhotoCollectionWCFPacket : esCollectionWCFPacket<ProductPhotoCollection>
		{
			public static implicit operator ProductPhotoCollection(ProductPhotoCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ProductPhotoCollectionWCFPacket(ProductPhotoCollection collection)
			{
				return new ProductPhotoCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ProductPhotoQuery : esProductPhotoQuery
	{
		public ProductPhotoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ProductPhotoQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProductPhotoQuery query)
		{
			return ProductPhotoQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProductPhotoQuery(string query)
		{
			return (ProductPhotoQuery)ProductPhotoQuery.SerializeHelper.FromXml(query, typeof(ProductPhotoQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProductPhoto : esEntity
	{
		public esProductPhoto()
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
			ProductPhotoQuery query = new ProductPhotoQuery();
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
		/// Maps to DNNspot_Store_ProductPhoto.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ProductPhotoMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductPhotoMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ProductPhotoMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductPhoto.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(ProductPhotoMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductPhotoMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(ProductPhotoMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductPhoto.DisplayName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DisplayName
		{
			get
			{
				return base.GetSystemString(ProductPhotoMetadata.ColumnNames.DisplayName);
			}
			
			set
			{
				if(base.SetSystemString(ProductPhotoMetadata.ColumnNames.DisplayName, value))
				{
					OnPropertyChanged(ProductPhotoMetadata.PropertyNames.DisplayName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductPhoto.Filename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Filename
		{
			get
			{
				return base.GetSystemString(ProductPhotoMetadata.ColumnNames.Filename);
			}
			
			set
			{
				if(base.SetSystemString(ProductPhotoMetadata.ColumnNames.Filename, value))
				{
					OnPropertyChanged(ProductPhotoMetadata.PropertyNames.Filename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductPhoto.SortOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? SortOrder
		{
			get
			{
				return base.GetSystemInt16(ProductPhotoMetadata.ColumnNames.SortOrder);
			}
			
			set
			{
				if(base.SetSystemInt16(ProductPhotoMetadata.ColumnNames.SortOrder, value))
				{
					OnPropertyChanged(ProductPhotoMetadata.PropertyNames.SortOrder);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Product _UpToProductByProductId;
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
						case "ProductId": this.str().ProductId = (string)value; break;							
						case "DisplayName": this.str().DisplayName = (string)value; break;							
						case "Filename": this.str().Filename = (string)value; break;							
						case "SortOrder": this.str().SortOrder = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ProductPhotoMetadata.PropertyNames.Id);
							break;
						
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(ProductPhotoMetadata.PropertyNames.ProductId);
							break;
						
						case "SortOrder":
						
							if (value == null || value is System.Int16)
								this.SortOrder = (System.Int16?)value;
								OnPropertyChanged(ProductPhotoMetadata.PropertyNames.SortOrder);
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
			public esStrings(esProductPhoto entity)
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
				
			public System.String ProductId
			{
				get
				{
					System.Int32? data = entity.ProductId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductId = null;
					else entity.ProductId = Convert.ToInt32(value);
				}
			}
				
			public System.String DisplayName
			{
				get
				{
					System.String data = entity.DisplayName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisplayName = null;
					else entity.DisplayName = Convert.ToString(value);
				}
			}
				
			public System.String Filename
			{
				get
				{
					System.String data = entity.Filename;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Filename = null;
					else entity.Filename = Convert.ToString(value);
				}
			}
				
			public System.String SortOrder
			{
				get
				{
					System.Int16? data = entity.SortOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SortOrder = null;
					else entity.SortOrder = Convert.ToInt16(value);
				}
			}
			

			private esProductPhoto entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProductPhotoMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProductPhotoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductPhotoQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductPhotoQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ProductPhotoQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ProductPhotoQuery query;		
	}



	[Serializable]
	abstract public partial class esProductPhotoCollection : esEntityCollection<ProductPhoto>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductPhotoMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProductPhotoCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProductPhotoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductPhotoQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductPhotoQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductPhotoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProductPhotoQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProductPhotoQuery)query);
		}

		#endregion
		
		private ProductPhotoQuery query;
	}



	[Serializable]
	abstract public partial class esProductPhotoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductPhotoMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "ProductId": return this.ProductId;
				case "DisplayName": return this.DisplayName;
				case "Filename": return this.Filename;
				case "SortOrder": return this.SortOrder;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ProductPhotoMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, ProductPhotoMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem DisplayName
		{
			get { return new esQueryItem(this, ProductPhotoMetadata.ColumnNames.DisplayName, esSystemType.String); }
		} 
		
		public esQueryItem Filename
		{
			get { return new esQueryItem(this, ProductPhotoMetadata.ColumnNames.Filename, esSystemType.String); }
		} 
		
		public esQueryItem SortOrder
		{
			get { return new esQueryItem(this, ProductPhotoMetadata.ColumnNames.SortOrder, esSystemType.Int16); }
		} 
		
		#endregion
		
	}


	
	public partial class ProductPhoto : esProductPhoto
	{

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ProductPhoto_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
					
		public Product UpToProductByProductId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToProductByProductId == null && ProductId != null)
				{
					this._UpToProductByProductId = new Product();
					this._UpToProductByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToProductByProductId", this._UpToProductByProductId);
					this._UpToProductByProductId.Query.Where(this._UpToProductByProductId.Query.Id == this.ProductId);
					this._UpToProductByProductId.Query.Load();
				}	
				return this._UpToProductByProductId;
			}
			
			set
			{
				this.RemovePreSave("UpToProductByProductId");
				

				if(value == null)
				{
					this.ProductId = null;
					this._UpToProductByProductId = null;
				}
				else
				{
					this.ProductId = value.Id;
					this._UpToProductByProductId = value;
					this.SetPreSave("UpToProductByProductId", this._UpToProductByProductId);
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
			if(!this.es.IsDeleted && this._UpToProductByProductId != null)
			{
				this.ProductId = this._UpToProductByProductId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class ProductPhotoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductPhotoMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductPhotoMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductPhotoMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductPhotoMetadata.ColumnNames.ProductId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductPhotoMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductPhotoMetadata.ColumnNames.DisplayName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductPhotoMetadata.PropertyNames.DisplayName;
			c.CharacterMaxLength = 250;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductPhotoMetadata.ColumnNames.Filename, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductPhotoMetadata.PropertyNames.Filename;
			c.CharacterMaxLength = 500;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductPhotoMetadata.ColumnNames.SortOrder, 4, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ProductPhotoMetadata.PropertyNames.SortOrder;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductPhotoMetadata Meta()
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
			 public const string ProductId = "ProductId";
			 public const string DisplayName = "DisplayName";
			 public const string Filename = "Filename";
			 public const string SortOrder = "SortOrder";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string ProductId = "ProductId";
			 public const string DisplayName = "DisplayName";
			 public const string Filename = "Filename";
			 public const string SortOrder = "SortOrder";
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
			lock (typeof(ProductPhotoMetadata))
			{
				if(ProductPhotoMetadata.mapDelegates == null)
				{
					ProductPhotoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductPhotoMetadata.meta == null)
				{
					ProductPhotoMetadata.meta = new ProductPhotoMetadata();
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
				meta.AddTypeMap("ProductId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DisplayName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Filename", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SortOrder", new esTypeMap("smallint", "System.Int16"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_ProductPhoto";
					meta.Destination = objectQualifier + "DNNspot_Store_ProductPhoto";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ProductPhotoInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ProductPhotoUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ProductPhotoDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ProductPhotoLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ProductPhotoLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ProductPhoto";
					meta.Destination = "DNNspot_Store_ProductPhoto";
									
					meta.spInsert = "proc_DNNspot_Store_ProductPhotoInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ProductPhotoUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ProductPhotoDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ProductPhotoLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ProductPhotoLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductPhotoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
