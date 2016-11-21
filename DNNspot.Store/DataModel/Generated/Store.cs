
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
	/// Encapsulates the 'DNNspot_Store_Store' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Store))]	
	[XmlType("Store")]
	[Table(Name="Store")]
	public partial class Store : esStore
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Store();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new Store();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Store();
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
		public override System.Int32? PortalId
		{
			get { return base.PortalId;  }
			set { base.PortalId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.DateTime? CreatedOn
		{
			get { return base.CreatedOn;  }
			set { base.CreatedOn = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? CreatedByUserId
		{
			get { return base.CreatedByUserId;  }
			set { base.CreatedByUserId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Guid? StoreGuid
		{
			get { return base.StoreGuid;  }
			set { base.StoreGuid = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("StoreCollection")]
	public partial class StoreCollection : esStoreCollection, IEnumerable<Store>
	{
		public Store FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Store))]
		public class StoreCollectionWCFPacket : esCollectionWCFPacket<StoreCollection>
		{
			public static implicit operator StoreCollection(StoreCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator StoreCollectionWCFPacket(StoreCollection collection)
			{
				return new StoreCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class StoreQuery : esStoreQuery
	{
		public StoreQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "StoreQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(StoreQuery query)
		{
			return StoreQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator StoreQuery(string query)
		{
			return (StoreQuery)StoreQuery.SerializeHelper.FromXml(query, typeof(StoreQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esStore : esEntity
	{
		public esStore()
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
			StoreQuery query = new StoreQuery();
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
		/// Maps to DNNspot_Store_Store.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(StoreMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(StoreMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(StoreMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Store.PortalId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? PortalId
		{
			get
			{
				return base.GetSystemInt32(StoreMetadata.ColumnNames.PortalId);
			}
			
			set
			{
				if(base.SetSystemInt32(StoreMetadata.ColumnNames.PortalId, value))
				{
					OnPropertyChanged(StoreMetadata.PropertyNames.PortalId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Store.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(StoreMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(StoreMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(StoreMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Store.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(StoreMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(StoreMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(StoreMetadata.PropertyNames.CreatedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Store.CreatedByUserId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? CreatedByUserId
		{
			get
			{
				return base.GetSystemInt32(StoreMetadata.ColumnNames.CreatedByUserId);
			}
			
			set
			{
				if(base.SetSystemInt32(StoreMetadata.ColumnNames.CreatedByUserId, value))
				{
					OnPropertyChanged(StoreMetadata.PropertyNames.CreatedByUserId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Store.StoreGuid
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Guid? StoreGuid
		{
			get
			{
				return base.GetSystemGuid(StoreMetadata.ColumnNames.StoreGuid);
			}
			
			set
			{
				if(base.SetSystemGuid(StoreMetadata.ColumnNames.StoreGuid, value))
				{
					OnPropertyChanged(StoreMetadata.PropertyNames.StoreGuid);
				}
			}
		}		
		
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
						case "PortalId": this.str().PortalId = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;							
						case "CreatedByUserId": this.str().CreatedByUserId = (string)value; break;							
						case "StoreGuid": this.str().StoreGuid = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(StoreMetadata.PropertyNames.Id);
							break;
						
						case "PortalId":
						
							if (value == null || value is System.Int32)
								this.PortalId = (System.Int32?)value;
								OnPropertyChanged(StoreMetadata.PropertyNames.PortalId);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(StoreMetadata.PropertyNames.CreatedOn);
							break;
						
						case "CreatedByUserId":
						
							if (value == null || value is System.Int32)
								this.CreatedByUserId = (System.Int32?)value;
								OnPropertyChanged(StoreMetadata.PropertyNames.CreatedByUserId);
							break;
						
						case "StoreGuid":
						
							if (value == null || value is System.Guid)
								this.StoreGuid = (System.Guid?)value;
								OnPropertyChanged(StoreMetadata.PropertyNames.StoreGuid);
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
			public esStrings(esStore entity)
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
				
			public System.String PortalId
			{
				get
				{
					System.Int32? data = entity.PortalId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PortalId = null;
					else entity.PortalId = Convert.ToInt32(value);
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
				
			public System.String CreatedByUserId
			{
				get
				{
					System.Int32? data = entity.CreatedByUserId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedByUserId = null;
					else entity.CreatedByUserId = Convert.ToInt32(value);
				}
			}
				
			public System.String StoreGuid
			{
				get
				{
					System.Guid? data = entity.StoreGuid;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.StoreGuid = null;
					else entity.StoreGuid = new Guid(value);
				}
			}
			

			private esStore entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return StoreMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public StoreQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StoreQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StoreQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(StoreQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private StoreQuery query;		
	}



	[Serializable]
	abstract public partial class esStoreCollection : esEntityCollection<Store>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StoreMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "StoreCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public StoreQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StoreQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StoreQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StoreQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(StoreQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((StoreQuery)query);
		}

		#endregion
		
		private StoreQuery query;
	}



	[Serializable]
	abstract public partial class esStoreQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StoreMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "PortalId": return this.PortalId;
				case "Name": return this.Name;
				case "CreatedOn": return this.CreatedOn;
				case "CreatedByUserId": return this.CreatedByUserId;
				case "StoreGuid": return this.StoreGuid;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, StoreMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem PortalId
		{
			get { return new esQueryItem(this, StoreMetadata.ColumnNames.PortalId, esSystemType.Int32); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, StoreMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, StoreMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem CreatedByUserId
		{
			get { return new esQueryItem(this, StoreMetadata.ColumnNames.CreatedByUserId, esSystemType.Int32); }
		} 
		
		public esQueryItem StoreGuid
		{
			get { return new esQueryItem(this, StoreMetadata.ColumnNames.StoreGuid, esSystemType.Guid); }
		} 
		
		#endregion
		
	}


	
	public partial class Store : esStore
	{

		#region CartCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_CartCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.CartCollectionByStoreId_Delegate;
				map.PropertyName = "CartCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void CartCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			CartQuery me = data.You != null ? data.You as CartQuery : new CartQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Cart_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public CartCollection CartCollectionByStoreId
		{
			get
			{
				if(this._CartCollectionByStoreId == null)
				{
					this._CartCollectionByStoreId = new CartCollection();
					this._CartCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CartCollectionByStoreId", this._CartCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._CartCollectionByStoreId.Query.Where(this._CartCollectionByStoreId.Query.StoreId == this.Id);
							this._CartCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._CartCollectionByStoreId.fks.Add(CartMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._CartCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CartCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("CartCollectionByStoreId"); 
					this._CartCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private CartCollection _CartCollectionByStoreId;
		#endregion

		#region CategoryCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_CategoryCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.CategoryCollectionByStoreId_Delegate;
				map.PropertyName = "CategoryCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void CategoryCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			CategoryQuery me = data.You != null ? data.You as CategoryQuery : new CategoryQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Category_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public CategoryCollection CategoryCollectionByStoreId
		{
			get
			{
				if(this._CategoryCollectionByStoreId == null)
				{
					this._CategoryCollectionByStoreId = new CategoryCollection();
					this._CategoryCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CategoryCollectionByStoreId", this._CategoryCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._CategoryCollectionByStoreId.Query.Where(this._CategoryCollectionByStoreId.Query.StoreId == this.Id);
							this._CategoryCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._CategoryCollectionByStoreId.fks.Add(CategoryMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._CategoryCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CategoryCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("CategoryCollectionByStoreId"); 
					this._CategoryCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private CategoryCollection _CategoryCollectionByStoreId;
		#endregion

		#region CouponCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_CouponCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.CouponCollectionByStoreId_Delegate;
				map.PropertyName = "CouponCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void CouponCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			CouponQuery me = data.You != null ? data.You as CouponQuery : new CouponQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Coupon_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public CouponCollection CouponCollectionByStoreId
		{
			get
			{
				if(this._CouponCollectionByStoreId == null)
				{
					this._CouponCollectionByStoreId = new CouponCollection();
					this._CouponCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CouponCollectionByStoreId", this._CouponCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._CouponCollectionByStoreId.Query.Where(this._CouponCollectionByStoreId.Query.StoreId == this.Id);
							this._CouponCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._CouponCollectionByStoreId.fks.Add(CouponMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._CouponCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CouponCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("CouponCollectionByStoreId"); 
					this._CouponCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private CouponCollection _CouponCollectionByStoreId;
		#endregion

		#region DiscountCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_DiscountCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.DiscountCollectionByStoreId_Delegate;
				map.PropertyName = "DiscountCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void DiscountCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			DiscountQuery me = data.You != null ? data.You as DiscountQuery : new DiscountQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Discount_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public DiscountCollection DiscountCollectionByStoreId
		{
			get
			{
				if(this._DiscountCollectionByStoreId == null)
				{
					this._DiscountCollectionByStoreId = new DiscountCollection();
					this._DiscountCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("DiscountCollectionByStoreId", this._DiscountCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._DiscountCollectionByStoreId.Query.Where(this._DiscountCollectionByStoreId.Query.StoreId == this.Id);
							this._DiscountCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._DiscountCollectionByStoreId.fks.Add(DiscountMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._DiscountCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._DiscountCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("DiscountCollectionByStoreId"); 
					this._DiscountCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private DiscountCollection _DiscountCollectionByStoreId;
		#endregion

		#region OrderCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_OrderCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.OrderCollectionByStoreId_Delegate;
				map.PropertyName = "OrderCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void OrderCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			OrderQuery me = data.You != null ? data.You as OrderQuery : new OrderQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Order_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public OrderCollection OrderCollectionByStoreId
		{
			get
			{
				if(this._OrderCollectionByStoreId == null)
				{
					this._OrderCollectionByStoreId = new OrderCollection();
					this._OrderCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("OrderCollectionByStoreId", this._OrderCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._OrderCollectionByStoreId.Query.Where(this._OrderCollectionByStoreId.Query.StoreId == this.Id);
							this._OrderCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._OrderCollectionByStoreId.fks.Add(OrderMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._OrderCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._OrderCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("OrderCollectionByStoreId"); 
					this._OrderCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private OrderCollection _OrderCollectionByStoreId;
		#endregion

		#region ShippingServiceCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_ShippingServiceCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.ShippingServiceCollectionByStoreId_Delegate;
				map.PropertyName = "ShippingServiceCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ShippingServiceCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			ShippingServiceQuery me = data.You != null ? data.You as ShippingServiceQuery : new ShippingServiceQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ShippingService_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public ShippingServiceCollection ShippingServiceCollectionByStoreId
		{
			get
			{
				if(this._ShippingServiceCollectionByStoreId == null)
				{
					this._ShippingServiceCollectionByStoreId = new ShippingServiceCollection();
					this._ShippingServiceCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ShippingServiceCollectionByStoreId", this._ShippingServiceCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ShippingServiceCollectionByStoreId.Query.Where(this._ShippingServiceCollectionByStoreId.Query.StoreId == this.Id);
							this._ShippingServiceCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ShippingServiceCollectionByStoreId.fks.Add(ShippingServiceMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._ShippingServiceCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ShippingServiceCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("ShippingServiceCollectionByStoreId"); 
					this._ShippingServiceCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private ShippingServiceCollection _ShippingServiceCollectionByStoreId;
		#endregion

		#region UpToEmailTemplateCollectionByStoreEmailTemplate - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public EmailTemplateCollection UpToEmailTemplateCollectionByStoreEmailTemplate
		{
			get
			{
				if(this._UpToEmailTemplateCollectionByStoreEmailTemplate == null)
				{
					this._UpToEmailTemplateCollectionByStoreEmailTemplate = new EmailTemplateCollection();
					this._UpToEmailTemplateCollectionByStoreEmailTemplate.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToEmailTemplateCollectionByStoreEmailTemplate", this._UpToEmailTemplateCollectionByStoreEmailTemplate);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						EmailTemplateQuery m = new EmailTemplateQuery("m");
						StoreEmailTemplateQuery j = new StoreEmailTemplateQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.EmailTemplateId);
                        m.Where(j.StoreId == this.Id);

						this._UpToEmailTemplateCollectionByStoreEmailTemplate.Load(m);
					}
				}

				return this._UpToEmailTemplateCollectionByStoreEmailTemplate;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToEmailTemplateCollectionByStoreEmailTemplate != null) 
				{ 
					this.RemovePostSave("UpToEmailTemplateCollectionByStoreEmailTemplate"); 
					this._UpToEmailTemplateCollectionByStoreEmailTemplate = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_Store
		/// </summary>
		public void AssociateEmailTemplateCollectionByStoreEmailTemplate(EmailTemplate entity)
		{
			if (this._StoreEmailTemplateCollection == null)
			{
				this._StoreEmailTemplateCollection = new StoreEmailTemplateCollection();
				this._StoreEmailTemplateCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StoreEmailTemplateCollection", this._StoreEmailTemplateCollection);
			}

			StoreEmailTemplate obj = this._StoreEmailTemplateCollection.AddNew();
			obj.StoreId = this.Id;
			obj.EmailTemplateId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_Store
		/// </summary>
		public void DissociateEmailTemplateCollectionByStoreEmailTemplate(EmailTemplate entity)
		{
			if (this._StoreEmailTemplateCollection == null)
			{
				this._StoreEmailTemplateCollection = new StoreEmailTemplateCollection();
				this._StoreEmailTemplateCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StoreEmailTemplateCollection", this._StoreEmailTemplateCollection);
			}

			StoreEmailTemplate obj = this._StoreEmailTemplateCollection.AddNew();
			obj.StoreId = this.Id;
            obj.EmailTemplateId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private EmailTemplateCollection _UpToEmailTemplateCollectionByStoreEmailTemplate;
		private StoreEmailTemplateCollection _StoreEmailTemplateCollection;
		#endregion

		#region StoreEmailTemplateCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_StoreEmailTemplateCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.StoreEmailTemplateCollectionByStoreId_Delegate;
				map.PropertyName = "StoreEmailTemplateCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void StoreEmailTemplateCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			StoreEmailTemplateQuery me = data.You != null ? data.You as StoreEmailTemplateQuery : new StoreEmailTemplateQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public StoreEmailTemplateCollection StoreEmailTemplateCollectionByStoreId
		{
			get
			{
				if(this._StoreEmailTemplateCollectionByStoreId == null)
				{
					this._StoreEmailTemplateCollectionByStoreId = new StoreEmailTemplateCollection();
					this._StoreEmailTemplateCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("StoreEmailTemplateCollectionByStoreId", this._StoreEmailTemplateCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._StoreEmailTemplateCollectionByStoreId.Query.Where(this._StoreEmailTemplateCollectionByStoreId.Query.StoreId == this.Id);
							this._StoreEmailTemplateCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._StoreEmailTemplateCollectionByStoreId.fks.Add(StoreEmailTemplateMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._StoreEmailTemplateCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._StoreEmailTemplateCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("StoreEmailTemplateCollectionByStoreId"); 
					this._StoreEmailTemplateCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private StoreEmailTemplateCollection _StoreEmailTemplateCollectionByStoreId;
		#endregion

		#region UpToPaymentProviderCollectionByStorePaymentProvider - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public PaymentProviderCollection UpToPaymentProviderCollectionByStorePaymentProvider
		{
			get
			{
				if(this._UpToPaymentProviderCollectionByStorePaymentProvider == null)
				{
					this._UpToPaymentProviderCollectionByStorePaymentProvider = new PaymentProviderCollection();
					this._UpToPaymentProviderCollectionByStorePaymentProvider.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToPaymentProviderCollectionByStorePaymentProvider", this._UpToPaymentProviderCollectionByStorePaymentProvider);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						PaymentProviderQuery m = new PaymentProviderQuery("m");
						StorePaymentProviderQuery j = new StorePaymentProviderQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.PaymentProviderId);
                        m.Where(j.StoreId == this.Id);

						this._UpToPaymentProviderCollectionByStorePaymentProvider.Load(m);
					}
				}

				return this._UpToPaymentProviderCollectionByStorePaymentProvider;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToPaymentProviderCollectionByStorePaymentProvider != null) 
				{ 
					this.RemovePostSave("UpToPaymentProviderCollectionByStorePaymentProvider"); 
					this._UpToPaymentProviderCollectionByStorePaymentProvider = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_Store
		/// </summary>
		public void AssociatePaymentProviderCollectionByStorePaymentProvider(PaymentProvider entity)
		{
			if (this._StorePaymentProviderCollection == null)
			{
				this._StorePaymentProviderCollection = new StorePaymentProviderCollection();
				this._StorePaymentProviderCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderCollection", this._StorePaymentProviderCollection);
			}

			StorePaymentProvider obj = this._StorePaymentProviderCollection.AddNew();
			obj.StoreId = this.Id;
			obj.PaymentProviderId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_Store
		/// </summary>
		public void DissociatePaymentProviderCollectionByStorePaymentProvider(PaymentProvider entity)
		{
			if (this._StorePaymentProviderCollection == null)
			{
				this._StorePaymentProviderCollection = new StorePaymentProviderCollection();
				this._StorePaymentProviderCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderCollection", this._StorePaymentProviderCollection);
			}

			StorePaymentProvider obj = this._StorePaymentProviderCollection.AddNew();
			obj.StoreId = this.Id;
            obj.PaymentProviderId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private PaymentProviderCollection _UpToPaymentProviderCollectionByStorePaymentProvider;
		private StorePaymentProviderCollection _StorePaymentProviderCollection;
		#endregion

		#region StorePaymentProviderCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_StorePaymentProviderCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.StorePaymentProviderCollectionByStoreId_Delegate;
				map.PropertyName = "StorePaymentProviderCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void StorePaymentProviderCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			StorePaymentProviderQuery me = data.You != null ? data.You as StorePaymentProviderQuery : new StorePaymentProviderQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessor_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public StorePaymentProviderCollection StorePaymentProviderCollectionByStoreId
		{
			get
			{
				if(this._StorePaymentProviderCollectionByStoreId == null)
				{
					this._StorePaymentProviderCollectionByStoreId = new StorePaymentProviderCollection();
					this._StorePaymentProviderCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("StorePaymentProviderCollectionByStoreId", this._StorePaymentProviderCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._StorePaymentProviderCollectionByStoreId.Query.Where(this._StorePaymentProviderCollectionByStoreId.Query.StoreId == this.Id);
							this._StorePaymentProviderCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._StorePaymentProviderCollectionByStoreId.fks.Add(StorePaymentProviderMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._StorePaymentProviderCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._StorePaymentProviderCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("StorePaymentProviderCollectionByStoreId"); 
					this._StorePaymentProviderCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private StorePaymentProviderCollection _StorePaymentProviderCollectionByStoreId;
		#endregion

		#region UpToPaymentProviderCollectionByStorePaymentProviderSetting - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public PaymentProviderCollection UpToPaymentProviderCollectionByStorePaymentProviderSetting
		{
			get
			{
				if(this._UpToPaymentProviderCollectionByStorePaymentProviderSetting == null)
				{
					this._UpToPaymentProviderCollectionByStorePaymentProviderSetting = new PaymentProviderCollection();
					this._UpToPaymentProviderCollectionByStorePaymentProviderSetting.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToPaymentProviderCollectionByStorePaymentProviderSetting", this._UpToPaymentProviderCollectionByStorePaymentProviderSetting);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						PaymentProviderQuery m = new PaymentProviderQuery("m");
						StorePaymentProviderSettingQuery j = new StorePaymentProviderSettingQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.PaymentProviderId);
                        m.Where(j.StoreId == this.Id);

						this._UpToPaymentProviderCollectionByStorePaymentProviderSetting.Load(m);
					}
				}

				return this._UpToPaymentProviderCollectionByStorePaymentProviderSetting;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToPaymentProviderCollectionByStorePaymentProviderSetting != null) 
				{ 
					this.RemovePostSave("UpToPaymentProviderCollectionByStorePaymentProviderSetting"); 
					this._UpToPaymentProviderCollectionByStorePaymentProviderSetting = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_Store
		/// </summary>
		public void AssociatePaymentProviderCollectionByStorePaymentProviderSetting(PaymentProvider entity)
		{
			if (this._StorePaymentProviderSettingCollection == null)
			{
				this._StorePaymentProviderSettingCollection = new StorePaymentProviderSettingCollection();
				this._StorePaymentProviderSettingCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderSettingCollection", this._StorePaymentProviderSettingCollection);
			}

			StorePaymentProviderSetting obj = this._StorePaymentProviderSettingCollection.AddNew();
			obj.StoreId = this.Id;
			obj.PaymentProviderId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_Store
		/// </summary>
		public void DissociatePaymentProviderCollectionByStorePaymentProviderSetting(PaymentProvider entity)
		{
			if (this._StorePaymentProviderSettingCollection == null)
			{
				this._StorePaymentProviderSettingCollection = new StorePaymentProviderSettingCollection();
				this._StorePaymentProviderSettingCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StorePaymentProviderSettingCollection", this._StorePaymentProviderSettingCollection);
			}

			StorePaymentProviderSetting obj = this._StorePaymentProviderSettingCollection.AddNew();
			obj.StoreId = this.Id;
            obj.PaymentProviderId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private PaymentProviderCollection _UpToPaymentProviderCollectionByStorePaymentProviderSetting;
		private StorePaymentProviderSettingCollection _StorePaymentProviderSettingCollection;
		#endregion

		#region StorePaymentProviderSettingCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_StorePaymentProviderSettingCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.StorePaymentProviderSettingCollectionByStoreId_Delegate;
				map.PropertyName = "StorePaymentProviderSettingCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void StorePaymentProviderSettingCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			StorePaymentProviderSettingQuery me = data.You != null ? data.You as StorePaymentProviderSettingQuery : new StorePaymentProviderSettingQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_StorePaymentProcessorSetting_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public StorePaymentProviderSettingCollection StorePaymentProviderSettingCollectionByStoreId
		{
			get
			{
				if(this._StorePaymentProviderSettingCollectionByStoreId == null)
				{
					this._StorePaymentProviderSettingCollectionByStoreId = new StorePaymentProviderSettingCollection();
					this._StorePaymentProviderSettingCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("StorePaymentProviderSettingCollectionByStoreId", this._StorePaymentProviderSettingCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._StorePaymentProviderSettingCollectionByStoreId.Query.Where(this._StorePaymentProviderSettingCollectionByStoreId.Query.StoreId == this.Id);
							this._StorePaymentProviderSettingCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._StorePaymentProviderSettingCollectionByStoreId.fks.Add(StorePaymentProviderSettingMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._StorePaymentProviderSettingCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._StorePaymentProviderSettingCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("StorePaymentProviderSettingCollectionByStoreId"); 
					this._StorePaymentProviderSettingCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private StorePaymentProviderSettingCollection _StorePaymentProviderSettingCollectionByStoreId;
		#endregion

		#region StoreSettingCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_StoreSettingCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.StoreSettingCollectionByStoreId_Delegate;
				map.PropertyName = "StoreSettingCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void StoreSettingCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			StoreSettingQuery me = data.You != null ? data.You as StoreSettingQuery : new StoreSettingQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_StoreSetting_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public StoreSettingCollection StoreSettingCollectionByStoreId
		{
			get
			{
				if(this._StoreSettingCollectionByStoreId == null)
				{
					this._StoreSettingCollectionByStoreId = new StoreSettingCollection();
					this._StoreSettingCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("StoreSettingCollectionByStoreId", this._StoreSettingCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._StoreSettingCollectionByStoreId.Query.Where(this._StoreSettingCollectionByStoreId.Query.StoreId == this.Id);
							this._StoreSettingCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._StoreSettingCollectionByStoreId.fks.Add(StoreSettingMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._StoreSettingCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._StoreSettingCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("StoreSettingCollectionByStoreId"); 
					this._StoreSettingCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private StoreSettingCollection _StoreSettingCollectionByStoreId;
		#endregion

		#region TaxRegionCollectionByStoreId - Zero To Many
		
		static public esPrefetchMap Prefetch_TaxRegionCollectionByStoreId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Store.TaxRegionCollectionByStoreId_Delegate;
				map.PropertyName = "TaxRegionCollectionByStoreId";
				map.MyColumnName = "StoreId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void TaxRegionCollectionByStoreId_Delegate(esPrefetchParameters data)
		{
			StoreQuery parent = new StoreQuery(data.NextAlias());

			TaxRegionQuery me = data.You != null ? data.You as TaxRegionQuery : new TaxRegionQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.StoreId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_RegionTax_DNNspot_Store_Store
		/// </summary>

		[XmlIgnore]
		public TaxRegionCollection TaxRegionCollectionByStoreId
		{
			get
			{
				if(this._TaxRegionCollectionByStoreId == null)
				{
					this._TaxRegionCollectionByStoreId = new TaxRegionCollection();
					this._TaxRegionCollectionByStoreId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("TaxRegionCollectionByStoreId", this._TaxRegionCollectionByStoreId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._TaxRegionCollectionByStoreId.Query.Where(this._TaxRegionCollectionByStoreId.Query.StoreId == this.Id);
							this._TaxRegionCollectionByStoreId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._TaxRegionCollectionByStoreId.fks.Add(TaxRegionMetadata.ColumnNames.StoreId, this.Id);
					}
				}

				return this._TaxRegionCollectionByStoreId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._TaxRegionCollectionByStoreId != null) 
				{ 
					this.RemovePostSave("TaxRegionCollectionByStoreId"); 
					this._TaxRegionCollectionByStoreId = null;
					
				} 
			} 			
		}
			
		
		private TaxRegionCollection _TaxRegionCollectionByStoreId;
		#endregion

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "CartCollectionByStoreId":
					coll = this.CartCollectionByStoreId;
					break;
				case "CategoryCollectionByStoreId":
					coll = this.CategoryCollectionByStoreId;
					break;
				case "CouponCollectionByStoreId":
					coll = this.CouponCollectionByStoreId;
					break;
				case "DiscountCollectionByStoreId":
					coll = this.DiscountCollectionByStoreId;
					break;
				case "OrderCollectionByStoreId":
					coll = this.OrderCollectionByStoreId;
					break;
				case "ShippingServiceCollectionByStoreId":
					coll = this.ShippingServiceCollectionByStoreId;
					break;
				case "StoreEmailTemplateCollectionByStoreId":
					coll = this.StoreEmailTemplateCollectionByStoreId;
					break;
				case "StorePaymentProviderCollectionByStoreId":
					coll = this.StorePaymentProviderCollectionByStoreId;
					break;
				case "StorePaymentProviderSettingCollectionByStoreId":
					coll = this.StorePaymentProviderSettingCollectionByStoreId;
					break;
				case "StoreSettingCollectionByStoreId":
					coll = this.StoreSettingCollectionByStoreId;
					break;
				case "TaxRegionCollectionByStoreId":
					coll = this.TaxRegionCollectionByStoreId;
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
			
			props.Add(new esPropertyDescriptor(this, "CartCollectionByStoreId", typeof(CartCollection), new Cart()));
			props.Add(new esPropertyDescriptor(this, "CategoryCollectionByStoreId", typeof(CategoryCollection), new Category()));
			props.Add(new esPropertyDescriptor(this, "CouponCollectionByStoreId", typeof(CouponCollection), new Coupon()));
			props.Add(new esPropertyDescriptor(this, "DiscountCollectionByStoreId", typeof(DiscountCollection), new Discount()));
			props.Add(new esPropertyDescriptor(this, "OrderCollectionByStoreId", typeof(OrderCollection), new Order()));
			props.Add(new esPropertyDescriptor(this, "ShippingServiceCollectionByStoreId", typeof(ShippingServiceCollection), new ShippingService()));
			props.Add(new esPropertyDescriptor(this, "StoreEmailTemplateCollectionByStoreId", typeof(StoreEmailTemplateCollection), new StoreEmailTemplate()));
			props.Add(new esPropertyDescriptor(this, "StorePaymentProviderCollectionByStoreId", typeof(StorePaymentProviderCollection), new StorePaymentProvider()));
			props.Add(new esPropertyDescriptor(this, "StorePaymentProviderSettingCollectionByStoreId", typeof(StorePaymentProviderSettingCollection), new StorePaymentProviderSetting()));
			props.Add(new esPropertyDescriptor(this, "StoreSettingCollectionByStoreId", typeof(StoreSettingCollection), new StoreSetting()));
			props.Add(new esPropertyDescriptor(this, "TaxRegionCollectionByStoreId", typeof(TaxRegionCollection), new TaxRegion()));
		
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
			if(this._CartCollectionByStoreId != null)
			{
				Apply(this._CartCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._CategoryCollectionByStoreId != null)
			{
				Apply(this._CategoryCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._CouponCollectionByStoreId != null)
			{
				Apply(this._CouponCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._DiscountCollectionByStoreId != null)
			{
				Apply(this._DiscountCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._OrderCollectionByStoreId != null)
			{
				Apply(this._OrderCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._ShippingServiceCollectionByStoreId != null)
			{
				Apply(this._ShippingServiceCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._StoreEmailTemplateCollection != null)
			{
				Apply(this._StoreEmailTemplateCollection, "StoreId", this.Id);
			}
			if(this._StoreEmailTemplateCollectionByStoreId != null)
			{
				Apply(this._StoreEmailTemplateCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._StorePaymentProviderCollection != null)
			{
				Apply(this._StorePaymentProviderCollection, "StoreId", this.Id);
			}
			if(this._StorePaymentProviderCollectionByStoreId != null)
			{
				Apply(this._StorePaymentProviderCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._StorePaymentProviderSettingCollection != null)
			{
				Apply(this._StorePaymentProviderSettingCollection, "StoreId", this.Id);
			}
			if(this._StorePaymentProviderSettingCollectionByStoreId != null)
			{
				Apply(this._StorePaymentProviderSettingCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._StoreSettingCollectionByStoreId != null)
			{
				Apply(this._StoreSettingCollectionByStoreId, "StoreId", this.Id);
			}
			if(this._TaxRegionCollectionByStoreId != null)
			{
				Apply(this._TaxRegionCollectionByStoreId, "StoreId", this.Id);
			}
		}
		
	}
	



	[Serializable]
	public partial class StoreMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StoreMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StoreMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StoreMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreMetadata.ColumnNames.PortalId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StoreMetadata.PropertyNames.PortalId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreMetadata.ColumnNames.Name, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = StoreMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 300;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreMetadata.ColumnNames.CreatedOn, 3, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = StoreMetadata.PropertyNames.CreatedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreMetadata.ColumnNames.CreatedByUserId, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StoreMetadata.PropertyNames.CreatedByUserId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreMetadata.ColumnNames.StoreGuid, 5, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = StoreMetadata.PropertyNames.StoreGuid;
			c.HasDefault = true;
			c.Default = @"(newid())";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public StoreMetadata Meta()
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
			 public const string PortalId = "PortalId";
			 public const string Name = "Name";
			 public const string CreatedOn = "CreatedOn";
			 public const string CreatedByUserId = "CreatedByUserId";
			 public const string StoreGuid = "StoreGuid";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string PortalId = "PortalId";
			 public const string Name = "Name";
			 public const string CreatedOn = "CreatedOn";
			 public const string CreatedByUserId = "CreatedByUserId";
			 public const string StoreGuid = "StoreGuid";
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
			lock (typeof(StoreMetadata))
			{
				if(StoreMetadata.mapDelegates == null)
				{
					StoreMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StoreMetadata.meta == null)
				{
					StoreMetadata.meta = new StoreMetadata();
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
				meta.AddTypeMap("PortalId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByUserId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("StoreGuid", new esTypeMap("uniqueidentifier", "System.Guid"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Store";
					meta.Destination = objectQualifier + "DNNspot_Store_Store";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_StoreInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_StoreUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_StoreDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_StoreLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_StoreLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Store";
					meta.Destination = "DNNspot_Store_Store";
									
					meta.spInsert = "proc_DNNspot_Store_StoreInsert";				
					meta.spUpdate = "proc_DNNspot_Store_StoreUpdate";		
					meta.spDelete = "proc_DNNspot_Store_StoreDelete";
					meta.spLoadAll = "proc_DNNspot_Store_StoreLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_StoreLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StoreMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
