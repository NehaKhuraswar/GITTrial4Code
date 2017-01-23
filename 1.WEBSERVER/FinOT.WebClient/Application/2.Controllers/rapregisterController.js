﻿'use strict';
var rapregisterController = ['$scope', '$modal', 'alertService', 'rapcustFactory', 'masterdataFactory', 'rapGlobalFactory', '$location', function ($scope, $modal, alert, rapFactory, masterFactory, rapGlobalFactory, $location) {
    var self = this;
    self.CustomerInfo = null;
    self.StateList = [];
    self.Password;
    self.ConfirmPassword;
    $scope.required = true;

    if (rapGlobalFactory.IsEdit == null || rapGlobalFactory.IsEdit == undefined)
    {
        var userType = rapGlobalFactory.GetUserType();
        if (userType == 'PublicUser') {
            $location.path("/publicdashboard");
        }
        else if (userType == 'CityUser') {
            $location.path("/staffdashboard");
        }      
    }

    self.bEdit =  rapGlobalFactory.IsEdit;
    self.Title = "Create a city user account";
    self.SubmitText = "Create account"

    if (rapGlobalFactory.IsEdit == true)
    {
        self.Title = "Edit a city user account";
        self.SubmitText = "Update account";
    }
    else
    {
        rapGlobalFactory.CustomerDetails = null;
    }

    var _GetCustomerFromID = function(custID)
    {
        return rapFactory.GetCustomerFromID(custID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.CustomerInfo = response.data;
        });
    }
    if (rapGlobalFactory.CustomerDetails != null)
    {
        self.CustomerInfo = rapGlobalFactory.CustomerDetails;
    }
    else {
        if (self.bEdit == true) {
            
            _GetCustomerFromID(rapGlobalFactory.SelectedForEdit.custID);
            
        }
    }

    var _GetCustomerModel = function () {
        return rapFactory.GetCustomer(null).then(function (response) {
               if (!alert.checkResponse(response)) { return; }
               self.CustomerInfo = response.data;
               //rapGlobalFactory.CustomerDetails = self.CustomerInfo;
        });        
    }

    if (self.CustomerInfo == null) {
        _GetCustomerModel();
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
    var checkPassword = function (pwd, email) {
        if (email == pwd)
            return false;
        var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\_\-])(?=.{8,})");
        return strongRegex.test(pwd);
    }
    var checkPhoneNumber = function (phoneNumber) {
        var strongRegex = new RegExp("^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$");
        return strongRegex.test(phoneNumber);
    }

    self.Cancel = function ()
    {
        if (rapGlobalFactory.IsEdit == true) {
            rapGlobalFactory.SelectedForEdit = null;
            rapGlobalFactory.IsEdit = false;
            $location.path("/admindashboard");
        }
        else if(rapGlobalFactory.IsAdmin == true)
        {
                rapGlobalFactory.IsAdmin = false;
                $location.path("/admindashboard");
        }
        else {
            $location.path("/Login");
        }
    }

    self.Register = function (model) {
        if (self.Password != self.ConfirmPassword)
        {
            alert.Error("Password not matching!")
            return;
            }
        model.Password = self.Password;
        
        //if (!checkPassword(model.Password, model.email))
        //    {
        //        alert.Error("The password you have entered is not valid! ")
        //    return;
        //        }
        //if (!checkPhoneNumber(model.PhoneNumber))
        //            {
        //        alert.Error("Phone number is not valid")
        //    return;
        //}
       
        rapFactory.SaveCustomer(null, model).then(function(response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            if (rapGlobalFactory.IsEdit == true)
            {
                rapGlobalFactory.SelectedForEdit = null;
                rapGlobalFactory.IsEdit = false;
                $location.path("/admindashboard");
            }
            else if(rapGlobalFactory.IsAdmin == true) {
                rapGlobalFactory.IsAdmin = false;
                $location.path("/admindashboard");
            }
            else {
                $location.path("/Login");
            }
            
        });
    }

    self.DeleteCustomer = function (model) {
        rapFactory.DeleteCustomer(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            if (rapGlobalFactory.IsEdit == true) {
                rapGlobalFactory.SelectedForEdit = null;
                rapGlobalFactory.IsEdit = false;
                $location.path("/admindashboard");
            }
            else {
                $location.path("/Login");
            }

        });
    }

}];
var rapregisterController_resolve = {
    model: ['$route', 'alertService', 'rapcustFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //     //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}