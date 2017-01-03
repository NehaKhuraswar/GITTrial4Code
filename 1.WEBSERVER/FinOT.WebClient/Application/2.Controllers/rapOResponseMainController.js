'use strict';
var rapOResponseMainController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', 'rapOResponsePetitionTypeFactory', 'model', function ($scope, $modal, alert, $location, rapGlobalFactory, rapFactory, model) {
    var self = this;
    self.custDetails = rapGlobalFactory.CustomerDetails;

    self.oResponseActiveStatus = null;


    var _DisableAll = function () {
        self.petitionType = false;
        self.oresponseImpInfo = false;
        self.oresponseApplicantInfo = false;
        self.oresponseRentalProperty = false;
        self.oresponseRentalHistory = false;
        self.oresponseDecreasedHousing = false;
        self.oresponseException = false;
        self.oresponseDocument = false;
        self.oresponseReview = false;
        self.oresponseVerification = false;
        self.oresponseConfirmation = false;
    };
    _DisableAll();

    self.oResponseCurrentStatus = {
        petitionType: false,
        ImportantInformation: false,
        ApplicantInformation: false,
        RentalProperty: false,
        RentHistory: false,
        DecreasedHousingServices: false,
        Exeption: false,
        AdditionalDocumentation: false,
        Review: false,
        Verification: false
    };


    self.DisableAllCurrent = function () {
        self.oResponseCurrentStatus.petitionType = false;
        self.oResponseCurrentStatus.ImportantInformation = false;
        self.oResponseCurrentStatus.ApplicantInformation = false;
        self.oResponseCurrentStatus.RentalProperty = false;
        self.oResponseCurrentStatus.RentHistory = false;
        self.oResponseCurrentStatus.DecreasedHousingServices = false;
        self.oResponseCurrentStatus.Exeption = false;
        self.oResponseCurrentStatus.AdditionalDocumentation = false;
        self.oResponseCurrentStatus.Review = false;
        self.oResponseCurrentStatus.Verification = false;
    };
    self.showPetitionType = function () {
        _DisableAll();
        self.petitionType = true;
        self.DisableAllCurrent();
        self.oResponseCurrentStatus.petitionType = true;
    };

    self.ShowOresponseImpInfo = function () {
        if (self.oResponseActiveStatus.ImportantInformation) {
            _DisableAll();
            self.oresponseImpInfo = true;
        }
    };
   
    self.ShowOresponseApplicantInfo = function () {
        if (self.oResponseActiveStatus.ApplicantInformation) {
            _DisableAll();          
            self.oresponseApplicantInfo = true; 
        }
    };
    self.ShowOresponseRentalProperty = function () {
        if (self.oResponseActiveStatus.RentalProperty) {
            _DisableAll();   
            self.oresponseRentalProperty = true;
        }
    };

    self.ShowOresponseRentalHistory = function () {
        if (self.oResponseActiveStatus.RentHistory) {
            _DisableAll();   
            self.oresponseRentalHistory = true;   
        }
    };
    self.ShowOresponseDecrasedHousing = function () {
        if (self.oResponseActiveStatus.DecreasedHousingServices) {
            _DisableAll();        
            self.oresponseDecreasedHousing = true;        
        }
    };
    self.ShowOresponseException = function () {
        if (self.oResponseActiveStatus.Review) {
            _DisableAll();
            self.oresponseException = true;
        }
    };
    self.ShowOresponseDocument = function () {
        if (self.oResponseActiveStatus.RentHisAdditionalDocumentationtory) {
            _DisableAll();
            self.oresponseDocument = true;
        }        
    };
    self.ShowOresponseReview = function () {
        if (self.oResponseActiveStatus.Review) {
            _DisableAll();
            self.oresponseReview = true;
        }        
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

    var _getPageSubmission = function () {
        rapFactory.GetOResponseSubmissionStatus(self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.oResponseActiveStatus = response.data;
        });
    }
    if (self.oResponseActiveStatus == null) {
        _getPageSubmission();
    }

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

