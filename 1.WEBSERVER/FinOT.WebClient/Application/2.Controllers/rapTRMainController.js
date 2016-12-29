'use strict';
var rapTRMainController = ['$scope', '$modal', 'alertService', 'rapTRPetitionTypeFactory', '$location', 'rapGlobalFactory', 'model', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, model) {
    var self = this;
    self.model = [];

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.selectedObj = {};
    self.TRSubmissionStatus = null;

    var _DisableAll = function () {
        self.bPetitionType = false;
        self.bImpInfo = false;
        self.bAppInfo = false;
        self.bExemptionContested = false;
        self.bRentalHistory = false;
        self.bAddDocuments = false;
        self.bReview = false;
        self.bVerification = false;
        self.bConfirm = false;
    };
    _DisableAll();
    

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
    self.showExemptionContested = function () {
        _DisableAll();
        self.bExemptionContested = true;
    };
    self.showRentalHistory = function () {
        _DisableAll();
        self.bRentalHistory = true;
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

    self.bPetitionType = true;
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

    var _getPageSubmission = function () {
        rapFactory.GetTRPageSubmission(self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.TRSubmissionStatus = response.data;
        });
    }
    if (self.TRSubmissionStatus == null) {
        _getPageSubmission();
    }

    var _getPetitionCategory = function () {
        rapFactory.GetPetitionCategory().then(function (response) {
            if(!alert.checkResponse(response)) {
                return;
            }
            self.model = response.data;
            self.caseinfo = self.model;
            rapGlobalFactory.CaseDetails = self.caseinfo;
            self.petitionType = true;
        });
        }
    if (self.caseinfo == null) {
        _getPetitionCategory();
    }

}];

var rapTRMainController_resolve = {
    model: ['$route', 'alertService', 'rapTRPetitionTypeFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
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