using System;

namespace RAP.Core.Common
{
   

    public enum ActivityDefaults
    {
        ActivityPetitionFiled = 1,
        AppealFiled = 26,
        ResponseFiled = 27,
        AdditionalDocumentation = 24,
        OwnerResponse = 35
    }
    public enum StatusDefaults
    {
        StatusSubmitted = 1,
        NotificationSent = 6,
        InProcess = 2
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
