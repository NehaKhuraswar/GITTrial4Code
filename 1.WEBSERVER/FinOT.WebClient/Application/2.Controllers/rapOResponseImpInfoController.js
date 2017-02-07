 'use strict';
 var rapOResponseImpInfoController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', '$anchorScroll', function ($scope, $modal, alert, $location, rapGlobalFactory, $anchorScroll) {
    var self = this;
    $scope.model.stepNo = 2;
    $anchorScroll();
    self.Continue = function () {
        $scope.model.oresponseImpInfo = false;
        $scope.model.oresponseApplicantInfo = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oResponseCurrentStatus.ApplicantInformation = true;
    }

}];
var rapOResponseImpInfoController_resolve = {
    model: ['$route', 'alertService', function ($route, alert) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}