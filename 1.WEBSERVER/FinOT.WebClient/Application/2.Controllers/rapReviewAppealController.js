'use strict';
var rapReviewAppealController = ['$scope', '$modal', 'alertService', 'rapreviewappealFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
   self.Error = "";
    //var _GetCaseInfoWithModel = function (CaseID) {
    //    rapFactory.GetCaseInfoWithModel(CaseID, self.custDetails.custID).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.caseinfo.TenantAppealInfo = response.data.TenantAppealInfo;
    //    });
    //}
    //_GetCaseInfoWithModel(rapGlobalFactory.CaseDetails.CaseID);

    self.SubmitAppeal = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SubmitAppeal(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
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