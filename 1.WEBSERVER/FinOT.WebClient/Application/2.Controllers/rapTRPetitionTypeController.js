'use strict';
var rapTRPetitionTypeController = ['$scope', '$modal', 'alertService', 'rapTRPetitionTypeFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.rent = [];
    self.selectedObj = {};
    self.model = $scope.model;
    self.bCaseFiledByThirdParty = false;


    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $scope.model.bPetitionType = false;
        $scope.model.bImpInfo = true;
        rapGlobalFactory.bCaseFiledByThirdParty = self.bCaseFiledByThirdParty;
                 
    }
}];
var rapTRPetitionTypeController_resolve = {
    model: ['$route', 'alertService', 'rapTRPetitionTypeFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}