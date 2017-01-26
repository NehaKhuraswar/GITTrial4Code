'use strict';
var rapOwnerRentalPropertyController = ['$scope', '$modal', 'alertService', 'rapOwnerRentalPropertyFactory', '$location', 'rapGlobalFactory','masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory,masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerPetitionInfo.PropertyInfo.CustomerID = self.custDetails.custID;
    self.StateList = [];
    self.IsTenant = false;
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
    
    var _GetIsTenant = function()
    {
        var isPresent = false;
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.forEach(function (tenant) {
            if (tenant.IsDeleted == false) {
                self.IsTenant = true;
                isPresent = true;
            }
        });
        if (isPresent == false)
        {
            self.IsTenant = false;
        }
    }
    rapFactory.GetOwnerPropertyAndTenantInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        _GetIsTenant();
    });

    self.Continue = function () {
        if (self.IsTenant == false)
        {
            if ((self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.FirstName != null && self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.FirstName != "") &&
                (self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine1 != null && self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine1 != "") &&
                (self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.City != null && self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.City != "") &&
                (self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Zip != null && self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Zip != ""))
            {
                self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.push(self.caseinfo.OwnerPetitionTenantInfo);
                
                self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.FirstName = "";
                self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.LastName = "";
            }
            _GetIsTenant();            
        }
        if (self.IsTenant == false)
        {
            self.Error = "Please add tenant information";
            return;
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOwnerPropertyAndTenantInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
            $scope.model.ownerRentalProperty = false;
            $scope.model.ownerRentalHistory = true;
            $scope.model.DisableAllCurrent();
            $scope.model.oPetionCurrentStatus.RentHistory = true;
            $scope.model.oPetionActiveStatus.RentalProperty = true;
        });
    
    }
    self.AddTenant = function (_userInfo)
    {
        //if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length > 0)
        //{
        //    _userInfo.TenantUserInfo.AddressLine1 = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine1;
        //    _userInfo.TenantUserInfo.AddressLine2 = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine2;
        //    _userInfo.TenantUserInfo.City = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.City;
        //    _userInfo.TenantUserInfo.State = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.State;
        //    _userInfo.TenantUserInfo.Zip = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Zip;
        //    _userInfo.TenantUserInfo.PhoneNumber = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.PhoneNumber;
        //    _userInfo.TenantUserInfo.Email = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Email;
        //}
        if (self.IsTenant == true) {
            for (var i = 0 ; i < self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length; i++) {
                var existingTenant;
                if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].IsDeleted == false) {
                    _userInfo.TenantUserInfo.AddressLine1 = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.AddressLine1;
                    _userInfo.TenantUserInfo.AddressLine2 = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.AddressLine2;
                    _userInfo.TenantUserInfo.City = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.City;
                    _userInfo.TenantUserInfo.State = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.State;
                    _userInfo.TenantUserInfo.Zip = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Zip;
                    break;
                }
            }
            //_userInfo.TenantUserInfo.AddressLine1 = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine1;
            //_userInfo.TenantUserInfo.AddressLine2 = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.AddressLine2;
            //_userInfo.TenantUserInfo.City = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.City;
            //_userInfo.TenantUserInfo.State = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.State;
            //_userInfo.TenantUserInfo.Zip = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Zip;
            //_userInfo.TenantUserInfo.PhoneNumber = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.PhoneNumber;
            // _userInfo.TenantUserInfo.Email = self.caseinfo.OwnerResponseInfo.PropertyInfo.TenantInfo[0].TenantUserInfo.Email;
        }
        if ((_userInfo.TenantUserInfo.FirstName != null && _userInfo.TenantUserInfo.FirstName != "") &&
                (_userInfo.TenantUserInfo.AddressLine1 != null && _userInfo.TenantUserInfo.AddressLine1 != "") &&
                (_userInfo.TenantUserInfo.City != null && _userInfo.TenantUserInfo.City != "") &&
                (_userInfo.TenantUserInfo.Zip != null && _userInfo.TenantUserInfo.Zip != "")) {
            var _userInfo1 = angular.copy(_userInfo);
            //  var _userInfo = self.caseinfo.OwnerPetitionTenantInfo;
            self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.push(_userInfo1);
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.FirstName = null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.LastName = null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.PhoneNumber = null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Email = null;
            _GetIsTenant();
            //self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine1 = "";
            //self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine2 = "";
            //self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.City = "";
            //self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.State.StateName = "";
            //self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Zip = 0;
            //self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.PhoneNumber =0;
            //self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Email = "";
        }
    }
    self.RemoveTenant = function(_tenant)
    {
        var index = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.indexOf(_tenant);
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.splice(index, 1);
        _tenant.IsDeleted = true;
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.push(_tenant);
        _GetIsTenant();
        if (self.IsTenant == false) {
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine1 = null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.AddressLine2 = null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.City =null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.State.StateName = "";
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Zip = null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.PhoneNumber = null;
            self.caseinfo.OwnerPetitionTenantInfo.TenantUserInfo.Email = null;
        }

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