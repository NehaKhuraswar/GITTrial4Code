'use strict';
var rapTPConfirmationController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, $location, rapGlobalFactory) {
    var self = this;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    self.Continue = function () {
        rapGlobalFactory.CaseDetails = null;
        $location.path("/publicdashboard");
    }
}];
var rapTPConfirmationController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}