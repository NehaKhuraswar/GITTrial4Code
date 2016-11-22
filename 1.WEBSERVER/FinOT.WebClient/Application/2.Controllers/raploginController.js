'use strict';
var raploginController = ['$scope', '$modal', 'alertService', 'raploginFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = [];
   // rapFactory.param.set("temp");
    self.Login = function (model) {
        
        rapFactory.Login(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                alert.Error(response.warnings[0]);
                return;
            }
 
          //  angular.copy(response.data, MyService.value);
            rapGlobalFactory.CustomerDetails = response.data;
            $scope.model = response.data;
            $location.path("/dashboard");

        });
    }

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