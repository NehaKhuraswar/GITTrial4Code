'use strict';
var rapVerifyController = ['$scope', '$modal', 'alertService', 'rapverificationFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.ContinueToVerification = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
       
        $scope.model.bReview = false;
        $scope.model.bVerification = true;
    }
}];
var rapVerifyController_resolve = {
    model: ['$route', 'alertService', 'rapverificationFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}