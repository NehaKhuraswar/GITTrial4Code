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
    public class ApplicationProcessingService : IApplicationProcessingService
    {
        public string CorrelationId { get; set; }
        private readonly IApplicationProcessingDBHandler _dbHandler;
        private readonly IExceptionHandler _eHandler;
        //TBD
        public ApplicationProcessingService()
        {
            _dbHandler = new ApplicationProcessingDBHandler();
        }
        public ApplicationProcessingService(IApplicationProcessingDBHandler dbHandler, IExceptionHandler eHandler)
        {
            this._dbHandler = dbHandler;
            this._eHandler = eHandler;
        }
       
        public ReturnResult<CaseInfoM> GetCaseDetails()
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetCaseDetails();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> GetCaseDetails(string caseID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetCaseDetails(caseID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(TenantAppealInfoM TenantAppealInfo)
        {
            //private ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(TenantAppealInfoM TenantAppealInfo)
             ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                result = _dbHandler.SaveTenantAppealInfo(TenantAppealInfo);
                return result;
            }
            catch(Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
       public ReturnResult<Boolean> SaveAppealGroundInfo(List<AppealGroundM> AppealGrounds)
        {
            //private ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(TenantAppealInfoM TenantAppealInfo)
            ReturnResult<Boolean> result = new ReturnResult<Boolean>();
            try
            {
                result = _dbHandler.SaveAppealGroundInfo(AppealGrounds);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.SaveCaseDetails(caseInfo);
                return result;
            }
            catch(Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        //implements all methods from IOTRequestService
    }
}
