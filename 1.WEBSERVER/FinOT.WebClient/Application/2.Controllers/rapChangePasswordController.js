'use strict';
var rapChangePasswordController = ['$scope', '$modal', 'alertService', 'rapchangepwdFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = [];
    self.Pwd1;
    self.Pwd2;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.Error = '';
    $anchorScroll();
    self.ChangePassword = function () {
        if (self.Pwd1 == null)
        {
            self.Error = "Enter password";
            return;
        }
        if (self.Pwd1 != self.Pwd2)
        {
            self.Error = "Password should be same";
            return;
        }
        self.custDetails.Password = self.Pwd1;
        rapFactory.ChangePassword(self.custDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.custDetails = response.data;
            $anchorScroll();
            $location.path("/publicdashboard");
        });        
    }
}];
var rapChangePasswordController_resolve = {
    model: ['$route', 'alertService', 'rapchangepwdFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}