
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
	/// Encapsulates the 'vDNNspot_Store_ShippingRateWeight' view
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(vShippingRateWeight))]	
	[XmlType("vShippingRateWeight")]
	public partial class vShippingRateWeight : esvShippingRateWeight
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new vShippingRateWeight();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("vShippingRateWeightCollection")]
	public partial class vShippingRateWeightCollection : esvShippingRateWeightCollection, IEnumerable<vShippingRateWeight>
	{

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(vShippingRateWeight))]
		public class vShippingRateWeightCollectionWCFPacket : esCollectionWCFPacket<vShippingRateWeightCollection>
		{
			public static implicit operator vShippingRateWeightCollection(vShippingRateWeightCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator vShippingRateWeightCollectionWCFPacket(vShippingRateWeightCollection collection)
			{
				return new vShippingRateWeightCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class vShippingRateWeightQuery : esvShippingRateWeightQuery
	{
		public vShippingRateWeightQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "vShippingRateWeightQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(vShippingRateWeightQuery query)
		{
			return vShippingRateWeightQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator vShippingRateWeightQuery(string query)
		{
			return (vShippingRateWeightQuery)vShippingRateWeightQuery.SerializeHelper.FromXml(query, typeof(vShippingRateWeightQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esvShippingRateWeight : esEntity
	{
		public esvShippingRateWeight()
		{

		}
		
		#region LoadByPrimaryKey
		
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingRateWeight.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(vShippingRateWeightMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(vShippingRateWeightMetadata.ColumnNames.StoreId, value))
				{
					OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingRateWeight.ShippingMethodId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? ShippingMethodId
		{
			get
			{
				return base.GetSystemInt16(vShippingRateWeightMetadata.ColumnNames.ShippingMethodId);
			}
			
			set
			{
				if(base.SetSystemInt16(vShippingRateWeightMetadata.ColumnNames.ShippingMethodId, value))
				{
					OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.ShippingMethodId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingRateWeight.ShippingMethodName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShippingMethodName
		{
			get
			{
				return base.GetSystemString(vShippingRateWeightMetadata.ColumnNames.ShippingMethodName);
			}
			
			set
			{
				if(base.SetSystemString(vShippingRateWeightMetadata.ColumnNames.ShippingMethodName, value))
				{
					OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.ShippingMethodName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingRateWeight.ShippingRateWeightId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ShippingRateWeightId
		{
			get
			{
				return base.GetSystemInt32(vShippingRateWeightMetadata.ColumnNames.ShippingRateWeightId);
			}
			
			set
			{
				if(base.SetSystemInt32(vShippingRateWeightMetadata.ColumnNames.ShippingRateWeightId, value))
				{
					OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.ShippingRateWeightId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingRateWeight.MinRange
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? MinRange
		{
			get
			{
				return base.GetSystemDecimal(vShippingRateWeightMetadata.ColumnNames.MinRange);
			}
			
			set
			{
				if(base.SetSystemDecimal(vShippingRateWeightMetadata.ColumnNames.MinRange, value))
				{
					OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.MinRange);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingRateWeight.MaxRange
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? MaxRange
		{
			get
			{
				return base.GetSystemDecimal(vShippingRateWeightMetadata.ColumnNames.MaxRange);
			}
			
			set
			{
				if(base.SetSystemDecimal(vShippingRateWeightMetadata.ColumnNames.MaxRange, value))
				{
					OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.MaxRange);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ShippingRateWeight.ShippingCost
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? ShippingCost
		{
			get
			{
				return base.GetSystemDecimal(vShippingRateWeightMetadata.ColumnNames.ShippingCost);
			}
			
			set
			{
				if(base.SetSystemDecimal(vShippingRateWeightMetadata.ColumnNames.ShippingCost, value))
				{
					OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.ShippingCost);
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
						case "StoreId": this.str().StoreId = (string)value; break;							
						case "ShippingMethodId": this.str().ShippingMethodId = (string)value; break;							
						case "ShippingMethodName": this.str().ShippingMethodName = (string)value; break;							
						case "ShippingRateWeightId": this.str().ShippingRateWeightId = (string)value; break;							
						case "MinRange": this.str().MinRange = (string)value; break;							
						case "MaxRange": this.str().MaxRange = (string)value; break;							
						case "ShippingCost": this.str().ShippingCost = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.StoreId);
							break;
						
						case "ShippingMethodId":
						
							if (value == null || value is System.Int16)
								this.ShippingMethodId = (System.Int16?)value;
								OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.ShippingMethodId);
							break;
						
						case "ShippingRateWeightId":
						
							if (value == null || value is System.Int32)
								this.ShippingRateWeightId = (System.Int32?)value;
								OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.ShippingRateWeightId);
							break;
						
						case "MinRange":
						
							if (value == null || value is System.Decimal)
								this.MinRange = (System.Decimal?)value;
								OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.MinRange);
							break;
						
						case "MaxRange":
						
							if (value == null || value is System.Decimal)
								this.MaxRange = (System.Decimal?)value;
								OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.MaxRange);
							break;
						
						case "ShippingCost":
						
							if (value == null || value is System.Decimal)
								this.ShippingCost = (System.Decimal?)value;
								OnPropertyChanged(vShippingRateWeightMetadata.PropertyNames.ShippingCost);
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
			public esStrings(esvShippingRateWeight entity)
			{
				this.entity = entity;
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
				
			public System.String ShippingMethodId
			{
				get
				{
					System.Int16? data = entity.ShippingMethodId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingMethodId = null;
					else entity.ShippingMethodId = Convert.ToInt16(value);
				}
			}
				
			public System.String ShippingMethodName
			{
				get
				{
					System.String data = entity.ShippingMethodName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingMethodName = null;
					else entity.ShippingMethodName = Convert.ToString(value);
				}
			}
				
			public System.String ShippingRateWeightId
			{
				get
				{
					System.Int32? data = entity.ShippingRateWeightId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingRateWeightId = null;
					else entity.ShippingRateWeightId = Convert.ToInt32(value);
				}
			}
				
			public System.String MinRange
			{
				get
				{
					System.Decimal? data = entity.MinRange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MinRange = null;
					else entity.MinRange = Convert.ToDecimal(value);
				}
			}
				
			public System.String MaxRange
			{
				get
				{
					System.Decimal? data = entity.MaxRange;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MaxRange = null;
					else entity.MaxRange = Convert.ToDecimal(value);
				}
			}
				
			public System.String ShippingCost
			{
				get
				{
					System.Decimal? data = entity.ShippingCost;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingCost = null;
					else entity.ShippingCost = Convert.ToDecimal(value);
				}
			}
			

			private esvShippingRateWeight entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return vShippingRateWeightMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public vShippingRateWeightQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vShippingRateWeightQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vShippingRateWeightQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(vShippingRateWeightQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private vShippingRateWeightQuery query;		
	}



	[Serializable]
	abstract public partial class esvShippingRateWeightCollection : esEntityCollection<vShippingRateWeight>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vShippingRateWeightMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "vShippingRateWeightCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public vShippingRateWeightQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vShippingRateWeightQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vShippingRateWeightQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vShippingRateWeightQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(vShippingRateWeightQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((vShippingRateWeightQuery)query);
		}

		#endregion
		
		private vShippingRateWeightQuery query;
	}



	[Serializable]
	abstract public partial class esvShippingRateWeightQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return vShippingRateWeightMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StoreId": return this.StoreId;
				case "ShippingMethodId": return this.ShippingMethodId;
				case "ShippingMethodName": return this.ShippingMethodName;
				case "ShippingRateWeightId": return this.ShippingRateWeightId;
				case "MinRange": return this.MinRange;
				case "MaxRange": return this.MaxRange;
				case "ShippingCost": return this.ShippingCost;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, vShippingRateWeightMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem ShippingMethodId
		{
			get { return new esQueryItem(this, vShippingRateWeightMetadata.ColumnNames.ShippingMethodId, esSystemType.Int16); }
		} 
		
		public esQueryItem ShippingMethodName
		{
			get { return new esQueryItem(this, vShippingRateWeightMetadata.ColumnNames.ShippingMethodName, esSystemType.String); }
		} 
		
		public esQueryItem ShippingRateWeightId
		{
			get { return new esQueryItem(this, vShippingRateWeightMetadata.ColumnNames.ShippingRateWeightId, esSystemType.Int32); }
		} 
		
		public esQueryItem MinRange
		{
			get { return new esQueryItem(this, vShippingRateWeightMetadata.ColumnNames.MinRange, esSystemType.Decimal); }
		} 
		
		public esQueryItem MaxRange
		{
			get { return new esQueryItem(this, vShippingRateWeightMetadata.ColumnNames.MaxRange, esSystemType.Decimal); }
		} 
		
		public esQueryItem ShippingCost
		{
			get { return new esQueryItem(this, vShippingRateWeightMetadata.ColumnNames.ShippingCost, esSystemType.Decimal); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class vShippingRateWeightMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vShippingRateWeightMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(vShippingRateWeightMetadata.ColumnNames.StoreId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vShippingRateWeightMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingRateWeightMetadata.ColumnNames.ShippingMethodId, 1, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = vShippingRateWeightMetadata.PropertyNames.ShippingMethodId;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingRateWeightMetadata.ColumnNames.ShippingMethodName, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = vShippingRateWeightMetadata.PropertyNames.ShippingMethodName;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingRateWeightMetadata.ColumnNames.ShippingRateWeightId, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vShippingRateWeightMetadata.PropertyNames.ShippingRateWeightId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingRateWeightMetadata.ColumnNames.MinRange, 4, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vShippingRateWeightMetadata.PropertyNames.MinRange;
			c.NumericPrecision = 14;
			c.NumericScale = 4;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingRateWeightMetadata.ColumnNames.MaxRange, 5, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vShippingRateWeightMetadata.PropertyNames.MaxRange;
			c.NumericPrecision = 14;
			c.NumericScale = 4;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vShippingRateWeightMetadata.ColumnNames.ShippingCost, 6, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vShippingRateWeightMetadata.PropertyNames.ShippingCost;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public vShippingRateWeightMetadata Meta()
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
			 public const string StoreId = "StoreId";
			 public const string ShippingMethodId = "ShippingMethodId";
			 public const string ShippingMethodName = "ShippingMethodName";
			 public const string ShippingRateWeightId = "ShippingRateWeightId";
			 public const string MinRange = "MinRange";
			 public const string MaxRange = "MaxRange";
			 public const string ShippingCost = "ShippingCost";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StoreId = "StoreId";
			 public const string ShippingMethodId = "ShippingMethodId";
			 public const string ShippingMethodName = "ShippingMethodName";
			 public const string ShippingRateWeightId = "ShippingRateWeightId";
			 public const string MinRange = "MinRange";
			 public const string MaxRange = "MaxRange";
			 public const string ShippingCost = "ShippingCost";
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
			lock (typeof(vShippingRateWeightMetadata))
			{
				if(vShippingRateWeightMetadata.mapDelegates == null)
				{
					vShippingRateWeightMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vShippingRateWeightMetadata.meta == null)
				{
					vShippingRateWeightMetadata.meta = new vShippingRateWeightMetadata();
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


				meta.AddTypeMap("StoreId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ShippingMethodId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("ShippingMethodName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ShippingRateWeightId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MinRange", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("MaxRange", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ShippingCost", new esTypeMap("money", "System.Decimal"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "vDNNspot_Store_ShippingRateWeight";
					meta.Destination = objectQualifier + "vDNNspot_Store_ShippingRateWeight";
					
					meta.spInsert = objectQualifier + "proc_vDNNspot_Store_ShippingRateWeightInsert";				
					meta.spUpdate = objectQualifier + "proc_vDNNspot_Store_ShippingRateWeightUpdate";		
					meta.spDelete = objectQualifier + "proc_vDNNspot_Store_ShippingRateWeightDelete";
					meta.spLoadAll = objectQualifier + "proc_vDNNspot_Store_ShippingRateWeightLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_vDNNspot_Store_ShippingRateWeightLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "vDNNspot_Store_ShippingRateWeight";
					meta.Destination = "vDNNspot_Store_ShippingRateWeight";
									
					meta.spInsert = "proc_vDNNspot_Store_ShippingRateWeightInsert";				
					meta.spUpdate = "proc_vDNNspot_Store_ShippingRateWeightUpdate";		
					meta.spDelete = "proc_vDNNspot_Store_ShippingRateWeightDelete";
					meta.spLoadAll = "proc_vDNNspot_Store_ShippingRateWeightLoadAll";
					meta.spLoadByPrimaryKey = "proc_vDNNspot_Store_ShippingRateWeightLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vShippingRateWeightMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
