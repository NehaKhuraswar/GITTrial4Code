using System;

namespace RAP.Core.Common
{
    public struct Constants
    {
        public const int APPLICATION_ID = 26;
        public const string FINAPISERVICE_ENDPOINT = "FINAPISERVICE_ENDPOINT";
        public const string FINLOGSERVICE_ENDPOINT = "FINLOGSERVICE_ENDPOINT";
    }

    public enum Action
    {
        LOGIN,
        CREATE,
        INSERT,
        UPDATE,
        RETRIEVE,
        VALIDATE,
        WEBERR,
        TIMER
    }
    public enum Severity
    {
        INFO,
        ACCESS,
        ERROR
    }
    public enum MessageType
    {
        JOBAID,
        DOCUMENT
    }

    public enum Context
    {
        Notes = 1,
        Save = 2,
        Approve = 3,
        Return = 4,
        Terminate = 5
    }

    public enum Role
    {
        Viewer = 2600,
        Originator = 2601
    }

    public enum RequestType
    {
        Regular=1,
        Stand_By=2,
        Agency_Wide=3
    }
    public enum CashOrComp
    {
        Cash = 1,
        Comp = 2
    }

    public enum ActivityDefaults
    {
        ActivityPetitionFiled = 1,
        AppealFiled = 26,
        ResponseFiled = 27,
        AdditionalDocumentation = 24
    }
    public enum StatusDefaults
    {
        StatusSubmitted = 1,
        NotificationSent = 2,
        InProcess = 14
    }
    public enum CityUserRoles
    {
        NonRapStaff = 1,
        AdminAssistant = 2,
        Analyst = 3,
        HearingOfficer = 4,
        CityAdmin = 5
    }
}
