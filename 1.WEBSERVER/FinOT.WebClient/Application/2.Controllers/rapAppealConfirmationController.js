'use strict';
var rapAppealConfirmationController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, $location, rapGlobalFactory) {
    var self = this;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    self.Continue = function () {
        $location.path("/publicdashboard");
    }
}];
var rapAppealImpInfoController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}