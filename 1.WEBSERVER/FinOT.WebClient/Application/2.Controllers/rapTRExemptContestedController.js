'use strict';
var rapTRExemptContestedController = ['$scope', '$modal', 'alertService', 'rapTRExemptContestedFactory', '$location', 'rapGlobalFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 4;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Error = "";
    
    var _GetTenantResponseExemptContestedInfo = function (TenantResponseID) {
        rapFactory.GetTenantResponseExemptContestedInfo(TenantResponseID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
        
            self.caseinfo.TenantResponseInfo.ExemptContestedInfo = response.data.TenantResponseInfo.ExemptContestedInfo;
            $anchorScroll();
        });
    }
    _GetTenantResponseExemptContestedInfo(self.caseinfo.TenantResponseInfo.TenantResponseID);
    
    self.ContinueToRentalHistory = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.TenantResponseInfo.ExemptContestedInfo.TenantResponseID = self.caseinfo.TenantResponseInfo.TenantResponseID;
        rapFactory.SaveTenantResponseExemptContestedInfo(rapGlobalFactory.CaseDetails.TenantResponseInfo.ExemptContestedInfo, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            $scope.model.bExemptionContested = false;
            $scope.model.bRentalHistory = true;
            $scope.model.TRSubmissionStatus.ExemptionContested = true;
        });
        
    }
}];