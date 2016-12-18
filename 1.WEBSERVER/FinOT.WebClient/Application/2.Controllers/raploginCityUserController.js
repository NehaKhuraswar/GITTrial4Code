'use strict';
var raploginCityUserController = ['$scope', '$modal', 'alertService', 'raploginCityUserFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = [];
    self.AccountTypesList = [];
    self.SelectedAccountType;
    var _getAccountTypes = function () {
        masterFactory.GetAccountTypes().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountTypesList = response.data;
        });
    }
    _getAccountTypes();
    self.ForgotPassword = function () {
        $location.path("/forgotpassword");
    }
    self.CreateAccount = function () {
        $location.path("/register");
    }
   // rapFactory.param.set("temp");
    //self.Login = function (model) {
        
    //    rapFactory.Login(model).then(function (response) {
    //        if (response.data !== "") {
    //                    sessionStorage.setItem('token', response.data.access_token);
    //                    sessionStorage.setItem('username', response.data.Username);
    //                    sessionStorage.setItem('roles', response.data.Roles);
    //                    sessionStorage.setItem('expire', new Date(Date.now() + response.data.expires_in * 1000));
                        
    //        } else { 
    //            $location.path("/notoken")
    //        }
                
 
    //        rapGlobalFactory.CustomerDetails.User.FirstName = response.data.FirstName;
    //        $scope.model = response.data;
    //        $location.path("/dashboard");

    //    });
    //}
    self.Login = function (model, accounttype) {
        //if (accounttype.AccountTypeID == 3) {
        //    rapFactory.Login(model).then(function (response) {
        //            if (!alert.checkResponse(response)) {
        //                alert.Error(response.warnings[0]);
        //                return;
        //            }                
        //            rapGlobalFactory.CustomerDetails = response.data;                    
        //            $location.path("/publicdashboard");    
        //    });
        //}
        //else //if (accounttype.AccountTypeID == 1 || ) 
        //{
           // model.AccountType = accounttype;
            rapFactory.LoginCity(model).then(function (response) {
                if (!alert.checkResponse(response)) {
                    alert.Error(response.warnings[0]);
                    return;
                }                
                rapGlobalFactory.CityUser = response.data;
                if (rapGlobalFactory.CityUser.AccountType.AccountTypeID == 1)
                    $location.path("/staffdashboard");
                else if (rapGlobalFactory.CityUser.AccountType.AccountTypeID == 2)
                    $location.path("/admindashboard");
            });
        }
        //else if (accounttype.AccountTypeID == 2) {
        //    rapGlobalFactory.CityAdmin = response.data;
        //    $location.path("/dashboard");
        //}
        
        
   // }

}];
var raploginCityUserController_resolve = {
    model: ['$route', 'alertService', 'raploginCityUserFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}