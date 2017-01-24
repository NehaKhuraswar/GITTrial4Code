'use strict';
var rapNewCaseStatusController = ['$scope', '$modal', 'alertService', 'rapnewcasestatusFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    if (rapGlobalFactory.SelectedCase == null || rapGlobalFactory.SelectedCase == undefined) {
        $location.path("/staffdashboard");
    }
    self.CityUser = rapGlobalFactory.CityUser;
    //self.custDetails = rapGlobalFactory.CustomerDetails;

    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.ActivityStatus = [];
    self.ActivityList = [];
    self.StatusList = [];
    
    self.CaseClick = function () {
        $location.path("/selectedcase");
    }
    self.Home = function () {
        $location.path("/staffdashboard");
    }
    var _getActivity = function () {
        return rapFactory.GetActivity().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.ActivityList = response.data;            
        });
    }
    //var _getStatus = function () {
    //    return rapFactory.GetStatus(1).then(function (response) {
    //        if (!alert.checkResponse(response)) { return; }
    //        self.StatusList = response.data;
    //    });
    //}

    var _getEmptyActivityStatus = function () {
        return rapFactory.GetEmptyActivityStatus().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.ActivityStatus = response.data;
        });
    }

    _getActivity();
    //_getStatus();
    _getEmptyActivityStatus();
    
    

    var _getStatus = function () {
        return rapFactory.GetStatus(1).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.StatusList = response.data;
        });               
    }
    _getStatus();

    self.Submit = function (model, C_ID) {
        //TBD remove C_ID hardcoding
        model.EmployeeID = self.CityUser.EmployeeID;
        rapFactory.SaveNewActivityStatus(model, C_ID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $location.path("/selectedcase");
        });        
    }
    self.Cancel = function (model, C_ID) {
        $location.path("/staffdashboard");
    }
}];
var rapNewCaseStatusController_resolve = {
    model: ['$route', 'alertService', 'rapnewcasestatusFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}