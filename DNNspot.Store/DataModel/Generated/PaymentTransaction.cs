
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
	/// Encapsulates the 'DNNspot_Store_PaymentTransaction' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(PaymentTransaction))]	
	[XmlType("PaymentTransaction")]
	[Table(Name="PaymentTransaction")]
	public partial class PaymentTransaction : esPaymentTransaction
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new PaymentTransaction();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Guid id)
		{
			var obj = new PaymentTransaction();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Guid id, esSqlAccessType sqlAccessType)
		{
			var obj = new PaymentTransaction();
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

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? OrderId
		{
			get { return base.OrderId;  }
			set { base.OrderId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? PaymentProviderId
		{
			get { return base.PaymentProviderId;  }
			set { base.PaymentProviderId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String GatewayUrl
		{
			get { return base.GatewayUrl;  }
			set { base.GatewayUrl = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String GatewayTransactionId
		{
			get { return base.GatewayTransactionId;  }
			set { base.GatewayTransactionId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String GatewayResponse
		{
			get { return base.GatewayResponse;  }
			set { base.GatewayResponse = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String GatewayDebugResponse
		{
			get { return base.GatewayDebugResponse;  }
			set { base.GatewayDebugResponse = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String GatewayError
		{
			get { return base.GatewayError;  }
			set { base.GatewayError = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? Amount
		{
			get { return base.Amount;  }
			set { base.Amount = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.DateTime? CreatedOn
		{
			get { return base.CreatedOn;  }
			set { base.CreatedOn = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("PaymentTransactionCollection")]
	public partial class PaymentTransactionCollection : esPaymentTransactionCollection, IEnumerable<PaymentTransaction>
	{
		public PaymentTransaction FindByPrimaryKey(System.Guid id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(PaymentTransaction))]
		public class PaymentTransactionCollectionWCFPacket : esCollectionWCFPacket<PaymentTransactionCollection>
		{
			public static implicit operator PaymentTransactionCollection(PaymentTransactionCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator PaymentTransactionCollectionWCFPacket(PaymentTransactionCollection collection)
			{
				return new PaymentTransactionCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class PaymentTransactionQuery : esPaymentTransactionQuery
	{
		public PaymentTransactionQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "PaymentTransactionQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(PaymentTransactionQuery query)
		{
			return PaymentTransactionQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator PaymentTransactionQuery(string query)
		{
			return (PaymentTransactionQuery)PaymentTransactionQuery.SerializeHelper.FromXml(query, typeof(PaymentTransactionQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esPaymentTransaction : esEntity
	{
		public esPaymentTransaction()
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
			PaymentTransactionQuery query = new PaymentTransactionQuery();
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
		/// Maps to DNNspot_Store_PaymentTransaction.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Guid? Id
		{
			get
			{
				return base.GetSystemGuid(PaymentTransactionMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemGuid(PaymentTransactionMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.OrderId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? OrderId
		{
			get
			{
				return base.GetSystemInt32(PaymentTransactionMetadata.ColumnNames.OrderId);
			}
			
			set
			{
				if(base.SetSystemInt32(PaymentTransactionMetadata.ColumnNames.OrderId, value))
				{
					this._UpToOrderByOrderId = null;
					this.OnPropertyChanged("UpToOrderByOrderId");
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.OrderId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.PaymentProviderId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? PaymentProviderId
		{
			get
			{
				return base.GetSystemInt16(PaymentTransactionMetadata.ColumnNames.PaymentProviderId);
			}
			
			set
			{
				if(base.SetSystemInt16(PaymentTransactionMetadata.ColumnNames.PaymentProviderId, value))
				{
					this._UpToPaymentProviderByPaymentProviderId = null;
					this.OnPropertyChanged("UpToPaymentProviderByPaymentProviderId");
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.PaymentProviderId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.GatewayUrl
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String GatewayUrl
		{
			get
			{
				return base.GetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayUrl);
			}
			
			set
			{
				if(base.SetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayUrl, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.GatewayUrl);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.GatewayTransactionId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String GatewayTransactionId
		{
			get
			{
				return base.GetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayTransactionId);
			}
			
			set
			{
				if(base.SetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayTransactionId, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.GatewayTransactionId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.GatewayResponse
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String GatewayResponse
		{
			get
			{
				return base.GetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayResponse);
			}
			
			set
			{
				if(base.SetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayResponse, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.GatewayResponse);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.GatewayDebugResponse
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String GatewayDebugResponse
		{
			get
			{
				return base.GetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayDebugResponse);
			}
			
			set
			{
				if(base.SetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayDebugResponse, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.GatewayDebugResponse);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.GatewayError
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String GatewayError
		{
			get
			{
				return base.GetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayError);
			}
			
			set
			{
				if(base.SetSystemString(PaymentTransactionMetadata.ColumnNames.GatewayError, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.GatewayError);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.Amount
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Amount
		{
			get
			{
				return base.GetSystemDecimal(PaymentTransactionMetadata.ColumnNames.Amount);
			}
			
			set
			{
				if(base.SetSystemDecimal(PaymentTransactionMetadata.ColumnNames.Amount, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.Amount);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentTransaction.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(PaymentTransactionMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(PaymentTransactionMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.CreatedOn);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Order _UpToOrderByOrderId;
		[CLSCompliant(false)]
		internal protected PaymentProvider _UpToPaymentProviderByPaymentProviderId;
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
						case "OrderId": this.str().OrderId = (string)value; break;							
						case "PaymentProviderId": this.str().PaymentProviderId = (string)value; break;							
						case "GatewayUrl": this.str().GatewayUrl = (string)value; break;							
						case "GatewayTransactionId": this.str().GatewayTransactionId = (string)value; break;							
						case "GatewayResponse": this.str().GatewayResponse = (string)value; break;							
						case "GatewayDebugResponse": this.str().GatewayDebugResponse = (string)value; break;							
						case "GatewayError": this.str().GatewayError = (string)value; break;							
						case "Amount": this.str().Amount = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Guid)
								this.Id = (System.Guid?)value;
								OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.Id);
							break;
						
						case "OrderId":
						
							if (value == null || value is System.Int32)
								this.OrderId = (System.Int32?)value;
								OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.OrderId);
							break;
						
						case "PaymentProviderId":
						
							if (value == null || value is System.Int16)
								this.PaymentProviderId = (System.Int16?)value;
								OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.PaymentProviderId);
							break;
						
						case "Amount":
						
							if (value == null || value is System.Decimal)
								this.Amount = (System.Decimal?)value;
								OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.Amount);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(PaymentTransactionMetadata.PropertyNames.CreatedOn);
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
			public esStrings(esPaymentTransaction entity)
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
				
			public System.String PaymentProviderId
			{
				get
				{
					System.Int16? data = entity.PaymentProviderId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PaymentProviderId = null;
					else entity.PaymentProviderId = Convert.ToInt16(value);
				}
			}
				
			public System.String GatewayUrl
			{
				get
				{
					System.String data = entity.GatewayUrl;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GatewayUrl = null;
					else entity.GatewayUrl = Convert.ToString(value);
				}
			}
				
			public System.String GatewayTransactionId
			{
				get
				{
					System.String data = entity.GatewayTransactionId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GatewayTransactionId = null;
					else entity.GatewayTransactionId = Convert.ToString(value);
				}
			}
				
			public System.String GatewayResponse
			{
				get
				{
					System.String data = entity.GatewayResponse;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GatewayResponse = null;
					else entity.GatewayResponse = Convert.ToString(value);
				}
			}
				
			public System.String GatewayDebugResponse
			{
				get
				{
					System.String data = entity.GatewayDebugResponse;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GatewayDebugResponse = null;
					else entity.GatewayDebugResponse = Convert.ToString(value);
				}
			}
				
			public System.String GatewayError
			{
				get
				{
					System.String data = entity.GatewayError;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GatewayError = null;
					else entity.GatewayError = Convert.ToString(value);
				}
			}
				
			public System.String Amount
			{
				get
				{
					System.Decimal? data = entity.Amount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Amount = null;
					else entity.Amount = Convert.ToDecimal(value);
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
			

			private esPaymentTransaction entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return PaymentTransactionMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public PaymentTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentTransactionQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PaymentTransactionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(PaymentTransactionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private PaymentTransactionQuery query;		
	}



	[Serializable]
	abstract public partial class esPaymentTransactionCollection : esEntityCollection<PaymentTransaction>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PaymentTransactionMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "PaymentTransactionCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public PaymentTransactionQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentTransactionQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PaymentTransactionQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PaymentTransactionQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(PaymentTransactionQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((PaymentTransactionQuery)query);
		}

		#endregion
		
		private PaymentTransactionQuery query;
	}



	[Serializable]
	abstract public partial class esPaymentTransactionQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PaymentTransactionMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "OrderId": return this.OrderId;
				case "PaymentProviderId": return this.PaymentProviderId;
				case "GatewayUrl": return this.GatewayUrl;
				case "GatewayTransactionId": return this.GatewayTransactionId;
				case "GatewayResponse": return this.GatewayResponse;
				case "GatewayDebugResponse": return this.GatewayDebugResponse;
				case "GatewayError": return this.GatewayError;
				case "Amount": return this.Amount;
				case "CreatedOn": return this.CreatedOn;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.Id, esSystemType.Guid); }
		} 
		
		public esQueryItem OrderId
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.OrderId, esSystemType.Int32); }
		} 
		
		public esQueryItem PaymentProviderId
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.PaymentProviderId, esSystemType.Int16); }
		} 
		
		public esQueryItem GatewayUrl
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.GatewayUrl, esSystemType.String); }
		} 
		
		public esQueryItem GatewayTransactionId
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.GatewayTransactionId, esSystemType.String); }
		} 
		
		public esQueryItem GatewayResponse
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.GatewayResponse, esSystemType.String); }
		} 
		
		public esQueryItem GatewayDebugResponse
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.GatewayDebugResponse, esSystemType.String); }
		} 
		
		public esQueryItem GatewayError
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.GatewayError, esSystemType.String); }
		} 
		
		public esQueryItem Amount
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.Amount, esSystemType.Decimal); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, PaymentTransactionMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		#endregion
		
	}


	
	public partial class PaymentTransaction : esPaymentTransaction
	{

				
		#region UpToOrderByOrderId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_PaymentTransaction_DNNspot_Store_Order
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
		

				
		#region UpToPaymentProviderByPaymentProviderId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_PaymentTransaction_DNNspot_Store_PaymentProvider
		/// </summary>

		[XmlIgnore]
					
		public PaymentProvider UpToPaymentProviderByPaymentProviderId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToPaymentProviderByPaymentProviderId == null && PaymentProviderId != null)
				{
					this._UpToPaymentProviderByPaymentProviderId = new PaymentProvider();
					this._UpToPaymentProviderByPaymentProviderId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToPaymentProviderByPaymentProviderId", this._UpToPaymentProviderByPaymentProviderId);
					this._UpToPaymentProviderByPaymentProviderId.Query.Where(this._UpToPaymentProviderByPaymentProviderId.Query.Id == this.PaymentProviderId);
					this._UpToPaymentProviderByPaymentProviderId.Query.Load();
				}	
				return this._UpToPaymentProviderByPaymentProviderId;
			}
			
			set
			{
				this.RemovePreSave("UpToPaymentProviderByPaymentProviderId");
				

				if(value == null)
				{
					this.PaymentProviderId = null;
					this._UpToPaymentProviderByPaymentProviderId = null;
				}
				else
				{
					this.PaymentProviderId = value.Id;
					this._UpToPaymentProviderByPaymentProviderId = value;
					this.SetPreSave("UpToPaymentProviderByPaymentProviderId", this._UpToPaymentProviderByPaymentProviderId);
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
	public partial class PaymentTransactionMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PaymentTransactionMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.Id, 0, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.HasDefault = true;
			c.Default = @"(newid())";
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.OrderId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.OrderId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.PaymentProviderId, 2, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.PaymentProviderId;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.GatewayUrl, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.GatewayUrl;
			c.CharacterMaxLength = 2000;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.GatewayTransactionId, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.GatewayTransactionId;
			c.CharacterMaxLength = 100;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.GatewayResponse, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.GatewayResponse;
			c.CharacterMaxLength = 2000;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.GatewayDebugResponse, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.GatewayDebugResponse;
			c.CharacterMaxLength = 1073741823;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.GatewayError, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.GatewayError;
			c.CharacterMaxLength = 1000;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.Amount, 8, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.Amount;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentTransactionMetadata.ColumnNames.CreatedOn, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = PaymentTransactionMetadata.PropertyNames.CreatedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public PaymentTransactionMetadata Meta()
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
			 public const string OrderId = "OrderId";
			 public const string PaymentProviderId = "PaymentProviderId";
			 public const string GatewayUrl = "GatewayUrl";
			 public const string GatewayTransactionId = "GatewayTransactionId";
			 public const string GatewayResponse = "GatewayResponse";
			 public const string GatewayDebugResponse = "GatewayDebugResponse";
			 public const string GatewayError = "GatewayError";
			 public const string Amount = "Amount";
			 public const string CreatedOn = "CreatedOn";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string OrderId = "OrderId";
			 public const string PaymentProviderId = "PaymentProviderId";
			 public const string GatewayUrl = "GatewayUrl";
			 public const string GatewayTransactionId = "GatewayTransactionId";
			 public const string GatewayResponse = "GatewayResponse";
			 public const string GatewayDebugResponse = "GatewayDebugResponse";
			 public const string GatewayError = "GatewayError";
			 public const string Amount = "Amount";
			 public const string CreatedOn = "CreatedOn";
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
			lock (typeof(PaymentTransactionMetadata))
			{
				if(PaymentTransactionMetadata.mapDelegates == null)
				{
					PaymentTransactionMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PaymentTransactionMetadata.meta == null)
				{
					PaymentTransactionMetadata.meta = new PaymentTransactionMetadata();
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
				meta.AddTypeMap("OrderId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("PaymentProviderId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("GatewayUrl", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("GatewayTransactionId", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("GatewayResponse", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("GatewayDebugResponse", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("GatewayError", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Amount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("CreatedOn", new esTypeMap("datetime", "System.DateTime"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_PaymentTransaction";
					meta.Destination = objectQualifier + "DNNspot_Store_PaymentTransaction";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_PaymentTransactionInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_PaymentTransactionUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_PaymentTransactionDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_PaymentTransactionLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_PaymentTransactionLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_PaymentTransaction";
					meta.Destination = "DNNspot_Store_PaymentTransaction";
									
					meta.spInsert = "proc_DNNspot_Store_PaymentTransactionInsert";				
					meta.spUpdate = "proc_DNNspot_Store_PaymentTransactionUpdate";		
					meta.spDelete = "proc_DNNspot_Store_PaymentTransactionDelete";
					meta.spLoadAll = "proc_DNNspot_Store_PaymentTransactionLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_PaymentTransactionLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PaymentTransactionMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
