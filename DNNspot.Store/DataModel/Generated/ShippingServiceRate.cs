
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
	/// Encapsulates the 'DNNspot_Store_ShippingServiceRate' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ShippingServiceRate))]	
	[XmlType("ShippingServiceRate")]
	[Table(Name="ShippingServiceRate")]
	public partial class ShippingServiceRate : esShippingServiceRate
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ShippingServiceRate();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ShippingServiceRate();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ShippingServiceRate();
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
		public override System.Int32? RateTypeId
		{
			get { return base.RateTypeId;  }
			set { base.RateTypeId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String CountryCode
		{
			get { return base.CountryCode;  }
			set { base.CountryCode = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Region
		{
			get { return base.Region;  }
			set { base.Region = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? WeightMin
		{
			get { return base.WeightMin;  }
			set { base.WeightMin = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Decimal? WeightMax
		{
			get { return base.WeightMax;  }
			set { base.WeightMax = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Decimal? Cost
		{
			get { return base.Cost;  }
			set { base.Cost = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ShippingServiceRateCollection")]
	public partial class ShippingServiceRateCollection : esShippingServiceRateCollection, IEnumerable<ShippingServiceRate>
	{
		public ShippingServiceRate FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ShippingServiceRate))]
		public class ShippingServiceRateCollectionWCFPacket : esCollectionWCFPacket<ShippingServiceRateCollection>
		{
			public static implicit operator ShippingServiceRateCollection(ShippingServiceRateCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ShippingServiceRateCollectionWCFPacket(ShippingServiceRateCollection collection)
			{
				return new ShippingServiceRateCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ShippingServiceRateQuery : esShippingServiceRateQuery
	{
		public ShippingServiceRateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ShippingServiceRateQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ShippingServiceRateQuery query)
		{
			return ShippingServiceRateQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ShippingServiceRateQuery(string query)
		{
			return (ShippingServiceRateQuery)ShippingServiceRateQuery.SerializeHelper.FromXml(query, typeof(ShippingServiceRateQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esShippingServiceRate : esEntity
	{
		public esShippingServiceRate()
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
			ShippingServiceRateQuery query = new ShippingServiceRateQuery();
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
		/// Maps to DNNspot_Store_ShippingServiceRate.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ShippingServiceRateMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingServiceRateMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRate.RateTypeId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? RateTypeId
		{
			get
			{
				return base.GetSystemInt32(ShippingServiceRateMetadata.ColumnNames.RateTypeId);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingServiceRateMetadata.ColumnNames.RateTypeId, value))
				{
					this._UpToShippingServiceRateTypeByRateTypeId = null;
					this.OnPropertyChanged("UpToShippingServiceRateTypeByRateTypeId");
					OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.RateTypeId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRate.CountryCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CountryCode
		{
			get
			{
				return base.GetSystemString(ShippingServiceRateMetadata.ColumnNames.CountryCode);
			}
			
			set
			{
				if(base.SetSystemString(ShippingServiceRateMetadata.ColumnNames.CountryCode, value))
				{
					OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.CountryCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRate.Region
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Region
		{
			get
			{
				return base.GetSystemString(ShippingServiceRateMetadata.ColumnNames.Region);
			}
			
			set
			{
				if(base.SetSystemString(ShippingServiceRateMetadata.ColumnNames.Region, value))
				{
					OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.Region);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRate.WeightMin
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? WeightMin
		{
			get
			{
				return base.GetSystemDecimal(ShippingServiceRateMetadata.ColumnNames.WeightMin);
			}
			
			set
			{
				if(base.SetSystemDecimal(ShippingServiceRateMetadata.ColumnNames.WeightMin, value))
				{
					OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.WeightMin);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRate.WeightMax
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? WeightMax
		{
			get
			{
				return base.GetSystemDecimal(ShippingServiceRateMetadata.ColumnNames.WeightMax);
			}
			
			set
			{
				if(base.SetSystemDecimal(ShippingServiceRateMetadata.ColumnNames.WeightMax, value))
				{
					OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.WeightMax);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRate.Cost
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Cost
		{
			get
			{
				return base.GetSystemDecimal(ShippingServiceRateMetadata.ColumnNames.Cost);
			}
			
			set
			{
				if(base.SetSystemDecimal(ShippingServiceRateMetadata.ColumnNames.Cost, value))
				{
					OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.Cost);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected ShippingServiceRateType _UpToShippingServiceRateTypeByRateTypeId;
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
						case "RateTypeId": this.str().RateTypeId = (string)value; break;							
						case "CountryCode": this.str().CountryCode = (string)value; break;							
						case "Region": this.str().Region = (string)value; break;							
						case "WeightMin": this.str().WeightMin = (string)value; break;							
						case "WeightMax": this.str().WeightMax = (string)value; break;							
						case "Cost": this.str().Cost = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.Id);
							break;
						
						case "RateTypeId":
						
							if (value == null || value is System.Int32)
								this.RateTypeId = (System.Int32?)value;
								OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.RateTypeId);
							break;
						
						case "WeightMin":
						
							if (value == null || value is System.Decimal)
								this.WeightMin = (System.Decimal?)value;
								OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.WeightMin);
							break;
						
						case "WeightMax":
						
							if (value == null || value is System.Decimal)
								this.WeightMax = (System.Decimal?)value;
								OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.WeightMax);
							break;
						
						case "Cost":
						
							if (value == null || value is System.Decimal)
								this.Cost = (System.Decimal?)value;
								OnPropertyChanged(ShippingServiceRateMetadata.PropertyNames.Cost);
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
			public esStrings(esShippingServiceRate entity)
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
				
			public System.String RateTypeId
			{
				get
				{
					System.Int32? data = entity.RateTypeId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RateTypeId = null;
					else entity.RateTypeId = Convert.ToInt32(value);
				}
			}
				
			public System.String CountryCode
			{
				get
				{
					System.String data = entity.CountryCode;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CountryCode = null;
					else entity.CountryCode = Convert.ToString(value);
				}
			}
				
			public System.String Region
			{
				get
				{
					System.String data = entity.Region;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Region = null;
					else entity.Region = Convert.ToString(value);
				}
			}
				
			public System.String WeightMin
			{
				get
				{
					System.Decimal? data = entity.WeightMin;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeightMin = null;
					else entity.WeightMin = Convert.ToDecimal(value);
				}
			}
				
			public System.String WeightMax
			{
				get
				{
					System.Decimal? data = entity.WeightMax;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.WeightMax = null;
					else entity.WeightMax = Convert.ToDecimal(value);
				}
			}
				
			public System.String Cost
			{
				get
				{
					System.Decimal? data = entity.Cost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Cost = null;
					else entity.Cost = Convert.ToDecimal(value);
				}
			}
			

			private esShippingServiceRate entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceRateMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ShippingServiceRateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceRateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceRateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ShippingServiceRateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ShippingServiceRateQuery query;		
	}



	[Serializable]
	abstract public partial class esShippingServiceRateCollection : esEntityCollection<ShippingServiceRate>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceRateMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ShippingServiceRateCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ShippingServiceRateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceRateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceRateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ShippingServiceRateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ShippingServiceRateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ShippingServiceRateQuery)query);
		}

		#endregion
		
		private ShippingServiceRateQuery query;
	}



	[Serializable]
	abstract public partial class esShippingServiceRateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceRateMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "RateTypeId": return this.RateTypeId;
				case "CountryCode": return this.CountryCode;
				case "Region": return this.Region;
				case "WeightMin": return this.WeightMin;
				case "WeightMax": return this.WeightMax;
				case "Cost": return this.Cost;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ShippingServiceRateMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem RateTypeId
		{
			get { return new esQueryItem(this, ShippingServiceRateMetadata.ColumnNames.RateTypeId, esSystemType.Int32); }
		} 
		
		public esQueryItem CountryCode
		{
			get { return new esQueryItem(this, ShippingServiceRateMetadata.ColumnNames.CountryCode, esSystemType.String); }
		} 
		
		public esQueryItem Region
		{
			get { return new esQueryItem(this, ShippingServiceRateMetadata.ColumnNames.Region, esSystemType.String); }
		} 
		
		public esQueryItem WeightMin
		{
			get { return new esQueryItem(this, ShippingServiceRateMetadata.ColumnNames.WeightMin, esSystemType.Decimal); }
		} 
		
		public esQueryItem WeightMax
		{
			get { return new esQueryItem(this, ShippingServiceRateMetadata.ColumnNames.WeightMax, esSystemType.Decimal); }
		} 
		
		public esQueryItem Cost
		{
			get { return new esQueryItem(this, ShippingServiceRateMetadata.ColumnNames.Cost, esSystemType.Decimal); }
		} 
		
		#endregion
		
	}


	
	public partial class ShippingServiceRate : esShippingServiceRate
	{

				
		#region UpToShippingServiceRateTypeByRateTypeId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ShippingServiceRate_DNNspot_Store_ShippingServiceRateType
		/// </summary>

		[XmlIgnore]
					
		public ShippingServiceRateType UpToShippingServiceRateTypeByRateTypeId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToShippingServiceRateTypeByRateTypeId == null && RateTypeId != null)
				{
					this._UpToShippingServiceRateTypeByRateTypeId = new ShippingServiceRateType();
					this._UpToShippingServiceRateTypeByRateTypeId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToShippingServiceRateTypeByRateTypeId", this._UpToShippingServiceRateTypeByRateTypeId);
					this._UpToShippingServiceRateTypeByRateTypeId.Query.Where(this._UpToShippingServiceRateTypeByRateTypeId.Query.Id == this.RateTypeId);
					this._UpToShippingServiceRateTypeByRateTypeId.Query.Load();
				}	
				return this._UpToShippingServiceRateTypeByRateTypeId;
			}
			
			set
			{
				this.RemovePreSave("UpToShippingServiceRateTypeByRateTypeId");
				

				if(value == null)
				{
					this.RateTypeId = null;
					this._UpToShippingServiceRateTypeByRateTypeId = null;
				}
				else
				{
					this.RateTypeId = value.Id;
					this._UpToShippingServiceRateTypeByRateTypeId = value;
					this.SetPreSave("UpToShippingServiceRateTypeByRateTypeId", this._UpToShippingServiceRateTypeByRateTypeId);
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
			if(!this.es.IsDeleted && this._UpToShippingServiceRateTypeByRateTypeId != null)
			{
				this.RateTypeId = this._UpToShippingServiceRateTypeByRateTypeId.Id;
			}
		}
		
	}
	



	[Serializable]
	public partial class ShippingServiceRateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ShippingServiceRateMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ShippingServiceRateMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingServiceRateMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateMetadata.ColumnNames.RateTypeId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingServiceRateMetadata.PropertyNames.RateTypeId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateMetadata.ColumnNames.CountryCode, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingServiceRateMetadata.PropertyNames.CountryCode;
			c.CharacterMaxLength = 2;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateMetadata.ColumnNames.Region, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingServiceRateMetadata.PropertyNames.Region;
			c.CharacterMaxLength = 150;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateMetadata.ColumnNames.WeightMin, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ShippingServiceRateMetadata.PropertyNames.WeightMin;
			c.NumericPrecision = 14;
			c.NumericScale = 4;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateMetadata.ColumnNames.WeightMax, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ShippingServiceRateMetadata.PropertyNames.WeightMax;
			c.NumericPrecision = 14;
			c.NumericScale = 4;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateMetadata.ColumnNames.Cost, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = ShippingServiceRateMetadata.PropertyNames.Cost;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ShippingServiceRateMetadata Meta()
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
			 public const string RateTypeId = "RateTypeId";
			 public const string CountryCode = "CountryCode";
			 public const string Region = "Region";
			 public const string WeightMin = "WeightMin";
			 public const string WeightMax = "WeightMax";
			 public const string Cost = "Cost";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string RateTypeId = "RateTypeId";
			 public const string CountryCode = "CountryCode";
			 public const string Region = "Region";
			 public const string WeightMin = "WeightMin";
			 public const string WeightMax = "WeightMax";
			 public const string Cost = "Cost";
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
			lock (typeof(ShippingServiceRateMetadata))
			{
				if(ShippingServiceRateMetadata.mapDelegates == null)
				{
					ShippingServiceRateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ShippingServiceRateMetadata.meta == null)
				{
					ShippingServiceRateMetadata.meta = new ShippingServiceRateMetadata();
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
				meta.AddTypeMap("RateTypeId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CountryCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Region", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("WeightMin", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("WeightMax", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Cost", new esTypeMap("money", "System.Decimal"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_ShippingServiceRate";
					meta.Destination = objectQualifier + "DNNspot_Store_ShippingServiceRate";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ShippingServiceRate";
					meta.Destination = "DNNspot_Store_ShippingServiceRate";
									
					meta.spInsert = "proc_DNNspot_Store_ShippingServiceRateInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ShippingServiceRateUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ShippingServiceRateDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ShippingServiceRateLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ShippingServiceRateLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ShippingServiceRateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
