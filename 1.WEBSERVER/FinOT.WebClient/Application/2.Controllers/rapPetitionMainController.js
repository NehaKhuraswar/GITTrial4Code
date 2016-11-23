'use strict';
var rapPetitionMainController = ['$scope', '$modal', 'alertService', 'rapfilepetitionFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.rent = [];
    //self.selectedValue = 1;
    self.selectedObj = {};
    
    
    //var _getrent = function () {
    //    return rapFactory.GetRent().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.rent = response.data;
    //    });
    //}
    var _DisableAll = function () {
        self.bPetitionType = false;
        self.bImpInfo = false;
        self.bAppInfo = false;
        self.bGrounds = false;
        self.bRentalHistory = false;
        self.bLostServices = false;
        self.bAddDocuments = false;
        self.bReview = false;
        self.bVerification = false;
    };
    _DisableAll();
    self.bPetitionType = true;

    self.showPetitionType = function () {
        _DisableAll();
        self.bPetitionType = true;
    };
    self.showImpInfo = function () {
        _DisableAll();
        self.bImpInfo = true;
    };
    self.showAppInfo = function () {
        _DisableAll();
        self.bAppInfo = true;
    };
    self.showGrounds = function () {
        _DisableAll();
        self.bGrounds = true;
    };
    self.showRentalHistory = function () {
        _DisableAll();
        self.bRentalHistory = true;
    };
    self.showLostServices = function () {
        _DisableAll();
        self.bLostServices = true;
    };
    self.showAddDocuments = function () {
        _DisableAll();
        self.bAddDocuments = true;
    };
    self.showReview = function () {
        _DisableAll();
        self.bReview = true;
    };
    self.showVerification = function () {
        _DisableAll();
        self.bVerification = true;
    };


    var _GetCaseInfo = function (model) {

        rapFactory.GetCaseInfo().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
           
            self.caseinfo = response.data;           
            rapGlobalFactory.CaseDetails = self.caseinfo;
        });
    }
    // _getrent();
    if (self.caseinfo == null) {
        _GetCaseInfo();
    }

    //self.Continue = function () {
    //    $location.path("/applicationinfo");
    //}
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
var rapPetitionMainController_resolve = {
    model: ['$route', 'alertService', 'rapfilepetitionFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}