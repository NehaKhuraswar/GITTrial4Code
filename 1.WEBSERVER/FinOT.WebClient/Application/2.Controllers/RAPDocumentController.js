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
    $scope.onAdditionalFileSelect = function ($files) {
        if ($files && $files.length) {
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                popupateDocument(file, 'TP_AdditionalDocuments');
            }
        }
    }


    function popupateDocument(file, dicTitle) {
        var filename = file.name;
        var mimetype = file.type;
        var filesize = ((file.size / 1024) / 1024).toFixed(4);
        //if (filesize < 25) {
        if (filesize < masterFactory.FileSize) {
            var index = filename.lastIndexOf(".");
            var ext = filename.substring(index, filename.length).toUpperCase();
            //if (ext == '.PDF' || ext == '.DOC' || ext == '.DOCX' || ext == '.XLS' || ext == '.JPEG' || ext == '.TIFF' || ext == '.PNG') {
            if (masterFactory.FileExtensons.indexOf(ext) > -1) {
                var document = self.caseinfo.Document;
                document.DocTitle = dicTitle;
                document.DocName = filename;
                document.MimeType = mimetype;
                document.CustomerID = self.custDetails.custID;
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var base64 = e.target.result;
                    if (base64 != null) {
                        document.Base64Content = base64.substring(base64.indexOf('base64') + 7)
                    }
                }
                self.caseinfo.Documents.push(document);
            }
        }

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