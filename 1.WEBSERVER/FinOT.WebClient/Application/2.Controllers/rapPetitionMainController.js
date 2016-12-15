'use strict';
var rapPetitionMainController = ['$scope', '$modal', 'alertService', 'rapfilepetitionFactory', '$location', 'rapGlobalFactory', 'model',function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, model) {
    var self = this;
    self.model = [];
    //self.indexModel = {
    //    bPetitionType: false,
    //    bImpInfo: false,
    //    bAppInfo: false,
    //    bGrounds: false,
    //    bRentalHistory: false,
    //    bLostServices: false,
    //    bAddDocuments: false,
    //    bReview: false,
    //    bVerification: false,
    //    bTenantPetition: false,
    //    bOwnerPetiton : false
    //};

    self.ownerImpInfo = false;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.rent = [];
    //self.selectedValue = 1;
    self.selectedObj = {};
    self.PetitionSubmissionStatus = null;
    self.oPetionActiveStatus = null;
    self.tPetionActieStatus = null;
    
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
        self.ownerImpInfo = false;
        self.ownerApplicantInfo = false;
        self.ownerJustification = false;
        self.ownerRentalProperty = false;
        self.ownerRentalHistory = false;
        self.ownerAdditionalDocuments = false;
        self.ownerReview = false;
        self.ownerVerification = false;
    };
    _DisableAll();
    

    self.showPetitionType = function () {
        _DisableAll();
        self.bPetitionType = true;
        self.DisableAllCurrent();
        self.oPetionCurrentStatus.PetitionCategory = true;
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
    self.ShowOwnerImpInfo = function () {
        if (self.oPetionActiveStatus.ImportantInformation) {
            _DisableAll();
            self.ownerImpInfo = true;
            self.oPetionCurrentStatus.ImportantInformation = true;
        }
    }
    self.ShowOwnerApplicantInfo = function () {
        if (self.oPetionActiveStatus.ApplicantInformation) {
            _DisableAll();
            self.DisableAllCurrent();
            self.ownerApplicantInfo = true;
            self.oPetionCurrentStatus.ApplicantInformation = true;
        }
    }
    self.ShowOwnerJustification = function () {
        if (self.oPetionActiveStatus.JustificationForRentIncrease) {
            _DisableAll();
            self.DisableAllCurrent();
            self.ownerJustification = true;
            self.oPetionCurrentStatus.JustificationForRentIncrease = true;
        }
    }
    self.ShowOwnerRentalProperty = function () {
        if (self.oPetionActiveStatus.RentalProperty) {
            _DisableAll();
            self.DisableAllCurrent();
            self.ownerRentalProperty = true;
            self.oPetionCurrentStatus.RentalProperty = true;
        }
    }
    self.ShowOwnerRentalHistory = function () {
        if (self.oPetionActiveStatus.RentHistory) {
            _DisableAll();
            self.DisableAllCurrent();
            self.ownerRentalHistory = true;
            self.oPetionCurrentStatus.RentHistory = true;
        }
    }
    self.ShowOwnerAdditionalDocuments = function () {
        if (self.oPetionActiveStatus.AdditionalDocumentation) {
            _DisableAll();
            self.DisableAllCurrent();
            self.ownerAdditionalDocuments = true;
            self.oPetionCurrentStatus.AdditionalDocumentation = true;
        }
    }
    self.ShowOwnerReview = function () {
        if (self.oPetionActiveStatus.Review) {
            _DisableAll();
            self.DisableAllCurrent();
            self.ownerReview = true;
            self.oPetionCurrentStatus.Review = true;
        }
    }
    self.ShowOwnerVerification = function () {
        if (self.oPetionActiveStatus.Verification) {
            _DisableAll();
            self.DisableAllCurrent();
            self.ownerVerification = true;
            self.oPetionCurrentStatus.Verification = true;
        }
    }




    var _GetCaseInfo = function () {

        
        rapFactory.GetCaseInfo(null, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.model = response.data;
            self.caseinfo = self.model;           
            rapGlobalFactory.CaseDetails = self.caseinfo;
            self.bPetitionType = true;
        });
    }

    var _getPetitionCategory = function () {
        rapFactory.GetPetitionCategory().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.model = response.data;
            self.caseinfo = self.model;
            rapGlobalFactory.CaseDetails = self.caseinfo;
            self.bPetitionType = true;
        });
    }
    
    
    var _getPageSubmission = function()
    {
        rapFactory.GetPageSubmissionStatus(self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.PetitionSubmissionStatus = response.data;
            self.oPetionActiveStatus = self.PetitionSubmissionStatus.OwnerPetition;
            self.tPetionActieStatus = self.PetitionSubmissionStatus.TenantPetition;
        });
    }
    // _getrent();
    if (self.caseinfo == null) {
        _getPetitionCategory();
    }
    if (self.PetitionSubmissionStatus == null)
    {
        _getPageSubmission();
    }

    self.oPetionCurrentStatus = {
        PetitionCategory: false,
        ImportantInformation: false,
        ApplicantInformation: false,
        JustificationForRentIncrease: false,
        RentalProperty: false,
        RentHistory: false,
        AdditionalDocumentation: false,
        Review: false,
        Verification: false
    }

    self.DisableAllCurrent = function()
    {
        self.oPetionCurrentStatus.PetitionCategory = false;
        self.oPetionCurrentStatus.ImportantInformation = false;
        self.oPetionCurrentStatus.ApplicantInformation = false;
        self.oPetionCurrentStatus.JustificationForRentIncrease = false;
        self.oPetionCurrentStatus.RentalProperty = false;
        self.oPetionCurrentStatus.RentHistory = false;
        self.oPetionCurrentStatus.AdditionalDocumentation = false;
        self.oPetionCurrentStatus.Review = false;
        self.oPetionCurrentStatus.Verification = false;
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