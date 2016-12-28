'use strict';
var rapOResponseImpInfoController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, $location, rapGlobalFactory) {
    var self = this;


    self.Continue = function () {
        $scope.model.oresponseImpInfo = false;
        $scope.model.oresponseApplicantInfo = true;
        //$scope.model.ownerImpInfo = false;
        //$scope.model.ownerApplicantInfo = true;
        ////$scope.model.DisableAllCurrent();
       // $scope.model.oPetionCurrentStatus.ApplicantInformation = true;
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