using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface IApplicationProcessingService
    {
        string CorrelationId { get; set; }
        ReturnResult<PetitionPageSubnmissionStatusM> GetPageSubmissionStatus(int CustomerID);
        ReturnResult<TenantResponsePageSubnmissionStatusM> GetTRPageSubmissionStatus(int CustomerID);
        ReturnResult<AppealPageSubnmissionStatusM> GetAppealPageSubmissionStatus(int CustomerID);
        ReturnResult<CaseInfoM> GetCaseDetails();
        ReturnResult<CaseInfoM> GetCaseInfo(string caseID, int CustomerID);
        ReturnResult<CaseInfoM> GetCaseDetails(int UserID);
        ReturnResult<CaseInfoM> SubmitAppeal(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitTenantResponse(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<CaseInfoM> SaveTenantResponseApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<bool> SaveTenantResponseExemptContestedInfo(TenantResponseExemptContestedInfoM message, int CustomerID);
        ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition, int CustomerID);
        ReturnResult<bool> SaveTenantResponseRentalHistoryInfo(TenantResponseRentalHistoryM rentalHistory, int CustomerID);
        ReturnResult<bool> SaveTenantRentalHistoryInfo(TenantRentalHistoryM rentalHistory, int CustomerID);
        ReturnResult<bool> SaveTenantLostServiceInfo(LostServicesPageM message, int CustomerID);
        ReturnResult<LostServicesPageM> GetTenantLostServiceInfo(int PetitionID, int CustomerID);
        ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int CustomerID);
        ReturnResult<CaseInfoM> GetPetitionViewInfo(int C_ID); 
        ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId);
        ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst(int UserID);
        ReturnResult<CaseInfoM> GetSelectedCase(int C_ID);
       // ReturnResult<List<CaseInfoM>> GetCasesforPublicDashboard(int CustomerID);
        ReturnResult<List<CaseInfoM>> GetCasesForCustomer(int CustomerID);
        ReturnResult<List<ThirdPartyCaseInfo>> GetThirdPartyCasesForCustomer(int CustomerID);
        ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID);
        ReturnResult<CaseInfoM> GetTenantResponseApplicationInfo(string CaseNumber, int CustomerID);
        ReturnResult<CaseInfoM> GetTenantResponseReviewInfo(string CaseNumber, int CustomerID);
        ReturnResult<TenantResponseRentalHistoryM> GetTenantResponseRentalHistoryInfo(int TenantResponseID, int CustomerID);
        ReturnResult<CaseInfoM> GetTenantResponseExemptContestedInfo(int TenantResponseID);
        ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID);
        ReturnResult<List<AppealGroundM>> GetAppealGroundInfo(string CaseNumber, int AppealFiledBy);
        ReturnResult<CaseInfoM> GetAppealServe(int AppealID);
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo, int CustomerID);
        ReturnResult<TenantAppealInfoM> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<bool> AddAnotherOpposingParty(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(TenantAppealInfoM tenantAppealInfo, int CustomerID);
        ReturnResult<CaseInfoM> GetPetitioncategory();
        ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetRentIncreaseReasonInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerPropertyAndTenantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerRentIncreaseAndPropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerAdditionalDocuments(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerReview(CaseInfoM model);
        ReturnResult<CaseInfoM> GetTRAdditionalDocuments(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveRentIncreaseReasonInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerPropertyAndTenantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerRentIncreaseAndUpdatePropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerAdditionalDocuments(CaseInfoM model);
        ReturnResult<bool> SaveOwnerReviewPageSubmission(int CustomerID);
        ReturnResult<CaseInfoM> SaveTRAdditionalDocuments(CaseInfoM model);
        ReturnResult<CaseInfoM> SubmitOwnerPetition(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOResponseApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOResponsePropertyAndTenantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOResponseRentIncreaseAndPropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOResponseDecreasedHousing(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOResponseExemption(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOResponseAdditionalDocuments(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOResponseReview(CaseInfoM model);
        ReturnResult<OwnerResponsePageSubnmissionStatusM> GetOResponseSubmissionStatus(int CustomerID);
        ReturnResult<CaseInfoM> SaveOResponseApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOResponsePropertyAndTenantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOResponseRentIncreaseAndUpdatePropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOResponseAdditionalDocuments(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOResponseExemption(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOResponseDecreasedHousing(CaseInfoM model);
        ReturnResult<bool> SaveOResponseReviewPageSubmission(int CustomerID);   
        ReturnResult<CaseInfoM> SubmitOwnerResponse(CaseInfoM model);
        ReturnResult<List<ThirdPartyCaseInfo>> UpdateThirdPartyAccessPrivilege(List<ThirdPartyCaseInfo> ThirdPartyCaseInfo, int CustomerID);
    }
}
