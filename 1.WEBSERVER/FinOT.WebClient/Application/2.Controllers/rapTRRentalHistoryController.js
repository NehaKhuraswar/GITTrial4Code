'use strict';
var rapTRRentalHistoryController = ['$scope', '$modal', 'alertService', 'rapTRrentalhistoryFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.RentalIncreaseModel;

    self.Calender = masterFactory.Calender;
    var _GetEmptyTenantResponseRentalIncrementInfo = function () {
        rapFactory.GetEmptyTenantResponseRentalIncrementInfo().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.RentalIncreaseModel = response.data;
        });
    }
    _GetEmptyTenantResponseRentalIncrementInfo();
    var _GetTenantResponseRentalHistoryInfo = function (tenantresponseID) {
        rapFactory.GetTenantResponseRentalHistoryInfo(tenantresponseID, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.TenantResponseInfo.TenantRentalHistory = response.data;
        });
    }
    _GetTenantResponseRentalHistoryInfo(self.caseinfo.TenantResponseInfo.TenantResponseID);

    self.ContinueToReview = function () {
        var a = self.selectedObj;
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.TenantResponseInfo.TenantRentalHistory.TenantResponseID = self.caseinfo.TenantResponseInfo.TenantResponseID;
        if (self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.length == 0) {
            self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.push(self.RentalIncreaseModel);
        }
        rapFactory.SaveTenantResponseRentalHistoryInfo(rapGlobalFactory.CaseDetails.TenantResponseInfo.TenantRentalHistory, self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bRentalHistory = false;
            $scope.model.bAddDocuments = true;
            $scope.model.TRSubmissionStatus.RentHistory = true;
        });
        
    }
    self.Download = function (doc) {
        masterFactory.GetDocument(doc);

    }
    self.AddAnotherRentIncrease = function (rentalIncrease) {
        var _rentalIncrease = angular.copy(rentalIncrease);
        self.caseinfo.TenantResponseInfo.TenantRentalHistory.RentIncreases.push(_rentalIncrease);
        rentalIncrease.bRentIncreaseNoticeGiven = 0;
        rentalIncrease.RentIncreaseNoticeDate = null;
        rentalIncrease.RentIncreaseEffectiveDate = null;
        rentalIncrease.RentIncreasedFrom = 0;
        rentalIncrease.RentIncreasedTo = 0;
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
                        var document = angular.copy(self.caseinfo.TenantResponseInfo.TenantRentalHistory.Document);
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
                        self.caseinfo.TenantResponseInfo.TenantRentalHistory.Documents.push(document);
                    }
                }

            }
        }
    }
    
}];