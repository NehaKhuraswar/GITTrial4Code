'use strict';
var rapLostServicesController = ['$scope', '$modal', 'alertService', 'raplostservicesFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.LostServices;
    self.Problems;

    var range = 10 / 2;
    var currentYear = new Date().getFullYear();
    self.years = [];
    for (var i = range; i > 0 ; i--) {

        self.years.push(currentYear - i);
    }
    for (var i = 0; i < range + 1; i++) {
        self.years.push(currentYear  + i);
    }
    self.Calender = masterFactory.Calender;
    var _GetEmptyLostServicesInfo = function () {
        rapFactory.GetEmptyLostServicesInfo().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.LostServices = response.data;
         });
     }
    _GetEmptyLostServicesInfo();

    var _GetEmptyProblemsInfo = function() {
        rapFactory.GetEmptyProblemsInfo().then(function (response) {
            if(!alert.checkResponse(response)) {
                return;
            }
            self.Problems = response.data;
        });
     }
    _GetEmptyProblemsInfo();


     var _GetTenantLostServiceInfo = function (petitionId) {
        rapFactory.GetTenantLostServiceInfo(petitionId).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
        }
        self.caseinfo.TenantPetitionInfo.LostServicesPage = response.data;
        });
    }
    _GetTenantLostServiceInfo(self.caseinfo.TenantPetitionInfo.PetitionID);

    
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

    self.AddAnotherLostServices = function (lostservice) {
        var _lostservice = angular.copy(lostservice);
        self.caseinfo.TenantPetitionInfo.LostServicesPage.LostServices.push(_lostservice);
        lostservice.LossBeganDate = null;
        lostservice.ReducedServiceDescription = null;
        lostservice.EstimatedLoss = 0;
    }

    self.AddAnotherProblem = function (problem) {
        var _problem = angular.copy(problem);
        self.caseinfo.TenantPetitionInfo.LostServicesPage.Problems.push(_problem);
        problem.ProblemBeganDate = null;
        problem.ProblemDescription = null;
        problem.EstimatedLoss = 0;
    }
    self.ContinueToDocument = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.TenantPetitionInfo.LostServicesPage.PetitionID = self.caseinfo.TenantPetitionInfo.PetitionID;
        rapFactory.SaveTenantLostServiceInfo(rapGlobalFactory.CaseDetails.TenantPetitionInfo.LostServicesPage).then(function (response) {
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