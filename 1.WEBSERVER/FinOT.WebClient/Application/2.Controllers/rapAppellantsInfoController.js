'use strict';
var rapAppellantsInfoController = ['$scope', '$modal', 'alertService', 'rapappellantsinfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.TenantAppealInfo;
    self.Calender = masterFactory.Calender;
    self.CaseID;
    self.bCaseFiledByThirdParty = self.caseinfo.bCaseFiledByThirdParty;
    self.bShowApplicantInfo = false;
    self.GetCaseInfoWithModel = function (CaseID) {
        rapFactory.GetCaseInfoWithModel(CaseID, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
            self.TenantAppealInfo = rapGlobalFactory.CaseDetails.TenantAppealInfo;
            if (self.TenantAppealInfo.AppealID == 0)
            {
                self.TenantAppealInfo.ApplicantUserInfo = angular.copy(self.custDetails.User);
                self.TenantAppealInfo.ApplicantUserInfo.Email = angular.copy(self.custDetails.email);
                self.TenantAppealInfo.AppealPropertyUserInfo = angular.copy(self.custDetails.User);
                self.TenantAppealInfo.AppealPropertyUserInfo.Email = angular.copy(self.custDetails.email);
            }
            self.bShowApplicantInfo = true;
            self.caseinfo.bCaseFiledByThirdParty = self.bCaseFiledByThirdParty;
            
        });
    }

    

    
    self.ContinueToGroundsforAppeal = function (model) {        
        rapFactory.SaveTenantAppealInfo(model, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            rapGlobalFactory.CaseDetails.TenantAppealInfo = response.data;
            self.caseinfo = rapGlobalFactory.CaseDetails.TenantAppealInfo;
            $scope.model.bAppellantInfo = false;
            $scope.model.bGrounds = true;
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