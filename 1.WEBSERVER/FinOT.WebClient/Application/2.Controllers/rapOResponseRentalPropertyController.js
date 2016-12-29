'use strict';
var rapOResponseRentalPropertyController = ['$scope', '$modal', 'alertService', 'rapOResponseRentalPropertyFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.OwnerResponseInfo.PropertyInfo.CustomerID = self.custDetails.custID;
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
        if (self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo.length == 0) {
            self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo.push(self.caseinfo.OwnerPetitionTenantInfo);
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOwnerPropertyAndTenantInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
        });
        $scope.model.oresponseRentalProperty = false;
        $scope.model.oresponseRentalHistory = true;
        //$scope.model.ownerRentalProperty = false;
        //$scope.model.ownerRentalHistory = true;
        //$scope.model.DisableAllCurrent();
        //$scope.model.oPetionCurrentStatus.RentHistory = true;
        //$scope.model.oPetionActiveStatus.RentalProperty = true;
    }
    self.AddTenant = function (_userInfo) {
        if (self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo.length > 0) {
            _userInfo.TenantUserInfo.AddressLine1 = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine1;
            _userInfo.TenantUserInfo.AddressLine2 = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine2;
            _userInfo.TenantUserInfo.City = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.City;
            _userInfo.TenantUserInfo.State = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.State;
            _userInfo.TenantUserInfo.Zip = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Zip;
            _userInfo.TenantUserInfo.PhoneNumber = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.PhoneNumber;
            _userInfo.TenantUserInfo.Email = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Email;
        }
        var _userInfo1 = angular.copy(_userInfo);
        //  var _userInfo = self.caseinfo.OwnerPetitionTenantInfo;
        self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo.push(_userInfo1);
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.FirstName = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.LastName = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine1 = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine2 = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.City = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.State.StateName = "";
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Zip = 0;
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.PhoneNumber = 0;
        self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Email = "";
    }
    self.RemoveTenant = function (_tenant) {
        var index = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo.indexOf(_tenant);
        self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo.splice(index, 1);
        _tenant.IsDeleted = true;
        self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo.push(_tenant);
        //self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.update(
        //    self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.indexOf(function (item) {
        //        return item.TenantUserInfo.FirstName == _tenant.TenantUserInfo.FirstName && item.TenantUserInfo.LastName == _tenant.TenantUserInfo.LastName;
        //    }), function (item) { return item.set(item.IsDeleted, true); }
        //    );    
        //self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.remove(_tenant);
        //_tenant.IsDeleted = true;
        //self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.push(_tenant);
    }

}];
var rapOwnerRentalPropertyController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalPropertyFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}