﻿'use strict';
var rapOResponseApplicantInfoController = ['$scope', '$modal', 'alertService', 'rapOResponseApplicantInfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 3;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerResponseInfo.ApplicantInfo.CustomerID = self.custDetails.custID;

    self.StateList = [];
    self.Hide = false;
    self.Error = '';
    self.bAcknowledgeNotification = false;
    self.bEditThirdParty = false;
    var _GetStateList = function () {
        masterFactory.GetStateList().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.StateList = response.data;
        });
    }
    _GetStateList();
    self.caseinfo.CaseFileBy = self.custDetails.custID;
    
    self.EditThirdParty = function () {
        $location.path("/Representative");
    }
    rapFactory.GetApplicationInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        if (self.caseinfo.bCaseFiledByThirdParty == false) {
            self.caseinfo.OwnerResponseInfo.ApplicantInfo.ApplicantUserInfo = self.custDetails.User;
        }
        else {
            self.caseinfo.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser = self.custDetails.User;
        }
        RestrictUpload();
        $anchorScroll();
    });

    self.Calender = masterFactory.Calender;
    self.Calender1900 = masterFactory.Calender1900;

    function RestrictUpload() {
        self.bBusinessTaxProofUpload = true;
        self.bPropertyServiceFeeUpload = true;
        for (var i = 0 ; i < self.caseinfo.Documents.length; i++) {
            if (self.caseinfo.Documents[i].DocTitle == 'OR_BusinessTaxProof') {
                self.bBusinessTaxProofUpload = false;
            }
            if (self.caseinfo.Documents[i].DocTitle == 'OR_PropertyServiceFee') {
                self.bPropertyServiceFeeUpload = false;
            }
        }
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

    self.ChangeAccountInformation = function () {
        rapGlobalFactory.IsEdit = true;
        $location.path("/editcustomerinformation");
    }

    var _CheckNotification = function () {
        var bInValid = false;
        if (self.caseinfo.bCaseFiledByThirdParty == false && self.caseinfo.OwnerResponseInfo.ApplicantInfo.bThirdPartyRepresentation == true && (self.caseinfo.OwnerResponseInfo.ApplicantInfo.ThirdPartyUser.UserID == 0 || self.bEditThirdParty == true)) {
            if (!(self.caseinfo.OwnerResponseInfo.ApplicantInfo.ThirdPartyMailNotification || self.caseinfo.OwnerResponseInfo.ApplicantInfo.ThirdPartyEmailNotification)) {
                self.Error = 'Third party notification preference is required';
                $anchorScroll();
                bInValid = true;
            }
            else if (!self.bAcknowledgeNotification) {
                self.Error = 'Please acknowledge Third party notification preference';
                $anchorScroll();
                bInValid = true;
            }
        }
        return bInValid;
    }
    self.Continue = function () {
        if (_CheckNotification()) {
            return;
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveApplicationInfo(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            MoveNext()
        });
       
    }

    function MoveNext() {
        $scope.model.oresponseApplicantInfo = false;
        $scope.model.oresponseRentalProperty = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oResponseCurrentStatus.RentalProperty = true;
        $scope.model.oResponseActiveStatus.petitionType = true;
        $scope.model.oResponseActiveStatus.ImportantInformation = true;
        $scope.model.oResponseActiveStatus.ApplicantInformation = true;
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