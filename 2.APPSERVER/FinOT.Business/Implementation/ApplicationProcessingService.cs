using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using RAP.Core.Common;
using RAP.Core.Services;
using RAP.Core.DataModels;
using RAP.Business.Helper;
using RAP.DAL;
using System.Configuration;
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID)
        {
            ReturnResult<List<PetitionGroundM>> result = new ReturnResult<List<PetitionGroundM>>();
            try
            {
                result = _dbHandler.GetPetitionGroundInfo(petitionID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }

       
        public ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId)
        {
            ReturnResult<TenantRentalHistoryM> result = new ReturnResult<TenantRentalHistoryM>();
            try
            {
                result = _dbHandler.GetRentalHistoryInfo(PetitionId);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        

        public ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                result = _dbHandler.GetTenantApplicationInfo(CustomerID);
                return result;
            }
            catch (Exception ex)
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

        public ReturnResult<bool> SaveTenantRentalHistoryInfo(TenantRentalHistoryM rentalHistory)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SaveTenantRentalHistoryInfo(rentalHistory);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<bool> SaveTenantLostServiceInfo(LostServicesPageM message)
        {
            ReturnResult<bool> result = new ReturnResult<bool>();
            try
            {
                result = _dbHandler.SaveTenantLostServiceInfo(message);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int PetitionID)
        {
            ReturnResult<TenantPetitionInfoM> result = new ReturnResult<TenantPetitionInfoM>();
            try
            {
                result = _dbHandler.GetTenantReviewInfo(PetitionID);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                return result;
            }
        }
        public ReturnResult<LostServicesPageM> GetTenantLostServiceInfo(int PetitionID)
        {
            ReturnResult<LostServicesPageM> result = new ReturnResult<LostServicesPageM>();
            try
            {
                result = _dbHandler.GetTenantLostServiceInfo(PetitionID);
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
        public ReturnResult<CaseInfoM> GetPetitioncategory()
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetPetitioncategory();
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }
        #endregion

        #region Get Owner Petition Methods
        public ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                string RAPFee = ConfigurationManager.AppSettings["RAPFee"];
                var dbResult = _dbHandler.GetOwnerApplicantInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                dbResult.result.OwnerPetitionInfo.ApplicantInfo.RAPFee = string.IsNullOrEmpty(RAPFee) ? "69" : RAPFee;
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> GetRentIncreaseReasonInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            List<OwnerRentIncreaseReasonsM> _reasons = new List<OwnerRentIncreaseReasonsM>();
            try
            {
                var dbResult = _dbHandler.GetRentIncreaseReasonInfo(model.OwnerPetitionInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.RentIncreaseReasons = dbResult.result;
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> GetOwnerPropertyAndTenantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.GetOwnerPropertyAndTenantInfo(model.OwnerPetitionInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.PropertyInfo = dbResult.result;
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> GetOwnerRentIncreaseAndPropertyInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                result = _dbHandler.GetOwnerRentIncreaseAndPropertyInfo(model);
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }
        #endregion

        #region Save Owner Petition Methods
        public ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveOwnerApplicantInfo(model);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                result.result = dbResult.result;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }


        public ReturnResult<CaseInfoM> SaveRentIncreaseReasonInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveRentIncreaseReasonInfo(model.OwnerPetitionInfo);  
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> SaveOwnerPropertyAndTenantInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveOwnerPropertyAndTenantInfo(model.OwnerPetitionInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.PropertyInfo = dbResult.result;
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }

        public ReturnResult<CaseInfoM> SaveOwnerRentIncreaseAndUpdatePropertyInfo(CaseInfoM model)
        {
            ReturnResult<CaseInfoM> result = new ReturnResult<CaseInfoM>();
            try
            {
                var dbResult = _dbHandler.SaveOwnerRentIncreaseAndUpdatePropertyInfo(model.OwnerPetitionInfo.PropertyInfo);
                if (dbResult.status.Status != StatusEnum.Success)
                {
                    result.status = dbResult.status;
                    return result;
                }
                model.OwnerPetitionInfo.PropertyInfo = dbResult.result;
                result.result = model;
                result.status = new OperationStatus() { Status = StatusEnum.Success };
                return result;
            }
            catch (Exception ex)
            {
                result.status = _eHandler.HandleException(ex);
                _commonService.LogError(result.status);
                return result;
            }
        }
        #endregion
    }
}
