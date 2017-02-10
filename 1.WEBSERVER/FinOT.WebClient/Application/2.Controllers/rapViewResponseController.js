'use strict';
var rapViewResponseController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.Title = '';
    if (rapGlobalFactory.CaseDetails == null || rapGlobalFactory.CaseDetails == undefined) {
        var userType = rapGlobalFactory.GetUserType();
        if (userType == 'PublicUser') {
            $location.path("/publicdashboard");
        }
        else if (userType == 'CityUser') {
            $location.path("/staffdashboard");
        }
    }
    self.caseinfo = rapGlobalFactory.CaseDetails;
    $anchorScroll();
    if (rapGlobalFactory.FromSelectedCase == true) {

        self.Title = 'Staff Dashboard';
    }
    else {
        self.Title = 'Dashboard';
    }
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
var rapViewResponseController_resolve = {
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