
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
	/// Encapsulates the 'DNNspot_Store_ShippingLog' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ShippingLog))]	
	[XmlType("ShippingLog")]
	[Table(Name="ShippingLog")]
	public partial class ShippingLog : esShippingLog
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ShippingLog();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ShippingLog();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ShippingLog();
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
		public override System.String ShippingRequestType
		{
			get { return base.ShippingRequestType;  }
			set { base.ShippingRequestType = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? OrderId
		{
			get { return base.OrderId;  }
			set { base.OrderId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Guid? CartId
		{
			get { return base.CartId;  }
			set { base.CartId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String RequestSent
		{
			get { return base.RequestSent;  }
			set { base.RequestSent = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String ResponseReceived
		{
			get { return base.ResponseReceived;  }
			set { base.ResponseReceived = value; }
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
	[XmlType("ShippingLogCollection")]
	public partial class ShippingLogCollection : esShippingLogCollection, IEnumerable<ShippingLog>
	{
		public ShippingLog FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ShippingLog))]
		public class ShippingLogCollectionWCFPacket : esCollectionWCFPacket<ShippingLogCollection>
		{
			public static implicit operator ShippingLogCollection(ShippingLogCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ShippingLogCollectionWCFPacket(ShippingLogCollection collection)
			{
				return new ShippingLogCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ShippingLogQuery : esShippingLogQuery
	{
		public ShippingLogQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ShippingLogQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ShippingLogQuery query)
		{
			return ShippingLogQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ShippingLogQuery(string query)
		{
			return (ShippingLogQuery)ShippingLogQuery.SerializeHelper.FromXml(query, typeof(ShippingLogQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esShippingLog : esEntity
	{
		public esShippingLog()
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
			ShippingLogQuery query = new ShippingLogQuery();
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
		/// Maps to DNNspot_Store_ShippingLog.id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ShippingLogMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingLogMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ShippingLogMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingLog.ShippingRequestType
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ShippingRequestType
		{
			get
			{
				return base.GetSystemString(ShippingLogMetadata.ColumnNames.ShippingRequestType);
			}
			
			set
			{
				if(base.SetSystemString(ShippingLogMetadata.ColumnNames.ShippingRequestType, value))
				{
					OnPropertyChanged(ShippingLogMetadata.PropertyNames.ShippingRequestType);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingLog.OrderId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? OrderId
		{
			get
			{
				return base.GetSystemInt32(ShippingLogMetadata.ColumnNames.OrderId);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingLogMetadata.ColumnNames.OrderId, value))
				{
					OnPropertyChanged(ShippingLogMetadata.PropertyNames.OrderId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingLog.CartId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Guid? CartId
		{
			get
			{
				return base.GetSystemGuid(ShippingLogMetadata.ColumnNames.CartId);
			}
			
			set
			{
				if(base.SetSystemGuid(ShippingLogMetadata.ColumnNames.CartId, value))
				{
					OnPropertyChanged(ShippingLogMetadata.PropertyNames.CartId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingLog.RequestSent
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String RequestSent
		{
			get
			{
				return base.GetSystemString(ShippingLogMetadata.ColumnNames.RequestSent);
			}
			
			set
			{
				if(base.SetSystemString(ShippingLogMetadata.ColumnNames.RequestSent, value))
				{
					OnPropertyChanged(ShippingLogMetadata.PropertyNames.RequestSent);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingLog.ResponseReceived
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ResponseReceived
		{
			get
			{
				return base.GetSystemString(ShippingLogMetadata.ColumnNames.ResponseReceived);
			}
			
			set
			{
				if(base.SetSystemString(ShippingLogMetadata.ColumnNames.ResponseReceived, value))
				{
					OnPropertyChanged(ShippingLogMetadata.PropertyNames.ResponseReceived);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingLog.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(ShippingLogMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(ShippingLogMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(ShippingLogMetadata.PropertyNames.CreatedOn);
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
						case "ShippingRequestType": this.str().ShippingRequestType = (string)value; break;							
						case "OrderId": this.str().OrderId = (string)value; break;							
						case "CartId": this.str().CartId = (string)value; break;							
						case "RequestSent": this.str().RequestSent = (string)value; break;							
						case "ResponseReceived": this.str().ResponseReceived = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ShippingLogMetadata.PropertyNames.Id);
							break;
						
						case "OrderId":
						
							if (value == null || value is System.Int32)
								this.OrderId = (System.Int32?)value;
								OnPropertyChanged(ShippingLogMetadata.PropertyNames.OrderId);
							break;
						
						case "CartId":
						
							if (value == null || value is System.Guid)
								this.CartId = (System.Guid?)value;
								OnPropertyChanged(ShippingLogMetadata.PropertyNames.CartId);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(ShippingLogMetadata.PropertyNames.CreatedOn);
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
			public esStrings(esShippingLog entity)
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
				
			public System.String ShippingRequestType
			{
				get
				{
					System.String data = entity.ShippingRequestType;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingRequestType = null;
					else entity.ShippingRequestType = Convert.ToString(value);
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
				
			public System.String CartId
			{
				get
				{
					System.Guid? data = entity.CartId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CartId = null;
					else entity.CartId = new Guid(value);
				}
			}
				
			public System.String RequestSent
			{
				get
				{
					System.String data = entity.RequestSent;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.RequestSent = null;
					else entity.RequestSent = Convert.ToString(value);
				}
			}
				
			public System.String ResponseReceived
			{
				get
				{
					System.String data = entity.ResponseReceived;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ResponseReceived = null;
					else entity.ResponseReceived = Convert.ToString(value);
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
			

			private esShippingLog entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ShippingLogMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ShippingLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingLogQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingLogQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ShippingLogQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ShippingLogQuery query;		
	}



	[Serializable]
	abstract public partial class esShippingLogCollection : esEntityCollection<ShippingLog>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ShippingLogMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ShippingLogCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ShippingLogQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingLogQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingLogQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ShippingLogQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ShippingLogQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ShippingLogQuery)query);
		}

		#endregion
		
		private ShippingLogQuery query;
	}



	[Serializable]
	abstract public partial class esShippingLogQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ShippingLogMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "ShippingRequestType": return this.ShippingRequestType;
				case "OrderId": return this.OrderId;
				case "CartId": return this.CartId;
				case "RequestSent": return this.RequestSent;
				case "ResponseReceived": return this.ResponseReceived;
				case "CreatedOn": return this.CreatedOn;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ShippingLogMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem ShippingRequestType
		{
			get { return new esQueryItem(this, ShippingLogMetadata.ColumnNames.ShippingRequestType, esSystemType.String); }
		} 
		
		public esQueryItem OrderId
		{
			get { return new esQueryItem(this, ShippingLogMetadata.ColumnNames.OrderId, esSystemType.Int32); }
		} 
		
		public esQueryItem CartId
		{
			get { return new esQueryItem(this, ShippingLogMetadata.ColumnNames.CartId, esSystemType.Guid); }
		} 
		
		public esQueryItem RequestSent
		{
			get { return new esQueryItem(this, ShippingLogMetadata.ColumnNames.RequestSent, esSystemType.String); }
		} 
		
		public esQueryItem ResponseReceived
		{
			get { return new esQueryItem(this, ShippingLogMetadata.ColumnNames.ResponseReceived, esSystemType.String); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, ShippingLogMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		#endregion
		
	}


	
	public partial class ShippingLog : esShippingLog
	{

		
		
	}
	



	[Serializable]
	public partial class ShippingLogMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ShippingLogMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ShippingLogMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingLogMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingLogMetadata.ColumnNames.ShippingRequestType, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingLogMetadata.PropertyNames.ShippingRequestType;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingLogMetadata.ColumnNames.OrderId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingLogMetadata.PropertyNames.OrderId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingLogMetadata.ColumnNames.CartId, 3, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = ShippingLogMetadata.PropertyNames.CartId;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingLogMetadata.ColumnNames.RequestSent, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingLogMetadata.PropertyNames.RequestSent;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingLogMetadata.ColumnNames.ResponseReceived, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingLogMetadata.PropertyNames.ResponseReceived;
			c.CharacterMaxLength = 2147483647;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingLogMetadata.ColumnNames.CreatedOn, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = ShippingLogMetadata.PropertyNames.CreatedOn;
			c.HasDefault = true;
			c.Default = @"(getdate())";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ShippingLogMetadata Meta()
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
			 public const string Id = "id";
			 public const string ShippingRequestType = "ShippingRequestType";
			 public const string OrderId = "OrderId";
			 public const string CartId = "CartId";
			 public const string RequestSent = "RequestSent";
			 public const string ResponseReceived = "ResponseReceived";
			 public const string CreatedOn = "CreatedOn";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string ShippingRequestType = "ShippingRequestType";
			 public const string OrderId = "OrderId";
			 public const string CartId = "CartId";
			 public const string RequestSent = "RequestSent";
			 public const string ResponseReceived = "ResponseReceived";
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
			lock (typeof(ShippingLogMetadata))
			{
				if(ShippingLogMetadata.mapDelegates == null)
				{
					ShippingLogMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ShippingLogMetadata.meta == null)
				{
					ShippingLogMetadata.meta = new ShippingLogMetadata();
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
				meta.AddTypeMap("ShippingRequestType", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("OrderId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("CartId", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("RequestSent", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ResponseReceived", new esTypeMap("varchar", "System.String"));
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
					meta.Source = objectQualifier + "DNNspot_Store_ShippingLog";
					meta.Destination = objectQualifier + "DNNspot_Store_ShippingLog";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ShippingLogInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ShippingLogUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ShippingLogDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ShippingLogLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ShippingLogLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ShippingLog";
					meta.Destination = "DNNspot_Store_ShippingLog";
									
					meta.spInsert = "proc_DNNspot_Store_ShippingLogInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ShippingLogUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ShippingLogDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ShippingLogLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ShippingLogLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ShippingLogMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
