﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RAP.Core.DataModels;
using RAP.Core.Common;

namespace RAP.DAL
{
    public class DashboardDBHandler:IDashboardDBHandler
    {
        private readonly string _connString;
        CommonDBHandler commondbHandler = new CommonDBHandler();
       
        public DashboardDBHandler()
        {
            _connString =  ConfigurationManager.AppSettings["RAPDBConnectionString"];
        }
        #region "Get"
        /// <summary>
        /// Get customer information
        /// </summary>
        /// <returns>Customer Info Object</returns>
        public ReturnResult<List<ActivityStatus_M>> GetActivityStatusForCase(int C_ID)
        {
            ReturnResult<List<ActivityStatus_M>> result = new ReturnResult<List<ActivityStatus_M>>();
            try
            {
                // CustomerInfo custinfo ;
                List<ActivityStatus_M> ActivityStatusList = new List<ActivityStatus_M>(); 

                using (DashboardDataContext db = new DashboardDataContext(_connString))
                {
                    string errorMessage = "";
                    int? errorCode = 0;

                    
                    //int errorCode ;
                    var activityStatusListDB = db.USP_ActivityStatusForCase_Get(C_ID, ref errorMessage, ref errorCode).ToList();

                    if (errorCode != 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseMessage, StatusMessage = errorMessage };
                        return result;
                    }

                    foreach (var item in activityStatusListDB)
                    {
                        ActivityStatus_M objActivityStatus = new ActivityStatus_M();
                        objActivityStatus.Activity.ActivityID = item.ActivityID;
                        objActivityStatus.Activity.ActivityDesc = item.ActivityName;
                        objActivityStatus.Status.StatusID = item.StatusID;
                        objActivityStatus.Status.StatusDesc = item.StatusDesc;
                        objActivityStatus.Date = item.CreatedDate;
                        ActivityStatusList.Add(objActivityStatus);

                    }
                    result.result = ActivityStatusList;
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
        public ReturnResult<List<CityUserAccount_M>> GetHearingOfficers()
        {
            ReturnResult<List<CityUserAccount_M>> result = new ReturnResult<List<CityUserAccount_M>>();
            try
            {
                List<CityUserAccount_M> HearingOfficers = new List<CityUserAccount_M>();

                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var HearingOfficersDB = db.CityUserAccounts.Where(x => x.IsHearingOfficer == true).ToList();

                    foreach (var item in HearingOfficersDB)
                    {
                        CityUserAccount_M objHearingOfficer = new CityUserAccount_M();
                        objHearingOfficer.UserID = (int)item.CityUserID;
                        objHearingOfficer.FirstName = item.FirstName;
                        objHearingOfficer.LastName = item.LastName;
                        objHearingOfficer.MobilePhoneNumber = item.MobilePhoneNumber;
                        objHearingOfficer.OfficePhoneNumber = item.OfficePhoneNumber;
                        objHearingOfficer.OfficeLocation = item.OfficeLocation;
                        objHearingOfficer.Title = item.Title;
                        objHearingOfficer.Department = item.Department;
                        objHearingOfficer.CreatedDate = item.CreatedDate;
                        objHearingOfficer.Email = item.Email;
                        objHearingOfficer.EmployeeID = (int)item.EmployeeID;
                        objHearingOfficer.IsAnalyst = item.IsAnalyst;
                        objHearingOfficer.IsHearingOfficer = item.IsHearingOfficer;
                        HearingOfficers.Add(objHearingOfficer);

                    }
                    result.result = HearingOfficers;
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
        public ReturnResult<List<CityUserAccount_M>> GetAnalysts()
        {
            ReturnResult<List<CityUserAccount_M>> result = new ReturnResult<List<CityUserAccount_M>>();
            try
            {
                List<CityUserAccount_M> Analysts = new List<CityUserAccount_M>();

                using (AccountManagementDataContext db = new AccountManagementDataContext(_connString))
                {
                    var AnalystsDB = db.CityUserAccounts.Where(x => x.IsAnalyst == true).ToList();

                    foreach (var item in AnalystsDB)
                    {
                        CityUserAccount_M objAnalyst = new CityUserAccount_M();
                        objAnalyst.UserID = (int)item.CityUserID;
                        objAnalyst.FirstName = item.FirstName;
                        objAnalyst.LastName = item.LastName;
                        objAnalyst.MobilePhoneNumber = item.MobilePhoneNumber;
                        objAnalyst.OfficePhoneNumber = item.OfficePhoneNumber;
                        objAnalyst.OfficeLocation = item.OfficeLocation;
                        objAnalyst.Title = item.Title;
                        objAnalyst.Department = item.Department;
                        objAnalyst.CreatedDate = item.CreatedDate;
                        objAnalyst.Email = item.Email;
                        objAnalyst.EmployeeID = (int)item.EmployeeID;
                        objAnalyst.IsAnalyst = item.IsAnalyst;
                        objAnalyst.IsHearingOfficer = item.IsHearingOfficer;
                        Analysts.Add(objAnalyst);

                    }
                    result.result = Analysts;
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

        public ReturnResult<bool> AssignAnalyst(int cID, int AnalystUserID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext(_connString))
                {
                    var caseDB = db.CaseDetails.Where(x => x.C_ID == cID).FirstOrDefault();
                    if (caseDB != null)
                    {
                        caseDB.CityAnalystUserID = AnalystUserID;
                    }

                    result.result = true;
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

        public ReturnResult<bool> AssignHearingOfficer(int cID, int HearingOfficerUserID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                using (ApplicationProcessingDataContext db = new ApplicationProcessingDataContext(_connString))
                {
                    var caseDB = db.CaseDetails.Where(x => x.C_ID == cID).FirstOrDefault();
                    if (caseDB != null)
                    {
                        caseDB.HearingOfficerUserID = HearingOfficerUserID;
                    }

                    result.result = true;
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

        public ReturnResult<List<Activity_M>> GetActivity()
        {
            ReturnResult<List<Activity_M>> result = new ReturnResult<List<Activity_M>>();
            try
            {
                List<Activity_M> ActivityList = new List<Activity_M>();

                using (DashboardDataContext db = new DashboardDataContext(_connString))
                {
                    var ActivitiesDB = db.Activities.ToList();

                    foreach (var item in ActivitiesDB)
                    {
                        Activity_M objActivity = new Activity_M();
                        objActivity.ActivityID = item.ActivityID;
                        objActivity.ActivityDesc = item.ActivityName;
                        ActivityList.Add(objActivity);

                    }
                    result.result = ActivityList;
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

        public ReturnResult<List<Status_M>> GetStatus(int activityID)
        {
            ReturnResult<List<Status_M>> result = new ReturnResult<List<Status_M>>();
            try
            {
                List<Status_M> StatusList = new List<Status_M>();

                using (DashboardDataContext db = new DashboardDataContext(_connString))
                {
                    string errorMessage = "";
                    int? errorCode = 0;


                    //int errorCode ;
                    var StatusDB = db.USP_Status_Get(activityID, ref errorMessage, ref errorCode).ToList();

                    if (errorCode != 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseMessage, StatusMessage = errorMessage };
                        return result;
                    }

                    foreach (var item in StatusDB)
                    {
                        Status_M objStatus = new Status_M();
                        objStatus.StatusID = item.StatusID;
                        objStatus.StatusDesc = item.StatusDesc;
                        StatusList.Add(objStatus);

                    }
                    result.result = StatusList;
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
        #endregion

        #region "Save"
        public ReturnResult<bool> SaveNewActivityStatus(ActivityStatus_M activityStatus, int C_ID)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {

                using (DashboardDataContext db = new DashboardDataContext(_connString))
                {
                    string errorMessage = "";
                    int? errorCode = 0;


                    //TBD
                    int returnCode =  db.USP_NewActivityStatus_Save(activityStatus.Activity.ActivityID, activityStatus.Status.StatusID,
                                    C_ID, activityStatus.Notes, DateTime.Now, activityStatus.EmployeeID, ref errorMessage, ref errorCode);

                    if (errorCode != 0)
                    {
                        result.result = false;
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseMessage, StatusMessage = errorMessage };
                        return result;
                    }

                    
                    result.result = true;
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
        #endregion

    }
}
