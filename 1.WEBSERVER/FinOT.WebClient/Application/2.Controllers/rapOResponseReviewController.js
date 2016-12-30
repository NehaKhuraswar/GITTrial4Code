'use strict';
var rapOResponseReviewController = ['$scope', '$modal', 'alertService', 'rapOResponseReviewFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerResponseInfo.PropertyInfo.CustomerID = self.custDetails.custID;

    self.Calender = masterFactory.Calender;


    rapFactory.GetOResponseReview(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;       
    });

   
    self.Continue = function () {
        $scope.model.oresponseReview = false;
        $scope.model.oresponseVerification = true;
        //$scope.model.ownerRentalHistory = false;
        //$scope.model.ownerAdditionalDocuments = true;
        //$scope.model.DisableAllCurrent();
        //$scope.model.oPetionCurrentStatus.AdditionalDocumentation = true;
        //$scope.model.oPetionActiveStatus.RentHistory = true;
    }
}];
var rapOResponseReviewController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalHistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}