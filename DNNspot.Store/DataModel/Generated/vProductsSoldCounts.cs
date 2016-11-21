
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
	/// Encapsulates the 'vDNNspot_Store_ProductsSoldCounts' view
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(vProductsSoldCounts))]	
	[XmlType("vProductsSoldCounts")]
	public partial class vProductsSoldCounts : esvProductsSoldCounts
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new vProductsSoldCounts();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("vProductsSoldCountsCollection")]
	public partial class vProductsSoldCountsCollection : esvProductsSoldCountsCollection, IEnumerable<vProductsSoldCounts>
	{

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(vProductsSoldCounts))]
		public class vProductsSoldCountsCollectionWCFPacket : esCollectionWCFPacket<vProductsSoldCountsCollection>
		{
			public static implicit operator vProductsSoldCountsCollection(vProductsSoldCountsCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator vProductsSoldCountsCollectionWCFPacket(vProductsSoldCountsCollection collection)
			{
				return new vProductsSoldCountsCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class vProductsSoldCountsQuery : esvProductsSoldCountsQuery
	{
		public vProductsSoldCountsQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "vProductsSoldCountsQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(vProductsSoldCountsQuery query)
		{
			return vProductsSoldCountsQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator vProductsSoldCountsQuery(string query)
		{
			return (vProductsSoldCountsQuery)vProductsSoldCountsQuery.SerializeHelper.FromXml(query, typeof(vProductsSoldCountsQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esvProductsSoldCounts : esEntity
	{
		public esvProductsSoldCounts()
		{

		}
		
		#region LoadByPrimaryKey
		
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ProductsSoldCounts.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(vProductsSoldCountsMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(vProductsSoldCountsMetadata.ColumnNames.ProductId, value))
				{
					OnPropertyChanged(vProductsSoldCountsMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_ProductsSoldCounts.NumSold
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? NumSold
		{
			get
			{
				return base.GetSystemInt32(vProductsSoldCountsMetadata.ColumnNames.NumSold);
			}
			
			set
			{
				if(base.SetSystemInt32(vProductsSoldCountsMetadata.ColumnNames.NumSold, value))
				{
					OnPropertyChanged(vProductsSoldCountsMetadata.PropertyNames.NumSold);
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
						case "NumSold": this.str().NumSold = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(vProductsSoldCountsMetadata.PropertyNames.ProductId);
							break;
						
						case "NumSold":
						
							if (value == null || value is System.Int32)
								this.NumSold = (System.Int32?)value;
								OnPropertyChanged(vProductsSoldCountsMetadata.PropertyNames.NumSold);
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
			public esStrings(esvProductsSoldCounts entity)
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
				
			public System.String NumSold
			{
				get
				{
					System.Int32? data = entity.NumSold;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NumSold = null;
					else entity.NumSold = Convert.ToInt32(value);
				}
			}
			

			private esvProductsSoldCounts entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return vProductsSoldCountsMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public vProductsSoldCountsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vProductsSoldCountsQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vProductsSoldCountsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(vProductsSoldCountsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private vProductsSoldCountsQuery query;		
	}



	[Serializable]
	abstract public partial class esvProductsSoldCountsCollection : esEntityCollection<vProductsSoldCounts>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vProductsSoldCountsMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "vProductsSoldCountsCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public vProductsSoldCountsQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vProductsSoldCountsQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vProductsSoldCountsQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vProductsSoldCountsQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(vProductsSoldCountsQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((vProductsSoldCountsQuery)query);
		}

		#endregion
		
		private vProductsSoldCountsQuery query;
	}



	[Serializable]
	abstract public partial class esvProductsSoldCountsQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return vProductsSoldCountsMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "ProductId": return this.ProductId;
				case "NumSold": return this.NumSold;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, vProductsSoldCountsMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem NumSold
		{
			get { return new esQueryItem(this, vProductsSoldCountsMetadata.ColumnNames.NumSold, esSystemType.Int32); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class vProductsSoldCountsMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vProductsSoldCountsMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(vProductsSoldCountsMetadata.ColumnNames.ProductId, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vProductsSoldCountsMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vProductsSoldCountsMetadata.ColumnNames.NumSold, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vProductsSoldCountsMetadata.PropertyNames.NumSold;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public vProductsSoldCountsMetadata Meta()
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
			 public const string NumSold = "NumSold";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string ProductId = "ProductId";
			 public const string NumSold = "NumSold";
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
			lock (typeof(vProductsSoldCountsMetadata))
			{
				if(vProductsSoldCountsMetadata.mapDelegates == null)
				{
					vProductsSoldCountsMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vProductsSoldCountsMetadata.meta == null)
				{
					vProductsSoldCountsMetadata.meta = new vProductsSoldCountsMetadata();
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
				meta.AddTypeMap("NumSold", new esTypeMap("int", "System.Int32"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "vDNNspot_Store_ProductsSoldCounts";
					meta.Destination = objectQualifier + "vDNNspot_Store_ProductsSoldCounts";
					
					meta.spInsert = objectQualifier + "proc_vDNNspot_Store_ProductsSoldCountsInsert";				
					meta.spUpdate = objectQualifier + "proc_vDNNspot_Store_ProductsSoldCountsUpdate";		
					meta.spDelete = objectQualifier + "proc_vDNNspot_Store_ProductsSoldCountsDelete";
					meta.spLoadAll = objectQualifier + "proc_vDNNspot_Store_ProductsSoldCountsLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_vDNNspot_Store_ProductsSoldCountsLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "vDNNspot_Store_ProductsSoldCounts";
					meta.Destination = "vDNNspot_Store_ProductsSoldCounts";
									
					meta.spInsert = "proc_vDNNspot_Store_ProductsSoldCountsInsert";				
					meta.spUpdate = "proc_vDNNspot_Store_ProductsSoldCountsUpdate";		
					meta.spDelete = "proc_vDNNspot_Store_ProductsSoldCountsDelete";
					meta.spLoadAll = "proc_vDNNspot_Store_ProductsSoldCountsLoadAll";
					meta.spLoadByPrimaryKey = "proc_vDNNspot_Store_ProductsSoldCountsLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vProductsSoldCountsMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
