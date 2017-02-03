'use strict';
var rapAppealMainController = ['$scope', '$modal', 'alertService', 'rapfileappealFactory', '$location', 'rapGlobalFactory', 'model', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, model) {
    var self = this;
    self.model = [];
    self.stepNo = 5;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.rent = [];
    //self.selectedValue = 1;
    self.selectedObj = {};
    self.AppealSubmissionStatus = null;
    self.Error = "";
    self.Toggle = true;
    var _getPageSubmission = function () {
        rapFactory.GetAppealPageSubmissionStatus(self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.AppealSubmissionStatus = response.data;
        });
    }
    if (self.AppealSubmissionStatus == null) {
        _getPageSubmission();
    }
    //var _getrent = function () {
    //    return rapFactory.GetRent().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.rent = response.data;
    //    });
    //}
    var _DisableAll = function () {
        self.bAppealType = false;
        self.bAppellantInfo = false;
        self.bImpInfoAppeal = false;
        self.bGrounds = false;
        self.bServingAppeal = false;
        self.bReview = false;
        self.bAddDocs = false;
        self.bConfirm = false;
    };
    _DisableAll();
    self.bAppealType = true;
    self.showAppealType = function () {
        _DisableAll();
        self.bAppealType = true;
    };

    self.showAppellantInfo = function () {
        if (self.AppealSubmissionStatus.ApplicantInformation) {
            _DisableAll();
            self.bAppellantInfo = true;
        }
    };
    self.showImpInfo = function () {
        if (self.AppealSubmissionStatus.ImportantInformation) {
            _DisableAll();
            self.bImpInfoAppeal = true;
        }
    };
    self.showGrounds = function () {
        if (self.AppealSubmissionStatus.GroundsOfAppeal) {
            _DisableAll();
            self.bGrounds = true;
        }
    };
    self.showServingAppeal = function () {
        if (self.AppealSubmissionStatus.ServingAppeal) {
            _DisableAll();
            self.bServingAppeal = true;
        }
    };
    self.showReview = function () {
        if (self.AppealSubmissionStatus.Review) {
            _DisableAll();
            self.bReview = true;
        }
    };
    self.showAddDoc = function () {
        if (self.AppealSubmissionStatus.AdditionalDocumentation) {
            _DisableAll();
            self.bAddDocs = true;
        }
    };


    //var _GetCaseInfo = function () {

        
    //    rapFactory.GetCaseInfo(null, self.custDetails.custID).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.model = response.data;
    //        self.caseinfo = self.model;           
    //        rapGlobalFactory.CaseDetails = self.caseinfo;
    //        self.bPetitionType = true;
    //    });
    //}
    //// _getrent();
    //if (self.caseinfo == null) {
    //    _GetCaseInfo();
    //}

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
var rapAppealMainController_resolve = {
    model: ['$route', 'alertService', 'rapfileappealFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //rapFactory.GetCaseInfo().then(function (response) {
        //    if (!alert.checkResponse(response)) {
        //        return;
        //    }
        //    return response.data;
        //    //self.caseinfo = response.data;
        //    //rapGlobalFactory.CaseDetails = self.caseinfo;
        //});
    }]
}