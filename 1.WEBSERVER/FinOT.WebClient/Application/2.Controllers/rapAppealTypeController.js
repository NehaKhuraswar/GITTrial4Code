'use strict';
var rapAppealTypeController = ['$scope', '$modal', 'alertService', 'rapappealtypeFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 1;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.Error = "";
    //self.caseinfo = rapGlobalFactory.CaseDetails;
    var _getPetitionCategory = function () {
        rapFactory.GetPetitionCategory().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
           // self.bPetitionType = true;
        });
    }
    _getPetitionCategory();

    //var _GetCaseInfo = function (caseID) {
    //    rapFactory.GetCaseInfo(caseID).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        //self.model = response.data;
    //        self.caseinfo = response.data;
    //        rapGlobalFactory.CaseDetails = self.caseinfo;
    //        _getPetitionCategory();
    //       // self.bPetitionType = true;
    //    });
    //}
    //_GetCaseInfo(self.caseinfo.CaseID);

    
    self.ContinueToImportantInformation = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $scope.model.bAppealType = false;
        $scope.model.bImpInfoAppeal = true;
        $scope.model.AppealSubmissionStatus.AppealType = true;
        
    }
}];
var rapAppealTypeController_resolve = {
    model: ['$route', 'alertService', 'rapappealtypeFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}