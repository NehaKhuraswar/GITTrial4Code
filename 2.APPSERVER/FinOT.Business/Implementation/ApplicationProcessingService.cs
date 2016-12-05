﻿using System;
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
        private readonly ICommonService _commonService;
        //TBD
        //public ApplicationProcessingService()
        //{
        //    _dbHandler = new ApplicationProcessingDBHandler();
        //}
        public ApplicationProcessingService(IApplicationProcessingDBHandler dbHandler, IExceptionHandler eHandler, ICommonService commonService)
        {
            this._dbHandler = dbHandler;
            this._eHandler = eHandler;
            this._commonService = commonService;
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
        public ReturnResult<CaseInfoM> GetCaseDetails(int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetCaseDetails(UserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo)
        {
            
             ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                result = _dbHandler.SaveTenantAppealInfo(caseInfo);
                return result;
            }
            catch(Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(CaseInfoM caseInfo)
        {

            ReturnResult<TenantAppealInfoM> result = new ReturnResult<TenantAppealInfoM>();
            try
            {
                result = _dbHandler.SaveTenantServingAppeal(caseInfo);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> AddAnotherOpposingParty(CaseInfoM caseInfo)
        {

            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.AddAnotherOpposingParty(caseInfo);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<Boolean> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo)
        {
            
            ReturnResult<Boolean> result = new ReturnResult<Boolean>();
            try
            {
                result = _dbHandler.SaveAppealGroundInfo(tenantAppealInfo);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.SubmitTenantPetition(caseInfo);
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
        public ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.SaveApplicationInfo(caseInfo, UserID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        public ReturnResult<bool> SaveTenantRentalIncrementInfo(TenantPetitionInfoM petition)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SaveTenantRentalIncrementInfo(petition);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> SaveTenantLostServiceInfo(TenantPetitionInfoM petition)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SaveTenantLostServiceInfo(petition);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SavePetitionGroundInfo(petition);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

        #region Common File Petition methods
        //public ReturnResult<CaseInfoM> GetCaseModel()
        //{
        //    ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
        //    CaseInfoM model = new CaseInfoM();
        //    result.result = model;
        //    result.status = new OperationStatus() { Status = StatusEnum.Success };
        //    return result;
        //}
        public ReturnResult<CaseInfoM> GetPetitioncategory()
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
           try
           {
               result = _dbHandler.GetPetitioncategory();
               return result;
           }
            catch(Exception ex)
           {
               result.status = _eHandler.HandleException(ex);
               _commonService.LogError(result.status);
               return result;
           }
        }
        #endregion
        //implements all methods from IOTRequestService
    }
}
