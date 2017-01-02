'use strict';
var rapVerificationController = ['$scope', '$modal', 'alertService', 'rapverificationFactory', '$location', 'rapGlobalFactory','masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    //   self.model = $scope.model;
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

    self.pin1 = null;
    self.pin2 = null;
    self.SubmitPetition = function () {
        if (self.caseinfo.OwnerResponseInfo.CustomerIdentityKey == self.pin1 && self.caseinfo.OwnerResponseInfo.CustomerIdentityKey == self.pin2) {
            rapGlobalFactory.CaseDetails = self.caseinfo;
            rapGlobalFactory.CaseDetails.CaseFileBy = self.custDetails.custID;
            rapFactory.SubmitTenantPetition(rapGlobalFactory.CaseDetails).then(function (response) {
                if (!alert.checkResponse(response)) {
                    return;
                }
                $scope.model.tPetionActiveStatus.Verification = true;
                $scope.model.bVerification = false;
                $scope.model.bConfirm = true;
                //  $location.path("/publicdashboard");
            });
        }
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