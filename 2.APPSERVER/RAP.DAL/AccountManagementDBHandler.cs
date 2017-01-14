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
    public class AccountManagementDBHandler : IAccountManagementDBHandler
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
                        message.IsSameMailingAddress = !Convert.ToBoolean(custdetails.bMailingAddress);

                        if (!message.IsSameMailingAddress)
                        {
                            var mailingaddress = db.MailingAddresses.Where(x => x.CustomerID == message.custID).FirstOrDefault();
                            if (mailingaddress != null)
                            {
                                message.MailingAddress.AddressLine1 = mailingaddress.AddressLine1;
                                message.MailingAddress.AddressLine2 = mailingaddress.AddressLine2;
                                message.MailingAddress.City = mailingaddress.City;
                                message.MailingAddress.PhoneNumber = mailingaddress.PhoneNumber;
                                message.MailingAddress.State.StateID = mailingaddress.StateID;
                                message.MailingAddress.Zip = mailingaddress.Zip;
                                using (CommonDataContext dbCommon = new CommonDataContext(_connString))
                                {
                                    var state = dbCommon.States.Where(x => x.StateID == message.MailingAddress.State.StateID).FirstOrDefault();
                                    message.MailingAddress.State.StateName = state.StateName;
                                    message.MailingAddress.State.StateCode = state.StateCode;
                                }
                            }
                        }
                        
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
        /// Get customer information
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<CustomerInfo> GetCustomer(int CustomerID)
        {
            ReturnResult<CustomerInfo> result = new ReturnResult<CustomerInfo>();
            try
            {
                CustomerInfo message = new CustomerInfo();
                // CustomerInfo custinfo ;
                //System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.CustomerID == CustomerID).FirstOrDefault();


                    if (custdetails != null)
                    {
                        message.User.UserID = (int)custdetails.UserID;
                        message.email = custdetails.Email;
                        message.custID = custdetails.CustomerID;
                        message.IsSameMailingAddress = !Convert.ToBoolean(custdetails.bMailingAddress);

                        if(!message.IsSameMailingAddress)
                        {
                            var mailingaddress = db.MailingAddresses.Where(x => x.CustomerID == message.custID).FirstOrDefault();
                            if(mailingaddress != null)
                            {
                                message.MailingAddress.AddressLine1 = mailingaddress.AddressLine1;
                                message.MailingAddress.AddressLine2 = mailingaddress.AddressLine2;
                                message.MailingAddress.City = mailingaddress.City;
                                message.MailingAddress.PhoneNumber = mailingaddress.PhoneNumber;
                                message.MailingAddress.State.StateID = mailingaddress.StateID;
                                message.MailingAddress.Zip = mailingaddress.Zip;
                            }
                        }
                        using (CommonDataContext dbCommon = new CommonDataContext(_connString))
                        {
                            var state = dbCommon.States.Where(x => x.StateID == message.MailingAddress.State.StateID).FirstOrDefault();
                            message.MailingAddress.State.StateName = state.StateName;
                            message.MailingAddress.State.StateCode = state.StateCode;
                        }

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
        /// Forget Password
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<string> ForgetPwd(string email)
        {
            ReturnResult<string> result = new ReturnResult<string>();
            try
            {
                // CustomerInfo custinfo ;
                //System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == email ).FirstOrDefault();
                    var cityDetails = db.CityUserAccounts.Where(x => x.Email == email).FirstOrDefault();
                    if (custdetails != null)
                    {
                        result.result = custdetails.Password;
                    }
                    else if(cityDetails != null)
                    {
                        result.result = cityDetails.Password;
                    }
                    else
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.EmailDoesnotExist };
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
        /// Resend Pin
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<string> ResendPin(CustomerInfo message)
        {
            ReturnResult<string> result = new ReturnResult<string>();
            try
            {
                // CustomerInfo custinfo ;
                //System.Diagnostics.EventLog.WriteEntry("Application", "DAL GetCustomer started");
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {

                    var custdetails = db.CustomerDetails.Where(x => x.Email == message.email && x.CustomerID == message.custID).FirstOrDefault();
                    if (custdetails != null)
                    {
                        result.result = custdetails.CustomerIdentityKey;
                    }
                    else
                    {
                        result.result = "";
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
                                                                    ).FirstOrDefault();


                    if (cityDetails != null)
                    {
                        AccountType accountType = new AccountType();
                        var AccountTypeDB = db.CityAccountTypes.Where(x => x.CityAccountTypeID == cityDetails.CityAccountTypeID).FirstOrDefault();
                        accountType.AccountTypeID = AccountTypeDB.CityAccountTypeID;
                        accountType.AccountTypeDesc = AccountTypeDB.CityAccountTypeDesc;
                        cityUser.UserID = (int)cityDetails.CityUserID;
                        cityUser.AccountType = accountType;
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
        /// Get City user information
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<CityUserAccount_M> GetCityUser(int UserID)
        {
            ReturnResult<CityUserAccount_M> result = new ReturnResult<CityUserAccount_M>();
            try
            {


                CityUserAccount_M cityUser = new CityUserAccount_M();
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {


                    var cityDetails = db.CityUserAccounts.Where(x => x.CityUserID == UserID).FirstOrDefault();


                    if (cityDetails != null)
                    {
                        cityUser.UserID = (int)cityDetails.CityUserID;
                        //cityUser.AccountType = cityDetails.AccountType;
                        cityUser.FirstName = cityDetails.FirstName;
                        cityUser.LastName = cityDetails.LastName;
                        cityUser.MobilePhoneNumber = cityDetails.MobilePhoneNumber;
                        cityUser.OfficePhoneNumber = cityDetails.OfficePhoneNumber;
                        cityUser.OfficeLocation = cityDetails.OfficeLocation;
                        cityUser.Title = cityDetails.Title;
                        cityUser.Department = cityDetails.Department;
                        cityUser.CreatedDate = cityDetails.CreatedDate;
                        cityUser.Email = cityDetails.Email;
                        cityUser.EmployeeID = (int)cityDetails.EmployeeID;
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
        /// Get Colloborator Users
        /// </summary>
        /// <returns>Collaborator details</returns>
        public ReturnResult<Collaborator> GetAuthorizedUsers(int custID)
        {
            ReturnResult<Collaborator> result = new ReturnResult<Collaborator>();

            try
            {
                Collaborator collaborator = new Collaborator();
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var collaboratorDB = db.CollaboratorAccesses.Where(x => x.CustomerID == custID).ToList();
                    if (collaboratorDB != null)
                    {
                        foreach (var item in collaboratorDB)
                        {
                            ReturnResult<CustomerInfo> collaboratorInfo = GetCustomer((int)item.CollaboratorCustID);
                            if (collaboratorInfo != null)
                            {
                                collaborator.collaboratorDetails.Add(collaboratorInfo.result);
                            }                            
                        }
                        collaborator.custID = custID;
                    }                    
                }
                result.result = collaborator;
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
                    if (db == null)
                    {
                        result.result = null;
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
                        return result;
                    }
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
                //result.status = eHandler.HandleException(ex);
                result.status = new OperationStatus() { Status = StatusEnum.DatabaseException };
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
        public ReturnResult<CustomerInfo> SearchInviteCollaborator(String message)
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
        /// Authorize Collaborator
        /// </summary>
        /// <returns>true or false</returns>
        public ReturnResult<bool> AuthorizeCollaborator(CollaboratorAccessM access)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    foreach(var item in access.caseInfo)
                    {
                        CollaboratorAccess collaborator = new CollaboratorAccess();
                        collaborator.CustomerID = access.custID;
                        collaborator.CollaboratorCustID = access.collaboratorCustID;
                       //ollaborator.C_ID = item;
                        collaborator.CreatedDate = DateTime.Now;

                        db.CollaboratorAccesses.InsertOnSubmit(collaborator);
                        db.SubmitChanges();
                    }
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
                   // thirdpartyTable.IsDeleted = true;
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

        public ReturnResult<bool> SaveOrUpdateThirdPartyInfo(ThirdPartyInfoM model)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    ReturnResult<UserInfoM> thirdPartySaveResult = new ReturnResult<UserInfoM>();
                    thirdPartySaveResult = commondbHandler.SaveUserInfo(model.ThirdPartyUser);
                    if (thirdPartySaveResult.status.Status != StatusEnum.Success)
                    {
                        result.status.Status = thirdPartySaveResult.status.Status;
                        return result;
                    }

                    var thirdParty = db.ThirdPartyRepresentations.Where(r => r.CustomerID == model.CustomerID).FirstOrDefault();

                    if (thirdParty != null)
                    {
                        thirdParty.ThirdPartyUserID = thirdPartySaveResult.result.UserID;
                        thirdParty.MailNotification = model.MailNotification;
                        thirdParty.EmailNotification = model.EmailNotification;
                        thirdParty.ModifiedDate = DateTime.Now;
                        db.SubmitChanges();
                    }
                    else
                    {
                        ThirdPartyRepresentation _thirdParty = new ThirdPartyRepresentation();
                        _thirdParty.CustomerID = model.CustomerID;
                        _thirdParty.ThirdPartyUserID = thirdPartySaveResult.result.UserID;
                        _thirdParty.MailNotification = model.MailNotification;
                        _thirdParty.EmailNotification = model.EmailNotification;
                        _thirdParty.CreatedDate = DateTime.Now;
                        db.ThirdPartyRepresentations.InsertOnSubmit(_thirdParty);
                        db.SubmitChanges();
                    }
                }
                result.result = true;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                commondbHandler.SaveErrorLog(result.status);
                return result;
            }

        }

        public ReturnResult<ThirdPartyInfoM> RemoveThirdPartyInfo(ThirdPartyInfoM model)
        {
            ReturnResult<ThirdPartyInfoM> result = new ReturnResult<ThirdPartyInfoM>();
            try
            {
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var thirdPartydb = db.ThirdPartyRepresentations.Where(r => r.CustomerID == model.CustomerID && r.ThirdPartyUserID == model.ThirdPartyUser.UserID).FirstOrDefault();

                    if (thirdPartydb != null)
                    {
                        db.ThirdPartyRepresentations.DeleteOnSubmit(thirdPartydb);
                        db.SubmitChanges();
                    }
                    
                }
                result = GetThirdPartyInfo(model.CustomerID);
                
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                commondbHandler.SaveErrorLog(result.status);
                return result;
            }

        }

        public ReturnResult<ThirdPartyInfoM> GetThirdPartyInfo(int CustomerID)
        {
            ReturnResult<ThirdPartyInfoM> result = new ReturnResult<ThirdPartyInfoM>();
            ThirdPartyInfoM model = new ThirdPartyInfoM();
            try
            {
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var thirdParty = db.ThirdPartyRepresentations.Where(r => r.CustomerID == CustomerID).FirstOrDefault();
                   if(thirdParty != null)
                    {
                        model.CustomerID = CustomerID;
                        model.ThirdPartyUser.UserID = thirdParty.ThirdPartyUserID;
                        model.EmailNotification = (thirdParty.EmailNotification == null) ? false : Convert.ToBoolean(thirdParty.EmailNotification);
                        model.MailNotification = (thirdParty.MailNotification == null) ? false : Convert.ToBoolean(thirdParty.MailNotification);

                        var thirdPartyUserInforesult = commondbHandler.GetUserInfo(model.ThirdPartyUser.UserID);
                        if (thirdPartyUserInforesult.status.Status != StatusEnum.Success)
                        {
                            result.status = thirdPartyUserInforesult.status;
                            return result;
                        }
                        model.ThirdPartyUser = thirdPartyUserInforesult.result;
                    }
                    else
                    {
                        model.CustomerID = CustomerID;                       
                    }
                }
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                commondbHandler.SaveErrorLog(result.status);
                return result;
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
               if ((int)(message.custID) == 0)
               {
                   if (CheckCustAccount(message))
                   {
                       result.status = new OperationStatus() { Status = StatusEnum.AccountAlreadyExist };
                       return result;
                   }
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
                  
                       var custTableEdit = db.CustomerDetails.Where(x => x.CustomerID == message.custID).FirstOrDefault();
                       if(custTableEdit != null)
                       {
                           message.CustomerIdentityKey = custTableEdit.CustomerIdentityKey;
                           custTableEdit.Email = message.email;
                           custTableEdit.Password = message.Password;
                           custTableEdit.UserID = message.User.UserID;
                           custTableEdit.CreatedDate = DateTime.Now;
                           custTableEdit.ModifiedDate = DateTime.Now;
                           custTableEdit.bMailingAddress = !message.IsSameMailingAddress;
                           db.SubmitChanges();
                       }
                       else
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
                       }

                       var NPreferenceDB = db.NotificationPreferences.Where(x=>x.UserID == message.User.UserID).FirstOrDefault();
                       if (NPreferenceDB != null)
                       {
                           NPreferenceDB.UserID = message.User.UserID;
                           NPreferenceDB.EmailNotification = message.EmailNotificationFlag;
                           NPreferenceDB.MailNotification = message.MailNotificationFlag;
                           NPreferenceDB.CreatedDate = DateTime.Now;
                           db.SubmitChanges();
                       }
                       else
                       {
                           NotificationPreference notificationTable = new NotificationPreference();
                           notificationTable.UserID = message.User.UserID;
                           notificationTable.EmailNotification = message.EmailNotificationFlag;
                           notificationTable.MailNotification = message.MailNotificationFlag;
                           notificationTable.CreatedDate = DateTime.Now;
                           db.NotificationPreferences.InsertOnSubmit(notificationTable);
                           db.SubmitChanges();
                       }

                       if (!message.IsSameMailingAddress)
                       {

                           var MailingDB = db.MailingAddresses.Where(x => x.CustomerID == message.custID).FirstOrDefault();
                           if (MailingDB != null)
                           {
                               MailingDB.AddressLine1 = message.MailingAddress.AddressLine1;
                               MailingDB.AddressLine2 = message.MailingAddress.AddressLine2;
                               MailingDB.City = message.MailingAddress.City;
                               MailingDB.StateID = message.MailingAddress.State.StateID;
                               MailingDB.Zip = message.MailingAddress.Zip;
                               MailingDB.PhoneNumber = message.MailingAddress.PhoneNumber;
                               MailingDB.CustomerID = message.custID;
                               MailingDB.LastModifiedDate = DateTime.Now;
                               db.SubmitChanges();
                           }
                           else
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

        /// <summary>
        /// Delete cutomer 
        /// </summary>
        /// <returns>true or false</returns>
        public ReturnResult<bool> DeleteCustomer(CustomerInfo message)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    CustomerDetail custTable = db.CustomerDetails.Where(x => x.CustomerID == message.custID).FirstOrDefault();
                    if (custTable != null)
                    {
                        custTable.IsDeleted = true;
                        db.SubmitChanges();
                        
                    }
                }
                //  System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveCustomer completed");
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                result.result = true;
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
                    //cityUserTable.CityAccountTypeID = (int)message.AccountType.AccountTypeID;
                    cityUserTable.FirstName = message.FirstName;
                    cityUserTable.LastName = message.LastName;
                    cityUserTable.Password = message.Password;
                    cityUserTable.Email = message.Email;
                    cityUserTable.EmployeeID = (int)message.EmployeeID;
                    cityUserTable.IsAnalyst = Convert.ToBoolean(message.IsAnalyst);
                    cityUserTable.IsHearingOfficer = Convert.ToBoolean(message.IsHearingOfficer);
                    cityUserTable.IsAdminAssistant = Convert.ToBoolean(message.IsAdminAssistant);
                    cityUserTable.IsCityAdmin = Convert.ToBoolean(message.IsCityAdmin);
                    if (message.IsCityAdmin == true)
                    {
                        cityUserTable.CityAccountTypeID = 3; //City Admin type
                    }
                    else
                    {
                        cityUserTable.CityAccountTypeID = 2;
                    }
                    cityUserTable.IsNonRAPStaff = Convert.ToBoolean(message.IsNonRAPStaff);
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
        private string getCustomerIdentityKey()
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
                        if (CustomerIdentityKeys.Contains(Convert.ToString(randomNo)))
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
            return Convert.ToString(randomNo);
        }
        #endregion

    }
}
