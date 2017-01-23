'use strict';
var rapNewRepresentativeController = ['$scope', '$modal', 'alertService', 'rapnewrepresentativeFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = [];
    self.StateList = [];
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.ThirdPartyInfo = null;
    self.bAcknowledge = false;
    self.Cases = null;
    self.Hide = false;
    self.Error = "";
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
            if (!alert.checkForResponse(response)) {
                return;
            }
            self.ThirdPartyInfo = response.data;
        });
    }
    _GetThirdPartyInfo();

    var _UpdateThirdPartyAccessPrivilege = function (cases) {
        return masterFactory.UpdateThirdPartyAccessPrivilege(cases, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.Cases = response.data;
            $location.path("/YourRepresentative");
        });
    }
    self.SaveOrUpdateThirdPartyInfo = function (model) {
        if (self.bAcknowledge == false)
        {
            alert.Error("Please acknowledge");
            return;
        }
        return rapFactory.SaveOrUpdateThirdPartyInfo(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            _UpdateThirdPartyAccessPrivilege(self.Cases);           
            
        });        
    }
    var __GetCasesForCustomer = function () {
        return masterFactory.GetThirdPartyCasesForCustomer(self.custDetails.custID).then(function (response) {
            self.Cases = response.data;
        });
    }
    __GetCasesForCustomer();

    self.Cancel = function()
    {
        $location.path("/YourRepresentative");
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