'use strict';
var rapChangePasswordController = ['$scope', '$modal', 'alertService', 'rapchangepwdFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = [];
    self.Pwd1;
    self.Pwd2;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.ChangePassword = function () {
        if (self.Pwd1 == null)
        {
            alert.Error("Enter password");
            return;
        }
        if (self.Pwd1 != self.Pwd2)
        {
            alert.Error("Password should be same");
            return;
        }
        self.custDetails.Password = self.Pwd1;
        rapFactory.ChangePassword(self.custDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.custDetails = response.data;
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