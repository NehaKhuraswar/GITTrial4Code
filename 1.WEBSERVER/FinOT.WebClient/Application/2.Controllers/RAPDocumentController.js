'use strict';
var rapDocumentController = ['$scope', '$modal', 'alertService', 'ajaxService', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, ajaxService, $location, rapGlobalFactory) {
    var self = this;

    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;

    self.ContinueToReview = function () {
        rapGlobalFactory.CaseDetails = self.caseinfo;
        $scope.model.bAddDocuments = false;
        $scope.model.bReview = true;
        $scope.model.tPetionActiveStatus.AdditionalDocumentation = true;
       
    }

    //$scope.onFileSelect = function ($files) {
    //    if ($files && $files.length)
    //    {
    //        //var fileName = $files[0].name;
    //        var file = $files[0];
    //        self.caseinfo.TenantPetitionInfo.File = file;
    //        var data = new FormData();
    //        data.append(file);
    //        blockUI.start();

    //        var _routePrefix = 'api/applicationprocessing';
    //        var url = _routePrefix + '/savecaseinfo';

    //        return ajax.Post(data, url)
    //        .finally(function () {
    //            blockUI.stop();
    //        });
    //    }
    //}

            //var file = new java.io.RandomAccessFile(file, "r");
            //var bArr = java.lang.reflect.Array.newInstance(java.lang.Byte.TYPE, file.length());
            //file.read(bArr)


           //var reader = new FileReader();
           // reader.readAsArrayBuffer(file);
           // var arrayBuffer = null;
           //reader.onload = function (e)
           //{

               //arrayBuffer = e.target.result;
              //   array = new Uint8Array(arrayBuffer),
              //   binaryString = String.fromCharCode.apply(null, array);

              //console.log(binaryString);

          // }
          
           // var file = $scope.createNewDocument();
           // file.FileName = newFileName;
            //self.editmodel.push(file);
          //  $scope.newFiles.push($files[0].name);
           // $scope.newFilesContent.push($files[0]);
        
    
    
}];
var rapDocumentController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory)
    {
    }]
}