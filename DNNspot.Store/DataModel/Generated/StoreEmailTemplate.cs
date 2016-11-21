
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
	/// Encapsulates the 'DNNspot_Store_StoreEmailTemplate' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(StoreEmailTemplate))]	
	[XmlType("StoreEmailTemplate")]
	[Table(Name="StoreEmailTemplate")]
	public partial class StoreEmailTemplate : esStoreEmailTemplate
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new StoreEmailTemplate();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 storeId, System.Int16 emailTemplateId)
		{
			var obj = new StoreEmailTemplate();
			obj.StoreId = storeId;
			obj.EmailTemplateId = emailTemplateId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 storeId, System.Int16 emailTemplateId, esSqlAccessType sqlAccessType)
		{
			var obj = new StoreEmailTemplate();
			obj.StoreId = storeId;
			obj.EmailTemplateId = emailTemplateId;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int32? StoreId
		{
			get { return base.StoreId;  }
			set { base.StoreId = value; }
		}

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.Int16? EmailTemplateId
		{
			get { return base.EmailTemplateId;  }
			set { base.EmailTemplateId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SubjectTemplate
		{
			get { return base.SubjectTemplate;  }
			set { base.SubjectTemplate = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String BodyTemplate
		{
			get { return base.BodyTemplate;  }
			set { base.BodyTemplate = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("StoreEmailTemplateCollection")]
	public partial class StoreEmailTemplateCollection : esStoreEmailTemplateCollection, IEnumerable<StoreEmailTemplate>
	{
		public StoreEmailTemplate FindByPrimaryKey(System.Int32 storeId, System.Int16 emailTemplateId)
		{
			return this.SingleOrDefault(e => e.StoreId == storeId && e.EmailTemplateId == emailTemplateId);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(StoreEmailTemplate))]
		public class StoreEmailTemplateCollectionWCFPacket : esCollectionWCFPacket<StoreEmailTemplateCollection>
		{
			public static implicit operator StoreEmailTemplateCollection(StoreEmailTemplateCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator StoreEmailTemplateCollectionWCFPacket(StoreEmailTemplateCollection collection)
			{
				return new StoreEmailTemplateCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class StoreEmailTemplateQuery : esStoreEmailTemplateQuery
	{
		public StoreEmailTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "StoreEmailTemplateQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(StoreEmailTemplateQuery query)
		{
			return StoreEmailTemplateQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator StoreEmailTemplateQuery(string query)
		{
			return (StoreEmailTemplateQuery)StoreEmailTemplateQuery.SerializeHelper.FromXml(query, typeof(StoreEmailTemplateQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esStoreEmailTemplate : esEntity
	{
		public esStoreEmailTemplate()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.Int32 storeId, System.Int16 emailTemplateId)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, emailTemplateId);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, emailTemplateId);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.Int32 storeId, System.Int16 emailTemplateId)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(storeId, emailTemplateId);
			else
				return LoadByPrimaryKeyStoredProcedure(storeId, emailTemplateId);
		}

		private bool LoadByPrimaryKeyDynamic(System.Int32 storeId, System.Int16 emailTemplateId)
		{
			StoreEmailTemplateQuery query = new StoreEmailTemplateQuery();
			query.Where(query.StoreId == storeId, query.EmailTemplateId == emailTemplateId);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.Int32 storeId, System.Int16 emailTemplateId)
		{
			esParameters parms = new esParameters();
			parms.Add("StoreId", storeId);			parms.Add("EmailTemplateId", emailTemplateId);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_StoreEmailTemplate.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(StoreEmailTemplateMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(StoreEmailTemplateMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(StoreEmailTemplateMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StoreEmailTemplate.EmailTemplateId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? EmailTemplateId
		{
			get
			{
				return base.GetSystemInt16(StoreEmailTemplateMetadata.ColumnNames.EmailTemplateId);
			}
			
			set
			{
				if(base.SetSystemInt16(StoreEmailTemplateMetadata.ColumnNames.EmailTemplateId, value))
				{
					this._UpToEmailTemplateByEmailTemplateId = null;
					this.OnPropertyChanged("UpToEmailTemplateByEmailTemplateId");
					OnPropertyChanged(StoreEmailTemplateMetadata.PropertyNames.EmailTemplateId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StoreEmailTemplate.SubjectTemplate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SubjectTemplate
		{
			get
			{
				return base.GetSystemString(StoreEmailTemplateMetadata.ColumnNames.SubjectTemplate);
			}
			
			set
			{
				if(base.SetSystemString(StoreEmailTemplateMetadata.ColumnNames.SubjectTemplate, value))
				{
					OnPropertyChanged(StoreEmailTemplateMetadata.PropertyNames.SubjectTemplate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_StoreEmailTemplate.BodyTemplate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BodyTemplate
		{
			get
			{
				return base.GetSystemString(StoreEmailTemplateMetadata.ColumnNames.BodyTemplate);
			}
			
			set
			{
				if(base.SetSystemString(StoreEmailTemplateMetadata.ColumnNames.BodyTemplate, value))
				{
					OnPropertyChanged(StoreEmailTemplateMetadata.PropertyNames.BodyTemplate);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected EmailTemplate _UpToEmailTemplateByEmailTemplateId;
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
						case "StoreId": this.str().StoreId = (string)value; break;							
						case "EmailTemplateId": this.str().EmailTemplateId = (string)value; break;							
						case "SubjectTemplate": this.str().SubjectTemplate = (string)value; break;							
						case "BodyTemplate": this.str().BodyTemplate = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(StoreEmailTemplateMetadata.PropertyNames.StoreId);
							break;
						
						case "EmailTemplateId":
						
							if (value == null || value is System.Int16)
								this.EmailTemplateId = (System.Int16?)value;
								OnPropertyChanged(StoreEmailTemplateMetadata.PropertyNames.EmailTemplateId);
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
			public esStrings(esStoreEmailTemplate entity)
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
				
			public System.String EmailTemplateId
			{
				get
				{
					System.Int16? data = entity.EmailTemplateId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.EmailTemplateId = null;
					else entity.EmailTemplateId = Convert.ToInt16(value);
				}
			}
				
			public System.String SubjectTemplate
			{
				get
				{
					System.String data = entity.SubjectTemplate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SubjectTemplate = null;
					else entity.SubjectTemplate = Convert.ToString(value);
				}
			}
				
			public System.String BodyTemplate
			{
				get
				{
					System.String data = entity.BodyTemplate;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.BodyTemplate = null;
					else entity.BodyTemplate = Convert.ToString(value);
				}
			}
			

			private esStoreEmailTemplate entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return StoreEmailTemplateMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public StoreEmailTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StoreEmailTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StoreEmailTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(StoreEmailTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private StoreEmailTemplateQuery query;		
	}



	[Serializable]
	abstract public partial class esStoreEmailTemplateCollection : esEntityCollection<StoreEmailTemplate>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return StoreEmailTemplateMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "StoreEmailTemplateCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public StoreEmailTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new StoreEmailTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(StoreEmailTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new StoreEmailTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(StoreEmailTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((StoreEmailTemplateQuery)query);
		}

		#endregion
		
		private StoreEmailTemplateQuery query;
	}



	[Serializable]
	abstract public partial class esStoreEmailTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return StoreEmailTemplateMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "StoreId": return this.StoreId;
				case "EmailTemplateId": return this.EmailTemplateId;
				case "SubjectTemplate": return this.SubjectTemplate;
				case "BodyTemplate": return this.BodyTemplate;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, StoreEmailTemplateMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem EmailTemplateId
		{
			get { return new esQueryItem(this, StoreEmailTemplateMetadata.ColumnNames.EmailTemplateId, esSystemType.Int16); }
		} 
		
		public esQueryItem SubjectTemplate
		{
			get { return new esQueryItem(this, StoreEmailTemplateMetadata.ColumnNames.SubjectTemplate, esSystemType.String); }
		} 
		
		public esQueryItem BodyTemplate
		{
			get { return new esQueryItem(this, StoreEmailTemplateMetadata.ColumnNames.BodyTemplate, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class StoreEmailTemplate : esStoreEmailTemplate
	{

				
		#region UpToEmailTemplateByEmailTemplateId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_EmailTemplate
		/// </summary>

		[XmlIgnore]
					
		public EmailTemplate UpToEmailTemplateByEmailTemplateId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToEmailTemplateByEmailTemplateId == null && EmailTemplateId != null)
				{
					this._UpToEmailTemplateByEmailTemplateId = new EmailTemplate();
					this._UpToEmailTemplateByEmailTemplateId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToEmailTemplateByEmailTemplateId", this._UpToEmailTemplateByEmailTemplateId);
					this._UpToEmailTemplateByEmailTemplateId.Query.Where(this._UpToEmailTemplateByEmailTemplateId.Query.Id == this.EmailTemplateId);
					this._UpToEmailTemplateByEmailTemplateId.Query.Load();
				}	
				return this._UpToEmailTemplateByEmailTemplateId;
			}
			
			set
			{
				this.RemovePreSave("UpToEmailTemplateByEmailTemplateId");
				

				if(value == null)
				{
					this.EmailTemplateId = null;
					this._UpToEmailTemplateByEmailTemplateId = null;
				}
				else
				{
					this.EmailTemplateId = value.Id;
					this._UpToEmailTemplateByEmailTemplateId = value;
					this.SetPreSave("UpToEmailTemplateByEmailTemplateId", this._UpToEmailTemplateByEmailTemplateId);
				}
				
			}
		}
		#endregion
		

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_Store
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
	public partial class StoreEmailTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected StoreEmailTemplateMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(StoreEmailTemplateMetadata.ColumnNames.StoreId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = StoreEmailTemplateMetadata.PropertyNames.StoreId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreEmailTemplateMetadata.ColumnNames.EmailTemplateId, 1, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = StoreEmailTemplateMetadata.PropertyNames.EmailTemplateId;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreEmailTemplateMetadata.ColumnNames.SubjectTemplate, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = StoreEmailTemplateMetadata.PropertyNames.SubjectTemplate;
			c.CharacterMaxLength = 300;
			m_columns.Add(c);
				
			c = new esColumnMetadata(StoreEmailTemplateMetadata.ColumnNames.BodyTemplate, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = StoreEmailTemplateMetadata.PropertyNames.BodyTemplate;
			c.CharacterMaxLength = 1073741823;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public StoreEmailTemplateMetadata Meta()
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
			 public const string EmailTemplateId = "EmailTemplateId";
			 public const string SubjectTemplate = "SubjectTemplate";
			 public const string BodyTemplate = "BodyTemplate";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string StoreId = "StoreId";
			 public const string EmailTemplateId = "EmailTemplateId";
			 public const string SubjectTemplate = "SubjectTemplate";
			 public const string BodyTemplate = "BodyTemplate";
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
			lock (typeof(StoreEmailTemplateMetadata))
			{
				if(StoreEmailTemplateMetadata.mapDelegates == null)
				{
					StoreEmailTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (StoreEmailTemplateMetadata.meta == null)
				{
					StoreEmailTemplateMetadata.meta = new StoreEmailTemplateMetadata();
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
				meta.AddTypeMap("EmailTemplateId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("SubjectTemplate", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("BodyTemplate", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_StoreEmailTemplate";
					meta.Destination = objectQualifier + "DNNspot_Store_StoreEmailTemplate";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_StoreEmailTemplateInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_StoreEmailTemplateUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_StoreEmailTemplateDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_StoreEmailTemplateLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_StoreEmailTemplateLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_StoreEmailTemplate";
					meta.Destination = "DNNspot_Store_StoreEmailTemplate";
									
					meta.spInsert = "proc_DNNspot_Store_StoreEmailTemplateInsert";				
					meta.spUpdate = "proc_DNNspot_Store_StoreEmailTemplateUpdate";		
					meta.spDelete = "proc_DNNspot_Store_StoreEmailTemplateDelete";
					meta.spLoadAll = "proc_DNNspot_Store_StoreEmailTemplateLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_StoreEmailTemplateLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private StoreEmailTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
