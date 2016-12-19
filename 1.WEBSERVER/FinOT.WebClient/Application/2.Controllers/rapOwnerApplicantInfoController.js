'use strict';
var rapOwnerApplicantInfoController = ['$scope', '$modal', 'alertService', 'rapOwnerApplicantInfoFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.OwnerPetitionInfo.ApplicantInfo.CustomerID = self.custDetails.custID;
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
    if (self.caseinfo.bCaseFiledByThirdParty == false)
    {
        self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo = self.custDetails.User;        
    }
    else
    {
        self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser = self.custDetails.User;
    }
    rapFactory.GetApplicationInfo(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
    });

    self.Calender = masterFactory.Calender;

    $scope.onFileSelect = function ($files) {
        if ($files && $files.length)
        {
            for (var i = 0; i < $files.length; i++) {          
                var file = $files[i];
                var filename = file.name;
                var filesize = ((file.size / 1024) / 1024).toFixed(4);
                if (filesize < 5) {
                    var index = filename.lastIndexOf(".");
                    var ext = filename.substring(index, filename.length).toUpperCase();
                    if (ext == '.PDF' || ext == '.DOC' || ext == '.DOCX' || ext == '.XLS') {
                        var document = self.caseinfo.Document;
                        document.DocTitle = 'Business Tax Proof'
                        document.DocName = filename;
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
        $scope.model.tPetionActiveStatus.PetitionCategory = true;
        $scope.model.tPetionActiveStatus.ImportantInformation = true;
        $scope.model.tPetionActiveStatus.ApplicantInformation = true;
    }
 
}];
var rapApplicationInfoController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerApplicantInfoFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}