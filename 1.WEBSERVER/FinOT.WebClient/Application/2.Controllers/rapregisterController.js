'use strict';
var rapregisterController = ['$scope', '$modal', 'alertService', 'rapcustFactory', function ($scope, $modal, alert, rapFactory) {
    var self = this;
    self.model = [];
    self.Register = function (model) {        
        rapFactory.SaveCustomer(null, model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
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