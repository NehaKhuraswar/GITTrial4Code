'use strict';
var rapTRDocumentController = ['$scope', '$modal', 'alertService', 'ajaxService', '$location', 'rapTRDocumentFactory', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, ajaxService, $location, rapFactory, rapGlobalFactory, masterFactory) {
    var self = this;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.DocDescriptions = masterFactory.DocDescription();

    masterFactory.DocDescription().then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        self.DocDescriptions = response.data;
    });
    self.description1 = null;
    self.description2 = null;

    self.ContinueToReview = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveTRAdditionalDocuments(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
        });
        $scope.model.bAddDocuments = false;
        $scope.model.bReview = true;
        $scope.model.TRSubmissionStatus.AdditionalDocumentation = true;
       
    }
    $scope.onAdditionalFileSelect = function ($files) {
        if ($files && $files.length) {
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                popupateDocument(file, 'TR_AdditionalDocuments');
            }
        }
    }

    rapFactory.GetTRAdditionalDocuments(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
    });

    self.Delete = function (doc) {
        var index = self.caseinfo.Documents.indexOf(doc);
        self.caseinfo.Documents.splice(index, 1);
    }
    self.Download = function (doc) {
        masterFactory.GetDocument(doc);

    }
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
                    //document.DocDescription = angular.copy(self.description2);
                    var desc = angular.copy(self.description1);
                    if (desc == null) {
                        desc = angular.copy(self.description2);
                    }
                    document.DocDescription = desc;
                    self.caseinfo.Documents.push(document);
                    self.description1 = null;
                    self.description2 = null;
                }
            }

        }
    }
        
    
    
}];
var rapTRDocumentController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory)
    {
    }]
}