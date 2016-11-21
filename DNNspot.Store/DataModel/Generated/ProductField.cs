
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
	/// Encapsulates the 'DNNspot_Store_ProductField' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ProductField))]	
	[XmlType("ProductField")]
	[Table(Name="ProductField")]
	public partial class ProductField : esProductField
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ProductField();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ProductField();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ProductField();
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
		public override System.String WidgetType
		{
			get { return base.WidgetType;  }
			set { base.WidgetType = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsRequired
		{
			get { return base.IsRequired;  }
			set { base.IsRequired = value; }
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

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String Slug
		{
			get { return base.Slug;  }
			set { base.Slug = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ProductFieldCollection")]
	public partial class ProductFieldCollection : esProductFieldCollection, IEnumerable<ProductField>
	{
		public ProductField FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ProductField))]
		public class ProductFieldCollectionWCFPacket : esCollectionWCFPacket<ProductFieldCollection>
		{
			public static implicit operator ProductFieldCollection(ProductFieldCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ProductFieldCollectionWCFPacket(ProductFieldCollection collection)
			{
				return new ProductFieldCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ProductFieldQuery : esProductFieldQuery
	{
		public ProductFieldQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ProductFieldQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProductFieldQuery query)
		{
			return ProductFieldQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProductFieldQuery(string query)
		{
			return (ProductFieldQuery)ProductFieldQuery.SerializeHelper.FromXml(query, typeof(ProductFieldQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProductField : esEntity
	{
		public esProductField()
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
			ProductFieldQuery query = new ProductFieldQuery();
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
		/// Maps to DNNspot_Store_ProductField.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ProductFieldMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductFieldMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(ProductFieldMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductFieldMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.WidgetType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String WidgetType
		{
			get
			{
				return base.GetSystemString(ProductFieldMetadata.ColumnNames.WidgetType);
			}
			
			set
			{
				if(base.SetSystemString(ProductFieldMetadata.ColumnNames.WidgetType, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.WidgetType);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ProductFieldMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ProductFieldMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.IsRequired
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsRequired
		{
			get
			{
				return base.GetSystemBoolean(ProductFieldMetadata.ColumnNames.IsRequired);
			}
			
			set
			{
				if(base.SetSystemBoolean(ProductFieldMetadata.ColumnNames.IsRequired, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.IsRequired);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.PriceAdjustment
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? PriceAdjustment
		{
			get
			{
				return base.GetSystemDecimal(ProductFieldMetadata.ColumnNames.PriceAdjustment);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductFieldMetadata.ColumnNames.PriceAdjustment, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.PriceAdjustment);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.WeightAdjustment
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? WeightAdjustment
		{
			get
			{
				return base.GetSystemDecimal(ProductFieldMetadata.ColumnNames.WeightAdjustment);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductFieldMetadata.ColumnNames.WeightAdjustment, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.WeightAdjustment);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.SortOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? SortOrder
		{
			get
			{
				return base.GetSystemInt16(ProductFieldMetadata.ColumnNames.SortOrder);
			}
			
			set
			{
				if(base.SetSystemInt16(ProductFieldMetadata.ColumnNames.SortOrder, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.SortOrder);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ProductField.Slug
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Slug
		{
			get
			{
				return base.GetSystemString(ProductFieldMetadata.ColumnNames.Slug);
			}
			
			set
			{
				if(base.SetSystemString(ProductFieldMetadata.ColumnNames.Slug, value))
				{
					OnPropertyChanged(ProductFieldMetadata.PropertyNames.Slug);
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
						case "WidgetType": this.str().WidgetType = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "IsRequired": this.str().IsRequired = (string)value; break;							
						case "PriceAdjustment": this.str().PriceAdjustment = (string)value; break;							
						case "WeightAdjustment": this.str().WeightAdjustment = (string)value; break;							
						case "SortOrder": this.str().SortOrder = (string)value; break;							
						case "Slug": this.str().Slug = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ProductFieldMetadata.PropertyNames.Id);
							break;
						
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(ProductFieldMetadata.PropertyNames.ProductId);
							break;
						
						case "IsRequired":
						
							if (value == null || value is System.Boolean)
								this.IsRequired = (System.Boolean?)value;
								OnPropertyChanged(ProductFieldMetadata.PropertyNames.IsRequired);
							break;
						
						case "PriceAdjustment":
						
							if (value == null || value is System.Decimal)
								this.PriceAdjustment = (System.Decimal?)value;
								OnPropertyChanged(ProductFieldMetadata.PropertyNames.PriceAdjustment);
							break;
						
						case "WeightAdjustment":
						
							if (value == null || value is System.Decimal)
								this.WeightAdjustment = (System.Decimal?)value;
								OnPropertyChanged(ProductFieldMetadata.PropertyNames.WeightAdjustment);
							break;
						
						case "SortOrder":
						
							if (value == null || value is System.Int16)
								this.SortOrder = (System.Int16?)value;
								OnPropertyChanged(ProductFieldMetadata.PropertyNames.SortOrder);
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
			public esStrings(esProductField entity)
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
				
			public System.String WidgetType
			{
				get
				{
					System.String data = entity.WidgetType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WidgetType = null;
					else entity.WidgetType = Convert.ToString(value);
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
				
			public System.String IsRequired
			{
				get
				{
					System.Boolean? data = entity.IsRequired;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsRequired = null;
					else entity.IsRequired = Convert.ToBoolean(value);
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
				
			public System.String Slug
			{
				get
				{
					System.String data = entity.Slug;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Slug = null;
					else entity.Slug = Convert.ToString(value);
				}
			}
			

			private esProductField entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProductFieldMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProductFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductFieldQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductFieldQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ProductFieldQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ProductFieldQuery query;		
	}



	[Serializable]
	abstract public partial class esProductFieldCollection : esEntityCollection<ProductField>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductFieldMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProductFieldCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProductFieldQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductFieldQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductFieldQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductFieldQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProductFieldQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProductFieldQuery)query);
		}

		#endregion
		
		private ProductFieldQuery query;
	}



	[Serializable]
	abstract public partial class esProductFieldQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductFieldMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "ProductId": return this.ProductId;
				case "WidgetType": return this.WidgetType;
				case "Name": return this.Name;
				case "IsRequired": return this.IsRequired;
				case "PriceAdjustment": return this.PriceAdjustment;
				case "WeightAdjustment": return this.WeightAdjustment;
				case "SortOrder": return this.SortOrder;
				case "Slug": return this.Slug;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem WidgetType
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.WidgetType, esSystemType.String); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem IsRequired
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.IsRequired, esSystemType.Boolean); }
		} 
		
		public esQueryItem PriceAdjustment
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.PriceAdjustment, esSystemType.Decimal); }
		} 
		
		public esQueryItem WeightAdjustment
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.WeightAdjustment, esSystemType.Decimal); }
		} 
		
		public esQueryItem SortOrder
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.SortOrder, esSystemType.Int16); }
		} 
		
		public esQueryItem Slug
		{
			get { return new esQueryItem(this, ProductFieldMetadata.ColumnNames.Slug, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class ProductField : esProductField
	{

		#region ProductFieldChoiceCollectionByProductFieldId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductFieldChoiceCollectionByProductFieldId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.ProductField.ProductFieldChoiceCollectionByProductFieldId_Delegate;
				map.PropertyName = "ProductFieldChoiceCollectionByProductFieldId";
				map.MyColumnName = "ProductFieldId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductFieldChoiceCollectionByProductFieldId_Delegate(esPrefetchParameters data)
		{
			ProductFieldQuery parent = new ProductFieldQuery(data.NextAlias());

			ProductFieldChoiceQuery me = data.You != null ? data.You as ProductFieldChoiceQuery : new ProductFieldChoiceQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductFieldId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductAttributeOption_DNNspot_Store_ProductAttribute
		/// </summary>

		[XmlIgnore]
		public ProductFieldChoiceCollection ProductFieldChoiceCollectionByProductFieldId
		{
			get
			{
				if(this._ProductFieldChoiceCollectionByProductFieldId == null)
				{
					this._ProductFieldChoiceCollectionByProductFieldId = new ProductFieldChoiceCollection();
					this._ProductFieldChoiceCollectionByProductFieldId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductFieldChoiceCollectionByProductFieldId", this._ProductFieldChoiceCollectionByProductFieldId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductFieldChoiceCollectionByProductFieldId.Query.Where(this._ProductFieldChoiceCollectionByProductFieldId.Query.ProductFieldId == this.Id);
							this._ProductFieldChoiceCollectionByProductFieldId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductFieldChoiceCollectionByProductFieldId.fks.Add(ProductFieldChoiceMetadata.ColumnNames.ProductFieldId, this.Id);
					}
				}

				return this._ProductFieldChoiceCollectionByProductFieldId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductFieldChoiceCollectionByProductFieldId != null) 
				{ 
					this.RemovePostSave("ProductFieldChoiceCollectionByProductFieldId"); 
					this._ProductFieldChoiceCollectionByProductFieldId = null;
					
				} 
			} 			
		}
			
		
		private ProductFieldChoiceCollection _ProductFieldChoiceCollectionByProductFieldId;
		#endregion

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ProductAttribute_DNNspot_Store_Product
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
		

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "ProductFieldChoiceCollectionByProductFieldId":
					coll = this.ProductFieldChoiceCollectionByProductFieldId;
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
			
			props.Add(new esPropertyDescriptor(this, "ProductFieldChoiceCollectionByProductFieldId", typeof(ProductFieldChoiceCollection), new ProductFieldChoice()));
		
			return props;
		}
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
			if(this._ProductFieldChoiceCollectionByProductFieldId != null)
			{
				Apply(this._ProductFieldChoiceCollectionByProductFieldId, "ProductFieldId", this.Id);
			}
		}
		
	}
	



	[Serializable]
	public partial class ProductFieldMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductFieldMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductFieldMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.ProductId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductFieldMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.WidgetType, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductFieldMetadata.PropertyNames.WidgetType;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.Name, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductFieldMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 100;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.IsRequired, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductFieldMetadata.PropertyNames.IsRequired;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.PriceAdjustment, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductFieldMetadata.PropertyNames.PriceAdjustment;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.WeightAdjustment, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductFieldMetadata.PropertyNames.WeightAdjustment;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.SortOrder, 7, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ProductFieldMetadata.PropertyNames.SortOrder;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductFieldMetadata.ColumnNames.Slug, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductFieldMetadata.PropertyNames.Slug;
			c.CharacterMaxLength = 100;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductFieldMetadata Meta()
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
			 public const string WidgetType = "WidgetType";
			 public const string Name = "Name";
			 public const string IsRequired = "IsRequired";
			 public const string PriceAdjustment = "PriceAdjustment";
			 public const string WeightAdjustment = "WeightAdjustment";
			 public const string SortOrder = "SortOrder";
			 public const string Slug = "Slug";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string ProductId = "ProductId";
			 public const string WidgetType = "WidgetType";
			 public const string Name = "Name";
			 public const string IsRequired = "IsRequired";
			 public const string PriceAdjustment = "PriceAdjustment";
			 public const string WeightAdjustment = "WeightAdjustment";
			 public const string SortOrder = "SortOrder";
			 public const string Slug = "Slug";
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
			lock (typeof(ProductFieldMetadata))
			{
				if(ProductFieldMetadata.mapDelegates == null)
				{
					ProductFieldMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductFieldMetadata.meta == null)
				{
					ProductFieldMetadata.meta = new ProductFieldMetadata();
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
				meta.AddTypeMap("WidgetType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsRequired", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("PriceAdjustment", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("WeightAdjustment", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("SortOrder", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Slug", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_ProductField";
					meta.Destination = objectQualifier + "DNNspot_Store_ProductField";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ProductFieldInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ProductFieldUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ProductFieldDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ProductFieldLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ProductFieldLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ProductField";
					meta.Destination = "DNNspot_Store_ProductField";
									
					meta.spInsert = "proc_DNNspot_Store_ProductFieldInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ProductFieldUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ProductFieldDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ProductFieldLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ProductFieldLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductFieldMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
