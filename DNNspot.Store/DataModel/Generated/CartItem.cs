
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
	/// Encapsulates the 'DNNspot_Store_CartItem' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(CartItem))]	
	[XmlType("CartItem")]
	[Table(Name="CartItem")]
	public partial class CartItem : esCartItem
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new CartItem();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new CartItem();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new CartItem();
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
		public override System.Guid? CartId
		{
			get { return base.CartId;  }
			set { base.CartId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int32? ProductId
		{
			get { return base.ProductId;  }
			set { base.ProductId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int32? Quantity
		{
			get { return base.Quantity;  }
			set { base.Quantity = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ProductFieldData
		{
			get { return base.ProductFieldData;  }
			set { base.ProductFieldData = value; }
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


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("CartItemCollection")]
	public partial class CartItemCollection : esCartItemCollection, IEnumerable<CartItem>
	{
		public CartItem FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(CartItem))]
		public class CartItemCollectionWCFPacket : esCollectionWCFPacket<CartItemCollection>
		{
			public static implicit operator CartItemCollection(CartItemCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CartItemCollectionWCFPacket(CartItemCollection collection)
			{
				return new CartItemCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CartItemQuery : esCartItemQuery
	{
		public CartItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CartItemQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CartItemQuery query)
		{
			return CartItemQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CartItemQuery(string query)
		{
			return (CartItemQuery)CartItemQuery.SerializeHelper.FromXml(query, typeof(CartItemQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCartItem : esEntity
	{
		public esCartItem()
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
			CartItemQuery query = new CartItemQuery();
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
		/// Maps to DNNspot_Store_CartItem.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(CartItemMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(CartItemMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CartItemMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_CartItem.CartId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Guid? CartId
		{
			get
			{
				return base.GetSystemGuid(CartItemMetadata.ColumnNames.CartId);
			}
			
			set
			{
				if(base.SetSystemGuid(CartItemMetadata.ColumnNames.CartId, value))
				{
					this._UpToCartByCartId = null;
					this.OnPropertyChanged("UpToCartByCartId");
					OnPropertyChanged(CartItemMetadata.PropertyNames.CartId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_CartItem.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(CartItemMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(CartItemMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(CartItemMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_CartItem.Quantity
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Quantity
		{
			get
			{
				return base.GetSystemInt32(CartItemMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				if(base.SetSystemInt32(CartItemMetadata.ColumnNames.Quantity, value))
				{
					OnPropertyChanged(CartItemMetadata.PropertyNames.Quantity);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_CartItem.ProductFieldData
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProductFieldData
		{
			get
			{
				return base.GetSystemString(CartItemMetadata.ColumnNames.ProductFieldData);
			}
			
			set
			{
				if(base.SetSystemString(CartItemMetadata.ColumnNames.ProductFieldData, value))
				{
					OnPropertyChanged(CartItemMetadata.PropertyNames.ProductFieldData);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_CartItem.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(CartItemMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(CartItemMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(CartItemMetadata.PropertyNames.CreatedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_CartItem.ModifiedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ModifiedOn
		{
			get
			{
				return base.GetSystemDateTime(CartItemMetadata.ColumnNames.ModifiedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(CartItemMetadata.ColumnNames.ModifiedOn, value))
				{
					OnPropertyChanged(CartItemMetadata.PropertyNames.ModifiedOn);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Cart _UpToCartByCartId;
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
						case "CartId": this.str().CartId = (string)value; break;							
						case "ProductId": this.str().ProductId = (string)value; break;							
						case "Quantity": this.str().Quantity = (string)value; break;							
						case "ProductFieldData": this.str().ProductFieldData = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;							
						case "ModifiedOn": this.str().ModifiedOn = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(CartItemMetadata.PropertyNames.Id);
							break;
						
						case "CartId":
						
							if (value == null || value is System.Guid)
								this.CartId = (System.Guid?)value;
								OnPropertyChanged(CartItemMetadata.PropertyNames.CartId);
							break;
						
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(CartItemMetadata.PropertyNames.ProductId);
							break;
						
						case "Quantity":
						
							if (value == null || value is System.Int32)
								this.Quantity = (System.Int32?)value;
								OnPropertyChanged(CartItemMetadata.PropertyNames.Quantity);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(CartItemMetadata.PropertyNames.CreatedOn);
							break;
						
						case "ModifiedOn":
						
							if (value == null || value is System.DateTime)
								this.ModifiedOn = (System.DateTime?)value;
								OnPropertyChanged(CartItemMetadata.PropertyNames.ModifiedOn);
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
			public esStrings(esCartItem entity)
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
				
			public System.String CartId
			{
				get
				{
					System.Guid? data = entity.CartId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CartId = null;
					else entity.CartId = new Guid(value);
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
				
			public System.String Quantity
			{
				get
				{
					System.Int32? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToInt32(value);
				}
			}
				
			public System.String ProductFieldData
			{
				get
				{
					System.String data = entity.ProductFieldData;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductFieldData = null;
					else entity.ProductFieldData = Convert.ToString(value);
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
			

			private esCartItem entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CartItemMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CartItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CartItemQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CartItemQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CartItemQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CartItemQuery query;		
	}



	[Serializable]
	abstract public partial class esCartItemCollection : esEntityCollection<CartItem>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CartItemMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CartItemCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CartItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CartItemQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CartItemQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CartItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CartItemQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CartItemQuery)query);
		}

		#endregion
		
		private CartItemQuery query;
	}



	[Serializable]
	abstract public partial class esCartItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CartItemMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "CartId": return this.CartId;
				case "ProductId": return this.ProductId;
				case "Quantity": return this.Quantity;
				case "ProductFieldData": return this.ProductFieldData;
				case "CreatedOn": return this.CreatedOn;
				case "ModifiedOn": return this.ModifiedOn;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CartItemMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem CartId
		{
			get { return new esQueryItem(this, CartItemMetadata.ColumnNames.CartId, esSystemType.Guid); }
		} 
		
		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, CartItemMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem Quantity
		{
			get { return new esQueryItem(this, CartItemMetadata.ColumnNames.Quantity, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductFieldData
		{
			get { return new esQueryItem(this, CartItemMetadata.ColumnNames.ProductFieldData, esSystemType.String); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, CartItemMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem ModifiedOn
		{
			get { return new esQueryItem(this, CartItemMetadata.ColumnNames.ModifiedOn, esSystemType.DateTime); }
		} 
		
		#endregion
		
	}


	
	public partial class CartItem : esCartItem
	{

				
		#region UpToCartByCartId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_CartItems_DNNspot_Store_Cart
		/// </summary>

		[XmlIgnore]
					
		public Cart UpToCartByCartId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToCartByCartId == null && CartId != null)
				{
					this._UpToCartByCartId = new Cart();
					this._UpToCartByCartId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToCartByCartId", this._UpToCartByCartId);
					this._UpToCartByCartId.Query.Where(this._UpToCartByCartId.Query.Id == this.CartId);
					this._UpToCartByCartId.Query.Load();
				}	
				return this._UpToCartByCartId;
			}
			
			set
			{
				this.RemovePreSave("UpToCartByCartId");
				

				if(value == null)
				{
					this.CartId = null;
					this._UpToCartByCartId = null;
				}
				else
				{
					this.CartId = value.Id;
					this._UpToCartByCartId = value;
					this.SetPreSave("UpToCartByCartId", this._UpToCartByCartId);
				}
				
			}
		}
		#endregion
		

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_CartItems_DNNspot_Store_Product
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
	public partial class CartItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CartItemMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CartItemMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CartItemMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartItemMetadata.ColumnNames.CartId, 1, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = CartItemMetadata.PropertyNames.CartId;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartItemMetadata.ColumnNames.ProductId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CartItemMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartItemMetadata.ColumnNames.Quantity, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CartItemMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartItemMetadata.ColumnNames.ProductFieldData, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CartItemMetadata.PropertyNames.ProductFieldData;
			c.CharacterMaxLength = 1073741823;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartItemMetadata.ColumnNames.CreatedOn, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CartItemMetadata.PropertyNames.CreatedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartItemMetadata.ColumnNames.ModifiedOn, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CartItemMetadata.PropertyNames.ModifiedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CartItemMetadata Meta()
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
			 public const string CartId = "CartId";
			 public const string ProductId = "ProductId";
			 public const string Quantity = "Quantity";
			 public const string ProductFieldData = "ProductFieldData";
			 public const string CreatedOn = "CreatedOn";
			 public const string ModifiedOn = "ModifiedOn";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string CartId = "CartId";
			 public const string ProductId = "ProductId";
			 public const string Quantity = "Quantity";
			 public const string ProductFieldData = "ProductFieldData";
			 public const string CreatedOn = "CreatedOn";
			 public const string ModifiedOn = "ModifiedOn";
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
			lock (typeof(CartItemMetadata))
			{
				if(CartItemMetadata.mapDelegates == null)
				{
					CartItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CartItemMetadata.meta == null)
				{
					CartItemMetadata.meta = new CartItemMetadata();
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
				meta.AddTypeMap("CartId", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("ProductId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Quantity", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ProductFieldData", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CreatedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ModifiedOn", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_CartItem";
					meta.Destination = objectQualifier + "DNNspot_Store_CartItem";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_CartItemInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_CartItemUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_CartItemDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_CartItemLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_CartItemLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_CartItem";
					meta.Destination = "DNNspot_Store_CartItem";
									
					meta.spInsert = "proc_DNNspot_Store_CartItemInsert";				
					meta.spUpdate = "proc_DNNspot_Store_CartItemUpdate";		
					meta.spDelete = "proc_DNNspot_Store_CartItemDelete";
					meta.spLoadAll = "proc_DNNspot_Store_CartItemLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_CartItemLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CartItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
