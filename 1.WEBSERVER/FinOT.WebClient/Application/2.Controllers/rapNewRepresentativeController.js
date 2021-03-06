﻿'use strict';
var rapNewRepresentativeController = ['$scope', '$modal', 'alertService', 'rapnewrepresentativeFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = [];
    self.StateList = [];
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.ThirdPartyInfo = null;
    self.bAcknowledge = false;
    self.Cases = null;
    self.Hide = false;
    self.Error = "";
    self.IsEdit = rapGlobalFactory.IsEdit;
    self.Title = "Add New Representative";
    if (self.IsEdit == true)
    {
        self.Title = "Edit Representative";
    }
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.StateList = response.data;
        });
    }
    _GetStateList();
    var _GetThirdPartyInfo = function () {        
        rapFactory.GetThirdPartyInfo(self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.ThirdPartyInfo = response.data;
        });
    }
    _GetThirdPartyInfo();
    $anchorScroll();
    var _UpdateThirdPartyAccessPrivilege = function (cases) {
        return masterFactory.UpdateThirdPartyAccessPrivilege(cases, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.Cases = response.data;
            $location.path("/YourRepresentative");
        });
    }
    self.SaveOrUpdateThirdPartyInfo = function (model) {
        
        if (model.MailNotification == false && model.EmailNotification == false) {
            self.Error = "Please select the preferred way of receiving notifications.";
            $anchorScroll();
            return;
        }
        if (model.ThirdPartyUser.State == null) {
            self.Error = "State is a required field";
            $anchorScroll();
            return;
        }
        if (self.bAcknowledge == false) {
            self.Error = "Please acknowledge that your representative will receive notifications";
            $anchorScroll();
            return;
        }
        return rapFactory.SaveOrUpdateThirdPartyInfo(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
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