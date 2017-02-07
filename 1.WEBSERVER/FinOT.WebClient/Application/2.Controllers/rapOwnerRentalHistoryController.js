﻿'use strict';
var rapOwnerRentalHistoryController = ['$scope', '$modal', 'alertService', 'rapOwnerRentalHistoryFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.OwnerPetitionInfo.PropertyInfo.CustomerID = self.custDetails.custID;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.showUploadedFile = false;
    self.Calender = masterFactory.Calender;
    self.Error = "";
    self.Hide = false;
    self.HasAdditionalRentRecord = false;
    $scope.model.stepNo = 6;
    //var range = 10 / 2;
    //var currentYear = new Date().getFullYear();
    //self.years = [];
    //for (var i = range; i > 0 ; i--) {

    //    self.years.push(currentYear - i);
    //}
    //for (var i = 0; i < range + 1; i++) {
    //    self.years.push(currentYear + i);
    //}

    rapFactory.GetOwnerRentIncreaseAndPropertyInfo(self.caseinfo).then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        self.Rent = angular.copy(self.caseinfo.OwnerPetitionRentalIncrementInfo);
        //if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.length > 0) {
        //    self.caseinfo.OwnerPetitionRentalIncrementInfo = self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo[0];
        //}
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
                        self.showUploadedFile = true;
                    }
                }

            }
        }
    }

    var _AdditionalRentRecordCheck = function () {
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.forEach(function (item) {
            if (item.isDeleted == false) {
                self.HasAdditionalRentRecord = true;            }
        });
    }
    self.Download = function (doc) {
        masterFactory.GetDocument(doc);    
    }

    self.Delete = function (doc) {
        var index = self.caseinfo.Documents.indexOf(doc);
        self.caseinfo.Documents.splice(index, 1);
    }

    self.AddRecord = function (_rent) {
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.push(_rent);
        self.Rent = new Object();
        self.Rent = self.caseinfo.OwnerPetitionRentalIncrementInfo;
        self.showUploadedFile = false;
    }
    self.RemoveRecord = function (_rent) {
        var index = self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.indexOf(_rent);
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.splice(index, 1);
        _rent.isDeleted = true;
        self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.push(_rent);
    }
    self.Continue = function () {
        if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.RAPNoticeStatusID == null || self.caseinfo.OwnerPetitionInfo.PropertyInfo.RAPNoticeStatusID == 0) {
            return;
        }
        _AdditionalRentRecordCheck();
        if (!self.HasAdditionalRentRecord) {
            self.caseinfo.OwnerPetitionInfo.PropertyInfo.RentalInfo.push(self.Rent);
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOwnerRentIncreaseAndUpdatePropertyInfo(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            $scope.model.ownerRentalHistory = false;
            $scope.model.ownerAdditionalDocuments = true;
            $scope.model.DisableAllCurrent();
            $scope.model.oPetionCurrentStatus.AdditionalDocumentation = true;
            $scope.model.oPetionActiveStatus.RentHistory = true;
        });
  
    }
}];
var rapOwnerRentalHistoryController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalHistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}