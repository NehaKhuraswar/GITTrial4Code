'use strict';
var rapTRVerificationController = ['$scope', '$modal', 'alertService', 'rapTRverificationFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            alert.Error("Pin is sent to your email");
        });
    }
    self.SubmitPetition = function () {

        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.CaseFileBy = self.custDetails.custID;
        rapFactory.SubmitTenantPetition(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            $scope.model.tPetionActiveStatus.Verification = true;
            $location.path("/publicdashboard");
        });
    }
}];