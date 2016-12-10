﻿'use strict';
var rapOwnerRentalPropertyController = ['$scope', '$modal', 'alertService', 'rapOwnerRentalPropertyFactory', '$location', 'rapGlobalFactory','masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory,masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.OwnerPetitionInfo.PropertyInfo.CustomerID = self.custDetails.custID;
    self.StateList = [];
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.StateList = response.data;
        });
    }
    
    _GetStateList();
    rapFactory.GetOwnerPropertyAndTenantInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
            self.caseinfo = response.data;
    });

    self.Continue = function () {
        if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length == 0)
        {
            self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.push(self.caseinfo.OwnerPetitionInfo);
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOwnerPropertyAndTenantInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
        });

    }
    self.AddTenant = function (_userInfo)
    {
        if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length > 0)
        {
            _userInfo.TenantUserInfo.AddressLine1 = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine1;
            _userInfo.TenantUserInfo.AddressLine2 = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine2;
            _userInfo.TenantUserInfo.City = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.City;
            _userInfo.TenantUserInfo.State = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.State;
            _userInfo.TenantUserInfo.Zip = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Zip;
            _userInfo.TenantUserInfo.PhoneNumber = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.PhoneNumber;
            _userInfo.TenantUserInfo.Email = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Email;
        }
        var _userInfo1 = angular.copy(_userInfo);
      //  var _userInfo = self.caseinfo.OwnerPetitionTenantInfo;
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.push(_userInfo1);      
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.FirstName = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.LastName = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine1 = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine2 = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.City = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.State.StateName = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Zip = 0;
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.PhoneNumber =0;
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Email = "";
    }

}];
var rapApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalPropertyFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}