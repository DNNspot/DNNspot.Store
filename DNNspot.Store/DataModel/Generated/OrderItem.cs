
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
	/// Encapsulates the 'DNNspot_Store_OrderItem' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(OrderItem))]	
	[XmlType("OrderItem")]
	[Table(Name="OrderItem")]
	public partial class OrderItem : esOrderItem
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new OrderItem();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new OrderItem();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new OrderItem();
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
		public override System.Int32? OrderId
		{
			get { return base.OrderId;  }
			set { base.OrderId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? ProductId
		{
			get { return base.ProductId;  }
			set { base.ProductId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
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
		public override System.Decimal? WeightTotal
		{
			get { return base.WeightTotal;  }
			set { base.WeightTotal = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? PriceTotal
		{
			get { return base.PriceTotal;  }
			set { base.PriceTotal = value; }
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


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("OrderItemCollection")]
	public partial class OrderItemCollection : esOrderItemCollection, IEnumerable<OrderItem>
	{
		public OrderItem FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(OrderItem))]
		public class OrderItemCollectionWCFPacket : esCollectionWCFPacket<OrderItemCollection>
		{
			public static implicit operator OrderItemCollection(OrderItemCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator OrderItemCollectionWCFPacket(OrderItemCollection collection)
			{
				return new OrderItemCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class OrderItemQuery : esOrderItemQuery
	{
		public OrderItemQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "OrderItemQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(OrderItemQuery query)
		{
			return OrderItemQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator OrderItemQuery(string query)
		{
			return (OrderItemQuery)OrderItemQuery.SerializeHelper.FromXml(query, typeof(OrderItemQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esOrderItem : esEntity
	{
		public esOrderItem()
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
			OrderItemQuery query = new OrderItemQuery();
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
		/// Maps to DNNspot_Store_OrderItem.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(OrderItemMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderItemMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.OrderId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? OrderId
		{
			get
			{
				return base.GetSystemInt32(OrderItemMetadata.ColumnNames.OrderId);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderItemMetadata.ColumnNames.OrderId, value))
				{
					this._UpToOrderByOrderId = null;
					this.OnPropertyChanged("UpToOrderByOrderId");
					OnPropertyChanged(OrderItemMetadata.PropertyNames.OrderId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(OrderItemMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderItemMetadata.ColumnNames.ProductId, value))
				{
					this._UpToProductByProductId = null;
					this.OnPropertyChanged("UpToProductByProductId");
					OnPropertyChanged(OrderItemMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(OrderItemMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(OrderItemMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.Sku
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Sku
		{
			get
			{
				return base.GetSystemString(OrderItemMetadata.ColumnNames.Sku);
			}
			
			set
			{
				if(base.SetSystemString(OrderItemMetadata.ColumnNames.Sku, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.Sku);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.Quantity
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Quantity
		{
			get
			{
				return base.GetSystemInt32(OrderItemMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				if(base.SetSystemInt32(OrderItemMetadata.ColumnNames.Quantity, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.Quantity);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.ProductFieldData
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProductFieldData
		{
			get
			{
				return base.GetSystemString(OrderItemMetadata.ColumnNames.ProductFieldData);
			}
			
			set
			{
				if(base.SetSystemString(OrderItemMetadata.ColumnNames.ProductFieldData, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.ProductFieldData);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.DigitalFileDisplayName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DigitalFileDisplayName
		{
			get
			{
				return base.GetSystemString(OrderItemMetadata.ColumnNames.DigitalFileDisplayName);
			}
			
			set
			{
				if(base.SetSystemString(OrderItemMetadata.ColumnNames.DigitalFileDisplayName, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.DigitalFileDisplayName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.DigitalFilename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DigitalFilename
		{
			get
			{
				return base.GetSystemString(OrderItemMetadata.ColumnNames.DigitalFilename);
			}
			
			set
			{
				if(base.SetSystemString(OrderItemMetadata.ColumnNames.DigitalFilename, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.DigitalFilename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.WeightTotal
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? WeightTotal
		{
			get
			{
				return base.GetSystemDecimal(OrderItemMetadata.ColumnNames.WeightTotal);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderItemMetadata.ColumnNames.WeightTotal, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.WeightTotal);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.PriceTotal
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? PriceTotal
		{
			get
			{
				return base.GetSystemDecimal(OrderItemMetadata.ColumnNames.PriceTotal);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderItemMetadata.ColumnNames.PriceTotal, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.PriceTotal);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.Length
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Length
		{
			get
			{
				return base.GetSystemDecimal(OrderItemMetadata.ColumnNames.Length);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderItemMetadata.ColumnNames.Length, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.Length);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.Width
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Width
		{
			get
			{
				return base.GetSystemDecimal(OrderItemMetadata.ColumnNames.Width);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderItemMetadata.ColumnNames.Width, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.Width);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_OrderItem.Height
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Height
		{
			get
			{
				return base.GetSystemDecimal(OrderItemMetadata.ColumnNames.Height);
			}
			
			set
			{
				if(base.SetSystemDecimal(OrderItemMetadata.ColumnNames.Height, value))
				{
					OnPropertyChanged(OrderItemMetadata.PropertyNames.Height);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Order _UpToOrderByOrderId;
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
						case "OrderId": this.str().OrderId = (string)value; break;							
						case "ProductId": this.str().ProductId = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "Sku": this.str().Sku = (string)value; break;							
						case "Quantity": this.str().Quantity = (string)value; break;							
						case "ProductFieldData": this.str().ProductFieldData = (string)value; break;							
						case "DigitalFileDisplayName": this.str().DigitalFileDisplayName = (string)value; break;							
						case "DigitalFilename": this.str().DigitalFilename = (string)value; break;							
						case "WeightTotal": this.str().WeightTotal = (string)value; break;							
						case "PriceTotal": this.str().PriceTotal = (string)value; break;							
						case "Length": this.str().Length = (string)value; break;							
						case "Width": this.str().Width = (string)value; break;							
						case "Height": this.str().Height = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.Id);
							break;
						
						case "OrderId":
						
							if (value == null || value is System.Int32)
								this.OrderId = (System.Int32?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.OrderId);
							break;
						
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.ProductId);
							break;
						
						case "Quantity":
						
							if (value == null || value is System.Int32)
								this.Quantity = (System.Int32?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.Quantity);
							break;
						
						case "WeightTotal":
						
							if (value == null || value is System.Decimal)
								this.WeightTotal = (System.Decimal?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.WeightTotal);
							break;
						
						case "PriceTotal":
						
							if (value == null || value is System.Decimal)
								this.PriceTotal = (System.Decimal?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.PriceTotal);
							break;
						
						case "Length":
						
							if (value == null || value is System.Decimal)
								this.Length = (System.Decimal?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.Length);
							break;
						
						case "Width":
						
							if (value == null || value is System.Decimal)
								this.Width = (System.Decimal?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.Width);
							break;
						
						case "Height":
						
							if (value == null || value is System.Decimal)
								this.Height = (System.Decimal?)value;
								OnPropertyChanged(OrderItemMetadata.PropertyNames.Height);
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
			public esStrings(esOrderItem entity)
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
				
			public System.String WeightTotal
			{
				get
				{
					System.Decimal? data = entity.WeightTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeightTotal = null;
					else entity.WeightTotal = Convert.ToDecimal(value);
				}
			}
				
			public System.String PriceTotal
			{
				get
				{
					System.Decimal? data = entity.PriceTotal;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.PriceTotal = null;
					else entity.PriceTotal = Convert.ToDecimal(value);
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
			

			private esOrderItem entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return OrderItemMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public OrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderItemQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(OrderItemQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(OrderItemQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private OrderItemQuery query;		
	}



	[Serializable]
	abstract public partial class esOrderItemCollection : esEntityCollection<OrderItem>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return OrderItemMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "OrderItemCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public OrderItemQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new OrderItemQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(OrderItemQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new OrderItemQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(OrderItemQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((OrderItemQuery)query);
		}

		#endregion
		
		private OrderItemQuery query;
	}



	[Serializable]
	abstract public partial class esOrderItemQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return OrderItemMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "OrderId": return this.OrderId;
				case "ProductId": return this.ProductId;
				case "Name": return this.Name;
				case "Sku": return this.Sku;
				case "Quantity": return this.Quantity;
				case "ProductFieldData": return this.ProductFieldData;
				case "DigitalFileDisplayName": return this.DigitalFileDisplayName;
				case "DigitalFilename": return this.DigitalFilename;
				case "WeightTotal": return this.WeightTotal;
				case "PriceTotal": return this.PriceTotal;
				case "Length": return this.Length;
				case "Width": return this.Width;
				case "Height": return this.Height;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem OrderId
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.OrderId, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Sku
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.Sku, esSystemType.String); }
		} 
		
		public esQueryItem Quantity
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.Quantity, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductFieldData
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.ProductFieldData, esSystemType.String); }
		} 
		
		public esQueryItem DigitalFileDisplayName
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.DigitalFileDisplayName, esSystemType.String); }
		} 
		
		public esQueryItem DigitalFilename
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.DigitalFilename, esSystemType.String); }
		} 
		
		public esQueryItem WeightTotal
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.WeightTotal, esSystemType.Decimal); }
		} 
		
		public esQueryItem PriceTotal
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.PriceTotal, esSystemType.Decimal); }
		} 
		
		public esQueryItem Length
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.Length, esSystemType.Decimal); }
		} 
		
		public esQueryItem Width
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.Width, esSystemType.Decimal); }
		} 
		
		public esQueryItem Height
		{
			get { return new esQueryItem(this, OrderItemMetadata.ColumnNames.Height, esSystemType.Decimal); }
		} 
		
		#endregion
		
	}


	
	public partial class OrderItem : esOrderItem
	{

				
		#region UpToOrderByOrderId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_OrderItem_DNNspot_Store_Order
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
		

				
		#region UpToProductByProductId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_OrderItem_DNNspot_Store_Product
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
			if(!this.es.IsDeleted && this._UpToOrderByOrderId != null)
			{
				this.OrderId = this._UpToOrderByOrderId.Id;
			}
			if(!this.es.IsDeleted && this._UpToProductByProductId != null)
			{
				this.ProductId = this._UpToProductByProductId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class OrderItemMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected OrderItemMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderItemMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.OrderId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderItemMetadata.PropertyNames.OrderId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.ProductId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderItemMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.Name, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderItemMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 250;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.Sku, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderItemMetadata.PropertyNames.Sku;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.Quantity, 5, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = OrderItemMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.ProductFieldData, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderItemMetadata.PropertyNames.ProductFieldData;
			c.CharacterMaxLength = 1073741823;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.DigitalFileDisplayName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderItemMetadata.PropertyNames.DigitalFileDisplayName;
			c.CharacterMaxLength = 250;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.DigitalFilename, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = OrderItemMetadata.PropertyNames.DigitalFilename;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.WeightTotal, 9, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderItemMetadata.PropertyNames.WeightTotal;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.PriceTotal, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderItemMetadata.PropertyNames.PriceTotal;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.Length, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderItemMetadata.PropertyNames.Length;
			c.NumericPrecision = 10;
			c.NumericScale = 8;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.Width, 12, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderItemMetadata.PropertyNames.Width;
			c.NumericPrecision = 10;
			c.NumericScale = 8;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(OrderItemMetadata.ColumnNames.Height, 13, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = OrderItemMetadata.PropertyNames.Height;
			c.NumericPrecision = 10;
			c.NumericScale = 8;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public OrderItemMetadata Meta()
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
			 public const string ProductId = "ProductId";
			 public const string Name = "Name";
			 public const string Sku = "Sku";
			 public const string Quantity = "Quantity";
			 public const string ProductFieldData = "ProductFieldData";
			 public const string DigitalFileDisplayName = "DigitalFileDisplayName";
			 public const string DigitalFilename = "DigitalFilename";
			 public const string WeightTotal = "WeightTotal";
			 public const string PriceTotal = "PriceTotal";
			 public const string Length = "Length";
			 public const string Width = "Width";
			 public const string Height = "Height";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string OrderId = "OrderId";
			 public const string ProductId = "ProductId";
			 public const string Name = "Name";
			 public const string Sku = "Sku";
			 public const string Quantity = "Quantity";
			 public const string ProductFieldData = "ProductFieldData";
			 public const string DigitalFileDisplayName = "DigitalFileDisplayName";
			 public const string DigitalFilename = "DigitalFilename";
			 public const string WeightTotal = "WeightTotal";
			 public const string PriceTotal = "PriceTotal";
			 public const string Length = "Length";
			 public const string Width = "Width";
			 public const string Height = "Height";
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
			lock (typeof(OrderItemMetadata))
			{
				if(OrderItemMetadata.mapDelegates == null)
				{
					OrderItemMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (OrderItemMetadata.meta == null)
				{
					OrderItemMetadata.meta = new OrderItemMetadata();
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
				meta.AddTypeMap("OrderId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ProductId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Sku", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Quantity", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ProductFieldData", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DigitalFileDisplayName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DigitalFilename", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("WeightTotal", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("PriceTotal", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("Length", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Width", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Height", new esTypeMap("decimal", "System.Decimal"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_OrderItem";
					meta.Destination = objectQualifier + "DNNspot_Store_OrderItem";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_OrderItemInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_OrderItemUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_OrderItemDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_OrderItemLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_OrderItemLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_OrderItem";
					meta.Destination = "DNNspot_Store_OrderItem";
									
					meta.spInsert = "proc_DNNspot_Store_OrderItemInsert";				
					meta.spUpdate = "proc_DNNspot_Store_OrderItemUpdate";		
					meta.spDelete = "proc_DNNspot_Store_OrderItemDelete";
					meta.spLoadAll = "proc_DNNspot_Store_OrderItemLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_OrderItemLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private OrderItemMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
