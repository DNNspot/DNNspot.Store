
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
	/// Encapsulates the 'DNNspot_Store_ProductCategory' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ProductCategory))]	
	[XmlType("ProductCategory")]
	[Table(Name="ProductCategory")]
	public partial class ProductCategory : esProductCategory
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ProductCategory();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 productId, System.Int32 categoryId)
		{
			var obj = new ProductCategory();
			obj.ProductId = productId;
			obj.CategoryId = categoryId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 productId, System.Int32 categoryId, esSqlAccessType sqlAccessType)
		{
			var obj = new ProductCategory();
			obj.ProductId = productId;
			obj.CategoryId = categoryId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int32? ProductId
		{
			get { return base.ProductId;  }
			set { base.ProductId = value; }
		}

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int32? CategoryId
		{
			get { return base.CategoryId;  }
			set { base.CategoryId = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ProductCategoryCollection")]
	public partial class ProductCategoryCollection : esProductCategoryCollection, IEnumerable<ProductCategory>
	{
		public ProductCategory FindByPrimaryKey(System.Int32 productId, System.Int32 categoryId)
		{
			return this.SingleOrDefault(e => e.ProductId == productId && e.CategoryId == categoryId);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ProductCategory))]
		public class ProductCategoryCollectionWCFPacket : esCollectionWCFPacket<ProductCategoryCollection>
		{
			public static implicit operator ProductCategoryCollection(ProductCategoryCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ProductCategoryCollectionWCFPacket(ProductCategoryCollection collection)
			{
				return new ProductCategoryCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ProductCategoryQuery : esProductCategoryQuery
	{
		public ProductCategoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ProductCategoryQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProductCategoryQuery query)
		{
			return ProductCategoryQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProductCategoryQuery(string query)
		{
			return (ProductCategoryQuery)ProductCategoryQuery.SerializeHelper.FromXml(query, typeof(ProductCategoryQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProductCategory : esEntity
	{
		public esProductCategory()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 productId, System.Int32 categoryId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productId, categoryId);
			else
				return LoadByPrimaryKeyStoredProcedure(productId, categoryId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 productId, System.Int32 categoryId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productId, categoryId);
			else
				return LoadByPrimaryKeyStoredProcedure(productId, categoryId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 productId, System.Int32 categoryId)
		{
			ProductCategoryQuery query = new ProductCategoryQuery();
			query.Where(query.ProductId == productId, query.CategoryId == categoryId);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 productId, System.Int32 categoryId)
		{
			esParameters parms = new esParameters();
			parms.Add("ProductId", productId);			parms.Add("CategoryId", categoryId);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductCategory.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(ProductCategoryMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductCategoryMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(ProductCategoryMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductCategory.CategoryId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? CategoryId
		{
			get
			{
				return base.GetSystemInt32(ProductCategoryMetadata.ColumnNames.CategoryId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductCategoryMetadata.ColumnNames.CategoryId, value))
				{
					this._UpToCategoryByCategoryId = null;
					this.OnPropertyChanged("UpToCategoryByCategoryId");
					OnPropertyChanged(ProductCategoryMetadata.PropertyNames.CategoryId);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Category _UpToCategoryByCategoryId;
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
						case "ProductId": this.str().ProductId = (string)value; break;							
						case "CategoryId": this.str().CategoryId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(ProductCategoryMetadata.PropertyNames.ProductId);
							break;
						
						case "CategoryId":
						
							if (value == null || value is System.Int32)
								this.CategoryId = (System.Int32?)value;
								OnPropertyChanged(ProductCategoryMetadata.PropertyNames.CategoryId);
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
			public esStrings(esProductCategory entity)
			{
				this.entity = entity;
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
				
			public System.String CategoryId
			{
				get
				{
					System.Int32? data = entity.CategoryId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CategoryId = null;
					else entity.CategoryId = Convert.ToInt32(value);
				}
			}
			

			private esProductCategory entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProductCategoryMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProductCategoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductCategoryQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductCategoryQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ProductCategoryQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ProductCategoryQuery query;		
	}



	[Serializable]
	abstract public partial class esProductCategoryCollection : esEntityCollection<ProductCategory>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductCategoryMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProductCategoryCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProductCategoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductCategoryQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductCategoryQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductCategoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProductCategoryQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProductCategoryQuery)query);
		}

		#endregion
		
		private ProductCategoryQuery query;
	}



	[Serializable]
	abstract public partial class esProductCategoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductCategoryMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ProductId": return this.ProductId;
				case "CategoryId": return this.CategoryId;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, ProductCategoryMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem CategoryId
		{
			get { return new esQueryItem(this, ProductCategoryMetadata.ColumnNames.CategoryId, esSystemType.Int32); }
		} 
		
		#endregion
		
	}


	
	public partial class ProductCategory : esProductCategory
	{

				
		#region UpToCategoryByCategoryId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Category
		/// </summary>

		[XmlIgnore]
					
		public Category UpToCategoryByCategoryId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToCategoryByCategoryId == null && CategoryId != null)
				{
					this._UpToCategoryByCategoryId = new Category();
					this._UpToCategoryByCategoryId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToCategoryByCategoryId", this._UpToCategoryByCategoryId);
					this._UpToCategoryByCategoryId.Query.Where(this._UpToCategoryByCategoryId.Query.Id == this.CategoryId);
					this._UpToCategoryByCategoryId.Query.Load();
				}	
				return this._UpToCategoryByCategoryId;
			}
			
			set
			{
				this.RemovePreSave("UpToCategoryByCategoryId");
				

				if(value == null)
				{
					this.CategoryId = null;
					this._UpToCategoryByCategoryId = null;
				}
				else
				{
					this.CategoryId = value.Id;
					this._UpToCategoryByCategoryId = value;
					this.SetPreSave("UpToCategoryByCategoryId", this._UpToCategoryByCategoryId);
				}
				
			}
		}
		#endregion
		

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Product
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
			if(!this.es.IsDeleted && this._UpToCategoryByCategoryId != null)
			{
				this.CategoryId = this._UpToCategoryByCategoryId.Id;
			}
			if(!this.es.IsDeleted && this._UpToProductByProductId != null)
			{
				this.ProductId = this._UpToProductByProductId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class ProductCategoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductCategoryMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductCategoryMetadata.ColumnNames.ProductId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductCategoryMetadata.PropertyNames.ProductId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductCategoryMetadata.ColumnNames.CategoryId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductCategoryMetadata.PropertyNames.CategoryId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductCategoryMetadata Meta()
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
			 public const string ProductId = "ProductId";
			 public const string CategoryId = "CategoryId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ProductId = "ProductId";
			 public const string CategoryId = "CategoryId";
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
			lock (typeof(ProductCategoryMetadata))
			{
				if(ProductCategoryMetadata.mapDelegates == null)
				{
					ProductCategoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductCategoryMetadata.meta == null)
				{
					ProductCategoryMetadata.meta = new ProductCategoryMetadata();
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


				meta.AddTypeMap("ProductId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CategoryId", new esTypeMap("int", "System.Int32"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_ProductCategory";
					meta.Destination = objectQualifier + "DNNspot_Store_ProductCategory";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ProductCategoryInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ProductCategoryUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ProductCategoryDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ProductCategoryLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ProductCategoryLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ProductCategory";
					meta.Destination = "DNNspot_Store_ProductCategory";
									
					meta.spInsert = "proc_DNNspot_Store_ProductCategoryInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ProductCategoryUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ProductCategoryDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ProductCategoryLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ProductCategoryLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductCategoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
