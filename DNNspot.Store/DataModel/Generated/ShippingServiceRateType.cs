
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
	/// Encapsulates the 'DNNspot_Store_ShippingServiceRateType' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(ShippingServiceRateType))]	
	[XmlType("ShippingServiceRateType")]
	[Table(Name="ShippingServiceRateType")]
	public partial class ShippingServiceRateType : esShippingServiceRateType
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new ShippingServiceRateType();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new ShippingServiceRateType();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new ShippingServiceRateType();
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
		public override System.Int32? ShippingServiceId
		{
			get { return base.ShippingServiceId;  }
			set { base.ShippingServiceId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String DisplayName
		{
			get { return base.DisplayName;  }
			set { base.DisplayName = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsEnabled
		{
			get { return base.IsEnabled;  }
			set { base.IsEnabled = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? SortOrder
		{
			get { return base.SortOrder;  }
			set { base.SortOrder = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("ShippingServiceRateTypeCollection")]
	public partial class ShippingServiceRateTypeCollection : esShippingServiceRateTypeCollection, IEnumerable<ShippingServiceRateType>
	{
		public ShippingServiceRateType FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(ShippingServiceRateType))]
		public class ShippingServiceRateTypeCollectionWCFPacket : esCollectionWCFPacket<ShippingServiceRateTypeCollection>
		{
			public static implicit operator ShippingServiceRateTypeCollection(ShippingServiceRateTypeCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator ShippingServiceRateTypeCollectionWCFPacket(ShippingServiceRateTypeCollection collection)
			{
				return new ShippingServiceRateTypeCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class ShippingServiceRateTypeQuery : esShippingServiceRateTypeQuery
	{
		public ShippingServiceRateTypeQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "ShippingServiceRateTypeQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(ShippingServiceRateTypeQuery query)
		{
			return ShippingServiceRateTypeQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator ShippingServiceRateTypeQuery(string query)
		{
			return (ShippingServiceRateTypeQuery)ShippingServiceRateTypeQuery.SerializeHelper.FromXml(query, typeof(ShippingServiceRateTypeQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esShippingServiceRateType : esEntity
	{
		public esShippingServiceRateType()
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
			ShippingServiceRateTypeQuery query = new ShippingServiceRateTypeQuery();
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
		/// Maps to DNNspot_Store_ShippingServiceRateType.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(ShippingServiceRateTypeMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingServiceRateTypeMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRateType.ShippingServiceId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ShippingServiceId
		{
			get
			{
				return base.GetSystemInt32(ShippingServiceRateTypeMetadata.ColumnNames.ShippingServiceId);
			}
			
			set
			{
				if(base.SetSystemInt32(ShippingServiceRateTypeMetadata.ColumnNames.ShippingServiceId, value))
				{
					this._UpToShippingServiceByShippingServiceId = null;
					this.OnPropertyChanged("UpToShippingServiceByShippingServiceId");
					OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.ShippingServiceId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRateType.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(ShippingServiceRateTypeMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(ShippingServiceRateTypeMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRateType.DisplayName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String DisplayName
		{
			get
			{
				return base.GetSystemString(ShippingServiceRateTypeMetadata.ColumnNames.DisplayName);
			}
			
			set
			{
				if(base.SetSystemString(ShippingServiceRateTypeMetadata.ColumnNames.DisplayName, value))
				{
					OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.DisplayName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRateType.IsEnabled
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsEnabled
		{
			get
			{
				return base.GetSystemBoolean(ShippingServiceRateTypeMetadata.ColumnNames.IsEnabled);
			}
			
			set
			{
				if(base.SetSystemBoolean(ShippingServiceRateTypeMetadata.ColumnNames.IsEnabled, value))
				{
					OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.IsEnabled);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_ShippingServiceRateType.SortOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? SortOrder
		{
			get
			{
				return base.GetSystemInt16(ShippingServiceRateTypeMetadata.ColumnNames.SortOrder);
			}
			
			set
			{
				if(base.SetSystemInt16(ShippingServiceRateTypeMetadata.ColumnNames.SortOrder, value))
				{
					OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.SortOrder);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected ShippingService _UpToShippingServiceByShippingServiceId;
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
						case "ShippingServiceId": this.str().ShippingServiceId = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "DisplayName": this.str().DisplayName = (string)value; break;							
						case "IsEnabled": this.str().IsEnabled = (string)value; break;							
						case "SortOrder": this.str().SortOrder = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.Id);
							break;
						
						case "ShippingServiceId":
						
							if (value == null || value is System.Int32)
								this.ShippingServiceId = (System.Int32?)value;
								OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.ShippingServiceId);
							break;
						
						case "IsEnabled":
						
							if (value == null || value is System.Boolean)
								this.IsEnabled = (System.Boolean?)value;
								OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.IsEnabled);
							break;
						
						case "SortOrder":
						
							if (value == null || value is System.Int16)
								this.SortOrder = (System.Int16?)value;
								OnPropertyChanged(ShippingServiceRateTypeMetadata.PropertyNames.SortOrder);
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
			public esStrings(esShippingServiceRateType entity)
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
				
			public System.String ShippingServiceId
			{
				get
				{
					System.Int32? data = entity.ShippingServiceId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ShippingServiceId = null;
					else entity.ShippingServiceId = Convert.ToInt32(value);
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
				
			public System.String DisplayName
			{
				get
				{
					System.String data = entity.DisplayName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.DisplayName = null;
					else entity.DisplayName = Convert.ToString(value);
				}
			}
				
			public System.String IsEnabled
			{
				get
				{
					System.Boolean? data = entity.IsEnabled;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsEnabled = null;
					else entity.IsEnabled = Convert.ToBoolean(value);
				}
			}
				
			public System.String SortOrder
			{
				get
				{
					System.Int16? data = entity.SortOrder;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SortOrder = null;
					else entity.SortOrder = Convert.ToInt16(value);
				}
			}
			

			private esShippingServiceRateType entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceRateTypeMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public ShippingServiceRateTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceRateTypeQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceRateTypeQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(ShippingServiceRateTypeQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private ShippingServiceRateTypeQuery query;		
	}



	[Serializable]
	abstract public partial class esShippingServiceRateTypeCollection : esEntityCollection<ShippingServiceRateType>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceRateTypeMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "ShippingServiceRateTypeCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public ShippingServiceRateTypeQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new ShippingServiceRateTypeQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(ShippingServiceRateTypeQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new ShippingServiceRateTypeQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(ShippingServiceRateTypeQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((ShippingServiceRateTypeQuery)query);
		}

		#endregion
		
		private ShippingServiceRateTypeQuery query;
	}



	[Serializable]
	abstract public partial class esShippingServiceRateTypeQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return ShippingServiceRateTypeMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "ShippingServiceId": return this.ShippingServiceId;
				case "Name": return this.Name;
				case "DisplayName": return this.DisplayName;
				case "IsEnabled": return this.IsEnabled;
				case "SortOrder": return this.SortOrder;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, ShippingServiceRateTypeMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem ShippingServiceId
		{
			get { return new esQueryItem(this, ShippingServiceRateTypeMetadata.ColumnNames.ShippingServiceId, esSystemType.Int32); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, ShippingServiceRateTypeMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem DisplayName
		{
			get { return new esQueryItem(this, ShippingServiceRateTypeMetadata.ColumnNames.DisplayName, esSystemType.String); }
		} 
		
		public esQueryItem IsEnabled
		{
			get { return new esQueryItem(this, ShippingServiceRateTypeMetadata.ColumnNames.IsEnabled, esSystemType.Boolean); }
		} 
		
		public esQueryItem SortOrder
		{
			get { return new esQueryItem(this, ShippingServiceRateTypeMetadata.ColumnNames.SortOrder, esSystemType.Int16); }
		} 
		
		#endregion
		
	}


	
	public partial class ShippingServiceRateType : esShippingServiceRateType
	{

		#region ShippingServiceRateCollectionByRateTypeId - Zero To Many
		
		static public esPrefetchMap Prefetch_ShippingServiceRateCollectionByRateTypeId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.ShippingServiceRateType.ShippingServiceRateCollectionByRateTypeId_Delegate;
				map.PropertyName = "ShippingServiceRateCollectionByRateTypeId";
				map.MyColumnName = "RateTypeId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ShippingServiceRateCollectionByRateTypeId_Delegate(esPrefetchParameters data)
		{
			ShippingServiceRateTypeQuery parent = new ShippingServiceRateTypeQuery(data.NextAlias());

			ShippingServiceRateQuery me = data.You != null ? data.You as ShippingServiceRateQuery : new ShippingServiceRateQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.RateTypeId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ShippingServiceRate_DNNspot_Store_ShippingServiceRateType
		/// </summary>

		[XmlIgnore]
		public ShippingServiceRateCollection ShippingServiceRateCollectionByRateTypeId
		{
			get
			{
				if(this._ShippingServiceRateCollectionByRateTypeId == null)
				{
					this._ShippingServiceRateCollectionByRateTypeId = new ShippingServiceRateCollection();
					this._ShippingServiceRateCollectionByRateTypeId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ShippingServiceRateCollectionByRateTypeId", this._ShippingServiceRateCollectionByRateTypeId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ShippingServiceRateCollectionByRateTypeId.Query.Where(this._ShippingServiceRateCollectionByRateTypeId.Query.RateTypeId == this.Id);
							this._ShippingServiceRateCollectionByRateTypeId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ShippingServiceRateCollectionByRateTypeId.fks.Add(ShippingServiceRateMetadata.ColumnNames.RateTypeId, this.Id);
					}
				}

				return this._ShippingServiceRateCollectionByRateTypeId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ShippingServiceRateCollectionByRateTypeId != null) 
				{ 
					this.RemovePostSave("ShippingServiceRateCollectionByRateTypeId"); 
					this._ShippingServiceRateCollectionByRateTypeId = null;
					
				} 
			} 			
		}
			
		
		private ShippingServiceRateCollection _ShippingServiceRateCollectionByRateTypeId;
		#endregion

				
		#region UpToShippingServiceByShippingServiceId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_ShippingServiceRateType_DNNspot_Store_ShippingService
		/// </summary>

		[XmlIgnore]
					
		public ShippingService UpToShippingServiceByShippingServiceId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToShippingServiceByShippingServiceId == null && ShippingServiceId != null)
				{
					this._UpToShippingServiceByShippingServiceId = new ShippingService();
					this._UpToShippingServiceByShippingServiceId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToShippingServiceByShippingServiceId", this._UpToShippingServiceByShippingServiceId);
					this._UpToShippingServiceByShippingServiceId.Query.Where(this._UpToShippingServiceByShippingServiceId.Query.Id == this.ShippingServiceId);
					this._UpToShippingServiceByShippingServiceId.Query.Load();
				}	
				return this._UpToShippingServiceByShippingServiceId;
			}
			
			set
			{
				this.RemovePreSave("UpToShippingServiceByShippingServiceId");
				

				if(value == null)
				{
					this.ShippingServiceId = null;
					this._UpToShippingServiceByShippingServiceId = null;
				}
				else
				{
					this.ShippingServiceId = value.Id;
					this._UpToShippingServiceByShippingServiceId = value;
					this.SetPreSave("UpToShippingServiceByShippingServiceId", this._UpToShippingServiceByShippingServiceId);
				}
				
			}
		}
		#endregion
		

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "ShippingServiceRateCollectionByRateTypeId":
					coll = this.ShippingServiceRateCollectionByRateTypeId;
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
			
			props.Add(new esPropertyDescriptor(this, "ShippingServiceRateCollectionByRateTypeId", typeof(ShippingServiceRateCollection), new ShippingServiceRate()));
		
			return props;
		}
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
			if(!this.es.IsDeleted && this._UpToShippingServiceByShippingServiceId != null)
			{
				this.ShippingServiceId = this._UpToShippingServiceByShippingServiceId.Id;
			}
		}
		
		/// <summary>
		/// Called by ApplyPostSaveKeys 
		/// </summary>
		/// <param name="coll">The collection to enumerate over</param>
		/// <param name="key">"The column name</param>
		/// <param name="value">The column value</param>
		private void Apply(esEntityCollectionBase coll, string key, object value)
		{
			foreach (esEntity obj in coll)
			{
				if (obj.es.IsAdded)
				{
					obj.SetProperty(key, value);
				}
			}
		}
		
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PostSave.
		/// </summary>
		protected override void ApplyPostSaveKeys()
		{
			if(this._ShippingServiceRateCollectionByRateTypeId != null)
			{
				Apply(this._ShippingServiceRateCollectionByRateTypeId, "RateTypeId", this.Id);
			}
		}
		
	}
	



	[Serializable]
	public partial class ShippingServiceRateTypeMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected ShippingServiceRateTypeMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(ShippingServiceRateTypeMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingServiceRateTypeMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateTypeMetadata.ColumnNames.ShippingServiceId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = ShippingServiceRateTypeMetadata.PropertyNames.ShippingServiceId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateTypeMetadata.ColumnNames.Name, 2, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingServiceRateTypeMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 100;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateTypeMetadata.ColumnNames.DisplayName, 3, typeof(System.String), esSystemType.String);
			c.PropertyName = ShippingServiceRateTypeMetadata.PropertyNames.DisplayName;
			c.CharacterMaxLength = 100;
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateTypeMetadata.ColumnNames.IsEnabled, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = ShippingServiceRateTypeMetadata.PropertyNames.IsEnabled;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(ShippingServiceRateTypeMetadata.ColumnNames.SortOrder, 5, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = ShippingServiceRateTypeMetadata.PropertyNames.SortOrder;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((99))";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public ShippingServiceRateTypeMetadata Meta()
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
			 public const string ShippingServiceId = "ShippingServiceId";
			 public const string Name = "Name";
			 public const string DisplayName = "DisplayName";
			 public const string IsEnabled = "IsEnabled";
			 public const string SortOrder = "SortOrder";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string ShippingServiceId = "ShippingServiceId";
			 public const string Name = "Name";
			 public const string DisplayName = "DisplayName";
			 public const string IsEnabled = "IsEnabled";
			 public const string SortOrder = "SortOrder";
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
			lock (typeof(ShippingServiceRateTypeMetadata))
			{
				if(ShippingServiceRateTypeMetadata.mapDelegates == null)
				{
					ShippingServiceRateTypeMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (ShippingServiceRateTypeMetadata.meta == null)
				{
					ShippingServiceRateTypeMetadata.meta = new ShippingServiceRateTypeMetadata();
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
				meta.AddTypeMap("ShippingServiceId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Name", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("DisplayName", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("IsEnabled", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("SortOrder", new esTypeMap("smallint", "System.Int16"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_ShippingServiceRateType";
					meta.Destination = objectQualifier + "DNNspot_Store_ShippingServiceRateType";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateTypeInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateTypeUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateTypeDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateTypeLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_ShippingServiceRateTypeLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_ShippingServiceRateType";
					meta.Destination = "DNNspot_Store_ShippingServiceRateType";
									
					meta.spInsert = "proc_DNNspot_Store_ShippingServiceRateTypeInsert";				
					meta.spUpdate = "proc_DNNspot_Store_ShippingServiceRateTypeUpdate";		
					meta.spDelete = "proc_DNNspot_Store_ShippingServiceRateTypeDelete";
					meta.spLoadAll = "proc_DNNspot_Store_ShippingServiceRateTypeLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_ShippingServiceRateTypeLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private ShippingServiceRateTypeMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
