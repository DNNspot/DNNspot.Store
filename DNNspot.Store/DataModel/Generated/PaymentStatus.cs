
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
	/// Encapsulates the 'DNNspot_Store_PaymentStatus' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(PaymentStatus))]	
	[XmlType("PaymentStatus")]
	[Table(Name="PaymentStatus")]
	public partial class PaymentStatus : esPaymentStatus
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new PaymentStatus();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int16 id)
		{
			var obj = new PaymentStatus();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int16 id, esSqlAccessType sqlAccessType)
		{
			var obj = new PaymentStatus();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int16? Id
		{
			get { return base.Id;  }
			set { base.Id = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("PaymentStatusCollection")]
	public partial class PaymentStatusCollection : esPaymentStatusCollection, IEnumerable<PaymentStatus>
	{
		public PaymentStatus FindByPrimaryKey(System.Int16 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(PaymentStatus))]
		public class PaymentStatusCollectionWCFPacket : esCollectionWCFPacket<PaymentStatusCollection>
		{
			public static implicit operator PaymentStatusCollection(PaymentStatusCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator PaymentStatusCollectionWCFPacket(PaymentStatusCollection collection)
			{
				return new PaymentStatusCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class PaymentStatusQuery : esPaymentStatusQuery
	{
		public PaymentStatusQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "PaymentStatusQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(PaymentStatusQuery query)
		{
			return PaymentStatusQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator PaymentStatusQuery(string query)
		{
			return (PaymentStatusQuery)PaymentStatusQuery.SerializeHelper.FromXml(query, typeof(PaymentStatusQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esPaymentStatus : esEntity
	{
		public esPaymentStatus()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int16 id)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int16 id)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(id);
			else
				return LoadByPrimaryKeyStoredProcedure(id);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int16 id)
		{
			PaymentStatusQuery query = new PaymentStatusQuery();
			query.Where(query.Id == id);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int16 id)
		{
			esParameters parms = new esParameters();
			parms.Add("Id", id);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentStatus.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? Id
		{
			get
			{
				return base.GetSystemInt16(PaymentStatusMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt16(PaymentStatusMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(PaymentStatusMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_PaymentStatus.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(PaymentStatusMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(PaymentStatusMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(PaymentStatusMetadata.PropertyNames.Name);
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
						case "Name": this.str().Name = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int16)
								this.Id = (System.Int16?)value;
								OnPropertyChanged(PaymentStatusMetadata.PropertyNames.Id);
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
			public esStrings(esPaymentStatus entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Id
			{
				get
				{
					System.Int16? data = entity.Id;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Id = null;
					else entity.Id = Convert.ToInt16(value);
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
			

			private esPaymentStatus entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return PaymentStatusMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public PaymentStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentStatusQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PaymentStatusQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(PaymentStatusQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private PaymentStatusQuery query;		
	}



	[Serializable]
	abstract public partial class esPaymentStatusCollection : esEntityCollection<PaymentStatus>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return PaymentStatusMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "PaymentStatusCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public PaymentStatusQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new PaymentStatusQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(PaymentStatusQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new PaymentStatusQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(PaymentStatusQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((PaymentStatusQuery)query);
		}

		#endregion
		
		private PaymentStatusQuery query;
	}



	[Serializable]
	abstract public partial class esPaymentStatusQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return PaymentStatusMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "Name": return this.Name;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, PaymentStatusMetadata.ColumnNames.Id, esSystemType.Int16); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, PaymentStatusMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class PaymentStatus : esPaymentStatus
	{

		#region OrderCollectionByPaymentStatusId - Zero To Many
		
		static public esPrefetchMap Prefetch_OrderCollectionByPaymentStatusId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.PaymentStatus.OrderCollectionByPaymentStatusId_Delegate;
				map.PropertyName = "OrderCollectionByPaymentStatusId";
				map.MyColumnName = "PaymentStatusId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void OrderCollectionByPaymentStatusId_Delegate(esPrefetchParameters data)
		{
			PaymentStatusQuery parent = new PaymentStatusQuery(data.NextAlias());

			OrderQuery me = data.You != null ? data.You as OrderQuery : new OrderQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.PaymentStatusId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Order_DNNspot_Store_PaymentStatus
		/// </summary>

		[XmlIgnore]
		public OrderCollection OrderCollectionByPaymentStatusId
		{
			get
			{
				if(this._OrderCollectionByPaymentStatusId == null)
				{
					this._OrderCollectionByPaymentStatusId = new OrderCollection();
					this._OrderCollectionByPaymentStatusId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("OrderCollectionByPaymentStatusId", this._OrderCollectionByPaymentStatusId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._OrderCollectionByPaymentStatusId.Query.Where(this._OrderCollectionByPaymentStatusId.Query.PaymentStatusId == this.Id);
							this._OrderCollectionByPaymentStatusId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._OrderCollectionByPaymentStatusId.fks.Add(OrderMetadata.ColumnNames.PaymentStatusId, this.Id);
					}
				}

				return this._OrderCollectionByPaymentStatusId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._OrderCollectionByPaymentStatusId != null) 
				{ 
					this.RemovePostSave("OrderCollectionByPaymentStatusId"); 
					this._OrderCollectionByPaymentStatusId = null;
					
				} 
			} 			
		}
			
		
		private OrderCollection _OrderCollectionByPaymentStatusId;
		#endregion

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "OrderCollectionByPaymentStatusId":
					coll = this.OrderCollectionByPaymentStatusId;
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
			
			props.Add(new esPropertyDescriptor(this, "OrderCollectionByPaymentStatusId", typeof(OrderCollection), new Order()));
		
			return props;
		}
		
	}
	



	[Serializable]
	public partial class PaymentStatusMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected PaymentStatusMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(PaymentStatusMetadata.ColumnNames.Id, 0, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = PaymentStatusMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(PaymentStatusMetadata.ColumnNames.Name, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = PaymentStatusMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public PaymentStatusMetadata Meta()
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
			 public const string Name = "Name";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string Name = "Name";
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
			lock (typeof(PaymentStatusMetadata))
			{
				if(PaymentStatusMetadata.mapDelegates == null)
				{
					PaymentStatusMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (PaymentStatusMetadata.meta == null)
				{
					PaymentStatusMetadata.meta = new PaymentStatusMetadata();
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


				meta.AddTypeMap("Id", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_PaymentStatus";
					meta.Destination = objectQualifier + "DNNspot_Store_PaymentStatus";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_PaymentStatusInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_PaymentStatusUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_PaymentStatusDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_PaymentStatusLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_PaymentStatusLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_PaymentStatus";
					meta.Destination = "DNNspot_Store_PaymentStatus";
									
					meta.spInsert = "proc_DNNspot_Store_PaymentStatusInsert";				
					meta.spUpdate = "proc_DNNspot_Store_PaymentStatusUpdate";		
					meta.spDelete = "proc_DNNspot_Store_PaymentStatusDelete";
					meta.spLoadAll = "proc_DNNspot_Store_PaymentStatusLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_PaymentStatusLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private PaymentStatusMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
