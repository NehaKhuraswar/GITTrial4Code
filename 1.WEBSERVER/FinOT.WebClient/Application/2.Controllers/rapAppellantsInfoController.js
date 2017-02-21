'use strict';
var rapAppellantsInfoController = ['$scope', '$modal', 'alertService', 'rapappellantsinfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    $scope.model.stepNo = 3;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.TenantAppealInfo;
    self.Calender = masterFactory.Calender;
    self.CaseID = self.caseinfo.CaseID;
    self.bEditApplicant = false;
    self.Error = "";
    self.bAcknowledgeNotification = false;
    self.bCaseFiledByThirdParty = self.caseinfo.bCaseFiledByThirdParty;
    self.bShowApplicantInfo = false;
    self.AppealCategoryID  = 0;
    if (self.caseinfo.CaseID != null)
    {
        self.bShowApplicantInfo = true;
     }
    self.StateList = [];
    self.bEditRepresentative = false;
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.StateList = response.data;
            $anchorScroll();
        });
    }
    _GetStateList();
    self.GetCaseInfoWithModel = function (CaseID) {
        if (CaseID != null) {

            if (rapGlobalFactory.CaseDetails.CaseID == CaseID) {
                return;
            }
        }
        self.AppealCategoryID = rapGlobalFactory.CaseDetails.PetitionCategoryID;
        rapFactory.GetCaseInfoWithModel(CaseID, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                 $anchorScroll();
                return;
            }
            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
            self.TenantAppealInfo = rapGlobalFactory.CaseDetails.TenantAppealInfo;
            self.caseinfo.bCaseFiledByThirdParty = self.bCaseFiledByThirdParty;
            
            if (self.TenantAppealInfo.AppealID == 0) {
                if (self.caseinfo.bCaseFiledByThirdParty != true) {
                    self.TenantAppealInfo.ApplicantUserInfo = angular.copy(self.custDetails.User);
                    self.TenantAppealInfo.ApplicantUserInfo.Email = angular.copy(self.custDetails.email);
                }
                if (self.caseinfo.PetitionCategoryID == 1) {
                        self.TenantAppealInfo.AppealPropertyUserInfo = angular.copy(rapGlobalFactory.CaseDetails.TenantPetitionInfo.ApplicantUserInfo);
                }
                else if (self.caseinfo.PetitionCategoryID == 2){
                       self.TenantAppealInfo.AppealPropertyUserInfo = angular.copy(rapGlobalFactory.CaseDetails.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo);
                }
                self.TenantAppealInfo.AppealCategoryID = self.AppealCategoryID;
            }
            else {
                if (self.caseinfo.PetitionCategoryID == 1) {
                    self.TenantAppealInfo.AppealPropertyUserInfo = angular.copy(rapGlobalFactory.CaseDetails.TenantPetitionInfo.ApplicantUserInfo);
                }
                else if (self.caseinfo.PetitionCategoryID == 2) {
                    self.TenantAppealInfo.AppealPropertyUserInfo = angular.copy(rapGlobalFactory.CaseDetails.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo);
                }
                self.TenantAppealInfo.AppealCategoryID = self.AppealCategoryID;
            }
            if (self.caseinfo.bCaseFiledByThirdParty == true)
            {

            }
            rapGlobalFactory.CaseDetails.PetitionCategoryID = self.AppealCategoryID;
            if (rapGlobalFactory.CaseDetails.CaseID != null) {
                self.CaseID = rapGlobalFactory.CaseDetails.CaseID;
                self.bShowApplicantInfo = true;
            }


            });
    }
    self.GetCaseInfoWithModel(null);
    var _CheckNotification = function () {
        var bInValid = false;     
        if (self.caseinfo.bCaseFiledByThirdParty == false && self.bEditRepresentative == true) {
            if (!(self.caseinfo.TenantAppealInfo.ThirdPartyMailNotification || self.caseinfo.TenantAppealInfo.ThirdPartyEmailNotification)) {
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

    
    self.EditApplicantName = function () {
        self.bEditApplicant = true;
    }
    
    self.ContinueToGroundsforAppeal = function (model) {
        if (_CheckNotification()) {
            return;
        }
        model.TenantAppealInfo.AppealCategoryID = self.AppealCategoryID;
        rapFactory.SaveTenantAppealInfo(model, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                 $anchorScroll();
                return;
            }
            rapGlobalFactory.CaseDetails.TenantAppealInfo = response.data;
            self.caseinfo = rapGlobalFactory.CaseDetails;
            $scope.model.bAppellantInfo = false;
            $scope.model.bGrounds = true;
            $scope.model.AppealSubmissionStatus.ApplicantInformation = true;
            $scope.model.AppealSubmissionStatus.ImportantInformation = true;
            $scope.model.AppealSubmissionStatus.PetitionType = true;
        });
       // rapGlobalFactory.CaseDetails = self.caseinfo;
        
    }
}];
var rapAppellantsInfoController_resolve = {
    model: ['$route', 'alertService', 'rapappellantsinfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}