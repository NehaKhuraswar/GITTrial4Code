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
      
        //#region "Save"
       
        #endregion

    }
}
