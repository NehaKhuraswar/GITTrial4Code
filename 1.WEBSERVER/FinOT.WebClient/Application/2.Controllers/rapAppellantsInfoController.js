'use strict';
var rapAppellantsInfoController = ['$scope', '$modal', 'alertService', 'rapappellantsinfoFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;


    
    self.ContinueToGroundsforAppeal = function (model) {
        $scope.model.bAppellantInfo = false;
        $scope.model.bGrounds = true;
        //rapFactory.SaveTenantAppealInfo(model).then(function (response) {
        //    if (!alert.checkResponse(response)) {
        //        return;
        //    }
        //    // $modalInstance.close(response.data);
        //    rapGlobalFactory.CaseDetails.TenantAppealInfo = response.data;
        //    self.caseinfo = rapGlobalFactory.CaseDetails.TenantAppealInfo;
        //    $location.path("/groundsforappeal");
       // });
       // rapGlobalFactory.CaseDetails = self.caseinfo;
        
    }
}];
var rapAppellantsInfoController_resolve = {
    model: ['$route', 'alertService', 'rapappellantsinfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}