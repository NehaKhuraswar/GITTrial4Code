'use strict';
var rapNewRepresentativeController = ['$scope', '$modal', 'alertService', 'rapnewrepresentativeFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = [];
    self.StateList = [];
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.ThirdPartyInfo = null;
    self.bAcknowledge = false;
    self.Cases = null;
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.StateList = response.data;
        });
    }
    _GetStateList();
    var _GetThirdPartyInfo = function () {        
        rapFactory.GetThirdPartyInfo(self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.ThirdPartyInfo = response.data;
        });
    }
    _GetThirdPartyInfo();

    self.SaveOrUpdateThirdPartyInfo = function (model) {
        if (self.bAcknowledge == false)
        {
            alert.Error("Please acknowledge");
            return;
        }
        rapFactory.SaveOrUpdateThirdPartyInfo(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.custDetails = response.data;
            $location.path("/publicdashboard");
        });        
    }
    var __GetCasesForCustomer = function () {
        return masterFactory.GetCasesForCustomer(self.custDetails.custID).then(function (response) {
            self.Cases = response.data;
        });
    }
    __GetCasesForCustomer();

    self.Cancel = function()
    {
        $location.path("/publicdashboard");
    }
}];
var rapNewRepresentativeController_resolve = {
    model: ['$route', 'alertService', 'rapnewrepresentativeFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}