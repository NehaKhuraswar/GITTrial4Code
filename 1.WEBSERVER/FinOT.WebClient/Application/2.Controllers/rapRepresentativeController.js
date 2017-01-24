'use strict';
var rapRepresentativeController = ['$scope', '$modal', 'alertService', 'raprepresentativeFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = [];
    self.StateList = [];
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.ThirdPartyInfo = null;
    self.bAcknowledge = false;
    self.Cases = null;
    self.AddNewRepresentative = function () {
        $location.path("/Representative");
    }
    self.EditRepresentative = function () {
        rapGlobalFactory.IsEdit = true;
        $location.path("/Representative");
    }
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
    var __GetThirdPartyCasesForCustomer = function () {
        return masterFactory.GetThirdPartyCasesForCustomer(self.custDetails.custID).then(function (response) {
            self.Cases = response.data;
        });
    }
    __GetThirdPartyCasesForCustomer();

    self.RemoveRepresentative = function () {
        return rapFactory.RemoveThirdPartyInfo(self.ThirdPartyInfo).then(function (response) {
            self.ThirdPartyInfo = response.data;
        });
    }

    self.UpdateThirdPartyAccessPrivilege = function (cases) {
        return masterFactory.UpdateThirdPartyAccessPrivilege(cases, self.custDetails.custID).then(function (response) {
            self.Cases = response.data;
        });
    }
    self.Cancel = function()
    {
        $location.path("/publicdashboard");
    }
}];
var rapRepresentativeController_resolve = {
    model: ['$route', 'alertService', 'raprepresentativeFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}