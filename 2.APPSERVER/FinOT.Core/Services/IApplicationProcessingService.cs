using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface IApplicationProcessingService
    {
        string CorrelationId { get; set; }
        ReturnResult<CaseInfoM> GetCaseDetails();
        ReturnResult<CaseInfoM> GetCaseDetails(string caseID);
        ReturnResult<CaseInfoM> GetCaseDetails(int UserID);
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition);
        ReturnResult<bool> SaveTenantRentalIncrementInfo(TenantPetitionInfoM petition);
        ReturnResult<bool> SaveTenantLostServiceInfo(TenantPetitionInfoM petition);
        ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID);
        ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID);
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo);
        ReturnResult<bool> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<bool> AddAnotherOpposingParty(CaseInfoM caseInfo);
        ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> GetPetitioncategory();
        ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetRentIncreaseReasonInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> GetOwnerPropertyAndTenantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveRentIncreaseReasonInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerPropertyAndTenantInfo(CaseInfoM model);
    }
}
