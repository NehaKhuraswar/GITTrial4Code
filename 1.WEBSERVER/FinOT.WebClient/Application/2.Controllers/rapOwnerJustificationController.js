'use strict';
var rapOwnerJustificationController = ['$scope', '$modal', 'alertService', 'rapOwnerJustificationFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.DocDescription = null;
    self.Error = "";
    self.ShowDocs = false;
    rapFactory.GetRentIncreaseReasonInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        _ShowDocs();
    });

    var _ShowDocs = function()
    {
        self.caseinfo.Documents.forEach(function (doc) {
            if (doc.DocTitle == 'OP_Justification') {
                self.ShowDocs = true;
            }
        });
    }

    $scope.onFileSelected = function ($files,docTitle) {
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
                        self.caseinfo.Documents.push(document);
                        updateDescription();
                        self.ShowDocs = true;
                    }
                }
               
            }
        }
    }

    self.Delete = function (doc) {
        var index = self.caseinfo.Documents.indexOf(doc);
        self.caseinfo.Documents.splice(index, 1);
        _ShowDocs();
    }

  
    self.Download = function (doc) {
        masterFactory.GetDocument(doc);
    }

    function updateDescription()
    {
        for(var i=0; i<self.caseinfo.Documents.length; i++)
        {
            if (self.caseinfo.Documents[i].DocTitle == 'OP_Justification' && self.caseinfo.Documents[i].isUploaded == false)
            self.caseinfo.Documents[i].DocDescription = self.DocDescription;
        }
    }
    self.Continue = function () {
        updateDescription();
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveRentIncreaseReasonInfo(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
        
        $scope.model.ownerJustification = false;
        $scope.model.ownerRentalProperty = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oPetionCurrentStatus.RentalProperty = true;
        $scope.model.oPetionActiveStatus.JustificationForRentIncrease = true;
        });
    }

}];
var rapApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerJustificationFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}