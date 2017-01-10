'use strict';
var rapSelectedCaseController = ['$scope', '$modal', 'alertService', 'rapSelectedCaseFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.model = rapGlobalFactory.CityUser;
    self.CaseList = [];
    self.Analysts = [];
    self.HearingOfficers = [];
    self.apnAddress = null;
    self.EditAPNAddress = false;
    self.EditAPNNumber = false;
    self.APNNumber = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.apnAddress.APNNumber;
    
    self.NewCaseStatus = function () {
        $location.path("/newCaseStatus");
    }
    self.AdditionalDocs = function () {
        $location.path("/additionaldocuments");
    }
    self.SendEmailNotification = function () {
        $location.path("/sendemailnotification");
    }
    self.USMailNotification = function () {
        $location.path("/usmailnotification");
    }
    var _GetSelectedCase = function (C_ID) {
        rapFactory.GetSelectedCase(C_ID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo = response.data;
        });
    }
    _GetSelectedCase(self.caseinfo.C_ID);

    self.GetCaseActivityStatus = function (model) {
        //self.caseinfo.CaseID = 
        rapFactory.GetCaseActivityStatus(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.ActivityStatus = response.data;

           // self.caseinfo = response.data;
            //rapGlobalFactory.CaseDetails = self.caseinfo;
        });
       // $location.path("/fileappeal");
    }
   

    var _GetAnalysts = function () {
        masterFactory.GetAnalysts().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.Analysts = response.data;
        });
    }
    _GetAnalysts();

    var _GetHearingOfficers = function () {
        masterFactory.GetHearingOfficers().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.HearingOfficers = response.data;
        });
    }
    _GetHearingOfficers();
    var _GetCaseInfo = function () {

        rapFactory.GetCaseInfo().then(function (response) {
            if (!alert.checkResponse(response)) {
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
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.CityAnalyst.FirstName = Analyst.FirstName;
            self.caseinfo.CityAnalyst.LastName = Analyst.LastName;
        });
    }

    self.ViewPage = function (activity, caseinfo) {
        if (activity.Activity.ActivityID == 1) {
            rapFactory.GetPetitionViewInfo(caseinfo.C_ID).then(function (response) {
                if (!alert.checkResponse(response)) {
                    return;
                }
                //self.caseinfo = response.data;
                rapGlobalFactory.CaseDetails = response.data;
                rapGlobalFactory.FromSelectedCase = true;
                $location.path("/ViewPetition");
            });
        }

    }

    self.AssignHearingOfficer = function (C_ID, HearingOfficer) {

        masterFactory.AssignHearingOfficer(C_ID, HearingOfficer.UserID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.HearingOfficer.FirstName = HearingOfficer.FirstName;
            self.caseinfo.HearingOfficer.LastName = HearingOfficer.LastName;
        });
    }

    self.UpdateAPNAddress = function (apnAddress) {
        apnAddress.APNNumber = self.APNNumber;
        masterFactory.UpdateAPNAddress(apnAddress).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.EditAPNAddress = false;           
        });
    }
    self.UpdateAPNNumber = function (apnAddress) {
        masterFactory.UpdateAPNAddress(apnAddress).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.EditAPNNumber = false;
            self.APNNumber = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.apnAddress.APNNumber;
        });
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