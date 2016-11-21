
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
	/// Encapsulates the 'DNNspot_Store_EmailTemplate' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(EmailTemplate))]	
	[XmlType("EmailTemplate")]
	[Table(Name="EmailTemplate")]
	public partial class EmailTemplate : esEmailTemplate
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new EmailTemplate();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int16 id)
		{
			var obj = new EmailTemplate();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int16 id, esSqlAccessType sqlAccessType)
		{
			var obj = new EmailTemplate();
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
		public override System.String NameKey
		{
			get { return base.NameKey;  }
			set { base.NameKey = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Description
		{
			get { return base.Description;  }
			set { base.Description = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DefaultSubject
		{
			get { return base.DefaultSubject;  }
			set { base.DefaultSubject = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DefaultBody
		{
			get { return base.DefaultBody;  }
			set { base.DefaultBody = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("EmailTemplateCollection")]
	public partial class EmailTemplateCollection : esEmailTemplateCollection, IEnumerable<EmailTemplate>
	{
		public EmailTemplate FindByPrimaryKey(System.Int16 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(EmailTemplate))]
		public class EmailTemplateCollectionWCFPacket : esCollectionWCFPacket<EmailTemplateCollection>
		{
			public static implicit operator EmailTemplateCollection(EmailTemplateCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator EmailTemplateCollectionWCFPacket(EmailTemplateCollection collection)
			{
				return new EmailTemplateCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class EmailTemplateQuery : esEmailTemplateQuery
	{
		public EmailTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "EmailTemplateQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(EmailTemplateQuery query)
		{
			return EmailTemplateQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator EmailTemplateQuery(string query)
		{
			return (EmailTemplateQuery)EmailTemplateQuery.SerializeHelper.FromXml(query, typeof(EmailTemplateQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esEmailTemplate : esEntity
	{
		public esEmailTemplate()
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
			EmailTemplateQuery query = new EmailTemplateQuery();
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
		/// Maps to DNNspot_Store_EmailTemplate.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? Id
		{
			get
			{
				return base.GetSystemInt16(EmailTemplateMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt16(EmailTemplateMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(EmailTemplateMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_EmailTemplate.NameKey
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String NameKey
		{
			get
			{
				return base.GetSystemString(EmailTemplateMetadata.ColumnNames.NameKey);
			}
			
			set
			{
				if(base.SetSystemString(EmailTemplateMetadata.ColumnNames.NameKey, value))
				{
					OnPropertyChanged(EmailTemplateMetadata.PropertyNames.NameKey);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_EmailTemplate.Description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(EmailTemplateMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(EmailTemplateMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(EmailTemplateMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_EmailTemplate.DefaultSubject
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DefaultSubject
		{
			get
			{
				return base.GetSystemString(EmailTemplateMetadata.ColumnNames.DefaultSubject);
			}
			
			set
			{
				if(base.SetSystemString(EmailTemplateMetadata.ColumnNames.DefaultSubject, value))
				{
					OnPropertyChanged(EmailTemplateMetadata.PropertyNames.DefaultSubject);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_EmailTemplate.DefaultBody
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DefaultBody
		{
			get
			{
				return base.GetSystemString(EmailTemplateMetadata.ColumnNames.DefaultBody);
			}
			
			set
			{
				if(base.SetSystemString(EmailTemplateMetadata.ColumnNames.DefaultBody, value))
				{
					OnPropertyChanged(EmailTemplateMetadata.PropertyNames.DefaultBody);
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
						case "NameKey": this.str().NameKey = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
						case "DefaultSubject": this.str().DefaultSubject = (string)value; break;							
						case "DefaultBody": this.str().DefaultBody = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int16)
								this.Id = (System.Int16?)value;
								OnPropertyChanged(EmailTemplateMetadata.PropertyNames.Id);
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
			public esStrings(esEmailTemplate entity)
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
				
			public System.String NameKey
			{
				get
				{
					System.String data = entity.NameKey;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NameKey = null;
					else entity.NameKey = Convert.ToString(value);
				}
			}
				
			public System.String Description
			{
				get
				{
					System.String data = entity.Description;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Description = null;
					else entity.Description = Convert.ToString(value);
				}
			}
				
			public System.String DefaultSubject
			{
				get
				{
					System.String data = entity.DefaultSubject;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DefaultSubject = null;
					else entity.DefaultSubject = Convert.ToString(value);
				}
			}
				
			public System.String DefaultBody
			{
				get
				{
					System.String data = entity.DefaultBody;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DefaultBody = null;
					else entity.DefaultBody = Convert.ToString(value);
				}
			}
			

			private esEmailTemplate entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return EmailTemplateMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public EmailTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmailTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(EmailTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(EmailTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private EmailTemplateQuery query;		
	}



	[Serializable]
	abstract public partial class esEmailTemplateCollection : esEntityCollection<EmailTemplate>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return EmailTemplateMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "EmailTemplateCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public EmailTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new EmailTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(EmailTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new EmailTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(EmailTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((EmailTemplateQuery)query);
		}

		#endregion
		
		private EmailTemplateQuery query;
	}



	[Serializable]
	abstract public partial class esEmailTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return EmailTemplateMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "NameKey": return this.NameKey;
				case "Description": return this.Description;
				case "DefaultSubject": return this.DefaultSubject;
				case "DefaultBody": return this.DefaultBody;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, EmailTemplateMetadata.ColumnNames.Id, esSystemType.Int16); }
		} 
		
		public esQueryItem NameKey
		{
			get { return new esQueryItem(this, EmailTemplateMetadata.ColumnNames.NameKey, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, EmailTemplateMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem DefaultSubject
		{
			get { return new esQueryItem(this, EmailTemplateMetadata.ColumnNames.DefaultSubject, esSystemType.String); }
		} 
		
		public esQueryItem DefaultBody
		{
			get { return new esQueryItem(this, EmailTemplateMetadata.ColumnNames.DefaultBody, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class EmailTemplate : esEmailTemplate
	{

		#region UpToStoreCollectionByStoreEmailTemplate - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_EmailTemplate
		/// </summary>

		[XmlIgnore]
		public StoreCollection UpToStoreCollectionByStoreEmailTemplate
		{
			get
			{
				if(this._UpToStoreCollectionByStoreEmailTemplate == null)
				{
					this._UpToStoreCollectionByStoreEmailTemplate = new StoreCollection();
					this._UpToStoreCollectionByStoreEmailTemplate.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToStoreCollectionByStoreEmailTemplate", this._UpToStoreCollectionByStoreEmailTemplate);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						StoreQuery m = new StoreQuery("m");
						StoreEmailTemplateQuery j = new StoreEmailTemplateQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.StoreId);
                        m.Where(j.EmailTemplateId == this.Id);

						this._UpToStoreCollectionByStoreEmailTemplate.Load(m);
					}
				}

				return this._UpToStoreCollectionByStoreEmailTemplate;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToStoreCollectionByStoreEmailTemplate != null) 
				{ 
					this.RemovePostSave("UpToStoreCollectionByStoreEmailTemplate"); 
					this._UpToStoreCollectionByStoreEmailTemplate = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_EmailTemplate
		/// </summary>
		public void AssociateStoreCollectionByStoreEmailTemplate(Store entity)
		{
			if (this._StoreEmailTemplateCollection == null)
			{
				this._StoreEmailTemplateCollection = new StoreEmailTemplateCollection();
				this._StoreEmailTemplateCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StoreEmailTemplateCollection", this._StoreEmailTemplateCollection);
			}

			StoreEmailTemplate obj = this._StoreEmailTemplateCollection.AddNew();
			obj.EmailTemplateId = this.Id;
			obj.StoreId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_EmailTemplate
		/// </summary>
		public void DissociateStoreCollectionByStoreEmailTemplate(Store entity)
		{
			if (this._StoreEmailTemplateCollection == null)
			{
				this._StoreEmailTemplateCollection = new StoreEmailTemplateCollection();
				this._StoreEmailTemplateCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("StoreEmailTemplateCollection", this._StoreEmailTemplateCollection);
			}

			StoreEmailTemplate obj = this._StoreEmailTemplateCollection.AddNew();
			obj.EmailTemplateId = this.Id;
            obj.StoreId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private StoreCollection _UpToStoreCollectionByStoreEmailTemplate;
		private StoreEmailTemplateCollection _StoreEmailTemplateCollection;
		#endregion

		#region StoreEmailTemplateCollectionByEmailTemplateId - Zero To Many
		
		static public esPrefetchMap Prefetch_StoreEmailTemplateCollectionByEmailTemplateId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.EmailTemplate.StoreEmailTemplateCollectionByEmailTemplateId_Delegate;
				map.PropertyName = "StoreEmailTemplateCollectionByEmailTemplateId";
				map.MyColumnName = "EmailTemplateId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void StoreEmailTemplateCollectionByEmailTemplateId_Delegate(esPrefetchParameters data)
		{
			EmailTemplateQuery parent = new EmailTemplateQuery(data.NextAlias());

			StoreEmailTemplateQuery me = data.You != null ? data.You as StoreEmailTemplateQuery : new StoreEmailTemplateQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.EmailTemplateId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_StoreEmailTemplate_DNNspot_Store_EmailTemplate
		/// </summary>

		[XmlIgnore]
		public StoreEmailTemplateCollection StoreEmailTemplateCollectionByEmailTemplateId
		{
			get
			{
				if(this._StoreEmailTemplateCollectionByEmailTemplateId == null)
				{
					this._StoreEmailTemplateCollectionByEmailTemplateId = new StoreEmailTemplateCollection();
					this._StoreEmailTemplateCollectionByEmailTemplateId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("StoreEmailTemplateCollectionByEmailTemplateId", this._StoreEmailTemplateCollectionByEmailTemplateId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._StoreEmailTemplateCollectionByEmailTemplateId.Query.Where(this._StoreEmailTemplateCollectionByEmailTemplateId.Query.EmailTemplateId == this.Id);
							this._StoreEmailTemplateCollectionByEmailTemplateId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._StoreEmailTemplateCollectionByEmailTemplateId.fks.Add(StoreEmailTemplateMetadata.ColumnNames.EmailTemplateId, this.Id);
					}
				}

				return this._StoreEmailTemplateCollectionByEmailTemplateId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._StoreEmailTemplateCollectionByEmailTemplateId != null) 
				{ 
					this.RemovePostSave("StoreEmailTemplateCollectionByEmailTemplateId"); 
					this._StoreEmailTemplateCollectionByEmailTemplateId = null;
					
				} 
			} 			
		}
			
		
		private StoreEmailTemplateCollection _StoreEmailTemplateCollectionByEmailTemplateId;
		#endregion

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "StoreEmailTemplateCollectionByEmailTemplateId":
					coll = this.StoreEmailTemplateCollectionByEmailTemplateId;
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
			
			props.Add(new esPropertyDescriptor(this, "StoreEmailTemplateCollectionByEmailTemplateId", typeof(StoreEmailTemplateCollection), new StoreEmailTemplate()));
		
			return props;
		}
		
	}
	



	[Serializable]
	public partial class EmailTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected EmailTemplateMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(EmailTemplateMetadata.ColumnNames.Id, 0, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = EmailTemplateMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(EmailTemplateMetadata.ColumnNames.NameKey, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = EmailTemplateMetadata.PropertyNames.NameKey;
			c.CharacterMaxLength = 75;
			m_columns.Add(c);
				
			c = new esColumnMetadata(EmailTemplateMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = EmailTemplateMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 1073741823;
			m_columns.Add(c);
				
			c = new esColumnMetadata(EmailTemplateMetadata.ColumnNames.DefaultSubject, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = EmailTemplateMetadata.PropertyNames.DefaultSubject;
			c.CharacterMaxLength = 300;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(EmailTemplateMetadata.ColumnNames.DefaultBody, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = EmailTemplateMetadata.PropertyNames.DefaultBody;
			c.CharacterMaxLength = 1073741823;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public EmailTemplateMetadata Meta()
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
			 public const string NameKey = "NameKey";
			 public const string Description = "Description";
			 public const string DefaultSubject = "DefaultSubject";
			 public const string DefaultBody = "DefaultBody";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string NameKey = "NameKey";
			 public const string Description = "Description";
			 public const string DefaultSubject = "DefaultSubject";
			 public const string DefaultBody = "DefaultBody";
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
			lock (typeof(EmailTemplateMetadata))
			{
				if(EmailTemplateMetadata.mapDelegates == null)
				{
					EmailTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (EmailTemplateMetadata.meta == null)
				{
					EmailTemplateMetadata.meta = new EmailTemplateMetadata();
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
				meta.AddTypeMap("NameKey", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DefaultSubject", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DefaultBody", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_EmailTemplate";
					meta.Destination = objectQualifier + "DNNspot_Store_EmailTemplate";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_EmailTemplateInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_EmailTemplateUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_EmailTemplateDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_EmailTemplateLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_EmailTemplateLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_EmailTemplate";
					meta.Destination = "DNNspot_Store_EmailTemplate";
									
					meta.spInsert = "proc_DNNspot_Store_EmailTemplateInsert";				
					meta.spUpdate = "proc_DNNspot_Store_EmailTemplateUpdate";		
					meta.spDelete = "proc_DNNspot_Store_EmailTemplateDelete";
					meta.spLoadAll = "proc_DNNspot_Store_EmailTemplateLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_EmailTemplateLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private EmailTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
