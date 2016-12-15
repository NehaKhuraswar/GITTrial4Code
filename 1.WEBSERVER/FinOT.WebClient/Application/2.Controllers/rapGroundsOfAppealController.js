'use strict';
var rapGroundsOfAppealController = ['$scope', '$modal', 'alertService', 'rapgroundsofappealFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    var _GetAppealGroundInfo = function (CaseNumber, appealFiledBy) {
        rapFactory.GetAppealGroundInfo(CaseNumber, appealFiledBy).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.TenantAppealInfo.AppealGrounds = response.data;
        });
    }
    _GetAppealGroundInfo(self.caseinfo.CaseID, self.custDetails.custID);

    self.ContinueToRentalHistory = function (model) {
        rapFactory.SaveAppealGroundInfo(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            if (response.data == true) {
                $location.path("/servingappeal");
            }
        });
    }
}];
//var rapGroundsOfAppealController_resolve = {
//    model: ['$route', 'alertService', 'rapgroundsofappealFactory', function ($route, alert, rapFactory) {
//        ////return auth.fetchToken().then(function (response) {
//        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
//        //  if (!alert.checkResponse(response)) { return; }
//        //        return response.data;
//        //    });
//    }]
//}