'use strict';
var rapRentalHistoryController = ['$scope', '$modal', 'alertService', 'raprentalhistoryFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
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

   // self.Continue = function () {
   //     $location.path("/applicationinfo");
   // }
   // self.ContinueToGroundsforPetition = function () {
   //     $location.path("/groundsforpetition");
   // }
   // self.ContinueToRentalHistory = function () {
   //     $location.path("/rentalhistory");
   // }
    self.ContinueToLostServices = function () {
        var a = self.selectedObj;
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $scope.model.bRentalHistory = false;
        $scope.model.bLostServices = true;
    }
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
var rapRentalHistoryController_resolve = {
    model: ['$route', 'alertService', 'raprentalhistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}