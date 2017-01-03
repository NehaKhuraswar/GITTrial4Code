var rapOwnerReviewController = ['$scope', '$modal', 'alertService', '$location', 'rapOwnerReviewFactory', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, $location, rapFactory, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    rapFactory.GetOwnerReview(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
    });

    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOwnerReviewPageSubmission(self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            $scope.model.ownerReview = false;
            $scope.model.ownerVerification = true;
            $scope.model.DisableAllCurrent();
            $scope.model.oPetionCurrentStatus.Verification = true;
            $scope.model.oPetionActiveStatus.Review = true;
        });
    }
  
}];

var rapOwnerReviewController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}