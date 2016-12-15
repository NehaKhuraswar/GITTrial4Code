﻿'use strict';
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

   self.Calender = masterFactory.Calender;   

    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveApplicationInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;            
        });
        $scope.model.ownerApplicantInfo = false;
        $scope.model.ownerJustification = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oPetionCurrentStatus.JustificationForRentIncrease = true;
        $scope.model.oPetionActiveStatus.PetitionCategory = true;
        $scope.model.oPetionActiveStatus.ImportantInformation = true;
        $scope.model.oPetionActiveStatus.ApplicantInformation = true;
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