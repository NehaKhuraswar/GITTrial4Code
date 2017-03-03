'use strict';
var rapReviewAppealController = ['$scope', '$modal', 'alertService', 'rapreviewappealFactory', '$location', 'rapGlobalFactory', 'rapAppealDocumentFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, rapAppealDocumentFactory, masterFactory, $anchorScroll) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Error = "";
    $scope.model.stepNo = 7;
    
    var _GetTenantAppealInfoForReview = function (AppealID) {
        rapFactory.GetTenantAppealInfoForReview(AppealID, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.caseinfo.TenantAppealInfo = response.data.TenantAppealInfo;
            self.caseinfo.CaseID = response.data.CaseID;
        });
    }
    _GetTenantAppealInfoForReview(self.caseinfo.TenantAppealInfo.AppealID);
   self.Documents = null;
   rapAppealDocumentFactory.GetAppealDocuments(self.custDetails.custID, 'A_AdditionalDocuments').then(function (response) {
       if (!alert.checkForResponse(response)) {
           self.Error = rapGlobalFactory.Error;
           return;
       }
       self.Documents = response.data;
       $anchorScroll();
   });
    self.SubmitAppeal = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.CaseFileBy = self.custDetails.custID;
        rapFactory.SubmitAppeal(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
        }
            //$scope.model.bServingAppeal = false;
            $scope.model.bReview = false;
            $scope.model.bConfirm = true;
            $scope.model.AppealSubmissionStatus.Review = true;
        });
    }

    self.Download = function (doc) {
        masterFactory.GetDocument(doc);
    }
    self.EditApplicantInfo = function () {
        $scope.model.bReview = false;
        $scope.model.bAppellantInfo = true;
    }
    self.EditDocuments = function () {
        $scope.model.bReview = false;
        $scope.model.bAddDocs = true;      
    }

    self.EditGrounds = function () {
        $scope.model.bReview = false;
        $scope.model.bGrounds = true;
    }
    self.EditServeAppeal = function () {
        $scope.model.bReview = false;
        $scope.model.bServingAppeal = true;
    }
   
}];
var rapReviewAppealController_resolve = {
    model: ['$route', 'alertService', 'rapreviewappealFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}