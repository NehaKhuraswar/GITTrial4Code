var rapMailController = ['$scope', 'alertService', '$location', 'rapMailFactory', 'rapGlobalFactory', 'masterdataFactory', 'rapnewcasestatusFactory', function ($scope, alert, $location, rapFactory, rapGlobalFactory, masterFactory, rapnewcasestatusFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CityUser;
    if (rapGlobalFactory.SelectedCase == null || rapGlobalFactory.SelectedCase == undefined) {
        $location.path("/staffdashboard");
    }
    self.c_id = rapGlobalFactory.SelectedCase.C_ID;
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.model = null;
    self.ActivityList = [];

    self.Tenant = null;
    self.Owner = null;
    self.ThirdParty = null;
    self.bTenant = false;
    self.bOwner = false;
    self.bThirdParty = false;
    if (self.caseinfo.PetitionCategoryID == 1) {
        if (self.caseinfo.TenantPetitionInfo != null) {
            if (self.caseinfo.TenantPetitionInfo.ApplicantUserInfo != null) {
                self.Tenant = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.FirstName + ' ' + self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.LastName;
            }
            if (self.caseinfo.TenantPetitionInfo.OwnerInfo != null) {
                self.Owner = self.caseinfo.TenantPetitionInfo.OwnerInfo.FirstName + ' ' + self.caseinfo.TenantPetitionInfo.OwnerInfo.LastName;
            }
            if (self.caseinfo.TenantPetitionInfo.ThirdPartyInfo != null) {
                self.ThirdParty = self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.FirstName + ' ' + self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.LastName;
            }
        }
    }
    else {
        if (self.caseinfo.OwnerPetitionInfo != null) {
            if (self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo.ApplicantUserInfo != null) {
                self.Owner = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.FirstName + ' ' + self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.LastName;
            }
            if (self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantUserInfo.ThirdPartyUser != null) {

                self.ThirdParty = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.FirstName + ' ' + self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.LastName;
            }
            if (self.caseinfo.OwnerPetitionInfo.PropertyInfo != null && self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo != null) {
                for (i = 0; i < self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length; i++) {
                    if (i == 0) {
                        self.Tenant = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].FirstName + ' ' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].LastName;
                    }
                    self.TenantEmail = self.TenantEmail + ',' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].Email;
                }
            }

        }
    }


    rapFactory.GetMail().then(function (response) {
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
                            document.Base64Content = base64.substring(base64.indexOf('base64') + 7);
                        }
                    }
                    self.model.Attachments.push(document);

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
        if (self.bTenant) {
            self.model.Recipient.push(self.Tenant);
        }
        if (self.bOwner) {
            self.model.Recipient.push(self.Owner);
        }
        if (self.bThirdParty) {
            self.model.Recipient.push(self.ThirdParty);
        }

        rapFactory.SubmitMail(self.model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            
        });
    }

}];

var rapMailController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}