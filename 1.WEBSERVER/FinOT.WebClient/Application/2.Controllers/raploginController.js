'use strict';
var raploginController = ['$scope', '$modal', 'alertService', 'raploginFactory', '$location', function ($scope, $modal, alert, rapFactory, $location) {
    var self = this;
    self.model = [];
    self.Login = function (model) {
        var plainBodyText = "";

        rapFactory.Login(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            $scope.model = response.data;
            $location.path("/dashboard");

        });
    }
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
var raploginController_resolve = {
    model: ['$route', 'alertService', 'raploginFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        return rapFactory.GetCustomer(null).then(function (response) {
            //   if (!alert.checkResponse(response)) { return; }
            //    return response.data;
            //});
        });
    }]
}