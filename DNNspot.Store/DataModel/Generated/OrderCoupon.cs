
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
	/// Encapsulates the 'DNNspot_Store_OrderCoupon' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(OrderCoupon))]	
	[XmlType("OrderCoupon")]
	[Table(Name="OrderCoupon")]
	public partial class OrderCoupon : esOrderCoupon
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new OrderCoupon();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 orderId, System.String couponCode)
		{
			var obj = new OrderCoupon();
			obj.OrderId = orderId;
			obj.CouponCode = couponCode;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 orderId, System.String couponCode, esSqlAccessType sqlAccessType)
		{
			var obj = new OrderCoupon();
			obj.OrderId = orderId;
			obj.CouponCode = couponCode;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int32? OrderId
		{
			get { return base.OrderId;  }
			set { base.OrderId = value; }
		}

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.String CouponCode
		{
			get { return base.CouponCode;  }
			set { base.CouponCode = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? DiscountAmount
		{
			get { return base.DiscountAmount;  }
			set { base.DiscountAmount = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("OrderCouponCollection")]
	public partial class OrderCouponCollection : esOrderCouponCollection, IEnumerable<OrderCoupon>
	{
		public OrderCoupon FindByPrimaryKey(System.Int32 orderId, System.String couponCode)
		{
			return this.SingleOrDefault(e => e.OrderId == orderId && e.CouponCode == couponCode);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(OrderCoupon))]
		public class OrderCouponCollectionWCFPacket : esCollectionWCFPacket<OrderCouponCollection>
		{
			public static implicit operator OrderCouponCollection(OrderCouponCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator OrderCouponCollectionWCFPacket(OrderCouponCollection collection)
			{
				return new OrderCouponCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class OrderCouponQuery : esOrderCouponQuery
	{
		public OrderCouponQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "OrderCouponQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(OrderCouponQuery query)
		{
			return OrderCouponQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator OrderCouponQuery(string query)
		{
			return (OrderCouponQuery)OrderCouponQuery.SerializeHelper.FromXml(query, typeof(OrderCouponQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esOrderCoupon : esEntity
	{
		public esOrderCoupon()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 orderId, System.String couponCode)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderId, couponCode);
			else
				return LoadByPrimaryKeyStoredProcedure(orderId, couponCode);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 orderId, System.String couponCode)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(orderId, couponCode);
			else
				return LoadByPrimaryKeyStoredProcedure(orderId, couponCode);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 orderId, System.String couponCode)
		{
			OrderCouponQuery query = new OrderCouponQuery();
			query.Where(query.OrderId == orderId, query.CouponCode == couponCode);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 orderId, System.String couponCode)
		{
			esParameters parms = new esParameters();
			parms.Add("OrderId", orderId);			parms.Add("CouponCode", couponCode);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderCoupon.OrderId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? OrderId
		{
			get
			{
				return base.GetSystemInt32(OrderCouponMetadata.ColumnNames.OrderId);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderCouponMetadata.ColumnNames.OrderId, value))
				{
					this._UpToOrderByOrderId = null;
					this.OnPropertyChanged("UpToOrderByOrderId");
					OnPropertyChanged(OrderCouponMetadata.PropertyNames.OrderId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderCoupon.CouponCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CouponCode
		{
			get
			{
				return base.GetSystemString(OrderCouponMetadata.ColumnNames.CouponCode);
			}
			
			set
			{
				if(base.SetSystemString(OrderCouponMetadata.ColumnNames.CouponCode, value))
				{
					OnPropertyChanged(OrderCouponMetadata.PropertyNames.CouponCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderCoupon.DiscountAmount
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(OrderCouponMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderCouponMetadata.ColumnNames.DiscountAmount, value))
				{
					OnPropertyChanged(OrderCouponMetadata.PropertyNames.DiscountAmount);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Order _UpToOrderByOrderId;
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
						case "OrderId": this.str().OrderId = (string)value; break;							
						case "CouponCode": this.str().CouponCode = (string)value; break;							
						case "DiscountAmount": this.str().DiscountAmount = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "OrderId":
						
							if (value == null || value is System.Int32)
								this.OrderId = (System.Int32?)value;
								OnPropertyChanged(OrderCouponMetadata.PropertyNames.OrderId);
							break;
						
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
								OnPropertyChanged(OrderCouponMetadata.PropertyNames.DiscountAmount);
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
			public esStrings(esOrderCoupon entity)
			{
				this.entity = entity;
			}
			
	
			public System.String OrderId
			{
				get
				{
					System.Int32? data = entity.OrderId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderId = null;
					else entity.OrderId = Convert.ToInt32(value);
				}
			}
				
			public System.String CouponCode
			{
				get
				{
					System.String data = entity.CouponCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CouponCode = null;
					else entity.CouponCode = Convert.ToString(value);
				}
			}
				
			public System.String DiscountAmount
			{
				get
				{
					System.Decimal? data = entity.DiscountAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountAmount = null;
					else entity.DiscountAmount = Convert.ToDecimal(value);
				}
			}
			

			private esOrderCoupon entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return OrderCouponMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public OrderCouponQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderCouponQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(OrderCouponQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(OrderCouponQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private OrderCouponQuery query;		
	}



	[Serializable]
	abstract public partial class esOrderCouponCollection : esEntityCollection<OrderCoupon>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OrderCouponMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "OrderCouponCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public OrderCouponQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderCouponQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(OrderCouponQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderCouponQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(OrderCouponQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((OrderCouponQuery)query);
		}

		#endregion
		
		private OrderCouponQuery query;
	}



	[Serializable]
	abstract public partial class esOrderCouponQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OrderCouponMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "OrderId": return this.OrderId;
				case "CouponCode": return this.CouponCode;
				case "DiscountAmount": return this.DiscountAmount;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem OrderId
		{
			get { return new esQueryItem(this, OrderCouponMetadata.ColumnNames.OrderId, esSystemType.Int32); }
		} 
		
		public esQueryItem CouponCode
		{
			get { return new esQueryItem(this, OrderCouponMetadata.ColumnNames.CouponCode, esSystemType.String); }
		} 
		
		public esQueryItem DiscountAmount
		{
			get { return new esQueryItem(this, OrderCouponMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal); }
		} 
		
		#endregion
		
	}


	
	public partial class OrderCoupon : esOrderCoupon
	{

				
		#region UpToOrderByOrderId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_OrderCoupon_DNNspot_Store_Order
		/// </summary>

		[XmlIgnore]
					
		public Order UpToOrderByOrderId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToOrderByOrderId == null && OrderId != null)
				{
					this._UpToOrderByOrderId = new Order();
					this._UpToOrderByOrderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToOrderByOrderId", this._UpToOrderByOrderId);
					this._UpToOrderByOrderId.Query.Where(this._UpToOrderByOrderId.Query.Id == this.OrderId);
					this._UpToOrderByOrderId.Query.Load();
				}	
				return this._UpToOrderByOrderId;
			}
			
			set
			{
				this.RemovePreSave("UpToOrderByOrderId");
				

				if(value == null)
				{
					this.OrderId = null;
					this._UpToOrderByOrderId = null;
				}
				else
				{
					this.OrderId = value.Id;
					this._UpToOrderByOrderId = value;
					this.SetPreSave("UpToOrderByOrderId", this._UpToOrderByOrderId);
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
			if(!this.es.IsDeleted && this._UpToOrderByOrderId != null)
			{
				this.OrderId = this._UpToOrderByOrderId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class OrderCouponMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OrderCouponMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OrderCouponMetadata.ColumnNames.OrderId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderCouponMetadata.PropertyNames.OrderId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderCouponMetadata.ColumnNames.CouponCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderCouponMetadata.PropertyNames.CouponCode;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderCouponMetadata.ColumnNames.DiscountAmount, 2, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderCouponMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public OrderCouponMetadata Meta()
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
			 public const string OrderId = "OrderId";
			 public const string CouponCode = "CouponCode";
			 public const string DiscountAmount = "DiscountAmount";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string OrderId = "OrderId";
			 public const string CouponCode = "CouponCode";
			 public const string DiscountAmount = "DiscountAmount";
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
			lock (typeof(OrderCouponMetadata))
			{
				if(OrderCouponMetadata.mapDelegates == null)
				{
					OrderCouponMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OrderCouponMetadata.meta == null)
				{
					OrderCouponMetadata.meta = new OrderCouponMetadata();
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


				meta.AddTypeMap("OrderId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CouponCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("money", "System.Decimal"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_OrderCoupon";
					meta.Destination = objectQualifier + "DNNspot_Store_OrderCoupon";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_OrderCouponInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_OrderCouponUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_OrderCouponDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_OrderCouponLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_OrderCouponLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_OrderCoupon";
					meta.Destination = "DNNspot_Store_OrderCoupon";
									
					meta.spInsert = "proc_DNNspot_Store_OrderCouponInsert";				
					meta.spUpdate = "proc_DNNspot_Store_OrderCouponUpdate";		
					meta.spDelete = "proc_DNNspot_Store_OrderCouponDelete";
					meta.spLoadAll = "proc_DNNspot_Store_OrderCouponLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_OrderCouponLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OrderCouponMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
