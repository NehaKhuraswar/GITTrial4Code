using System;
using RAP.Core.Common;
using RAP.Core.DataModels;
using System.Collections.Generic;

namespace RAP.DAL
{
   public interface IApplicationProcessingDBHandler
    {
        ReturnResult<CaseInfoM> GetCaseDetails();
        ReturnResult<CaseInfoM> GetCaseDetails(string caseID);
        ReturnResult<CaseInfoM> GetCaseDetails(int UserID);
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SubmitTenantPetition(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<TenantPetitionInfoM> GetTenantApplicationInfo(int CustomerID);
        ReturnResult<List<CaseInfoM>> GetCasesNoAnalyst();
        ReturnResult<TenantRentalHistoryM> GetRentalHistoryInfo(int PetitionId);
        ReturnResult<bool> SaveTenantRentalHistoryInfo(TenantRentalHistoryM rentalHistory);
        ReturnResult<List<PetitionGroundM>> GetPetitionGroundInfo(int petitionID);
        ReturnResult<bool> SavePetitionGroundInfo(TenantPetitionInfoM petition);
        ReturnResult<bool> SaveTenantLostServiceInfo(LostServicesPageM message);
        ReturnResult<LostServicesPageM> GetTenantLostServiceInfo(int PetitionID);
        ReturnResult<TenantPetitionInfoM> GetTenantReviewInfo(int CustomerID);
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo);
        ReturnResult<Boolean> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<bool> AddAnotherOpposingParty(CaseInfoM caseInfo);
        ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> GetPetitioncategory();
        ReturnResult<CaseInfoM> GetOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<OwnerPetitionPropertyInfoM> GetOwnerPropertyAndTenantInfo(OwnerPetitionPropertyInfoM model);
        ReturnResult<List<OwnerRentIncreaseReasonsM>> GetRentIncreaseReasonInfo(OwnerPetitionInfoM petition);
        ReturnResult<CaseInfoM> GetOwnerRentIncreaseAndPropertyInfo(CaseInfoM model);
        ReturnResult<CaseInfoM> SaveOwnerApplicantInfo(CaseInfoM model);
        ReturnResult<bool> SaveRentIncreaseReasonInfo(OwnerPetitionInfoM petition);
        ReturnResult<OwnerPetitionPropertyInfoM> SaveOwnerPropertyAndTenantInfo(OwnerPetitionPropertyInfoM model);
        ReturnResult<OwnerPetitionPropertyInfoM> SaveOwnerRentIncreaseAndUpdatePropertyInfo(OwnerPetitionPropertyInfoM model);
        ReturnResult<CaseInfoM> SubmitOwnerPetition(CaseInfoM model);


     //   ReturnResult<CaseInfoM> GetCaseDetails(int caseID);
    }
}
