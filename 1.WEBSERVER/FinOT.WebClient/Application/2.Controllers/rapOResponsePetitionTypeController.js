'use strict';
var rapOResponsePetitionTypeController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, $location, rapGlobalFactory) {
    var self = this;
    // self.model = model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    //self.rent = [];
    //self.selectedValue = 1;
    //self.selectedObj = {};
    self.model = $scope.model;
    //self.bCaseFiledByThirdParty = false;

    //var _getrent = function () {
    //    return rapFactory.GetRent().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.rent = response.data;
    //    });
    //}
    //self.ShowtPetitionForm = function () {
    //    if (self.caseinfo.PetitionCategoryID == 1)
    //    {
    //        $scope.model.bTenantPetition = true;
    //        $scope.model.bOwnerPetiton = false;
    //    }
    //    else if (self.caseinfo.PetitionCategoryID == 2)
    //    {
    //        $scope.model.bOwnerPetiton = true;
    //        $scope.model.bTenantPetition = false;
    //    }
    //}




    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;       
        $scope.model.petitionType = false;
        $scope.model.oresponseImpInfo = true;
        $scope.model.oResponseCurrentStatus.ImportantInformation = true;
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