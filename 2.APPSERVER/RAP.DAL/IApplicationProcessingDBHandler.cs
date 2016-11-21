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
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(TenantAppealInfoM TenantAppealInfo);
        ReturnResult<Boolean> SaveAppealGroundInfo(List<AppealGroundM> AppealGrounds);

     //   ReturnResult<CaseInfoM> GetCaseDetails(int caseID);
    }
}
