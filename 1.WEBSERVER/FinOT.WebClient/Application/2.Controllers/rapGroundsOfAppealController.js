'use strict';
var rapGroundsOfAppealController = ['$scope', '$modal', 'alertService', 'rapgroundsofappealFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Error = "";

    var _GetAppealDocs = function (CustomerID, DocTitle) {
        rapFactory.GetAppealDocuments(CustomerID, DocTitle).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.caseinfo.TenantAppealInfo.Documents = response.data;
        });
    }
    var _GetAppealGroundInfo = function (CaseNumber, appealFiledBy) {
        rapFactory.GetAppealGroundInfo(CaseNumber, appealFiledBy).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.caseinfo.TenantAppealInfo.AppealGrounds = response.data;
        });
    }
    _GetAppealGroundInfo(self.caseinfo.CaseID, self.custDetails.custID);
    _GetAppealDocs(self.custDetails.custID, 'A_Grounds');


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
                    document.C_ID = self.caseinfo.C_ID;
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
                    self.caseinfo.TenantAppealInfo.Documents.push(document);
                }
            }

        }
    }

    self.Delete = function (doc) {
        var index = self.caseinfo.TenantAppealInfo.Documents.indexOf(doc);
        self.caseinfo.TenantAppealInfo.Documents.splice(index, 1);
    }


    self.Download = function (doc) {
        masterFactory.GetDocument(doc);

    }

    self.ContinueToRentalHistory = function (model) {
        var selected = false;
        model.AppealGrounds.forEach(function(appealGround)
        {
            if(appealGround.Selected == true)
            {
                selected = true;
            }
        });
        if (selected == false)
        {
            self.Error = "Grounds of appeal is a required field";
            return;
        }



        model.AppealCategoryID = self.caseinfo.PetitionCategoryID;
        model.CaseNumber = self.caseinfo.CaseID;
        model.AppealFiledBy = self.custDetails.custID;
        rapFactory.SaveAppealGroundInfo(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            rapGlobalFactory.CaseDetails.TenantAppealInfo = response.data;
            $scope.model.bGrounds = false;
            $scope.model.bAddDocs = true;
            $scope.model.AppealSubmissionStatus.GroundsOfAppeal = true;
            //if (response.data == true) {
            //    $location.path("/servingappeal");
            //}
        });
    }
}];
//var rapGroundsOfAppealController_resolve = {
//    model: ['$route', 'alertService', 'rapgroundsofappealFactory', function ($route, alert, rapFactory) {
//        ////return auth.fetchToken().then(function (response) {
//        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
//        //  if (!alert.checkResponse(response)) { return; }
//        //        return response.data;
//        //    });
//    }]
//}