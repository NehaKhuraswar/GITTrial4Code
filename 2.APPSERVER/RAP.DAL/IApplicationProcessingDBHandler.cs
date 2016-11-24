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
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
        ReturnResult<CaseInfoM> SaveApplicationInfo(CaseInfoM caseInfo, int UserID);
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo);
        ReturnResult<Boolean> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);
        ReturnResult<bool> AddAnotherOpposingParty(CaseInfoM caseInfo);
        ReturnResult<TenantAppealInfoM> SaveTenantServingAppeal(CaseInfoM caseInfo);
       

     //   ReturnResult<CaseInfoM> GetCaseDetails(int caseID);
    }
}
