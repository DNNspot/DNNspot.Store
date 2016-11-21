
/*
===============================================================================
                    EntitySpaces Studio by EntitySpaces, LLC
             Persistence Layer and Business Objects for Microsoft .NET
             EntitySpaces(TM) is a legal trademark of EntitySpaces, LLC
                          http://www.entityspaces.net
===============================================================================
EntitySpaces Version : 2012.1.0930.0
EntitySpaces Driver  : SQL
Date Generated       : 4/25/2013 4:44:21 PM
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
	/// Encapsulates the 'vDNNspot_Store_CartItemProductInfo' view
	/// </summary>

    [DebuggerDisplay("Data = {Debug}")]
	[Serializable]
	[DataContract]
	[KnownType(typeof(vCartItemProductInfo))]	
	[XmlType("vCartItemProductInfo")]
	public partial class vCartItemProductInfo : esvCartItemProductInfo
	{	
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden | DebuggerBrowsableState.Never)]
		protected override esEntityDebuggerView[] Debug
		{
			get { return base.Debug; }
		}

		override public esEntity CreateInstance()
		{
			return new vCartItemProductInfo();
		}
		
		#region Static Quick Access Methods
		
		#endregion

		
					
		
	
	}



    [DebuggerDisplay("Count = {Count}")]
	[Serializable]
	[CollectionDataContract]
	[XmlType("vCartItemProductInfoCollection")]
	public partial class vCartItemProductInfoCollection : esvCartItemProductInfoCollection, IEnumerable<vCartItemProductInfo>
	{

		
		
		#region WCF Service Class
		
		[DataContract]
		[KnownType(typeof(vCartItemProductInfo))]
		public class vCartItemProductInfoCollectionWCFPacket : esCollectionWCFPacket<vCartItemProductInfoCollection>
		{
			public static implicit operator vCartItemProductInfoCollection(vCartItemProductInfoCollectionWCFPacket packet)
			{
				return packet.Collection;
			}

			public static implicit operator vCartItemProductInfoCollectionWCFPacket(vCartItemProductInfoCollection collection)
			{
				return new vCartItemProductInfoCollectionWCFPacket() { Collection = collection };
			}
		}
		
		#endregion
		
				
	}



    [DebuggerDisplay("Query = {Parse()}")]
	[Serializable]	
	public partial class vCartItemProductInfoQuery : esvCartItemProductInfoQuery
	{
		public vCartItemProductInfoQuery(string joinAlias)
		{
			this.es.JoinAlias = joinAlias;
		}	

		override protected string GetQueryName()
		{
			return "vCartItemProductInfoQuery";
		}
		
					
	
		#region Explicit Casts
		
		public static explicit operator string(vCartItemProductInfoQuery query)
		{
			return vCartItemProductInfoQuery.SerializeHelper.ToXml(query);
		}

		public static explicit operator vCartItemProductInfoQuery(string query)
		{
			return (vCartItemProductInfoQuery)vCartItemProductInfoQuery.SerializeHelper.FromXml(query, typeof(vCartItemProductInfoQuery));
		}
		
		#endregion		
	}

	[DataContract]
	[Serializable]
	abstract public partial class esvCartItemProductInfo : esEntity
	{
		public esvCartItemProductInfo()
		{

		}
		
		#region LoadByPrimaryKey
		
		#endregion
		
		#region Properties
		
		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.Id
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Id
		{
			get
			{
				return base.GetSystemInt32(vCartItemProductInfoMetadata.ColumnNames.Id);
			}
			
			set
			{
				if(base.SetSystemInt32(vCartItemProductInfoMetadata.ColumnNames.Id, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.Id);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.CartId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Guid? CartId
		{
			get
			{
				return base.GetSystemGuid(vCartItemProductInfoMetadata.ColumnNames.CartId);
			}
			
			set
			{
				if(base.SetSystemGuid(vCartItemProductInfoMetadata.ColumnNames.CartId, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.CartId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? ProductId
		{
			get
			{
				return base.GetSystemInt32(vCartItemProductInfoMetadata.ColumnNames.ProductId);
			}
			
			set
			{
				if(base.SetSystemInt32(vCartItemProductInfoMetadata.ColumnNames.ProductId, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.Quantity
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int32? Quantity
		{
			get
			{
				return base.GetSystemInt32(vCartItemProductInfoMetadata.ColumnNames.Quantity);
			}
			
			set
			{
				if(base.SetSystemInt32(vCartItemProductInfoMetadata.ColumnNames.Quantity, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.Quantity);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductFieldData
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProductFieldData
		{
			get
			{
				return base.GetSystemString(vCartItemProductInfoMetadata.ColumnNames.ProductFieldData);
			}
			
			set
			{
				if(base.SetSystemString(vCartItemProductInfoMetadata.ColumnNames.ProductFieldData, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductFieldData);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.CreatedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? CreatedOn
		{
			get
			{
				return base.GetSystemDateTime(vCartItemProductInfoMetadata.ColumnNames.CreatedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(vCartItemProductInfoMetadata.ColumnNames.CreatedOn, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.CreatedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ModifiedOn
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.DateTime? ModifiedOn
		{
			get
			{
				return base.GetSystemDateTime(vCartItemProductInfoMetadata.ColumnNames.ModifiedOn);
			}
			
			set
			{
				if(base.SetSystemDateTime(vCartItemProductInfoMetadata.ColumnNames.ModifiedOn, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ModifiedOn);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.MainPhotoFilename
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String MainPhotoFilename
		{
			get
			{
				return base.GetSystemString(vCartItemProductInfoMetadata.ColumnNames.MainPhotoFilename);
			}
			
			set
			{
				if(base.SetSystemString(vCartItemProductInfoMetadata.ColumnNames.MainPhotoFilename, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.MainPhotoFilename);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductName
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProductName
		{
			get
			{
				return base.GetSystemString(vCartItemProductInfoMetadata.ColumnNames.ProductName);
			}
			
			set
			{
				if(base.SetSystemString(vCartItemProductInfoMetadata.ColumnNames.ProductName, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductName);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductSlug
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.String ProductSlug
		{
			get
			{
				return base.GetSystemString(vCartItemProductInfoMetadata.ColumnNames.ProductSlug);
			}
			
			set
			{
				if(base.SetSystemString(vCartItemProductInfoMetadata.ColumnNames.ProductSlug, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductSlug);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductWeight
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? ProductWeight
		{
			get
			{
				return base.GetSystemDecimal(vCartItemProductInfoMetadata.ColumnNames.ProductWeight);
			}
			
			set
			{
				if(base.SetSystemDecimal(vCartItemProductInfoMetadata.ColumnNames.ProductWeight, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductWeight);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductShippingAdditionalFeePerItem
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Decimal? ProductShippingAdditionalFeePerItem
		{
			get
			{
				return base.GetSystemDecimal(vCartItemProductInfoMetadata.ColumnNames.ProductShippingAdditionalFeePerItem);
			}
			
			set
			{
				if(base.SetSystemDecimal(vCartItemProductInfoMetadata.ColumnNames.ProductShippingAdditionalFeePerItem, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductShippingAdditionalFeePerItem);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductDeliveryMethodId
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Int16? ProductDeliveryMethodId
		{
			get
			{
				return base.GetSystemInt16(vCartItemProductInfoMetadata.ColumnNames.ProductDeliveryMethodId);
			}
			
			set
			{
				if(base.SetSystemInt16(vCartItemProductInfoMetadata.ColumnNames.ProductDeliveryMethodId, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductDeliveryMethodId);
				}
			}
		}		
		
		/// <summary>
		/// Maps to vDNNspot_Store_CartItemProductInfo.ProductIsTaxable
		/// </summary>
		[DataMember(EmitDefaultValue=false)]
		virtual public System.Boolean? ProductIsTaxable
		{
			get
			{
				return base.GetSystemBoolean(vCartItemProductInfoMetadata.ColumnNames.ProductIsTaxable);
			}
			
			set
			{
				if(base.SetSystemBoolean(vCartItemProductInfoMetadata.ColumnNames.ProductIsTaxable, value))
				{
					OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductIsTaxable);
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
						case "CartId": this.str().CartId = (string)value; break;							
						case "ProductId": this.str().ProductId = (string)value; break;							
						case "Quantity": this.str().Quantity = (string)value; break;							
						case "ProductFieldData": this.str().ProductFieldData = (string)value; break;							
						case "CreatedOn": this.str().CreatedOn = (string)value; break;							
						case "ModifiedOn": this.str().ModifiedOn = (string)value; break;							
						case "MainPhotoFilename": this.str().MainPhotoFilename = (string)value; break;							
						case "ProductName": this.str().ProductName = (string)value; break;							
						case "ProductSlug": this.str().ProductSlug = (string)value; break;							
						case "ProductWeight": this.str().ProductWeight = (string)value; break;							
						case "ProductShippingAdditionalFeePerItem": this.str().ProductShippingAdditionalFeePerItem = (string)value; break;							
						case "ProductDeliveryMethodId": this.str().ProductDeliveryMethodId = (string)value; break;							
						case "ProductIsTaxable": this.str().ProductIsTaxable = (string)value; break;
					}
				}
				else
				{
					switch (name)
					{	
						case "Id":
						
							if (value == null || value is System.Int32)
								this.Id = (System.Int32?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.Id);
							break;
						
						case "CartId":
						
							if (value == null || value is System.Guid)
								this.CartId = (System.Guid?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.CartId);
							break;
						
						case "ProductId":
						
							if (value == null || value is System.Int32)
								this.ProductId = (System.Int32?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductId);
							break;
						
						case "Quantity":
						
							if (value == null || value is System.Int32)
								this.Quantity = (System.Int32?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.Quantity);
							break;
						
						case "CreatedOn":
						
							if (value == null || value is System.DateTime)
								this.CreatedOn = (System.DateTime?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.CreatedOn);
							break;
						
						case "ModifiedOn":
						
							if (value == null || value is System.DateTime)
								this.ModifiedOn = (System.DateTime?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ModifiedOn);
							break;
						
						case "ProductWeight":
						
							if (value == null || value is System.Decimal)
								this.ProductWeight = (System.Decimal?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductWeight);
							break;
						
						case "ProductShippingAdditionalFeePerItem":
						
							if (value == null || value is System.Decimal)
								this.ProductShippingAdditionalFeePerItem = (System.Decimal?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductShippingAdditionalFeePerItem);
							break;
						
						case "ProductDeliveryMethodId":
						
							if (value == null || value is System.Int16)
								this.ProductDeliveryMethodId = (System.Int16?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductDeliveryMethodId);
							break;
						
						case "ProductIsTaxable":
						
							if (value == null || value is System.Boolean)
								this.ProductIsTaxable = (System.Boolean?)value;
								OnPropertyChanged(vCartItemProductInfoMetadata.PropertyNames.ProductIsTaxable);
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
			public esStrings(esvCartItemProductInfo entity)
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
				
			public System.String Quantity
			{
				get
				{
					System.Int32? data = entity.Quantity;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.Quantity = null;
					else entity.Quantity = Convert.ToInt32(value);
				}
			}
				
			public System.String ProductFieldData
			{
				get
				{
					System.String data = entity.ProductFieldData;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductFieldData = null;
					else entity.ProductFieldData = Convert.ToString(value);
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
				
			public System.String ModifiedOn
			{
				get
				{
					System.DateTime? data = entity.ModifiedOn;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ModifiedOn = null;
					else entity.ModifiedOn = Convert.ToDateTime(value);
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
				
			public System.String ProductName
			{
				get
				{
					System.String data = entity.ProductName;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductName = null;
					else entity.ProductName = Convert.ToString(value);
				}
			}
				
			public System.String ProductSlug
			{
				get
				{
					System.String data = entity.ProductSlug;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductSlug = null;
					else entity.ProductSlug = Convert.ToString(value);
				}
			}
				
			public System.String ProductWeight
			{
				get
				{
					System.Decimal? data = entity.ProductWeight;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductWeight = null;
					else entity.ProductWeight = Convert.ToDecimal(value);
				}
			}
				
			public System.String ProductShippingAdditionalFeePerItem
			{
				get
				{
					System.Decimal? data = entity.ProductShippingAdditionalFeePerItem;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductShippingAdditionalFeePerItem = null;
					else entity.ProductShippingAdditionalFeePerItem = Convert.ToDecimal(value);
				}
			}
				
			public System.String ProductDeliveryMethodId
			{
				get
				{
					System.Int16? data = entity.ProductDeliveryMethodId;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductDeliveryMethodId = null;
					else entity.ProductDeliveryMethodId = Convert.ToInt16(value);
				}
			}
				
			public System.String ProductIsTaxable
			{
				get
				{
					System.Boolean? data = entity.ProductIsTaxable;
					return (data == null) ? String.Empty : Convert.ToString(data);
				}

				set
				{
					if (value == null || value.Length == 0) entity.ProductIsTaxable = null;
					else entity.ProductIsTaxable = Convert.ToBoolean(value);
				}
			}
			

			private esvCartItemProductInfo entity;
		}
		
		[NonSerialized]
		private esStrings esstrings;		
		
		#endregion
		
		#region Housekeeping methods

		override protected IMetadata Meta
		{
			get
			{
				return vCartItemProductInfoMetadata.Meta();
			}
		}

		#endregion		
		
		#region Query Logic

		public vCartItemProductInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vCartItemProductInfoQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vCartItemProductInfoQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return this.Query.Load();
		}
		
		protected void InitQuery(vCartItemProductInfoQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntity)this).Connection;
			}			
		}

		#endregion
		
        [IgnoreDataMember]
		private vCartItemProductInfoQuery query;		
	}



	[Serializable]
	abstract public partial class esvCartItemProductInfoCollection : esEntityCollection<vCartItemProductInfo>
	{
		#region Housekeeping methods
		override protected IMetadata Meta
		{
			get
			{
				return vCartItemProductInfoMetadata.Meta();
			}
		}

		protected override string GetCollectionName()
		{
			return "vCartItemProductInfoCollection";
		}

		#endregion		
		
		#region Query Logic

	#if (!WindowsCE)
		[BrowsableAttribute(false)]
	#endif
		public vCartItemProductInfoQuery Query
		{
			get
			{
				if (this.query == null)
				{
					this.query = new vCartItemProductInfoQuery();
					InitQuery(this.query);
				}

				return this.query;
			}
		}

		public bool Load(vCartItemProductInfoQuery query)
		{
			this.query = query;
			InitQuery(this.query);
			return Query.Load();
		}

		override protected esDynamicQuery GetDynamicQuery()
		{
			if (this.query == null)
			{
				this.query = new vCartItemProductInfoQuery();
				this.InitQuery(query);
			}
			return this.query;
		}

		protected void InitQuery(vCartItemProductInfoQuery query)
		{
			query.OnLoadDelegate = this.OnQueryLoaded;
			
			if (!query.es2.HasConnection)
			{
				query.es2.Connection = ((IEntityCollection)this).Connection;
			}			
		}

		protected override void HookupQuery(esDynamicQuery query)
		{
			this.InitQuery((vCartItemProductInfoQuery)query);
		}

		#endregion
		
		private vCartItemProductInfoQuery query;
	}



	[Serializable]
	abstract public partial class esvCartItemProductInfoQuery : esDynamicQuery
	{
		override protected IMetadata Meta
		{
			get
			{
				return vCartItemProductInfoMetadata.Meta();
			}
		}	
		
		#region QueryItemFromName
		
        protected override esQueryItem QueryItemFromName(string name)
        {
            switch (name)
            {
				case "Id": return this.Id;
				case "CartId": return this.CartId;
				case "ProductId": return this.ProductId;
				case "Quantity": return this.Quantity;
				case "ProductFieldData": return this.ProductFieldData;
				case "CreatedOn": return this.CreatedOn;
				case "ModifiedOn": return this.ModifiedOn;
				case "MainPhotoFilename": return this.MainPhotoFilename;
				case "ProductName": return this.ProductName;
				case "ProductSlug": return this.ProductSlug;
				case "ProductWeight": return this.ProductWeight;
				case "ProductShippingAdditionalFeePerItem": return this.ProductShippingAdditionalFeePerItem;
				case "ProductDeliveryMethodId": return this.ProductDeliveryMethodId;
				case "ProductIsTaxable": return this.ProductIsTaxable;

                default: return null;
            }
        }		
		
		#endregion
		
		#region esQueryItems

		public esQueryItem Id
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.Id, esSystemType.Int32); }
		} 
		
		public esQueryItem CartId
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.CartId, esSystemType.Guid); }
		} 
		
		public esQueryItem ProductId
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductId, esSystemType.Int32); }
		} 
		
		public esQueryItem Quantity
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.Quantity, esSystemType.Int32); }
		} 
		
		public esQueryItem ProductFieldData
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductFieldData, esSystemType.String); }
		} 
		
		public esQueryItem CreatedOn
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.CreatedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem ModifiedOn
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ModifiedOn, esSystemType.DateTime); }
		} 
		
		public esQueryItem MainPhotoFilename
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.MainPhotoFilename, esSystemType.String); }
		} 
		
		public esQueryItem ProductName
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductName, esSystemType.String); }
		} 
		
		public esQueryItem ProductSlug
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductSlug, esSystemType.String); }
		} 
		
		public esQueryItem ProductWeight
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductWeight, esSystemType.Decimal); }
		} 
		
		public esQueryItem ProductShippingAdditionalFeePerItem
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductShippingAdditionalFeePerItem, esSystemType.Decimal); }
		} 
		
		public esQueryItem ProductDeliveryMethodId
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductDeliveryMethodId, esSystemType.Int16); }
		} 
		
		public esQueryItem ProductIsTaxable
		{
			get { return new esQueryItem(this, vCartItemProductInfoMetadata.ColumnNames.ProductIsTaxable, esSystemType.Boolean); }
		} 
		
		#endregion
		
	}



	[Serializable]
	public partial class vCartItemProductInfoMetadata : esMetadata, IMetadata
	{
		#region Protected Constructor
		protected vCartItemProductInfoMetadata()
		{
			m_columns = new esColumnMetadataCollection();
			esColumnMetadata c;

			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.Id, 0, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.Id;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.CartId, 1, typeof(System.Guid), esSystemType.Guid);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.CartId;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductId, 2, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductId;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.Quantity, 3, typeof(System.Int32), esSystemType.Int32);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.Quantity;
			c.NumericPrecision = 10;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductFieldData, 4, typeof(System.String), esSystemType.String);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductFieldData;
			c.CharacterMaxLength = 1073741823;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.CreatedOn, 5, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.CreatedOn;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ModifiedOn, 6, typeof(System.DateTime), esSystemType.DateTime);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ModifiedOn;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.MainPhotoFilename, 7, typeof(System.String), esSystemType.String);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.MainPhotoFilename;
			c.CharacterMaxLength = 500;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductName, 8, typeof(System.String), esSystemType.String);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductName;
			c.CharacterMaxLength = 250;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductSlug, 9, typeof(System.String), esSystemType.String);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductSlug;
			c.CharacterMaxLength = 50;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductWeight, 10, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductWeight;
			c.NumericPrecision = 10;
			c.NumericScale = 4;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductShippingAdditionalFeePerItem, 11, typeof(System.Decimal), esSystemType.Decimal);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductShippingAdditionalFeePerItem;
			c.NumericPrecision = 19;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductDeliveryMethodId, 12, typeof(System.Int16), esSystemType.Int16);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductDeliveryMethodId;
			c.NumericPrecision = 5;
			c.IsNullable = true;
			m_columns.Add(c);
				
			c = new esColumnMetadata(vCartItemProductInfoMetadata.ColumnNames.ProductIsTaxable, 13, typeof(System.Boolean), esSystemType.Boolean);
			c.PropertyName = vCartItemProductInfoMetadata.PropertyNames.ProductIsTaxable;
			m_columns.Add(c);
				
		}
		#endregion	
	
		static public vCartItemProductInfoMetadata Meta()
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
			 public const string CartId = "CartId";
			 public const string ProductId = "ProductId";
			 public const string Quantity = "Quantity";
			 public const string ProductFieldData = "ProductFieldData";
			 public const string CreatedOn = "CreatedOn";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string MainPhotoFilename = "MainPhotoFilename";
			 public const string ProductName = "ProductName";
			 public const string ProductSlug = "ProductSlug";
			 public const string ProductWeight = "ProductWeight";
			 public const string ProductShippingAdditionalFeePerItem = "ProductShippingAdditionalFeePerItem";
			 public const string ProductDeliveryMethodId = "ProductDeliveryMethodId";
			 public const string ProductIsTaxable = "ProductIsTaxable";
		}
		#endregion	
		
		#region PropertyNames
		public class PropertyNames
		{ 
			 public const string Id = "Id";
			 public const string CartId = "CartId";
			 public const string ProductId = "ProductId";
			 public const string Quantity = "Quantity";
			 public const string ProductFieldData = "ProductFieldData";
			 public const string CreatedOn = "CreatedOn";
			 public const string ModifiedOn = "ModifiedOn";
			 public const string MainPhotoFilename = "MainPhotoFilename";
			 public const string ProductName = "ProductName";
			 public const string ProductSlug = "ProductSlug";
			 public const string ProductWeight = "ProductWeight";
			 public const string ProductShippingAdditionalFeePerItem = "ProductShippingAdditionalFeePerItem";
			 public const string ProductDeliveryMethodId = "ProductDeliveryMethodId";
			 public const string ProductIsTaxable = "ProductIsTaxable";
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
			lock (typeof(vCartItemProductInfoMetadata))
			{
				if(vCartItemProductInfoMetadata.mapDelegates == null)
				{
					vCartItemProductInfoMetadata.mapDelegates = new Dictionary<string,MapToMeta>();
				}
				
				if (vCartItemProductInfoMetadata.meta == null)
				{
					vCartItemProductInfoMetadata.meta = new vCartItemProductInfoMetadata();
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
				meta.AddTypeMap("CartId", new esTypeMap("uniqueidentifier", "System.Guid"));
				meta.AddTypeMap("ProductId", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("Quantity", new esTypeMap("int", "System.Int32"));
				meta.AddTypeMap("ProductFieldData", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("CreatedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("ModifiedOn", new esTypeMap("datetime", "System.DateTime"));
				meta.AddTypeMap("MainPhotoFilename", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ProductName", new esTypeMap("nvarchar", "System.String"));
				meta.AddTypeMap("ProductSlug", new esTypeMap("varchar", "System.String"));
				meta.AddTypeMap("ProductWeight", new esTypeMap("decimal", "System.Decimal"));
				meta.AddTypeMap("ProductShippingAdditionalFeePerItem", new esTypeMap("money", "System.Decimal"));
				meta.AddTypeMap("ProductDeliveryMethodId", new esTypeMap("smallint", "System.Int16"));
				meta.AddTypeMap("ProductIsTaxable", new esTypeMap("bit", "System.Boolean"));			
				
				
				
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifier = provider.Attributes["objectQualifier"];

				if ((objectQualifier != string.Empty) && (objectQualifier.EndsWith("_") == false))
				{
					objectQualifier += "_";
				}

				if (objectQualifier != string.Empty)
				{
					meta.Source = objectQualifier + "vDNNspot_Store_CartItemProductInfo";
					meta.Destination = objectQualifier + "vDNNspot_Store_CartItemProductInfo";
					
					meta.spInsert = objectQualifier + "proc_vDNNspot_Store_CartItemProductInfoInsert";				
					meta.spUpdate = objectQualifier + "proc_vDNNspot_Store_CartItemProductInfoUpdate";		
					meta.spDelete = objectQualifier + "proc_vDNNspot_Store_CartItemProductInfoDelete";
					meta.spLoadAll = objectQualifier + "proc_vDNNspot_Store_CartItemProductInfoLoadAll";
					meta.spLoadByPrimaryKey = objectQualifier + "proc_vDNNspot_Store_CartItemProductInfoLoadByPrimaryKey";
				}
				else
				{
					meta.Source = "vDNNspot_Store_CartItemProductInfo";
					meta.Destination = "vDNNspot_Store_CartItemProductInfo";
									
					meta.spInsert = "proc_vDNNspot_Store_CartItemProductInfoInsert";				
					meta.spUpdate = "proc_vDNNspot_Store_CartItemProductInfoUpdate";		
					meta.spDelete = "proc_vDNNspot_Store_CartItemProductInfoDelete";
					meta.spLoadAll = "proc_vDNNspot_Store_CartItemProductInfoLoadAll";
					meta.spLoadByPrimaryKey = "proc_vDNNspot_Store_CartItemProductInfoLoadByPrimaryKey";
				}
				
				
				this.m_providerMetadataMaps["esDefault"] = meta;
			}
			
			return this.m_providerMetadataMaps["esDefault"];
		}

		#endregion

		static private vCartItemProductInfoMetadata meta;
		static protected Dictionary<string, MapToMeta> mapDelegates;
		static private int _esDefault = RegisterDelegateesDefault();
	}
}
