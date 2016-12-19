﻿using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface IApplicationProcessingService
    {
        string CorrelationId { get; set; }
        ReturnResult<PetitionPageSubnmissionStatusM> GetPageSubmissionStatus(int CustomerID);
        ReturnResult<CaseInfoM> GetCaseDetails();
        ReturnResult<CaseInfoM> GetCaseInfo(string caseID);
        ReturnResult<CaseInfoM> GetCaseDetails(int UserID);
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition, int CustomerID);
        ReturnResult<bool> SaveTenantRentalHistoryInfo(TenantRentalHistoryM rentalHistory, int CustomerID);
        ReturnResult<bool> SaveTenantLostServiceInfo(LostServicesPageM message, int CustomerID);
        ReturnResult<LostServicesPageM> GetTenantLostServiceInfo(int PetitionID);
        ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int CustomerID);        
        ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId);
        ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst();
       // ReturnResult<List<CaseInfoM>> GetCasesforPublicDashboard(int CustomerID);
        ReturnResult<List<CaseInfoM>> GetCasesForCustomer(int CustomerID);
        ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID);
        ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID);
        ReturnResult<List<AppealGroundM>> GetAppealGroundInfo(string CaseNumber, int AppealFiledBy);
        ReturnResult<ServeAppealM> GetAppealServe(int AppealID);
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo);
        ReturnResult<TenantAppealInfoM> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<bool> AddAnotherOpposingParty(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<CaseInfoM> GetPetitioncategory();
        ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetRentIncreaseReasonInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerPropertyAndTenantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerRentIncreaseAndPropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveRentIncreaseReasonInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerPropertyAndTenantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerRentIncreaseAndUpdatePropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SubmitOwnerPetition(CaseInfoM model);
    }
}
