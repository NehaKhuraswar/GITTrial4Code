'use strict';
var rapAppellantsInfoController = ['$scope', '$modal', 'alertService', 'rapappellantsinfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.TenantAppealInfo = rapGlobalFactory.CaseDetails.TenantAppealInfo;
    self.Calender = masterFactory.Calender;


    
    self.ContinueToGroundsforAppeal = function (model) {        
        rapFactory.SaveTenantAppealInfo(model, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            rapGlobalFactory.CaseDetails.TenantAppealInfo = response.data;
            self.caseinfo = rapGlobalFactory.CaseDetails.TenantAppealInfo;
            $scope.model.bAppellantInfo = false;
            $scope.model.bGrounds = true;
        });
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