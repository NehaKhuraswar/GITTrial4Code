'use strict';
var rapLostServicesController = ['$scope', '$modal', 'alertService', 'raplostservicesFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.LostServices;
    self.Problems;
    self.Error = "";
    $scope.model.stepNo = 6;

    var range = 10 / 2;
    var currentYear = new Date().getFullYear();
    self.years = [];
    for (var i = range; i > 0 ; i--) {

        self.years.push(currentYear - i);
    }
    for (var i = 0; i < range + 1; i++) {
        self.years.push(currentYear  + i);
    }
    self.Calender = masterFactory.Calender;
    var _GetEmptyLostServicesInfo = function () {
        rapFactory.GetEmptyLostServicesInfo().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.LostServices = response.data;
            $anchorScroll();
         });
     }
    _GetEmptyLostServicesInfo();

    var _GetEmptyProblemsInfo = function() {
        rapFactory.GetEmptyProblemsInfo().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                $anchorScroll();
                return;
            }
            self.Problems = response.data;
            $anchorScroll();
        });
     }
    _GetEmptyProblemsInfo();


     var _GetTenantLostServiceInfo = function (petitionId) {
         rapFactory.GetTenantLostServiceInfo(petitionId, self.custDetails.custID).then(function (response) {
             if (!alert.checkForResponse(response)) {
                 self.Error = rapGlobalFactory.Error;
                 $anchorScroll();
                 return;
             }
             self.caseinfo.TenantPetitionInfo.LostServicesPage = response.data;
             $anchorScroll();
        });
    }
    _GetTenantLostServiceInfo(self.caseinfo.TenantPetitionInfo.PetitionID);

    self.AddAnotherLostServices = function (lostservice) {
        var _lostservice = angular.copy(lostservice);
        self.caseinfo.TenantPetitionInfo.LostServicesPage.LostServices.push(_lostservice);
        lostservice.LossBeganDate = null;
        lostservice.ReducedServiceDescription = null;
        lostservice.EstimatedLoss = 0;
    }

    self.AddAnotherProblem = function (problem) {
        var _problem = angular.copy(problem);
        self.caseinfo.TenantPetitionInfo.LostServicesPage.Problems.push(_problem);
        problem.ProblemBeganDate = null;
        problem.ProblemDescription = null;
        problem.EstimatedLoss = 0;
    }
    self.ContinueToDocument = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.TenantPetitionInfo.LostServicesPage.PetitionID = self.caseinfo.TenantPetitionInfo.PetitionID;
        if (self.caseinfo.TenantPetitionInfo.LostServicesPage.Problems.length == 0) {
            if (self.Problems.ProblemBeganDate != null || self.Problems.PayingToProblemBeganDate != null) {

                self.caseinfo.TenantPetitionInfo.LostServicesPage.Problems.push(self.Problems);
            }
        }
        if (self.caseinfo.TenantPetitionInfo.LostServicesPage.LostServices.length == 0) {
            if (self.LostServices.PayingToServiceBeganDate != null || self.LostServices.LossBeganDate != null) {
                self.caseinfo.TenantPetitionInfo.LostServicesPage.LostServices.push(self.LostServices);
            }
        }
        rapFactory.SaveTenantLostServiceInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo.LostServicesPage, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            $scope.model.bLostServices = false;
            $scope.model.bAddDocuments = true;
            $scope.model.tPetionActiveStatus.LostService = true;
        });
       // $location.path("/document");
         
    }

    $scope.onFileSelected = function ($files, docTitle) {
        if ($files && $files.length) {
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                var filename = file.name;
                var mimetype = file.type;
                var filesize = ((file.size / 1024) / 1024).toFixed(4);
                //if (filesize < 25) {
                if (filesize < masterFactory.FileSize) {
                    var index = filename.lastIndexOf(".");
                    var ext = filename.substring(index, filename.length).toUpperCase();
                    //if (ext == '.PDF' || ext == '.DOC' || ext == '.DOCX' || ext == '.XLS' || ext == '.JPEG' || ext == '.TIFF' || ext == '.PNG') {
                    if (masterFactory.FileExtensons.indexOf(ext) > -1) {
                        var document = angular.copy(self.caseinfo.TenantPetitionInfo.LostServicesPage.Document);
                        document.DocTitle = docTitle;
                        document.DocName = filename;
                        document.MimeType = mimetype;
                        document.CustomerID = self.custDetails.custID;
                        var reader = new FileReader();
                        reader.readAsDataURL(file);
                        reader.onload = function (e) {
                            var base64 = e.target.result;
                            if (base64 != null) {
                                document.Base64Content = base64.substring(base64.indexOf('base64') + 7);
                            }
                        }
                        self.caseinfo.TenantPetitionInfo.LostServicesPage.Documents.push(document);
                    }
                }

            }
        }
    }


    self.Download = function (doc) {
        masterFactory.GetDocument(doc);
    }

    self.Delete = function (doc) {
        var index = self.caseinfo.Documents.indexOf(doc);
        self.caseinfo.TenantPetitionInfo.LostServicesPage.Documents.splice(index, 1);
    }
}];
var rapLostServicesController_resolve = {
    model: ['$route', 'alertService', 'raplostservicesFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}