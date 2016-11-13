using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface IApplicationProcessingService
    {
        string CorrelationId { get; set; }
        ReturnResult<CaseInfoM> GetCaseDetails();
        ReturnResult<CaseInfoM> SaveCaseDetails(CaseInfoM caseInfo);

    }
}
