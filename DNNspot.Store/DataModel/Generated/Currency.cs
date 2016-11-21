
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
	/// Encapsulates the 'DNNspot_Store_Currency' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Currency))]	
	[XmlType("Currency")]
	[Table(Name="Currency")]
	public partial class Currency : esCurrency
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Currency();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.String code)
		{
			var obj = new Currency();
			obj.Code = code;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.String code, esSqlAccessType sqlAccessType)
		{
			var obj = new Currency();
			obj.Code = code;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save(sqlAccessType);
		}
		#endregion

		
					
		

		#region LINQtoSQL overrides (shame but we must do this)

			
		[Column(IsPrimaryKey = true, CanBeNull = false)]
		public override System.String Code
		{
			get { return base.Code;  }
			set { base.Code = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.String CultureName
		{
			get { return base.CultureName;  }
			set { base.CultureName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Description
		{
			get { return base.Description;  }
			set { base.Description = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Symbol
		{
			get { return base.Symbol;  }
			set { base.Symbol = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SymbolPosition
		{
			get { return base.SymbolPosition;  }
			set { base.SymbolPosition = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String GroupSeparator
		{
			get { return base.GroupSeparator;  }
			set { base.GroupSeparator = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DecimalSeparator
		{
			get { return base.DecimalSeparator;  }
			set { base.DecimalSeparator = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("CurrencyCollection")]
	public partial class CurrencyCollection : esCurrencyCollection, IEnumerable<Currency>
	{
		public Currency FindByPrimaryKey(System.String code)
		{
			return this.SingleOrDefault(e => e.Code == code);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Currency))]
		public class CurrencyCollectionWCFPacket : esCollectionWCFPacket<CurrencyCollection>
		{
			public static implicit operator CurrencyCollection(CurrencyCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CurrencyCollectionWCFPacket(CurrencyCollection collection)
			{
				return new CurrencyCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CurrencyQuery : esCurrencyQuery
	{
		public CurrencyQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CurrencyQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CurrencyQuery query)
		{
			return CurrencyQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CurrencyQuery(string query)
		{
			return (CurrencyQuery)CurrencyQuery.SerializeHelper.FromXml(query, typeof(CurrencyQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCurrency : esEntity
	{
		public esCurrency()
		{

		}
		
		#region LoadByPrimaryKey
		public virtual bool LoadByPrimaryKey(System.String code)
		{
			if(this.es.Connection.SqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(code);
			else
				return LoadByPrimaryKeyStoredProcedure(code);
		}

		public virtual bool LoadByPrimaryKey(esSqlAccessType sqlAccessType, System.String code)
		{
			if (sqlAccessType == esSqlAccessType.DynamicSQL)
				return LoadByPrimaryKeyDynamic(code);
			else
				return LoadByPrimaryKeyStoredProcedure(code);
		}

		private bool LoadByPrimaryKeyDynamic(System.String code)
		{
			CurrencyQuery query = new CurrencyQuery();
			query.Where(query.Code == code);
			return this.Load(query);
		}

		private bool LoadByPrimaryKeyStoredProcedure(System.String code)
		{
			esParameters parms = new esParameters();
			parms.Add("Code", code);
			return this.Load(esQueryType.StoredProcedure, this.es.spLoadByPrimaryKey, parms);
		}
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to DNNspot_Store_Currency.Code
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Code
		{
			get
			{
				return base.GetSystemString(CurrencyMetadata.ColumnNames.Code);
			}
			
			set
			{
				if(base.SetSystemString(CurrencyMetadata.ColumnNames.Code, value))
				{
					OnPropertyChanged(CurrencyMetadata.PropertyNames.Code);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Currency.CultureName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String CultureName
		{
			get
			{
				return base.GetSystemString(CurrencyMetadata.ColumnNames.CultureName);
			}
			
			set
			{
				if(base.SetSystemString(CurrencyMetadata.ColumnNames.CultureName, value))
				{
					OnPropertyChanged(CurrencyMetadata.PropertyNames.CultureName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Currency.Description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(CurrencyMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(CurrencyMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(CurrencyMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Currency.Symbol
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Symbol
		{
			get
			{
				return base.GetSystemString(CurrencyMetadata.ColumnNames.Symbol);
			}
			
			set
			{
				if(base.SetSystemString(CurrencyMetadata.ColumnNames.Symbol, value))
				{
					OnPropertyChanged(CurrencyMetadata.PropertyNames.Symbol);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Currency.SymbolPosition
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SymbolPosition
		{
			get
			{
				return base.GetSystemString(CurrencyMetadata.ColumnNames.SymbolPosition);
			}
			
			set
			{
				if(base.SetSystemString(CurrencyMetadata.ColumnNames.SymbolPosition, value))
				{
					OnPropertyChanged(CurrencyMetadata.PropertyNames.SymbolPosition);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Currency.GroupSeparator
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String GroupSeparator
		{
			get
			{
				return base.GetSystemString(CurrencyMetadata.ColumnNames.GroupSeparator);
			}
			
			set
			{
				if(base.SetSystemString(CurrencyMetadata.ColumnNames.GroupSeparator, value))
				{
					OnPropertyChanged(CurrencyMetadata.PropertyNames.GroupSeparator);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Currency.DecimalSeparator
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DecimalSeparator
		{
			get
			{
				return base.GetSystemString(CurrencyMetadata.ColumnNames.DecimalSeparator);
			}
			
			set
			{
				if(base.SetSystemString(CurrencyMetadata.ColumnNames.DecimalSeparator, value))
				{
					OnPropertyChanged(CurrencyMetadata.PropertyNames.DecimalSeparator);
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
						case "Code": this.str().Code = (string)value; break;							
						case "CultureName": this.str().CultureName = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
						case "Symbol": this.str().Symbol = (string)value; break;							
						case "SymbolPosition": this.str().SymbolPosition = (string)value; break;							
						case "GroupSeparator": this.str().GroupSeparator = (string)value; break;							
						case "DecimalSeparator": this.str().DecimalSeparator = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{

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
			public esStrings(esCurrency entity)
			{
				this.entity = entity;
			}
			
	
			public System.String Code
			{
				get
				{
					System.String data = entity.Code;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Code = null;
					else entity.Code = Convert.ToString(value);
				}
			}
				
			public System.String CultureName
			{
				get
				{
					System.String data = entity.CultureName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.CultureName = null;
					else entity.CultureName = Convert.ToString(value);
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
				
			public System.String Symbol
			{
				get
				{
					System.String data = entity.Symbol;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Symbol = null;
					else entity.Symbol = Convert.ToString(value);
				}
			}
				
			public System.String SymbolPosition
			{
				get
				{
					System.String data = entity.SymbolPosition;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SymbolPosition = null;
					else entity.SymbolPosition = Convert.ToString(value);
				}
			}
				
			public System.String GroupSeparator
			{
				get
				{
					System.String data = entity.GroupSeparator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.GroupSeparator = null;
					else entity.GroupSeparator = Convert.ToString(value);
				}
			}
				
			public System.String DecimalSeparator
			{
				get
				{
					System.String data = entity.DecimalSeparator;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DecimalSeparator = null;
					else entity.DecimalSeparator = Convert.ToString(value);
				}
			}
			

			private esCurrency entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CurrencyMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CurrencyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CurrencyQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CurrencyQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CurrencyQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CurrencyQuery query;		
	}



	[Serializable]
	abstract public partial class esCurrencyCollection : esEntityCollection<Currency>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CurrencyMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CurrencyCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CurrencyQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CurrencyQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CurrencyQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CurrencyQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CurrencyQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CurrencyQuery)query);
		}

		#endregion
		
		private CurrencyQuery query;
	}



	[Serializable]
	abstract public partial class esCurrencyQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CurrencyMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Code": return this.Code;
				case "CultureName": return this.CultureName;
				case "Description": return this.Description;
				case "Symbol": return this.Symbol;
				case "SymbolPosition": return this.SymbolPosition;
				case "GroupSeparator": return this.GroupSeparator;
				case "DecimalSeparator": return this.DecimalSeparator;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Code
		{
			get { return new esQueryItem(this, CurrencyMetadata.ColumnNames.Code, esSystemType.String); }
		} 
		
		public esQueryItem CultureName
		{
			get { return new esQueryItem(this, CurrencyMetadata.ColumnNames.CultureName, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, CurrencyMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem Symbol
		{
			get { return new esQueryItem(this, CurrencyMetadata.ColumnNames.Symbol, esSystemType.String); }
		} 
		
		public esQueryItem SymbolPosition
		{
			get { return new esQueryItem(this, CurrencyMetadata.ColumnNames.SymbolPosition, esSystemType.String); }
		} 
		
		public esQueryItem GroupSeparator
		{
			get { return new esQueryItem(this, CurrencyMetadata.ColumnNames.GroupSeparator, esSystemType.String); }
		} 
		
		public esQueryItem DecimalSeparator
		{
			get { return new esQueryItem(this, CurrencyMetadata.ColumnNames.DecimalSeparator, esSystemType.String); }
		} 
		
		#endregion
		
	}


	
	public partial class Currency : esCurrency
	{

		
		
	}
	



	[Serializable]
	public partial class CurrencyMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CurrencyMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CurrencyMetadata.ColumnNames.Code, 0, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyMetadata.PropertyNames.Code;
			c.IsInPrimaryKey = true;
			c.CharacterMaxLength = 3;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyMetadata.ColumnNames.CultureName, 1, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyMetadata.PropertyNames.CultureName;
			c.CharacterMaxLength = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyMetadata.ColumnNames.Description, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 500;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyMetadata.ColumnNames.Symbol, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyMetadata.PropertyNames.Symbol;
			c.CharacterMaxLength = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyMetadata.ColumnNames.SymbolPosition, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyMetadata.PropertyNames.SymbolPosition;
			c.CharacterMaxLength = 20;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyMetadata.ColumnNames.GroupSeparator, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyMetadata.PropertyNames.GroupSeparator;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CurrencyMetadata.ColumnNames.DecimalSeparator, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CurrencyMetadata.PropertyNames.DecimalSeparator;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CurrencyMetadata Meta()
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
			 public const string Code = "Code";
			 public const string CultureName = "CultureName";
			 public const string Description = "Description";
			 public const string Symbol = "Symbol";
			 public const string SymbolPosition = "SymbolPosition";
			 public const string GroupSeparator = "GroupSeparator";
			 public const string DecimalSeparator = "DecimalSeparator";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Code = "Code";
			 public const string CultureName = "CultureName";
			 public const string Description = "Description";
			 public const string Symbol = "Symbol";
			 public const string SymbolPosition = "SymbolPosition";
			 public const string GroupSeparator = "GroupSeparator";
			 public const string DecimalSeparator = "DecimalSeparator";
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
			lock (typeof(CurrencyMetadata))
			{
				if(CurrencyMetadata.mapDelegates == null)
				{
					CurrencyMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CurrencyMetadata.meta == null)
				{
					CurrencyMetadata.meta = new CurrencyMetadata();
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


				meta.AddTypeMap("Code", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("CultureName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Symbol", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SymbolPosition", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("GroupSeparator", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("DecimalSeparator", new esTypeMap("nvarchar", "System.String"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Currency";
					meta.Destination = objectQualifier + "DNNspot_Store_Currency";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_CurrencyInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_CurrencyUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_CurrencyDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_CurrencyLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_CurrencyLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Currency";
					meta.Destination = "DNNspot_Store_Currency";
									
					meta.spInsert = "proc_DNNspot_Store_CurrencyInsert";				
					meta.spUpdate = "proc_DNNspot_Store_CurrencyUpdate";		
					meta.spDelete = "proc_DNNspot_Store_CurrencyDelete";
					meta.spLoadAll = "proc_DNNspot_Store_CurrencyLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_CurrencyLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CurrencyMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
