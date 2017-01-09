var rapAdditionalCaseDocumentController = ['$scope', '$modal', 'alertService', '$location', 'rapAdditionalCaseDocumentFactory', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, $location, rapFactory, rapGlobalFactory, masterFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CityUser;
    self.c_id = rapGlobalFactory.SelectedCase.C_ID;   
    self.DocDescriptions = masterFactory.DocDescription();

    masterFactory.DocDescription().then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        self.DocDescriptions = response.data;
    });
    self.description1 = null;
    self.description2 = null;
 
    self.Documents = null;
    rapFactory.GetCaseDocuments(self.c_id).then(function (response) {
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
                //if (filesize < 25) {
                if (filesize < masterFactory.FileSize)
                    var index = filename.lastIndexOf(".");
                var ext = filename.substring(index, filename.length).toUpperCase();
                //if (ext == '.PDF' || ext == '.DOC' || ext == '.DOCX' || ext == '.XLS' || ext == '.JPEG' || ext == '.TIFF' || ext == '.PNG') {
                if (masterFactory.FileExtensons.indexOf(ext) > -1) {
                    var document = {}; // angular.copy(self.caseinfo.Document);
                    document.DocTitle = docTitle;
                    document.DocName = filename;
                    document.MimeType = mimetype;
                    document.EmployeeID = self.custDetails.EmployeeID;
                    document.C_ID = self.c_id;
                    document.isUploaded = false;
                    document.IsPetitonFiled = true;
                    var reader = new FileReader();
                    reader.readAsArrayBuffer(file);
                    reader.onload = function (e) {
                        var arrayBuffer = e.target.result;
                        var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
                        document.Base64Content = base64String;
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
    self.Submit = function () {
         rapFactory.SaveCaseDocuments(self.Documents).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.Documents = response.data;
        });   
    }

}];

var rapAdditionalCaseDocumentController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}