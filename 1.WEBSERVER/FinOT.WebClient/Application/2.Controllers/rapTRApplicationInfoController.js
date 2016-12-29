﻿'use strict';
var rapTRApplicationInfoController = ['$scope', '$modal', 'alertService', 'rapTRapplicationinfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.CaseID;
    self.bCaseInfo = false;

    self.GetTenantResponseApplicationInfo = function (CaseNumber) {
        rapFactory.GetTenantResponseApplicationInfo(CaseNumber, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo = response.data;
            self.caseinfo.bCaseFiledByThirdParty = rapGlobalFactory.bCaseFiledByThirdParty;
            rapGlobalFactory.bCaseFiledByThirdParty = false;
            if (self.caseinfo.bCaseFiledByThirdParty == false) {
                self.caseinfo.TenantResponseInfo.ApplicantUserInfo = angular.copy(self.custDetails.User);
            }
            else {
                self.caseinfo.TenantResponseInfo.ThirdPartyUser = self.custDetails.User;
            }
            self.caseinfo.TenantResponseInfo.CustomerID = self.custDetails.custID;
            self.bCaseInfo = true;
            rapGlobalFactory.CaseDetails = self.caseinfo;

        });
    }
   // _GetTenantResponseApplicationInfo(self.custDetails.custID);

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

    self.ChangeSameAsPropertyOwner = function () {
        if (self.caseinfo.TenantResponseInfo.bSameAsOwnerInfo == true) {
            //self.caseinfo.TenantPetitionInfo.PropertyManager.FirstName = self.caseinfo.TenantPetitionInfo.OwnerInfo.FirstName;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.LastName = self.caseinfo.TenantPetitionInfo.OwnerInfo.LastName;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.AddressLine1 = self.caseinfo.TenantPetitionInfo.OwnerInfo.AddressLine1;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.AddressLine2 = self.caseinfo.TenantPetitionInfo.OwnerInfo.AddressLine2;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.State = self.caseinfo.TenantPetitionInfo.OwnerInfo.State;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.City = self.caseinfo.TenantPetitionInfo.OwnerInfo.City;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.Zip = self.caseinfo.TenantPetitionInfo.OwnerInfo.Zip;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.PhoneNumber = self.caseinfo.TenantPetitionInfo.OwnerInfo.PhoneNumber;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.Email = self.caseinfo.TenantPetitionInfo.OwnerInfo.Email;
            self.caseinfo.TenantResponseInfo.PropertyManager = angular.copy(self.caseinfo.TenantResponseInfo.OwnerInfo);
        }
        else {
            self.caseinfo.TenantResponseInfo.PropertyManager.FirstName = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.LastName = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.BusinessName = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.AddressLine1 = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.AddressLine2 = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.State = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.City = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.Zip = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.PhoneNumber = "";
            self.caseinfo.TenantResponseInfo.PropertyManager.Email = "";
        }

    }

    self.ChangeAccountInformation = function () {
        rapGlobalFactory.IsEdit = true;
        $location.path("/editcustomerinformation");
    }

    self.ContinueToExemptionContested = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveTenantResponseApplicationInfo(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
            $scope.model.bAppInfo = false;
            $scope.model.bExemptionContested = true;
            $scope.model.TRSubmissionStatus.PetitionType = true;
            $scope.model.TRSubmissionStatus.ImportantInformation = true;
            $scope.model.TRSubmissionStatus.ApplicantInformation = true;

        });
    }
}];
var rapTRApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapapplicationinfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}