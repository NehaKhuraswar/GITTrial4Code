using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface ICommonService
    {
        void SaveErrorDetails(string ErrorNumber, string ErrorMessage, string ErrorMessageDetails, int CustID, string OperationName);

    }
}
