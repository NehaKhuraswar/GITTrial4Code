using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface ICommonService
    {
        void LogError(OperationStatus status);
        ReturnResult<DocumentM> SaveDocument(DocumentM doc);
    }
}
