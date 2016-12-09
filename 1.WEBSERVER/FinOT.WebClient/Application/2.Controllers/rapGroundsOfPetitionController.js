'use strict';
var rapGroundsOfPetitionController = ['$scope', '$modal', 'alertService', 'rapgroundsofpetitionFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    
    var _GetPetitionGroundInfo = function (petitionId) {
        rapFactory.GetPetitionGroundInfo(petitionId).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.TenantPetitionInfo.PetitionGrounds = response.data;
        });
    }
    _GetPetitionGroundInfo(self.caseinfo.TenantPetitionInfo.PetitionID);
    
    //var _getrent = function () {
    //    return rapFactory.GetRent().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.rent = response.data;
    //    });
    //}

   // var _GetCaseInfo = function (model) {

   //     rapFactory.GetCaseInfo().then(function (response) {
   //         if (!alert.checkResponse(response)) {
   //             return;
   //         }
           
   //         self.caseinfo = response.data;           

   //     });
   // }
   //// _getrent();
   // _GetCaseInfo();

    //self.Continue = function () {
    //    $location.path("/applicationinfo");
    //}
    //self.ContinueToGroundsforPetition = function () {
    //    $location.path("/groundsforpetition");
    //}
    self.ContinueToRentalHistory = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SavePetitionGroundInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bGrounds = false;
            $scope.model.bRentalHistory = true;
        });
        
    }
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
    //self.SubmitPetition = function () {
    //  //  $location.path("/verification");
    //}
    //self.SubmitPetition = function (model) {
     

    //    rapFactory.SaveCaseInfo(model).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        $modalInstance.close(response.data);
    //    });
    //}
}];
var rapGroundsOfPetitionController_resolve = {
    model: ['$route', 'alertService', 'rapgroundsofpetitionFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}