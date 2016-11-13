using System;
using RAP.Core.Common;
using RAP.Core.DataModels;

namespace RAP.DAL
{
   public interface IApplicationProcessingDBHandler
    {
        ReturnResult<CaseInfoM> GetCaseDetails();
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);
    }
}
