'use strict';
var rapOwnerApplicantInfoController = ['$scope', '$modal', 'alertService', 'rapOwnerApplicantInfoFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;


    //self.ContinueToGroundsforPetition = function () {
    //    rapGlobalFactory.CaseDetails = self.caseinfo;
    //    rapFactory.SaveApplicationInfo(rapGlobalFactory.CaseDetails).then(function (response) {
    //        if (!alert.checkResponse(response)) { return; }
    //        rapGlobalFactory.CaseDetails = response.data;
    //        $scope.model.bAppInfo = false;
    //        $scope.model.bGrounds = true;
    //    });
    //}
}];
var rapApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerApplicantInfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}