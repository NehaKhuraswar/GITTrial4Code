'use strict';
var rapVerificationController = ['$scope', '$modal', 'alertService', 'rapverificationFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.model = $scope.model;
    $scope.model.stepNo = 9;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Error = "";
    self.Hide = false;
    $anchorScroll();
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.Error = "Pin is sent to your email";
            $anchorScroll();
        });
    }
    self.SubmitPetition = function () {
        if (self.caseinfo.TenantPetitionInfo.Verification.bAcknowledgePinName != true)
        {
            self.Error = "Please acknowledge the consent to conduct business";
            $anchorScroll();
                return;
                }

        if (self.caseinfo.TenantPetitionInfo.Verification.bDeclarePenalty != true) {
            self.Error = "Please declare that all the entered information true to your knowledge";
            $anchorScroll();
                return;
            }
        if (self.caseinfo.TenantPetitionInfo.Verification.bCaseMediation == true) {
             if(self.caseinfo.TenantPetitionInfo.Verification.bAcknowledgePinNameMediation != true) {
                 self.Error = "Please acknowledge the consent to conduct mediation";
                 $anchorScroll();
                return;
             }
        }
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapGlobalFactory.CaseDetails.CaseFileBy = self.custDetails.custID;
        rapFactory.SubmitTenantPetition(rapGlobalFactory.CaseDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
        }
        $scope.model.tPetionActiveStatus.Verification = true;
             $scope.model.bVerification = false;
             $scope.model.bConfirm = true;
             rapGlobalFactory.CaseDetails = response.data;
          //  $location.path("/publicdashboard");
        });
    }
}];
var rapVerificationController_resolve = {
    model: ['$route', 'alertService', 'rapverificationFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}