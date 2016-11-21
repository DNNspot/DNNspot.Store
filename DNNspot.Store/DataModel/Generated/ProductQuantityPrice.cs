
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
	/// Encapsulates the 'DNNspot_Store_ProductQuantityPrice' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ProductQuantityPrice))]	
	[XmlType("ProductQuantityPrice")]
	[Table(Name="ProductQuantityPrice")]
	public partial class ProductQuantityPrice : esProductQuantityPrice
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ProductQuantityPrice();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ProductQuantityPrice();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ProductQuantityPrice();
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
		public override System.Int32? Min
		{
			get { return base.Min;  }
			set { base.Min = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int32? Max
		{
			get { return base.Max;  }
			set { base.Max = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? PricePerItem
		{
			get { return base.PricePerItem;  }
			set { base.PricePerItem = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ProductQuantityPriceCollection")]
	public partial class ProductQuantityPriceCollection : esProductQuantityPriceCollection, IEnumerable<ProductQuantityPrice>
	{
		public ProductQuantityPrice FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ProductQuantityPrice))]
		public class ProductQuantityPriceCollectionWCFPacket : esCollectionWCFPacket<ProductQuantityPriceCollection>
		{
			public static implicit operator ProductQuantityPriceCollection(ProductQuantityPriceCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ProductQuantityPriceCollectionWCFPacket(ProductQuantityPriceCollection collection)
			{
				return new ProductQuantityPriceCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ProductQuantityPriceQuery : esProductQuantityPriceQuery
	{
		public ProductQuantityPriceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ProductQuantityPriceQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProductQuantityPriceQuery query)
		{
			return ProductQuantityPriceQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProductQuantityPriceQuery(string query)
		{
			return (ProductQuantityPriceQuery)ProductQuantityPriceQuery.SerializeHelper.FromXml(query, typeof(ProductQuantityPriceQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProductQuantityPrice : esEntity
	{
		public esProductQuantityPrice()
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
			ProductQuantityPriceQuery query = new ProductQuantityPriceQuery();
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
		/// Maps to DNNspot_Store_ProductQuantityPrice.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductQuantityPrice.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductQuantityPrice.Min
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Min
		{
			get
			{
				return base.GetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.Min);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.Min, value))
				{
					OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.Min);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductQuantityPrice.Max
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Max
		{
			get
			{
				return base.GetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.Max);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductQuantityPriceMetadata.ColumnNames.Max, value))
				{
					OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.Max);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductQuantityPrice.PricePerItem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? PricePerItem
		{
			get
			{
				return base.GetSystemDecimal(ProductQuantityPriceMetadata.ColumnNames.PricePerItem);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductQuantityPriceMetadata.ColumnNames.PricePerItem, value))
				{
					OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.PricePerItem);
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
						case "Min": this.str().Min = (string)value; break;							
						case "Max": this.str().Max = (string)value; break;							
						case "PricePerItem": this.str().PricePerItem = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.Id);
							break;
						
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.ProductId);
							break;
						
						case "Min":
						
							if (value == null || value is System.Int32)
								this.Min = (System.Int32?)value;
								OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.Min);
							break;
						
						case "Max":
						
							if (value == null || value is System.Int32)
								this.Max = (System.Int32?)value;
								OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.Max);
							break;
						
						case "PricePerItem":
						
							if (value == null || value is System.Decimal)
								this.PricePerItem = (System.Decimal?)value;
								OnPropertyChanged(ProductQuantityPriceMetadata.PropertyNames.PricePerItem);
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
			public esStrings(esProductQuantityPrice entity)
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
				
			public System.String Min
			{
				get
				{
					System.Int32? data = entity.Min;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Min = null;
					else entity.Min = Convert.ToInt32(value);
				}
			}
				
			public System.String Max
			{
				get
				{
					System.Int32? data = entity.Max;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Max = null;
					else entity.Max = Convert.ToInt32(value);
				}
			}
				
			public System.String PricePerItem
			{
				get
				{
					System.Decimal? data = entity.PricePerItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PricePerItem = null;
					else entity.PricePerItem = Convert.ToDecimal(value);
				}
			}
			

			private esProductQuantityPrice entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProductQuantityPriceMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProductQuantityPriceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductQuantityPriceQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductQuantityPriceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ProductQuantityPriceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ProductQuantityPriceQuery query;		
	}



	[Serializable]
	abstract public partial class esProductQuantityPriceCollection : esEntityCollection<ProductQuantityPrice>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductQuantityPriceMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProductQuantityPriceCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProductQuantityPriceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductQuantityPriceQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductQuantityPriceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductQuantityPriceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProductQuantityPriceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProductQuantityPriceQuery)query);
		}

		#endregion
		
		private ProductQuantityPriceQuery query;
	}



	[Serializable]
	abstract public partial class esProductQuantityPriceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductQuantityPriceMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "ProductId": return this.ProductId;
				case "Min": return this.Min;
				case "Max": return this.Max;
				case "PricePerItem": return this.PricePerItem;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ProductQuantityPriceMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, ProductQuantityPriceMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem Min
		{
			get { return new esQueryItem(this, ProductQuantityPriceMetadata.ColumnNames.Min, esSystemType.Int32); }
		} 
		
		public esQueryItem Max
		{
			get { return new esQueryItem(this, ProductQuantityPriceMetadata.ColumnNames.Max, esSystemType.Int32); }
		} 
		
		public esQueryItem PricePerItem
		{
			get { return new esQueryItem(this, ProductQuantityPriceMetadata.ColumnNames.PricePerItem, esSystemType.Decimal); }
		} 
		
		#endregion
		
	}


	
	public partial class ProductQuantityPrice : esProductQuantityPrice
	{

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ProductQuantityPrice_DNNspot_Store_ProductQuantityPrice
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
	public partial class ProductQuantityPriceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductQuantityPriceMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductQuantityPriceMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductQuantityPriceMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductQuantityPriceMetadata.ColumnNames.ProductId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductQuantityPriceMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductQuantityPriceMetadata.ColumnNames.Min, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductQuantityPriceMetadata.PropertyNames.Min;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductQuantityPriceMetadata.ColumnNames.Max, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductQuantityPriceMetadata.PropertyNames.Max;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductQuantityPriceMetadata.ColumnNames.PricePerItem, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductQuantityPriceMetadata.PropertyNames.PricePerItem;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductQuantityPriceMetadata Meta()
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
			 public const string Min = "Min";
			 public const string Max = "Max";
			 public const string PricePerItem = "PricePerItem";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string ProductId = "ProductId";
			 public const string Min = "Min";
			 public const string Max = "Max";
			 public const string PricePerItem = "PricePerItem";
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
			lock (typeof(ProductQuantityPriceMetadata))
			{
				if(ProductQuantityPriceMetadata.mapDelegates == null)
				{
					ProductQuantityPriceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductQuantityPriceMetadata.meta == null)
				{
					ProductQuantityPriceMetadata.meta = new ProductQuantityPriceMetadata();
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
				meta.AddTypeMap("Min", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Max", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PricePerItem", new esTypeMap("money", "System.Decimal"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_ProductQuantityPrice";
					meta.Destination = objectQualifier + "DNNspot_Store_ProductQuantityPrice";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ProductQuantityPriceInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ProductQuantityPriceUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ProductQuantityPriceDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ProductQuantityPriceLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ProductQuantityPriceLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ProductQuantityPrice";
					meta.Destination = "DNNspot_Store_ProductQuantityPrice";
									
					meta.spInsert = "proc_DNNspot_Store_ProductQuantityPriceInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ProductQuantityPriceUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ProductQuantityPriceDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ProductQuantityPriceLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ProductQuantityPriceLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductQuantityPriceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
