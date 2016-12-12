'use strict';
var rapeditCustController = ['$scope', '$modal', 'alertService', 'rapeditcustFactory', 'masterdataFactory', '$location', function ($scope, $modal, alert, rapFactory, masterFactory, $location) {
    var self = this;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.StateList = response.data;
        });
    }
    _GetStateList();

    self.EditCustomer = function (model) {       
        rapFactory.EditCustomer(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            $location.path("/admindashboard");
        });
    }    

}];
var rapeditCustController_resolve = {
    model: ['$route', 'alertService', 'rapeditcustFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //     //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}