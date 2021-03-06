﻿var rapOwnerDocumentsController = ['$scope', '$modal', 'alertService', '$location', 'rapOwnerDocumentFactory', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, $location, rapFactory, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    $scope.model.stepNo = 7;
    self.DocDescriptions = masterFactory.DocDescription();
    self.Error = "";
    
    masterFactory.DocDescription().then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
        self.DocDescriptions = response.data;     
    });
    self.description1 = null;
    self.description2 = null;
    
    rapFactory.GetOwnerAdditionalDocuments(self.caseinfo).then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        $anchorScroll();
    });

    $scope.onFileSelected = function ($files, docTitle) {
        if ($files && $files.length) {
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                var filename = file.name;
                var mimetype = file.type;
                var filesize = ((file.size / 1024) / 1024).toFixed(4);
                //if (filesize < 25) {
                if (filesize < masterFactory.FileSize)  
                    var index = filename.lastIndexOf(".");
                    var ext = filename.substring(index, filename.length).toUpperCase();
                    //if (ext == '.PDF' || ext == '.DOC' || ext == '.DOCX' || ext == '.XLS' || ext == '.JPEG' || ext == '.TIFF' || ext == '.PNG') {
                    if (masterFactory.FileExtensons.indexOf(ext) > -1) {
                        var document = angular.copy(self.caseinfo.Document);
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
                        var desc = angular.copy(self.description1);
                        if (desc == null) {
                            desc = angular.copy(self.description2);
                        }
                        document.DocDescription =desc
                        self.caseinfo.Documents.push(document);
                        self.description1 = null;
                        self.description2 = null;
                    }
                }

            }
        }
    

    self.Download = function (doc) {
        masterFactory.GetDocument(doc);

    }

    self.Delete = function (doc) {
        var index = self.caseinfo.Documents.indexOf(doc);
        self.caseinfo.Documents.splice(index, 1);
    }

    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;       
        rapFactory.SaveOwnerAdditionalDocuments(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            $scope.model.ownerAdditionalDocuments = false;
            $scope.model.ownerReview = true;
            $scope.model.DisableAllCurrent();
            $scope.model.oPetionCurrentStatus.Review = true;
            $scope.model.oPetionActiveStatus.AdditionalDocumentation = true;
        });
       
    }
        
}];

var rapOwnerDocumentsController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {
   
    }]
}