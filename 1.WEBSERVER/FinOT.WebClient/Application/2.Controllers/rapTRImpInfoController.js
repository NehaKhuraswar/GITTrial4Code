'use strict';
var rapTRImpInfoController = ['$scope', '$modal', 'alertService', 'rapTRPetitionTypeFactory', '$location', 'rapGlobalFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, $anchorScroll) {
    var self = this;
    $scope.model.stepNo = 2;
    $anchorScroll();
    self.Continue = function () {
        $scope.model.bImpInfo = false;
        $scope.model.bAppInfo = true;
        $scope.model.TRSubmissionStatus.PetitionType = true;
        $scope.model.TRSubmissionStatus.ImportantInformation = true;
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