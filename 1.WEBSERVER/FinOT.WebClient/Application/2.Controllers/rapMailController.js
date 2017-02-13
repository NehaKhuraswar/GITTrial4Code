var rapMailController = ['$scope', 'alertService', '$location', 'rapMailFactory', 'rapGlobalFactory', 'masterdataFactory', 'rapnewcasestatusFactory', '$anchorScroll', function ($scope, alert, $location, rapFactory, rapGlobalFactory, masterFactory, rapnewcasestatusFactory, $anchorScroll) {
    var self = this;
    self.custDetails = rapGlobalFactory.CityUser;
    if (rapGlobalFactory.SelectedCase == null || rapGlobalFactory.SelectedCase == undefined) {
        $location.path("/staffdashboard");
    }
    self.c_id = rapGlobalFactory.CaseDetails.C_ID;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = null;
    self.ActivityList = [];
    self.Error = '';
    self.CaseClick = function () {
        $location.path("/selectedcase");
    }
    self.Home = function () {
        $location.path("/staffdashboard");
    }
    self.SelectedActivity = null;
    self.Tenant = null;
    self.Owner = null;
    self.ThirdParty = null;
    self.bTenant = false;
    self.bOwner = false;
    $anchorScroll();
    self.bThirdParty = false;
    if (self.caseinfo.PetitionCategoryID == 1) {
        if (self.caseinfo.TenantPetitionInfo != null) {
            if (self.caseinfo.TenantPetitionInfo.ApplicantUserInfo != null) {
                if (self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.FirstName != null && self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.LastName != null) {
                    self.Tenant = self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.FirstName + ' ' + self.caseinfo.TenantPetitionInfo.ApplicantUserInfo.LastName;
                }
            }
            if (self.caseinfo.TenantPetitionInfo.OwnerInfo != null) {
                if (self.caseinfo.TenantPetitionInfo.OwnerInfo.FirstName != null && self.caseinfo.TenantPetitionInfo.OwnerInfo.LastName != null) {
                    self.Owner = self.caseinfo.TenantPetitionInfo.OwnerInfo.FirstName + ' ' + self.caseinfo.TenantPetitionInfo.OwnerInfo.LastName;
                }
            }
            if (self.caseinfo.TenantPetitionInfo.ThirdPartyInfo != null) {
                if (self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.FirstName != null && self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.LastName != null) {
                    self.ThirdParty = self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.FirstName + ' ' + self.caseinfo.TenantPetitionInfo.ThirdPartyInfo.LastName;
                }
            }
        }
    }
    else {
        if (self.caseinfo.OwnerPetitionInfo != null) {
            if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo != null) {
                if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.FirstName != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.LastName != null) 

                    self.Owner = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.FirstName + ' ' + self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ApplicantUserInfo.LastName;
            }
            }
        if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser != null) {
                if (self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.FirstName != null && self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.LastName != null) {
                    self.ThirdParty = self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.FirstName + ' ' + self.caseinfo.OwnerPetitionInfo.ApplicantInfo.ThirdPartyUser.LastName;
                }
            }
            if (self.caseinfo.OwnerPetitionInfo.PropertyInfo != null && self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo != null) {
                for (i = 0; i < self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo.length; i++) {
                    if (i == 0) {
                        if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.FirstName != null && self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.LastName != null) {
                            self.Tenant = self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.FirstName + ' ' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.LastName;
                        }
                    }
                    if (self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.FirstName != null && self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.LastName != null) {
                        self.Tenant = self.Tenant + ',' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.FirstName + ' ' + self.caseinfo.OwnerPetitionInfo.PropertyInfo.TenantInfo[i].TenantUserInfo.LastName;
                    }
                }
            }

        }
    
    rapFactory.GetMail().then(function (response) {
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
    self.Delete = function (doc) {
        var index = self.model.Attachments.indexOf(doc);
        self.model.Attachments.splice(index, 1);     
    }
    self.Submit = function () {
        self.model.C_ID = self.c_id;
        self.model.CityUserID = self.custDetails.UserID;
        if (self.bTenant) {
            self.model.Recipient.push(self.Tenant);
        }
        if (self.bOwner) {
            self.model.Recipient.push(self.Owner);
        }
        if (self.bThirdParty) {
            self.model.Recipient.push(self.ThirdParty);
        }
        self.model.Activity = self.SelectedActivity.ActivityDesc;
        self.model.ActivityID = self.SelectedActivity.ActivityID;
        if (self.model.Recipient.length > 0) {
            rapFactory.SubmitMail(self.model).then(function (response) {
                if (!alert.checkForResponse(response)) {
                    self.Error = rapGlobalFactory.Error;
                    $anchorScroll();
                    return;
                }
                rapGlobalFactory.MailNotification = response.data;
                rapFactory.GetMailNotification(rapGlobalFactory.MailNotification.NotificationID).then(function (response) {
                    if (!alert.checkResponse(response)) { return; }
                    rapGlobalFactory.MailNotification = response.data;
                    rapGlobalFactory.FromSelectedCase = true;
                    rapGlobalFactory.Notification_CaseID = angular.copy(rapGlobalFactory.CaseDetails.CaseID);
                    rapGlobalFactory.CaseDetails = null;
                    $location.path("/usmailnotificationsent");
                });
            });
        }
    }

}];

var rapMailController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}