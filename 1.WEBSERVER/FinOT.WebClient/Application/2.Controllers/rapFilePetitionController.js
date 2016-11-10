'use strict';
var rapFilePetitionController = ['$scope', '$modal', 'alertService', 'rapfilepetitionFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = rapGlobalFactory.CustomerDetails;
    self.rent = [];
    //self.selectedValue = 1;
    self.selectedObj = {};
    var _getrent = function () {
        return rapFactory.GetRent().then(function (response) {
           // if (!alert.checkresponse(response)) { return; }
            self.rent = response.data;
        });
    }
    _getrent();

    self.Continue = function () {
        $location.path("/applicationinfo");
    }
    self.ContinueToGroundsforPetition = function () {
        $location.path("/groundsforpetition");
    }
    self.ContinueToRentalHistory = function () {
        $location.path("/rentalhistory");
    }
    self.ContinueToLostServices = function () {
        var a = self.selectedObj;
        $location.path("/lostservices");
    }
    self.ContinueToReview  = function () {
        $location.path("/review");
    }
    self.ContinueToVerification = function () {
        $location.path("/verification");
    }
    self.SubmitPetition = function () {
      //  $location.path("/verification");
    }

}];
var rapFilePetitionController_resolve = {
    model: ['$route', 'alertService', 'rapfilepetitionFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //     //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}