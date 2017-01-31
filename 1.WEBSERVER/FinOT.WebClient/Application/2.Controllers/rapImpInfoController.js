'use strict';
var rapImpInfoController = ['$scope', '$modal', 'alertService', 'rapfilepetitionFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.Continue = function () {
        $scope.model.bImpInfo = false;
        $scope.model.bAppInfo = true;
        $scope.model.tPetionActiveStatus.ImportantInformation = true;
    }
}];
var rapImpInfoController_resolve = {
    model: ['$route', 'alertService', 'rapfilepetitionFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}