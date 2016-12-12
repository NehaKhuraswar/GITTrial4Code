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

    }
}
