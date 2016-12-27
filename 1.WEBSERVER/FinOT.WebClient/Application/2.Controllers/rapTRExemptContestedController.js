'use strict';
var rapTRExemptContestedController = ['$scope', '$modal', 'alertService', 'rapTRExemptContestedFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    
    var _GetTenantResponseExemptContestedInfo = function (TenantResponseID) {
        rapFactory.GetTenantResponseExemptContestedInfo(TenantResponseID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.TenantResponseInfo.ExemptContestedInfo = response.data.TenantResponseInfo.ExemptContestedInfo;
        });
    }
    _GetTenantResponseExemptContestedInfo(self.caseinfo.TenantResponseInfo.TenantResponseID);
    
    self.ContinueToRentalHistory = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.TenantResponseInfo.ExemptContestedInfo.TenantResponseID = self.caseinfo.TenantResponseInfo.TenantResponseID;
        rapFactory.SaveTenantResponseExemptContestedInfo(rapGlobalFactory.CaseDetails.TenantResponseInfo.ExemptContestedInfo, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bExemptionContested = false;
            $scope.model.bRentalHistory = true;
           // $scope.model.tPetionActiveStatus.ExemptionContested = true;
        });
        
    }
}];