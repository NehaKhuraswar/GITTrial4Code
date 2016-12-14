'use strict';
var rapAppealTypeController = ['$scope', '$modal', 'alertService', 'rapappealtypeFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    
    self.ContinueToImportantInformation = function () {
        self.model.bAppealType = false;
        self.model.bImpInfoAppeal = true;
        
    }
}];
var rapAppealTypeController_resolve = {
    model: ['$route', 'alertService', 'rapappealtypeFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}