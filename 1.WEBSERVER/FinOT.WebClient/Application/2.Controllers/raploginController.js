'use strict';
var raploginController = ['$scope', '$modal', 'alertService', 'raploginFactory', function ($scope, $modal, alert, rapFactory) {
    var self = this;
    self.model = [];
    self.Login = function (model) {
        var plainBodyText = "";

        rapFactory.Login(null, model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            $modalInstance.close(response.data);
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