'use strict';
var rapTRRentalHistoryController = ['$scope', '$modal', 'alertService', 'rapTRrentalhistoryFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.RentalIncreaseModel;

    self.Calender = masterFactory.Calender;
    var _GetEmptyTenantResponseRentalIncrementInfo = function () {
        rapFactory.GetEmptyTenantResponseRentalIncrementInfo().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.RentalIncreaseModel = response.data;
        });
    }
    _GetEmptyTenantResponseRentalIncrementInfo();
    var _GetTenantResponseRentalHistoryInfo = function (tenantresponseID) {
        rapFactory.GetTenantResponseRentalHistoryInfo(tenantresponseID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.TenantResponseInfo.TenantRentalHistory = response.data;
        });
    }
    _GetTenantResponseRentalHistoryInfo(self.caseinfo.TenantResponseInfo.TenantResponseID);

    self.ContinueToReview = function () {
        var a = self.selectedObj;
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.TenantResponseInfo.TenantRentalHistory.TenantResponseID = self.caseinfo.TenantResponseInfo.TenantResponseID;
        if (self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.length == 0) {
            self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.push(self.RentalIncreaseModel);
        }
        rapFactory.SaveTenantResponseRentalHistoryInfo(rapGlobalFactory.CaseDetails.TenantResponseInfo.TenantRentalHistory, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bRentalHistory = false;
            $scope.model.bAddDocuments = true;
           // $scope.model.tPetionActiveStatus.RentHistory = true;
        });
        
    }

    self.AddAnotherRentIncrease = function (rentalIncrease) {
        var _rentalIncrease = angular.copy(rentalIncrease);
        self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.push(_rentalIncrease);
        rentalIncrease.bRentIncreaseNoticeGiven = 0;
        rentalIncrease.RentIncreaseNoticeDate = null;
        rentalIncrease.RentIncreaseEffectiveDate = null;
        rentalIncrease.RentIncreasedFrom = 0;
        rentalIncrease.RentIncreasedTo = 0;
    }

    //self.Save = function () {
    //    var a = self.selectedObj;
    //    rapGlobalFactory.CaseDetails = self.caseinfo;
    //    if (self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.length == 0) {
    //        self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.push(self.RentalIncreaseModel);
    //    }
    //    rapFactory.SaveTenantResponseRentalHistoryInfo(rapGlobalFactory.CaseDetails.TenantResponseInfo, self.caseinfo.custID).then(function (response) {
    //        if (!alert.checkResponse(response)) { return; }          
    //    });

    //}
    
}];