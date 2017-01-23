'use strict';
var rapViewAppealController = ['$scope', '$modal', 'alertService',  '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert,  $location, rapGlobalFactory, masterFactory) {
    var self = this;
    if (rapGlobalFactory.CaseDetails == null || rapGlobalFactory.CaseDetails == undefined)
    {
        var userType = rapGlobalFactory.GetUserType();
        if (userType == 'PublicUser') {
            $location.path("/publicdashboard");
        }
        else if (userType == 'CityUser') {
            $location.path("/staffdashboard");
        }
    }
    self.caseinfo = rapGlobalFactory.CaseDetails;
    if (rapGlobalFactory.FromSelectedCase != null)
    {
        self.FromSelectedCase = rapGlobalFactory.FromSelectedCase;
    }
    else {
        self.FromSelectedCase = false;
    }
   
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
var rapViewAppealController_resolve = {
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