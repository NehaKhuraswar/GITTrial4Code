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

                using (DashboardDataContext db = new DashboardDataContext(_connString))
                {
                    string errorMessage = "";
                    int? errorCode = 0;
                    //int errorCode ;
                    var item =  db.USP_ActivityCase_Get(Convert.ToInt32(C_ID), ref errorMessage, ref errorCode);

                    if(errorCode != 0)
                    {
                        result.status = new OperationStatus() { Status = StatusEnum.DatabaseMessage, StatusMessage=errorMessage };
                        return result;
                    }
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
