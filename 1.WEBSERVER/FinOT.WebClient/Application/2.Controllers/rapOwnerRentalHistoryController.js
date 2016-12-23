'use strict';
var rapOwnerRentalHistoryController = ['$scope', '$modal', 'alertService', 'rapOwnerRentalHistoryFactory', '$location', 'rapGlobalFactory','masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory,masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.OwnerPetitionInfo.PropertyInfo.CustomerID = self.custDetails.custID;
    
    self.Calender = masterFactory.Calender;

    //var range = 10 / 2;
    //var currentYear = new Date().getFullYear();
    //self.years = [];
    //for (var i = range; i > 0 ; i--) {

    //    self.years.push(currentYear - i);
    //}
    //for (var i = 0; i < range + 1; i++) {
    //    self.years.push(currentYear + i);
    //}

    rapFactory.GetOwnerRentIncreaseAndPropertyInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.length > 0) {
            self.caseinfo.OwnerPetitionRentalIncrementInfo = self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo[0];
        }
    });

    self.Continue = function () {
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.push(self.caseinfo.OwnerPetitionRentalIncrementInfo);
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOwnerRentIncreaseAndUpdatePropertyInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
        });
        $scope.model.ownerRentalHistory = false;
        $scope.model.ownerAdditionalDocuments = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oPetionCurrentStatus.AdditionalDocumentation = true;
        $scope.model.oPetionActiveStatus.RentHistory = true;
    }
}];
var rapOwnerRentalHistoryController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalHistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}