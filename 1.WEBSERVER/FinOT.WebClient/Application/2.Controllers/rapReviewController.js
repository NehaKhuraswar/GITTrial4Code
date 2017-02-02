'use strict';
var rapReviewController = ['$scope', '$modal', 'alertService', 'rapreviewFactory', '$location', 'rapGlobalFactory', 'rapTenantlDocumentFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, rapTenantlDocumentFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Error = "";
    self.Documents = null;
    $scope.model.stepNo = 8;
    rapTenantlDocumentFactory.GetTenantDocuments(self.custDetails.custID, 'TP_AdditionalDocuments').then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        self.Documents = response.data;
    });
    var _GetTenantReviewInfo = function (custID) {
        rapFactory.GetTenantReviewInfo(custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.caseinfo.TenantPetitionInfo = response.data;
        });
    }
    _GetTenantReviewInfo(self.custDetails.custID);

    self.EditApplicantInfo = function () {
        $scope.model.bReview = false;
        $scope.model.bAppInfo = true;
    }
    self.EditGroundsInfo = function () {
        $scope.model.bReview = false;
        $scope.model.bGrounds = true;
    }
    self.EditRentalHistoryInfo = function () {
        $scope.model.bReview = false;
        $scope.model.bRentalHistory = true;
    }
    self.EditDocumentsInfo = function () {
        $scope.model.bReview = false;
        $scope.model.bAddDocuments = true;
    }
    self.ContinueToVerification = function () {
        $scope.model.bReview = false;
        $scope.model.bVerification = true;
        $scope.model.tPetionActiveStatus.Review = true;
    }

}];
var rapReviewController_resolve = {
    model: ['$route', 'alertService', 'rapreviewFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}