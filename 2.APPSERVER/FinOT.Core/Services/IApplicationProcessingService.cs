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
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition);
        ReturnResult<bool> SaveTenantRentalIncrementInfo(TenantPetitionInfoM petition);
        ReturnResult<bool> SaveTenantLostServiceInfo(TenantPetitionInfoM petition);
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo);
        ReturnResult<bool> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<bool> AddAnotherOpposingParty(CaseInfoM caseInfo);
        ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(CaseInfoM caseInfo);
        

    }
}
