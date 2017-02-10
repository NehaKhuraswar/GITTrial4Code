var rapCustomEmailController = ['$scope', 'alertService', '$location', 'rapCustomEmailFactory', 'rapGlobalFactory', 'masterdataFactory', 'rapnewcasestatusFactory', '$anchorScroll', function ($scope, alert, $location, rapFactory, rapGlobalFactory, masterFactory, rapnewcasestatusFactory, $anchorScroll) {
    var self = this;
    if (rapGlobalFactory.SelectedCase == null || rapGlobalFactory.SelectedCase == undefined) {
        $location.path("/staffdashboard");
    }
    self.custDetails = rapGlobalFactory.CityUser;
    self.c_id = rapGlobalFactory.SelectedCase.C_ID;
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.model = null;
    self.ActivityList = [];
    $anchorScroll();
    self.CaseClick = function () {
        $location.path("/selectedcase");
    }
    self.Home = function () {
        $location.path("/staffdashboard");
    }
    self.SelectedActivity = null;
    self.TenantEmail = null;
    self.OwnerEmail = null;
    self.ThirdPartyEmail = null;
    self.bTenant = false;
    self.bOwner = false;
    self.bThirdParty = false;
    if (self.caseinfo.PetitionCategoryID == 1) {
        if (self.caseinfo.TenantPetitionInfo != null) {
            if (self.caseinfo.TenantPetitionInfo.ApplicantUserInfo != null && self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.Email != null) {
                self.TenantEmail = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.Email;
            }
            if (self.caseinfo.TenantPetitionInfo.OwnerInfo != null && self.caseinfo.TenantPetitionInfo.OwnerInfo.Email != null) {
                self.OwnerEmail = self.caseinfo.TenantPetitionInfo.OwnerInfo.Email;
            }
            if (self.caseinfo.TenantPetitionInfo.ThirdPartyInfo != null && self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.Email != null) {
                self.ThirdPartyEmail = self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.Email;
            }
        }
    }
  
else {
        if (self.caseinfo.OwnerPetitionInfo != null) {
        if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo != null) {
            if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.Email != null) 

                self.OwnerEmail = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.Email;
        }
    }
    if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser != null) {
        if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.Email != null) {
            self.ThirdPartyEmail = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.Email;
        }
    }
    if (self.caseinfo.OwnerPetitionInfo.PropertyInfo != null && self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo != null) {
        for (i = 0; i < self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length; i++) {
            if (i == 0) {
                if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email != null) {
                    self.TenantEmail = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email;
                }
            }
            if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email != null) {
                self.TenantEmail = self.TenantEmail + ',' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.Email;
            }
        }
    }
}
        
    rapFactory.GetCustomEmail(self.c_id).then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
        self.model = response.data;
    });

    rapnewcasestatusFactory.GetActivity().then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            $anchorScroll();
            return;
        }
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
                    document.CityUserID = self.custDetails.UserID;
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

     self.Delete = function (doc) {
         var index = self.model.Message.Attachments.indexOf(doc);
         self.model.Message.Attachments.splice(index, 1);
    }
    self.BackToCase = function () {
        rapGlobalFactory.Notification = null;
        $location.path("/selectedcase");
    }
    self.Submit = function () {
        self.model.C_ID = self.c_id;
        self.model.CityUserID = self.custDetails.UserID;
        self.model.SentBy = self.custDetails.FirstName + ' ' + self.custDetails.LastName;       
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
        self.model.Message.Subject = self.SelectedActivity.ActivityDesc;
        self.model.ActivityID = self.SelectedActivity.ActivityID;
  
        rapFactory.SubmitCustomEmail(self.model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }           
            rapGlobalFactory.Notification = response.data;
            rapGlobalFactory.FromSelectedCase = true;
            rapGlobalFactory.Notification_CaseID = rapGlobalFactory.SelectedCase.CaseID;
                $location.path("/emailnotificationsent");            
        });   
    }

}];

var rapCustomEmailController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}