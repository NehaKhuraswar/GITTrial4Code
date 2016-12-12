var rapOwnerDocumentsController = ['$scope', '$modal', 'alertService','$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
   
    self.Continue = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $scope.model.ownerAdditionalDocuments = false;
        $scope.model.ownerReview = true;
    }    
}];

var rapOwnerDocumentsController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {
   
    }]
}