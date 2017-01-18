'use strict';
var rapApplicationInfoController = ['$scope', '$modal', 'alertService', 'rapapplicationinfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Hide = true;
    self.Error = "";

    var _GetTenantApplicationInfo = function (custID) {
        rapFactory.GetTenantApplicationInfo(custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
                }
            self.caseinfo.TenantPetitionInfo = response.data;
            if (self.caseinfo.bCaseFiledByThirdParty == false) {
                self.caseinfo.TenantPetitionInfo.ApplicantUserInfo = self.custDetails.User;
                self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.Email = self.custDetails.email;
            }
            else {
                self.caseinfo.TenantPetitionInfo.ThirdPartyUser = self.custDetails.User;
                self.caseinfo.TenantPetitionInfo.ThirdPartyUser.Email = self.custDetails.email;
            }
            self.caseinfo.TenantPetitionInfo.CustomerID = self.custDetails.custID;
            
        });
    }
    _GetTenantApplicationInfo(self.custDetails.custID);

    self.StateList = [];
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
    }
            self.StateList = response.data;
            
        });
    }
    _GetStateList();
    
    self.ChangeSameAsPropertyOwner = function ()
    {
        if (self.caseinfo.TenantPetitionInfo.bSameAsOwnerInfo == true)
        {
            //self.caseinfo.TenantPetitionInfo.PropertyManager.FirstName = self.caseinfo.TenantPetitionInfo.OwnerInfo.FirstName;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.LastName = self.caseinfo.TenantPetitionInfo.OwnerInfo.LastName;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.AddressLine1 = self.caseinfo.TenantPetitionInfo.OwnerInfo.AddressLine1;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.AddressLine2 = self.caseinfo.TenantPetitionInfo.OwnerInfo.AddressLine2;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.State = self.caseinfo.TenantPetitionInfo.OwnerInfo.State;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.City = self.caseinfo.TenantPetitionInfo.OwnerInfo.City;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.Zip = self.caseinfo.TenantPetitionInfo.OwnerInfo.Zip;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.PhoneNumber = self.caseinfo.TenantPetitionInfo.OwnerInfo.PhoneNumber;
            //self.caseinfo.TenantPetitionInfo.PropertyManager.Email = self.caseinfo.TenantPetitionInfo.OwnerInfo.Email;
            self.caseinfo.TenantPetitionInfo.PropertyManager = angular.copy(self.caseinfo.TenantPetitionInfo.OwnerInfo);
        }
        else
        {
            self.caseinfo.TenantPetitionInfo.PropertyManager.FirstName = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.LastName = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.BusinessName = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.AddressLine1 = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.AddressLine2 = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.State = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.City = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.Zip = "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.PhoneNumber= "";
            self.caseinfo.TenantPetitionInfo.PropertyManager.Email = "";
        }

    }

    self.ChangeAccountInformation = function () {
        rapGlobalFactory.IsEdit = true;
        $location.path("/editcustomerinformation");
    }

    self.ContinueToGroundsforPetition = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveApplicationInfo(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            $scope.model.bAppInfo = false;
            $scope.model.bGrounds = true;
            $scope.model.tPetionActiveStatus.PetitionCategory = true;
            $scope.model.tPetionActiveStatus.ImportantInformation = true;
            $scope.model.tPetionActiveStatus.ApplicantInformation = true;
           
         });     
    }
}];
var rapApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapapplicationinfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}