﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RAP.DAL
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="OAKRAP")]
	public partial class OAKRAPDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCustomerDetail(CustomerDetail instance);
    partial void UpdateCustomerDetail(CustomerDetail instance);
    partial void DeleteCustomerDetail(CustomerDetail instance);
    partial void InsertUserType(UserType instance);
    partial void UpdateUserType(UserType instance);
    partial void DeleteUserType(UserType instance);
    partial void InsertThirdPartyRepresentation(ThirdPartyRepresentation instance);
    partial void UpdateThirdPartyRepresentation(ThirdPartyRepresentation instance);
    partial void DeleteThirdPartyRepresentation(ThirdPartyRepresentation instance);
    partial void InsertNotificationPreference(NotificationPreference instance);
    partial void UpdateNotificationPreference(NotificationPreference instance);
    partial void DeleteNotificationPreference(NotificationPreference instance);
    #endregion
		
		public OAKRAPDataContext() : 
				base(global::RAP.DAL.Properties.Settings.Default.OAKRAPConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public OAKRAPDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OAKRAPDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OAKRAPDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public OAKRAPDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CustomerDetail> CustomerDetails
		{
			get
			{
				return this.GetTable<CustomerDetail>();
			}
		}
		
		public System.Data.Linq.Table<UserType> UserTypes
		{
			get
			{
				return this.GetTable<UserType>();
			}
		}
		
		public System.Data.Linq.Table<ThirdPartyRepresentation> ThirdPartyRepresentations
		{
			get
			{
				return this.GetTable<ThirdPartyRepresentation>();
			}
		}
		
		public System.Data.Linq.Table<NotificationPreference> NotificationPreferences
		{
			get
			{
				return this.GetTable<NotificationPreference>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.CustomerDetails")]
	public partial class CustomerDetail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CustomerID;
		
		private string _FirstName;
		
		private string _LastName;
		
		private string _PhoneNumber;
		
		private string _email;
		
		private string _AddressLine1;
		
		private string _AddressLine2;
		
		private string _City;
		
		private string _State;
		
		private string _Zip;
		
		private System.Nullable<int> _UserTypeID;
		
		private string _Password;
		
		private System.Nullable<System.DateTime> _CreatedDate;
		
		private System.Nullable<bool> _EmailNotificationFlag;
		
		private EntityRef<ThirdPartyRepresentation> _ThirdPartyRepresentation;
		
		private EntitySet<ThirdPartyRepresentation> _ThirdPartyRepresentations;
		
		private EntityRef<NotificationPreference> _NotificationPreference;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCustomerIDChanging(int value);
    partial void OnCustomerIDChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnPhoneNumberChanging(string value);
    partial void OnPhoneNumberChanged();
    partial void OnemailChanging(string value);
    partial void OnemailChanged();
    partial void OnAddressLine1Changing(string value);
    partial void OnAddressLine1Changed();
    partial void OnAddressLine2Changing(string value);
    partial void OnAddressLine2Changed();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnStateChanging(string value);
    partial void OnStateChanged();
    partial void OnZipChanging(string value);
    partial void OnZipChanged();
    partial void OnUserTypeIDChanging(System.Nullable<int> value);
    partial void OnUserTypeIDChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnCreatedDateChanged();
    partial void OnEmailNotificationFlagChanging(System.Nullable<bool> value);
    partial void OnEmailNotificationFlagChanged();
    #endregion
		
		public CustomerDetail()
		{
			this._ThirdPartyRepresentation = default(EntityRef<ThirdPartyRepresentation>);
			this._ThirdPartyRepresentations = new EntitySet<ThirdPartyRepresentation>(new Action<ThirdPartyRepresentation>(this.attach_ThirdPartyRepresentations), new Action<ThirdPartyRepresentation>(this.detach_ThirdPartyRepresentations));
			this._NotificationPreference = default(EntityRef<NotificationPreference>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				if ((this._CustomerID != value))
				{
					this.OnCustomerIDChanging(value);
					this.SendPropertyChanging();
					this._CustomerID = value;
					this.SendPropertyChanged("CustomerID");
					this.OnCustomerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhoneNumber", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string PhoneNumber
		{
			get
			{
				return this._PhoneNumber;
			}
			set
			{
				if ((this._PhoneNumber != value))
				{
					this.OnPhoneNumberChanging(value);
					this.SendPropertyChanging();
					this._PhoneNumber = value;
					this.SendPropertyChanged("PhoneNumber");
					this.OnPhoneNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email", DbType="VarChar(35) NOT NULL", CanBeNull=false)]
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this.OnemailChanging(value);
					this.SendPropertyChanging();
					this._email = value;
					this.SendPropertyChanged("email");
					this.OnemailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddressLine1", DbType="VarChar(25) NOT NULL", CanBeNull=false)]
		public string AddressLine1
		{
			get
			{
				return this._AddressLine1;
			}
			set
			{
				if ((this._AddressLine1 != value))
				{
					this.OnAddressLine1Changing(value);
					this.SendPropertyChanging();
					this._AddressLine1 = value;
					this.SendPropertyChanged("AddressLine1");
					this.OnAddressLine1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddressLine2", DbType="VarChar(25)")]
		public string AddressLine2
		{
			get
			{
				return this._AddressLine2;
			}
			set
			{
				if ((this._AddressLine2 != value))
				{
					this.OnAddressLine2Changing(value);
					this.SendPropertyChanging();
					this._AddressLine2 = value;
					this.SendPropertyChanged("AddressLine2");
					this.OnAddressLine2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="VarChar(20)")]
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				if ((this._City != value))
				{
					this.OnCityChanging(value);
					this.SendPropertyChanging();
					this._City = value;
					this.SendPropertyChanged("City");
					this.OnCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="VarChar(20)")]
		public string State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Zip", DbType="VarChar(5)")]
		public string Zip
		{
			get
			{
				return this._Zip;
			}
			set
			{
				if ((this._Zip != value))
				{
					this.OnZipChanging(value);
					this.SendPropertyChanging();
					this._Zip = value;
					this.SendPropertyChanged("Zip");
					this.OnZipChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserTypeID", DbType="Int")]
		public System.Nullable<int> UserTypeID
		{
			get
			{
				return this._UserTypeID;
			}
			set
			{
				if ((this._UserTypeID != value))
				{
					this.OnUserTypeIDChanging(value);
					this.SendPropertyChanging();
					this._UserTypeID = value;
					this.SendPropertyChanged("UserTypeID");
					this.OnUserTypeIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="VarChar(15)")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailNotificationFlag", DbType="Bit")]
		public System.Nullable<bool> EmailNotificationFlag
		{
			get
			{
				return this._EmailNotificationFlag;
			}
			set
			{
				if ((this._EmailNotificationFlag != value))
				{
					this.OnEmailNotificationFlagChanging(value);
					this.SendPropertyChanging();
					this._EmailNotificationFlag = value;
					this.SendPropertyChanged("EmailNotificationFlag");
					this.OnEmailNotificationFlagChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerDetail_ThirdPartyRepresentation", Storage="_ThirdPartyRepresentation", ThisKey="CustomerID", OtherKey="CustomerID", IsUnique=true, IsForeignKey=false)]
		public ThirdPartyRepresentation ThirdPartyRepresentation
		{
			get
			{
				return this._ThirdPartyRepresentation.Entity;
			}
			set
			{
				ThirdPartyRepresentation previousValue = this._ThirdPartyRepresentation.Entity;
				if (((previousValue != value) 
							|| (this._ThirdPartyRepresentation.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ThirdPartyRepresentation.Entity = null;
						previousValue.CustomerDetail = null;
					}
					this._ThirdPartyRepresentation.Entity = value;
					if ((value != null))
					{
						value.CustomerDetail = this;
					}
					this.SendPropertyChanged("ThirdPartyRepresentation");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerDetail_ThirdPartyRepresentation1", Storage="_ThirdPartyRepresentations", ThisKey="CustomerID", OtherKey="ThirdPartyCustomerID")]
		public EntitySet<ThirdPartyRepresentation> ThirdPartyRepresentations
		{
			get
			{
				return this._ThirdPartyRepresentations;
			}
			set
			{
				this._ThirdPartyRepresentations.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerDetail_NotificationPreference", Storage="_NotificationPreference", ThisKey="CustomerID", OtherKey="CustomerID", IsUnique=true, IsForeignKey=false)]
		public NotificationPreference NotificationPreference
		{
			get
			{
				return this._NotificationPreference.Entity;
			}
			set
			{
				NotificationPreference previousValue = this._NotificationPreference.Entity;
				if (((previousValue != value) 
							|| (this._NotificationPreference.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._NotificationPreference.Entity = null;
						previousValue.CustomerDetail = null;
					}
					this._NotificationPreference.Entity = value;
					if ((value != null))
					{
						value.CustomerDetail = this;
					}
					this.SendPropertyChanged("NotificationPreference");
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
		
		private void attach_ThirdPartyRepresentations(ThirdPartyRepresentation entity)
		{
			this.SendPropertyChanging();
			entity.CustomerDetail1 = this;
		}
		
		private void detach_ThirdPartyRepresentations(ThirdPartyRepresentation entity)
		{
			this.SendPropertyChanging();
			entity.CustomerDetail1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserType")]
	public partial class UserType : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserTypeID;
		
		private string _Description;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserTypeIDChanging(int value);
    partial void OnUserTypeIDChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public UserType()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserTypeID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserTypeID
		{
			get
			{
				return this._UserTypeID;
			}
			set
			{
				if ((this._UserTypeID != value))
				{
					this.OnUserTypeIDChanging(value);
					this.SendPropertyChanging();
					this._UserTypeID = value;
					this.SendPropertyChanged("UserTypeID");
					this.OnUserTypeIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="VarChar(20)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ThirdPartyRepresentation")]
	public partial class ThirdPartyRepresentation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ThirdPartyRepresentationID;
		
		private int _CustomerID;
		
		private int _ThirdPartyCustomerID;
		
		private System.Nullable<System.DateTime> _CreatedDate;
		
		private EntityRef<CustomerDetail> _CustomerDetail;
		
		private EntityRef<CustomerDetail> _CustomerDetail1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnThirdPartyRepresentationIDChanging(int value);
    partial void OnThirdPartyRepresentationIDChanged();
    partial void OnCustomerIDChanging(int value);
    partial void OnCustomerIDChanged();
    partial void OnThirdPartyCustomerIDChanging(int value);
    partial void OnThirdPartyCustomerIDChanged();
    partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnCreatedDateChanged();
    #endregion
		
		public ThirdPartyRepresentation()
		{
			this._CustomerDetail = default(EntityRef<CustomerDetail>);
			this._CustomerDetail1 = default(EntityRef<CustomerDetail>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ThirdPartyRepresentationID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int ThirdPartyRepresentationID
		{
			get
			{
				return this._ThirdPartyRepresentationID;
			}
			set
			{
				if ((this._ThirdPartyRepresentationID != value))
				{
					this.OnThirdPartyRepresentationIDChanging(value);
					this.SendPropertyChanging();
					this._ThirdPartyRepresentationID = value;
					this.SendPropertyChanged("ThirdPartyRepresentationID");
					this.OnThirdPartyRepresentationIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				if ((this._CustomerID != value))
				{
					if (this._CustomerDetail.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCustomerIDChanging(value);
					this.SendPropertyChanging();
					this._CustomerID = value;
					this.SendPropertyChanged("CustomerID");
					this.OnCustomerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ThirdPartyCustomerID", DbType="Int NOT NULL")]
		public int ThirdPartyCustomerID
		{
			get
			{
				return this._ThirdPartyCustomerID;
			}
			set
			{
				if ((this._ThirdPartyCustomerID != value))
				{
					if (this._CustomerDetail1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnThirdPartyCustomerIDChanging(value);
					this.SendPropertyChanging();
					this._ThirdPartyCustomerID = value;
					this.SendPropertyChanged("ThirdPartyCustomerID");
					this.OnThirdPartyCustomerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerDetail_ThirdPartyRepresentation", Storage="_CustomerDetail", ThisKey="CustomerID", OtherKey="CustomerID", IsForeignKey=true)]
		public CustomerDetail CustomerDetail
		{
			get
			{
				return this._CustomerDetail.Entity;
			}
			set
			{
				CustomerDetail previousValue = this._CustomerDetail.Entity;
				if (((previousValue != value) 
							|| (this._CustomerDetail.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CustomerDetail.Entity = null;
						previousValue.ThirdPartyRepresentation = null;
					}
					this._CustomerDetail.Entity = value;
					if ((value != null))
					{
						value.ThirdPartyRepresentation = this;
						this._CustomerID = value.CustomerID;
					}
					else
					{
						this._CustomerID = default(int);
					}
					this.SendPropertyChanged("CustomerDetail");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerDetail_ThirdPartyRepresentation1", Storage="_CustomerDetail1", ThisKey="ThirdPartyCustomerID", OtherKey="CustomerID", IsForeignKey=true)]
		public CustomerDetail CustomerDetail1
		{
			get
			{
				return this._CustomerDetail1.Entity;
			}
			set
			{
				CustomerDetail previousValue = this._CustomerDetail1.Entity;
				if (((previousValue != value) 
							|| (this._CustomerDetail1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CustomerDetail1.Entity = null;
						previousValue.ThirdPartyRepresentations.Remove(this);
					}
					this._CustomerDetail1.Entity = value;
					if ((value != null))
					{
						value.ThirdPartyRepresentations.Add(this);
						this._ThirdPartyCustomerID = value.CustomerID;
					}
					else
					{
						this._ThirdPartyCustomerID = default(int);
					}
					this.SendPropertyChanged("CustomerDetail1");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.NotificationPreference")]
	public partial class NotificationPreference : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _NotificationPreferenceID;
		
		private int _CustomerID;
		
		private bool _EmailNotification;
		
		private bool _MailNotification;
		
		private System.Nullable<System.DateTime> _CreatedDate;
		
		private EntityRef<CustomerDetail> _CustomerDetail;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnNotificationPreferenceIDChanging(int value);
    partial void OnNotificationPreferenceIDChanged();
    partial void OnCustomerIDChanging(int value);
    partial void OnCustomerIDChanged();
    partial void OnEmailNotificationChanging(bool value);
    partial void OnEmailNotificationChanged();
    partial void OnMailNotificationChanging(bool value);
    partial void OnMailNotificationChanged();
    partial void OnCreatedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnCreatedDateChanged();
    #endregion
		
		public NotificationPreference()
		{
			this._CustomerDetail = default(EntityRef<CustomerDetail>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NotificationPreferenceID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int NotificationPreferenceID
		{
			get
			{
				return this._NotificationPreferenceID;
			}
			set
			{
				if ((this._NotificationPreferenceID != value))
				{
					this.OnNotificationPreferenceIDChanging(value);
					this.SendPropertyChanging();
					this._NotificationPreferenceID = value;
					this.SendPropertyChanged("NotificationPreferenceID");
					this.OnNotificationPreferenceIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CustomerID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int CustomerID
		{
			get
			{
				return this._CustomerID;
			}
			set
			{
				if ((this._CustomerID != value))
				{
					if (this._CustomerDetail.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCustomerIDChanging(value);
					this.SendPropertyChanging();
					this._CustomerID = value;
					this.SendPropertyChanged("CustomerID");
					this.OnCustomerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EmailNotification", DbType="Bit NOT NULL")]
		public bool EmailNotification
		{
			get
			{
				return this._EmailNotification;
			}
			set
			{
				if ((this._EmailNotification != value))
				{
					this.OnEmailNotificationChanging(value);
					this.SendPropertyChanging();
					this._EmailNotification = value;
					this.SendPropertyChanged("EmailNotification");
					this.OnEmailNotificationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MailNotification", DbType="Bit NOT NULL")]
		public bool MailNotification
		{
			get
			{
				return this._MailNotification;
			}
			set
			{
				if ((this._MailNotification != value))
				{
					this.OnMailNotificationChanging(value);
					this.SendPropertyChanging();
					this._MailNotification = value;
					this.SendPropertyChanged("MailNotification");
					this.OnMailNotificationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreatedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				if ((this._CreatedDate != value))
				{
					this.OnCreatedDateChanging(value);
					this.SendPropertyChanging();
					this._CreatedDate = value;
					this.SendPropertyChanged("CreatedDate");
					this.OnCreatedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="CustomerDetail_NotificationPreference", Storage="_CustomerDetail", ThisKey="CustomerID", OtherKey="CustomerID", IsForeignKey=true)]
		public CustomerDetail CustomerDetail
		{
			get
			{
				return this._CustomerDetail.Entity;
			}
			set
			{
				CustomerDetail previousValue = this._CustomerDetail.Entity;
				if (((previousValue != value) 
							|| (this._CustomerDetail.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._CustomerDetail.Entity = null;
						previousValue.NotificationPreference = null;
					}
					this._CustomerDetail.Entity = value;
					if ((value != null))
					{
						value.NotificationPreference = this;
						this._CustomerID = value.CustomerID;
					}
					else
					{
						this._CustomerID = default(int);
					}
					this.SendPropertyChanged("CustomerDetail");
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
