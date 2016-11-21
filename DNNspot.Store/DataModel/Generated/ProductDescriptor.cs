
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
	/// Encapsulates the 'DNNspot_Store_ProductDescriptor' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ProductDescriptor))]	
	[XmlType("ProductDescriptor")]
	[Table(Name="ProductDescriptor")]
	public partial class ProductDescriptor : esProductDescriptor
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ProductDescriptor();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ProductDescriptor();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ProductDescriptor();
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
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Text
		{
			get { return base.Text;  }
			set { base.Text = value; }
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
	[XmlType("ProductDescriptorCollection")]
	public partial class ProductDescriptorCollection : esProductDescriptorCollection, IEnumerable<ProductDescriptor>
	{
		public ProductDescriptor FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ProductDescriptor))]
		public class ProductDescriptorCollectionWCFPacket : esCollectionWCFPacket<ProductDescriptorCollection>
		{
			public static implicit operator ProductDescriptorCollection(ProductDescriptorCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ProductDescriptorCollectionWCFPacket(ProductDescriptorCollection collection)
			{
				return new ProductDescriptorCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ProductDescriptorQuery : esProductDescriptorQuery
	{
		public ProductDescriptorQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ProductDescriptorQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProductDescriptorQuery query)
		{
			return ProductDescriptorQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProductDescriptorQuery(string query)
		{
			return (ProductDescriptorQuery)ProductDescriptorQuery.SerializeHelper.FromXml(query, typeof(ProductDescriptorQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProductDescriptor : esEntity
	{
		public esProductDescriptor()
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
			ProductDescriptorQuery query = new ProductDescriptorQuery();
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
		/// Maps to DNNspot_Store_ProductDescriptor.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ProductDescriptorMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductDescriptorMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductDescriptor.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(ProductDescriptorMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductDescriptorMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductDescriptor.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ProductDescriptorMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ProductDescriptorMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductDescriptor.Text
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Text
		{
			get
			{
				return base.GetSystemString(ProductDescriptorMetadata.ColumnNames.Text);
			}
			
			set
			{
				if(base.SetSystemString(ProductDescriptorMetadata.ColumnNames.Text, value))
				{
					OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.Text);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductDescriptor.SortOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? SortOrder
		{
			get
			{
				return base.GetSystemInt16(ProductDescriptorMetadata.ColumnNames.SortOrder);
			}
			
			set
			{
				if(base.SetSystemInt16(ProductDescriptorMetadata.ColumnNames.SortOrder, value))
				{
					OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.SortOrder);
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
						case "Name": this.str().Name = (string)value; break;							
						case "Text": this.str().Text = (string)value; break;							
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
								OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.Id);
							break;
						
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.ProductId);
							break;
						
						case "SortOrder":
						
							if (value == null || value is System.Int16)
								this.SortOrder = (System.Int16?)value;
								OnPropertyChanged(ProductDescriptorMetadata.PropertyNames.SortOrder);
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
			public esStrings(esProductDescriptor entity)
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
				
			public System.String Text
			{
				get
				{
					System.String data = entity.Text;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Text = null;
					else entity.Text = Convert.ToString(value);
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
			

			private esProductDescriptor entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProductDescriptorMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProductDescriptorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductDescriptorQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductDescriptorQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ProductDescriptorQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ProductDescriptorQuery query;		
	}



	[Serializable]
	abstract public partial class esProductDescriptorCollection : esEntityCollection<ProductDescriptor>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductDescriptorMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProductDescriptorCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProductDescriptorQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductDescriptorQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductDescriptorQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductDescriptorQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProductDescriptorQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProductDescriptorQuery)query);
		}

		#endregion
		
		private ProductDescriptorQuery query;
	}



	[Serializable]
	abstract public partial class esProductDescriptorQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductDescriptorMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "ProductId": return this.ProductId;
				case "Name": return this.Name;
				case "Text": return this.Text;
				case "SortOrder": return this.SortOrder;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ProductDescriptorMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, ProductDescriptorMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ProductDescriptorMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Text
		{
			get { return new esQueryItem(this, ProductDescriptorMetadata.ColumnNames.Text, esSystemType.String); }
		} 
		
		public esQueryItem SortOrder
		{
			get { return new esQueryItem(this, ProductDescriptorMetadata.ColumnNames.SortOrder, esSystemType.Int16); }
		} 
		
		#endregion
		
	}


	
	public partial class ProductDescriptor : esProductDescriptor
	{

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ProductDescriptor_DNNspot_Store_Product
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
	public partial class ProductDescriptorMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductDescriptorMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductDescriptorMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductDescriptorMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductDescriptorMetadata.ColumnNames.ProductId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductDescriptorMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductDescriptorMetadata.ColumnNames.Name, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductDescriptorMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 75;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductDescriptorMetadata.ColumnNames.Text, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductDescriptorMetadata.PropertyNames.Text;
			c.CharacterMaxLength = 1073741823;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductDescriptorMetadata.ColumnNames.SortOrder, 4, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ProductDescriptorMetadata.PropertyNames.SortOrder;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((99))";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductDescriptorMetadata Meta()
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
			 public const string Name = "Name";
			 public const string Text = "Text";
			 public const string SortOrder = "SortOrder";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string ProductId = "ProductId";
			 public const string Name = "Name";
			 public const string Text = "Text";
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
			lock (typeof(ProductDescriptorMetadata))
			{
				if(ProductDescriptorMetadata.mapDelegates == null)
				{
					ProductDescriptorMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductDescriptorMetadata.meta == null)
				{
					ProductDescriptorMetadata.meta = new ProductDescriptorMetadata();
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
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Text", new esTypeMap("nvarchar", "System.String"));
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
					meta.Source = objectQualifier + "DNNspot_Store_ProductDescriptor";
					meta.Destination = objectQualifier + "DNNspot_Store_ProductDescriptor";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ProductDescriptorInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ProductDescriptorUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ProductDescriptorDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ProductDescriptorLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ProductDescriptorLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ProductDescriptor";
					meta.Destination = "DNNspot_Store_ProductDescriptor";
									
					meta.spInsert = "proc_DNNspot_Store_ProductDescriptorInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ProductDescriptorUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ProductDescriptorDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ProductDescriptorLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ProductDescriptorLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductDescriptorMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
