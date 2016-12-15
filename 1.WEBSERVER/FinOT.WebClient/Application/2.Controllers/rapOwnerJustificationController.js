'use strict';
var rapOwnerJustificationController = ['$scope', '$modal', 'alertService', 'rapOwnerJustificationFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    
    rapFactory.GetRentIncreaseReasonInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
    });

    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveRentIncreaseReasonInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
        });
        $scope.model.ownerJustification = false;
        $scope.model.ownerRentalProperty = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oPetionCurrentStatus.RentalProperty = true;
        $scope.model.oPetionActiveStatus.JustificationForRentIncrease = true;
    }

}];
var rapApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerJustificationFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}