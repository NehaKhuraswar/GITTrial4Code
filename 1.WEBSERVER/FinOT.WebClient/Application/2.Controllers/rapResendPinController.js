'use strict';
var rapResendPinController = ['$scope', '$modal', 'alertService', 'rapresendpinFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = [];
    self.Pwd1;
    self.Pwd2;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    $anchorScroll();
    self.ResendPin = function () {
        rapFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
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