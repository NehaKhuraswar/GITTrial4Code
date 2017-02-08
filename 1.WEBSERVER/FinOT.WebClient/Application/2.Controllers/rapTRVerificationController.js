'use strict';
var rapTRVerificationController = ['$scope', '$modal', 'alertService', 'rapTRverificationFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Error = "";
    self.Hide = false;
    $scope.model.stepNo = 8;
    $anchorScroll();
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.Error= "Your pin has been sent to your email address";
            $anchorScroll();
        });
    }
    self.SubmitResponse = function () {

        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.CaseFileBy = self.custDetails.custID;
        rapFactory.SubmitTenantResponse(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            $scope.model.TRSubmissionStatus.Verification = true;
            $scope.model.bVerification = false;
            $scope.model.bConfirm = true;
        });
    }
}];