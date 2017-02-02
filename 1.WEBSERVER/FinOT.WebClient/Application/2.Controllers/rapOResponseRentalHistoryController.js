'use strict';
var rapOResponseRentalHistoryController = ['$scope', '$modal', 'alertService', 'rapOResponseRentalHistoryFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 5;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerResponseInfo.PropertyInfo.CustomerID = self.custDetails.custID;
    self.docDescription = null;
    self.showUploadedFile = false;
    self.Hide = false;
    self.Error = '';
    self.Calender = masterFactory.Calender;
    self.IsJustificationSelected = false;

    rapFactory.GetOResponseRentIncreaseAndPropertyInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        self.Rent = angular.copy(self.caseinfo.OwnerResponseInfo.PropertyInfo.Rent);
        RestrictUpload()
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
                        reader.readAsDataURL(file);
                        reader.onload = function (e) {
                            var base64 = e.target.result;
                            if (base64 != null) {
                                document.Base64Content = base64.substring(base64.indexOf('base64') + 7);
                            }
                        }
                        if (docTitle == 'OR_Justification')
                        {
                            document.DocDescription = angular.copy(self.docDescription);
                            self.docDescription = null;
                        }
                        self.caseinfo.Documents.push(document);
                        self.showUploadedFile = true;
                        RestrictUpload()
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
        self.caseinfo.Documents.splice(index, 1);
        RestrictUpload();
    }
    function RestrictUpload() {
   
        self.bRAPNotice1 = true;
        for (var i = 0 ; i < self.caseinfo.Documents.length; i++) {
              if (self.caseinfo.Documents[i].DocTitle == 'OR_RAPNotice1') {
                self.bRAPNotice1 = false;
            }
        }
    }
  
    var checkSelectedJustification = function (rent) {
        var isSelected = false;
        rent.RentIncreaseReasons.forEach(function (item) {
            if (item.IsSelected == true) {
                isSelected = true;                
            }
        });
        return isSelected;
    }
    self.IsRecordAdded = false;
    var checkRentRecord = function () {
        self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.forEach(function (item) {
            if (item.isDeleted == false) {
                self.IsRecordAdded = true;
            }
        });
    }
    self.AddRecord = function (_rent)
    {
        if (checkSelectedJustification(_rent)) {
            self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.push(_rent);
            self.Rent = new Object();
            self.Rent = self.caseinfo.OwnerResponseInfo.PropertyInfo.Rent;
            self.showUploadedFile = false;
        }
        else
        {
            self.Error = 'Justification is not selected';
        }
    }
    self.RemoveRecord = function (_rent) {
        var index = self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.indexOf(_rent);
        self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.splice(index, 1);
        _rent.isDeleted = true;
        self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.push(_rent);  
    }

    self.Continue = function () {
        if (self.caseinfo.OwnerResponseInfo.PropertyInfo.RAPNoticeStatusID == null || self.caseinfo.OwnerResponseInfo.PropertyInfo.RAPNoticeStatusID == 0) {
            return;
        }
        checkRentRecord();
        if (!self.IsRecordAdded)
        {
            if (checkSelectedJustification(self.Rent)) {
                self.caseinfo.OwnerResponseInfo.PropertyInfo.RentalInfo.push(self.Rent);
            }
            else
            {
                self.Error = 'Justification is not selected';
            }
        }
        
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOResponseRentIncreaseAndUpdatePropertyInfo(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
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