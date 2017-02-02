var rapOwnerVerificationController = ['$scope', '$modal', 'alertService', '$location', 'rapOwnerVerificationFactory', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, $location, rapFactory, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.CaseFileBy = self.custDetails.custID;
    self.Error = '';
    $scope.model.stepNo = 9;
    self.Hide = false;
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.Error="Pin is sent to your email";
        });
    }
    self.Continue = function () {
        if (self.caseinfo.OwnerPetitionInfo.Verification.bAcknowledgePinName != true) {
            self.Error = "Please acknowledge the consent to conduct business";
            return;
        }

        if (self.caseinfo.OwnerPetitionInfo.Verification.bDeclarePenalty != true) {
            self.Error = "Please declare that all the entered information true to your knowledge";
            return;
        }
        if (self.caseinfo.OwnerPetitionInfo.Verification.bCaseMediation == true) {
            if (self.caseinfo.OwnerPetitionInfo.Verification.bAcknowledgePinNameMediation != true) {
                self.Error = "Please acknowledge the consent to conduct mediation";
                return;
            }
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SubmitOwnerPetition(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            $scope.model.ownerVerification = false;
            $scope.model.ownerConfirmation = true;
            $scope.model.oPetionActiveStatus.Verification = true;
            $scope.model.DisableAllCurrent();
        });
       
    }
}];

var rapOwnerVerificationController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}