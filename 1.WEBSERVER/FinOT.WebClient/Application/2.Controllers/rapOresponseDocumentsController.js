var rapOresponseDocumentsController = ['$scope', '$modal', 'alertService', '$location', 'rapOResponseDocumentFactory', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, $location, rapFactory, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 8;
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
    self.Hide = false;
    self.Error = '';
    rapFactory.GetOResponseAdditionalDocuments(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
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
                    document.DocDescription = desc
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
        RestrictUpload();
    }
    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOResponseAdditionalDocuments(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            MoveNext();
        });      
    }
    function MoveNext() {
        $scope.model.oresponseDocument = false;
        $scope.model.oresponseReview = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oResponseCurrentStatus.Review = true;
        $scope.model.oResponseActiveStatus.AdditionalDocumentation = true;
    }

}];

var rapOresponseDocumentsController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}