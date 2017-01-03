using System;
using RAP.Core.Common;
using RAP.Core.DataModels;
using System.Collections.Generic;

namespace RAP.DAL
{
    public interface IApplicationProcessingDBHandler
    {
        ReturnResult<PetitionPageSubnmissionStatusM> GetPageSubmissionStatus(int CustomerID);
        ReturnResult<TenantResponsePageSubnmissionStatusM> GetTRPageSubmissionStatus(int CustomerID);
        ReturnResult<AppealPageSubnmissionStatusM> GetAppealPageSubmissionStatus(int CustomerID);
        ReturnResult<CaseInfoM> GetCaseDetails();
        // ReturnResult<CaseInfoM> GetCaseDetails(string caseID);
        ReturnResult<CaseInfoM> GetCaseDetails(int UserID);
        ReturnResult<CaseInfoM> GetCaseInfo(string CaseID, int CustomerID);
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitAppeal(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitTenantResponse(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<CaseInfoM> SaveTenantResponseApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<bool> SaveTenantResponseExemptContestedInfo(TenantResponseExemptContestedInfoM message, int CustomerID);
        ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID);
        ReturnResult<CaseInfoM> GetTenantResponseApplicationInfo(string CaseNumber, int CustomerID);
        ReturnResult<TenantResponseInfoM> GetTenantResponseReviewInfo(string CaseNumber, int CustomerID);
        ReturnResult<CaseInfoM> GetTenantResponseExemptContestedInfo(int TenantResponseID);
        ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst();
        ReturnResult<List<CaseInfoM>> GetCasesForCustomer(int CustomerID);
        ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId);
        ReturnResult<TenantResponseRentalHistoryM> GetTenantResponseRentalHistoryInfo(int TenantResponseID);
        ReturnResult<bool> SaveTenantRentalHistoryInfo(TenantRentalHistoryM rentalHistory, int CustomerID);
        ReturnResult<bool> SaveTenantResponseRentalHistoryInfo(TenantResponseRentalHistoryM rentalHistory, int CustomerID);
        ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID);
        ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition, int CustomerID);
        ReturnResult<bool> SaveTenantLostServiceInfo(LostServicesPageM message, int CustomerID);
        ReturnResult<LostServicesPageM> GetTenantLostServiceInfo(int PetitionID);
        ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int CustomerID);
        ReturnResult<CaseInfoM> GetPetitionViewInfo(int C_ID);
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo, int CustomerID);
        ReturnResult<List<AppealGroundM>> GetAppealGroundInfo(string CaseNumber, int AppealFiledBy);
        ReturnResult<CaseInfoM> GetAppealServe(int AppealID);
        ReturnResult<TenantAppealInfoM> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<bool> AddAnotherOpposingParty(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(TenantAppealInfoM tenantAppealInfo,int CustomerID);
        ReturnResult<CaseInfoM> GetPetitioncategory();
        ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<OwnerPetitionPropertyInfoM> GetOwnerPropertyAndTenantInfo(OwnerPetitionPropertyInfoM model);
        ReturnResult<List<OwnerRentIncreaseReasonsM>> GetRentIncreaseReasonInfo(OwnerPetitionInfoM petition);
        ReturnResult<CaseInfoM> GetOwnerRentIncreaseAndPropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerReview(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<bool> SaveRentIncreaseReasonInfo(OwnerPetitionInfoM petition);
        ReturnResult<OwnerPetitionPropertyInfoM> SaveOwnerPropertyAndTenantInfo(OwnerPetitionPropertyInfoM model);
        ReturnResult<OwnerPetitionPropertyInfoM> SaveOwnerRentIncreaseAndUpdatePropertyInfo(OwnerPetitionPropertyInfoM model);
        ReturnResult<CaseInfoM> SubmitOwnerPetition(CaseInfoM model);
        ReturnResult<bool> OwnerUpdateAdditionalDocumentsPageSubmission(int CustomerID);
        ReturnResult<bool> OwnerUpdateReviewPageSubmission(int CustomerID);
        ReturnResult<CaseInfoM> GetOResponseApplicantInfo(CaseInfoM model);
        ReturnResult<OwnerResponsePropertyInfoM> GetOResponsePropertyAndTenantInfo(OwnerResponsePropertyInfoM model);
        ReturnResult<CaseInfoM> GetOResponseRentIncreaseAndPropertyInfo(CaseInfoM model);
        ReturnResult<OwnerResponsePropertyInfoM> GetOResponseExemption(OwnerResponsePropertyInfoM model);
        ReturnResult<CaseInfoM> GetOResponseReview(CaseInfoM model);
        ReturnResult<OwnerResponsePageSubnmissionStatusM> GetOResponseSubmissionStatus(int CustomerID);
        ReturnResult<CaseInfoM> SaveOResponseApplicantInfo(CaseInfoM model);
        ReturnResult<OwnerResponsePropertyInfoM> SaveOResponsePropertyAndTenantInfo(OwnerResponsePropertyInfoM model);
        ReturnResult<OwnerResponsePropertyInfoM> SaveOResponseRentIncreaseAndUpdatePropertyInfo(OwnerResponsePropertyInfoM model);
        ReturnResult<OwnerResponsePropertyInfoM> SaveOResponseExemption(OwnerResponsePropertyInfoM model);
        ReturnResult<CaseInfoM> SubmitOwnerResponse(CaseInfoM model);
        ReturnResult<bool> OResponseUpdateDecreasedHousingPageSubmission(int CustomerID);
        ReturnResult<bool> OResponseUpdateReviewPageSubmission(int CustomerID);
        ReturnResult<bool> OResponseUpdateAdditionalDocumentsPageSubmission(int CustomerID);
       
    }
}
