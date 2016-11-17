'use strict';
var rapAppellantsInfoController = ['$scope', '$modal', 'alertService', 'rapappellantsinfoFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.ContinueToGroundsforAppeal = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $location.path("/groundsforappeal");
    }
}];
var rapAppellantsInfoController_resolve = {
    model: ['$route', 'alertService', 'rapappellantsinfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}