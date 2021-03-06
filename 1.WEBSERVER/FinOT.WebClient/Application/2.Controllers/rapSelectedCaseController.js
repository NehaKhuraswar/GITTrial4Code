﻿'use strict';
var rapSelectedCaseController = ['$scope', '$modal', 'alertService', 'rapSelectedCaseFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    if (rapGlobalFactory.SelectedCase == null || rapGlobalFactory.SelectedCase == undefined) {
        $location.path("/staffdashboard");
    }
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.model = rapGlobalFactory.CityUser;
    self.CaseList = [];
    self.Analysts = [];
    self.HearingOfficers = [];
    self.apnAddress = null;
    self.EditAPNAddress = false;
    self.EditAPNNumber = false;
    self.Error = '';

    // Setting the local APN NUmber after getting it back
    if (self.caseinfo.TenantPetitionInfo != null) {
        self.APNNumber = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.apnAddress.APNNumber;
    }
    if (self.caseinfo.OwnerPetitionInfo != null) {
        self.APNNumber = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.apnAddress.APNNumber;
    }
    self.Home = function () {
        $location.path("/staffdashboard");
    }
    self.NewCaseStatus = function () {
        $location.path("/newCaseStatus");
    }
    self.AdditionalDocs = function () {
        $location.path("/additionaldocuments");
    }
    self.SendEmailNotification = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $location.path("/sendemailnotification");
    }
    self.USMailNotification = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $location.path("/usmailnotification");
    }
    var _GetSelectedCase = function (C_ID) {
        rapFactory.GetSelectedCase(C_ID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.caseinfo = response.data;
            // Setting the local APN NUmber after getting it back
            if (self.caseinfo.TenantPetitionInfo != null) {
                self.APNNumber = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.apnAddress.APNNumber;
            }
            if (self.caseinfo.OwnerPetitionInfo != null) {
                self.APNNumber = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.apnAddress.APNNumber;
            }
            checkRecipients();
        });
    }
    _GetSelectedCase(self.caseinfo.C_ID);

    self.Documents = null;

    var _GetCaseDocuments = function(C_ID) {
        rapFactory.GetCaseDocuments(C_ID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
        self.Documents = response.data;
        });
    }
    _GetCaseDocuments(self.caseinfo.C_ID);
    $anchorScroll();

    self.GetCaseActivityStatus = function (model) {
        //self.caseinfo.CaseID = 
        rapFactory.GetCaseActivityStatus(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.caseinfo.ActivityStatus = response.data;

            // self.caseinfo = response.data;
        //rapGlobalFactory.CaseDetails = self.caseinfo;
    });
    // $location.path("/fileappeal");
    }

    self.Download = function (doc) {
       masterFactory.GetDocument(doc);

    }
    var _GetAnalysts = function () {
        masterFactory.GetAnalysts().then(function (response) {
               if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.Analysts = response.data;
        });
        }
    _GetAnalysts();

    var _GetHearingOfficers = function () {
        masterFactory.GetHearingOfficers().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.HearingOfficers = response.data;
        });
        }
    _GetHearingOfficers();
    var _GetCaseInfo = function () {

        rapFactory.GetCaseInfo().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }

            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
});
}
    //if (self.caseinfo == null) {
    //    _GetCaseInfo();
    //}


    self.AssignAnalyst = function (C_ID, Analyst) {

        masterFactory.AssignAnalyst(C_ID, Analyst.UserID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.caseinfo.CityAnalyst.FirstName = Analyst.FirstName;
            self.caseinfo.CityAnalyst.LastName = Analyst.LastName;
});
}

self.ViewPage = function (activity, caseinfo) {
    if (activity.Activity.ActivityID == 1) {
        rapFactory.GetPetitionViewInfo(caseinfo.C_ID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            //self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = response.data;
            rapGlobalFactory.FromSelectedCase = true;
            if (rapGlobalFactory.CaseDetails.PetitionCategoryID == 1) {
                    $location.path("/ViewPetition");
            }
            else {
                    $location.path("/ViewownerPetition");
        }

        });
    }
    else if (activity.Activity.ActivityID == 26) {
        rapFactory.GetAppealInfoForView(caseinfo.C_ID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
    self.caseinfo = response.data;
    rapGlobalFactory.CaseDetails = self.caseinfo;
                rapGlobalFactory.FromSelectedCase = true;
            $location.path("/ViewAppeal");
    });
    }
    else if (activity.Activity.ActivityID == 35) {
        rapFactory.GetOResponseViewByCaseID(caseinfo.C_ID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
            rapGlobalFactory.FromSelectedCase = true;
            $location.path("/ViewOwnerResponse");
        });
    }
    else if (activity.Activity.ActivityID == 27) {
        rapFactory.GetTenantResponseViewInfo(caseinfo.C_ID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
            rapGlobalFactory.FromSelectedCase = true;
            $location.path("/ViewTenantResponse");
        });
    }
    else if (activity.Status.StatusID == 6) {
        if (activity.NotificationType == 1)
        {
            rapFactory.GetCustomEmailNotification(caseinfo.C_ID, activity.Activity.ActivityID, activity.NotificationID).then(function (response) {
                if (!alert.checkForResponse(response)) {
                    return;
                }
                rapGlobalFactory.Notification = response.data;
                rapGlobalFactory.Notification_CaseID = self.caseinfo.CaseID;
                rapGlobalFactory.FromSelectedCase = true;
                $location.path("/emailnotificationsent");
            });
        }  
        else if (activity.NotificationType == 2)
        {
            rapFactory.GetMailNotification(activity.NotificationID).then(function (response) {
                if (!alert.checkForResponse(response)) {
                    return;
                }
                rapGlobalFactory.MailNotification = response.data;
                rapGlobalFactory.Notification_CaseID = self.caseinfo.CaseID;
                rapGlobalFactory.FromSelectedCase = true;
                $location.path("/usmailnotificationsent");
            });
        }     
    }

}

self.AssignHearingOfficer = function (C_ID, HearingOfficer) {

    masterFactory.AssignHearingOfficer(C_ID, HearingOfficer.UserID).then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
            self.caseinfo.HearingOfficer.FirstName = HearingOfficer.FirstName;
            self.caseinfo.HearingOfficer.LastName = HearingOfficer.LastName;
});
}

self.UpdateAPNAddress = function (apnAddress) {
    apnAddress.APNNumber = self.APNNumber;
    masterFactory.UpdateAPNAddress(apnAddress).then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
            self.EditAPNAddress = false;
        });
        }
    self.UpdateAPNNumber = function (apnAddress) {
        masterFactory.UpdateAPNAddress(apnAddress).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.EditAPNNumber = false;
            if (self.caseinfo.PetitionCategoryID == 1) {
                self.APNNumber = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.apnAddress.APNNumber;
            }
            if (self.caseinfo.PetitionCategoryID == 2) {
                self.APNNumber = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.apnAddress.APNNumber;
            }
});
    }
    self.bEnableSendEmail = false
    var checkRecipients = function () {
        if (self.caseinfo.PetitionCategoryID == 1) {
            if (self.caseinfo.TenantPetitionInfo != null) {
                if (!self.caseinfo.bCaseFiledByThirdParty) {
                    if (self.caseinfo.TenantPetitionInfo.bApplicantEmailNotification) {
                        if (self.caseinfo.TenantPetitionInfo.ApplicantUserInfo != null && self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.Email != null) {
                            self.bEnableSendEmail = true;
                        }
                    }
                }
                //if (self.caseinfo.TenantPetitionInfo.OwnerInfo != null && self.caseinfo.TenantPetitionInfo.OwnerInfo.Email != null) {
                //    self.OwnerEmail = self.caseinfo.TenantPetitionInfo.OwnerInfo.Email;
                //}
                if (self.caseinfo.bCaseFiledByThirdParty || self.caseinfo.TenantPetitionInfo.bThirdPartyRepresentation) {
                    if (self.caseinfo.TenantPetitionInfo.ThirdPartyEmailNotification) {
                        if (self.caseinfo.TenantPetitionInfo.ThirdPartyInfo != null && self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.Email != null) {
                            self.bEnableSendEmail = true;
                        }
                    }
                }
            }
        }
        else {
            if (self.caseinfo.OwnerPetitionInfo != null) {
                if (!self.caseinfo.bCaseFiledByThirdParty) {
                    if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.bApplicantEmailNotification) {
                        if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo != null) {
                            if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.Email != null)
                                self.bEnableSendEmail = true;
                        }
                    }
                }
                if (self.caseinfo.bCaseFiledByThirdParty || self.caseinfo.OwnerPetitionInfo.ApplicantInfo.bThirdPartyRepresentation) {
                    if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyEmailNotification) {
                        if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser != null) {
                            if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.Email != null) {
                                self.bEnableSendEmail = true;
                            }
                        }
                    }
                }
                //if (self.caseinfo.OwnerPetitionInfo.PropertyInfo != null && self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo != null) {
                //    for (i = 0; i < self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length; i++) {
                //        if (i == 0) {
                //            if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email != null) {
                //                self.TenantEmail = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email;
                //            }
                //        }
                //        if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email != null) {
                //            self.TenantEmail = self.TenantEmail + ',' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email;
                //        }
                //    }
            }
        }
    }

}];
var rapSelectedCaseController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //    $scope.model = response.data;
        //    //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}