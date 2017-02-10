'use strict';
var raploginController = ['$scope', '$modal', 'alertService', 'raploginFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = [];
    self.AccountTypesList = [];
    self.SelectedAccountType;
    self.bCityUser = false;
    self.Error = "";
    self.Hide = false;
    //var _getAccountTypes = function () {
    //    masterFactory.GetAccountTypes().then(function (response) {
    //        if (!alert.checkResponse(response)) { return; }
    //        self.AccountTypesList = response.data;
    //    });
    //}
    //_getAccountTypes();
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
    $anchorScroll();
    self.Login = function (model, accounttype) {
        self.Hide = false;
        rapFactory.Login(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                    return;
            }
          
            rapGlobalFactory.CustomerDetails = response.data;
            rapGlobalFactory.SaveCustomer(response.data);
            rapGlobalFactory.SaveUserType('PublicUser');        
            $location.path("/publicdashboard");    
        });          
    }
}];
var raploginController_resolve = {
    model: ['$route', 'alertService', 'raploginFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}