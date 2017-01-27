using System;
using RAP.Core.Common;
using RAP.Core.DataModels;
using System.Collections.Generic;

namespace RAP.DAL
{
   public interface ICommonDBHandler
    {
       ReturnResult<UserInfoM> SaveUserInfo(UserInfoM userInfo);
       ReturnResult<UserInfoM> GetUserInfo(int UserId);
       ReturnResult<APNAddress> UpdateAPNAddress(APNAddress apnAddress);
       void SaveErrorLog(OperationStatus status);
       CustomDate GetDateFromDatabase(DateTime DatabaseDate);
       ReturnResult<DocumentM> SaveDocument(DocumentM doc);
       ReturnResult<List<DocumentM>> GetDocuments(int CustmerID, bool isPetitiofiled, string docTitle = null);
       ReturnResult<bool> PetitionFiledActivity(int C_ID, int CaseFileBy, int ActivityID, int StatusID);
       ReturnResult<List<string>> GetDocDescription();
       ReturnResult<List<DocumentM>> GetCaseDocuments(int c_id);
       ReturnResult<DocumentM> SaveCaseDocument(DocumentM doc);
       ReturnResult<List<DocumentM>> GetDocumentsByCategory(int CustmerID, bool isPetitiofiled, string docCategory);
       ReturnResult<bool> UpdateDocumentCaseInfo(int CustmerID, int C_ID, string docCategory);
       ReturnResult<APNAddress> GetAPNAddress(int UserID);
       ReturnResult<EmailM> SaveCustomEmailNotification(EmailM message, int employeeID, int c_id, int activityID);
       ReturnResult<MailM> SaveMailNotification(MailM message);
       ReturnResult<bool> MailSentActivity(int C_ID, int SentBy, int ActivityID, int NotificationID);
       ReturnResult<CustomEmailM> GetCustomEmailNotification(int c_id, int ActivityID, int NotificationID);
    }
}
