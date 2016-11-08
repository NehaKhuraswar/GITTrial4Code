'use strict';
var raploginController = ['$scope', '$modal', 'alertService', 'raploginFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = [];
   // rapFactory.param.set("temp");
    self.Login = function (model) {
        
        rapFactory.Login(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
 
          //  angular.copy(response.data, MyService.value);
            rapGlobalFactory.CustomerDetails = response.data;
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