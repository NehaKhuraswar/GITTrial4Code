var rapOResponseVerificationController = ['$scope', '$modal', 'alertService', '$location', 'rapOResponseVerificationFactory', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, $location, rapFactory, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;

    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            alert.Error("Pin is sent to your email");
        });
    }
    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SubmitOwnerResponse(self.caseinfo).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            rapGlobalFactory.CaseDetails = response.data;
        });
        $scope.model.oPetionActiveStatus.Verification = true;
    }
}];

var rapOResponseVerificationController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}