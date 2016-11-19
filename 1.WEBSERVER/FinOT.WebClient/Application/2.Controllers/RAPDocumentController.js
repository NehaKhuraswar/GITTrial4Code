'use strict';
var rapDocumentController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, $location, rapGlobalFactory) {
    var self = this;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    self.ContinueToReview = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $location.path("/review");
    }

    $scope.onFileSelect = function ($files) {
        if ($files && $files.length)
        {
            var fileName = $files[0].name;
          //  var fileContent = $files[0]);

           // var file = $scope.createNewDocument();
           // file.FileName = newFileName;
            //self.editmodel.push(file);
          //  $scope.newFiles.push($files[0].name);
           // $scope.newFilesContent.push($files[0]);
        }
    }
    
}];
var rapDocumentController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory)
    {
    }]
}