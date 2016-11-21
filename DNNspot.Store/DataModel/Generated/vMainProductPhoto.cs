
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
	/// Encapsulates the 'vDNNspot_Store_MainProductPhoto' view
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(vMainProductPhoto))]	
	[XmlType("vMainProductPhoto")]
	public partial class vMainProductPhoto : esvMainProductPhoto
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new vMainProductPhoto();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("vMainProductPhotoCollection")]
	public partial class vMainProductPhotoCollection : esvMainProductPhotoCollection, IEnumerable<vMainProductPhoto>
	{

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(vMainProductPhoto))]
		public class vMainProductPhotoCollectionWCFPacket : esCollectionWCFPacket<vMainProductPhotoCollection>
		{
			public static implicit operator vMainProductPhotoCollection(vMainProductPhotoCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator vMainProductPhotoCollectionWCFPacket(vMainProductPhotoCollection collection)
			{
				return new vMainProductPhotoCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class vMainProductPhotoQuery : esvMainProductPhotoQuery
	{
		public vMainProductPhotoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "vMainProductPhotoQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(vMainProductPhotoQuery query)
		{
			return vMainProductPhotoQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator vMainProductPhotoQuery(string query)
		{
			return (vMainProductPhotoQuery)vMainProductPhotoQuery.SerializeHelper.FromXml(query, typeof(vMainProductPhotoQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esvMainProductPhoto : esEntity
	{
		public esvMainProductPhoto()
		{

		}
		
		#region LoadByPrimaryKey
		
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to vDNNspot_Store_MainProductPhoto.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(vMainProductPhotoMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(vMainProductPhotoMetadata.ColumnNames.ProductId, value))
				{
					OnPropertyChanged(vMainProductPhotoMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_MainProductPhoto.MainPhotoFilename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String MainPhotoFilename
		{
			get
			{
				return base.GetSystemString(vMainProductPhotoMetadata.ColumnNames.MainPhotoFilename);
			}
			
			set
			{
				if(base.SetSystemString(vMainProductPhotoMetadata.ColumnNames.MainPhotoFilename, value))
				{
					OnPropertyChanged(vMainProductPhotoMetadata.PropertyNames.MainPhotoFilename);
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
						case "ProductId": this.str().ProductId = (string)value; break;							
						case "MainPhotoFilename": this.str().MainPhotoFilename = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(vMainProductPhotoMetadata.PropertyNames.ProductId);
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
			public esStrings(esvMainProductPhoto entity)
			{
				this.entity = entity;
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
				
			public System.String MainPhotoFilename
			{
				get
				{
					System.String data = entity.MainPhotoFilename;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.MainPhotoFilename = null;
					else entity.MainPhotoFilename = Convert.ToString(value);
				}
			}
			

			private esvMainProductPhoto entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return vMainProductPhotoMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public vMainProductPhotoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vMainProductPhotoQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vMainProductPhotoQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(vMainProductPhotoQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private vMainProductPhotoQuery query;		
	}



	[Serializable]
	abstract public partial class esvMainProductPhotoCollection : esEntityCollection<vMainProductPhoto>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vMainProductPhotoMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "vMainProductPhotoCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public vMainProductPhotoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vMainProductPhotoQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vMainProductPhotoQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vMainProductPhotoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(vMainProductPhotoQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((vMainProductPhotoQuery)query);
		}

		#endregion
		
		private vMainProductPhotoQuery query;
	}



	[Serializable]
	abstract public partial class esvMainProductPhotoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return vMainProductPhotoMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ProductId": return this.ProductId;
				case "MainPhotoFilename": return this.MainPhotoFilename;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, vMainProductPhotoMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem MainPhotoFilename
		{
			get { return new esQueryItem(this, vMainProductPhotoMetadata.ColumnNames.MainPhotoFilename, esSystemType.String); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class vMainProductPhotoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vMainProductPhotoMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(vMainProductPhotoMetadata.ColumnNames.ProductId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vMainProductPhotoMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vMainProductPhotoMetadata.ColumnNames.MainPhotoFilename, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = vMainProductPhotoMetadata.PropertyNames.MainPhotoFilename;
			c.CharacterMaxLength = 500;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public vMainProductPhotoMetadata Meta()
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
			 public const string ProductId = "ProductId";
			 public const string MainPhotoFilename = "MainPhotoFilename";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ProductId = "ProductId";
			 public const string MainPhotoFilename = "MainPhotoFilename";
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
			lock (typeof(vMainProductPhotoMetadata))
			{
				if(vMainProductPhotoMetadata.mapDelegates == null)
				{
					vMainProductPhotoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vMainProductPhotoMetadata.meta == null)
				{
					vMainProductPhotoMetadata.meta = new vMainProductPhotoMetadata();
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


				meta.AddTypeMap("ProductId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("MainPhotoFilename", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "vDNNspot_Store_MainProductPhoto";
					meta.Destination = objectQualifier + "vDNNspot_Store_MainProductPhoto";
					
					meta.spInsert = objectQualifier + "proc_vDNNspot_Store_MainProductPhotoInsert";				
					meta.spUpdate = objectQualifier + "proc_vDNNspot_Store_MainProductPhotoUpdate";		
					meta.spDelete = objectQualifier + "proc_vDNNspot_Store_MainProductPhotoDelete";
					meta.spLoadAll = objectQualifier + "proc_vDNNspot_Store_MainProductPhotoLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_vDNNspot_Store_MainProductPhotoLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "vDNNspot_Store_MainProductPhoto";
					meta.Destination = "vDNNspot_Store_MainProductPhoto";
									
					meta.spInsert = "proc_vDNNspot_Store_MainProductPhotoInsert";				
					meta.spUpdate = "proc_vDNNspot_Store_MainProductPhotoUpdate";		
					meta.spDelete = "proc_vDNNspot_Store_MainProductPhotoDelete";
					meta.spLoadAll = "proc_vDNNspot_Store_MainProductPhotoLoadAll";
					meta.spLoadByPrimaryKey = "proc_vDNNspot_Store_MainProductPhotoLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vMainProductPhotoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
