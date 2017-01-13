var rapCustomEmailController = ['$scope', 'alertService', '$location', 'rapCustomEmailFactory', 'rapGlobalFactory', 'masterdataFactory', 'rapnewcasestatusFactory', function ($scope, alert, $location, rapFactory, rapGlobalFactory, masterFactory, rapnewcasestatusFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CityUser;
    self.c_id = rapGlobalFactory.SelectedCase.C_ID;
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.model = null;
    self.ActivityList = [];
  
    self.TenantEmail = null;
    self.OwnerEmail = null;
    self.ThirdPartyEmail = null;
    self.bTenant = false;
    self.bOwner = false;
    self.bThirdParty = false;
    if (self.caseinfo.PetitionCategoryID == 1) {
        if (self.caseinfo.TenantPetitionInfo != null) {
            if (self.caseinfo.TenantPetitionInfo.ApplicantUserInfo != null) {
                self.TenantEmail = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.Email;
            }
            if (self.caseinfo.TenantPetitionInfo.OwnerInfo != null) {
                self.OwnerEmail = self.caseinfo.TenantPetitionInfo.OwnerInfo.Email;
            }
            if (self.caseinfo.TenantPetitionInfo.ThirdPartyInfo != null) {
                self.ThirdPartyEmail = self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.Email;
            }
        }
    }
    else {
        if (self.caseinfo.OwnerPetitionInfo != null) {
            if (self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo.ApplicantUserInfo != null) {
                self.OwnerEmail = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.Email;
            }
            if (self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo.ThirdPartyUser != null) {

                self.ThirdPartyEmail = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.Email;
            }
            if (self.caseinfo.OwnerPetitionInfo.PropertyInfo != null && self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo != null) {
                for (i = 0; i < self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length; i++) {
                    if (i == 0) {
                        self.TenantEmail = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].Email
                    }
                    self.TenantEmail = self.TenantEmail + ',' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].Email;
                }
            }

        }
    }

        

    
  
  
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
                    reader.readAsDataURL(file);
                    reader.onload = function (e) {
                        var base64 = e.target.result;
                        if (base64 != null) {
                            document.Base64Content = base64.substring(base64.indexOf('base64') + 7)
                        }
                    }
                    self.model.Message.Attachments.push(document);
                    
                }
            }

        }
    }

    self.BackToCase = function () {
        rapGlobalFactory.Notification = null;
        $location.path("/selectedcase");
    }
    self.Submit = function () {
        self.model.C_ID = self.c_id;
        self.model.EmployeeID = self.custDetails.EmployeeID;
        if (self.bTenant)
        {
            self.model.Message.RecipientAddress.push(self.TenantEmail);
        }
        if (self.bOwner)
        {
            self.model.Message.RecipientAddress.push(self.OwnerEmail);
        }
        if (self.bThirdParty)
        {
            self.model.Message.RecipientAddress.push(self.ThirdPartyEmail);
        }

        rapFactory.SubmitCustomEmail(self.model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            if (response.data) {
                rapGlobalFactory.Notification = self.model;              
                $location.path("/emailnotificationsent");
            }
        });   
    }

}];

var rapCustomEmailController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}