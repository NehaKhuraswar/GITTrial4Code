'use strict';
var rapGroundsOfAppealController = ['$scope', '$modal', 'alertService', 'rapgroundsofappealFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    //self.ContinueToRentalHistory = function () {
    //    rapGlobalFactory.CaseDetails = self.caseinfo;
    //    $location.path("/servingappeal");
    //}
    self.ContinueToRentalHistory = function (model) {
        rapFactory.SaveAppealGroundInfo(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            // $modalInstance.close(response.data);
            //rapGlobalFactory.CaseDetails.TenantAppealInfo = response.data;
            //self.caseinfo = rapGlobalFactory.CaseDetails.TenantAppealInfo;
            if (response.data == true) {
                $location.path("/servingappeal");
            }
        });
        // rapGlobalFactory.CaseDetails = self.caseinfo;

    }
}];
var rapGroundsOfAppealController_resolve = {
    model: ['$route', 'alertService', 'rapgroundsofappealFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}