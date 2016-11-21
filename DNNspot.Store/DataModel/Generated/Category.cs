
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
	/// Encapsulates the 'DNNspot_Store_Category' table
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(Category))]	
	[XmlType("Category")]
	[Table(Name="Category")]
	public partial class Category : esCategory
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new Category();
		}
		
		#region Static Quick Access Methods
		static public void Delete(System.Int32 id)
		{
			var obj = new Category();
			obj.Id = id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
			obj.Save();
		}

	    static public void Delete(System.Int32 id, esSqlAccessType sqlAccessType)
		{
			var obj = new Category();
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
		public override System.Int32? StoreId
		{
			get { return base.StoreId;  }
			set { base.StoreId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = true)]
		public override System.Int32? ParentId
		{
			get { return base.ParentId;  }
			set { base.ParentId = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? NestingLevel
		{
			get { return base.NestingLevel;  }
			set { base.NestingLevel = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsDisplayed
		{
			get { return base.IsDisplayed;  }
			set { base.IsDisplayed = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Slug
		{
			get { return base.Slug;  }
			set { base.Slug = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Name
		{
			get { return base.Name;  }
			set { base.Name = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Title
		{
			get { return base.Title;  }
			set { base.Title = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String Description
		{
			get { return base.Description;  }
			set { base.Description = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SeoTitle
		{
			get { return base.SeoTitle;  }
			set { base.SeoTitle = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SeoDescription
		{
			get { return base.SeoDescription;  }
			set { base.SeoDescription = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.String SeoKeywords
		{
			get { return base.SeoKeywords;  }
			set { base.SeoKeywords = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Int16? SortOrder
		{
			get { return base.SortOrder;  }
			set { base.SortOrder = value; }
		}

			
		[Column(IsPrimaryKey = false, CanBeNull = false)]
		public override System.Boolean? IsSystemCategory
		{
			get { return base.IsSystemCategory;  }
			set { base.IsSystemCategory = value; }
		}


		#endregion
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("CategoryCollection")]
	public partial class CategoryCollection : esCategoryCollection, IEnumerable<Category>
	{
		public Category FindByPrimaryKey(System.Int32 id)
		{
			return this.SingleOrDefault(e => e.Id == id);
		}

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(Category))]
		public class CategoryCollectionWCFPacket : esCollectionWCFPacket<CategoryCollection>
		{
			public static implicit operator CategoryCollection(CategoryCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator CategoryCollectionWCFPacket(CategoryCollection collection)
			{
				return new CategoryCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class CategoryQuery : esCategoryQuery
	{
		public CategoryQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "CategoryQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(CategoryQuery query)
		{
			return CategoryQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator CategoryQuery(string query)
		{
			return (CategoryQuery)CategoryQuery.SerializeHelper.FromXml(query, typeof(CategoryQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esCategory : esEntity
	{
		public esCategory()
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
			CategoryQuery query = new CategoryQuery();
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
		/// Maps to DNNspot_Store_Category.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(CategoryMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(CategoryMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.StoreId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? StoreId
		{
			get
			{
				return base.GetSystemInt32(CategoryMetadata.ColumnNames.StoreId);
			}
			
			set
			{
				if(base.SetSystemInt32(CategoryMetadata.ColumnNames.StoreId, value))
				{
					this._UpToStoreByStoreId = null;
					this.OnPropertyChanged("UpToStoreByStoreId");
					OnPropertyChanged(CategoryMetadata.PropertyNames.StoreId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.ParentId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ParentId
		{
			get
			{
				return base.GetSystemInt32(CategoryMetadata.ColumnNames.ParentId);
			}
			
			set
			{
				if(base.SetSystemInt32(CategoryMetadata.ColumnNames.ParentId, value))
				{
					this._UpToCategoryByParentId = null;
					this.OnPropertyChanged("UpToCategoryByParentId");
					OnPropertyChanged(CategoryMetadata.PropertyNames.ParentId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.NestingLevel
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? NestingLevel
		{
			get
			{
				return base.GetSystemInt16(CategoryMetadata.ColumnNames.NestingLevel);
			}
			
			set
			{
				if(base.SetSystemInt16(CategoryMetadata.ColumnNames.NestingLevel, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.NestingLevel);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.IsDisplayed
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsDisplayed
		{
			get
			{
				return base.GetSystemBoolean(CategoryMetadata.ColumnNames.IsDisplayed);
			}
			
			set
			{
				if(base.SetSystemBoolean(CategoryMetadata.ColumnNames.IsDisplayed, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.IsDisplayed);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.Slug
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Slug
		{
			get
			{
				return base.GetSystemString(CategoryMetadata.ColumnNames.Slug);
			}
			
			set
			{
				if(base.SetSystemString(CategoryMetadata.ColumnNames.Slug, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.Slug);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.Name
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Name
		{
			get
			{
				return base.GetSystemString(CategoryMetadata.ColumnNames.Name);
			}
			
			set
			{
				if(base.SetSystemString(CategoryMetadata.ColumnNames.Name, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.Name);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.Title
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Title
		{
			get
			{
				return base.GetSystemString(CategoryMetadata.ColumnNames.Title);
			}
			
			set
			{
				if(base.SetSystemString(CategoryMetadata.ColumnNames.Title, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.Title);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.Description
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String Description
		{
			get
			{
				return base.GetSystemString(CategoryMetadata.ColumnNames.Description);
			}
			
			set
			{
				if(base.SetSystemString(CategoryMetadata.ColumnNames.Description, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.Description);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.SeoTitle
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SeoTitle
		{
			get
			{
				return base.GetSystemString(CategoryMetadata.ColumnNames.SeoTitle);
			}
			
			set
			{
				if(base.SetSystemString(CategoryMetadata.ColumnNames.SeoTitle, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.SeoTitle);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.SeoDescription
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SeoDescription
		{
			get
			{
				return base.GetSystemString(CategoryMetadata.ColumnNames.SeoDescription);
			}
			
			set
			{
				if(base.SetSystemString(CategoryMetadata.ColumnNames.SeoDescription, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.SeoDescription);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.SeoKeywords
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String SeoKeywords
		{
			get
			{
				return base.GetSystemString(CategoryMetadata.ColumnNames.SeoKeywords);
			}
			
			set
			{
				if(base.SetSystemString(CategoryMetadata.ColumnNames.SeoKeywords, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.SeoKeywords);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.SortOrder
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? SortOrder
		{
			get
			{
				return base.GetSystemInt16(CategoryMetadata.ColumnNames.SortOrder);
			}
			
			set
			{
				if(base.SetSystemInt16(CategoryMetadata.ColumnNames.SortOrder, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.SortOrder);
				}
			}
		}		
		
		/// <summary>
		/// Maps to DNNspot_Store_Category.IsSystemCategory
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? IsSystemCategory
		{
			get
			{
				return base.GetSystemBoolean(CategoryMetadata.ColumnNames.IsSystemCategory);
			}
			
			set
			{
				if(base.SetSystemBoolean(CategoryMetadata.ColumnNames.IsSystemCategory, value))
				{
					OnPropertyChanged(CategoryMetadata.PropertyNames.IsSystemCategory);
				}
			}
		}		
		
		[CLSCompliant(false)]
		internal protected Category _UpToCategoryByParentId;
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
						case "Id": this.str().Id = (string)value; break;							
						case "StoreId": this.str().StoreId = (string)value; break;							
						case "ParentId": this.str().ParentId = (string)value; break;							
						case "NestingLevel": this.str().NestingLevel = (string)value; break;							
						case "IsDisplayed": this.str().IsDisplayed = (string)value; break;							
						case "Slug": this.str().Slug = (string)value; break;							
						case "Name": this.str().Name = (string)value; break;							
						case "Title": this.str().Title = (string)value; break;							
						case "Description": this.str().Description = (string)value; break;							
						case "SeoTitle": this.str().SeoTitle = (string)value; break;							
						case "SeoDescription": this.str().SeoDescription = (string)value; break;							
						case "SeoKeywords": this.str().SeoKeywords = (string)value; break;							
						case "SortOrder": this.str().SortOrder = (string)value; break;							
						case "IsSystemCategory": this.str().IsSystemCategory = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(CategoryMetadata.PropertyNames.Id);
							break;
						
						case "StoreId":
						
							if (value == null || value is System.Int32)
								this.StoreId = (System.Int32?)value;
								OnPropertyChanged(CategoryMetadata.PropertyNames.StoreId);
							break;
						
						case "ParentId":
						
							if (value == null || value is System.Int32)
								this.ParentId = (System.Int32?)value;
								OnPropertyChanged(CategoryMetadata.PropertyNames.ParentId);
							break;
						
						case "NestingLevel":
						
							if (value == null || value is System.Int16)
								this.NestingLevel = (System.Int16?)value;
								OnPropertyChanged(CategoryMetadata.PropertyNames.NestingLevel);
							break;
						
						case "IsDisplayed":
						
							if (value == null || value is System.Boolean)
								this.IsDisplayed = (System.Boolean?)value;
								OnPropertyChanged(CategoryMetadata.PropertyNames.IsDisplayed);
							break;
						
						case "SortOrder":
						
							if (value == null || value is System.Int16)
								this.SortOrder = (System.Int16?)value;
								OnPropertyChanged(CategoryMetadata.PropertyNames.SortOrder);
							break;
						
						case "IsSystemCategory":
						
							if (value == null || value is System.Boolean)
								this.IsSystemCategory = (System.Boolean?)value;
								OnPropertyChanged(CategoryMetadata.PropertyNames.IsSystemCategory);
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
			public esStrings(esCategory entity)
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
				
			public System.String ParentId
			{
				get
				{
					System.Int32? data = entity.ParentId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ParentId = null;
					else entity.ParentId = Convert.ToInt32(value);
				}
			}
				
			public System.String NestingLevel
			{
				get
				{
					System.Int16? data = entity.NestingLevel;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.NestingLevel = null;
					else entity.NestingLevel = Convert.ToInt16(value);
				}
			}
				
			public System.String IsDisplayed
			{
				get
				{
					System.Boolean? data = entity.IsDisplayed;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsDisplayed = null;
					else entity.IsDisplayed = Convert.ToBoolean(value);
				}
			}
				
			public System.String Slug
			{
				get
				{
					System.String data = entity.Slug;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Slug = null;
					else entity.Slug = Convert.ToString(value);
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
				
			public System.String Title
			{
				get
				{
					System.String data = entity.Title;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Title = null;
					else entity.Title = Convert.ToString(value);
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
				
			public System.String SeoTitle
			{
				get
				{
					System.String data = entity.SeoTitle;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeoTitle = null;
					else entity.SeoTitle = Convert.ToString(value);
				}
			}
				
			public System.String SeoDescription
			{
				get
				{
					System.String data = entity.SeoDescription;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeoDescription = null;
					else entity.SeoDescription = Convert.ToString(value);
				}
			}
				
			public System.String SeoKeywords
			{
				get
				{
					System.String data = entity.SeoKeywords;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.SeoKeywords = null;
					else entity.SeoKeywords = Convert.ToString(value);
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
				
			public System.String IsSystemCategory
			{
				get
				{
					System.Boolean? data = entity.IsSystemCategory;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.IsSystemCategory = null;
					else entity.IsSystemCategory = Convert.ToBoolean(value);
				}
			}
			

			private esCategory entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return CategoryMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public CategoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CategoryQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CategoryQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(CategoryQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private CategoryQuery query;		
	}



	[Serializable]
	abstract public partial class esCategoryCollection : esEntityCollection<Category>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return CategoryMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "CategoryCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public CategoryQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new CategoryQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(CategoryQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new CategoryQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(CategoryQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((CategoryQuery)query);
		}

		#endregion
		
		private CategoryQuery query;
	}



	[Serializable]
	abstract public partial class esCategoryQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return CategoryMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "StoreId": return this.StoreId;
				case "ParentId": return this.ParentId;
				case "NestingLevel": return this.NestingLevel;
				case "IsDisplayed": return this.IsDisplayed;
				case "Slug": return this.Slug;
				case "Name": return this.Name;
				case "Title": return this.Title;
				case "Description": return this.Description;
				case "SeoTitle": return this.SeoTitle;
				case "SeoDescription": return this.SeoDescription;
				case "SeoKeywords": return this.SeoKeywords;
				case "SortOrder": return this.SortOrder;
				case "IsSystemCategory": return this.IsSystemCategory;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem StoreId
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.StoreId, esSystemType.Int32); }
		} 
		
		public esQueryItem ParentId
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.ParentId, esSystemType.Int32); }
		} 
		
		public esQueryItem NestingLevel
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.NestingLevel, esSystemType.Int16); }
		} 
		
		public esQueryItem IsDisplayed
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.IsDisplayed, esSystemType.Boolean); }
		} 
		
		public esQueryItem Slug
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.Slug, esSystemType.String); }
		} 
		
		public esQueryItem Name
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.Name, esSystemType.String); }
		} 
		
		public esQueryItem Title
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.Title, esSystemType.String); }
		} 
		
		public esQueryItem Description
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.Description, esSystemType.String); }
		} 
		
		public esQueryItem SeoTitle
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.SeoTitle, esSystemType.String); }
		} 
		
		public esQueryItem SeoDescription
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.SeoDescription, esSystemType.String); }
		} 
		
		public esQueryItem SeoKeywords
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.SeoKeywords, esSystemType.String); }
		} 
		
		public esQueryItem SortOrder
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.SortOrder, esSystemType.Int16); }
		} 
		
		public esQueryItem IsSystemCategory
		{
			get { return new esQueryItem(this, CategoryMetadata.ColumnNames.IsSystemCategory, esSystemType.Boolean); }
		} 
		
		#endregion
		
	}


	
	public partial class Category : esCategory
	{

		#region CategoryCollectionByParentId - Zero To Many
		
		static public esPrefetchMap Prefetch_CategoryCollectionByParentId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Category.CategoryCollectionByParentId_Delegate;
				map.PropertyName = "CategoryCollectionByParentId";
				map.MyColumnName = "Id";
				map.ParentColumnName = "ParentId";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void CategoryCollectionByParentId_Delegate(esPrefetchParameters data)
		{
			CategoryQuery parent = new CategoryQuery(data.NextAlias());

			CategoryQuery me = data.You != null ? data.You as CategoryQuery : new CategoryQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.ParentId == me.Id);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_Category_DNNspot_Store_Category1
		/// </summary>

		[XmlIgnore]
		public CategoryCollection CategoryCollectionByParentId
		{
			get
			{
				if(this._CategoryCollectionByParentId == null)
				{
					this._CategoryCollectionByParentId = new CategoryCollection();
					this._CategoryCollectionByParentId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("CategoryCollectionByParentId", this._CategoryCollectionByParentId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._CategoryCollectionByParentId.Query.Where(this._CategoryCollectionByParentId.Query.ParentId == this.Id);
							this._CategoryCollectionByParentId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._CategoryCollectionByParentId.fks.Add(CategoryMetadata.ColumnNames.ParentId, this.Id);
					}
				}

				return this._CategoryCollectionByParentId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._CategoryCollectionByParentId != null) 
				{ 
					this.RemovePostSave("CategoryCollectionByParentId"); 
					this._CategoryCollectionByParentId = null;
					
				} 
			} 			
		}
			
		
		private CategoryCollection _CategoryCollectionByParentId;
		#endregion

				
		#region UpToCategoryByParentId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Category_DNNspot_Store_Category1
		/// </summary>

		[XmlIgnore]
					
		public Category UpToCategoryByParentId
		{
			get
			{
				if (this.es.IsLazyLoadDisabled) return null;
				
				if(this._UpToCategoryByParentId == null && ParentId != null)
				{
					this._UpToCategoryByParentId = new Category();
					this._UpToCategoryByParentId.es.Connection.Name = this.es.Connection.Name;
					this.SetPreSave("UpToCategoryByParentId", this._UpToCategoryByParentId);
					this._UpToCategoryByParentId.Query.Where(this._UpToCategoryByParentId.Query.Id == this.ParentId);
					this._UpToCategoryByParentId.Query.Load();
				}	
				return this._UpToCategoryByParentId;
			}
			
			set
			{
				this.RemovePreSave("UpToCategoryByParentId");
				

				if(value == null)
				{
					this.ParentId = null;
					this._UpToCategoryByParentId = null;
				}
				else
				{
					this.ParentId = value.Id;
					this._UpToCategoryByParentId = value;
					this.SetPreSave("UpToCategoryByParentId", this._UpToCategoryByParentId);
				}
				
			}
		}
		#endregion
		

		#region UpToProductCollectionByProductCategory - Many To Many
		/// <summary>
		/// Many to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Category
		/// </summary>

		[XmlIgnore]
		public ProductCollection UpToProductCollectionByProductCategory
		{
			get
			{
				if(this._UpToProductCollectionByProductCategory == null)
				{
					this._UpToProductCollectionByProductCategory = new ProductCollection();
					this._UpToProductCollectionByProductCategory.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("UpToProductCollectionByProductCategory", this._UpToProductCollectionByProductCategory);
					if (!this.es.IsLazyLoadDisabled && this.Id != null)
					{
						ProductQuery m = new ProductQuery("m");
						ProductCategoryQuery j = new ProductCategoryQuery("j");
						m.Select(m);
						m.InnerJoin(j).On(m.Id == j.ProductId);
                        m.Where(j.CategoryId == this.Id);

						this._UpToProductCollectionByProductCategory.Load(m);
					}
				}

				return this._UpToProductCollectionByProductCategory;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._UpToProductCollectionByProductCategory != null) 
				{ 
					this.RemovePostSave("UpToProductCollectionByProductCategory"); 
					this._UpToProductCollectionByProductCategory = null;
					
				} 
			}  			
		}

		/// <summary>
		/// Many to Many Associate
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Category
		/// </summary>
		public void AssociateProductCollectionByProductCategory(Product entity)
		{
			if (this._ProductCategoryCollection == null)
			{
				this._ProductCategoryCollection = new ProductCategoryCollection();
				this._ProductCategoryCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("ProductCategoryCollection", this._ProductCategoryCollection);
			}

			ProductCategory obj = this._ProductCategoryCollection.AddNew();
			obj.CategoryId = this.Id;
			obj.ProductId = entity.Id;
		}

		/// <summary>
		/// Many to Many Dissociate
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Category
		/// </summary>
		public void DissociateProductCollectionByProductCategory(Product entity)
		{
			if (this._ProductCategoryCollection == null)
			{
				this._ProductCategoryCollection = new ProductCategoryCollection();
				this._ProductCategoryCollection.es.Connection.Name = this.es.Connection.Name;
				this.SetPostSave("ProductCategoryCollection", this._ProductCategoryCollection);
			}

			ProductCategory obj = this._ProductCategoryCollection.AddNew();
			obj.CategoryId = this.Id;
            obj.ProductId = entity.Id;
			obj.AcceptChanges();
			obj.MarkAsDeleted();
		}

		private ProductCollection _UpToProductCollectionByProductCategory;
		private ProductCategoryCollection _ProductCategoryCollection;
		#endregion

		#region ProductCategoryCollectionByCategoryId - Zero To Many
		
		static public esPrefetchMap Prefetch_ProductCategoryCollectionByCategoryId
		{
			get
			{
				esPrefetchMap map = new esPrefetchMap();
				map.PrefetchDelegate = DNNspot.Store.DataModel.Category.ProductCategoryCollectionByCategoryId_Delegate;
				map.PropertyName = "ProductCategoryCollectionByCategoryId";
				map.MyColumnName = "CategoryId";
				map.ParentColumnName = "Id";
				map.IsMultiPartKey = false;
				return map;
			}
		}		
		
		static private void ProductCategoryCollectionByCategoryId_Delegate(esPrefetchParameters data)
		{
			CategoryQuery parent = new CategoryQuery(data.NextAlias());

			ProductCategoryQuery me = data.You != null ? data.You as ProductCategoryQuery : new ProductCategoryQuery(data.NextAlias());

			if (data.Root == null)
			{
				data.Root = me;
			}
			
			data.Root.InnerJoin(parent).On(parent.Id == me.CategoryId);

			data.You = parent;
		}			
		
		/// <summary>
		/// Zero to Many
		/// Foreign Key Name - FK_DNNspot_Store_ProductCategory_DNNspot_Store_Category
		/// </summary>

		[XmlIgnore]
		public ProductCategoryCollection ProductCategoryCollectionByCategoryId
		{
			get
			{
				if(this._ProductCategoryCollectionByCategoryId == null)
				{
					this._ProductCategoryCollectionByCategoryId = new ProductCategoryCollection();
					this._ProductCategoryCollectionByCategoryId.es.Connection.Name = this.es.Connection.Name;
					this.SetPostSave("ProductCategoryCollectionByCategoryId", this._ProductCategoryCollectionByCategoryId);
				
					if (this.Id != null)
					{
						if (!this.es.IsLazyLoadDisabled)
						{
							this._ProductCategoryCollectionByCategoryId.Query.Where(this._ProductCategoryCollectionByCategoryId.Query.CategoryId == this.Id);
							this._ProductCategoryCollectionByCategoryId.Query.Load();
						}

						// Auto-hookup Foreign Keys
						this._ProductCategoryCollectionByCategoryId.fks.Add(ProductCategoryMetadata.ColumnNames.CategoryId, this.Id);
					}
				}

				return this._ProductCategoryCollectionByCategoryId;
			}
			
			set 
			{ 
				if (value != null) throw new Exception("'value' Must be null"); 
			 
				if (this._ProductCategoryCollectionByCategoryId != null) 
				{ 
					this.RemovePostSave("ProductCategoryCollectionByCategoryId"); 
					this._ProductCategoryCollectionByCategoryId = null;
					
				} 
			} 			
		}
			
		
		private ProductCategoryCollection _ProductCategoryCollectionByCategoryId;
		#endregion

				
		#region UpToStoreByStoreId - Many To One
		/// <summary>
		/// Many to One
		/// Foreign Key Name - FK_DNNspot_Store_Category_DNNspot_Store_Store
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
		

		
		protected override esEntityCollectionBase CreateCollectionForPrefetch(string name)
		{
			esEntityCollectionBase coll = null;

			switch (name)
			{
				case "CategoryCollectionByParentId":
					coll = this.CategoryCollectionByParentId;
					break;
				case "ProductCategoryCollectionByCategoryId":
					coll = this.ProductCategoryCollectionByCategoryId;
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
			
			props.Add(new esPropertyDescriptor(this, "CategoryCollectionByParentId", typeof(CategoryCollection), new Category()));
			props.Add(new esPropertyDescriptor(this, "ProductCategoryCollectionByCategoryId", typeof(ProductCategoryCollection), new ProductCategory()));
		
			return props;
		}
		/// <summary>
		/// Used internally for retrieving AutoIncrementing keys
		/// during hierarchical PreSave.
		/// </summary>
		protected override void ApplyPreSaveKeys()
		{
			if(!this.es.IsDeleted && this._UpToCategoryByParentId != null)
			{
				this.ParentId = this._UpToCategoryByParentId.Id;
			}
			if(!this.es.IsDeleted && this._UpToStoreByStoreId != null)
			{
				this.StoreId = this._UpToStoreByStoreId.Id;
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
			if(this._CategoryCollectionByParentId != null)
			{
				Apply(this._CategoryCollectionByParentId, "ParentId", this.Id);
			}
			if(this._ProductCategoryCollection != null)
			{
				Apply(this._ProductCategoryCollection, "CategoryId", this.Id);
			}
			if(this._ProductCategoryCollectionByCategoryId != null)
			{
				Apply(this._ProductCategoryCollectionByCategoryId, "CategoryId", this.Id);
			}
		}
		
	}
	



	[Serializable]
	public partial class CategoryMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected CategoryMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(CategoryMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CategoryMetadata.PropertyNames.Id;
			c.IsInPrimaryKey = true;
			c.IsAutoIncrement = true;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.StoreId, 1, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CategoryMetadata.PropertyNames.StoreId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.ParentId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = CategoryMetadata.PropertyNames.ParentId;
			c.NumericPrecision = 10;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.NestingLevel, 3, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = CategoryMetadata.PropertyNames.NestingLevel;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.IsDisplayed, 4, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CategoryMetadata.PropertyNames.IsDisplayed;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.Slug, 5, typeof(System.String), esSystemType.String);
			c.PropertyName = CategoryMetadata.PropertyNames.Slug;
			c.CharacterMaxLength = 50;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.Name, 6, typeof(System.String), esSystemType.String);
			c.PropertyName = CategoryMetadata.PropertyNames.Name;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.Title, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = CategoryMetadata.PropertyNames.Title;
			c.CharacterMaxLength = 150;
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.Description, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = CategoryMetadata.PropertyNames.Description;
			c.CharacterMaxLength = 1073741823;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.SeoTitle, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = CategoryMetadata.PropertyNames.SeoTitle;
			c.CharacterMaxLength = 300;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.SeoDescription, 10, typeof(System.String), esSystemType.String);
			c.PropertyName = CategoryMetadata.PropertyNames.SeoDescription;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.SeoKeywords, 11, typeof(System.String), esSystemType.String);
			c.PropertyName = CategoryMetadata.PropertyNames.SeoKeywords;
			c.CharacterMaxLength = 500;
			c.HasDefault = true;
			c.Default = @"('')";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.SortOrder, 12, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = CategoryMetadata.PropertyNames.SortOrder;
			c.NumericPrecision = 5;
			c.HasDefault = true;
			c.Default = @"((1))";
			m_columns.Add(c);
				
			c = new esColumnMetadata(CategoryMetadata.ColumnNames.IsSystemCategory, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = CategoryMetadata.PropertyNames.IsSystemCategory;
			c.HasDefault = true;
			c.Default = @"((0))";
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public CategoryMetadata Meta()
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
			 public const string StoreId = "StoreId";
			 public const string ParentId = "ParentId";
			 public const string NestingLevel = "NestingLevel";
			 public const string IsDisplayed = "IsDisplayed";
			 public const string Slug = "Slug";
			 public const string Name = "Name";
			 public const string Title = "Title";
			 public const string Description = "Description";
			 public const string SeoTitle = "SeoTitle";
			 public const string SeoDescription = "SeoDescription";
			 public const string SeoKeywords = "SeoKeywords";
			 public const string SortOrder = "SortOrder";
			 public const string IsSystemCategory = "IsSystemCategory";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string StoreId = "StoreId";
			 public const string ParentId = "ParentId";
			 public const string NestingLevel = "NestingLevel";
			 public const string IsDisplayed = "IsDisplayed";
			 public const string Slug = "Slug";
			 public const string Name = "Name";
			 public const string Title = "Title";
			 public const string Description = "Description";
			 public const string SeoTitle = "SeoTitle";
			 public const string SeoDescription = "SeoDescription";
			 public const string SeoKeywords = "SeoKeywords";
			 public const string SortOrder = "SortOrder";
			 public const string IsSystemCategory = "IsSystemCategory";
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
			lock (typeof(CategoryMetadata))
			{
				if(CategoryMetadata.mapDelegates == null)
				{
					CategoryMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (CategoryMetadata.meta == null)
				{
					CategoryMetadata.meta = new CategoryMetadata();
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
				meta.AddTypeMap("StoreId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ParentId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("NestingLevel", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsDisplayed", new esTypeMap("bit", "System.Boolean"));
				meta.AddTypeMap("Slug", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("Name", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Title", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("Description", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("SeoTitle", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeoDescription", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SeoKeywords", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("SortOrder", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("IsSystemCategory", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "DNNspot_Store_Category";
					meta.Destination = objectQualifier + "DNNspot_Store_Category";
					
					meta.spInsert = objectQualifier + "proc_DNNspot_Store_CategoryInsert";				
					meta.spUpdate = objectQualifier + "proc_DNNspot_Store_CategoryUpdate";		
					meta.spDelete = objectQualifier + "proc_DNNspot_Store_CategoryDelete";
					meta.spLoadAll = objectQualifier + "proc_DNNspot_Store_CategoryLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_DNNspot_Store_CategoryLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "DNNspot_Store_Category";
					meta.Destination = "DNNspot_Store_Category";
									
					meta.spInsert = "proc_DNNspot_Store_CategoryInsert";				
					meta.spUpdate = "proc_DNNspot_Store_CategoryUpdate";		
					meta.spDelete = "proc_DNNspot_Store_CategoryDelete";
					meta.spLoadAll = "proc_DNNspot_Store_CategoryLoadAll";
					meta.spLoadByPrimaryKey = "proc_DNNspot_Store_CategoryLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private CategoryMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
