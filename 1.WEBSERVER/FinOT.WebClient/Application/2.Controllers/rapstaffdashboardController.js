﻿'use strict';
var rapstaffdashboardController = ['$scope', '$modal', 'alertService', 'rapstaffdashboardFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = rapGlobalFactory.CityUser;
    self.CaseList = [];
    self.Analysts = [];
    self.HearingOfficers = [];
    self.InviteThirdPartyUser = function () {
        $location.path("/invitethirdparty");
    }
    self.FilePetition = function () {
        $location.path("/filePetition");
    }
    self.NewCaseStatus = function () {
        $location.path("/newCaseStatus");
    }
    self.AccountSearch = function () {
        $location.path("/accountSearch");
    }
   
    self.FileAppeal = function (model) {
        //self.caseinfo.CaseID = 
        rapFactory.GetCaseInfoWithModel(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }

            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
        });
        $location.path("/fileappeal");
    }

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
    var _GetCasesNoAnalyst = function () {
        rapFactory.GetCasesNoAnalyst().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.CaseList = response.data;
        });
    }
    _GetCasesNoAnalyst();

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
    // _getrent();
    if (self.caseinfo == null) {
        _GetCaseInfo();
    }

    self.AssignAnalyst = function (C_ID, AnalystUserID) {

        rapFactory.AssignAnalyst(C_ID, AnalystUserID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            _GetCasesNoAnalyst();
        });
    }

    self.AssignHearingOfficer = function (C_ID, HearingOfficerUserID) {

        rapFactory.AssignHearingOfficer(C_ID, HearingOfficerUserID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            _GetCasesNoAnalyst();
        });
    }
    

}];
var rapstaffdashboardController_resolve = {
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