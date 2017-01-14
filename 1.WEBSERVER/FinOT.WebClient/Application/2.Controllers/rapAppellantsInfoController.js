'use strict';
var rapAppellantsInfoController = ['$scope', '$modal', 'alertService', 'rapappellantsinfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.TenantAppealInfo;
    self.Calender = masterFactory.Calender;
    self.CaseID = self.caseinfo.CaseID;
    self.bEditApplicant = false;
    self.bCaseFiledByThirdParty = self.caseinfo.bCaseFiledByThirdParty;
    self.bShowApplicantInfo = false;
    if (self.caseinfo.CaseID != null)
    {
        self.bShowApplicantInfo = true;
     }
    self.StateList = [];
    self.bEditRepresentative = false;
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.StateList = response.data;
        });
    }
    _GetStateList();
    self.GetCaseInfoWithModel = function (CaseID) {
        rapFactory.GetCaseInfoWithModel(CaseID, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
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

            }
            if (self.caseinfo.bCaseFiledByThirdParty == true)
            {

            }
            self.bShowApplicantInfo = true;


            });
        }
            

    
    self.EditApplicantName = function () {
        self.bEditApplicant = true;
    }
    
    self.ContinueToGroundsforAppeal = function (model) {        
        rapFactory.SaveTenantAppealInfo(model, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
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