
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
	/// Encapsulates the 'DNNspot_Store_Coupon' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Coupon))]	
	[XmlType("Coupon")]
	[Table(Name="Coupon")]
	public partial class Coupon : esCoupon
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Coupon();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new Coupon();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Coupon();
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
		public override System.String Code
		{
			get { return base.Code;  }
			set { base.Code = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DescriptionForCustomer
		{
			get { return base.DescriptionForCustomer;  }
			set { base.DescriptionForCustomer = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsCombinable
		{
			get { return base.IsCombinable;  }
			set { base.IsCombinable = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.DateTime? ValidFromDate
		{
			get { return base.ValidFromDate;  }
			set { base.ValidFromDate = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.DateTime? ValidToDate
		{
			get { return base.ValidToDate;  }
			set { base.ValidToDate = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DiscountType
		{
			get { return base.DiscountType;  }
			set { base.DiscountType = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? MinOrderAmount
		{
			get { return base.MinOrderAmount;  }
			set { base.MinOrderAmount = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? MaxUsesPerUser
		{
			get { return base.MaxUsesPerUser;  }
			set { base.MaxUsesPerUser = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? MaxUsesLifetime
		{
			get { return base.MaxUsesLifetime;  }
			set { base.MaxUsesLifetime = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? MaxDiscountAmountPerOrder
		{
			get { return base.MaxDiscountAmountPerOrder;  }
			set { base.MaxDiscountAmountPerOrder = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String AppliesToProductIds
		{
			get { return base.AppliesToProductIds;  }
			set { base.AppliesToProductIds = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String AppliesToShippingRateTypes
		{
			get { return base.AppliesToShippingRateTypes;  }
			set { base.AppliesToShippingRateTypes = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? PercentOff
		{
			get { return base.PercentOff;  }
			set { base.PercentOff = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? AmountOff
		{
			get { return base.AmountOff;  }
			set { base.AmountOff = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("CouponCollection")]
	public partial class CouponCollection : esCouponCollection, IEnumerable<Coupon>
	{
		public Coupon FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Coupon))]
		public class CouponCollectionWCFPacket : esCollectionWCFPacket<CouponCollection>
		{
			public static implicit operator CouponCollection(CouponCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CouponCollectionWCFPacket(CouponCollection collection)
			{
				return new CouponCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CouponQuery : esCouponQuery
	{
		public CouponQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CouponQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CouponQuery query)
		{
			return CouponQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CouponQuery(string query)
		{
			return (CouponQuery)CouponQuery.SerializeHelper.FromXml(query, typeof(CouponQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCoupon : esEntity
	{
		public esCoupon()
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
			CouponQuery query = new CouponQuery();
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
		/// Maps to DNNspot_Store_Coupon.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(CouponMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(CouponMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(CouponMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(CouponMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(CouponMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.IsActive
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(CouponMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				if(base.SetSystemBoolean(CouponMetadata.ColumnNames.IsActive, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.IsActive);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.Code
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Code
		{
			get
			{
				return base.GetSystemString(CouponMetadata.ColumnNames.Code);
			}
			
			set
			{
				if(base.SetSystemString(CouponMetadata.ColumnNames.Code, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.Code);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.DescriptionForCustomer
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DescriptionForCustomer
		{
			get
			{
				return base.GetSystemString(CouponMetadata.ColumnNames.DescriptionForCustomer);
			}
			
			set
			{
				if(base.SetSystemString(CouponMetadata.ColumnNames.DescriptionForCustomer, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.DescriptionForCustomer);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.IsCombinable
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsCombinable
		{
			get
			{
				return base.GetSystemBoolean(CouponMetadata.ColumnNames.IsCombinable);
			}
			
			set
			{
				if(base.SetSystemBoolean(CouponMetadata.ColumnNames.IsCombinable, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.IsCombinable);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.ValidFromDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ValidFromDate
		{
			get
			{
				return base.GetSystemDateTime(CouponMetadata.ColumnNames.ValidFromDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(CouponMetadata.ColumnNames.ValidFromDate, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.ValidFromDate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.ValidToDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ValidToDate
		{
			get
			{
				return base.GetSystemDateTime(CouponMetadata.ColumnNames.ValidToDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(CouponMetadata.ColumnNames.ValidToDate, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.ValidToDate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.DiscountType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DiscountType
		{
			get
			{
				return base.GetSystemString(CouponMetadata.ColumnNames.DiscountType);
			}
			
			set
			{
				if(base.SetSystemString(CouponMetadata.ColumnNames.DiscountType, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.DiscountType);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.MinOrderAmount
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? MinOrderAmount
		{
			get
			{
				return base.GetSystemDecimal(CouponMetadata.ColumnNames.MinOrderAmount);
			}
			
			set
			{
				if(base.SetSystemDecimal(CouponMetadata.ColumnNames.MinOrderAmount, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.MinOrderAmount);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.MaxUsesPerUser
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? MaxUsesPerUser
		{
			get
			{
				return base.GetSystemInt32(CouponMetadata.ColumnNames.MaxUsesPerUser);
			}
			
			set
			{
				if(base.SetSystemInt32(CouponMetadata.ColumnNames.MaxUsesPerUser, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.MaxUsesPerUser);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.MaxUsesLifetime
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? MaxUsesLifetime
		{
			get
			{
				return base.GetSystemInt32(CouponMetadata.ColumnNames.MaxUsesLifetime);
			}
			
			set
			{
				if(base.SetSystemInt32(CouponMetadata.ColumnNames.MaxUsesLifetime, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.MaxUsesLifetime);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.MaxDiscountAmountPerOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? MaxDiscountAmountPerOrder
		{
			get
			{
				return base.GetSystemDecimal(CouponMetadata.ColumnNames.MaxDiscountAmountPerOrder);
			}
			
			set
			{
				if(base.SetSystemDecimal(CouponMetadata.ColumnNames.MaxDiscountAmountPerOrder, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.MaxDiscountAmountPerOrder);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.AppliesToProductIds
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String AppliesToProductIds
		{
			get
			{
				return base.GetSystemString(CouponMetadata.ColumnNames.AppliesToProductIds);
			}
			
			set
			{
				if(base.SetSystemString(CouponMetadata.ColumnNames.AppliesToProductIds, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.AppliesToProductIds);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.AppliesToShippingRateTypes
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String AppliesToShippingRateTypes
		{
			get
			{
				return base.GetSystemString(CouponMetadata.ColumnNames.AppliesToShippingRateTypes);
			}
			
			set
			{
				if(base.SetSystemString(CouponMetadata.ColumnNames.AppliesToShippingRateTypes, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.AppliesToShippingRateTypes);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.PercentOff
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? PercentOff
		{
			get
			{
				return base.GetSystemDecimal(CouponMetadata.ColumnNames.PercentOff);
			}
			
			set
			{
				if(base.SetSystemDecimal(CouponMetadata.ColumnNames.PercentOff, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.PercentOff);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Coupon.AmountOff
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? AmountOff
		{
			get
			{
				return base.GetSystemDecimal(CouponMetadata.ColumnNames.AmountOff);
			}
			
			set
			{
				if(base.SetSystemDecimal(CouponMetadata.ColumnNames.AmountOff, value))
				{
					OnPropertyChanged(CouponMetadata.PropertyNames.AmountOff);
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
						case "IsActive": this.str().IsActive = (string)value; break;							
						case "Code": this.str().Code = (string)value; break;							
						case "DescriptionForCustomer": this.str().DescriptionForCustomer = (string)value; break;							
						case "IsCombinable": this.str().IsCombinable = (string)value; break;							
						case "ValidFromDate": this.str().ValidFromDate = (string)value; break;							
						case "ValidToDate": this.str().ValidToDate = (string)value; break;							
						case "DiscountType": this.str().DiscountType = (string)value; break;							
						case "MinOrderAmount": this.str().MinOrderAmount = (string)value; break;							
						case "MaxUsesPerUser": this.str().MaxUsesPerUser = (string)value; break;							
						case "MaxUsesLifetime": this.str().MaxUsesLifetime = (string)value; break;							
						case "MaxDiscountAmountPerOrder": this.str().MaxDiscountAmountPerOrder = (string)value; break;							
						case "AppliesToProductIds": this.str().AppliesToProductIds = (string)value; break;							
						case "AppliesToShippingRateTypes": this.str().AppliesToShippingRateTypes = (string)value; break;							
						case "PercentOff": this.str().PercentOff = (string)value; break;							
						case "AmountOff": this.str().AmountOff = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.Id);
							break;
						
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.StoreId);
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.IsActive);
							break;
						
						case "IsCombinable":
						
							if (value == null || value is System.Boolean)
								this.IsCombinable = (System.Boolean?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.IsCombinable);
							break;
						
						case "ValidFromDate":
						
							if (value == null || value is System.DateTime)
								this.ValidFromDate = (System.DateTime?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.ValidFromDate);
							break;
						
						case "ValidToDate":
						
							if (value == null || value is System.DateTime)
								this.ValidToDate = (System.DateTime?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.ValidToDate);
							break;
						
						case "MinOrderAmount":
						
							if (value == null || value is System.Decimal)
								this.MinOrderAmount = (System.Decimal?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.MinOrderAmount);
							break;
						
						case "MaxUsesPerUser":
						
							if (value == null || value is System.Int32)
								this.MaxUsesPerUser = (System.Int32?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.MaxUsesPerUser);
							break;
						
						case "MaxUsesLifetime":
						
							if (value == null || value is System.Int32)
								this.MaxUsesLifetime = (System.Int32?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.MaxUsesLifetime);
							break;
						
						case "MaxDiscountAmountPerOrder":
						
							if (value == null || value is System.Decimal)
								this.MaxDiscountAmountPerOrder = (System.Decimal?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.MaxDiscountAmountPerOrder);
							break;
						
						case "PercentOff":
						
							if (value == null || value is System.Decimal)
								this.PercentOff = (System.Decimal?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.PercentOff);
							break;
						
						case "AmountOff":
						
							if (value == null || value is System.Decimal)
								this.AmountOff = (System.Decimal?)value;
								OnPropertyChanged(CouponMetadata.PropertyNames.AmountOff);
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
			public esStrings(esCoupon entity)
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
				
			public System.String Code
			{
				get
				{
					System.String data = entity.Code;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Code = null;
					else entity.Code = Convert.ToString(value);
				}
			}
				
			public System.String DescriptionForCustomer
			{
				get
				{
					System.String data = entity.DescriptionForCustomer;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DescriptionForCustomer = null;
					else entity.DescriptionForCustomer = Convert.ToString(value);
				}
			}
				
			public System.String IsCombinable
			{
				get
				{
					System.Boolean? data = entity.IsCombinable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsCombinable = null;
					else entity.IsCombinable = Convert.ToBoolean(value);
				}
			}
				
			public System.String ValidFromDate
			{
				get
				{
					System.DateTime? data = entity.ValidFromDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidFromDate = null;
					else entity.ValidFromDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String ValidToDate
			{
				get
				{
					System.DateTime? data = entity.ValidToDate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ValidToDate = null;
					else entity.ValidToDate = Convert.ToDateTime(value);
				}
			}
				
			public System.String DiscountType
			{
				get
				{
					System.String data = entity.DiscountType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DiscountType = null;
					else entity.DiscountType = Convert.ToString(value);
				}
			}
				
			public System.String MinOrderAmount
			{
				get
				{
					System.Decimal? data = entity.MinOrderAmount;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinOrderAmount = null;
					else entity.MinOrderAmount = Convert.ToDecimal(value);
				}
			}
				
			public System.String MaxUsesPerUser
			{
				get
				{
					System.Int32? data = entity.MaxUsesPerUser;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxUsesPerUser = null;
					else entity.MaxUsesPerUser = Convert.ToInt32(value);
				}
			}
				
			public System.String MaxUsesLifetime
			{
				get
				{
					System.Int32? data = entity.MaxUsesLifetime;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxUsesLifetime = null;
					else entity.MaxUsesLifetime = Convert.ToInt32(value);
				}
			}
				
			public System.String MaxDiscountAmountPerOrder
			{
				get
				{
					System.Decimal? data = entity.MaxDiscountAmountPerOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxDiscountAmountPerOrder = null;
					else entity.MaxDiscountAmountPerOrder = Convert.ToDecimal(value);
				}
			}
				
			public System.String AppliesToProductIds
			{
				get
				{
					System.String data = entity.AppliesToProductIds;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppliesToProductIds = null;
					else entity.AppliesToProductIds = Convert.ToString(value);
				}
			}
				
			public System.String AppliesToShippingRateTypes
			{
				get
				{
					System.String data = entity.AppliesToShippingRateTypes;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppliesToShippingRateTypes = null;
					else entity.AppliesToShippingRateTypes = Convert.ToString(value);
				}
			}
				
			public System.String PercentOff
			{
				get
				{
					System.Decimal? data = entity.PercentOff;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PercentOff = null;
					else entity.PercentOff = Convert.ToDecimal(value);
				}
			}
				
			public System.String AmountOff
			{
				get
				{
					System.Decimal? data = entity.AmountOff;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AmountOff = null;
					else entity.AmountOff = Convert.ToDecimal(value);
				}
			}
			

			private esCoupon entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CouponMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CouponQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CouponQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CouponQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CouponQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CouponQuery query;		
	}



	[Serializable]
	abstract public partial class esCouponCollection : esEntityCollection<Coupon>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CouponMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CouponCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CouponQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CouponQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CouponQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CouponQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CouponQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CouponQuery)query);
		}

		#endregion
		
		private CouponQuery query;
	}



	[Serializable]
	abstract public partial class esCouponQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CouponMetadata.Meta();
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
				case "Code": return this.Code;
				case "DescriptionForCustomer": return this.DescriptionForCustomer;
				case "IsCombinable": return this.IsCombinable;
				case "ValidFromDate": return this.ValidFromDate;
				case "ValidToDate": return this.ValidToDate;
				case "DiscountType": return this.DiscountType;
				case "MinOrderAmount": return this.MinOrderAmount;
				case "MaxUsesPerUser": return this.MaxUsesPerUser;
				case "MaxUsesLifetime": return this.MaxUsesLifetime;
				case "MaxDiscountAmountPerOrder": return this.MaxDiscountAmountPerOrder;
				case "AppliesToProductIds": return this.AppliesToProductIds;
				case "AppliesToShippingRateTypes": return this.AppliesToShippingRateTypes;
				case "PercentOff": return this.PercentOff;
				case "AmountOff": return this.AmountOff;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem IsActive
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.IsActive, esSystemType.Boolean); }
		} 
		
		public esQueryItem Code
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.Code, esSystemType.String); }
		} 
		
		public esQueryItem DescriptionForCustomer
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.DescriptionForCustomer, esSystemType.String); }
		} 
		
		public esQueryItem IsCombinable
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.IsCombinable, esSystemType.Boolean); }
		} 
		
		public esQueryItem ValidFromDate
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.ValidFromDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem ValidToDate
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.ValidToDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem DiscountType
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.DiscountType, esSystemType.String); }
		} 
		
		public esQueryItem MinOrderAmount
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.MinOrderAmount, esSystemType.Decimal); }
		} 
		
		public esQueryItem MaxUsesPerUser
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.MaxUsesPerUser, esSystemType.Int32); }
		} 
		
		public esQueryItem MaxUsesLifetime
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.MaxUsesLifetime, esSystemType.Int32); }
		} 
		
		public esQueryItem MaxDiscountAmountPerOrder
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.MaxDiscountAmountPerOrder, esSystemType.Decimal); }
		} 
		
		public esQueryItem AppliesToProductIds
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.AppliesToProductIds, esSystemType.String); }
		} 
		
		public esQueryItem AppliesToShippingRateTypes
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.AppliesToShippingRateTypes, esSystemType.String); }
		} 
		
		public esQueryItem PercentOff
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.PercentOff, esSystemType.Decimal); }
		} 
		
		public esQueryItem AmountOff
		{
			get { return new esQueryItem(this, CouponMetadata.ColumnNames.AmountOff, esSystemType.Decimal); }
		} 
		
		#endregion
		
	}


	
	public partial class Coupon : esCoupon
	{

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Coupon_DNNspot_Store_Store
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
	public partial class CouponMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CouponMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CouponMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CouponMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.StoreId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CouponMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CouponMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.Code, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CouponMetadata.PropertyNames.Code;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.DescriptionForCustomer, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CouponMetadata.PropertyNames.DescriptionForCustomer;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.IsCombinable, 5, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CouponMetadata.PropertyNames.IsCombinable;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.ValidFromDate, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CouponMetadata.PropertyNames.ValidFromDate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.ValidToDate, 7, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = CouponMetadata.PropertyNames.ValidToDate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.DiscountType, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CouponMetadata.PropertyNames.DiscountType;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.MinOrderAmount, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CouponMetadata.PropertyNames.MinOrderAmount;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.MaxUsesPerUser, 10, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CouponMetadata.PropertyNames.MaxUsesPerUser;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.MaxUsesLifetime, 11, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CouponMetadata.PropertyNames.MaxUsesLifetime;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.MaxDiscountAmountPerOrder, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CouponMetadata.PropertyNames.MaxDiscountAmountPerOrder;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.AppliesToProductIds, 13, typeof(System.String), esSystemType.String);
			c.PropertyName = CouponMetadata.PropertyNames.AppliesToProductIds;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.AppliesToShippingRateTypes, 14, typeof(System.String), esSystemType.String);
			c.PropertyName = CouponMetadata.PropertyNames.AppliesToShippingRateTypes;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.PercentOff, 15, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CouponMetadata.PropertyNames.PercentOff;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CouponMetadata.ColumnNames.AmountOff, 16, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = CouponMetadata.PropertyNames.AmountOff;
			c.NumericPrecision = 19;
			c.HasDefault = true;
			c.Default = @"((0))";
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CouponMetadata Meta()
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
			 public const string Code = "Code";
			 public const string DescriptionForCustomer = "DescriptionForCustomer";
			 public const string IsCombinable = "IsCombinable";
			 public const string ValidFromDate = "ValidFromDate";
			 public const string ValidToDate = "ValidToDate";
			 public const string DiscountType = "DiscountType";
			 public const string MinOrderAmount = "MinOrderAmount";
			 public const string MaxUsesPerUser = "MaxUsesPerUser";
			 public const string MaxUsesLifetime = "MaxUsesLifetime";
			 public const string MaxDiscountAmountPerOrder = "MaxDiscountAmountPerOrder";
			 public const string AppliesToProductIds = "AppliesToProductIds";
			 public const string AppliesToShippingRateTypes = "AppliesToShippingRateTypes";
			 public const string PercentOff = "PercentOff";
			 public const string AmountOff = "AmountOff";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string StoreId = "StoreId";
			 public const string IsActive = "IsActive";
			 public const string Code = "Code";
			 public const string DescriptionForCustomer = "DescriptionForCustomer";
			 public const string IsCombinable = "IsCombinable";
			 public const string ValidFromDate = "ValidFromDate";
			 public const string ValidToDate = "ValidToDate";
			 public const string DiscountType = "DiscountType";
			 public const string MinOrderAmount = "MinOrderAmount";
			 public const string MaxUsesPerUser = "MaxUsesPerUser";
			 public const string MaxUsesLifetime = "MaxUsesLifetime";
			 public const string MaxDiscountAmountPerOrder = "MaxDiscountAmountPerOrder";
			 public const string AppliesToProductIds = "AppliesToProductIds";
			 public const string AppliesToShippingRateTypes = "AppliesToShippingRateTypes";
			 public const string PercentOff = "PercentOff";
			 public const string AmountOff = "AmountOff";
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
			lock (typeof(CouponMetadata))
			{
				if(CouponMetadata.mapDelegates == null)
				{
					CouponMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CouponMetadata.meta == null)
				{
					CouponMetadata.meta = new CouponMetadata();
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
				meta.AddTypeMap("Code", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DescriptionForCustomer", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("IsCombinable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidFromDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidToDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("DiscountType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("MinOrderAmount", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("MaxUsesPerUser", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MaxUsesLifetime", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MaxDiscountAmountPerOrder", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("AppliesToProductIds", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AppliesToShippingRateTypes", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PercentOff", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AmountOff", new esTypeMap("money", "System.Decimal"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Coupon";
					meta.Destination = objectQualifier + "DNNspot_Store_Coupon";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_CouponInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_CouponUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_CouponDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_CouponLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_CouponLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Coupon";
					meta.Destination = "DNNspot_Store_Coupon";
									
					meta.spInsert = "proc_DNNspot_Store_CouponInsert";				
					meta.spUpdate = "proc_DNNspot_Store_CouponUpdate";		
					meta.spDelete = "proc_DNNspot_Store_CouponDelete";
					meta.spLoadAll = "proc_DNNspot_Store_CouponLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_CouponLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CouponMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
