
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
	/// Encapsulates the 'DNNspot_Store_Cart' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Cart))]	
	[XmlType("Cart")]
	[Table(Name="Cart")]
	public partial class Cart : esCart
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Cart();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Guid id)
		{
			var obj = new Cart();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Guid id, esSqlAccessType sqlAccessType)
		{
			var obj = new Cart();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Guid? Id
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

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? UserId
		{
			get { return base.UserId;  }
			set { base.UserId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.DateTime? CreatedOn
		{
			get { return base.CreatedOn;  }
			set { base.CreatedOn = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CreatedByIP
		{
			get { return base.CreatedByIP;  }
			set { base.CreatedByIP = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.DateTime? ModifiedOn
		{
			get { return base.ModifiedOn;  }
			set { base.ModifiedOn = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ModifiedByIP
		{
			get { return base.ModifiedByIP;  }
			set { base.ModifiedByIP = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("CartCollection")]
	public partial class CartCollection : esCartCollection, IEnumerable<Cart>
	{
		public Cart FindByPrimaryKey(System.Guid id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Cart))]
		public class CartCollectionWCFPacket : esCollectionWCFPacket<CartCollection>
		{
			public static implicit operator CartCollection(CartCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CartCollectionWCFPacket(CartCollection collection)
			{
				return new CartCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CartQuery : esCartQuery
	{
		public CartQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CartQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CartQuery query)
		{
			return CartQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CartQuery(string query)
		{
			return (CartQuery)CartQuery.SerializeHelper.FromXml(query, typeof(CartQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCart : esEntity
	{
		public esCart()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Guid id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Guid id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Guid id)
		{
			CartQuery query = new CartQuery();
			query.Where(query.Id == id);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Guid id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id", id);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_Cart.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Guid? Id
		{
			get
			{
				return base.GetSystemGuid(CartMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemGuid(CartMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CartMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Cart.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(CartMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(CartMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(CartMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Cart.UserId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? UserId
		{
			get
			{
				return base.GetSystemInt32(CartMetadata.ColumnNames.UserId);
			}
			
			set
			{
				if(base.SetSystemInt32(CartMetadata.ColumnNames.UserId, value))
				{
					OnPropertyChanged(CartMetadata.PropertyNames.UserId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Cart.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(CartMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(CartMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(CartMetadata.PropertyNames.CreatedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Cart.CreatedByIP
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreatedByIP
		{
			get
			{
				return base.GetSystemString(CartMetadata.ColumnNames.CreatedByIP);
			}
			
			set
			{
				if(base.SetSystemString(CartMetadata.ColumnNames.CreatedByIP, value))
				{
					OnPropertyChanged(CartMetadata.PropertyNames.CreatedByIP);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Cart.ModifiedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ModifiedOn
		{
			get
			{
				return base.GetSystemDateTime(CartMetadata.ColumnNames.ModifiedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(CartMetadata.ColumnNames.ModifiedOn, value))
				{
					OnPropertyChanged(CartMetadata.PropertyNames.ModifiedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Cart.ModifiedByIP
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ModifiedByIP
		{
			get
			{
				return base.GetSystemString(CartMetadata.ColumnNames.ModifiedByIP);
			}
			
			set
			{
				if(base.SetSystemString(CartMetadata.ColumnNames.ModifiedByIP, value))
				{
					OnPropertyChanged(CartMetadata.PropertyNames.ModifiedByIP);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Store _UpToStoreByStoreId;
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
						case "UserId": this.str().UserId = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;							
						case "CreatedByIP": this.str().CreatedByIP = (string)value; break;							
						case "ModifiedOn": this.str().ModifiedOn = (string)value; break;							
						case "ModifiedByIP": this.str().ModifiedByIP = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Guid)
								this.Id = (System.Guid?)value;
								OnPropertyChanged(CartMetadata.PropertyNames.Id);
							break;
						
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(CartMetadata.PropertyNames.StoreId);
							break;
						
						case "UserId":
						
							if (value == null || value is System.Int32)
								this.UserId = (System.Int32?)value;
								OnPropertyChanged(CartMetadata.PropertyNames.UserId);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(CartMetadata.PropertyNames.CreatedOn);
							break;
						
						case "ModifiedOn":
						
							if (value == null || value is System.DateTime)
								this.ModifiedOn = (System.DateTime?)value;
								OnPropertyChanged(CartMetadata.PropertyNames.ModifiedOn);
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
			public esStrings(esCart entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Guid? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = new Guid(value);
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
				
			public System.String UserId
			{
				get
				{
					System.Int32? data = entity.UserId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.UserId = null;
					else entity.UserId = Convert.ToInt32(value);
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
				
			public System.String CreatedByIP
			{
				get
				{
					System.String data = entity.CreatedByIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByIP = null;
					else entity.CreatedByIP = Convert.ToString(value);
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
				
			public System.String ModifiedByIP
			{
				get
				{
					System.String data = entity.ModifiedByIP;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ModifiedByIP = null;
					else entity.ModifiedByIP = Convert.ToString(value);
				}
			}
			

			private esCart entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CartMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CartQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CartQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CartQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CartQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CartQuery query;		
	}



	[Serializable]
	abstract public partial class esCartCollection : esEntityCollection<Cart>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CartMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CartCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CartQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CartQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CartQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CartQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CartQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CartQuery)query);
		}

		#endregion
		
		private CartQuery query;
	}



	[Serializable]
	abstract public partial class esCartQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CartMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "StoreId": return this.StoreId;
				case "UserId": return this.UserId;
				case "CreatedOn": return this.CreatedOn;
				case "CreatedByIP": return this.CreatedByIP;
				case "ModifiedOn": return this.ModifiedOn;
				case "ModifiedByIP": return this.ModifiedByIP;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CartMetadata.ColumnNames.Id, esSystemType.Guid); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, CartMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem UserId
		{
			get { return new esQueryItem(this, CartMetadata.ColumnNames.UserId, esSystemType.Int32); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, CartMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem CreatedByIP
		{
			get { return new esQueryItem(this, CartMetadata.ColumnNames.CreatedByIP, esSystemType.String); }
		} 
		
		public esQueryItem ModifiedOn
		{
			get { return new esQueryItem(this, CartMetadata.ColumnNames.ModifiedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem ModifiedByIP
		{
			get { return new esQueryItem(this, CartMetadata.ColumnNames.ModifiedByIP, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class Cart : esCart
	{

		#region CartItemCollectionByCartId - Zero To Many
		
		static public esPrefetchMap Prefetch_CartItemCollectionByCartId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Cart.CartItemCollectionByCartId_Delegate;
				map.PropertyName = "CartItemCollectionByCartId";
				map.MyColumnName = "CartId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void CartItemCollectionByCartId_Delegate(esPrefetchParameters data)
		{
			CartQuery parent = new CartQuery(data.NextAlias());

			CartItemQuery me = data.You != null ? data.You as CartItemQuery : new CartItemQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.CartId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_CartItems_DNNspot_Store_Cart
		/// </summary>

		[XmlIgnore]
		public CartItemCollection CartItemCollectionByCartId
		{
			get
			{
				if(this._CartItemCollectionByCartId == null)
				{
					this._CartItemCollectionByCartId = new CartItemCollection();
					this._CartItemCollectionByCartId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CartItemCollectionByCartId", this._CartItemCollectionByCartId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._CartItemCollectionByCartId.Query.Where(this._CartItemCollectionByCartId.Query.CartId == this.Id);
							this._CartItemCollectionByCartId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._CartItemCollectionByCartId.fks.Add(CartItemMetadata.ColumnNames.CartId, this.Id);
					}
				}

				return this._CartItemCollectionByCartId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CartItemCollectionByCartId != null) 
				{ 
					this.RemovePostSave("CartItemCollectionByCartId"); 
					this._CartItemCollectionByCartId = null;
					
				} 
			} 			
		}
			
		
		private CartItemCollection _CartItemCollectionByCartId;
		#endregion

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Cart_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
					
		public Store UpToStoreByStoreId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToStoreByStoreId == null && StoreId != null)
				{
					this._UpToStoreByStoreId = new Store();
					this._UpToStoreByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToStoreByStoreId", this._UpToStoreByStoreId);
					this._UpToStoreByStoreId.Query.Where(this._UpToStoreByStoreId.Query.Id == this.StoreId);
					this._UpToStoreByStoreId.Query.Load();
				}	
				return this._UpToStoreByStoreId;
			}
			
			set
			{
				this.RemovePreSave("UpToStoreByStoreId");
				

				if(value == null)
				{
					this.StoreId = null;
					this._UpToStoreByStoreId = null;
				}
				else
				{
					this.StoreId = value.Id;
					this._UpToStoreByStoreId = value;
					this.SetPreSave("UpToStoreByStoreId", this._UpToStoreByStoreId);
				}
				
			}
		}
		#endregion
		

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "CartItemCollectionByCartId":
					coll = this.CartItemCollectionByCartId;
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
			
			props.Add(new esPropertyDescriptor(this, "CartItemCollectionByCartId", typeof(CartItemCollection), new CartItem()));
		
			return props;
		}
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
			if(!this.es.IsDeleted && this._UpToStoreByStoreId != null)
			{
				this.StoreId = this._UpToStoreByStoreId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class CartMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CartMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CartMetadata.ColumnNames.Id, 0, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = CartMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartMetadata.ColumnNames.StoreId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CartMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartMetadata.ColumnNames.UserId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CartMetadata.PropertyNames.UserId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartMetadata.ColumnNames.CreatedOn, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CartMetadata.PropertyNames.CreatedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartMetadata.ColumnNames.CreatedByIP, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CartMetadata.PropertyNames.CreatedByIP;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartMetadata.ColumnNames.ModifiedOn, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CartMetadata.PropertyNames.ModifiedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CartMetadata.ColumnNames.ModifiedByIP, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CartMetadata.PropertyNames.ModifiedByIP;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CartMetadata Meta()
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
			 public const string UserId = "UserId";
			 public const string CreatedOn = "CreatedOn";
			 public const string CreatedByIP = "CreatedByIP";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string ModifiedByIP = "ModifiedByIP";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string StoreId = "StoreId";
			 public const string UserId = "UserId";
			 public const string CreatedOn = "CreatedOn";
			 public const string CreatedByIP = "CreatedByIP";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string ModifiedByIP = "ModifiedByIP";
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
			lock (typeof(CartMetadata))
			{
				if(CartMetadata.mapDelegates == null)
				{
					CartMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CartMetadata.meta == null)
				{
					CartMetadata.meta = new CartMetadata();
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


				meta.AddTypeMap("Id", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("StoreId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("UserId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CreatedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByIP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ModifiedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ModifiedByIP", new esTypeMap("varchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Cart";
					meta.Destination = objectQualifier + "DNNspot_Store_Cart";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_CartInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_CartUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_CartDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_CartLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_CartLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Cart";
					meta.Destination = "DNNspot_Store_Cart";
									
					meta.spInsert = "proc_DNNspot_Store_CartInsert";				
					meta.spUpdate = "proc_DNNspot_Store_CartUpdate";		
					meta.spDelete = "proc_DNNspot_Store_CartDelete";
					meta.spLoadAll = "proc_DNNspot_Store_CartLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_CartLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CartMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
