'use strict';
var rapOwnerApplicantInfoController = ['$scope', '$modal', 'alertService', 'rapOwnerApplicantInfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.OwnerPetitionInfo.ApplicantInfo.CustomerID = self.custDetails.custID;
    self.StateList = [];
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.StateList = response.data;
        });
    }
    _GetStateList();
    self.caseinfo.CaseFileBy = self.custDetails.custID;
    if (self.caseinfo.bCaseFiledByThirdParty == false)
    {
        self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo = self.custDetails.User;        
    }
    else
    {
        self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser = self.custDetails.User;
    }
    rapFactory.GetApplicationInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
    });

    var range = 10 / 2;
    var currentYear = new Date().getFullYear();
    self.years = [];
    for (var i = range; i > 0 ; i--) {

        self.years.push(currentYear - i);
    }
    for (var i = 0; i < range + 1; i++) {
        self.years.push(currentYear + i);
    }

    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveApplicationInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;            
        });
        $scope.model.ownerApplicantInfo = false;
        $scope.model.ownerJustification = true;
    }
 
}];
var rapApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerApplicantInfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}