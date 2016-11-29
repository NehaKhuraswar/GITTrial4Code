'use strict';
var rapLostServicesController = ['$scope', '$modal', 'alertService', 'raplostservicesFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    //var _getrent = function () {
    //    return rapFactory.GetRent().then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.rent = response.data;
    //    });
    //}

   // var _GetCaseInfo = function (model) {

   //     rapFactory.GetCaseInfo().then(function (response) {
   //         if (!alert.checkResponse(response)) {
   //             return;
   //         }
           
   //         self.caseinfo = response.data;           

   //     });
   // }
   //// _getrent();
   // _GetCaseInfo();

    //self.Continue = function () {
    //    $location.path("/applicationinfo");
    //}
    //self.ContinueToGroundsforPetition = function () {
    //    $location.path("/groundsforpetition");
    //}
    //self.ContinueToRentalHistory = function () {
    //    $location.path("/rentalhistory");
    //}
    //self.ContinueToLostServices = function () {
    //    var a = self.selectedObj;
    //    $location.path("/lostservices");
    //}

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