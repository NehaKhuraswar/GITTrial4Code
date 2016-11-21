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
        ReturnResult<TenantAppealInfoM> SaveTenantAppealInfo(CaseInfoM caseInfo);
        ReturnResult<Boolean> SaveAppealGroundInfo(TenantAppealInfoM tenantAppealInfo);

     //   ReturnResult<CaseInfoM> GetCaseDetails(int caseID);
    }
}
