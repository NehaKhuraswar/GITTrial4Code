'use strict';
var rapResendPinController = ['$scope', '$modal', 'alertService', 'rapresendpinFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = [];
    self.Pwd1;
    self.Pwd2;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.ResendPin = function () {
        rapFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            $location.path("/publicdashboard");
        });        
    }
}];
var rapResendPinController_resolve = {
    model: ['$route', 'alertService', 'rapresendpinFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}