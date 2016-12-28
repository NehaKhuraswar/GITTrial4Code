'use strict';
var rapTRReviewController = ['$scope', '$modal', 'alertService', 'rapTRreviewFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    var _GetTenantResponseReviewInfo = function (CaseNumber,custID) {
        rapFactory.GetTenantResponseReviewInfo(CaseNumber, custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.TenantResponseInfo = response.data;
        });
    }
    _GetTenantResponseReviewInfo(self.caseinfo.CaseID, self.custDetails.custID);

    self.EditApplicantInfo = function () {
        $scope.model.bReview = false;
        $scope.model.bAppInfo = true;
    }
    self.EditExemptContested = function () {
        $scope.model.bReview = false;
        $scope.model.bExemptionContested = true;
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
      //  $scope.model.tPetionActiveStatus.Review = true;
    }

}];