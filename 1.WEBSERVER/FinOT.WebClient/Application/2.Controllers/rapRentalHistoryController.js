'use strict';
var rapRentalHistoryController = ['$scope', '$modal', 'alertService', 'raprentalhistoryFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    var range = 10 / 2;
    var currentYear = new Date().getFullYear();
    self.years = [];
    for (var i = range; i > 0 ; i--) {

        self.years.push(currentYear - i);
    }
    for (var i = 0; i < range + 1; i++) {
        self.years.push(currentYear + i);
    }
    self.ContinueToLostServices = function () {
        var a = self.selectedObj;
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveTenantRentalIncrementInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bRentalHistory = false;
            $scope.model.bLostServices = true;
        });
        
    }
    self.Save = function () {
        var a = self.selectedObj;
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveTenantRentalIncrementInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }          
        });

    }
    
}];
var rapRentalHistoryController_resolve = {
    model: ['$route', 'alertService', 'raprentalhistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}