var rapOResponseVerificationController = ['$scope', '$modal', 'alertService', '$location', 'rapOResponseVerificationFactory', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, $location, rapFactory, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 10;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.Error = '';
    self.Hide = false;
    $anchorScroll();
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.Error = "Your pin has been sent to your email address";
            $anchorScroll();
        });
    }
    self.Continue = function () {
        if (self.caseinfo.OwnerResponseInfo.Verification.bAcknowledgePinName != true) {
            self.Error = "Please acknowledge the consent to conduct business";
            $anchorScroll();
            return;
        }

        if (self.caseinfo.OwnerResponseInfo.Verification.bDeclarePenalty != true) {
            self.Error = "Please declare that all the entered information true to your knowledge";
            $anchorScroll();
            return;
        }
        if (self.caseinfo.OwnerResponseInfo.Verification.bCaseMediation == true) {
            if (self.caseinfo.OwnerResponseInfo.Verification.bAcknowledgePinNameMediation != true) {
                self.Error = "Please acknowledge the consent to conduct mediation";
                $anchorScroll();
                return;
            }
        }
         
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SubmitOwnerResponse(self.caseinfo).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            rapGlobalFactory.CaseDetails = response.data;
            $scope.model.oresponseVerification = false;
            $scope.model.DisableAllCurrent();
            $scope.model.oResponseActiveStatus.Verification = true;
            $scope.model.oresponseConfirmation = true;
        });
     
    }
}];

var rapOResponseVerificationController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}