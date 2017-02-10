'use strict';
var rapForgetPwdController = ['$scope', '$modal', 'alertService', 'rapforgetPasswordFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = [];
    self.Email;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    $anchorScroll();
    self.ForgetPwd = function (model) {
        rapFactory.ForgetPwd(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            $location.path("/Login");
        });        
    }
}];
var rapForgetPwdController_resolve = {
    model: ['$route', 'alertService', 'rapforgetPasswordFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}