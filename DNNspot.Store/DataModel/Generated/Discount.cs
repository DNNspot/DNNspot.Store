
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
	/// Encapsulates the 'DNNspot_Store_Discount' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Discount))]	
	[XmlType("Discount")]
	[Table(Name="Discount")]
	public partial class Discount : esDiscount
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Discount();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new Discount();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Discount();
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
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? DnnRoleId
		{
			get { return base.DnnRoleId;  }
			set { base.DnnRoleId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DiscountType
		{
			get { return base.DiscountType;  }
			set { base.DiscountType = value; }
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
		public override System.String AppliesToProductIds
		{
			get { return base.AppliesToProductIds;  }
			set { base.AppliesToProductIds = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String AppliesToCategoryIds
		{
			get { return base.AppliesToCategoryIds;  }
			set { base.AppliesToCategoryIds = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("DiscountCollection")]
	public partial class DiscountCollection : esDiscountCollection, IEnumerable<Discount>
	{
		public Discount FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Discount))]
		public class DiscountCollectionWCFPacket : esCollectionWCFPacket<DiscountCollection>
		{
			public static implicit operator DiscountCollection(DiscountCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator DiscountCollectionWCFPacket(DiscountCollection collection)
			{
				return new DiscountCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class DiscountQuery : esDiscountQuery
	{
		public DiscountQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "DiscountQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(DiscountQuery query)
		{
			return DiscountQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator DiscountQuery(string query)
		{
			return (DiscountQuery)DiscountQuery.SerializeHelper.FromXml(query, typeof(DiscountQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esDiscount : esEntity
	{
		public esDiscount()
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
			DiscountQuery query = new DiscountQuery();
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
		/// Maps to DNNspot_Store_Discount.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(DiscountMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(DiscountMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(DiscountMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(DiscountMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(DiscountMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.IsActive
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsActive
		{
			get
			{
				return base.GetSystemBoolean(DiscountMetadata.ColumnNames.IsActive);
			}
			
			set
			{
				if(base.SetSystemBoolean(DiscountMetadata.ColumnNames.IsActive, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.IsActive);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(DiscountMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(DiscountMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.DnnRoleId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? DnnRoleId
		{
			get
			{
				return base.GetSystemInt32(DiscountMetadata.ColumnNames.DnnRoleId);
			}
			
			set
			{
				if(base.SetSystemInt32(DiscountMetadata.ColumnNames.DnnRoleId, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.DnnRoleId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.DiscountType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DiscountType
		{
			get
			{
				return base.GetSystemString(DiscountMetadata.ColumnNames.DiscountType);
			}
			
			set
			{
				if(base.SetSystemString(DiscountMetadata.ColumnNames.DiscountType, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.DiscountType);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.PercentOff
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? PercentOff
		{
			get
			{
				return base.GetSystemDecimal(DiscountMetadata.ColumnNames.PercentOff);
			}
			
			set
			{
				if(base.SetSystemDecimal(DiscountMetadata.ColumnNames.PercentOff, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.PercentOff);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.AmountOff
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? AmountOff
		{
			get
			{
				return base.GetSystemDecimal(DiscountMetadata.ColumnNames.AmountOff);
			}
			
			set
			{
				if(base.SetSystemDecimal(DiscountMetadata.ColumnNames.AmountOff, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.AmountOff);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.IsCombinable
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsCombinable
		{
			get
			{
				return base.GetSystemBoolean(DiscountMetadata.ColumnNames.IsCombinable);
			}
			
			set
			{
				if(base.SetSystemBoolean(DiscountMetadata.ColumnNames.IsCombinable, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.IsCombinable);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.ValidFromDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ValidFromDate
		{
			get
			{
				return base.GetSystemDateTime(DiscountMetadata.ColumnNames.ValidFromDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(DiscountMetadata.ColumnNames.ValidFromDate, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.ValidFromDate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.ValidToDate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ValidToDate
		{
			get
			{
				return base.GetSystemDateTime(DiscountMetadata.ColumnNames.ValidToDate);
			}
			
			set
			{
				if(base.SetSystemDateTime(DiscountMetadata.ColumnNames.ValidToDate, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.ValidToDate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.AppliesToProductIds
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String AppliesToProductIds
		{
			get
			{
				return base.GetSystemString(DiscountMetadata.ColumnNames.AppliesToProductIds);
			}
			
			set
			{
				if(base.SetSystemString(DiscountMetadata.ColumnNames.AppliesToProductIds, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.AppliesToProductIds);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Discount.AppliesToCategoryIds
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String AppliesToCategoryIds
		{
			get
			{
				return base.GetSystemString(DiscountMetadata.ColumnNames.AppliesToCategoryIds);
			}
			
			set
			{
				if(base.SetSystemString(DiscountMetadata.ColumnNames.AppliesToCategoryIds, value))
				{
					OnPropertyChanged(DiscountMetadata.PropertyNames.AppliesToCategoryIds);
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
						case "Name": this.str().Name = (string)value; break;							
						case "DnnRoleId": this.str().DnnRoleId = (string)value; break;							
						case "DiscountType": this.str().DiscountType = (string)value; break;							
						case "PercentOff": this.str().PercentOff = (string)value; break;							
						case "AmountOff": this.str().AmountOff = (string)value; break;							
						case "IsCombinable": this.str().IsCombinable = (string)value; break;							
						case "ValidFromDate": this.str().ValidFromDate = (string)value; break;							
						case "ValidToDate": this.str().ValidToDate = (string)value; break;							
						case "AppliesToProductIds": this.str().AppliesToProductIds = (string)value; break;							
						case "AppliesToCategoryIds": this.str().AppliesToCategoryIds = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.Id);
							break;
						
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.StoreId);
							break;
						
						case "IsActive":
						
							if (value == null || value is System.Boolean)
								this.IsActive = (System.Boolean?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.IsActive);
							break;
						
						case "DnnRoleId":
						
							if (value == null || value is System.Int32)
								this.DnnRoleId = (System.Int32?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.DnnRoleId);
							break;
						
						case "PercentOff":
						
							if (value == null || value is System.Decimal)
								this.PercentOff = (System.Decimal?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.PercentOff);
							break;
						
						case "AmountOff":
						
							if (value == null || value is System.Decimal)
								this.AmountOff = (System.Decimal?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.AmountOff);
							break;
						
						case "IsCombinable":
						
							if (value == null || value is System.Boolean)
								this.IsCombinable = (System.Boolean?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.IsCombinable);
							break;
						
						case "ValidFromDate":
						
							if (value == null || value is System.DateTime)
								this.ValidFromDate = (System.DateTime?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.ValidFromDate);
							break;
						
						case "ValidToDate":
						
							if (value == null || value is System.DateTime)
								this.ValidToDate = (System.DateTime?)value;
								OnPropertyChanged(DiscountMetadata.PropertyNames.ValidToDate);
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
			public esStrings(esDiscount entity)
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
				
			public System.String DnnRoleId
			{
				get
				{
					System.Int32? data = entity.DnnRoleId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DnnRoleId = null;
					else entity.DnnRoleId = Convert.ToInt32(value);
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
				
			public System.String AppliesToCategoryIds
			{
				get
				{
					System.String data = entity.AppliesToCategoryIds;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.AppliesToCategoryIds = null;
					else entity.AppliesToCategoryIds = Convert.ToString(value);
				}
			}
			

			private esDiscount entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return DiscountMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public DiscountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DiscountQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(DiscountQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(DiscountQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private DiscountQuery query;		
	}



	[Serializable]
	abstract public partial class esDiscountCollection : esEntityCollection<Discount>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return DiscountMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "DiscountCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public DiscountQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new DiscountQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(DiscountQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new DiscountQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(DiscountQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((DiscountQuery)query);
		}

		#endregion
		
		private DiscountQuery query;
	}



	[Serializable]
	abstract public partial class esDiscountQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return DiscountMetadata.Meta();
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
				case "Name": return this.Name;
				case "DnnRoleId": return this.DnnRoleId;
				case "DiscountType": return this.DiscountType;
				case "PercentOff": return this.PercentOff;
				case "AmountOff": return this.AmountOff;
				case "IsCombinable": return this.IsCombinable;
				case "ValidFromDate": return this.ValidFromDate;
				case "ValidToDate": return this.ValidToDate;
				case "AppliesToProductIds": return this.AppliesToProductIds;
				case "AppliesToCategoryIds": return this.AppliesToCategoryIds;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem IsActive
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.IsActive, esSystemType.Boolean); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem DnnRoleId
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.DnnRoleId, esSystemType.Int32); }
		} 
		
		public esQueryItem DiscountType
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.DiscountType, esSystemType.String); }
		} 
		
		public esQueryItem PercentOff
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.PercentOff, esSystemType.Decimal); }
		} 
		
		public esQueryItem AmountOff
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.AmountOff, esSystemType.Decimal); }
		} 
		
		public esQueryItem IsCombinable
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.IsCombinable, esSystemType.Boolean); }
		} 
		
		public esQueryItem ValidFromDate
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.ValidFromDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem ValidToDate
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.ValidToDate, esSystemType.DateTime); }
		} 
		
		public esQueryItem AppliesToProductIds
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.AppliesToProductIds, esSystemType.String); }
		} 
		
		public esQueryItem AppliesToCategoryIds
		{
			get { return new esQueryItem(this, DiscountMetadata.ColumnNames.AppliesToCategoryIds, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class Discount : esDiscount
	{

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Discount_DNNspot_Store_Store
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
	public partial class DiscountMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected DiscountMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(DiscountMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DiscountMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.StoreId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DiscountMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.IsActive, 2, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DiscountMetadata.PropertyNames.IsActive;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.Name, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = DiscountMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.DnnRoleId, 4, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = DiscountMetadata.PropertyNames.DnnRoleId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.DiscountType, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = DiscountMetadata.PropertyNames.DiscountType;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.PercentOff, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DiscountMetadata.PropertyNames.PercentOff;
			c.NumericPrecision = 10;
			c.NumericScale = 2;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.AmountOff, 7, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = DiscountMetadata.PropertyNames.AmountOff;
			c.NumericPrecision = 19;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.IsCombinable, 8, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = DiscountMetadata.PropertyNames.IsCombinable;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.ValidFromDate, 9, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DiscountMetadata.PropertyNames.ValidFromDate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.ValidToDate, 10, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = DiscountMetadata.PropertyNames.ValidToDate;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.AppliesToProductIds, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = DiscountMetadata.PropertyNames.AppliesToProductIds;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(DiscountMetadata.ColumnNames.AppliesToCategoryIds, 12, typeof(System.String), esSystemType.String);
			c.PropertyName = DiscountMetadata.PropertyNames.AppliesToCategoryIds;
			c.CharacterMaxLength = 2147483647;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public DiscountMetadata Meta()
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
			 public const string Name = "Name";
			 public const string DnnRoleId = "DnnRoleId";
			 public const string DiscountType = "DiscountType";
			 public const string PercentOff = "PercentOff";
			 public const string AmountOff = "AmountOff";
			 public const string IsCombinable = "IsCombinable";
			 public const string ValidFromDate = "ValidFromDate";
			 public const string ValidToDate = "ValidToDate";
			 public const string AppliesToProductIds = "AppliesToProductIds";
			 public const string AppliesToCategoryIds = "AppliesToCategoryIds";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string StoreId = "StoreId";
			 public const string IsActive = "IsActive";
			 public const string Name = "Name";
			 public const string DnnRoleId = "DnnRoleId";
			 public const string DiscountType = "DiscountType";
			 public const string PercentOff = "PercentOff";
			 public const string AmountOff = "AmountOff";
			 public const string IsCombinable = "IsCombinable";
			 public const string ValidFromDate = "ValidFromDate";
			 public const string ValidToDate = "ValidToDate";
			 public const string AppliesToProductIds = "AppliesToProductIds";
			 public const string AppliesToCategoryIds = "AppliesToCategoryIds";
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
			lock (typeof(DiscountMetadata))
			{
				if(DiscountMetadata.mapDelegates == null)
				{
					DiscountMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (DiscountMetadata.meta == null)
				{
					DiscountMetadata.meta = new DiscountMetadata();
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
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DnnRoleId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("DiscountType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("PercentOff", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("AmountOff", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("IsCombinable", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("ValidFromDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ValidToDate", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("AppliesToProductIds", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("AppliesToCategoryIds", new esTypeMap("varchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Discount";
					meta.Destination = objectQualifier + "DNNspot_Store_Discount";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_DiscountInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_DiscountUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_DiscountDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_DiscountLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_DiscountLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Discount";
					meta.Destination = "DNNspot_Store_Discount";
									
					meta.spInsert = "proc_DNNspot_Store_DiscountInsert";				
					meta.spUpdate = "proc_DNNspot_Store_DiscountUpdate";		
					meta.spDelete = "proc_DNNspot_Store_DiscountDelete";
					meta.spLoadAll = "proc_DNNspot_Store_DiscountLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_DiscountLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private DiscountMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
