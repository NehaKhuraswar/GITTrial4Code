'use strict';
var rapdashboardController = ['$scope', '$modal', 'alertService', 'model', 'rapdashboardFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, model, rapGlobalFactory) {
    var self = this;
    self.model = rapGlobalFactory.CustomerDetails;
    self.SearchInviteThirdPartyUser = function (model) {
        rapFactory.SearchInviteThirdPartyUser(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                                return;
            }
        });
    }

}];
var rapdashboardController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //    $scope.model = response.data;
        //    //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}