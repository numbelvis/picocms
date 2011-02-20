﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace picocms.data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="numbelvi_JamesAutoService")]
	public partial class picoDataContextDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertContentRegion(ContentRegion instance);
    partial void UpdateContentRegion(ContentRegion instance);
    partial void DeleteContentRegion(ContentRegion instance);
    #endregion
		
		public picoDataContextDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public picoDataContextDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public picoDataContextDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public picoDataContextDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<ContentRegion> ContentRegions
		{
			get
			{
				return this.GetTable<ContentRegion>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ContentRegion")]
	public partial class ContentRegion : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _ContentRegionID;
		
		private string _Key;
		
		private string _Text;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnContentRegionIDChanging(System.Guid value);
    partial void OnContentRegionIDChanged();
    partial void OnKeyChanging(string value);
    partial void OnKeyChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    #endregion
		
		public ContentRegion()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ContentRegionID", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
		public System.Guid ContentRegionID
		{
			get
			{
				return this._ContentRegionID;
			}
			set
			{
				if ((this._ContentRegionID != value))
				{
					this.OnContentRegionIDChanging(value);
					this.SendPropertyChanging();
					this._ContentRegionID = value;
					this.SendPropertyChanged("ContentRegionID");
					this.OnContentRegionIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Key]", Storage="_Key", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string Key
		{
			get
			{
				return this._Key;
			}
			set
			{
				if ((this._Key != value))
				{
					this.OnKeyChanging(value);
					this.SendPropertyChanging();
					this._Key = value;
					this.SendPropertyChanged("Key");
					this.OnKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
