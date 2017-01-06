'use strict';
var rapViewPetitionController = ['$scope', '$modal', 'alertService',  '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert,  $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.FromSelectedCase = rapGlobalFactory.FromSelectedCase;
    self.Back = function()
    {
        if (rapGlobalFactory.FromSelectedCase == true) {
            rapGlobalFactory.FromSelectedCase = false;
            $location.path("/selectedcase");
        }
        else {
            $location.path("publicdashboard");
        }
    }
}];
var rapViewPetitionController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //    $scope.model = response.data;
        //    //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}