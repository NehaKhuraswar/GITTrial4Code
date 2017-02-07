'use strict';
var rapOwnerImpInfoController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', '$anchorScroll', function ($scope, $modal, alert, $location, rapGlobalFactory, $anchorScroll) {
    var self = this;
    $scope.model.stepNo = 2;
    $anchorScroll();
    self.Continue = function () {
        $scope.model.ownerImpInfo = false;
        $scope.model.ownerApplicantInfo = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oPetionCurrentStatus.ApplicantInformation = true;
        $scope.model.oPetionActiveStatus.ImportantInformation = true;
    }
}];
var rapImpInfoController_resolve = {
    model: ['$route', 'alertService', function ($route, alert) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}