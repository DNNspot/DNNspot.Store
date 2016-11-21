
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
	/// Encapsulates the 'vDNNspot_Store_StoreEmailTemplate' view
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(vStoreEmailTemplate))]	
	[XmlType("vStoreEmailTemplate")]
	public partial class vStoreEmailTemplate : esvStoreEmailTemplate
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new vStoreEmailTemplate();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("vStoreEmailTemplateCollection")]
	public partial class vStoreEmailTemplateCollection : esvStoreEmailTemplateCollection, IEnumerable<vStoreEmailTemplate>
	{

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(vStoreEmailTemplate))]
		public class vStoreEmailTemplateCollectionWCFPacket : esCollectionWCFPacket<vStoreEmailTemplateCollection>
		{
			public static implicit operator vStoreEmailTemplateCollection(vStoreEmailTemplateCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator vStoreEmailTemplateCollectionWCFPacket(vStoreEmailTemplateCollection collection)
			{
				return new vStoreEmailTemplateCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class vStoreEmailTemplateQuery : esvStoreEmailTemplateQuery
	{
		public vStoreEmailTemplateQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "vStoreEmailTemplateQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(vStoreEmailTemplateQuery query)
		{
			return vStoreEmailTemplateQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator vStoreEmailTemplateQuery(string query)
		{
			return (vStoreEmailTemplateQuery)vStoreEmailTemplateQuery.SerializeHelper.FromXml(query, typeof(vStoreEmailTemplateQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esvStoreEmailTemplate : esEntity
	{
		public esvStoreEmailTemplate()
		{

		}
		
		#region LoadByPrimaryKey
		
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to vDNNspot_Store_StoreEmailTemplate.NameKey
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String NameKey
		{
			get
			{
				return base.GetSystemString(vStoreEmailTemplateMetadata.ColumnNames.NameKey);
			}
			
			set
			{
				if(base.SetSystemString(vStoreEmailTemplateMetadata.ColumnNames.NameKey, value))
				{
					OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.NameKey);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_StoreEmailTemplate.Description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(vStoreEmailTemplateMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(vStoreEmailTemplateMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_StoreEmailTemplate.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(vStoreEmailTemplateMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(vStoreEmailTemplateMetadata.ColumnNames.StoreId, value))
				{
					OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_StoreEmailTemplate.EmailTemplateId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? EmailTemplateId
		{
			get
			{
				return base.GetSystemInt16(vStoreEmailTemplateMetadata.ColumnNames.EmailTemplateId);
			}
			
			set
			{
				if(base.SetSystemInt16(vStoreEmailTemplateMetadata.ColumnNames.EmailTemplateId, value))
				{
					OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.EmailTemplateId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_StoreEmailTemplate.SubjectTemplate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SubjectTemplate
		{
			get
			{
				return base.GetSystemString(vStoreEmailTemplateMetadata.ColumnNames.SubjectTemplate);
			}
			
			set
			{
				if(base.SetSystemString(vStoreEmailTemplateMetadata.ColumnNames.SubjectTemplate, value))
				{
					OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.SubjectTemplate);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_StoreEmailTemplate.BodyTemplate
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String BodyTemplate
		{
			get
			{
				return base.GetSystemString(vStoreEmailTemplateMetadata.ColumnNames.BodyTemplate);
			}
			
			set
			{
				if(base.SetSystemString(vStoreEmailTemplateMetadata.ColumnNames.BodyTemplate, value))
				{
					OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.BodyTemplate);
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
						case "NameKey": this.str().NameKey = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
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
								OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.StoreId);
							break;
						
						case "EmailTemplateId":
						
							if (value == null || value is System.Int16)
								this.EmailTemplateId = (System.Int16?)value;
								OnPropertyChanged(vStoreEmailTemplateMetadata.PropertyNames.EmailTemplateId);
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
			public esStrings(esvStoreEmailTemplate entity)
			{
				this.entity = entity;
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
			

			private esvStoreEmailTemplate entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return vStoreEmailTemplateMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public vStoreEmailTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vStoreEmailTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vStoreEmailTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(vStoreEmailTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private vStoreEmailTemplateQuery query;		
	}



	[Serializable]
	abstract public partial class esvStoreEmailTemplateCollection : esEntityCollection<vStoreEmailTemplate>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vStoreEmailTemplateMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "vStoreEmailTemplateCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public vStoreEmailTemplateQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vStoreEmailTemplateQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vStoreEmailTemplateQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vStoreEmailTemplateQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(vStoreEmailTemplateQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((vStoreEmailTemplateQuery)query);
		}

		#endregion
		
		private vStoreEmailTemplateQuery query;
	}



	[Serializable]
	abstract public partial class esvStoreEmailTemplateQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return vStoreEmailTemplateMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "NameKey": return this.NameKey;
				case "Description": return this.Description;
				case "StoreId": return this.StoreId;
				case "EmailTemplateId": return this.EmailTemplateId;
				case "SubjectTemplate": return this.SubjectTemplate;
				case "BodyTemplate": return this.BodyTemplate;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem NameKey
		{
			get { return new esQueryItem(this, vStoreEmailTemplateMetadata.ColumnNames.NameKey, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, vStoreEmailTemplateMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, vStoreEmailTemplateMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem EmailTemplateId
		{
			get { return new esQueryItem(this, vStoreEmailTemplateMetadata.ColumnNames.EmailTemplateId, esSystemType.Int16); }
		} 
		
		public esQueryItem SubjectTemplate
		{
			get { return new esQueryItem(this, vStoreEmailTemplateMetadata.ColumnNames.SubjectTemplate, esSystemType.String); }
		} 
		
		public esQueryItem BodyTemplate
		{
			get { return new esQueryItem(this, vStoreEmailTemplateMetadata.ColumnNames.BodyTemplate, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class vStoreEmailTemplateMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vStoreEmailTemplateMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(vStoreEmailTemplateMetadata.ColumnNames.NameKey, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = vStoreEmailTemplateMetadata.PropertyNames.NameKey;
			c.CharacterMaxLength = 75;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vStoreEmailTemplateMetadata.ColumnNames.Description, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = vStoreEmailTemplateMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 1073741823;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vStoreEmailTemplateMetadata.ColumnNames.StoreId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vStoreEmailTemplateMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vStoreEmailTemplateMetadata.ColumnNames.EmailTemplateId, 3, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = vStoreEmailTemplateMetadata.PropertyNames.EmailTemplateId;
			c.NumericPrecision = 5;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vStoreEmailTemplateMetadata.ColumnNames.SubjectTemplate, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = vStoreEmailTemplateMetadata.PropertyNames.SubjectTemplate;
			c.CharacterMaxLength = 300;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vStoreEmailTemplateMetadata.ColumnNames.BodyTemplate, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = vStoreEmailTemplateMetadata.PropertyNames.BodyTemplate;
			c.CharacterMaxLength = 1073741823;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public vStoreEmailTemplateMetadata Meta()
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
			 public const string NameKey = "NameKey";
			 public const string Description = "Description";
			 public const string StoreId = "StoreId";
			 public const string EmailTemplateId = "EmailTemplateId";
			 public const string SubjectTemplate = "SubjectTemplate";
			 public const string BodyTemplate = "BodyTemplate";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string NameKey = "NameKey";
			 public const string Description = "Description";
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
			lock (typeof(vStoreEmailTemplateMetadata))
			{
				if(vStoreEmailTemplateMetadata.mapDelegates == null)
				{
					vStoreEmailTemplateMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vStoreEmailTemplateMetadata.meta == null)
				{
					vStoreEmailTemplateMetadata.meta = new vStoreEmailTemplateMetadata();
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


				meta.AddTypeMap("NameKey", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
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
					meta.Source = objectQualifier + "vDNNspot_Store_StoreEmailTemplate";
					meta.Destination = objectQualifier + "vDNNspot_Store_StoreEmailTemplate";
					
					meta.spInsert = objectQualifier + "proc_vDNNspot_Store_StoreEmailTemplateInsert";				
					meta.spUpdate = objectQualifier + "proc_vDNNspot_Store_StoreEmailTemplateUpdate";		
					meta.spDelete = objectQualifier + "proc_vDNNspot_Store_StoreEmailTemplateDelete";
					meta.spLoadAll = objectQualifier + "proc_vDNNspot_Store_StoreEmailTemplateLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_vDNNspot_Store_StoreEmailTemplateLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "vDNNspot_Store_StoreEmailTemplate";
					meta.Destination = "vDNNspot_Store_StoreEmailTemplate";
									
					meta.spInsert = "proc_vDNNspot_Store_StoreEmailTemplateInsert";				
					meta.spUpdate = "proc_vDNNspot_Store_StoreEmailTemplateUpdate";		
					meta.spDelete = "proc_vDNNspot_Store_StoreEmailTemplateDelete";
					meta.spLoadAll = "proc_vDNNspot_Store_StoreEmailTemplateLoadAll";
					meta.spLoadByPrimaryKey = "proc_vDNNspot_Store_StoreEmailTemplateLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vStoreEmailTemplateMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
