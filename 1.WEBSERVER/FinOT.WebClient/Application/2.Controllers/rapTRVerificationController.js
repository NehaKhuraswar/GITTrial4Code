'use strict';
var rapTRVerificationController = ['$scope', '$modal', 'alertService', 'rapTRverificationFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Error = "";
    self.Hide = false;
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            alert.Error("Pin is sent to your email");
        });
    }
    self.SubmitResponse = function () {

        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.CaseFileBy = self.custDetails.custID;
        rapFactory.SubmitTenantResponse(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            $scope.model.TRSubmissionStatus.Verification = true;
            $scope.model.bVerification = false;
            $scope.model.bConfirm = true;
        });
    }
}];