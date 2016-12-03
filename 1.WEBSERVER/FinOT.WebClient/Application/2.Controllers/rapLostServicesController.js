'use strict';
var rapLostServicesController = ['$scope', '$modal', 'alertService', 'raplostservicesFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    var range = 10 / 2;
    var currentYear = new Date().getFullYear();
    self.years = [];
    for (var i = range; i > 0 ; i--) {

        self.years.push(currentYear - i);
    }
    for (var i = 0; i < range + 1; i++) {
        self.years.push(currentYear  + i);
    }
    

    self.onFileSelect = function ($files) {
        if ($files && $files.length) {
            var fileName = $files[0].name;
            var file = $files[0];
            var reader = new FileReader();
            reader.fileName = file.name;
            self.caseinfo.TenantPetitionInfo.Document.DocName = file.name;
            reader.readAsArrayBuffer(file);
           
            reader.onload = function (e)
            {
                var arrayBuffer = e.target.result;
                var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
                self.caseinfo.TenantPetitionInfo.Document.Base64Content = base64String;
                // self.caseinfo.TenantPetitionInfo.FileContet = new Uint8Array(arrayBuffer);
               
            }
           
        }
    }

    self.ContinueToDocument = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveTenantLostServiceInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.bLostServices = false;
            $scope.model.bAddDocuments = true;
        });
       // $location.path("/document");
         
    }
    //self.ContinueToVerification = function () {
    //    $location.path("/verification");
    //}
    ////self.SubmitPetition = function () {
    ////  //  $location.path("/verification");
    ////}
    //self.SubmitPetition = function (model) {
     

    //    rapFactory.SaveCaseInfo(model).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        $modalInstance.close(response.data);
    //    });
    //}
}];
var rapLostServicesController_resolve = {
    model: ['$route', 'alertService', 'raplostservicesFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}