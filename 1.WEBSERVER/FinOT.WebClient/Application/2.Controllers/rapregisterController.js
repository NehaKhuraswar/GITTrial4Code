'use strict';
var rapregisterController = ['$scope', '$modal', 'alertService', 'rapcustFactory', 'masterdataFactory', '$location', function ($scope, $modal, alert, rapFactory, masterFactory, $location) {
    var self = this;
    self.CustomerInfo = [];
    self.StateList = [];
    self.Password;
    self.ConfirmPassword;
    var _GetCustomerModel = function () {
        return rapFactory.GetCustomer(null).then(function (response) {
               if (!alert.checkResponse(response)) { return; }
               self.CustomerInfo = response.data;
        });
        
    }
    _GetCustomerModel();
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
            $location.path("/login");
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