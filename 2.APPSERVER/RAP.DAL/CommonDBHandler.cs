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
                    //EditUserInfo
                    if (userInfo.UserID != 0)
                    {
                        var userDB = db.UserInfos.Where(x => x.UserID == userInfo.UserID).FirstOrDefault();
                        userDB.FirstName = userInfo.FirstName;
                        userDB.LastName = userInfo.LastName;
                        userDB.BusinessName = userInfo.BusinessName;
                        userDB.AddressLine1 = userInfo.AddressLine1;
                        userDB.AddressLine2 = userInfo.AddressLine2;
                        userDB.City = userInfo.City;
                        userDB.StateID = userInfo.State.StateID;
                        userDB.Zip = userInfo.Zip;
                        userDB.PhoneNumber = userInfo.PhoneNumber;
                        userDB.ContactEmail = userInfo.Email;
                        userDB.CreatedDate = DateTime.Now;

                        db.SubmitChanges();

                        System.Diagnostics.EventLog.WriteEntry("Application", "DAL EditUserInfo completed");
                        result.result = userInfo;
                        result.status = new OperationStatus() { Status = StatusEnum.Success };
                        return result;
                    }
                    var user = db.UserInfos.Where(x => (x.FirstName == userInfo.FirstName
                                                            && x.LastName == userInfo.LastName
                                                            && x.AddressLine1 == userInfo.AddressLine1
                                                            && x.AddressLine2 == userInfo.AddressLine2
                                                            && x.City == userInfo.City
                                                            && x.StateID == userInfo.State.StateID
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
                        userInfoDB.BusinessName = userInfo.BusinessName;
                        userInfoDB.AddressLine1 = userInfo.AddressLine1;
                        userInfoDB.AddressLine2 = userInfo.AddressLine2;
                        userInfoDB.City = userInfo.City;
                        userInfoDB.StateID = userInfo.State.StateID;
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
            catch (Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "Error : " + ex.Message + "| StackTrace" + ex.StackTrace.ToString());
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                return result;
            }
        }
        /// <summary>
        /// Edits the data needed to to display on the tenant petition form
        /// </summary>
        /// <returns></returns>

        public ReturnResult<UserInfoM> EditUserInfo(UserInfoM userInfo)
        {
            ReturnResult<UserInfoM> result = new ReturnResult<UserInfoM>();
            try
            {
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveUserInfo started");
                using (CommonDataContext db = new CommonDataContext(_connString))
                {
                    var userInfoDB = db.UserInfos.Where(x => (x.UserID == userInfo.UserID)).FirstOrDefault();

                    userInfoDB.FirstName = userInfo.FirstName;
                    userInfoDB.LastName = userInfo.LastName;
                    userInfoDB.BusinessName = userInfo.BusinessName;
                    userInfoDB.AddressLine1 = userInfo.AddressLine1;
                    userInfoDB.AddressLine2 = userInfo.AddressLine2;
                    userInfoDB.City = userInfo.City;
                    userInfoDB.StateID = userInfo.State.StateID;
                    userInfoDB.Zip = userInfo.Zip;
                    userInfoDB.PhoneNumber = userInfo.PhoneNumber;
                    userInfoDB.ContactEmail = userInfo.Email;
                    db.SubmitChanges();
                }
                System.Diagnostics.EventLog.WriteEntry("Application", "DAL SaveUserInfo completed");
                result.result = userInfo;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
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
                    var userinfo = db.UserInfos.Where(x => x.UserID == UserId).FirstOrDefault();
                    //.Select(c => new UserInfoM()
                    //{
                    //    UserID = c.UserID,
                    //    FirstName = c.FirstName,
                    //    LastName = c.LastName,
                    //    AddressLine1 = c.AddressLine1,
                    //    AddressLine2 = c.AddressLine2,
                    //    City = c.City,
                    //    PhoneNumber = c.PhoneNumber,
                    //    State.StateID = c.State.SID,
                    //    Zip = c.Zip,
                    //    Email = c.ContactEmail,
                    //})

                    if (userinfo != null)
                    {


                        _userinfo.UserID = userinfo.UserID;
                        _userinfo.FirstName = userinfo.FirstName;
                        _userinfo.LastName = userinfo.LastName;
                        _userinfo.BusinessName = userinfo.BusinessName;
                        _userinfo.AddressLine1 = userinfo.AddressLine1;
                        _userinfo.AddressLine2 = userinfo.AddressLine2;
                        _userinfo.City = userinfo.City;
                        _userinfo.PhoneNumber = userinfo.PhoneNumber;
                        _userinfo.State.StateID = userinfo.StateID;
                        _userinfo.Zip = userinfo.Zip;
                        _userinfo.Email = userinfo.ContactEmail;

                    }
                    var state = db.States.Where(x => x.StateID == _userinfo.State.StateID).FirstOrDefault();
                    _userinfo.State.StateName = state.StateName;

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

        /// <summary>
        /// Coverts Datetime to CustomDate
        /// </summary>
        /// <param name="DatabaseDate"></param>
        /// <returns></returns>
        public CustomDate GetDateFromDatabase(DateTime DatabaseDate)
        {
            try
            {
                CustomDate FrontEndDate = new CustomDate();
                FrontEndDate.Day = DatabaseDate.Day;
                FrontEndDate.Month = DatabaseDate.Month;
                FrontEndDate.Year = DatabaseDate.Year;
                return FrontEndDate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ReturnResult<DocumentM> SaveDocument(DocumentM doc)
        {
            ReturnResult<DocumentM> result = new ReturnResult<DocumentM>();
            try
            {
                if (doc != null)
                {
                    using (CommonDataContext db = new CommonDataContext(_connString))
                    {
                        Document document = new Document();
                        document.DocName = doc.DocName;
                        document.DocTitle = doc.DocTitle;
                        document.DocThirdPartyID = doc.DocThirdPartyID;
                        document.CustomerID = doc.CustomerID;
                        document.DocCategory = doc.DocCategory;
                        document.DocDescription = string.IsNullOrEmpty(doc.DocDescription) ? null : doc.DocDescription;
                        document.IsPetitionFiled = false;
                        document.MimeType = doc.MimeType;
                        db.Documents.InsertOnSubmit(document);
                        db.SubmitChanges();
                        doc.DocID = document.DocID;
                    }
                    result.result = doc;
                }
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                SaveErrorLog(result.status);
                return result;
            }
        }

        public ReturnResult<bool> PetitionFiledActivity(int C_ID, int CaseFileBy)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                using (DashboardDataContext db = new DashboardDataContext(ConfigurationManager.AppSettings["RAPDBConnectionString"]))
                {
                    string errorMessage = "";
                    int? errorCode = 0;
                    //TBD
                    int returnCode = db.USP_NewActivityStatus_Save((int)ActivityDefaults.ActivityPetitionFiled, (int)StatusDefaults.StatusSubmitted,
                                     C_ID, "", DateTime.Now, CaseFileBy, ref errorMessage, ref errorCode);

                    if (errorCode != 0)
                    {
                        result.result = false;
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseMessage, StatusMessage = errorMessage };
                        return result;
                    }
                }
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                SaveErrorLog(result.status);
                return result;
            }
        }

        public ReturnResult<List<DocumentM>> GetDocuments(int CustmerID, bool isPetitiofiled, string docTitle = null)
        {
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            List<DocumentM> docs = new List<DocumentM>();
            try
            {

                using (CommonDataContext db = new CommonDataContext(_connString))
                {

                    if (string.IsNullOrEmpty(docTitle))
                    {
                        var _documents = db.Documents.Where(r => r.CustomerID == CustmerID && r.IsPetitionFiled == isPetitiofiled);
                        if (_documents != null)
                        {
                            foreach (var item in _documents)
                            {
                                DocumentM doc = new DocumentM();
                                doc.C_ID = item.C_ID;
                                doc.CustomerID = item.CustomerID;
                                doc.DocCategory = item.DocCategory;
                                doc.DocName = item.DocName;
                                doc.DocID = item.DocID;
                                doc.DocThirdPartyID = item.DocThirdPartyID;
                                doc.DocDescription = item.DocDescription;
                                doc.DocTitle = item.DocTitle;
                                doc.MimeType = item.MimeType;
                                doc.IsPetitonFiled = (bool)item.IsPetitionFiled;
                                doc.isUploaded = true;
                                docs.Add(doc);
                            }
                        }
                    }
                    else
                    {
                        var _documents = db.Documents.Where(r => r.CustomerID == CustmerID && r.IsPetitionFiled == isPetitiofiled && r.DocTitle == docTitle);
                        if (_documents != null)
                        {
                            foreach (var item in _documents)
                            {
                                DocumentM doc = new DocumentM();                             
                                doc.C_ID = item.C_ID;
                                doc.CustomerID = item.CustomerID;
                                doc.DocCategory = item.DocCategory;
                                doc.DocName = item.DocName;
                                doc.DocID = item.DocID;
                                doc.DocThirdPartyID = item.DocThirdPartyID;
                                doc.DocDescription = item.DocDescription;
                                doc.DocTitle = item.DocTitle;
                                doc.MimeType = item.MimeType;
                                doc.IsPetitonFiled = (bool)item.IsPetitionFiled;
                                doc.isUploaded = true;
                                docs.Add(doc);
                            }
                        }
                    }

                }
                result.result = docs;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                SaveErrorLog(result.status);
                return result;
            }
        }

        public ReturnResult<List<DocumentM>> GetDocumentsByCategory(int CustmerID, bool isPetitiofiled, string docCategory)
        {
            ReturnResult<List<DocumentM>> result = new ReturnResult<List<DocumentM>>();
            List<DocumentM> docs = new List<DocumentM>();
            try
            {

                using (CommonDataContext db = new CommonDataContext(_connString))
                {

                    var _documents = db.Documents.Where(r => r.CustomerID == CustmerID && r.IsPetitionFiled == isPetitiofiled && r.DocCategory == docCategory);
                    if (_documents != null)
                    {
                        foreach (var item in _documents)
                        {
                            DocumentM doc = new DocumentM();
                            doc.C_ID = item.C_ID;
                            doc.CustomerID = item.CustomerID;
                            doc.DocCategory = item.DocCategory;
                            doc.DocName = item.DocName;
                            doc.DocID = item.DocID;
                            doc.DocThirdPartyID = item.DocThirdPartyID;
                            doc.DocDescription = item.DocDescription;
                            doc.DocTitle = item.DocTitle;
                            doc.MimeType = item.MimeType;
                            doc.IsPetitonFiled = (bool)item.IsPetitionFiled;
                            doc.isUploaded = true;
                            docs.Add(doc);
                        }
                    }

                }
                result.result = docs;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                SaveErrorLog(result.status);
                return result;
            }
        }

        public ReturnResult<bool> UpdateDocumentCaseInfo(int CustmerID, int C_ID, string docCategory)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                using (CommonDataContext db = new CommonDataContext(_connString))
                {

                    var _documents = db.Documents.Where(r => r.CustomerID == CustmerID && r.IsPetitionFiled == false && r.DocCategory == docCategory);
                    if (_documents.Any())
                    {
                        foreach (var doc in _documents)
                        {
                            doc.C_ID = C_ID;
                            doc.IsPetitionFiled = true;
                            db.SubmitChanges();
                        }
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
                SaveErrorLog(result.status);
                return result;
            }

        }

        public ReturnResult<List<string>> GetDocDescription()
        {
            ReturnResult<List<string>> result = new ReturnResult<List<string>>();
            try
            {
                using (CommonDataContext db = new CommonDataContext(_connString))
                {
                    var descriptions = db.DocDescriptions.Select(r => r.Description);

                    if (descriptions != null && descriptions.Count() > 0)
                    {
                        result.result = descriptions.ToList();
                    }
                }
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                IExceptionHandler eHandler = new ExceptionHandler();
                result.status = eHandler.HandleException(ex);
                SaveErrorLog(result.status);
                return result;
            }
        }

    }
}
