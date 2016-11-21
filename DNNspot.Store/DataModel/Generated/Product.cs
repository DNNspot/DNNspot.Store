
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
	/// Encapsulates the 'DNNspot_Store_Product' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Product))]	
	[XmlType("Product")]
	[Table(Name="Product")]
	public partial class Product : esProduct
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Product();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new Product();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Product();
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
		public override System.Boolean? IsActive
		{
			get { return base.IsActive;  }
			set { base.IsActive = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Slug
		{
			get { return base.Slug;  }
			set { base.Slug = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Sku
		{
			get { return base.Sku;  }
			set { base.Sku = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SpecialNotes
		{
			get { return base.SpecialNotes;  }
			set { base.SpecialNotes = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? Price
		{
			get { return base.Price;  }
			set { base.Price = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? Weight
		{
			get { return base.Weight;  }
			set { base.Weight = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int16? DeliveryMethodId
		{
			get { return base.DeliveryMethodId;  }
			set { base.DeliveryMethodId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? ShippingAdditionalFeePerItem
		{
			get { return base.ShippingAdditionalFeePerItem;  }
			set { base.ShippingAdditionalFeePerItem = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String QuantityWidget
		{
			get { return base.QuantityWidget;  }
			set { base.QuantityWidget = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String QuantityOptions
		{
			get { return base.QuantityOptions;  }
			set { base.QuantityOptions = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? InventoryIsEnabled
		{
			get { return base.InventoryIsEnabled;  }
			set { base.InventoryIsEnabled = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? InventoryAllowNegativeStockLevel
		{
			get { return base.InventoryAllowNegativeStockLevel;  }
			set { base.InventoryAllowNegativeStockLevel = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? InventoryQtyInStock
		{
			get { return base.InventoryQtyInStock;  }
			set { base.InventoryQtyInStock = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? InventoryQtyLowThreshold
		{
			get { return base.InventoryQtyLowThreshold;  }
			set { base.InventoryQtyLowThreshold = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DigitalFileDisplayName
		{
			get { return base.DigitalFileDisplayName;  }
			set { base.DigitalFileDisplayName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DigitalFilename
		{
			get { return base.DigitalFilename;  }
			set { base.DigitalFilename = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SeoTitle
		{
			get { return base.SeoTitle;  }
			set { base.SeoTitle = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SeoDescription
		{
			get { return base.SeoDescription;  }
			set { base.SeoDescription = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SeoKeywords
		{
			get { return base.SeoKeywords;  }
			set { base.SeoKeywords = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.DateTime? CreatedOn
		{
			get { return base.CreatedOn;  }
			set { base.CreatedOn = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.DateTime? ModifiedOn
		{
			get { return base.ModifiedOn;  }
			set { base.ModifiedOn = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsTaxable
		{
			get { return base.IsTaxable;  }
			set { base.IsTaxable = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CheckoutAssignRoleInfoJson
		{
			get { return base.CheckoutAssignRoleInfoJson;  }
			set { base.CheckoutAssignRoleInfoJson = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsPriceDisplayed
		{
			get { return base.IsPriceDisplayed;  }
			set { base.IsPriceDisplayed = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsAvailableForPurchase
		{
			get { return base.IsAvailableForPurchase;  }
			set { base.IsAvailableForPurchase = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? Length
		{
			get { return base.Length;  }
			set { base.Length = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? Width
		{
			get { return base.Width;  }
			set { base.Width = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? Height
		{
			get { return base.Height;  }
			set { base.Height = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String ViewPermissions
		{
			get { return base.ViewPermissions;  }
			set { base.ViewPermissions = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String CheckoutPermissions
		{
			get { return base.CheckoutPermissions;  }
			set { base.CheckoutPermissions = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ProductCollection")]
	public partial class ProductCollection : esProductCollection, IEnumerable<Product>
	{
		public Product FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Product))]
		public class ProductCollectionWCFPacket : esCollectionWCFPacket<ProductCollection>
		{
			public static implicit operator ProductCollection(ProductCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ProductCollectionWCFPacket(ProductCollection collection)
			{
				return new ProductCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ProductQuery : esProductQuery
	{
		public ProductQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ProductQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ProductQuery query)
		{
			return ProductQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ProductQuery(string query)
		{
			return (ProductQuery)ProductQuery.SerializeHelper.FromXml(query, typeof(ProductQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esProduct : esEntity
	{
		public esProduct()
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
			ProductQuery query = new ProductQuery();
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
		/// Maps to DNNspot_Store_Product.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ProductMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(ProductMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductMetadata.ColumnNames.StoreId, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.IsActive
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(ProductMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				if(base.SetSystemBoolean(ProductMetadata.ColumnNames.IsActive, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.IsActive);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Slug
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Slug
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.Slug);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.Slug, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Slug);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Sku
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Sku
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.Sku);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.Sku, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Sku);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.SpecialNotes
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SpecialNotes
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.SpecialNotes);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.SpecialNotes, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.SpecialNotes);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Price
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Price
		{
			get
			{
				return base.GetSystemDecimal(ProductMetadata.ColumnNames.Price);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductMetadata.ColumnNames.Price, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Price);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Weight
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Weight
		{
			get
			{
				return base.GetSystemDecimal(ProductMetadata.ColumnNames.Weight);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductMetadata.ColumnNames.Weight, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Weight);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.DeliveryMethodId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? DeliveryMethodId
		{
			get
			{
				return base.GetSystemInt16(ProductMetadata.ColumnNames.DeliveryMethodId);
			}
			
			set
			{
				if(base.SetSystemInt16(ProductMetadata.ColumnNames.DeliveryMethodId, value))
				{
					this._UpToDeliveryMethodByDeliveryMethodId = null;
					this.OnPropertyChanged("UpToDeliveryMethodByDeliveryMethodId");
					OnPropertyChanged(ProductMetadata.PropertyNames.DeliveryMethodId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.ShippingAdditionalFeePerItem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? ShippingAdditionalFeePerItem
		{
			get
			{
				return base.GetSystemDecimal(ProductMetadata.ColumnNames.ShippingAdditionalFeePerItem);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductMetadata.ColumnNames.ShippingAdditionalFeePerItem, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.ShippingAdditionalFeePerItem);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.QuantityWidget
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String QuantityWidget
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.QuantityWidget);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.QuantityWidget, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.QuantityWidget);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.QuantityOptions
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String QuantityOptions
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.QuantityOptions);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.QuantityOptions, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.QuantityOptions);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.InventoryIsEnabled
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? InventoryIsEnabled
		{
			get
			{
				return base.GetSystemBoolean(ProductMetadata.ColumnNames.InventoryIsEnabled);
			}
			
			set
			{
				if(base.SetSystemBoolean(ProductMetadata.ColumnNames.InventoryIsEnabled, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.InventoryIsEnabled);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.InventoryAllowNegativeStockLevel
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? InventoryAllowNegativeStockLevel
		{
			get
			{
				return base.GetSystemBoolean(ProductMetadata.ColumnNames.InventoryAllowNegativeStockLevel);
			}
			
			set
			{
				if(base.SetSystemBoolean(ProductMetadata.ColumnNames.InventoryAllowNegativeStockLevel, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.InventoryAllowNegativeStockLevel);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.InventoryQtyInStock
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? InventoryQtyInStock
		{
			get
			{
				return base.GetSystemInt32(ProductMetadata.ColumnNames.InventoryQtyInStock);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductMetadata.ColumnNames.InventoryQtyInStock, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.InventoryQtyInStock);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.InventoryQtyLowThreshold
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? InventoryQtyLowThreshold
		{
			get
			{
				return base.GetSystemInt32(ProductMetadata.ColumnNames.InventoryQtyLowThreshold);
			}
			
			set
			{
				if(base.SetSystemInt32(ProductMetadata.ColumnNames.InventoryQtyLowThreshold, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.InventoryQtyLowThreshold);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.DigitalFileDisplayName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DigitalFileDisplayName
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.DigitalFileDisplayName);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.DigitalFileDisplayName, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.DigitalFileDisplayName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.DigitalFilename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DigitalFilename
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.DigitalFilename);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.DigitalFilename, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.DigitalFilename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.SeoTitle
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SeoTitle
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.SeoTitle);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.SeoTitle, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.SeoTitle);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.SeoDescription
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SeoDescription
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.SeoDescription);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.SeoDescription, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.SeoDescription);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.SeoKeywords
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SeoKeywords
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.SeoKeywords);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.SeoKeywords, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.SeoKeywords);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(ProductMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(ProductMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.CreatedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.ModifiedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ModifiedOn
		{
			get
			{
				return base.GetSystemDateTime(ProductMetadata.ColumnNames.ModifiedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(ProductMetadata.ColumnNames.ModifiedOn, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.ModifiedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.IsTaxable
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsTaxable
		{
			get
			{
				return base.GetSystemBoolean(ProductMetadata.ColumnNames.IsTaxable);
			}
			
			set
			{
				if(base.SetSystemBoolean(ProductMetadata.ColumnNames.IsTaxable, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.IsTaxable);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.CheckoutAssignRoleInfoJson
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CheckoutAssignRoleInfoJson
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.CheckoutAssignRoleInfoJson);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.CheckoutAssignRoleInfoJson, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.CheckoutAssignRoleInfoJson);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.IsPriceDisplayed
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsPriceDisplayed
		{
			get
			{
				return base.GetSystemBoolean(ProductMetadata.ColumnNames.IsPriceDisplayed);
			}
			
			set
			{
				if(base.SetSystemBoolean(ProductMetadata.ColumnNames.IsPriceDisplayed, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.IsPriceDisplayed);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.IsAvailableForPurchase
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsAvailableForPurchase
		{
			get
			{
				return base.GetSystemBoolean(ProductMetadata.ColumnNames.IsAvailableForPurchase);
			}
			
			set
			{
				if(base.SetSystemBoolean(ProductMetadata.ColumnNames.IsAvailableForPurchase, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.IsAvailableForPurchase);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Length
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Length
		{
			get
			{
				return base.GetSystemDecimal(ProductMetadata.ColumnNames.Length);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductMetadata.ColumnNames.Length, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Length);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Width
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Width
		{
			get
			{
				return base.GetSystemDecimal(ProductMetadata.ColumnNames.Width);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductMetadata.ColumnNames.Width, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Width);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.Height
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Height
		{
			get
			{
				return base.GetSystemDecimal(ProductMetadata.ColumnNames.Height);
			}
			
			set
			{
				if(base.SetSystemDecimal(ProductMetadata.ColumnNames.Height, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.Height);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.ViewPermissions
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ViewPermissions
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.ViewPermissions);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.ViewPermissions, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.ViewPermissions);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Product.CheckoutPermissions
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CheckoutPermissions
		{
			get
			{
				return base.GetSystemString(ProductMetadata.ColumnNames.CheckoutPermissions);
			}
			
			set
			{
				if(base.SetSystemString(ProductMetadata.ColumnNames.CheckoutPermissions, value))
				{
					OnPropertyChanged(ProductMetadata.PropertyNames.CheckoutPermissions);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected DeliveryMethod _UpToDeliveryMethodByDeliveryMethodId;
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
						case "IsActive": this.str().IsActive = (string)value; break;							
						case "Slug": this.str().Slug = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "Sku": this.str().Sku = (string)value; break;							
						case "SpecialNotes": this.str().SpecialNotes = (string)value; break;							
						case "Price": this.str().Price = (string)value; break;							
						case "Weight": this.str().Weight = (string)value; break;							
						case "DeliveryMethodId": this.str().DeliveryMethodId = (string)value; break;							
						case "ShippingAdditionalFeePerItem": this.str().ShippingAdditionalFeePerItem = (string)value; break;							
						case "QuantityWidget": this.str().QuantityWidget = (string)value; break;							
						case "QuantityOptions": this.str().QuantityOptions = (string)value; break;							
						case "InventoryIsEnabled": this.str().InventoryIsEnabled = (string)value; break;							
						case "InventoryAllowNegativeStockLevel": this.str().InventoryAllowNegativeStockLevel = (string)value; break;							
						case "InventoryQtyInStock": this.str().InventoryQtyInStock = (string)value; break;							
						case "InventoryQtyLowThreshold": this.str().InventoryQtyLowThreshold = (string)value; break;							
						case "DigitalFileDisplayName": this.str().DigitalFileDisplayName = (string)value; break;							
						case "DigitalFilename": this.str().DigitalFilename = (string)value; break;							
						case "SeoTitle": this.str().SeoTitle = (string)value; break;							
						case "SeoDescription": this.str().SeoDescription = (string)value; break;							
						case "SeoKeywords": this.str().SeoKeywords = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;							
						case "ModifiedOn": this.str().ModifiedOn = (string)value; break;							
						case "IsTaxable": this.str().IsTaxable = (string)value; break;							
						case "CheckoutAssignRoleInfoJson": this.str().CheckoutAssignRoleInfoJson = (string)value; break;							
						case "IsPriceDisplayed": this.str().IsPriceDisplayed = (string)value; break;							
						case "IsAvailableForPurchase": this.str().IsAvailableForPurchase = (string)value; break;							
						case "Length": this.str().Length = (string)value; break;							
						case "Width": this.str().Width = (string)value; break;							
						case "Height": this.str().Height = (string)value; break;							
						case "ViewPermissions": this.str().ViewPermissions = (string)value; break;							
						case "CheckoutPermissions": this.str().CheckoutPermissions = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.Id);
							break;
						
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.StoreId);
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.IsActive);
							break;
						
						case "Price":
						
							if (value == null || value is System.Decimal)
								this.Price = (System.Decimal?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.Price);
							break;
						
						case "Weight":
						
							if (value == null || value is System.Decimal)
								this.Weight = (System.Decimal?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.Weight);
							break;
						
						case "DeliveryMethodId":
						
							if (value == null || value is System.Int16)
								this.DeliveryMethodId = (System.Int16?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.DeliveryMethodId);
							break;
						
						case "ShippingAdditionalFeePerItem":
						
							if (value == null || value is System.Decimal)
								this.ShippingAdditionalFeePerItem = (System.Decimal?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.ShippingAdditionalFeePerItem);
							break;
						
						case "InventoryIsEnabled":
						
							if (value == null || value is System.Boolean)
								this.InventoryIsEnabled = (System.Boolean?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.InventoryIsEnabled);
							break;
						
						case "InventoryAllowNegativeStockLevel":
						
							if (value == null || value is System.Boolean)
								this.InventoryAllowNegativeStockLevel = (System.Boolean?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.InventoryAllowNegativeStockLevel);
							break;
						
						case "InventoryQtyInStock":
						
							if (value == null || value is System.Int32)
								this.InventoryQtyInStock = (System.Int32?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.InventoryQtyInStock);
							break;
						
						case "InventoryQtyLowThreshold":
						
							if (value == null || value is System.Int32)
								this.InventoryQtyLowThreshold = (System.Int32?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.InventoryQtyLowThreshold);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.CreatedOn);
							break;
						
						case "ModifiedOn":
						
							if (value == null || value is System.DateTime)
								this.ModifiedOn = (System.DateTime?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.ModifiedOn);
							break;
						
						case "IsTaxable":
						
							if (value == null || value is System.Boolean)
								this.IsTaxable = (System.Boolean?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.IsTaxable);
							break;
						
						case "IsPriceDisplayed":
						
							if (value == null || value is System.Boolean)
								this.IsPriceDisplayed = (System.Boolean?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.IsPriceDisplayed);
							break;
						
						case "IsAvailableForPurchase":
						
							if (value == null || value is System.Boolean)
								this.IsAvailableForPurchase = (System.Boolean?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.IsAvailableForPurchase);
							break;
						
						case "Length":
						
							if (value == null || value is System.Decimal)
								this.Length = (System.Decimal?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.Length);
							break;
						
						case "Width":
						
							if (value == null || value is System.Decimal)
								this.Width = (System.Decimal?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.Width);
							break;
						
						case "Height":
						
							if (value == null || value is System.Decimal)
								this.Height = (System.Decimal?)value;
								OnPropertyChanged(ProductMetadata.PropertyNames.Height);
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
			public esStrings(esProduct entity)
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
				
			public System.String IsActive
			{
				get
				{
					System.Boolean? data = entity.IsActive;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsActive = null;
					else entity.IsActive = Convert.ToBoolean(value);
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
				
			public System.String Sku
			{
				get
				{
					System.String data = entity.Sku;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Sku = null;
					else entity.Sku = Convert.ToString(value);
				}
			}
				
			public System.String SpecialNotes
			{
				get
				{
					System.String data = entity.SpecialNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SpecialNotes = null;
					else entity.SpecialNotes = Convert.ToString(value);
				}
			}
				
			public System.String Price
			{
				get
				{
					System.Decimal? data = entity.Price;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Price = null;
					else entity.Price = Convert.ToDecimal(value);
				}
			}
				
			public System.String Weight
			{
				get
				{
					System.Decimal? data = entity.Weight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Weight = null;
					else entity.Weight = Convert.ToDecimal(value);
				}
			}
				
			public System.String DeliveryMethodId
			{
				get
				{
					System.Int16? data = entity.DeliveryMethodId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DeliveryMethodId = null;
					else entity.DeliveryMethodId = Convert.ToInt16(value);
				}
			}
				
			public System.String ShippingAdditionalFeePerItem
			{
				get
				{
					System.Decimal? data = entity.ShippingAdditionalFeePerItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingAdditionalFeePerItem = null;
					else entity.ShippingAdditionalFeePerItem = Convert.ToDecimal(value);
				}
			}
				
			public System.String QuantityWidget
			{
				get
				{
					System.String data = entity.QuantityWidget;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityWidget = null;
					else entity.QuantityWidget = Convert.ToString(value);
				}
			}
				
			public System.String QuantityOptions
			{
				get
				{
					System.String data = entity.QuantityOptions;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.QuantityOptions = null;
					else entity.QuantityOptions = Convert.ToString(value);
				}
			}
				
			public System.String InventoryIsEnabled
			{
				get
				{
					System.Boolean? data = entity.InventoryIsEnabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InventoryIsEnabled = null;
					else entity.InventoryIsEnabled = Convert.ToBoolean(value);
				}
			}
				
			public System.String InventoryAllowNegativeStockLevel
			{
				get
				{
					System.Boolean? data = entity.InventoryAllowNegativeStockLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InventoryAllowNegativeStockLevel = null;
					else entity.InventoryAllowNegativeStockLevel = Convert.ToBoolean(value);
				}
			}
				
			public System.String InventoryQtyInStock
			{
				get
				{
					System.Int32? data = entity.InventoryQtyInStock;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InventoryQtyInStock = null;
					else entity.InventoryQtyInStock = Convert.ToInt32(value);
				}
			}
				
			public System.String InventoryQtyLowThreshold
			{
				get
				{
					System.Int32? data = entity.InventoryQtyLowThreshold;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.InventoryQtyLowThreshold = null;
					else entity.InventoryQtyLowThreshold = Convert.ToInt32(value);
				}
			}
				
			public System.String DigitalFileDisplayName
			{
				get
				{
					System.String data = entity.DigitalFileDisplayName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DigitalFileDisplayName = null;
					else entity.DigitalFileDisplayName = Convert.ToString(value);
				}
			}
				
			public System.String DigitalFilename
			{
				get
				{
					System.String data = entity.DigitalFilename;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DigitalFilename = null;
					else entity.DigitalFilename = Convert.ToString(value);
				}
			}
				
			public System.String SeoTitle
			{
				get
				{
					System.String data = entity.SeoTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeoTitle = null;
					else entity.SeoTitle = Convert.ToString(value);
				}
			}
				
			public System.String SeoDescription
			{
				get
				{
					System.String data = entity.SeoDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeoDescription = null;
					else entity.SeoDescription = Convert.ToString(value);
				}
			}
				
			public System.String SeoKeywords
			{
				get
				{
					System.String data = entity.SeoKeywords;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeoKeywords = null;
					else entity.SeoKeywords = Convert.ToString(value);
				}
			}
				
			public System.String CreatedOn
			{
				get
				{
					System.DateTime? data = entity.CreatedOn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedOn = null;
					else entity.CreatedOn = Convert.ToDateTime(value);
				}
			}
				
			public System.String ModifiedOn
			{
				get
				{
					System.DateTime? data = entity.ModifiedOn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ModifiedOn = null;
					else entity.ModifiedOn = Convert.ToDateTime(value);
				}
			}
				
			public System.String IsTaxable
			{
				get
				{
					System.Boolean? data = entity.IsTaxable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsTaxable = null;
					else entity.IsTaxable = Convert.ToBoolean(value);
				}
			}
				
			public System.String CheckoutAssignRoleInfoJson
			{
				get
				{
					System.String data = entity.CheckoutAssignRoleInfoJson;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckoutAssignRoleInfoJson = null;
					else entity.CheckoutAssignRoleInfoJson = Convert.ToString(value);
				}
			}
				
			public System.String IsPriceDisplayed
			{
				get
				{
					System.Boolean? data = entity.IsPriceDisplayed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsPriceDisplayed = null;
					else entity.IsPriceDisplayed = Convert.ToBoolean(value);
				}
			}
				
			public System.String IsAvailableForPurchase
			{
				get
				{
					System.Boolean? data = entity.IsAvailableForPurchase;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsAvailableForPurchase = null;
					else entity.IsAvailableForPurchase = Convert.ToBoolean(value);
				}
			}
				
			public System.String Length
			{
				get
				{
					System.Decimal? data = entity.Length;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Length = null;
					else entity.Length = Convert.ToDecimal(value);
				}
			}
				
			public System.String Width
			{
				get
				{
					System.Decimal? data = entity.Width;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Width = null;
					else entity.Width = Convert.ToDecimal(value);
				}
			}
				
			public System.String Height
			{
				get
				{
					System.Decimal? data = entity.Height;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Height = null;
					else entity.Height = Convert.ToDecimal(value);
				}
			}
				
			public System.String ViewPermissions
			{
				get
				{
					System.String data = entity.ViewPermissions;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ViewPermissions = null;
					else entity.ViewPermissions = Convert.ToString(value);
				}
			}
				
			public System.String CheckoutPermissions
			{
				get
				{
					System.String data = entity.CheckoutPermissions;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CheckoutPermissions = null;
					else entity.CheckoutPermissions = Convert.ToString(value);
				}
			}
			

			private esProduct entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ProductMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ProductQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ProductQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ProductQuery query;		
	}



	[Serializable]
	abstract public partial class esProductCollection : esEntityCollection<Product>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ProductMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ProductCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ProductQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ProductQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ProductQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ProductQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ProductQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ProductQuery)query);
		}

		#endregion
		
		private ProductQuery query;
	}



	[Serializable]
	abstract public partial class esProductQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ProductMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "StoreId": return this.StoreId;
				case "IsActive": return this.IsActive;
				case "Slug": return this.Slug;
				case "Name": return this.Name;
				case "Sku": return this.Sku;
				case "SpecialNotes": return this.SpecialNotes;
				case "Price": return this.Price;
				case "Weight": return this.Weight;
				case "DeliveryMethodId": return this.DeliveryMethodId;
				case "ShippingAdditionalFeePerItem": return this.ShippingAdditionalFeePerItem;
				case "QuantityWidget": return this.QuantityWidget;
				case "QuantityOptions": return this.QuantityOptions;
				case "InventoryIsEnabled": return this.InventoryIsEnabled;
				case "InventoryAllowNegativeStockLevel": return this.InventoryAllowNegativeStockLevel;
				case "InventoryQtyInStock": return this.InventoryQtyInStock;
				case "InventoryQtyLowThreshold": return this.InventoryQtyLowThreshold;
				case "DigitalFileDisplayName": return this.DigitalFileDisplayName;
				case "DigitalFilename": return this.DigitalFilename;
				case "SeoTitle": return this.SeoTitle;
				case "SeoDescription": return this.SeoDescription;
				case "SeoKeywords": return this.SeoKeywords;
				case "CreatedOn": return this.CreatedOn;
				case "ModifiedOn": return this.ModifiedOn;
				case "IsTaxable": return this.IsTaxable;
				case "CheckoutAssignRoleInfoJson": return this.CheckoutAssignRoleInfoJson;
				case "IsPriceDisplayed": return this.IsPriceDisplayed;
				case "IsAvailableForPurchase": return this.IsAvailableForPurchase;
				case "Length": return this.Length;
				case "Width": return this.Width;
				case "Height": return this.Height;
				case "ViewPermissions": return this.ViewPermissions;
				case "CheckoutPermissions": return this.CheckoutPermissions;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem IsActive
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.IsActive, esSystemType.Boolean); }
		} 
		
		public esQueryItem Slug
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Slug, esSystemType.String); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Sku
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Sku, esSystemType.String); }
		} 
		
		public esQueryItem SpecialNotes
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.SpecialNotes, esSystemType.String); }
		} 
		
		public esQueryItem Price
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Price, esSystemType.Decimal); }
		} 
		
		public esQueryItem Weight
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Weight, esSystemType.Decimal); }
		} 
		
		public esQueryItem DeliveryMethodId
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.DeliveryMethodId, esSystemType.Int16); }
		} 
		
		public esQueryItem ShippingAdditionalFeePerItem
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.ShippingAdditionalFeePerItem, esSystemType.Decimal); }
		} 
		
		public esQueryItem QuantityWidget
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.QuantityWidget, esSystemType.String); }
		} 
		
		public esQueryItem QuantityOptions
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.QuantityOptions, esSystemType.String); }
		} 
		
		public esQueryItem InventoryIsEnabled
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.InventoryIsEnabled, esSystemType.Boolean); }
		} 
		
		public esQueryItem InventoryAllowNegativeStockLevel
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.InventoryAllowNegativeStockLevel, esSystemType.Boolean); }
		} 
		
		public esQueryItem InventoryQtyInStock
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.InventoryQtyInStock, esSystemType.Int32); }
		} 
		
		public esQueryItem InventoryQtyLowThreshold
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.InventoryQtyLowThreshold, esSystemType.Int32); }
		} 
		
		public esQueryItem DigitalFileDisplayName
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.DigitalFileDisplayName, esSystemType.String); }
		} 
		
		public esQueryItem DigitalFilename
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.DigitalFilename, esSystemType.String); }
		} 
		
		public esQueryItem SeoTitle
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.SeoTitle, esSystemType.String); }
		} 
		
		public esQueryItem SeoDescription
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.SeoDescription, esSystemType.String); }
		} 
		
		public esQueryItem SeoKeywords
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.SeoKeywords, esSystemType.String); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem ModifiedOn
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.ModifiedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem IsTaxable
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.IsTaxable, esSystemType.Boolean); }
		} 
		
		public esQueryItem CheckoutAssignRoleInfoJson
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.CheckoutAssignRoleInfoJson, esSystemType.String); }
		} 
		
		public esQueryItem IsPriceDisplayed
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.IsPriceDisplayed, esSystemType.Boolean); }
		} 
		
		public esQueryItem IsAvailableForPurchase
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.IsAvailableForPurchase, esSystemType.Boolean); }
		} 
		
		public esQueryItem Length
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Length, esSystemType.Decimal); }
		} 
		
		public esQueryItem Width
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Width, esSystemType.Decimal); }
		} 
		
		public esQueryItem Height
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.Height, esSystemType.Decimal); }
		} 
		
		public esQueryItem ViewPermissions
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.ViewPermissions, esSystemType.String); }
		} 
		
		public esQueryItem CheckoutPermissions
		{
			get { return new esQueryItem(this, ProductMetadata.ColumnNames.CheckoutPermissions, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class Product : esProduct
	{

		#region CartItemCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_CartItemCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.CartItemCollectionByProductId_Delegate;
				map.PropertyName = "CartItemCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void CartItemCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			CartItemQuery me = data.You != null ? data.You as CartItemQuery : new CartItemQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_CartItems_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public CartItemCollection CartItemCollectionByProductId
		{
			get
			{
				if(this._CartItemCollectionByProductId == null)
				{
					this._CartItemCollectionByProductId = new CartItemCollection();
					this._CartItemCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CartItemCollectionByProductId", this._CartItemCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._CartItemCollectionByProductId.Query.Where(this._CartItemCollectionByProductId.Query.ProductId == this.Id);
							this._CartItemCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._CartItemCollectionByProductId.fks.Add(CartItemMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._CartItemCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CartItemCollectionByProductId != null) 
				{ 
					this.RemovePostSave("CartItemCollectionByProductId"); 
					this._CartItemCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private CartItemCollection _CartItemCollectionByProductId;
		#endregion

		#region OrderItemCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_OrderItemCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.OrderItemCollectionByProductId_Delegate;
				map.PropertyName = "OrderItemCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void OrderItemCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			OrderItemQuery me = data.You != null ? data.You as OrderItemQuery : new OrderItemQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_OrderItem_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public OrderItemCollection OrderItemCollectionByProductId
		{
			get
			{
				if(this._OrderItemCollectionByProductId == null)
				{
					this._OrderItemCollectionByProductId = new OrderItemCollection();
					this._OrderItemCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("OrderItemCollectionByProductId", this._OrderItemCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._OrderItemCollectionByProductId.Query.Where(this._OrderItemCollectionByProductId.Query.ProductId == this.Id);
							this._OrderItemCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._OrderItemCollectionByProductId.fks.Add(OrderItemMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._OrderItemCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._OrderItemCollectionByProductId != null) 
				{ 
					this.RemovePostSave("OrderItemCollectionByProductId"); 
					this._OrderItemCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private OrderItemCollection _OrderItemCollectionByProductId;
		#endregion

		#region UpToCategoryCollectionByProductCategory - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public CategoryCollection UpToCategoryCollectionByProductCategory
		{
			get
			{
				if(this._UpToCategoryCollectionByProductCategory == null)
				{
					this._UpToCategoryCollectionByProductCategory = new CategoryCollection();
					this._UpToCategoryCollectionByProductCategory.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToCategoryCollectionByProductCategory", this._UpToCategoryCollectionByProductCategory);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						CategoryQuery m = new CategoryQuery("m");
						ProductCategoryQuery j = new ProductCategoryQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.CategoryId);
                        m.Where(j.ProductId == this.Id);

						this._UpToCategoryCollectionByProductCategory.Load(m);
					}
				}

				return this._UpToCategoryCollectionByProductCategory;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToCategoryCollectionByProductCategory != null) 
				{ 
					this.RemovePostSave("UpToCategoryCollectionByProductCategory"); 
					this._UpToCategoryCollectionByProductCategory = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Product
		/// </summary>
		public void AssociateCategoryCollectionByProductCategory(Category entity)
		{
			if (this._ProductCategoryCollection == null)
			{
				this._ProductCategoryCollection = new ProductCategoryCollection();
				this._ProductCategoryCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("ProductCategoryCollection", this._ProductCategoryCollection);
			}

			ProductCategory obj = this._ProductCategoryCollection.AddNew();
			obj.ProductId = this.Id;
			obj.CategoryId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Product
		/// </summary>
		public void DissociateCategoryCollectionByProductCategory(Category entity)
		{
			if (this._ProductCategoryCollection == null)
			{
				this._ProductCategoryCollection = new ProductCategoryCollection();
				this._ProductCategoryCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("ProductCategoryCollection", this._ProductCategoryCollection);
			}

			ProductCategory obj = this._ProductCategoryCollection.AddNew();
			obj.ProductId = this.Id;
            obj.CategoryId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private CategoryCollection _UpToCategoryCollectionByProductCategory;
		private ProductCategoryCollection _ProductCategoryCollection;
		#endregion

		#region ProductCategoryCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductCategoryCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.ProductCategoryCollectionByProductId_Delegate;
				map.PropertyName = "ProductCategoryCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductCategoryCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			ProductCategoryQuery me = data.You != null ? data.You as ProductCategoryQuery : new ProductCategoryQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public ProductCategoryCollection ProductCategoryCollectionByProductId
		{
			get
			{
				if(this._ProductCategoryCollectionByProductId == null)
				{
					this._ProductCategoryCollectionByProductId = new ProductCategoryCollection();
					this._ProductCategoryCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductCategoryCollectionByProductId", this._ProductCategoryCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductCategoryCollectionByProductId.Query.Where(this._ProductCategoryCollectionByProductId.Query.ProductId == this.Id);
							this._ProductCategoryCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductCategoryCollectionByProductId.fks.Add(ProductCategoryMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._ProductCategoryCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductCategoryCollectionByProductId != null) 
				{ 
					this.RemovePostSave("ProductCategoryCollectionByProductId"); 
					this._ProductCategoryCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private ProductCategoryCollection _ProductCategoryCollectionByProductId;
		#endregion

		#region ProductDescriptorCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductDescriptorCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.ProductDescriptorCollectionByProductId_Delegate;
				map.PropertyName = "ProductDescriptorCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductDescriptorCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			ProductDescriptorQuery me = data.You != null ? data.You as ProductDescriptorQuery : new ProductDescriptorQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductDescriptor_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public ProductDescriptorCollection ProductDescriptorCollectionByProductId
		{
			get
			{
				if(this._ProductDescriptorCollectionByProductId == null)
				{
					this._ProductDescriptorCollectionByProductId = new ProductDescriptorCollection();
					this._ProductDescriptorCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductDescriptorCollectionByProductId", this._ProductDescriptorCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductDescriptorCollectionByProductId.Query.Where(this._ProductDescriptorCollectionByProductId.Query.ProductId == this.Id);
							this._ProductDescriptorCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductDescriptorCollectionByProductId.fks.Add(ProductDescriptorMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._ProductDescriptorCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductDescriptorCollectionByProductId != null) 
				{ 
					this.RemovePostSave("ProductDescriptorCollectionByProductId"); 
					this._ProductDescriptorCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private ProductDescriptorCollection _ProductDescriptorCollectionByProductId;
		#endregion

		#region ProductFieldCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductFieldCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.ProductFieldCollectionByProductId_Delegate;
				map.PropertyName = "ProductFieldCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductFieldCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			ProductFieldQuery me = data.You != null ? data.You as ProductFieldQuery : new ProductFieldQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductAttribute_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public ProductFieldCollection ProductFieldCollectionByProductId
		{
			get
			{
				if(this._ProductFieldCollectionByProductId == null)
				{
					this._ProductFieldCollectionByProductId = new ProductFieldCollection();
					this._ProductFieldCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductFieldCollectionByProductId", this._ProductFieldCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductFieldCollectionByProductId.Query.Where(this._ProductFieldCollectionByProductId.Query.ProductId == this.Id);
							this._ProductFieldCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductFieldCollectionByProductId.fks.Add(ProductFieldMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._ProductFieldCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductFieldCollectionByProductId != null) 
				{ 
					this.RemovePostSave("ProductFieldCollectionByProductId"); 
					this._ProductFieldCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private ProductFieldCollection _ProductFieldCollectionByProductId;
		#endregion

		#region ProductPhotoCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductPhotoCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.ProductPhotoCollectionByProductId_Delegate;
				map.PropertyName = "ProductPhotoCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductPhotoCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			ProductPhotoQuery me = data.You != null ? data.You as ProductPhotoQuery : new ProductPhotoQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductPhoto_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public ProductPhotoCollection ProductPhotoCollectionByProductId
		{
			get
			{
				if(this._ProductPhotoCollectionByProductId == null)
				{
					this._ProductPhotoCollectionByProductId = new ProductPhotoCollection();
					this._ProductPhotoCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductPhotoCollectionByProductId", this._ProductPhotoCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductPhotoCollectionByProductId.Query.Where(this._ProductPhotoCollectionByProductId.Query.ProductId == this.Id);
							this._ProductPhotoCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductPhotoCollectionByProductId.fks.Add(ProductPhotoMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._ProductPhotoCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductPhotoCollectionByProductId != null) 
				{ 
					this.RemovePostSave("ProductPhotoCollectionByProductId"); 
					this._ProductPhotoCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private ProductPhotoCollection _ProductPhotoCollectionByProductId;
		#endregion

		#region ProductQuantityPriceCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductQuantityPriceCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.ProductQuantityPriceCollectionByProductId_Delegate;
				map.PropertyName = "ProductQuantityPriceCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductQuantityPriceCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			ProductQuantityPriceQuery me = data.You != null ? data.You as ProductQuantityPriceQuery : new ProductQuantityPriceQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductQuantityPrice_DNNspot_Store_ProductQuantityPrice
		/// </summary>

		[XmlIgnore]
		public ProductQuantityPriceCollection ProductQuantityPriceCollectionByProductId
		{
			get
			{
				if(this._ProductQuantityPriceCollectionByProductId == null)
				{
					this._ProductQuantityPriceCollectionByProductId = new ProductQuantityPriceCollection();
					this._ProductQuantityPriceCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductQuantityPriceCollectionByProductId", this._ProductQuantityPriceCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductQuantityPriceCollectionByProductId.Query.Where(this._ProductQuantityPriceCollectionByProductId.Query.ProductId == this.Id);
							this._ProductQuantityPriceCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductQuantityPriceCollectionByProductId.fks.Add(ProductQuantityPriceMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._ProductQuantityPriceCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductQuantityPriceCollectionByProductId != null) 
				{ 
					this.RemovePostSave("ProductQuantityPriceCollectionByProductId"); 
					this._ProductQuantityPriceCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private ProductQuantityPriceCollection _ProductQuantityPriceCollectionByProductId;
		#endregion

		#region RelatedProductCollectionByProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_RelatedProductCollectionByProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.RelatedProductCollectionByProductId_Delegate;
				map.PropertyName = "RelatedProductCollectionByProductId";
				map.MyColumnName = "ProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void RelatedProductCollectionByProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			RelatedProductQuery me = data.You != null ? data.You as RelatedProductQuery : new RelatedProductQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.ProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_RelatedProduct_DNNspot_Store_Product
		/// </summary>

		[XmlIgnore]
		public RelatedProductCollection RelatedProductCollectionByProductId
		{
			get
			{
				if(this._RelatedProductCollectionByProductId == null)
				{
					this._RelatedProductCollectionByProductId = new RelatedProductCollection();
					this._RelatedProductCollectionByProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("RelatedProductCollectionByProductId", this._RelatedProductCollectionByProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._RelatedProductCollectionByProductId.Query.Where(this._RelatedProductCollectionByProductId.Query.ProductId == this.Id);
							this._RelatedProductCollectionByProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._RelatedProductCollectionByProductId.fks.Add(RelatedProductMetadata.ColumnNames.ProductId, this.Id);
					}
				}

				return this._RelatedProductCollectionByProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._RelatedProductCollectionByProductId != null) 
				{ 
					this.RemovePostSave("RelatedProductCollectionByProductId"); 
					this._RelatedProductCollectionByProductId = null;
					
				} 
			} 			
		}
			
		
		private RelatedProductCollection _RelatedProductCollectionByProductId;
		#endregion

		#region RelatedProductCollectionByRelatedProductId - Zero To Many
		
		static public esPrefetchMap Prefetch_RelatedProductCollectionByRelatedProductId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Product.RelatedProductCollectionByRelatedProductId_Delegate;
				map.PropertyName = "RelatedProductCollectionByRelatedProductId";
				map.MyColumnName = "RelatedProductId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void RelatedProductCollectionByRelatedProductId_Delegate(esPrefetchParameters data)
		{
			ProductQuery parent = new ProductQuery(data.NextAlias());

			RelatedProductQuery me = data.You != null ? data.You as RelatedProductQuery : new RelatedProductQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.RelatedProductId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_RelatedProduct_DNNspot_Store_Product1
		/// </summary>

		[XmlIgnore]
		public RelatedProductCollection RelatedProductCollectionByRelatedProductId
		{
			get
			{
				if(this._RelatedProductCollectionByRelatedProductId == null)
				{
					this._RelatedProductCollectionByRelatedProductId = new RelatedProductCollection();
					this._RelatedProductCollectionByRelatedProductId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("RelatedProductCollectionByRelatedProductId", this._RelatedProductCollectionByRelatedProductId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._RelatedProductCollectionByRelatedProductId.Query.Where(this._RelatedProductCollectionByRelatedProductId.Query.RelatedProductId == this.Id);
							this._RelatedProductCollectionByRelatedProductId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._RelatedProductCollectionByRelatedProductId.fks.Add(RelatedProductMetadata.ColumnNames.RelatedProductId, this.Id);
					}
				}

				return this._RelatedProductCollectionByRelatedProductId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._RelatedProductCollectionByRelatedProductId != null) 
				{ 
					this.RemovePostSave("RelatedProductCollectionByRelatedProductId"); 
					this._RelatedProductCollectionByRelatedProductId = null;
					
				} 
			} 			
		}
			
		
		private RelatedProductCollection _RelatedProductCollectionByRelatedProductId;
		#endregion

				
		#region UpToDeliveryMethodByDeliveryMethodId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Product_DNNspot_Store_DeliveryMethod
		/// </summary>

		[XmlIgnore]
					
		public DeliveryMethod UpToDeliveryMethodByDeliveryMethodId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToDeliveryMethodByDeliveryMethodId == null && DeliveryMethodId != null)
				{
					this._UpToDeliveryMethodByDeliveryMethodId = new DeliveryMethod();
					this._UpToDeliveryMethodByDeliveryMethodId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToDeliveryMethodByDeliveryMethodId", this._UpToDeliveryMethodByDeliveryMethodId);
					this._UpToDeliveryMethodByDeliveryMethodId.Query.Where(this._UpToDeliveryMethodByDeliveryMethodId.Query.Id == this.DeliveryMethodId);
					this._UpToDeliveryMethodByDeliveryMethodId.Query.Load();
				}	
				return this._UpToDeliveryMethodByDeliveryMethodId;
			}
			
			set
			{
				this.RemovePreSave("UpToDeliveryMethodByDeliveryMethodId");
				

				if(value == null)
				{
					this.DeliveryMethodId = null;
					this._UpToDeliveryMethodByDeliveryMethodId = null;
				}
				else
				{
					this.DeliveryMethodId = value.Id;
					this._UpToDeliveryMethodByDeliveryMethodId = value;
					this.SetPreSave("UpToDeliveryMethodByDeliveryMethodId", this._UpToDeliveryMethodByDeliveryMethodId);
				}
				
			}
		}
		#endregion
		

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "CartItemCollectionByProductId":
					coll = this.CartItemCollectionByProductId;
					break;
				case "OrderItemCollectionByProductId":
					coll = this.OrderItemCollectionByProductId;
					break;
				case "ProductCategoryCollectionByProductId":
					coll = this.ProductCategoryCollectionByProductId;
					break;
				case "ProductDescriptorCollectionByProductId":
					coll = this.ProductDescriptorCollectionByProductId;
					break;
				case "ProductFieldCollectionByProductId":
					coll = this.ProductFieldCollectionByProductId;
					break;
				case "ProductPhotoCollectionByProductId":
					coll = this.ProductPhotoCollectionByProductId;
					break;
				case "ProductQuantityPriceCollectionByProductId":
					coll = this.ProductQuantityPriceCollectionByProductId;
					break;
				case "RelatedProductCollectionByProductId":
					coll = this.RelatedProductCollectionByProductId;
					break;
				case "RelatedProductCollectionByRelatedProductId":
					coll = this.RelatedProductCollectionByRelatedProductId;
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
			
			props.Add(new esPropertyDescriptor(this, "CartItemCollectionByProductId", typeof(CartItemCollection), new CartItem()));
			props.Add(new esPropertyDescriptor(this, "OrderItemCollectionByProductId", typeof(OrderItemCollection), new OrderItem()));
			props.Add(new esPropertyDescriptor(this, "ProductCategoryCollectionByProductId", typeof(ProductCategoryCollection), new ProductCategory()));
			props.Add(new esPropertyDescriptor(this, "ProductDescriptorCollectionByProductId", typeof(ProductDescriptorCollection), new ProductDescriptor()));
			props.Add(new esPropertyDescriptor(this, "ProductFieldCollectionByProductId", typeof(ProductFieldCollection), new ProductField()));
			props.Add(new esPropertyDescriptor(this, "ProductPhotoCollectionByProductId", typeof(ProductPhotoCollection), new ProductPhoto()));
			props.Add(new esPropertyDescriptor(this, "ProductQuantityPriceCollectionByProductId", typeof(ProductQuantityPriceCollection), new ProductQuantityPrice()));
			props.Add(new esPropertyDescriptor(this, "RelatedProductCollectionByProductId", typeof(RelatedProductCollection), new RelatedProduct()));
			props.Add(new esPropertyDescriptor(this, "RelatedProductCollectionByRelatedProductId", typeof(RelatedProductCollection), new RelatedProduct()));
		
			return props;
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
			if(this._CartItemCollectionByProductId != null)
			{
				Apply(this._CartItemCollectionByProductId, "ProductId", this.Id);
			}
			if(this._OrderItemCollectionByProductId != null)
			{
				Apply(this._OrderItemCollectionByProductId, "ProductId", this.Id);
			}
			if(this._ProductCategoryCollection != null)
			{
				Apply(this._ProductCategoryCollection, "ProductId", this.Id);
			}
			if(this._ProductCategoryCollectionByProductId != null)
			{
				Apply(this._ProductCategoryCollectionByProductId, "ProductId", this.Id);
			}
			if(this._ProductDescriptorCollectionByProductId != null)
			{
				Apply(this._ProductDescriptorCollectionByProductId, "ProductId", this.Id);
			}
			if(this._ProductFieldCollectionByProductId != null)
			{
				Apply(this._ProductFieldCollectionByProductId, "ProductId", this.Id);
			}
			if(this._ProductPhotoCollectionByProductId != null)
			{
				Apply(this._ProductPhotoCollectionByProductId, "ProductId", this.Id);
			}
			if(this._ProductQuantityPriceCollectionByProductId != null)
			{
				Apply(this._ProductQuantityPriceCollectionByProductId, "ProductId", this.Id);
			}
			if(this._RelatedProductCollectionByProductId != null)
			{
				Apply(this._RelatedProductCollectionByProductId, "ProductId", this.Id);
			}
			if(this._RelatedProductCollectionByRelatedProductId != null)
			{
				Apply(this._RelatedProductCollectionByRelatedProductId, "RelatedProductId", this.Id);
			}
		}
		
	}
	



	[Serializable]
	public partial class ProductMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ProductMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ProductMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.StoreId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Slug, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.Slug;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Name, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 250;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Sku, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.Sku;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.SpecialNotes, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.SpecialNotes;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Price, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductMetadata.PropertyNames.Price;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Weight, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductMetadata.PropertyNames.Weight;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.DeliveryMethodId, 9, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ProductMetadata.PropertyNames.DeliveryMethodId;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((1))";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.ShippingAdditionalFeePerItem, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductMetadata.PropertyNames.ShippingAdditionalFeePerItem;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.QuantityWidget, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.QuantityWidget;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('textbox')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.QuantityOptions, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.QuantityOptions;
			c.CharacterMaxLength = 2000;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.InventoryIsEnabled, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductMetadata.PropertyNames.InventoryIsEnabled;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.InventoryAllowNegativeStockLevel, 14, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductMetadata.PropertyNames.InventoryAllowNegativeStockLevel;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.InventoryQtyInStock, 15, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductMetadata.PropertyNames.InventoryQtyInStock;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.InventoryQtyLowThreshold, 16, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ProductMetadata.PropertyNames.InventoryQtyLowThreshold;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.DigitalFileDisplayName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.DigitalFileDisplayName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.DigitalFilename, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.DigitalFilename;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.SeoTitle, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.SeoTitle;
			c.CharacterMaxLength = 300;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.SeoDescription, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.SeoDescription;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.SeoKeywords, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.SeoKeywords;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.CreatedOn, 22, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductMetadata.PropertyNames.CreatedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.ModifiedOn, 23, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ProductMetadata.PropertyNames.ModifiedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.IsTaxable, 24, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductMetadata.PropertyNames.IsTaxable;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.CheckoutAssignRoleInfoJson, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.CheckoutAssignRoleInfoJson;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.IsPriceDisplayed, 26, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductMetadata.PropertyNames.IsPriceDisplayed;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.IsAvailableForPurchase, 27, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ProductMetadata.PropertyNames.IsAvailableForPurchase;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Length, 28, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductMetadata.PropertyNames.Length;
			c.NumericPrecision = 10;
			c.NumericScale = 8;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Width, 29, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductMetadata.PropertyNames.Width;
			c.NumericPrecision = 10;
			c.NumericScale = 8;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.Height, 30, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ProductMetadata.PropertyNames.Height;
			c.NumericPrecision = 10;
			c.NumericScale = 8;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.ViewPermissions, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.ViewPermissions;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('-1')";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ProductMetadata.ColumnNames.CheckoutPermissions, 32, typeof(System.String), esSystemType.String);
			c.PropertyName = ProductMetadata.PropertyNames.CheckoutPermissions;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('-1')";
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ProductMetadata Meta()
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
			 public const string IsActive = "IsActive";
			 public const string Slug = "Slug";
			 public const string Name = "Name";
			 public const string Sku = "Sku";
			 public const string SpecialNotes = "SpecialNotes";
			 public const string Price = "Price";
			 public const string Weight = "Weight";
			 public const string DeliveryMethodId = "DeliveryMethodId";
			 public const string ShippingAdditionalFeePerItem = "ShippingAdditionalFeePerItem";
			 public const string QuantityWidget = "QuantityWidget";
			 public const string QuantityOptions = "QuantityOptions";
			 public const string InventoryIsEnabled = "InventoryIsEnabled";
			 public const string InventoryAllowNegativeStockLevel = "InventoryAllowNegativeStockLevel";
			 public const string InventoryQtyInStock = "InventoryQtyInStock";
			 public const string InventoryQtyLowThreshold = "InventoryQtyLowThreshold";
			 public const string DigitalFileDisplayName = "DigitalFileDisplayName";
			 public const string DigitalFilename = "DigitalFilename";
			 public const string SeoTitle = "SeoTitle";
			 public const string SeoDescription = "SeoDescription";
			 public const string SeoKeywords = "SeoKeywords";
			 public const string CreatedOn = "CreatedOn";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string IsTaxable = "IsTaxable";
			 public const string CheckoutAssignRoleInfoJson = "CheckoutAssignRoleInfoJson";
			 public const string IsPriceDisplayed = "IsPriceDisplayed";
			 public const string IsAvailableForPurchase = "IsAvailableForPurchase";
			 public const string Length = "Length";
			 public const string Width = "Width";
			 public const string Height = "Height";
			 public const string ViewPermissions = "ViewPermissions";
			 public const string CheckoutPermissions = "CheckoutPermissions";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string StoreId = "StoreId";
			 public const string IsActive = "IsActive";
			 public const string Slug = "Slug";
			 public const string Name = "Name";
			 public const string Sku = "Sku";
			 public const string SpecialNotes = "SpecialNotes";
			 public const string Price = "Price";
			 public const string Weight = "Weight";
			 public const string DeliveryMethodId = "DeliveryMethodId";
			 public const string ShippingAdditionalFeePerItem = "ShippingAdditionalFeePerItem";
			 public const string QuantityWidget = "QuantityWidget";
			 public const string QuantityOptions = "QuantityOptions";
			 public const string InventoryIsEnabled = "InventoryIsEnabled";
			 public const string InventoryAllowNegativeStockLevel = "InventoryAllowNegativeStockLevel";
			 public const string InventoryQtyInStock = "InventoryQtyInStock";
			 public const string InventoryQtyLowThreshold = "InventoryQtyLowThreshold";
			 public const string DigitalFileDisplayName = "DigitalFileDisplayName";
			 public const string DigitalFilename = "DigitalFilename";
			 public const string SeoTitle = "SeoTitle";
			 public const string SeoDescription = "SeoDescription";
			 public const string SeoKeywords = "SeoKeywords";
			 public const string CreatedOn = "CreatedOn";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string IsTaxable = "IsTaxable";
			 public const string CheckoutAssignRoleInfoJson = "CheckoutAssignRoleInfoJson";
			 public const string IsPriceDisplayed = "IsPriceDisplayed";
			 public const string IsAvailableForPurchase = "IsAvailableForPurchase";
			 public const string Length = "Length";
			 public const string Width = "Width";
			 public const string Height = "Height";
			 public const string ViewPermissions = "ViewPermissions";
			 public const string CheckoutPermissions = "CheckoutPermissions";
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
			lock (typeof(ProductMetadata))
			{
				if(ProductMetadata.mapDelegates == null)
				{
					ProductMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ProductMetadata.meta == null)
				{
					ProductMetadata.meta = new ProductMetadata();
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
				meta.AddTypeMap("IsActive", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Slug", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Sku", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SpecialNotes", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Price", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Weight", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("DeliveryMethodId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("ShippingAdditionalFeePerItem", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("QuantityWidget", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("QuantityOptions", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("InventoryIsEnabled", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InventoryAllowNegativeStockLevel", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("InventoryQtyInStock", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("InventoryQtyLowThreshold", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DigitalFileDisplayName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DigitalFilename", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SeoTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeoDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeoKeywords", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ModifiedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("IsTaxable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("CheckoutAssignRoleInfoJson", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsPriceDisplayed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("IsAvailableForPurchase", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Length", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Width", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Height", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ViewPermissions", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CheckoutPermissions", new esTypeMap("varchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Product";
					meta.Destination = objectQualifier + "DNNspot_Store_Product";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ProductInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ProductUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ProductDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ProductLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ProductLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Product";
					meta.Destination = "DNNspot_Store_Product";
									
					meta.spInsert = "proc_DNNspot_Store_ProductInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ProductUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ProductDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ProductLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ProductLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ProductMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
