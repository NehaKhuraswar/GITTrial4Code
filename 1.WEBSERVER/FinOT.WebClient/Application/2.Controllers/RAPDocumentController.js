'use strict';
var rapDocumentController = ['$scope', '$modal', 'alertService', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, $location, rapGlobalFactory) {
    var self = this;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    self.ContinueToReview = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $scope.model.bAddDocuments = false;
        $scope.model.bReview = true;
       
    }

    $scope.onFileSelect = function ($files) {
        if ($files && $files.length)
        {
            //var fileName = $files[0].name;
            var file = $files[0];

            //var file = new java.io.RandomAccessFile(file, "r");
            //var bArr = java.lang.reflect.Array.newInstance(java.lang.Byte.TYPE, file.length());
            //file.read(bArr)


           var reader = new FileReader();
            reader.readAsArrayBuffer(file);
            var arrayBuffer = null;
           reader.onload = function (e)
           {

               arrayBuffer = e.target.result;
              //   array = new Uint8Array(arrayBuffer),
              //   binaryString = String.fromCharCode.apply(null, array);

              //console.log(binaryString);

           }
          
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