
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/25/2013 4:44:22 PM
===============================================================================
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Data;
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
	/// Encapsulates the 'vDNNspot_Store_ShippingServiceRates' view
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(vShippingServiceRates))]	
	[XmlType("vShippingServiceRates")]
	public partial class vShippingServiceRates : esvShippingServiceRates
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new vShippingServiceRates();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("vShippingServiceRatesCollection")]
	public partial class vShippingServiceRatesCollection : esvShippingServiceRatesCollection, IEnumerable<vShippingServiceRates>
	{

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(vShippingServiceRates))]
		public class vShippingServiceRatesCollectionWCFPacket : esCollectionWCFPacket<vShippingServiceRatesCollection>
		{
			public static implicit operator vShippingServiceRatesCollection(vShippingServiceRatesCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator vShippingServiceRatesCollectionWCFPacket(vShippingServiceRatesCollection collection)
			{
				return new vShippingServiceRatesCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class vShippingServiceRatesQuery : esvShippingServiceRatesQuery
	{
		public vShippingServiceRatesQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "vShippingServiceRatesQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(vShippingServiceRatesQuery query)
		{
			return vShippingServiceRatesQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator vShippingServiceRatesQuery(string query)
		{
			return (vShippingServiceRatesQuery)vShippingServiceRatesQuery.SerializeHelper.FromXml(query, typeof(vShippingServiceRatesQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esvShippingServiceRates : esEntity
	{
		public esvShippingServiceRates()
		{

		}
		
		#region LoadByPrimaryKey
		
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.RateId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? RateId
		{
			get
			{
				return base.GetSystemInt32(vShippingServiceRatesMetadata.ColumnNames.RateId);
			}
			
			set
			{
				if(base.SetSystemInt32(vShippingServiceRatesMetadata.ColumnNames.RateId, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.CountryCode
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CountryCode
		{
			get
			{
				return base.GetSystemString(vShippingServiceRatesMetadata.ColumnNames.CountryCode);
			}
			
			set
			{
				if(base.SetSystemString(vShippingServiceRatesMetadata.ColumnNames.CountryCode, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.CountryCode);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.Region
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Region
		{
			get
			{
				return base.GetSystemString(vShippingServiceRatesMetadata.ColumnNames.Region);
			}
			
			set
			{
				if(base.SetSystemString(vShippingServiceRatesMetadata.ColumnNames.Region, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.Region);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.WeightMin
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? WeightMin
		{
			get
			{
				return base.GetSystemDecimal(vShippingServiceRatesMetadata.ColumnNames.WeightMin);
			}
			
			set
			{
				if(base.SetSystemDecimal(vShippingServiceRatesMetadata.ColumnNames.WeightMin, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.WeightMin);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.WeightMax
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? WeightMax
		{
			get
			{
				return base.GetSystemDecimal(vShippingServiceRatesMetadata.ColumnNames.WeightMax);
			}
			
			set
			{
				if(base.SetSystemDecimal(vShippingServiceRatesMetadata.ColumnNames.WeightMax, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.WeightMax);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.Cost
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? Cost
		{
			get
			{
				return base.GetSystemDecimal(vShippingServiceRatesMetadata.ColumnNames.Cost);
			}
			
			set
			{
				if(base.SetSystemDecimal(vShippingServiceRatesMetadata.ColumnNames.Cost, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.Cost);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.RateTypeId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? RateTypeId
		{
			get
			{
				return base.GetSystemInt32(vShippingServiceRatesMetadata.ColumnNames.RateTypeId);
			}
			
			set
			{
				if(base.SetSystemInt32(vShippingServiceRatesMetadata.ColumnNames.RateTypeId, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.RateTypeName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String RateTypeName
		{
			get
			{
				return base.GetSystemString(vShippingServiceRatesMetadata.ColumnNames.RateTypeName);
			}
			
			set
			{
				if(base.SetSystemString(vShippingServiceRatesMetadata.ColumnNames.RateTypeName, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.RateTypeDisplayName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String RateTypeDisplayName
		{
			get
			{
				return base.GetSystemString(vShippingServiceRatesMetadata.ColumnNames.RateTypeDisplayName);
			}
			
			set
			{
				if(base.SetSystemString(vShippingServiceRatesMetadata.ColumnNames.RateTypeDisplayName, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeDisplayName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.RateTypeIsEnabled
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? RateTypeIsEnabled
		{
			get
			{
				return base.GetSystemBoolean(vShippingServiceRatesMetadata.ColumnNames.RateTypeIsEnabled);
			}
			
			set
			{
				if(base.SetSystemBoolean(vShippingServiceRatesMetadata.ColumnNames.RateTypeIsEnabled, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeIsEnabled);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingServiceRates.RateTypeSortOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? RateTypeSortOrder
		{
			get
			{
				return base.GetSystemInt16(vShippingServiceRatesMetadata.ColumnNames.RateTypeSortOrder);
			}
			
			set
			{
				if(base.SetSystemInt16(vShippingServiceRatesMetadata.ColumnNames.RateTypeSortOrder, value))
				{
					OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeSortOrder);
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
						case "RateId": this.str().RateId = (string)value; break;							
						case "CountryCode": this.str().CountryCode = (string)value; break;							
						case "Region": this.str().Region = (string)value; break;							
						case "WeightMin": this.str().WeightMin = (string)value; break;							
						case "WeightMax": this.str().WeightMax = (string)value; break;							
						case "Cost": this.str().Cost = (string)value; break;							
						case "RateTypeId": this.str().RateTypeId = (string)value; break;							
						case "RateTypeName": this.str().RateTypeName = (string)value; break;							
						case "RateTypeDisplayName": this.str().RateTypeDisplayName = (string)value; break;							
						case "RateTypeIsEnabled": this.str().RateTypeIsEnabled = (string)value; break;							
						case "RateTypeSortOrder": this.str().RateTypeSortOrder = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "RateId":
						
							if (value == null || value is System.Int32)
								this.RateId = (System.Int32?)value;
								OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateId);
							break;
						
						case "WeightMin":
						
							if (value == null || value is System.Decimal)
								this.WeightMin = (System.Decimal?)value;
								OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.WeightMin);
							break;
						
						case "WeightMax":
						
							if (value == null || value is System.Decimal)
								this.WeightMax = (System.Decimal?)value;
								OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.WeightMax);
							break;
						
						case "Cost":
						
							if (value == null || value is System.Decimal)
								this.Cost = (System.Decimal?)value;
								OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.Cost);
							break;
						
						case "RateTypeId":
						
							if (value == null || value is System.Int32)
								this.RateTypeId = (System.Int32?)value;
								OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeId);
							break;
						
						case "RateTypeIsEnabled":
						
							if (value == null || value is System.Boolean)
								this.RateTypeIsEnabled = (System.Boolean?)value;
								OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeIsEnabled);
							break;
						
						case "RateTypeSortOrder":
						
							if (value == null || value is System.Int16)
								this.RateTypeSortOrder = (System.Int16?)value;
								OnPropertyChanged(vShippingServiceRatesMetadata.PropertyNames.RateTypeSortOrder);
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
			public esStrings(esvShippingServiceRates entity)
			{
				this.entity = entity;
			}
			
	
			public System.String RateId
			{
				get
				{
					System.Int32? data = entity.RateId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RateId = null;
					else entity.RateId = Convert.ToInt32(value);
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
				
			public System.String RateTypeName
			{
				get
				{
					System.String data = entity.RateTypeName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RateTypeName = null;
					else entity.RateTypeName = Convert.ToString(value);
				}
			}
				
			public System.String RateTypeDisplayName
			{
				get
				{
					System.String data = entity.RateTypeDisplayName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RateTypeDisplayName = null;
					else entity.RateTypeDisplayName = Convert.ToString(value);
				}
			}
				
			public System.String RateTypeIsEnabled
			{
				get
				{
					System.Boolean? data = entity.RateTypeIsEnabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RateTypeIsEnabled = null;
					else entity.RateTypeIsEnabled = Convert.ToBoolean(value);
				}
			}
				
			public System.String RateTypeSortOrder
			{
				get
				{
					System.Int16? data = entity.RateTypeSortOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RateTypeSortOrder = null;
					else entity.RateTypeSortOrder = Convert.ToInt16(value);
				}
			}
			

			private esvShippingServiceRates entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return vShippingServiceRatesMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public vShippingServiceRatesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vShippingServiceRatesQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vShippingServiceRatesQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(vShippingServiceRatesQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private vShippingServiceRatesQuery query;		
	}



	[Serializable]
	abstract public partial class esvShippingServiceRatesCollection : esEntityCollection<vShippingServiceRates>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vShippingServiceRatesMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "vShippingServiceRatesCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public vShippingServiceRatesQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vShippingServiceRatesQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vShippingServiceRatesQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vShippingServiceRatesQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(vShippingServiceRatesQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((vShippingServiceRatesQuery)query);
		}

		#endregion
		
		private vShippingServiceRatesQuery query;
	}



	[Serializable]
	abstract public partial class esvShippingServiceRatesQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return vShippingServiceRatesMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "RateId": return this.RateId;
				case "CountryCode": return this.CountryCode;
				case "Region": return this.Region;
				case "WeightMin": return this.WeightMin;
				case "WeightMax": return this.WeightMax;
				case "Cost": return this.Cost;
				case "RateTypeId": return this.RateTypeId;
				case "RateTypeName": return this.RateTypeName;
				case "RateTypeDisplayName": return this.RateTypeDisplayName;
				case "RateTypeIsEnabled": return this.RateTypeIsEnabled;
				case "RateTypeSortOrder": return this.RateTypeSortOrder;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem RateId
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.RateId, esSystemType.Int32); }
		} 
		
		public esQueryItem CountryCode
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.CountryCode, esSystemType.String); }
		} 
		
		public esQueryItem Region
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.Region, esSystemType.String); }
		} 
		
		public esQueryItem WeightMin
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.WeightMin, esSystemType.Decimal); }
		} 
		
		public esQueryItem WeightMax
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.WeightMax, esSystemType.Decimal); }
		} 
		
		public esQueryItem Cost
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.Cost, esSystemType.Decimal); }
		} 
		
		public esQueryItem RateTypeId
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.RateTypeId, esSystemType.Int32); }
		} 
		
		public esQueryItem RateTypeName
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.RateTypeName, esSystemType.String); }
		} 
		
		public esQueryItem RateTypeDisplayName
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.RateTypeDisplayName, esSystemType.String); }
		} 
		
		public esQueryItem RateTypeIsEnabled
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.RateTypeIsEnabled, esSystemType.Boolean); }
		} 
		
		public esQueryItem RateTypeSortOrder
		{
			get { return new esQueryItem(this, vShippingServiceRatesMetadata.ColumnNames.RateTypeSortOrder, esSystemType.Int16); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class vShippingServiceRatesMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vShippingServiceRatesMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.RateId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.RateId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.CountryCode, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.CountryCode;
			c.CharacterMaxLength = 2;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.Region, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.Region;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.WeightMin, 3, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.WeightMin;
			c.NumericPrecision = 14;
			c.NumericScale = 4;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.WeightMax, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.WeightMax;
			c.NumericPrecision = 14;
			c.NumericScale = 4;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.Cost, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.Cost;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.RateTypeId, 6, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.RateTypeId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.RateTypeName, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.RateTypeName;
			c.CharacterMaxLength = 100;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.RateTypeDisplayName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.RateTypeDisplayName;
			c.CharacterMaxLength = 100;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.RateTypeIsEnabled, 9, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.RateTypeIsEnabled;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingServiceRatesMetadata.ColumnNames.RateTypeSortOrder, 10, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = vShippingServiceRatesMetadata.PropertyNames.RateTypeSortOrder;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public vShippingServiceRatesMetadata Meta()
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
			 public const string RateId = "RateId";
			 public const string CountryCode = "CountryCode";
			 public const string Region = "Region";
			 public const string WeightMin = "WeightMin";
			 public const string WeightMax = "WeightMax";
			 public const string Cost = "Cost";
			 public const string RateTypeId = "RateTypeId";
			 public const string RateTypeName = "RateTypeName";
			 public const string RateTypeDisplayName = "RateTypeDisplayName";
			 public const string RateTypeIsEnabled = "RateTypeIsEnabled";
			 public const string RateTypeSortOrder = "RateTypeSortOrder";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string RateId = "RateId";
			 public const string CountryCode = "CountryCode";
			 public const string Region = "Region";
			 public const string WeightMin = "WeightMin";
			 public const string WeightMax = "WeightMax";
			 public const string Cost = "Cost";
			 public const string RateTypeId = "RateTypeId";
			 public const string RateTypeName = "RateTypeName";
			 public const string RateTypeDisplayName = "RateTypeDisplayName";
			 public const string RateTypeIsEnabled = "RateTypeIsEnabled";
			 public const string RateTypeSortOrder = "RateTypeSortOrder";
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
			lock (typeof(vShippingServiceRatesMetadata))
			{
				if(vShippingServiceRatesMetadata.mapDelegates == null)
				{
					vShippingServiceRatesMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vShippingServiceRatesMetadata.meta == null)
				{
					vShippingServiceRatesMetadata.meta = new vShippingServiceRatesMetadata();
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


				meta.AddTypeMap("RateId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CountryCode", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Region", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("WeightMin", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("WeightMax", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("Cost", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("RateTypeId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("RateTypeName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RateTypeDisplayName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("RateTypeIsEnabled", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("RateTypeSortOrder", new esTypeMap("smallint", "System.Int16"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "vDNNspot_Store_ShippingServiceRates";
					meta.Destination = objectQualifier + "vDNNspot_Store_ShippingServiceRates";
					
					meta.spInsert = objectQualifier + "proc_vDNNspot_Store_ShippingServiceRatesInsert";				
					meta.spUpdate = objectQualifier + "proc_vDNNspot_Store_ShippingServiceRatesUpdate";		
					meta.spDelete = objectQualifier + "proc_vDNNspot_Store_ShippingServiceRatesDelete";
					meta.spLoadAll = objectQualifier + "proc_vDNNspot_Store_ShippingServiceRatesLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_vDNNspot_Store_ShippingServiceRatesLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "vDNNspot_Store_ShippingServiceRates";
					meta.Destination = "vDNNspot_Store_ShippingServiceRates";
									
					meta.spInsert = "proc_vDNNspot_Store_ShippingServiceRatesInsert";				
					meta.spUpdate = "proc_vDNNspot_Store_ShippingServiceRatesUpdate";		
					meta.spDelete = "proc_vDNNspot_Store_ShippingServiceRatesDelete";
					meta.spLoadAll = "proc_vDNNspot_Store_ShippingServiceRatesLoadAll";
					meta.spLoadByPrimaryKey = "proc_vDNNspot_Store_ShippingServiceRatesLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vShippingServiceRatesMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
