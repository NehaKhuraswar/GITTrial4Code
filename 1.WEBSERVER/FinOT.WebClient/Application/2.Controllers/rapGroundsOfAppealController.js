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
        model.AppealCategoryID = self.caseinfo.PetitionCategoryID;
        model.CaseNumber = self.caseinfo.CaseID;
        model.AppealFiledBy = self.custDetails.custID;
        rapFactory.SaveAppealGroundInfo(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            rapGlobalFactory.CaseDetails.TenantAppealInfo = response.data;
            $scope.model.bGrounds = false;
            $scope.model.bAddDocs = true;
            $scope.model.AppealSubmissionStatus.GroundsOfAppeal = true;
            //if (response.data == true) {
            //    $location.path("/servingappeal");
            //}
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