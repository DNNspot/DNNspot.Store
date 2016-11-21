
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
	/// Encapsulates the 'DNNspot_Store_RelatedProduct' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(RelatedProduct))]	
	[XmlType("RelatedProduct")]
	[Table(Name="RelatedProduct")]
	public partial class RelatedProduct : esRelatedProduct
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new RelatedProduct();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 productId, System.Int32 relatedProductId)
		{
			var obj = new RelatedProduct();
			obj.ProductId = productId;
			obj.RelatedProductId = relatedProductId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 productId, System.Int32 relatedProductId, esSqlAccessType sqlAccessType)
		{
			var obj = new RelatedProduct();
			obj.ProductId = productId;
			obj.RelatedProductId = relatedProductId;
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
		public override System.Int32? RelatedProductId
		{
			get { return base.RelatedProductId;  }
			set { base.RelatedProductId = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("RelatedProductCollection")]
	public partial class RelatedProductCollection : esRelatedProductCollection, IEnumerable<RelatedProduct>
	{
		public RelatedProduct FindByPrimaryKey(System.Int32 productId, System.Int32 relatedProductId)
		{
			return this.SingleOrDefault(e => e.ProductId == productId && e.RelatedProductId == relatedProductId);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(RelatedProduct))]
		public class RelatedProductCollectionWCFPacket : esCollectionWCFPacket<RelatedProductCollection>
		{
			public static implicit operator RelatedProductCollection(RelatedProductCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator RelatedProductCollectionWCFPacket(RelatedProductCollection collection)
			{
				return new RelatedProductCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class RelatedProductQuery : esRelatedProductQuery
	{
		public RelatedProductQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "RelatedProductQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(RelatedProductQuery query)
		{
			return RelatedProductQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator RelatedProductQuery(string query)
		{
			return (RelatedProductQuery)RelatedProductQuery.SerializeHelper.FromXml(query, typeof(RelatedProductQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esRelatedProduct : esEntity
	{
		public esRelatedProduct()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 productId, System.Int32 relatedProductId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productId, relatedProductId);
			else
				return LoadByPrimaryKeyStoredProcedure(productId, relatedProductId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 productId, System.Int32 relatedProductId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(productId, relatedProductId);
			else
				return LoadByPrimaryKeyStoredProcedure(productId, relatedProductId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 productId, System.Int32 relatedProductId)
		{
			RelatedProductQuery query = new RelatedProductQuery();
			query.Where(query.ProductId == productId, query.RelatedProductId == relatedProductId);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 productId, System.Int32 relatedProductId)
		{
			esParameters parms = new esParameters();
			parms.Add("ProductId", productId);			parms.Add("RelatedProductId", relatedProductId);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_RelatedProduct.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(RelatedProductMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(RelatedProductMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(RelatedProductMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_RelatedProduct.RelatedProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? RelatedProductId
		{
			get
			{
				return base.GetSystemInt32(RelatedProductMetadata.ColumnNames.RelatedProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(RelatedProductMetadata.ColumnNames.RelatedProductId, value))
				{
					this._UpToProductByRelatedProductId = null;
					this.OnPropertyChanged("UpToProductByRelatedProductId");
					OnPropertyChanged(RelatedProductMetadata.PropertyNames.RelatedProductId);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Product _UpToProductByProductId;
		[CLSCompliant(false)]
		internal protected Product _UpToProductByRelatedProductId;
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
						case "RelatedProductId": this.str().RelatedProductId = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(RelatedProductMetadata.PropertyNames.ProductId);
							break;
						
						case "RelatedProductId":
						
							if (value == null || value is System.Int32)
								this.RelatedProductId = (System.Int32?)value;
								OnPropertyChanged(RelatedProductMetadata.PropertyNames.RelatedProductId);
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
			public esStrings(esRelatedProduct entity)
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
				
			public System.String RelatedProductId
			{
				get
				{
					System.Int32? data = entity.RelatedProductId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RelatedProductId = null;
					else entity.RelatedProductId = Convert.ToInt32(value);
				}
			}
			

			private esRelatedProduct entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return RelatedProductMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public RelatedProductQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RelatedProductQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(RelatedProductQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(RelatedProductQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private RelatedProductQuery query;		
	}



	[Serializable]
	abstract public partial class esRelatedProductCollection : esEntityCollection<RelatedProduct>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return RelatedProductMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "RelatedProductCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public RelatedProductQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new RelatedProductQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(RelatedProductQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new RelatedProductQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(RelatedProductQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((RelatedProductQuery)query);
		}

		#endregion
		
		private RelatedProductQuery query;
	}



	[Serializable]
	abstract public partial class esRelatedProductQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return RelatedProductMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ProductId": return this.ProductId;
				case "RelatedProductId": return this.RelatedProductId;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, RelatedProductMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem RelatedProductId
		{
			get { return new esQueryItem(this, RelatedProductMetadata.ColumnNames.RelatedProductId, esSystemType.Int32); }
		} 
		
		#endregion
		
	}


	
	public partial class RelatedProduct : esRelatedProduct
	{

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_RelatedProduct_DNNspot_Store_Product
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
		

				
		#region UpToProductByRelatedProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_RelatedProduct_DNNspot_Store_Product1
		/// </summary>

		[XmlIgnore]
					
		public Product UpToProductByRelatedProductId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToProductByRelatedProductId == null && RelatedProductId != null)
				{
					this._UpToProductByRelatedProductId = new Product();
					this._UpToProductByRelatedProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToProductByRelatedProductId", this._UpToProductByRelatedProductId);
					this._UpToProductByRelatedProductId.Query.Where(this._UpToProductByRelatedProductId.Query.Id == this.RelatedProductId);
					this._UpToProductByRelatedProductId.Query.Load();
				}	
				return this._UpToProductByRelatedProductId;
			}
			
			set
			{
				this.RemovePreSave("UpToProductByRelatedProductId");
				

				if(value == null)
				{
					this.RelatedProductId = null;
					this._UpToProductByRelatedProductId = null;
				}
				else
				{
					this.RelatedProductId = value.Id;
					this._UpToProductByRelatedProductId = value;
					this.SetPreSave("UpToProductByRelatedProductId", this._UpToProductByRelatedProductId);
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
			if(!this.es.IsDeleted && this._UpToProductByRelatedProductId != null)
			{
				this.RelatedProductId = this._UpToProductByRelatedProductId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class RelatedProductMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected RelatedProductMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(RelatedProductMetadata.ColumnNames.ProductId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RelatedProductMetadata.PropertyNames.ProductId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(RelatedProductMetadata.ColumnNames.RelatedProductId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = RelatedProductMetadata.PropertyNames.RelatedProductId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public RelatedProductMetadata Meta()
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
			 public const string RelatedProductId = "RelatedProductId";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ProductId = "ProductId";
			 public const string RelatedProductId = "RelatedProductId";
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
			lock (typeof(RelatedProductMetadata))
			{
				if(RelatedProductMetadata.mapDelegates == null)
				{
					RelatedProductMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (RelatedProductMetadata.meta == null)
				{
					RelatedProductMetadata.meta = new RelatedProductMetadata();
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
				meta.AddTypeMap("RelatedProductId", new esTypeMap("int", "System.Int32"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_RelatedProduct";
					meta.Destination = objectQualifier + "DNNspot_Store_RelatedProduct";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_RelatedProductInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_RelatedProductUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_RelatedProductDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_RelatedProductLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_RelatedProductLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_RelatedProduct";
					meta.Destination = "DNNspot_Store_RelatedProduct";
									
					meta.spInsert = "proc_DNNspot_Store_RelatedProductInsert";				
					meta.spUpdate = "proc_DNNspot_Store_RelatedProductUpdate";		
					meta.spDelete = "proc_DNNspot_Store_RelatedProductDelete";
					meta.spLoadAll = "proc_DNNspot_Store_RelatedProductLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_RelatedProductLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private RelatedProductMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
