'use strict';
var rapReviewAppealController = ['$scope', '$modal', 'alertService', 'rapreviewappealFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
   
    self.SubmitAppeal = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveCaseInfo(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            $modalInstance.close(response.data);
        });
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