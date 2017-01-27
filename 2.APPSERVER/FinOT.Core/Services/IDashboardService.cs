using RAP.Core.DataModels;
using RAP.Core.Common;
using System.Collections.Generic;

namespace RAP.Core.Services
{
    public interface IDashboardService
    {
        string CorrelationId { get; set; }
        ReturnResult<List<ActivityStatus_M>> GetActivityStatusForCase(int C_ID);
        ReturnResult<List<Status_M>> GetStatus(int activityID);
        ReturnResult<List<Activity_M>> GetActivity();
        ReturnResult<bool> SaveNewActivityStatus(ActivityStatus_M activityStatus, int C_ID);
        ReturnResult<List<CityUserAccount_M>> GetHearingOfficers();
        ReturnResult<List<CityUserAccount_M>> GetAnalysts();
        ReturnResult<bool> AssignAnalyst(int cID, int AnalystUserID);
        ReturnResult<bool> AssignHearingOfficer(int cID, int HearingOfficerUserID);
        ReturnResult<List<DocumentM>> GetCaseDocuments(int c_id);
        ReturnResult<List<DocumentM>> SaveCaseDocuments(List<DocumentM> documents);
        ReturnResult<CustomEmailM> GetCustomEmail(int c_id);
        ReturnResult<CustomEmailM> GetCustomEmailNotification(int c_id, int ActivityID, int NotificationID);
        ReturnResult<MailM> GetMailNotification(int NotificationID);
        ReturnResult<CustomEmailM> SubmitCustomEmail(CustomEmailM cMail);
        ReturnResult<MailM> GetMail();
        ReturnResult<MailM> SubmitMail(MailM mail);
        ReturnResult<SearchCaseResult> GetCaseSearch(CaseSearch caseSearch);

    }
}
