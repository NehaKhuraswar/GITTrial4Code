'use strict';
var rapCityUserAcctController = ['$scope', '$modal', 'alertService', 'rapcityuserregisterFactory', function ($scope, $modal, alert, rapFactory) {
    var self = this;
   // self.CityUserAccount = [];
    self.AccountTypesList = [];
    self.CreateAccount = function (model) {

        rapFactory.CreateCityUserAccount(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }            
        });
    }

    //self.GetCityUserAcctEmpty = function () {
    //    rapFactory.GetCityUserAcctEmpty().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.CityUserAccount = response.data;
    //    });
    //}   
    
    var _getAccountTypes = function () {
        rapFactory.GetAccountTypes().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountTypesList = response.data;
        });
    }
    _getAccountTypes();
    //self.Login = function (model) {
    //    var plainBodyText = "";

    //    rapFactory.LoginCustomer(null, model).then(function (response) {
    //            if (!alert.checkResponse(response)) {
    //                return;
    //            }
    //            $modalInstance.close(response.data);
    //        });
    //    //otFactory.SaveCustDetails(custID, self.model).then(function (response) {
    //    //    if (!alert.checkResponse(response)) {
    //    //        return;
    //    //    }
    //    //    $modalInstance.close(response.data);
    //    //});
    //}

}];
var rapCityUserAcctController_resolve = {
    model: ['$route', 'alertService', 'rapcityuserregisterFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //     //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}