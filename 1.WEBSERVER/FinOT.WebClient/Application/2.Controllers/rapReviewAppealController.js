'use strict';
var rapReviewAppealController = ['$scope', '$modal', 'alertService', 'rapreviewappealFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
   
    self.SubmitAppeal = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SubmitAppeal(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            //$scope.model.bServingAppeal = false;
            $scope.model.bReview = false;
            $scope.model.bConfirm = true;
            $scope.model.AppealSubmissionStatus.Review = true;
        });
    }
    self.EditApplicantInfo = function () {
        $scope.model.bReview = false;
        $scope.model.bAppellantInfo = true;
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