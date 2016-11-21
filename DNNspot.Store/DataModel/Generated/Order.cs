
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
	/// Encapsulates the 'DNNspot_Store_Order' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Order))]	
	[XmlType("Order")]
	[Table(Name="Order")]
	public partial class Order : esOrder
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Order();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new Order();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Order();
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
		public override System.Boolean? IsDeleted
		{
			get { return base.IsDeleted;  }
			set { base.IsDeleted = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? UserId
		{
			get { return base.UserId;  }
			set { base.UserId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String OrderNumber
		{
			get { return base.OrderNumber;  }
			set { base.OrderNumber = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? OrderStatusId
		{
			get { return base.OrderStatusId;  }
			set { base.OrderStatusId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? PaymentStatusId
		{
			get { return base.PaymentStatusId;  }
			set { base.PaymentStatusId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CustomerFirstName
		{
			get { return base.CustomerFirstName;  }
			set { base.CustomerFirstName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CustomerLastName
		{
			get { return base.CustomerLastName;  }
			set { base.CustomerLastName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CustomerEmail
		{
			get { return base.CustomerEmail;  }
			set { base.CustomerEmail = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BillAddress1
		{
			get { return base.BillAddress1;  }
			set { base.BillAddress1 = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BillAddress2
		{
			get { return base.BillAddress2;  }
			set { base.BillAddress2 = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BillCity
		{
			get { return base.BillCity;  }
			set { base.BillCity = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BillRegion
		{
			get { return base.BillRegion;  }
			set { base.BillRegion = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BillPostalCode
		{
			get { return base.BillPostalCode;  }
			set { base.BillPostalCode = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BillCountryCode
		{
			get { return base.BillCountryCode;  }
			set { base.BillCountryCode = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BillTelephone
		{
			get { return base.BillTelephone;  }
			set { base.BillTelephone = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipRecipientName
		{
			get { return base.ShipRecipientName;  }
			set { base.ShipRecipientName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipRecipientBusinessName
		{
			get { return base.ShipRecipientBusinessName;  }
			set { base.ShipRecipientBusinessName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipAddress1
		{
			get { return base.ShipAddress1;  }
			set { base.ShipAddress1 = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipAddress2
		{
			get { return base.ShipAddress2;  }
			set { base.ShipAddress2 = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipCity
		{
			get { return base.ShipCity;  }
			set { base.ShipCity = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipRegion
		{
			get { return base.ShipRegion;  }
			set { base.ShipRegion = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipPostalCode
		{
			get { return base.ShipPostalCode;  }
			set { base.ShipPostalCode = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipCountryCode
		{
			get { return base.ShipCountryCode;  }
			set { base.ShipCountryCode = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShipTelephone
		{
			get { return base.ShipTelephone;  }
			set { base.ShipTelephone = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CreditCardType
		{
			get { return base.CreditCardType;  }
			set { base.CreditCardType = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CreditCardNumberLast4
		{
			get { return base.CreditCardNumberLast4;  }
			set { base.CreditCardNumberLast4 = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CreditCardNumberEncrypted
		{
			get { return base.CreditCardNumberEncrypted;  }
			set { base.CreditCardNumberEncrypted = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CreditCardExpiration
		{
			get { return base.CreditCardExpiration;  }
			set { base.CreditCardExpiration = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CreditCardNameOnCard
		{
			get { return base.CreditCardNameOnCard;  }
			set { base.CreditCardNameOnCard = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String ShippingServiceOption
		{
			get { return base.ShippingServiceOption;  }
			set { base.ShippingServiceOption = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? SubTotal
		{
			get { return base.SubTotal;  }
			set { base.SubTotal = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? ShippingAmount
		{
			get { return base.ShippingAmount;  }
			set { base.ShippingAmount = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? DiscountAmount
		{
			get { return base.DiscountAmount;  }
			set { base.DiscountAmount = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? TaxAmount
		{
			get { return base.TaxAmount;  }
			set { base.TaxAmount = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? Total
		{
			get { return base.Total;  }
			set { base.Total = value; }
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

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Guid? CreatedFromCartId
		{
			get { return base.CreatedFromCartId;  }
			set { base.CreatedFromCartId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.DateTime? ModifiedOn
		{
			get { return base.ModifiedOn;  }
			set { base.ModifiedOn = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShippingServiceProvider
		{
			get { return base.ShippingServiceProvider;  }
			set { base.ShippingServiceProvider = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? ShippingServicePrice
		{
			get { return base.ShippingServicePrice;  }
			set { base.ShippingServicePrice = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShippingServiceTrackingNumber
		{
			get { return base.ShippingServiceTrackingNumber;  }
			set { base.ShippingServiceTrackingNumber = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String ShippingServiceLabelFile
		{
			get { return base.ShippingServiceLabelFile;  }
			set { base.ShippingServiceLabelFile = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CustomerNotes
		{
			get { return base.CustomerNotes;  }
			set { base.CustomerNotes = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CreditCardSecurityCode
		{
			get { return base.CreditCardSecurityCode;  }
			set { base.CreditCardSecurityCode = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String OrderNotes
		{
			get { return base.OrderNotes;  }
			set { base.OrderNotes = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String ShippingServiceType
		{
			get { return base.ShippingServiceType;  }
			set { base.ShippingServiceType = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("OrderCollection")]
	public partial class OrderCollection : esOrderCollection, IEnumerable<Order>
	{
		public Order FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Order))]
		public class OrderCollectionWCFPacket : esCollectionWCFPacket<OrderCollection>
		{
			public static implicit operator OrderCollection(OrderCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator OrderCollectionWCFPacket(OrderCollection collection)
			{
				return new OrderCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class OrderQuery : esOrderQuery
	{
		public OrderQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "OrderQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(OrderQuery query)
		{
			return OrderQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator OrderQuery(string query)
		{
			return (OrderQuery)OrderQuery.SerializeHelper.FromXml(query, typeof(OrderQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esOrder : esEntity
	{
		public esOrder()
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
			OrderQuery query = new OrderQuery();
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
		/// Maps to DNNspot_Store_Order.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(OrderMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(OrderMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(OrderMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.IsDeleted
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsDeleted
		{
			get
			{
				return base.GetSystemBoolean(OrderMetadata.ColumnNames.IsDeleted);
			}
			
			set
			{
				if(base.SetSystemBoolean(OrderMetadata.ColumnNames.IsDeleted, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.IsDeleted);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.UserId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? UserId
		{
			get
			{
				return base.GetSystemInt32(OrderMetadata.ColumnNames.UserId);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderMetadata.ColumnNames.UserId, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.UserId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.OrderNumber
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String OrderNumber
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.OrderNumber);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.OrderNumber, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.OrderNumber);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.OrderStatusId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? OrderStatusId
		{
			get
			{
				return base.GetSystemInt16(OrderMetadata.ColumnNames.OrderStatusId);
			}
			
			set
			{
				if(base.SetSystemInt16(OrderMetadata.ColumnNames.OrderStatusId, value))
				{
					this._UpToOrderStatusByOrderStatusId = null;
					this.OnPropertyChanged("UpToOrderStatusByOrderStatusId");
					OnPropertyChanged(OrderMetadata.PropertyNames.OrderStatusId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.PaymentStatusId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? PaymentStatusId
		{
			get
			{
				return base.GetSystemInt16(OrderMetadata.ColumnNames.PaymentStatusId);
			}
			
			set
			{
				if(base.SetSystemInt16(OrderMetadata.ColumnNames.PaymentStatusId, value))
				{
					this._UpToPaymentStatusByPaymentStatusId = null;
					this.OnPropertyChanged("UpToPaymentStatusByPaymentStatusId");
					OnPropertyChanged(OrderMetadata.PropertyNames.PaymentStatusId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CustomerFirstName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CustomerFirstName
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CustomerFirstName);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CustomerFirstName, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CustomerFirstName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CustomerLastName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CustomerLastName
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CustomerLastName);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CustomerLastName, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CustomerLastName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CustomerEmail
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CustomerEmail
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CustomerEmail);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CustomerEmail, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CustomerEmail);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.BillAddress1
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BillAddress1
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.BillAddress1);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.BillAddress1, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.BillAddress1);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.BillAddress2
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BillAddress2
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.BillAddress2);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.BillAddress2, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.BillAddress2);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.BillCity
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BillCity
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.BillCity);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.BillCity, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.BillCity);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.BillRegion
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BillRegion
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.BillRegion);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.BillRegion, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.BillRegion);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.BillPostalCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BillPostalCode
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.BillPostalCode);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.BillPostalCode, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.BillPostalCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.BillCountryCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BillCountryCode
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.BillCountryCode);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.BillCountryCode, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.BillCountryCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.BillTelephone
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BillTelephone
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.BillTelephone);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.BillTelephone, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.BillTelephone);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipRecipientName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipRecipientName
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipRecipientName);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipRecipientName, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipRecipientName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipRecipientBusinessName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipRecipientBusinessName
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipRecipientBusinessName);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipRecipientBusinessName, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipRecipientBusinessName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipAddress1
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipAddress1
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipAddress1);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipAddress1, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipAddress1);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipAddress2
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipAddress2
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipAddress2);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipAddress2, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipAddress2);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipCity
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipCity
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipCity);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipCity, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipCity);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipRegion
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipRegion
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipRegion);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipRegion, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipRegion);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipPostalCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipPostalCode
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipPostalCode);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipPostalCode, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipPostalCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipCountryCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipCountryCode
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipCountryCode);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipCountryCode, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipCountryCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShipTelephone
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShipTelephone
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShipTelephone);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShipTelephone, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShipTelephone);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreditCardType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreditCardType
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CreditCardType);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CreditCardType, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreditCardType);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreditCardNumberLast4
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreditCardNumberLast4
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CreditCardNumberLast4);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CreditCardNumberLast4, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreditCardNumberLast4);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreditCardNumberEncrypted
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreditCardNumberEncrypted
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CreditCardNumberEncrypted);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CreditCardNumberEncrypted, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreditCardNumberEncrypted);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreditCardExpiration
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreditCardExpiration
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CreditCardExpiration);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CreditCardExpiration, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreditCardExpiration);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreditCardNameOnCard
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreditCardNameOnCard
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CreditCardNameOnCard);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CreditCardNameOnCard, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreditCardNameOnCard);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShippingServiceOption
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShippingServiceOption
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShippingServiceOption);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShippingServiceOption, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShippingServiceOption);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.SubTotal
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? SubTotal
		{
			get
			{
				return base.GetSystemDecimal(OrderMetadata.ColumnNames.SubTotal);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderMetadata.ColumnNames.SubTotal, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.SubTotal);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShippingAmount
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? ShippingAmount
		{
			get
			{
				return base.GetSystemDecimal(OrderMetadata.ColumnNames.ShippingAmount);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderMetadata.ColumnNames.ShippingAmount, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShippingAmount);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.DiscountAmount
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? DiscountAmount
		{
			get
			{
				return base.GetSystemDecimal(OrderMetadata.ColumnNames.DiscountAmount);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderMetadata.ColumnNames.DiscountAmount, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.DiscountAmount);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.TaxAmount
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? TaxAmount
		{
			get
			{
				return base.GetSystemDecimal(OrderMetadata.ColumnNames.TaxAmount);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderMetadata.ColumnNames.TaxAmount, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.TaxAmount);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.Total
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Total
		{
			get
			{
				return base.GetSystemDecimal(OrderMetadata.ColumnNames.Total);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderMetadata.ColumnNames.Total, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.Total);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(OrderMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(OrderMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreatedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreatedByIP
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreatedByIP
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CreatedByIP);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CreatedByIP, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreatedByIP);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreatedFromCartId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Guid? CreatedFromCartId
		{
			get
			{
				return base.GetSystemGuid(OrderMetadata.ColumnNames.CreatedFromCartId);
			}
			
			set
			{
				if(base.SetSystemGuid(OrderMetadata.ColumnNames.CreatedFromCartId, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreatedFromCartId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ModifiedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ModifiedOn
		{
			get
			{
				return base.GetSystemDateTime(OrderMetadata.ColumnNames.ModifiedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(OrderMetadata.ColumnNames.ModifiedOn, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ModifiedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShippingServiceProvider
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShippingServiceProvider
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShippingServiceProvider);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShippingServiceProvider, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShippingServiceProvider);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShippingServicePrice
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? ShippingServicePrice
		{
			get
			{
				return base.GetSystemDecimal(OrderMetadata.ColumnNames.ShippingServicePrice);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderMetadata.ColumnNames.ShippingServicePrice, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShippingServicePrice);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShippingServiceTrackingNumber
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShippingServiceTrackingNumber
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShippingServiceTrackingNumber);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShippingServiceTrackingNumber, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShippingServiceTrackingNumber);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShippingServiceLabelFile
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShippingServiceLabelFile
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShippingServiceLabelFile);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShippingServiceLabelFile, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShippingServiceLabelFile);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CustomerNotes
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CustomerNotes
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CustomerNotes);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CustomerNotes, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CustomerNotes);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.CreditCardSecurityCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CreditCardSecurityCode
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.CreditCardSecurityCode);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.CreditCardSecurityCode, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.CreditCardSecurityCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.OrderNotes
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String OrderNotes
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.OrderNotes);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.OrderNotes, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.OrderNotes);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Order.ShippingServiceType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShippingServiceType
		{
			get
			{
				return base.GetSystemString(OrderMetadata.ColumnNames.ShippingServiceType);
			}
			
			set
			{
				if(base.SetSystemString(OrderMetadata.ColumnNames.ShippingServiceType, value))
				{
					OnPropertyChanged(OrderMetadata.PropertyNames.ShippingServiceType);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected OrderStatus _UpToOrderStatusByOrderStatusId;
		[CLSCompliant(false)]
		internal protected PaymentStatus _UpToPaymentStatusByPaymentStatusId;
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
						case "IsDeleted": this.str().IsDeleted = (string)value; break;							
						case "UserId": this.str().UserId = (string)value; break;							
						case "OrderNumber": this.str().OrderNumber = (string)value; break;							
						case "OrderStatusId": this.str().OrderStatusId = (string)value; break;							
						case "PaymentStatusId": this.str().PaymentStatusId = (string)value; break;							
						case "CustomerFirstName": this.str().CustomerFirstName = (string)value; break;							
						case "CustomerLastName": this.str().CustomerLastName = (string)value; break;							
						case "CustomerEmail": this.str().CustomerEmail = (string)value; break;							
						case "BillAddress1": this.str().BillAddress1 = (string)value; break;							
						case "BillAddress2": this.str().BillAddress2 = (string)value; break;							
						case "BillCity": this.str().BillCity = (string)value; break;							
						case "BillRegion": this.str().BillRegion = (string)value; break;							
						case "BillPostalCode": this.str().BillPostalCode = (string)value; break;							
						case "BillCountryCode": this.str().BillCountryCode = (string)value; break;							
						case "BillTelephone": this.str().BillTelephone = (string)value; break;							
						case "ShipRecipientName": this.str().ShipRecipientName = (string)value; break;							
						case "ShipRecipientBusinessName": this.str().ShipRecipientBusinessName = (string)value; break;							
						case "ShipAddress1": this.str().ShipAddress1 = (string)value; break;							
						case "ShipAddress2": this.str().ShipAddress2 = (string)value; break;							
						case "ShipCity": this.str().ShipCity = (string)value; break;							
						case "ShipRegion": this.str().ShipRegion = (string)value; break;							
						case "ShipPostalCode": this.str().ShipPostalCode = (string)value; break;							
						case "ShipCountryCode": this.str().ShipCountryCode = (string)value; break;							
						case "ShipTelephone": this.str().ShipTelephone = (string)value; break;							
						case "CreditCardType": this.str().CreditCardType = (string)value; break;							
						case "CreditCardNumberLast4": this.str().CreditCardNumberLast4 = (string)value; break;							
						case "CreditCardNumberEncrypted": this.str().CreditCardNumberEncrypted = (string)value; break;							
						case "CreditCardExpiration": this.str().CreditCardExpiration = (string)value; break;							
						case "CreditCardNameOnCard": this.str().CreditCardNameOnCard = (string)value; break;							
						case "ShippingServiceOption": this.str().ShippingServiceOption = (string)value; break;							
						case "SubTotal": this.str().SubTotal = (string)value; break;							
						case "ShippingAmount": this.str().ShippingAmount = (string)value; break;							
						case "DiscountAmount": this.str().DiscountAmount = (string)value; break;							
						case "TaxAmount": this.str().TaxAmount = (string)value; break;							
						case "Total": this.str().Total = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;							
						case "CreatedByIP": this.str().CreatedByIP = (string)value; break;							
						case "CreatedFromCartId": this.str().CreatedFromCartId = (string)value; break;							
						case "ModifiedOn": this.str().ModifiedOn = (string)value; break;							
						case "ShippingServiceProvider": this.str().ShippingServiceProvider = (string)value; break;							
						case "ShippingServicePrice": this.str().ShippingServicePrice = (string)value; break;							
						case "ShippingServiceTrackingNumber": this.str().ShippingServiceTrackingNumber = (string)value; break;							
						case "ShippingServiceLabelFile": this.str().ShippingServiceLabelFile = (string)value; break;							
						case "CustomerNotes": this.str().CustomerNotes = (string)value; break;							
						case "CreditCardSecurityCode": this.str().CreditCardSecurityCode = (string)value; break;							
						case "OrderNotes": this.str().OrderNotes = (string)value; break;							
						case "ShippingServiceType": this.str().ShippingServiceType = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.Id);
							break;
						
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.StoreId);
							break;
						
						case "IsDeleted":
						
							if (value == null || value is System.Boolean)
								this.IsDeleted = (System.Boolean?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.IsDeleted);
							break;
						
						case "UserId":
						
							if (value == null || value is System.Int32)
								this.UserId = (System.Int32?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.UserId);
							break;
						
						case "OrderStatusId":
						
							if (value == null || value is System.Int16)
								this.OrderStatusId = (System.Int16?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.OrderStatusId);
							break;
						
						case "PaymentStatusId":
						
							if (value == null || value is System.Int16)
								this.PaymentStatusId = (System.Int16?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.PaymentStatusId);
							break;
						
						case "SubTotal":
						
							if (value == null || value is System.Decimal)
								this.SubTotal = (System.Decimal?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.SubTotal);
							break;
						
						case "ShippingAmount":
						
							if (value == null || value is System.Decimal)
								this.ShippingAmount = (System.Decimal?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.ShippingAmount);
							break;
						
						case "DiscountAmount":
						
							if (value == null || value is System.Decimal)
								this.DiscountAmount = (System.Decimal?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.DiscountAmount);
							break;
						
						case "TaxAmount":
						
							if (value == null || value is System.Decimal)
								this.TaxAmount = (System.Decimal?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.TaxAmount);
							break;
						
						case "Total":
						
							if (value == null || value is System.Decimal)
								this.Total = (System.Decimal?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.Total);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.CreatedOn);
							break;
						
						case "CreatedFromCartId":
						
							if (value == null || value is System.Guid)
								this.CreatedFromCartId = (System.Guid?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.CreatedFromCartId);
							break;
						
						case "ModifiedOn":
						
							if (value == null || value is System.DateTime)
								this.ModifiedOn = (System.DateTime?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.ModifiedOn);
							break;
						
						case "ShippingServicePrice":
						
							if (value == null || value is System.Decimal)
								this.ShippingServicePrice = (System.Decimal?)value;
								OnPropertyChanged(OrderMetadata.PropertyNames.ShippingServicePrice);
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
			public esStrings(esOrder entity)
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
				
			public System.String IsDeleted
			{
				get
				{
					System.Boolean? data = entity.IsDeleted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDeleted = null;
					else entity.IsDeleted = Convert.ToBoolean(value);
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
				
			public System.String OrderNumber
			{
				get
				{
					System.String data = entity.OrderNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNumber = null;
					else entity.OrderNumber = Convert.ToString(value);
				}
			}
				
			public System.String OrderStatusId
			{
				get
				{
					System.Int16? data = entity.OrderStatusId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderStatusId = null;
					else entity.OrderStatusId = Convert.ToInt16(value);
				}
			}
				
			public System.String PaymentStatusId
			{
				get
				{
					System.Int16? data = entity.PaymentStatusId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentStatusId = null;
					else entity.PaymentStatusId = Convert.ToInt16(value);
				}
			}
				
			public System.String CustomerFirstName
			{
				get
				{
					System.String data = entity.CustomerFirstName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerFirstName = null;
					else entity.CustomerFirstName = Convert.ToString(value);
				}
			}
				
			public System.String CustomerLastName
			{
				get
				{
					System.String data = entity.CustomerLastName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerLastName = null;
					else entity.CustomerLastName = Convert.ToString(value);
				}
			}
				
			public System.String CustomerEmail
			{
				get
				{
					System.String data = entity.CustomerEmail;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerEmail = null;
					else entity.CustomerEmail = Convert.ToString(value);
				}
			}
				
			public System.String BillAddress1
			{
				get
				{
					System.String data = entity.BillAddress1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BillAddress1 = null;
					else entity.BillAddress1 = Convert.ToString(value);
				}
			}
				
			public System.String BillAddress2
			{
				get
				{
					System.String data = entity.BillAddress2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BillAddress2 = null;
					else entity.BillAddress2 = Convert.ToString(value);
				}
			}
				
			public System.String BillCity
			{
				get
				{
					System.String data = entity.BillCity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BillCity = null;
					else entity.BillCity = Convert.ToString(value);
				}
			}
				
			public System.String BillRegion
			{
				get
				{
					System.String data = entity.BillRegion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BillRegion = null;
					else entity.BillRegion = Convert.ToString(value);
				}
			}
				
			public System.String BillPostalCode
			{
				get
				{
					System.String data = entity.BillPostalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BillPostalCode = null;
					else entity.BillPostalCode = Convert.ToString(value);
				}
			}
				
			public System.String BillCountryCode
			{
				get
				{
					System.String data = entity.BillCountryCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BillCountryCode = null;
					else entity.BillCountryCode = Convert.ToString(value);
				}
			}
				
			public System.String BillTelephone
			{
				get
				{
					System.String data = entity.BillTelephone;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BillTelephone = null;
					else entity.BillTelephone = Convert.ToString(value);
				}
			}
				
			public System.String ShipRecipientName
			{
				get
				{
					System.String data = entity.ShipRecipientName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipRecipientName = null;
					else entity.ShipRecipientName = Convert.ToString(value);
				}
			}
				
			public System.String ShipRecipientBusinessName
			{
				get
				{
					System.String data = entity.ShipRecipientBusinessName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipRecipientBusinessName = null;
					else entity.ShipRecipientBusinessName = Convert.ToString(value);
				}
			}
				
			public System.String ShipAddress1
			{
				get
				{
					System.String data = entity.ShipAddress1;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipAddress1 = null;
					else entity.ShipAddress1 = Convert.ToString(value);
				}
			}
				
			public System.String ShipAddress2
			{
				get
				{
					System.String data = entity.ShipAddress2;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipAddress2 = null;
					else entity.ShipAddress2 = Convert.ToString(value);
				}
			}
				
			public System.String ShipCity
			{
				get
				{
					System.String data = entity.ShipCity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipCity = null;
					else entity.ShipCity = Convert.ToString(value);
				}
			}
				
			public System.String ShipRegion
			{
				get
				{
					System.String data = entity.ShipRegion;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipRegion = null;
					else entity.ShipRegion = Convert.ToString(value);
				}
			}
				
			public System.String ShipPostalCode
			{
				get
				{
					System.String data = entity.ShipPostalCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipPostalCode = null;
					else entity.ShipPostalCode = Convert.ToString(value);
				}
			}
				
			public System.String ShipCountryCode
			{
				get
				{
					System.String data = entity.ShipCountryCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipCountryCode = null;
					else entity.ShipCountryCode = Convert.ToString(value);
				}
			}
				
			public System.String ShipTelephone
			{
				get
				{
					System.String data = entity.ShipTelephone;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShipTelephone = null;
					else entity.ShipTelephone = Convert.ToString(value);
				}
			}
				
			public System.String CreditCardType
			{
				get
				{
					System.String data = entity.CreditCardType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditCardType = null;
					else entity.CreditCardType = Convert.ToString(value);
				}
			}
				
			public System.String CreditCardNumberLast4
			{
				get
				{
					System.String data = entity.CreditCardNumberLast4;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditCardNumberLast4 = null;
					else entity.CreditCardNumberLast4 = Convert.ToString(value);
				}
			}
				
			public System.String CreditCardNumberEncrypted
			{
				get
				{
					System.String data = entity.CreditCardNumberEncrypted;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditCardNumberEncrypted = null;
					else entity.CreditCardNumberEncrypted = Convert.ToString(value);
				}
			}
				
			public System.String CreditCardExpiration
			{
				get
				{
					System.String data = entity.CreditCardExpiration;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditCardExpiration = null;
					else entity.CreditCardExpiration = Convert.ToString(value);
				}
			}
				
			public System.String CreditCardNameOnCard
			{
				get
				{
					System.String data = entity.CreditCardNameOnCard;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditCardNameOnCard = null;
					else entity.CreditCardNameOnCard = Convert.ToString(value);
				}
			}
				
			public System.String ShippingServiceOption
			{
				get
				{
					System.String data = entity.ShippingServiceOption;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServiceOption = null;
					else entity.ShippingServiceOption = Convert.ToString(value);
				}
			}
				
			public System.String SubTotal
			{
				get
				{
					System.Decimal? data = entity.SubTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubTotal = null;
					else entity.SubTotal = Convert.ToDecimal(value);
				}
			}
				
			public System.String ShippingAmount
			{
				get
				{
					System.Decimal? data = entity.ShippingAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingAmount = null;
					else entity.ShippingAmount = Convert.ToDecimal(value);
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
				
			public System.String TaxAmount
			{
				get
				{
					System.Decimal? data = entity.TaxAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.TaxAmount = null;
					else entity.TaxAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String Total
			{
				get
				{
					System.Decimal? data = entity.Total;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Total = null;
					else entity.Total = Convert.ToDecimal(value);
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
				
			public System.String CreatedFromCartId
			{
				get
				{
					System.Guid? data = entity.CreatedFromCartId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreatedFromCartId = null;
					else entity.CreatedFromCartId = new Guid(value);
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
				
			public System.String ShippingServiceProvider
			{
				get
				{
					System.String data = entity.ShippingServiceProvider;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServiceProvider = null;
					else entity.ShippingServiceProvider = Convert.ToString(value);
				}
			}
				
			public System.String ShippingServicePrice
			{
				get
				{
					System.Decimal? data = entity.ShippingServicePrice;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServicePrice = null;
					else entity.ShippingServicePrice = Convert.ToDecimal(value);
				}
			}
				
			public System.String ShippingServiceTrackingNumber
			{
				get
				{
					System.String data = entity.ShippingServiceTrackingNumber;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServiceTrackingNumber = null;
					else entity.ShippingServiceTrackingNumber = Convert.ToString(value);
				}
			}
				
			public System.String ShippingServiceLabelFile
			{
				get
				{
					System.String data = entity.ShippingServiceLabelFile;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServiceLabelFile = null;
					else entity.ShippingServiceLabelFile = Convert.ToString(value);
				}
			}
				
			public System.String CustomerNotes
			{
				get
				{
					System.String data = entity.CustomerNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CustomerNotes = null;
					else entity.CustomerNotes = Convert.ToString(value);
				}
			}
				
			public System.String CreditCardSecurityCode
			{
				get
				{
					System.String data = entity.CreditCardSecurityCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CreditCardSecurityCode = null;
					else entity.CreditCardSecurityCode = Convert.ToString(value);
				}
			}
				
			public System.String OrderNotes
			{
				get
				{
					System.String data = entity.OrderNotes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.OrderNotes = null;
					else entity.OrderNotes = Convert.ToString(value);
				}
			}
				
			public System.String ShippingServiceType
			{
				get
				{
					System.String data = entity.ShippingServiceType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServiceType = null;
					else entity.ShippingServiceType = Convert.ToString(value);
				}
			}
			

			private esOrder entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return OrderMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public OrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(OrderQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(OrderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private OrderQuery query;		
	}



	[Serializable]
	abstract public partial class esOrderCollection : esEntityCollection<Order>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OrderMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "OrderCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public OrderQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(OrderQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(OrderQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((OrderQuery)query);
		}

		#endregion
		
		private OrderQuery query;
	}



	[Serializable]
	abstract public partial class esOrderQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OrderMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "StoreId": return this.StoreId;
				case "IsDeleted": return this.IsDeleted;
				case "UserId": return this.UserId;
				case "OrderNumber": return this.OrderNumber;
				case "OrderStatusId": return this.OrderStatusId;
				case "PaymentStatusId": return this.PaymentStatusId;
				case "CustomerFirstName": return this.CustomerFirstName;
				case "CustomerLastName": return this.CustomerLastName;
				case "CustomerEmail": return this.CustomerEmail;
				case "BillAddress1": return this.BillAddress1;
				case "BillAddress2": return this.BillAddress2;
				case "BillCity": return this.BillCity;
				case "BillRegion": return this.BillRegion;
				case "BillPostalCode": return this.BillPostalCode;
				case "BillCountryCode": return this.BillCountryCode;
				case "BillTelephone": return this.BillTelephone;
				case "ShipRecipientName": return this.ShipRecipientName;
				case "ShipRecipientBusinessName": return this.ShipRecipientBusinessName;
				case "ShipAddress1": return this.ShipAddress1;
				case "ShipAddress2": return this.ShipAddress2;
				case "ShipCity": return this.ShipCity;
				case "ShipRegion": return this.ShipRegion;
				case "ShipPostalCode": return this.ShipPostalCode;
				case "ShipCountryCode": return this.ShipCountryCode;
				case "ShipTelephone": return this.ShipTelephone;
				case "CreditCardType": return this.CreditCardType;
				case "CreditCardNumberLast4": return this.CreditCardNumberLast4;
				case "CreditCardNumberEncrypted": return this.CreditCardNumberEncrypted;
				case "CreditCardExpiration": return this.CreditCardExpiration;
				case "CreditCardNameOnCard": return this.CreditCardNameOnCard;
				case "ShippingServiceOption": return this.ShippingServiceOption;
				case "SubTotal": return this.SubTotal;
				case "ShippingAmount": return this.ShippingAmount;
				case "DiscountAmount": return this.DiscountAmount;
				case "TaxAmount": return this.TaxAmount;
				case "Total": return this.Total;
				case "CreatedOn": return this.CreatedOn;
				case "CreatedByIP": return this.CreatedByIP;
				case "CreatedFromCartId": return this.CreatedFromCartId;
				case "ModifiedOn": return this.ModifiedOn;
				case "ShippingServiceProvider": return this.ShippingServiceProvider;
				case "ShippingServicePrice": return this.ShippingServicePrice;
				case "ShippingServiceTrackingNumber": return this.ShippingServiceTrackingNumber;
				case "ShippingServiceLabelFile": return this.ShippingServiceLabelFile;
				case "CustomerNotes": return this.CustomerNotes;
				case "CreditCardSecurityCode": return this.CreditCardSecurityCode;
				case "OrderNotes": return this.OrderNotes;
				case "ShippingServiceType": return this.ShippingServiceType;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem IsDeleted
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.IsDeleted, esSystemType.Boolean); }
		} 
		
		public esQueryItem UserId
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.UserId, esSystemType.Int32); }
		} 
		
		public esQueryItem OrderNumber
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.OrderNumber, esSystemType.String); }
		} 
		
		public esQueryItem OrderStatusId
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.OrderStatusId, esSystemType.Int16); }
		} 
		
		public esQueryItem PaymentStatusId
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.PaymentStatusId, esSystemType.Int16); }
		} 
		
		public esQueryItem CustomerFirstName
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CustomerFirstName, esSystemType.String); }
		} 
		
		public esQueryItem CustomerLastName
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CustomerLastName, esSystemType.String); }
		} 
		
		public esQueryItem CustomerEmail
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CustomerEmail, esSystemType.String); }
		} 
		
		public esQueryItem BillAddress1
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.BillAddress1, esSystemType.String); }
		} 
		
		public esQueryItem BillAddress2
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.BillAddress2, esSystemType.String); }
		} 
		
		public esQueryItem BillCity
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.BillCity, esSystemType.String); }
		} 
		
		public esQueryItem BillRegion
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.BillRegion, esSystemType.String); }
		} 
		
		public esQueryItem BillPostalCode
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.BillPostalCode, esSystemType.String); }
		} 
		
		public esQueryItem BillCountryCode
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.BillCountryCode, esSystemType.String); }
		} 
		
		public esQueryItem BillTelephone
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.BillTelephone, esSystemType.String); }
		} 
		
		public esQueryItem ShipRecipientName
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipRecipientName, esSystemType.String); }
		} 
		
		public esQueryItem ShipRecipientBusinessName
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipRecipientBusinessName, esSystemType.String); }
		} 
		
		public esQueryItem ShipAddress1
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipAddress1, esSystemType.String); }
		} 
		
		public esQueryItem ShipAddress2
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipAddress2, esSystemType.String); }
		} 
		
		public esQueryItem ShipCity
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipCity, esSystemType.String); }
		} 
		
		public esQueryItem ShipRegion
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipRegion, esSystemType.String); }
		} 
		
		public esQueryItem ShipPostalCode
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipPostalCode, esSystemType.String); }
		} 
		
		public esQueryItem ShipCountryCode
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipCountryCode, esSystemType.String); }
		} 
		
		public esQueryItem ShipTelephone
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShipTelephone, esSystemType.String); }
		} 
		
		public esQueryItem CreditCardType
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreditCardType, esSystemType.String); }
		} 
		
		public esQueryItem CreditCardNumberLast4
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreditCardNumberLast4, esSystemType.String); }
		} 
		
		public esQueryItem CreditCardNumberEncrypted
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreditCardNumberEncrypted, esSystemType.String); }
		} 
		
		public esQueryItem CreditCardExpiration
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreditCardExpiration, esSystemType.String); }
		} 
		
		public esQueryItem CreditCardNameOnCard
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreditCardNameOnCard, esSystemType.String); }
		} 
		
		public esQueryItem ShippingServiceOption
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShippingServiceOption, esSystemType.String); }
		} 
		
		public esQueryItem SubTotal
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.SubTotal, esSystemType.Decimal); }
		} 
		
		public esQueryItem ShippingAmount
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShippingAmount, esSystemType.Decimal); }
		} 
		
		public esQueryItem DiscountAmount
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.DiscountAmount, esSystemType.Decimal); }
		} 
		
		public esQueryItem TaxAmount
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.TaxAmount, esSystemType.Decimal); }
		} 
		
		public esQueryItem Total
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.Total, esSystemType.Decimal); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem CreatedByIP
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreatedByIP, esSystemType.String); }
		} 
		
		public esQueryItem CreatedFromCartId
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreatedFromCartId, esSystemType.Guid); }
		} 
		
		public esQueryItem ModifiedOn
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ModifiedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem ShippingServiceProvider
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShippingServiceProvider, esSystemType.String); }
		} 
		
		public esQueryItem ShippingServicePrice
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShippingServicePrice, esSystemType.Decimal); }
		} 
		
		public esQueryItem ShippingServiceTrackingNumber
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShippingServiceTrackingNumber, esSystemType.String); }
		} 
		
		public esQueryItem ShippingServiceLabelFile
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShippingServiceLabelFile, esSystemType.String); }
		} 
		
		public esQueryItem CustomerNotes
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CustomerNotes, esSystemType.String); }
		} 
		
		public esQueryItem CreditCardSecurityCode
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.CreditCardSecurityCode, esSystemType.String); }
		} 
		
		public esQueryItem OrderNotes
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.OrderNotes, esSystemType.String); }
		} 
		
		public esQueryItem ShippingServiceType
		{
			get { return new esQueryItem(this, OrderMetadata.ColumnNames.ShippingServiceType, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class Order : esOrder
	{

		#region OrderCouponCollectionByOrderId - Zero To Many
		
		static public esPrefetchMap Prefetch_OrderCouponCollectionByOrderId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Order.OrderCouponCollectionByOrderId_Delegate;
				map.PropertyName = "OrderCouponCollectionByOrderId";
				map.MyColumnName = "OrderId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void OrderCouponCollectionByOrderId_Delegate(esPrefetchParameters data)
		{
			OrderQuery parent = new OrderQuery(data.NextAlias());

			OrderCouponQuery me = data.You != null ? data.You as OrderCouponQuery : new OrderCouponQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.OrderId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_OrderCoupon_DNNspot_Store_Order
		/// </summary>

		[XmlIgnore]
		public OrderCouponCollection OrderCouponCollectionByOrderId
		{
			get
			{
				if(this._OrderCouponCollectionByOrderId == null)
				{
					this._OrderCouponCollectionByOrderId = new OrderCouponCollection();
					this._OrderCouponCollectionByOrderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("OrderCouponCollectionByOrderId", this._OrderCouponCollectionByOrderId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._OrderCouponCollectionByOrderId.Query.Where(this._OrderCouponCollectionByOrderId.Query.OrderId == this.Id);
							this._OrderCouponCollectionByOrderId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._OrderCouponCollectionByOrderId.fks.Add(OrderCouponMetadata.ColumnNames.OrderId, this.Id);
					}
				}

				return this._OrderCouponCollectionByOrderId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._OrderCouponCollectionByOrderId != null) 
				{ 
					this.RemovePostSave("OrderCouponCollectionByOrderId"); 
					this._OrderCouponCollectionByOrderId = null;
					
				} 
			} 			
		}
			
		
		private OrderCouponCollection _OrderCouponCollectionByOrderId;
		#endregion

		#region OrderItemCollectionByOrderId - Zero To Many
		
		static public esPrefetchMap Prefetch_OrderItemCollectionByOrderId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Order.OrderItemCollectionByOrderId_Delegate;
				map.PropertyName = "OrderItemCollectionByOrderId";
				map.MyColumnName = "OrderId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void OrderItemCollectionByOrderId_Delegate(esPrefetchParameters data)
		{
			OrderQuery parent = new OrderQuery(data.NextAlias());

			OrderItemQuery me = data.You != null ? data.You as OrderItemQuery : new OrderItemQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.OrderId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_OrderItem_DNNspot_Store_Order
		/// </summary>

		[XmlIgnore]
		public OrderItemCollection OrderItemCollectionByOrderId
		{
			get
			{
				if(this._OrderItemCollectionByOrderId == null)
				{
					this._OrderItemCollectionByOrderId = new OrderItemCollection();
					this._OrderItemCollectionByOrderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("OrderItemCollectionByOrderId", this._OrderItemCollectionByOrderId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._OrderItemCollectionByOrderId.Query.Where(this._OrderItemCollectionByOrderId.Query.OrderId == this.Id);
							this._OrderItemCollectionByOrderId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._OrderItemCollectionByOrderId.fks.Add(OrderItemMetadata.ColumnNames.OrderId, this.Id);
					}
				}

				return this._OrderItemCollectionByOrderId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._OrderItemCollectionByOrderId != null) 
				{ 
					this.RemovePostSave("OrderItemCollectionByOrderId"); 
					this._OrderItemCollectionByOrderId = null;
					
				} 
			} 			
		}
			
		
		private OrderItemCollection _OrderItemCollectionByOrderId;
		#endregion

		#region PaymentTransactionCollectionByOrderId - Zero To Many
		
		static public esPrefetchMap Prefetch_PaymentTransactionCollectionByOrderId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Order.PaymentTransactionCollectionByOrderId_Delegate;
				map.PropertyName = "PaymentTransactionCollectionByOrderId";
				map.MyColumnName = "OrderId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void PaymentTransactionCollectionByOrderId_Delegate(esPrefetchParameters data)
		{
			OrderQuery parent = new OrderQuery(data.NextAlias());

			PaymentTransactionQuery me = data.You != null ? data.You as PaymentTransactionQuery : new PaymentTransactionQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.OrderId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_PaymentTransaction_DNNspot_Store_Order
		/// </summary>

		[XmlIgnore]
		public PaymentTransactionCollection PaymentTransactionCollectionByOrderId
		{
			get
			{
				if(this._PaymentTransactionCollectionByOrderId == null)
				{
					this._PaymentTransactionCollectionByOrderId = new PaymentTransactionCollection();
					this._PaymentTransactionCollectionByOrderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("PaymentTransactionCollectionByOrderId", this._PaymentTransactionCollectionByOrderId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._PaymentTransactionCollectionByOrderId.Query.Where(this._PaymentTransactionCollectionByOrderId.Query.OrderId == this.Id);
							this._PaymentTransactionCollectionByOrderId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._PaymentTransactionCollectionByOrderId.fks.Add(PaymentTransactionMetadata.ColumnNames.OrderId, this.Id);
					}
				}

				return this._PaymentTransactionCollectionByOrderId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._PaymentTransactionCollectionByOrderId != null) 
				{ 
					this.RemovePostSave("PaymentTransactionCollectionByOrderId"); 
					this._PaymentTransactionCollectionByOrderId = null;
					
				} 
			} 			
		}
			
		
		private PaymentTransactionCollection _PaymentTransactionCollectionByOrderId;
		#endregion

				
		#region UpToOrderStatusByOrderStatusId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Order_DNNspot_Store_OrderStatus
		/// </summary>

		[XmlIgnore]
					
		public OrderStatus UpToOrderStatusByOrderStatusId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToOrderStatusByOrderStatusId == null && OrderStatusId != null)
				{
					this._UpToOrderStatusByOrderStatusId = new OrderStatus();
					this._UpToOrderStatusByOrderStatusId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToOrderStatusByOrderStatusId", this._UpToOrderStatusByOrderStatusId);
					this._UpToOrderStatusByOrderStatusId.Query.Where(this._UpToOrderStatusByOrderStatusId.Query.Id == this.OrderStatusId);
					this._UpToOrderStatusByOrderStatusId.Query.Load();
				}	
				return this._UpToOrderStatusByOrderStatusId;
			}
			
			set
			{
				this.RemovePreSave("UpToOrderStatusByOrderStatusId");
				

				if(value == null)
				{
					this.OrderStatusId = null;
					this._UpToOrderStatusByOrderStatusId = null;
				}
				else
				{
					this.OrderStatusId = value.Id;
					this._UpToOrderStatusByOrderStatusId = value;
					this.SetPreSave("UpToOrderStatusByOrderStatusId", this._UpToOrderStatusByOrderStatusId);
				}
				
			}
		}
		#endregion
		

				
		#region UpToPaymentStatusByPaymentStatusId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Order_DNNspot_Store_PaymentStatus
		/// </summary>

		[XmlIgnore]
					
		public PaymentStatus UpToPaymentStatusByPaymentStatusId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToPaymentStatusByPaymentStatusId == null && PaymentStatusId != null)
				{
					this._UpToPaymentStatusByPaymentStatusId = new PaymentStatus();
					this._UpToPaymentStatusByPaymentStatusId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToPaymentStatusByPaymentStatusId", this._UpToPaymentStatusByPaymentStatusId);
					this._UpToPaymentStatusByPaymentStatusId.Query.Where(this._UpToPaymentStatusByPaymentStatusId.Query.Id == this.PaymentStatusId);
					this._UpToPaymentStatusByPaymentStatusId.Query.Load();
				}	
				return this._UpToPaymentStatusByPaymentStatusId;
			}
			
			set
			{
				this.RemovePreSave("UpToPaymentStatusByPaymentStatusId");
				

				if(value == null)
				{
					this.PaymentStatusId = null;
					this._UpToPaymentStatusByPaymentStatusId = null;
				}
				else
				{
					this.PaymentStatusId = value.Id;
					this._UpToPaymentStatusByPaymentStatusId = value;
					this.SetPreSave("UpToPaymentStatusByPaymentStatusId", this._UpToPaymentStatusByPaymentStatusId);
				}
				
			}
		}
		#endregion
		

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Order_DNNspot_Store_Store
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
				case "OrderCouponCollectionByOrderId":
					coll = this.OrderCouponCollectionByOrderId;
					break;
				case "OrderItemCollectionByOrderId":
					coll = this.OrderItemCollectionByOrderId;
					break;
				case "PaymentTransactionCollectionByOrderId":
					coll = this.PaymentTransactionCollectionByOrderId;
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
			
			props.Add(new esPropertyDescriptor(this, "OrderCouponCollectionByOrderId", typeof(OrderCouponCollection), new OrderCoupon()));
			props.Add(new esPropertyDescriptor(this, "OrderItemCollectionByOrderId", typeof(OrderItemCollection), new OrderItem()));
			props.Add(new esPropertyDescriptor(this, "PaymentTransactionCollectionByOrderId", typeof(PaymentTransactionCollection), new PaymentTransaction()));
		
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
			if(this._OrderCouponCollectionByOrderId != null)
			{
				Apply(this._OrderCouponCollectionByOrderId, "OrderId", this.Id);
			}
			if(this._OrderItemCollectionByOrderId != null)
			{
				Apply(this._OrderItemCollectionByOrderId, "OrderId", this.Id);
			}
			if(this._PaymentTransactionCollectionByOrderId != null)
			{
				Apply(this._PaymentTransactionCollectionByOrderId, "OrderId", this.Id);
			}
		}
		
	}
	



	[Serializable]
	public partial class OrderMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OrderMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OrderMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.StoreId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.IsDeleted, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = OrderMetadata.PropertyNames.IsDeleted;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.UserId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderMetadata.PropertyNames.UserId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.OrderNumber, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.OrderNumber;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.OrderStatusId, 5, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = OrderMetadata.PropertyNames.OrderStatusId;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.PaymentStatusId, 6, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = OrderMetadata.PropertyNames.PaymentStatusId;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CustomerFirstName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CustomerFirstName;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CustomerLastName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CustomerLastName;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CustomerEmail, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CustomerEmail;
			c.CharacterMaxLength = 400;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.BillAddress1, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.BillAddress1;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.BillAddress2, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.BillAddress2;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.BillCity, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.BillCity;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.BillRegion, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.BillRegion;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.BillPostalCode, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.BillPostalCode;
			c.CharacterMaxLength = 20;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.BillCountryCode, 15, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.BillCountryCode;
			c.CharacterMaxLength = 2;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.BillTelephone, 16, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.BillTelephone;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipRecipientName, 17, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipRecipientName;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipRecipientBusinessName, 18, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipRecipientBusinessName;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipAddress1, 19, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipAddress1;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipAddress2, 20, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipAddress2;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipCity, 21, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipCity;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipRegion, 22, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipRegion;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipPostalCode, 23, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipPostalCode;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipCountryCode, 24, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipCountryCode;
			c.CharacterMaxLength = 2;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShipTelephone, 25, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShipTelephone;
			c.CharacterMaxLength = 30;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreditCardType, 26, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CreditCardType;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreditCardNumberLast4, 27, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CreditCardNumberLast4;
			c.CharacterMaxLength = 4;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreditCardNumberEncrypted, 28, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CreditCardNumberEncrypted;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreditCardExpiration, 29, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CreditCardExpiration;
			c.CharacterMaxLength = 10;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreditCardNameOnCard, 30, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CreditCardNameOnCard;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShippingServiceOption, 31, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShippingServiceOption;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.SubTotal, 32, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderMetadata.PropertyNames.SubTotal;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShippingAmount, 33, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderMetadata.PropertyNames.ShippingAmount;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.DiscountAmount, 34, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderMetadata.PropertyNames.DiscountAmount;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.TaxAmount, 35, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderMetadata.PropertyNames.TaxAmount;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.Total, 36, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderMetadata.PropertyNames.Total;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreatedOn, 37, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrderMetadata.PropertyNames.CreatedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreatedByIP, 38, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CreatedByIP;
			c.CharacterMaxLength = 15;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreatedFromCartId, 39, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = OrderMetadata.PropertyNames.CreatedFromCartId;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ModifiedOn, 40, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = OrderMetadata.PropertyNames.ModifiedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShippingServiceProvider, 41, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShippingServiceProvider;
			c.CharacterMaxLength = 200;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShippingServicePrice, 42, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderMetadata.PropertyNames.ShippingServicePrice;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShippingServiceTrackingNumber, 43, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShippingServiceTrackingNumber;
			c.CharacterMaxLength = 200;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShippingServiceLabelFile, 44, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShippingServiceLabelFile;
			c.CharacterMaxLength = 300;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CustomerNotes, 45, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CustomerNotes;
			c.CharacterMaxLength = 1073741823;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.CreditCardSecurityCode, 46, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.CreditCardSecurityCode;
			c.CharacterMaxLength = 5;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.OrderNotes, 47, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.OrderNotes;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderMetadata.ColumnNames.ShippingServiceType, 48, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderMetadata.PropertyNames.ShippingServiceType;
			c.CharacterMaxLength = 150;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public OrderMetadata Meta()
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
			 public const string IsDeleted = "IsDeleted";
			 public const string UserId = "UserId";
			 public const string OrderNumber = "OrderNumber";
			 public const string OrderStatusId = "OrderStatusId";
			 public const string PaymentStatusId = "PaymentStatusId";
			 public const string CustomerFirstName = "CustomerFirstName";
			 public const string CustomerLastName = "CustomerLastName";
			 public const string CustomerEmail = "CustomerEmail";
			 public const string BillAddress1 = "BillAddress1";
			 public const string BillAddress2 = "BillAddress2";
			 public const string BillCity = "BillCity";
			 public const string BillRegion = "BillRegion";
			 public const string BillPostalCode = "BillPostalCode";
			 public const string BillCountryCode = "BillCountryCode";
			 public const string BillTelephone = "BillTelephone";
			 public const string ShipRecipientName = "ShipRecipientName";
			 public const string ShipRecipientBusinessName = "ShipRecipientBusinessName";
			 public const string ShipAddress1 = "ShipAddress1";
			 public const string ShipAddress2 = "ShipAddress2";
			 public const string ShipCity = "ShipCity";
			 public const string ShipRegion = "ShipRegion";
			 public const string ShipPostalCode = "ShipPostalCode";
			 public const string ShipCountryCode = "ShipCountryCode";
			 public const string ShipTelephone = "ShipTelephone";
			 public const string CreditCardType = "CreditCardType";
			 public const string CreditCardNumberLast4 = "CreditCardNumberLast4";
			 public const string CreditCardNumberEncrypted = "CreditCardNumberEncrypted";
			 public const string CreditCardExpiration = "CreditCardExpiration";
			 public const string CreditCardNameOnCard = "CreditCardNameOnCard";
			 public const string ShippingServiceOption = "ShippingServiceOption";
			 public const string SubTotal = "SubTotal";
			 public const string ShippingAmount = "ShippingAmount";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string TaxAmount = "TaxAmount";
			 public const string Total = "Total";
			 public const string CreatedOn = "CreatedOn";
			 public const string CreatedByIP = "CreatedByIP";
			 public const string CreatedFromCartId = "CreatedFromCartId";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string ShippingServiceProvider = "ShippingServiceProvider";
			 public const string ShippingServicePrice = "ShippingServicePrice";
			 public const string ShippingServiceTrackingNumber = "ShippingServiceTrackingNumber";
			 public const string ShippingServiceLabelFile = "ShippingServiceLabelFile";
			 public const string CustomerNotes = "CustomerNotes";
			 public const string CreditCardSecurityCode = "CreditCardSecurityCode";
			 public const string OrderNotes = "OrderNotes";
			 public const string ShippingServiceType = "ShippingServiceType";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string StoreId = "StoreId";
			 public const string IsDeleted = "IsDeleted";
			 public const string UserId = "UserId";
			 public const string OrderNumber = "OrderNumber";
			 public const string OrderStatusId = "OrderStatusId";
			 public const string PaymentStatusId = "PaymentStatusId";
			 public const string CustomerFirstName = "CustomerFirstName";
			 public const string CustomerLastName = "CustomerLastName";
			 public const string CustomerEmail = "CustomerEmail";
			 public const string BillAddress1 = "BillAddress1";
			 public const string BillAddress2 = "BillAddress2";
			 public const string BillCity = "BillCity";
			 public const string BillRegion = "BillRegion";
			 public const string BillPostalCode = "BillPostalCode";
			 public const string BillCountryCode = "BillCountryCode";
			 public const string BillTelephone = "BillTelephone";
			 public const string ShipRecipientName = "ShipRecipientName";
			 public const string ShipRecipientBusinessName = "ShipRecipientBusinessName";
			 public const string ShipAddress1 = "ShipAddress1";
			 public const string ShipAddress2 = "ShipAddress2";
			 public const string ShipCity = "ShipCity";
			 public const string ShipRegion = "ShipRegion";
			 public const string ShipPostalCode = "ShipPostalCode";
			 public const string ShipCountryCode = "ShipCountryCode";
			 public const string ShipTelephone = "ShipTelephone";
			 public const string CreditCardType = "CreditCardType";
			 public const string CreditCardNumberLast4 = "CreditCardNumberLast4";
			 public const string CreditCardNumberEncrypted = "CreditCardNumberEncrypted";
			 public const string CreditCardExpiration = "CreditCardExpiration";
			 public const string CreditCardNameOnCard = "CreditCardNameOnCard";
			 public const string ShippingServiceOption = "ShippingServiceOption";
			 public const string SubTotal = "SubTotal";
			 public const string ShippingAmount = "ShippingAmount";
			 public const string DiscountAmount = "DiscountAmount";
			 public const string TaxAmount = "TaxAmount";
			 public const string Total = "Total";
			 public const string CreatedOn = "CreatedOn";
			 public const string CreatedByIP = "CreatedByIP";
			 public const string CreatedFromCartId = "CreatedFromCartId";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string ShippingServiceProvider = "ShippingServiceProvider";
			 public const string ShippingServicePrice = "ShippingServicePrice";
			 public const string ShippingServiceTrackingNumber = "ShippingServiceTrackingNumber";
			 public const string ShippingServiceLabelFile = "ShippingServiceLabelFile";
			 public const string CustomerNotes = "CustomerNotes";
			 public const string CreditCardSecurityCode = "CreditCardSecurityCode";
			 public const string OrderNotes = "OrderNotes";
			 public const string ShippingServiceType = "ShippingServiceType";
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
			lock (typeof(OrderMetadata))
			{
				if(OrderMetadata.mapDelegates == null)
				{
					OrderMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OrderMetadata.meta == null)
				{
					OrderMetadata.meta = new OrderMetadata();
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
				meta.AddTypeMap("IsDeleted", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("UserId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("OrderNumber", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("OrderStatusId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("PaymentStatusId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("CustomerFirstName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CustomerLastName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CustomerEmail", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BillAddress1", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BillAddress2", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BillCity", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BillRegion", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BillPostalCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BillCountryCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BillTelephone", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipRecipientName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipRecipientBusinessName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipAddress1", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipAddress2", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipCity", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipRegion", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipPostalCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipCountryCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShipTelephone", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CreditCardType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreditCardNumberLast4", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreditCardNumberEncrypted", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CreditCardExpiration", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreditCardNameOnCard", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShippingServiceOption", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SubTotal", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ShippingAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("DiscountAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("TaxAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Total", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreatedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("CreatedByIP", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CreatedFromCartId", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("ModifiedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ShippingServiceProvider", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShippingServicePrice", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ShippingServiceTrackingNumber", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShippingServiceLabelFile", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CustomerNotes", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CreditCardSecurityCode", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderNotes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ShippingServiceType", new esTypeMap("varchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Order";
					meta.Destination = objectQualifier + "DNNspot_Store_Order";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_OrderInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_OrderUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_OrderDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_OrderLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_OrderLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Order";
					meta.Destination = "DNNspot_Store_Order";
									
					meta.spInsert = "proc_DNNspot_Store_OrderInsert";				
					meta.spUpdate = "proc_DNNspot_Store_OrderUpdate";		
					meta.spDelete = "proc_DNNspot_Store_OrderDelete";
					meta.spLoadAll = "proc_DNNspot_Store_OrderLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_OrderLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OrderMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
