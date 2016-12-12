using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RAP.Core.DataModels;
using RAP.Core.Common;

namespace RAP.DAL
{
    public class AccountManagementDBHandler
    {
        private readonly string _connString;
        CommonDBHandler commondbHandler = new CommonDBHandler();
        Random random = new Random();
        public AccountManagementDBHandler()
        {
            _connString =  ConfigurationManager.AppSettings["RAPDBConnectionString"];
        }
        #region "Get"
        /// <summary>
        /// Get customer information
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<CustomerInfo> GetCustomer(CustomerInfo message)
        {
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            try
            {
                // CustomerInfo custinfo ;
                //System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == message.email && x.Password == message.Password).FirstOrDefault();


                    if (custdetails != null)
                    {
                        message.User.UserID = (int)custdetails.UserID;
                        message.email = custdetails.Email;
                        message.custID = custdetails.CustomerID;
                        var notifications = db.NotificationPreferences.Where(x => x.UserID == message.User.UserID)
                                                                .Select(c => new CustomerInfo()
                                                                {
                                                                    EmailNotificationFlag = c.EmailNotification,
                                                                    MailNotificationFlag = c.MailNotification
                                                                }).FirstOrDefault();
                        if (notifications != null)
                        {
                            message.MailNotificationFlag = notifications.MailNotificationFlag;
                            message.EmailNotificationFlag = notifications.EmailNotificationFlag;
                        }
                    }
                    else
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.AuthenticationFailed };
                        return result;
                    }
                }
                if (message != null)
                {
                    ReturnResult<UserInfoM> resultUserInfo = commondbHandler.GetUserInfo(message.User.UserID);
                    message.User = resultUserInfo.result;
                }
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started"); 
                result.result = message;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Error Occured" + "Message" + ex.Message + "StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Update/change Password 
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<CustomerInfo> ChangePassword(CustomerInfo message)
        {
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            try
            {
                // CustomerInfo custinfo ;
                //System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == message.email && x.CustomerID == message.custID).FirstOrDefault();
                    if(custdetails != null)
                    {
                        custdetails.Password = message.Password;
                        db.SubmitChanges();
                    }
                    else
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.AuthenticationFailed };
                        return result;
                    }
                }
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
                result.result = message;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Error Occured" + "Message" + ex.Message + "StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Resend Pin
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<Int32> ResendPin(CustomerInfo message)
        {
            ReturnResult<Int32> result = new ReturnResult<Int32>();
            try
            {
                // CustomerInfo custinfo ;
                //System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == message.email && x.CustomerID == message.custID).FirstOrDefault();
                    if (custdetails != null)
                    {
                        result.result = (int)custdetails.CustomerIdentityKey;
                    }
                    else
                    {
                        result.result = 0;
                        result.status = new OperationStatus() { Status = StatusEnum.AuthenticationFailed };
                        return result;
                    }
                }
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
               // result.result = message;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Error Occured" + "Message" + ex.Message + "StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Get City user information
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<CityUserAccount_M> GetCityUser(CityUserAccount_M message)
        {
            ReturnResult<CityUserAccount_M> result = new ReturnResult<CityUserAccount_M>();
            try
            {
                CityUserAccount_M cityUser = new CityUserAccount_M();
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    

                    var cityDetails = db.CityUserAccounts.Where(x => x.Email == message.Email && x.Password == message.Password
                                                                    && x.CityAccountTypeID == message.AccountType.AccountTypeID).FirstOrDefault();


                    if (cityDetails != null)
                    {
                        cityUser.UserID = (int)cityDetails.CityUserID;
                        cityUser.AccountType = message.AccountType;
                        cityUser.FirstName = cityDetails.FirstName;
                        cityUser.LastName = cityDetails.LastName;
                        cityUser.MobilePhoneNumber = cityDetails.MobilePhoneNumber;
                        cityUser.OfficePhoneNumber = cityDetails.OfficePhoneNumber;
                        cityUser.OfficeLocation = cityDetails.OfficeLocation;
                        cityUser.Title = cityDetails.Title;
                        cityUser.Department = cityDetails.Department;
                        cityUser.CreatedDate = cityDetails.CreatedDate;
                        cityUser.Email = cityDetails.Email;
                        cityUser.EmployeeID =(int) cityDetails.EmployeeID;
                        cityUser.IsAnalyst = cityDetails.IsAnalyst;
                        cityUser.IsHearingOfficer = cityDetails.IsHearingOfficer;
                        
                    }
                    else
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.AuthenticationFailed };
                        return result;
                    }
                }
                // System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started"); 
                result.result = cityUser;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                //System.Diagnostics.EventLog.WriteEntry("Application", "Error Occured" + "Message" + ex.Message + "StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Get third party users
        /// </summary>
        /// <returns>Third party details</returns>
        public ReturnResult<List<ThirdPartyDetails>> GetAuthorizedUsers(int custID)
        {
            ReturnResult<List<ThirdPartyDetails>> result = new ReturnResult<List<ThirdPartyDetails>>();

            try
            {
                List<ThirdPartyDetails> thirdPartyDetails;
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var custdetails = db.ThirdPartyRepresentations.Where(x => x.CustomerID == custID)
                                                            .Select(c => new ThirdPartyDetails()
                                                            {
                                                                ThirdPartyRepresentationID = c.ThirdPartyCustomerID,
                                                                //  UserID = (int)c.UserID,
                                                                //  custID = (int)c.CustomerID
                                                            }).FirstOrDefault();
                    var query =
                        db.ThirdPartyRepresentations.AsEnumerable().Join(db.CustomerDetails.AsEnumerable(),
                        t => t.ThirdPartyCustomerID,
                        c => c.CustomerID,
                        (t, c) => new
                        {
                            ID = t.ThirdPartyRepresentationID,
                            CustomerID = t.ThirdPartyCustomerID,
                            //NEW-RAP-TBD
                            //FirstName = c.FirstName,
                            //LastName = c.LastName,
                            //email = c.email
                        });


                    thirdPartyDetails = new List<ThirdPartyDetails>();
                    int index = 0;

                    foreach (var CustomerDetails in query)
                    {
                        ThirdPartyDetails obj = new ThirdPartyDetails();
                        obj.ThirdPartyRepresentationID = CustomerDetails.ID;
                        obj.custID = CustomerDetails.CustomerID;
                        //NEW-RAP-TBD
                        //obj.FirstName = CustomerDetails.FirstName;
                        //obj.LastName = CustomerDetails.LastName;
                        //obj.email = CustomerDetails.email;

                        thirdPartyDetails.Add(obj);
                        index++;
                    }
                }
                result.result = thirdPartyDetails;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<List<AccountType>> GetAccountTypes()
        {
            System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetAccountTypes started");
            ReturnResult<List<AccountType>> result = new ReturnResult<List<AccountType>>();

            try
            {
                List<AccountType> accountTypes = new List<AccountType>();
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var accountTypesDB = db.CityAccountTypes.ToList();


                    foreach (var item in accountTypesDB)
                    {
                        AccountType obj = new AccountType();
                        obj.AccountTypeID = item.CityAccountTypeID;
                        obj.AccountTypeDesc = item.CityAccountTypeDesc;
                        accountTypes.Add(obj);                        
                    }
                }
                result.result = accountTypes;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetAccountTypes exception : " + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<StateM>> GetStateList()
        {
            System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetStateList started");
            ReturnResult<List<StateM>> result = new ReturnResult<List<StateM>>();

            try
            {
                List<StateM> states = new List<StateM>();
                using (CommonDataContext db = new CommonDataContext(_connString))
                {
                    var stateDB = db.States.ToList();


                    foreach (var item in stateDB)
                    {
                        StateM obj = new StateM();
                        obj.StateID = item.StateID;
                        obj.StateCode = item.StateCode;
                        obj.StateName = item.StateName;
                        states.Add(obj);
                    }
                }
                result.result = states;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
               
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetStateList exception : " + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<SearchResult> GetAccountSearch(AccountSearch accountSearch)
        {
            ReturnResult<SearchResult> result = new ReturnResult<SearchResult>();

            try
            {
                SearchResult searchResult = new SearchResult();
                //List<CustomerInfo accounts = new CustomerInfo();
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    
                    string errorMessage = "";
                    int? TotalCount = 0;

                    var Resultdb = db.USP_SearchAccount_Get(accountSearch.AccountType.AccountTypeID,
                        accountSearch.FirstName,accountSearch.LastName, accountSearch.Email, accountSearch.APNNumber,
                        accountSearch.FromDate, accountSearch.ToDate,
                         accountSearch.AddressLine1, accountSearch.AddressLine2, accountSearch.City, 
                         accountSearch.Zip, accountSearch.PhoneNumber,
                         accountSearch.SortBy, accountSearch.SortReverse,
                        accountSearch.PageSize, accountSearch.CurrentPage, ref TotalCount, ref errorMessage );
                    
                    
                    foreach (var item in Resultdb)
                    {
                        SearchResultCustomerInfo account = new SearchResultCustomerInfo();
                        account.custID = (int)item.CustomerID;
                        account.email = item.Email;
                        account.AccountType = item.AcctTypeDesc;
                        account.CreatedDate = Convert.ToDateTime(item.CreatedDate);
                        account.Name = item.Name;
                        account.RankNo = (int)item.RankNo;
                        searchResult.List.Add(account);
                        
                    }
                   
                    
                    searchResult.PageSize = accountSearch.PageSize;
                    searchResult.SortBy = accountSearch.SortBy;
                    searchResult.SortReverse = accountSearch.SortReverse;
                    searchResult.CurrentPage = accountSearch.CurrentPage;
                    searchResult.TotalCount = (int)TotalCount;
                }
               
                result.result = searchResult;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        #endregion  
        /// <summary>
        /// Search third party
        /// </summary>
        /// <returns>true or false</returns>
        public ReturnResult<CustomerInfo> SearchInviteThirdPartyUser(String message)
        {
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            try
            {
                CustomerInfo custinfo;
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == message)
                                                            .FirstOrDefault();
                    if (custdetails != null)
                    {
                        custinfo = new CustomerInfo();
                        //custinfo.FirstName = custdetails.FirstName;
                        //custinfo.LastName = custdetails.LastName;
                        custinfo.User.UserID = (int)custdetails.UserID;
                        custinfo.email = custdetails.Email;
                        custinfo.custID = custdetails.CustomerID;
                    }
                    else
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.NoDataFound };
                        return result;
                    }
                }
                using (CommonDataContext db = new CommonDataContext(_connString))
                {

                    var userinfos = db.UserInfos.Where(x => x.UserID == custinfo.User.UserID)
                                                            .Select(c => new UserInfoM()
                                                            {
                                                                FirstName = c.FirstName,
                                                                LastName = c.LastName,
                                                            }).FirstOrDefault();

                    if (userinfos != null)
                    {
                        custinfo.User.FirstName = userinfos.FirstName;
                        custinfo.User.LastName = userinfos.LastName;
                    }
                }
                result.result = custinfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Authorize third party
        /// </summary>
        /// <returns>true or false</returns>
        public ReturnResult<bool> AuthorizeThirdPartyUser(int CustID, int thirdpartyCustID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
               // CustomerInfo custinfo;
                
                    using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                    {

                        ThirdPartyRepresentation thirdpartyTable = new ThirdPartyRepresentation();
                        thirdpartyTable.CustomerID = CustID;
                        thirdpartyTable.ThirdPartyCustomerID = thirdpartyCustID;
                        thirdpartyTable.CreatedDate = DateTime.Now;

                        db.ThirdPartyRepresentations.InsertOnSubmit(thirdpartyTable);
                        db.SubmitChanges();
                    }
                    result.result = true;
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                    return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }


        /// <summary>
        /// Remove third party
        /// </summary>
        /// <returns>true or false</returns>
        public ReturnResult<bool> RemoveThirdParty(int CustID, int thirdPartyRepresentationID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                // CustomerInfo custinfo;

                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    ThirdPartyRepresentation thirdpartyTable = db.ThirdPartyRepresentations.First(i => i.ThirdPartyRepresentationID == thirdPartyRepresentationID);
                    thirdpartyTable.IsDeleted = true;
                    thirdpartyTable.ModifiedDate = DateTime.Now;
                   // thirdpartyTable.ThirdPartyCustomerID = thirdPartyRepresentationID;
                    db.SubmitChanges();
                  //  db.ThirdPartyRepresentations.InsertOnSubmit(thirdpartyTable);
                   // db.SubmitChanges();
                }

                result.result = true;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                    return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Check if City User/Admin exists or not
        /// </summary>
        /// <returns>true or false</returns>
        public bool CheckCityUser(string Email)
        {
            try
            {
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var cityUser = db.CityUserAccounts.Where(x => x.Email == Email)
                                    .Select(c => new CustomerInfo()
                                    {
                                        custID = c.CityUserID
                                    }).FirstOrDefault();
                    if (cityUser != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Check if customer exists or not
        /// </summary>
        /// <returns>true or false</returns>
        public bool CheckCustAccount(CustomerInfo message)
        {
            try
            {               
                 using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var custInfo = db.CustomerDetails.Where(x => x.Email == message.email)
                                    .Select(c => new CustomerInfo()
                                    {
                                        custID = c.CustomerID,
                                    }).FirstOrDefault();
                    if (custInfo != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #region "Save"
        /// <summary>
        /// Save cutomer information
        /// </summary>
        /// <returns>true or false</returns>
        public ReturnResult<CustomerInfo> SaveCustomer(CustomerInfo message)
       {
           ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
           ReturnResult<UserInfoM> UserResult = new ReturnResult<UserInfoM>();
           try
           {
             //  System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveCustomer started");
               // Account already exists
               if (CheckCustAccount(message))
               {
                   result.status = new OperationStatus() { Status = StatusEnum.AccountAlreadyExist };
                   return result;
               }

               UserResult = commondbHandler.SaveUserInfo(message.User);
               if (UserResult.status.Status != StatusEnum.Success)
               {
                   result.status.Status = UserResult.status.Status;
                   return result;
               }
               message.User = UserResult.result;                
               
               using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
               {

                   CustomerDetail custTable = new CustomerDetail();
                   custTable.Email = message.email;
                   custTable.Password = message.Password;
                   custTable.UserID = message.User.UserID;  
                   custTable.CreatedDate = DateTime.Now;
                   custTable.ModifiedDate = DateTime.Now;
                   custTable.bMailingAddress = !message.IsSameMailingAddress;
                   message.CustomerIdentityKey = getCustomerIdentityKey();
                   custTable.CustomerIdentityKey = message.CustomerIdentityKey;
                   db.CustomerDetails.InsertOnSubmit(custTable);
                   db.SubmitChanges();
                   message.custID = custTable.CustomerID;

                   NotificationPreference notificationTable = new NotificationPreference();
                   notificationTable.UserID = message.User.UserID;  
                   notificationTable.EmailNotification = message.EmailNotificationFlag;
                   notificationTable.MailNotification = message.MailNotificationFlag;
                   notificationTable.CreatedDate = DateTime.Now;
                   db.NotificationPreferences.InsertOnSubmit(notificationTable);
                   db.SubmitChanges();

                   if (!message.IsSameMailingAddress)
                   {
                       MailingAddress mailing = new MailingAddress();
                       mailing.AddressLine1 = message.MailingAddress.AddressLine1;
                       mailing.AddressLine2 = message.MailingAddress.AddressLine2;
                       mailing.City = message.MailingAddress.City;
                       mailing.StateID = message.MailingAddress.State.StateID;
                       mailing.Zip = message.MailingAddress.Zip;
                       mailing.PhoneNumber = message.MailingAddress.PhoneNumber;
                       mailing.CustomerID = message.custID;
                       mailing.LastModifiedDate = DateTime.Now;
                       db.MailingAddresses.InsertOnSubmit(mailing);
                       db.SubmitChanges();
                   }
                   
               }
             //  System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveCustomer completed");
               result.status = new OperationStatus() { Status = StatusEnum.Success };
               result.result = message;
               return result;
           }
           catch(Exception ex)
           {
              // System.Diagnostics.EventLog.WriteEntry("Application", "Error : " + ex.Message + "StackTrace" + ex.StackTrace.ToString());
              IExceptionHandler eHandler = new ExceptionHandler();
               result.status = eHandler.HandleException(ex);
               return result;
           }
       }

        /// <summary>
        /// Edit cutomer information
        /// </summary>
        /// <returns>true or false</returns>
        public ReturnResult<CustomerInfo> EditCustomer(CustomerInfo message)
        {
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            ReturnResult<UserInfoM> UserResult = new ReturnResult<UserInfoM>();
            CommonDBHandler commondb = new CommonDBHandler();
            try
            {
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    CustomerDetail custTable = db.CustomerDetails.Where(x => x.CustomerID == message.custID).FirstOrDefault();
                    if (custTable != null)
                    {
                        commondb.EditUserInfo(message.User);

                        custTable.Email = message.email;
                        custTable.Password = message.Password;
                        custTable.UserID = message.User.UserID;
                        custTable.ModifiedDate = DateTime.Now;
                        custTable.bMailingAddress = !message.IsSameMailingAddress;
                        message.CustomerIdentityKey = getCustomerIdentityKey();
                        custTable.CustomerIdentityKey = message.CustomerIdentityKey;
                        db.SubmitChanges();

                        NotificationPreference notificationTable = db.NotificationPreferences.Where(x => x.UserID == message.User.UserID).FirstOrDefault();
                        notificationTable.EmailNotification = message.EmailNotificationFlag;
                        notificationTable.MailNotification = message.MailNotificationFlag;
                        db.SubmitChanges();

                        if (!message.IsSameMailingAddress)
                        {
                            MailingAddress mailing = db.MailingAddresses.Where(x => x.CustomerID == message.custID).FirstOrDefault();
                            mailing.AddressLine1 = message.MailingAddress.AddressLine1;
                            mailing.AddressLine2 = message.MailingAddress.AddressLine2;
                            mailing.City = message.MailingAddress.City;
                            mailing.StateID = message.MailingAddress.State.StateID;
                            mailing.Zip = message.MailingAddress.Zip;
                            mailing.PhoneNumber = message.MailingAddress.PhoneNumber;
                            mailing.CustomerID = message.custID;
                            mailing.LastModifiedDate = DateTime.Now;
                            db.SubmitChanges();
                        }
                    }
                }
                //  System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveCustomer completed");
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                result.result = message;
                return result;
            }
            catch (Exception ex)
            {
                // System.Diagnostics.EventLog.WriteEntry("Application", "Error : " + ex.Message + "StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<CityUserAccount_M> CreateCityUserAccount(CityUserAccount_M message)
        {
            ReturnResult<CityUserAccount_M> result = new ReturnResult<CityUserAccount_M>();
            ReturnResult<CityUserAccount_M> UserResult = new ReturnResult<CityUserAccount_M>();
            try
            {
                //  System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveCustomer started");
                // Account already exists
                if (CheckCityUser(message.Email))
                {
                    result.status = new OperationStatus() { Status = StatusEnum.AccountAlreadyExist };
                    return result;
                }

                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    CityUserAccount cityUserTable = new CityUserAccount();
                    cityUserTable.CityAccountTypeID = (int)message.AccountType.AccountTypeID;
                    cityUserTable.FirstName = message.FirstName;
                    cityUserTable.LastName = message.LastName;
                    cityUserTable.Password = message.Password;
                    cityUserTable.Email = message.Email;
                    cityUserTable.EmployeeID = (int)message.EmployeeID;
                    cityUserTable.IsAnalyst = Convert.ToBoolean(message.IsAnalyst);
                    cityUserTable.IsHearingOfficer = Convert.ToBoolean(message.IsHearingOfficer);
                    cityUserTable.Title = message.Title;
                    cityUserTable.Department = message.Department;
                    cityUserTable.OfficePhoneNumber = message.OfficePhoneNumber;
                    cityUserTable.MobilePhoneNumber = message.MobilePhoneNumber;
                    cityUserTable.OfficeLocation = message.OfficeLocation;
                    cityUserTable.CreatedDate = DateTime.Now;
                    db.CityUserAccounts.InsertOnSubmit(cityUserTable);
                    db.SubmitChanges();
                    message.UserID = cityUserTable.CityUserID;                  
                }
                //  System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveCustomer completed");
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                result.result = message;
                return result;
            }
            catch (Exception ex)
            {
                // System.Diagnostics.EventLog.WriteEntry("Application", "Error : " + ex.Message + "StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        private Int32 getCustomerIdentityKey()
        {
            int randomNo = 0;
            int CustomerIdentityKeyFrom = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CustomerIdentityKeyFrom"]) ? 100000000 : Convert.ToInt32(ConfigurationManager.AppSettings["CustomerIdentityKeyFrom"]);
            int CustomerIdentityKeyTo = string.IsNullOrEmpty(ConfigurationManager.AppSettings["CustomerIdentityKeyTo"]) ? 999999999 : Convert.ToInt32(ConfigurationManager.AppSettings["CustomerIdentityKeyTo"]);
            using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
            {
                var CustomerIdentityKeys = from r in db.CustomerDetails select r.CustomerIdentityKey;
                if (CustomerIdentityKeys != null)
                {
                    for (int i = 0; i < CustomerIdentityKeys.Count(); i++)
                    {
                        randomNo = random.Next(CustomerIdentityKeyFrom, CustomerIdentityKeyTo);
                        if (CustomerIdentityKeys.Contains(randomNo))
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return randomNo;
        }
        #endregion

    }
}
