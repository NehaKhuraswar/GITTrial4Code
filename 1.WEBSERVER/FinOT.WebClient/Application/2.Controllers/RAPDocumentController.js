    'use strict';
    var rapDocumentController = ['$scope', '$modal', 'alertService', 'ajaxService', '$location', 'rapGlobalFactory', 'rapTenantlDocumentFactory', 'masterdataFactory', function ($scope, $modal, alert, ajaxService, $location, rapGlobalFactory, rapFactory, masterFactory) {
        var self = this;
        self.model = $scope.model;
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
        self.Documents = null;
        rapFactory.GetTenantDocuments(self.custDetails.custID, 'TP_AdditionalDocuments').then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.Documents = response.data;
        });

        $scope.onFileSelected = function ($files, docTitle) {
            if ($files && $files.length) {
                for (var i = 0; i < $files.length; i++) {
                    var file = $files[i];
                    var filename = file.name;
                    var mimetype = file.type;
                    var filesize = ((file.size / 1024) / 1024).toFixed(4);
                    if (filesize < masterFactory.FileSize)
                        var index = filename.lastIndexOf(".");
                    var ext = filename.substring(index, filename.length).toUpperCase();
                    if (masterFactory.FileExtensons.indexOf(ext) > -1) {
                        var document = {};
                        document.DocTitle = docTitle;
                        document.DocName = filename;
                        document.MimeType = mimetype;
                        document.CustomerID = self.custDetails.custID;                       
                        document.isUploaded = false;
                        document.IsPetitonFiled = false;
                        var reader = new FileReader();
                        reader.readAsDataURL(file);
                        reader.onload = function (e) {
                            var base64 = e.target.result;
                            if (base64 != null) {
                                document.Base64Content = base64.substring(base64.indexOf('base64') + 7)
                            }
                        }
                        var desc = angular.copy(self.description1);
                        if (desc == '<--Select-->') {
                            desc = angular.copy(self.description2);
                        }
                        document.DocDescription = desc
                        self.Documents.push(document);
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
            var index = self.Documents.indexOf(doc);
            self.Documents.splice(index, 1);
        }

    self.ContinueToReview = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveTenantDocuments(self.Documents).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bAddDocuments = false;
            $scope.model.bReview = true;
            $scope.model.tPetionActiveStatus.AdditionalDocumentation = true;
        });      
       
    }
   
    
    
}];
var rapDocumentController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory)
    {
    }]
}