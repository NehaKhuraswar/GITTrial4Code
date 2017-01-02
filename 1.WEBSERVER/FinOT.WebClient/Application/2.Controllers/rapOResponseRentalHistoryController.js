'use strict';
var rapOResponseRentalHistoryController = ['$scope', '$modal', 'alertService', 'rapOResponseRentalHistoryFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerResponseInfo.PropertyInfo.CustomerID = self.custDetails.custID;
    self.docDescription = null;
    self.showUploadedFile = false;

    self.Calender = masterFactory.Calender;


    rapFactory.GetOResponseRentIncreaseAndPropertyInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        self.Rent = angular.copy(self.caseinfo.OwnerResponseInfo.PropertyInfo.Rent);
        //if (self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.length > 0) {
        //    self.caseinfo.OwnerPetitionRentalIncrementInfo = self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo[0];
        //}
    });
    
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
                        if (docTitle == 'OR_Justification')
                        {
                            document.DocDescription = angular.copy(self.docDescription);
                            self.docDescription = null;
                        }
                        self.caseinfo.Documents.push(document);
                        self.showUploadedFile = true;
                    }
                }

            }
        }
    }


    self.Download = function (doc) {
        masterFactory.GetDocument(doc);

    }

    self.AddRecord = function (_rent)
    {
        self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.push(_rent);
        self.Rent = new Object();
        self.Rent = self.caseinfo.OwnerResponseInfo.PropertyInfo.Rent;
        self.showUploadedFile = false;
    }
    self.RemoveRecord = function (_rent) {
        var index = self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.indexOf(_rent);
        self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.splice(index, 1);
        _rent.isDeleted = true;
        self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.push(_rent);  
    }

    self.Continue = function () {
        if (self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.length == 0)
        {
            self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.push(self.Rent);
        }
        
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOResponseRentIncreaseAndUpdatePropertyInfo(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
            MoveNext()
        });       
    }
    function MoveNext() {
        $scope.model.oresponseRentalHistory = false;
        $scope.model.oresponseDecreasedHousing = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oResponseCurrentStatus.DecreasedHousingServices = true;
        $scope.model.oResponseActiveStatus.RentHistory = true;
    }

}];
var rapOResponseRentalHistoryControllerr_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalHistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}