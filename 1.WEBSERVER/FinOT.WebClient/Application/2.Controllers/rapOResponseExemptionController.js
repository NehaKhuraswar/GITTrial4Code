'use strict';
var rapOResponseExemptionController = ['$scope', '$modal', 'alertService', 'rapOResponseExemptionFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 7;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerResponseInfo.PropertyInfo.CustomerID = self.custDetails.custID;  

    self.Calender = masterFactory.Calender;
    self.Hide = false;
    self.Error = '';

    rapFactory.GetOResponseExemption(self.caseinfo).then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        $anchorScroll();
    });

    self.Continue = function () {   
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOResponseExemption(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            MoveNext();
        });   
    }

    function MoveNext() {
        $scope.model.oresponseException = false;
        $scope.model.oresponseDocument = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oResponseCurrentStatus.AdditionalDocumentation = true;
        $scope.model.oResponseActiveStatus.Exeption = true;
    }
}];
var rapOResponseExemptionController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalHistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}