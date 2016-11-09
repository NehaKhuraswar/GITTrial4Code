'use strict';
var rapdashboardController = ['$scope', '$modal', 'alertService',  'rapdashboardFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = rapGlobalFactory.CustomerDetails;
    self.InviteThirdPartyUser = function () {
        $location.path("/invitethirdparty");
    }
    self.FilePetition = function () {
        $location.path("/filePetition");
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