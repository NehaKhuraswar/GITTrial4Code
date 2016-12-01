using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.Business.Helper;
using RAP.DAL;

namespace RAP.Business.Implementation
{
    public class DashboardService : IDashboardService
    {
        public string CorrelationId { get; set; }
        private readonly IDashboardDBHandler _dbHandler;
        private readonly IExceptionHandler _eHandler = new ExceptionHandler();
        //TBD
        //public ApplicationProcessingService()
        //{
        //    _dbHandler = new ApplicationProcessingDBHandler();
        //}
        public DashboardService(IDashboardDBHandler dbHandler)
        {
            this._dbHandler = dbHandler;
        }

        public ReturnResult<List<ActivityStatus_M>> GetActivityStatusForCase(int C_ID)
        {
            ReturnResult<List<ActivityStatus_M>> result = new ReturnResult<List<ActivityStatus_M>>();
            try
            {
                result = _dbHandler.GetActivityStatusForCase(C_ID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<Activity_M>> GetActivity()
        {
            ReturnResult<List<Activity_M>> result = new ReturnResult<List<Activity_M>>();
            try
            {
                result = _dbHandler.GetActivity();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<Status_M>> GetStatus(int activityID)
        {
            ReturnResult<List<Status_M>> result = new ReturnResult<List<Status_M>>();
            try
            {
                result = _dbHandler.GetStatus(activityID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        //implements all methods from IDashboardService
    }
}
