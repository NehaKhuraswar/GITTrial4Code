'use strict';
var rapTRImpInfoController = ['$scope', '$modal', 'alertService', 'rapTRPetitionTypeFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.Continue = function () {
        $scope.model.bImpInfo = false;
        $scope.model.bAppInfo = true;
    }
}];
var rapTRImpInfoController_resolve = {
    model: ['$route', 'alertService', 'rapTRPetitionTypeFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}