using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface ICommonService
    {
        void LogError(OperationStatus status);
        ReturnResult<DocumentM> SaveDocument(DocumentM doc);
        ReturnResult<List<DocumentM>> GetDocuments(int CustmerID, bool isPetitiofiled, string docTitle = null);
        ReturnResult<List<string>> GetDocDescription();
        ReturnResult<List<DocumentM>> GetCaseDocuments(int c_id);
        ReturnResult<DocumentM> SaveCaseDocument(DocumentM doc);
        ReturnResult<APNAddress> UpdateAPNAddress(APNAddress apnAddress);
        ReturnResult<bool> SaveCustomEmailNotification(EmailM message, int employeeID, int c_id);
    }
}
