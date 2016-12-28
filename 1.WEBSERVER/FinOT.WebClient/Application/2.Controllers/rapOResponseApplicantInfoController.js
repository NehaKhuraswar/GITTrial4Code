'use strict';
var rapOResponseApplicantInfoController = ['$scope', '$modal', 'alertService', 'rapOResponseApplicantInfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerResponseInfo.ApplicantInfo.CustomerID = self.custDetails.custID;

    self.StateList = [];

    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.StateList = response.data;
        });
    }
    _GetStateList();
    self.caseinfo.CaseFileBy = self.custDetails.custID;
    if (self.caseinfo.bCaseFiledByThirdParty == false) {
        self.caseinfo.OwnerResponseInfo.ApplicantInfo.ApplicantUserInfo = self.custDetails.User;
    }
    else {
        self.caseinfo.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser = self.custDetails.User;
    }
    rapFactory.GetApplicationInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
    });

    self.Calender = masterFactory.Calender;


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
                        var document = angular.copy(self.caseinfo.Document);
                        document.DocTitle = docTitle;
                        document.DocName = filename;
                        document.MimeType = mimetype;
                        document.CustomerID = self.custDetails.custID;
                        var reader = new FileReader();
                        reader.readAsArrayBuffer(file);
                        reader.onload = function (e) {
                            var arrayBuffer = e.target.result;
                            var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
                            document.Base64Content = base64String;
                        }
                        self.caseinfo.Documents.push(document);
                    }
                }

            }
        }
    }

    
    function popupateDocument(file, docTitle) {
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
                reader.readAsArrayBuffer(file);
                reader.onload = function (e) {
                    var arrayBuffer = e.target.result;
                    var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
                    document.Base64Content = base64String;
                }
                self.caseinfo.Documents.push(document);
            }
        }

    }

    self.Download = function (doc) {
        masterFactory.GetDocument(doc);
        //var blob = b64toBlob(doc.Base64Content, 'application/vnd.openxmlformats-officedocument.wordprocessingml.document');
        //var blobUrl = URL.createObjectURL(blob);
        //// window.location = blobUrl;
        //if (window.navigator && window.navigator.msSaveOrOpenBlob) {
        //    window.navigator.msSaveOrOpenBlob(blob);
        //}
        //else {

        //    window.open(blobUrl);
        //}      

    }

    //function b64toBlob(b64Data, contentType, sliceSize) {
    //    contentType = contentType || '';
    //    sliceSize = sliceSize || 512;

    //    var byteCharacters = atob(b64Data);
    //    var byteArrays = [];

    //    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
    //        var slice = byteCharacters.slice(offset, offset + sliceSize);

    //        var byteNumbers = new Array(slice.length);
    //        for (var i = 0; i < slice.length; i++) {
    //            byteNumbers[i] = slice.charCodeAt(i);
    //        }

    //        var byteArray = new Uint8Array(byteNumbers);

    //        byteArrays.push(byteArray);
    //    }

    //    var blob = new Blob(byteArrays, { type: contentType });
    //    return blob;
    //}

    self.ChangeAccountInformation = function () {
        rapGlobalFactory.IsEdit = true;
        $location.path("/editcustomerinformation");
    }


    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveApplicationInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
        });
        $scope.model.ownerApplicantInfo = false;
        $scope.model.ownerJustification = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oPetionCurrentStatus.JustificationForRentIncrease = true;
        $scope.model.oPetionActiveStatus.PetitionCategory = true;
        $scope.model.oPetionActiveStatus.ImportantInformation = true;
        $scope.model.oPetionActiveStatus.ApplicantInformation = true;
    }

}];
var rapOResponseApplicantInfoController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerApplicantInfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}