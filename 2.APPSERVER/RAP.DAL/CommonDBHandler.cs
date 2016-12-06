using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RAP.Core.Common;
using RAP.Core.DataModels;

namespace RAP.DAL
{
    public class CommonDBHandler : ICommonDBHandler
    {
        private readonly string _connString;
        private bool _logToDatabase; 
         
        public CommonDBHandler()
        {
            _connString = ConfigurationManager.AppSettings["RAPDBConnectionString"];
             _logToDatabase = string.IsNullOrEmpty(ConfigurationManager.AppSettings["logToDatabase"]) ? false : ((ConfigurationManager.AppSettings["logToDatabase"] == "true" ? true : false));
        }

        /// <summary>
        /// Logs error details to the database
        /// </summary>
        /// <returns></returns>

        public void SaveErrorLog(OperationStatus status)
        {
           
            try
            {
                if (_logToDatabase)
                {
                    using (CommonDataContext db = new CommonDataContext(_connString))
                    {
                        ErrorLog error = new ErrorLog();
                        error.ErrorNumber = status.StatusCode;
                        error.ErrorMessage = status.StatusMessage;
                        error.ErrorMessageDetails = status.StatusDetails;
                        error.CreatedDate = DateTime.Now;
                        db.ErrorLogs.InsertOnSubmit(error);
                        db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                              
            }
        }
        /// <summary>
        /// Gets the data needed to to display on the tenant petition form
        /// </summary>
        /// <returns></returns>

        public ReturnResult<UserInfoM> SaveUserInfo(UserInfoM userInfo)
        {
            ReturnResult<UserInfoM> result = new ReturnResult<UserInfoM>();
            try
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveUserInfo started");
                using (CommonDataContext db = new CommonDataContext(_connString))
                {
                    var user = db.UserInfos.Where(x => (x.FirstName == userInfo.FirstName
                                                            && x.LastName == userInfo.LastName
                                                            && x.AddressLine1 == userInfo.AddressLine1
                                                            && x.AddressLine2 == userInfo.AddressLine2
                                                            && x.City == userInfo.City
                                                            && x.State == userInfo.State
                                                            && x.Zip == userInfo.Zip)).FirstOrDefault();

                    if (user != null)
                    {
                        userInfo.UserID = user.UserID;                      
                    }
                    else
                    {
                        UserInfo userInfoDB = new UserInfo();
                        userInfoDB.FirstName = userInfo.FirstName;
                        userInfoDB.LastName = userInfo.LastName;
                        userInfoDB.AddressLine1 = userInfo.AddressLine1;
                        userInfoDB.AddressLine2 = userInfo.AddressLine2;
                        userInfoDB.City = userInfo.City;
                        userInfoDB.State = userInfo.State;
                        userInfoDB.Zip = userInfo.Zip;
                        userInfoDB.PhoneNumber = userInfo.PhoneNumber;
                        userInfoDB.ContactEmail = userInfo.Email;
                        userInfoDB.CreatedDate = DateTime.Now;

                        db.UserInfos.InsertOnSubmit(userInfoDB);
                        db.SubmitChanges();
                        userInfo.UserID = userInfoDB.UserID;
                    }
                    System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveUserInfo completed");
                    result.result = userInfo;
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                    return result;
                }                
            }
            catch(Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Error : " + ex.Message + "| StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<UserInfoM> GetUserInfo(int UserId)
        {
            ReturnResult<UserInfoM> result = new ReturnResult<UserInfoM>();
            UserInfoM _userinfo = new UserInfoM();
            try
            {
                using (CommonDataContext db = new CommonDataContext(_connString))
                {
                    var userinfo = db.UserInfos.Where(x => x.UserID == UserId)
                                                                .Select(c => new UserInfoM()
                                                                {
                                                                    UserID = c.UserID,
                                                                    FirstName = c.FirstName,
                                                                    LastName = c.LastName,
                                                                    AddressLine1 = c.AddressLine1,
                                                                    AddressLine2 = c.AddressLine2,
                                                                    City = c.City,
                                                                    PhoneNumber = c.PhoneNumber,
                                                                    State = c.State,
                                                                    Zip = c.Zip,
                                                                    Email = c.ContactEmail,
                                                                }).FirstOrDefault();

                    if (userinfo != null)
                    {
                        _userinfo = userinfo;
                        //userinfo.UserID = userinfos.UserID;
                        //userinfo.FirstName = userinfos.FirstName;
                        //userinfo.LastName = userinfos.LastName;
                        //userinfo.AddressLine1 = userinfos.AddressLine1;
                        //userinfo.AddressLine2 = userinfos.AddressLine2;
                        //userinfo.City = userinfos.City;
                        //userinfo.PhoneNumber = userinfos.PhoneNumber;
                        //userinfo.State = userinfos.State;
                        //userinfo.Zip = userinfos.Zip;
                        //userinfo.Email = userinfos.Email;

                    }
                    result.result = _userinfo;
                    result.status = new OperationStatus() { Status = StatusEnum.Success };
                    return result;
                }
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }

    }
}
