'use strict';
var rapOResponseConfirmationController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', '$anchorScroll', function ($scope, $modal, alert, $location, rapGlobalFactory, $anchorScroll) {
    var self = this;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    $anchorScroll();
    self.Continue = function () {
        $location.path("/publicdashboard");
    }
}];
var rapTPConfirmationController_resolve = {
    model: ['$route', 'alertService', function ($route, alert) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}