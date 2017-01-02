'use strict';
var rapOResponseReviewController = ['$scope', '$modal', 'alertService', 'rapOResponseReviewFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.caseinfo.CustomerID = self.custDetails.custID;
    self.caseinfo.OwnerResponseInfo.PropertyInfo.CustomerID = self.custDetails.custID;

    self.Calender = masterFactory.Calender;


    rapFactory.GetOResponseReview(self.caseinfo).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        rapGlobalFactory.CaseDetails = response.data;
        self.caseinfo = response.data;
        
    });

   
    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        rapFactory.SaveOResponseReviewPageSubmission(self.custDetails.custID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }            
            MoveNext();
        });
    }
    function MoveNext() {
        $scope.model.oresponseReview = false;
        $scope.model.oresponseVerification = true;
        $scope.model.DisableAllCurrent();
        $scope.model.oResponseCurrentStatus.Verification = true;
        $scope.model.oResponseActiveStatus.Review = true;
    }
}];
var rapOResponseReviewController_resolve = {
    model: ['$route', 'alertService', 'rapOwnerRentalHistoryFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}