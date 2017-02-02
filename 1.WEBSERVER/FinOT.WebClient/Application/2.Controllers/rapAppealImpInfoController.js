'use strict';
var rapAppealImpInfoController = ['$scope', '$modal', 'alertService',  '$location', 'rapGlobalFactory', function ($scope, $modal, alert,  $location, rapGlobalFactory) {
    var self = this;
    $scope.model.stepNo = 2;
    
    self.Continue = function () {
        $scope.model.bImpInfoAppeal = false;
        $scope.model.bAppellantInfo = true;
        $scope.model.AppealSubmissionStatus.ImportantInformation = true;
    }
}];
var rapAppealImpInfoController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}