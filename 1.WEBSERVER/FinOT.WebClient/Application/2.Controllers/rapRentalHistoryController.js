'use strict';
var rapRentalHistoryController = ['$scope', '$modal', 'alertService', '$http', 'raprentalhistoryFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, $http, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.RentalIncreaseModel;
    self.Error = "";
    $http.get('..js/tooltips.json').success(function (data) {
        $scope.tooltips = data;
    });
    var range = 10 / 2;
    var currentYear = new Date().getFullYear();
    self.years = [];
    for (var i = range; i > 0 ; i--) {

        self.years.push(currentYear - i);
    }
    for (var i = 0; i < range + 1; i++) {
        self.years.push(currentYear + i);
    }
    self.Calender = masterFactory.Calender;
    var _GetEmptyTenantRentalIncrementInfo = function () {
        rapFactory.GetEmptyTenantRentalIncrementInfo().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
        }
            self.RentalIncreaseModel = response.data;
        });
    }
    _GetEmptyTenantRentalIncrementInfo();
    var _GetRentalHistoryInfo = function (petitionId) {
        rapFactory.GetRentalHistoryInfo(petitionId).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.caseinfo.TenantPetitionInfo.TenantRentalHistory = response.data;
        });
    }
    _GetRentalHistoryInfo(self.caseinfo.TenantPetitionInfo.PetitionID);

    self.ContinueToLostServices = function () {
        var a = self.selectedObj;
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.TenantPetitionInfo.TenantRentalHistory.PetitionID = self.caseinfo.TenantPetitionInfo.PetitionID;
        if (self.caseinfo.TenantPetitionInfo.TenantRentalHistory.RentIncreases.length == 0) {
            if (self.RentalIncreaseModel.RentIncreaseNoticeDate != null && self.RentalIncreaseModel.RentIncreaseEffectiveDate != null)
                {
            self.caseinfo.TenantPetitionInfo.TenantRentalHistory.RentIncreases.push(self.RentalIncreaseModel);
            }
        }
        rapFactory.SaveTenantRentalHistoryInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo.TenantRentalHistory, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bRentalHistory = false;
            $scope.model.bLostServices = true;
            $scope.model.tPetionActiveStatus.RentHistory = true;
        });
        
    }

    self.AddAnotherRentIncrease = function (rentalIncrease) {
        var _rentalIncrease = angular.copy(rentalIncrease);
        self.caseinfo.TenantPetitionInfo.TenantRentalHistory.RentIncreases.push(_rentalIncrease);
        rentalIncrease.bRentIncreaseNoticeGiven = 0;
        rentalIncrease.RentIncreaseNoticeDate = null;
        rentalIncrease.RentIncreaseEffectiveDate = null;
        rentalIncrease.RentIncreasedFrom = null;
        rentalIncrease.RentIncreasedTo = null;
        rentalIncrease.bRentIncreaseContested = 0;

    }

    self.Save = function () {
        var a = self.selectedObj;
        rapGlobalFactory.CaseDetails = self.caseinfo;
        if (self.caseinfo.TenantPetitionInfo.TenantRentalHistory.RentIncreases.length == 0) {
            self.caseinfo.TenantPetitionInfo.TenantRentalHistory.RentIncreases.push(self.RentalIncreaseModel);
        }
        rapFactory.SaveTenantRentalHistoryInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo, self.caseinfo.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
                }
        });

    }
    
}];
var rapRentalHistoryController_resolve = {
    model: ['$route', 'alertService', 'raprentalhistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}