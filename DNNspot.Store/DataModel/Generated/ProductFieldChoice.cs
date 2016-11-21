
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
	/// Encapsulates the 'DNNspot_Store_ProductFieldChoice' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ProductFieldChoice))]	
	[XmlType("ProductFieldChoice")]
	[Table(Name="ProductFieldChoice")]
	public partial class ProductFieldChoice : esProductFieldChoice
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ProductFieldChoice();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ProductFieldChoice();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ProductFieldChoice();
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
		public override System.Int32? ProductFieldId
		{
			get { return base.ProductFieldId;  }
			set { base.ProductFieldId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? PriceAdjustment
		{
			get { return base.PriceAdjustment;  }
			set { base.PriceAdjustment = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? WeightAdjustment
		{
			get { return base.WeightAdjustment;  }
			set { base.WeightAdjustment = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? SortOrder
		{
			get { return base.SortOrder;  }
			set { base.SortOrder = value; }
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
	[XmlType("ProductFieldChoiceCollection")]
	public partial class ProductFieldChoiceCollection : esProductFieldChoiceCollection, IEnumerable<ProductFieldChoice>
	{
		public ProductFieldChoice FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ProductFieldChoice))]
		public class ProductFieldChoiceCollectionWCFPacket : esCollectionWCFPacket<ProductFieldChoiceCollection>
		{
			public static implicit operator ProductFieldChoiceCollection(ProductFieldChoiceCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ProductFieldChoiceCollectionWCFPacket(ProductFieldChoiceCollection collection)
			{
				return new ProductFieldChoiceCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ProductFieldChoiceQuery : esProductFieldChoiceQuery
	{
		public ProductFieldChoiceQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ProductFieldChoiceQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProductFieldChoiceQuery query)
		{
			return ProductFieldChoiceQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProductFieldChoiceQuery(string query)
		{
			return (ProductFieldChoiceQuery)ProductFieldChoiceQuery.SerializeHelper.FromXml(query, typeof(ProductFieldChoiceQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProductFieldChoice : esEntity
	{
		public esProductFieldChoice()
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
			ProductFieldChoiceQuery query = new ProductFieldChoiceQuery();
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
		/// Maps to DNNspot_Store_ProductFieldChoice.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ProductFieldChoiceMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductFieldChoiceMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductFieldChoice.ProductFieldId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductFieldId
		{
			get
			{
				return base.GetSystemInt32(ProductFieldChoiceMetadata.ColumnNames.ProductFieldId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductFieldChoiceMetadata.ColumnNames.ProductFieldId, value))
				{
					this._UpToProductFieldByProductFieldId = null;
					this.OnPropertyChanged("UpToProductFieldByProductFieldId");
					OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.ProductFieldId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductFieldChoice.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ProductFieldChoiceMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ProductFieldChoiceMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductFieldChoice.PriceAdjustment
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? PriceAdjustment
		{
			get
			{
				return base.GetSystemDecimal(ProductFieldChoiceMetadata.ColumnNames.PriceAdjustment);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductFieldChoiceMetadata.ColumnNames.PriceAdjustment, value))
				{
					OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.PriceAdjustment);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductFieldChoice.WeightAdjustment
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? WeightAdjustment
		{
			get
			{
				return base.GetSystemDecimal(ProductFieldChoiceMetadata.ColumnNames.WeightAdjustment);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductFieldChoiceMetadata.ColumnNames.WeightAdjustment, value))
				{
					OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.WeightAdjustment);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductFieldChoice.SortOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? SortOrder
		{
			get
			{
				return base.GetSystemInt16(ProductFieldChoiceMetadata.ColumnNames.SortOrder);
			}
			
			set
			{
				if(base.SetSystemInt16(ProductFieldChoiceMetadata.ColumnNames.SortOrder, value))
				{
					OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.SortOrder);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductFieldChoice.Value
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Value
		{
			get
			{
				return base.GetSystemString(ProductFieldChoiceMetadata.ColumnNames.Value);
			}
			
			set
			{
				if(base.SetSystemString(ProductFieldChoiceMetadata.ColumnNames.Value, value))
				{
					OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.Value);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected ProductField _UpToProductFieldByProductFieldId;
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
						case "ProductFieldId": this.str().ProductFieldId = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "PriceAdjustment": this.str().PriceAdjustment = (string)value; break;							
						case "WeightAdjustment": this.str().WeightAdjustment = (string)value; break;							
						case "SortOrder": this.str().SortOrder = (string)value; break;							
						case "Value": this.str().Value = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.Id);
							break;
						
						case "ProductFieldId":
						
							if (value == null || value is System.Int32)
								this.ProductFieldId = (System.Int32?)value;
								OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.ProductFieldId);
							break;
						
						case "PriceAdjustment":
						
							if (value == null || value is System.Decimal)
								this.PriceAdjustment = (System.Decimal?)value;
								OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.PriceAdjustment);
							break;
						
						case "WeightAdjustment":
						
							if (value == null || value is System.Decimal)
								this.WeightAdjustment = (System.Decimal?)value;
								OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.WeightAdjustment);
							break;
						
						case "SortOrder":
						
							if (value == null || value is System.Int16)
								this.SortOrder = (System.Int16?)value;
								OnPropertyChanged(ProductFieldChoiceMetadata.PropertyNames.SortOrder);
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
			public esStrings(esProductFieldChoice entity)
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
				
			public System.String ProductFieldId
			{
				get
				{
					System.Int32? data = entity.ProductFieldId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductFieldId = null;
					else entity.ProductFieldId = Convert.ToInt32(value);
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
				
			public System.String PriceAdjustment
			{
				get
				{
					System.Decimal? data = entity.PriceAdjustment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceAdjustment = null;
					else entity.PriceAdjustment = Convert.ToDecimal(value);
				}
			}
				
			public System.String WeightAdjustment
			{
				get
				{
					System.Decimal? data = entity.WeightAdjustment;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeightAdjustment = null;
					else entity.WeightAdjustment = Convert.ToDecimal(value);
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
			

			private esProductFieldChoice entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProductFieldChoiceMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProductFieldChoiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductFieldChoiceQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductFieldChoiceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ProductFieldChoiceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ProductFieldChoiceQuery query;		
	}



	[Serializable]
	abstract public partial class esProductFieldChoiceCollection : esEntityCollection<ProductFieldChoice>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductFieldChoiceMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProductFieldChoiceCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProductFieldChoiceQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductFieldChoiceQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductFieldChoiceQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductFieldChoiceQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProductFieldChoiceQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProductFieldChoiceQuery)query);
		}

		#endregion
		
		private ProductFieldChoiceQuery query;
	}



	[Serializable]
	abstract public partial class esProductFieldChoiceQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductFieldChoiceMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "ProductFieldId": return this.ProductFieldId;
				case "Name": return this.Name;
				case "PriceAdjustment": return this.PriceAdjustment;
				case "WeightAdjustment": return this.WeightAdjustment;
				case "SortOrder": return this.SortOrder;
				case "Value": return this.Value;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ProductFieldChoiceMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductFieldId
		{
			get { return new esQueryItem(this, ProductFieldChoiceMetadata.ColumnNames.ProductFieldId, esSystemType.Int32); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ProductFieldChoiceMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem PriceAdjustment
		{
			get { return new esQueryItem(this, ProductFieldChoiceMetadata.ColumnNames.PriceAdjustment, esSystemType.Decimal); }
		} 
		
		public esQueryItem WeightAdjustment
		{
			get { return new esQueryItem(this, ProductFieldChoiceMetadata.ColumnNames.WeightAdjustment, esSystemType.Decimal); }
		} 
		
		public esQueryItem SortOrder
		{
			get { return new esQueryItem(this, ProductFieldChoiceMetadata.ColumnNames.SortOrder, esSystemType.Int16); }
		} 
		
		public esQueryItem Value
		{
			get { return new esQueryItem(this, ProductFieldChoiceMetadata.ColumnNames.Value, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class ProductFieldChoice : esProductFieldChoice
	{

				
		#region UpToProductFieldByProductFieldId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ProductAttributeOption_DNNspot_Store_ProductAttribute
		/// </summary>

		[XmlIgnore]
					
		public ProductField UpToProductFieldByProductFieldId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToProductFieldByProductFieldId == null && ProductFieldId != null)
				{
					this._UpToProductFieldByProductFieldId = new ProductField();
					this._UpToProductFieldByProductFieldId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToProductFieldByProductFieldId", this._UpToProductFieldByProductFieldId);
					this._UpToProductFieldByProductFieldId.Query.Where(this._UpToProductFieldByProductFieldId.Query.Id == this.ProductFieldId);
					this._UpToProductFieldByProductFieldId.Query.Load();
				}	
				return this._UpToProductFieldByProductFieldId;
			}
			
			set
			{
				this.RemovePreSave("UpToProductFieldByProductFieldId");
				

				if(value == null)
				{
					this.ProductFieldId = null;
					this._UpToProductFieldByProductFieldId = null;
				}
				else
				{
					this.ProductFieldId = value.Id;
					this._UpToProductFieldByProductFieldId = value;
					this.SetPreSave("UpToProductFieldByProductFieldId", this._UpToProductFieldByProductFieldId);
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
			if(!this.es.IsDeleted && this._UpToProductFieldByProductFieldId != null)
			{
				this.ProductFieldId = this._UpToProductFieldByProductFieldId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class ProductFieldChoiceMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductFieldChoiceMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductFieldChoiceMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductFieldChoiceMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldChoiceMetadata.ColumnNames.ProductFieldId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductFieldChoiceMetadata.PropertyNames.ProductFieldId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldChoiceMetadata.ColumnNames.Name, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductFieldChoiceMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 100;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldChoiceMetadata.ColumnNames.PriceAdjustment, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductFieldChoiceMetadata.PropertyNames.PriceAdjustment;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldChoiceMetadata.ColumnNames.WeightAdjustment, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductFieldChoiceMetadata.PropertyNames.WeightAdjustment;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldChoiceMetadata.ColumnNames.SortOrder, 5, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ProductFieldChoiceMetadata.PropertyNames.SortOrder;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldChoiceMetadata.ColumnNames.Value, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductFieldChoiceMetadata.PropertyNames.Value;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductFieldChoiceMetadata Meta()
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
			 public const string ProductFieldId = "ProductFieldId";
			 public const string Name = "Name";
			 public const string PriceAdjustment = "PriceAdjustment";
			 public const string WeightAdjustment = "WeightAdjustment";
			 public const string SortOrder = "SortOrder";
			 public const string Value = "Value";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string ProductFieldId = "ProductFieldId";
			 public const string Name = "Name";
			 public const string PriceAdjustment = "PriceAdjustment";
			 public const string WeightAdjustment = "WeightAdjustment";
			 public const string SortOrder = "SortOrder";
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
			lock (typeof(ProductFieldChoiceMetadata))
			{
				if(ProductFieldChoiceMetadata.mapDelegates == null)
				{
					ProductFieldChoiceMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductFieldChoiceMetadata.meta == null)
				{
					ProductFieldChoiceMetadata.meta = new ProductFieldChoiceMetadata();
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
				meta.AddTypeMap("ProductFieldId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("PriceAdjustment", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("WeightAdjustment", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SortOrder", new esTypeMap("smallint", "System.Int16"));
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
					meta.Source = objectQualifier + "DNNspot_Store_ProductFieldChoice";
					meta.Destination = objectQualifier + "DNNspot_Store_ProductFieldChoice";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ProductFieldChoiceInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ProductFieldChoiceUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ProductFieldChoiceDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ProductFieldChoiceLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ProductFieldChoiceLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ProductFieldChoice";
					meta.Destination = "DNNspot_Store_ProductFieldChoice";
									
					meta.spInsert = "proc_DNNspot_Store_ProductFieldChoiceInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ProductFieldChoiceUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ProductFieldChoiceDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ProductFieldChoiceLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ProductFieldChoiceLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductFieldChoiceMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
