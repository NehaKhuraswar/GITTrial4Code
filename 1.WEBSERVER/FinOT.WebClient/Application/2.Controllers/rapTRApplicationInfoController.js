'use strict';
var rapTRApplicationInfoController = ['$scope', '$modal', 'alertService', 'rapTRapplicationinfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 3;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.bCaseInfo = false;
    self.Error = "";
    self.bAcknowledgeNotification = false;
    self.bEditThirdParty = false;
    self.CaseID;
    if (self.caseinfo != null) {
        self.CaseID = self.caseinfo.CaseID;
        if (self.caseinfo.CaseID != null) {
            self.bCaseInfo = true;
        }        
    }
    
    
    self.bEditRepresentative = false;
    self.GetTenantResponseApplicationInfo = function (CaseNumber) {
        self.Error = "";
        rapFactory.GetTenantResponseApplicationInfo(CaseNumber, self.custDetails.custID).then(function (response) {
             if (!alert.checkForResponse(response)) {
                 self.Error = rapGlobalFactory.Error;
                 $anchorScroll();
                return;
                }
             self.caseinfo = response.data;
             if (self.caseinfo.CaseID != null) {

                 self.caseinfo.bCaseFiledByThirdParty = rapGlobalFactory.bCaseFiledByThirdParty;
                 rapGlobalFactory.bCaseFiledByThirdParty = false;
                 if (self.caseinfo.bCaseFiledByThirdParty == false) {
                     self.caseinfo.TenantResponseInfo.ApplicantUserInfo = angular.copy(self.custDetails.User);
                     self.caseinfo.TenantResponseInfo.ApplicantUserInfo.Email = angular.copy(self.custDetails.email);
                 }
                 //else {
                 //   // self.caseinfo.TenantResponseInfo.ThirdPartyUser = self.custDetails.User;
                 //}
                 self.caseinfo.TenantResponseInfo.CustomerID = self.custDetails.custID;
                 self.bCaseInfo = true;
                 rapGlobalFactory.CaseDetails = self.caseinfo;
                 self.CaseID = self.caseinfo.CaseID;
                 $anchorScroll();
             }
        });
    }
    //if (self.caseinfo)
   // _GetTenantResponseApplicationInfo(self.custDetails.custID);
    self.GetTenantResponseApplicationInfo(null);

    self.StateList = [];
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
             if (!alert.checkForResponse(response)) {
                 self.Error = rapGlobalFactory.Error;
                  $anchorScroll();
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
    var _CheckNotification = function () {
        var bInValid = false;
        if (self.caseinfo.bCaseFiledByThirdParty == false && self.caseinfo.TenantResponseInfo.bThirdPartyRepresentation == true && (self.caseinfo.TenantResponseInfo.ThirdPartyInfo.UserID == 0 || self.bEditThirdParty == true)) {
            if (!(self.caseinfo.TenantResponseInfo.ThirdPartyMailNotification || self.caseinfo.TenantResponseInfo.ThirdPartyEmailNotification)) {
                self.Error = 'Third party notification preference is required';
                $anchorScroll();
                bInValid = true;
            }
            else if (!self.bAcknowledgeNotification) {
                self.Error = 'Please acknowledge Third party notification preference';
                $anchorScroll();
                bInValid = true;
            }
        }
        return bInValid;
    }
    self.ChangeAccountInformation = function () {
        rapGlobalFactory.IsEdit = true;
        $location.path("/editcustomerinformation");
    }

    self.ContinueToExemptionContested = function () {
        if (_CheckNotification()) {
            return;
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        if (self.caseinfo.CaseID == null) {
            self.Error = 'Please load the case information before proceeding';
            $anchorScroll();
            return;
        }

        rapFactory.SaveTenantResponseApplicationInfo(rapGlobalFactory.CaseDetails, self.custDetails.custID).then(function (response) {
             if (!alert.checkForResponse(response)) {
                 self.Error = rapGlobalFactory.Error;
                 $anchorScroll();
                 return;
        }
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