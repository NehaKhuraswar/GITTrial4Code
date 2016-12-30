'use strict';
var rapNewCaseStatusController = ['$scope', '$modal', 'alertService', 'rapnewcasestatusFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.CityUser = rapGlobalFactory.CityUser;
    //self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.ActivityStatus = [];
    self.ActivityList = [];
    self.StatusList = [];
    

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
    
    

    self.ActivityChanged = function (model) {
        return rapFactory.GetStatus(model.ActivityID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.StatusList = response.data;
        });               
    }
    self.Submit = function (model, C_ID) {
        //TBD remove C_ID hardcoding
        model.EmployeeID = self.CityUser.EmployeeID;
        rapFactory.SaveNewActivityStatus(model, 2).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $location.path("/staffdashboard");
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