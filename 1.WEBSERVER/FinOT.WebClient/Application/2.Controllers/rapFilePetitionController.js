'use strict';
var rapFilePetitionController = ['$scope', '$modal', 'alertService', 'rapfilepetitionFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.rent = [];
    //self.selectedValue = 1;
    self.selectedObj = {};
    self.model = $scope.model;
    
    
    //var _getrent = function () {
    //    return rapFactory.GetRent().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.rent = response.data;
    //    });
    //}
    


    //var _GetCaseInfo = function (model) {

    //    rapFactory.GetCaseInfo().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
           
    //        self.caseinfo = response.data;           
    //        rapGlobalFactory.CaseDetails = self.caseinfo;
    //    });
    //}
    //// _getrent();
    //if (self.caseinfo == null) {
    //    _GetCaseInfo();
    //}

    self.Continue = function () {
        $scope.model.bPetitionType = false;
        $scope.model.bAppInfo = true;
        //$location.path("/applicationinfo");
    }
    //self.ContinueToGroundsforPetition = function () {
    //    $location.path("/groundsforpetition");
    //}
    //self.ContinueToRentalHistory = function () {
    //    $location.path("/rentalhistory");
    //}
    //self.ContinueToLostServices = function () {
    //    var a = self.selectedObj;
    //    $location.path("/lostservices");
    //}
    //self.ContinueToReview  = function () {
    //    $location.path("/review");
    //}
    //self.ContinueToVerification = function () {
    //    $location.path("/verification");
    //}
    ////self.SubmitPetition = function () {
    ////  //  $location.path("/verification");
    ////}
    //self.SubmitPetition = function (model) {
     

    //    rapFactory.SaveCaseInfo(model).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        $modalInstance.close(response.data);
    //    });
    //}
}];
var rapFilePetitionController_resolve = {
    model: ['$route', 'alertService', 'rapfilepetitionFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}