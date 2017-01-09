var rapCustomEmailController = ['$scope', 'alertService', '$location', 'rapCustomEmailFactory', 'rapGlobalFactory', 'masterdataFactory', 'rapnewcasestatusFactory', function ($scope, alert, $location, rapFactory, rapGlobalFactory, masterFactory, rapnewcasestatusFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CityUser;
    self.c_id = rapGlobalFactory.SelectedCase.C_ID;
    self.model = null;
    self.ActivityList = [];
      
  
    rapFactory.GetCustomEmail(self.c_id).then(function (response) {
        if (!alert.checkResponse(response)) { return; }
        self.model = response.data;
    });

    rapnewcasestatusFactory.GetActivity().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.ActivityList = response.data;
        });
    
    $scope.onFileSelected = function ($files, docTitle) {
        if ($files && $files.length) {
            for (var i = 0; i < $files.length; i++) {
                var file = $files[i];
                var filename = file.name;
                var mimetype = file.type;
                var filesize = ((file.size / 1024) / 1024).toFixed(4);
                //if (filesize < 25) {
                if (filesize < masterFactory.FileSize)
                    var index = filename.lastIndexOf(".");
                var ext = filename.substring(index, filename.length).toUpperCase();
                //if (ext == '.PDF' || ext == '.DOC' || ext == '.DOCX' || ext == '.XLS' || ext == '.JPEG' || ext == '.TIFF' || ext == '.PNG') {
                if (masterFactory.FileExtensons.indexOf(ext) > -1) {
                    var document = {}; // angular.copy(self.caseinfo.Document);
                    document.DocTitle = docTitle;
                    document.DocName = filename;
                    document.MimeType = mimetype;
                    document.EmployeeID = self.custDetails.EmployeeID;
                    document.C_ID = self.c_id;
                    document.isUploaded = false;
                    document.IsPetitonFiled = true;
                    var reader = new FileReader();
                    reader.readAsArrayBuffer(file);
                    reader.onload = function (e) {
                        var arrayBuffer = e.target.result;
                        var base64String = btoa(String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
                        document.Base64Content = base64String;
        
                    }                  
                    self.model.Message.Attachments.push(document);
                    
                }
            }

        }
    }

    self.Submit = function () {
        self.model.C_ID = self.c_id;
        self.model.EmployeeID = self.custDetails.EmployeeID;
        rapFactory.SubmitCustomEmail(self.model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.Documents = response.data;
        });   
    }

}];

var rapCustomEmailController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}