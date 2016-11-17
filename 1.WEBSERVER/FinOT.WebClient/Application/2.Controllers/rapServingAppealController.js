'use strict';
var rapServingAppealController = ['$scope', '$modal', 'alertService', 'rapservingappealFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.ContinueToReview = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $location.path("/reviewappeal");
    }
}];
var rapServingAppealController_resolve = {
    model: ['$route', 'alertService', 'rapservingappealFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}