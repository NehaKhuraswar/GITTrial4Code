'use strict';
var rapOResponseMainController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', 'rapOResponsePetitionTypeFactory', 'model', function ($scope, $modal, alert, $location, rapGlobalFactory, rapFactory, model) {
    var self = this;
    //self.custDetails = rapGlobalFactory.CustomerDetails;

    self.PetitionSubmissionStatus = null;
    self.oPetionActiveStatus = null;
    self.tPetionActiveStatus = null;

    var _DisableAll = function () {
        self.petitionType = false;
        self.oresponseImpInfo = false;
        self.oresponseApplicantInfo = false;
        self.oresponseRentalProperty = false;
        self.oresponseRentalHistory = false;
        self.oresponseDecreasedHousing = false;
        self.oresponseException = false;
        self.oresponseDocument = false;
      
    };
    _DisableAll();


    self.showPetitionType = function () {
        _DisableAll();
        self.petitionType = true;
        //self.DisableAllCurrent();
        //self.oPetionCurrentStatus.PetitionCategory = true;
    };

    self.ShowOwnerImpInfo = function () {
        //    if (self.oPetionActiveStatus.ImportantInformation) {
        _DisableAll();
        self.oresponseImpInfo = true;
        //        self.oPetionCurrentStatus.ImportantInformation = true;
    };
    //};
    self.ShowOresponseApplicantInfo = function () {
        //if (self.oPetionActiveStatus.ApplicantInformation) {
            _DisableAll();
            //self.DisableAllCurrent();
            self.oresponseApplicantInfo = true;
            //self.oPetionCurrentStatus.ApplicantInformation = true;
        //}
    };
    self.ShowOresponseRentalProperty = function () {
    //    if (self.oPetionActiveStatus.JustificationForRentIncrease) {
           _DisableAll();
    //        self.DisableAllCurrent();
           self.oresponseRentalProperty = true;
    //        self.oPetionCurrentStatus.JustificationForRentIncrease = true;
    //    }
    };

    self.ShowOresponseRentalHistory = function () {
    //    if (self.oPetionActiveStatus.RentHistory) {
           _DisableAll();
    //        self.DisableAllCurrent();
         self.oresponseRentalHistory = true;
    //        self.oPetionCurrentStatus.RentHistory = true;
    //    }
    };
    self.ShowOresponseDecrasedHousing = function () {
        //    if (self.oPetionActiveStatus.RentHistory) {
        _DisableAll();
        //        self.DisableAllCurrent();
        self.oresponseDecreasedHousing = true;
        //        self.oPetionCurrentStatus.RentHistory = true;
        //    }
    };
    self.ShowOresponseException = function () {
        //    if (self.oPetionActiveStatus.RentHistory) {
        _DisableAll();
        //        self.DisableAllCurrent();
        self.oresponseException = true;
        //        self.oPetionCurrentStatus.RentHistory = true;
        //    }
    };
    self.ShowOresponseDocument = function () {
        //    if (self.oPetionActiveStatus.RentHistory) {
        _DisableAll();
        //        self.DisableAllCurrent();
        self.oresponseDocument = true;
        //        self.oPetionCurrentStatus.RentHistory = true;
        //    }
    };

    //self.ShowOwnerAdditionalDocuments = function () {
    //    if (self.oPetionActiveStatus.AdditionalDocumentation) {
    //        _DisableAll();
    //        self.DisableAllCurrent();
    //        self.ownerAdditionalDocuments = true;
    //        self.oPetionCurrentStatus.AdditionalDocumentation = true;
    //    }
    //};
    //self.ShowOwnerReview = function () {
    //    if (self.oPetionActiveStatus.Review) {
    //        _DisableAll();
    //        self.DisableAllCurrent();
    //        self.ownerReview = true;
    //        self.oPetionCurrentStatus.Review = true;
    //    }
    //};
    //self.ShowOwnerVerification = function () {
    //    if (self.oPetionActiveStatus.Verification) {
    //        _DisableAll();
    //        self.DisableAllCurrent();
    //        self.ownerVerification = true;
    //        self.oPetionCurrentStatus.Verification = true;
    //    }
    //};


    //self.oPetionCurrentStatus = {
    //    PetitionCategory: false,
    //    ImportantInformation: false,
    //    ApplicantInformation: false,
    //    JustificationForRentIncrease: false,
    //    RentalProperty: false,
    //    RentHistory: false,
    //    AdditionalDocumentation: false,
    //    Review: false,
    //    Verification: false
    //};

    //self.DisableAllCurrent = function () {
    //    self.oPetionCurrentStatus.PetitionCategory = false;
    //    self.oPetionCurrentStatus.ImportantInformation = false;
    //    self.oPetionCurrentStatus.ApplicantInformation = false;
    //    self.oPetionCurrentStatus.JustificationForRentIncrease = false;
    //    self.oPetionCurrentStatus.RentalProperty = false;
    //    self.oPetionCurrentStatus.RentHistory = false;
    //    self.oPetionCurrentStatus.AdditionalDocumentation = false;
    //    self.oPetionCurrentStatus.Review = false;
    //    self.oPetionCurrentStatus.Verification = false;
    //};
   // self.showPetitionType();
   

    var _getPetitionCategory = function () {
        rapFactory.GetPetitionCategory().then(function (response) {
            if (!alert.checkResponse(response)) {
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
var rapOResponseMainController_resolve = {
    model: ['$route', 'alertService', 'rapfilepetitionFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
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

