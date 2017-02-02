'use strict';
var rapdashboardController = ['$scope', '$modal', 'alertService', 'rapdashboardFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    rapGlobalFactory.CaseDetails = null;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = rapGlobalFactory.CustomerDetails;
    //if (self.model == null || self.model == undefined)
    //{
    //    var custID = rapGlobalFactory.GetCustomer();
    //    masterFactory.GetCustomer(custID).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            return;
    //        }
    //        self.model = response.data;
    //        rapGlobalFactory.CustomerDetails = response.data;
    //        __GetCasesForCustomer();
    //    });
    //}
  //  self.model = rapGlobalFactory.GetCustomer();

    self.btoggle = false;
    self.ThirdPartyRepresentative = function () {
        $location.path("/YourRepresentative");
    }
    self.FilePetition = function (type) {
        rapGlobalFactory.PetitionCategoryID = type;
        $location.path("/filePetition");
    }
    self.NewCaseStatus = function () {
        $location.path("/newCaseStatus");
    }
    self.AccountSearch = function () {
        $location.path("/accountSearch");
    }
    self.CreateCityUserAccount = function () {
        $location.path("/createCityUserAccount");
    }
    self.ChangePassword = function () {
        $location.path("/changepassword");
    }
    self.ResendPin = function () {
        $location.path("/resendpin");
    }
    self.Collaborator = function () {
        $location.path("/collaborator");
    }
    self.OwnerResponse = function () {
        $location.path("/ownerresponse");
    }
    self.toggle = function () {
        if (self.btoggle == false) {
                self.btoggle = true;
                }
        else {
            self.btoggle = false;
                    }

    }
    self.ChangeAccountInformation = function () {
        rapGlobalFactory.IsEdit = true;
        $location.path("/editcustomerinformation");
    }
    self.FileAppeal = function (C_ID) {
       
            $location.path("/fileappeal");
        
        
    }
    self.FileTenantResponse = function () {
            $location.path("/filetenantresponse");
    }
    

    var __GetCasesForCustomer = function () {
        if (self.model == null)
        {
            return;
        }
        return masterFactory.GetCasesForCustomer(self.model.custID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.Cases = response.data;
        });
    }
    __GetCasesForCustomer();

    self.GetCaseActivityStatus = function (model) {
        //self.caseinfo.CaseID = 
        rapFactory.GetCaseActivityStatus(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.ActivityStatus = response.data;
           
        });
    }

    self.CalculateRemainingDays = function (date, statusID) {
        
        var d = new Date(date);
        var day =  d.getDate();

        var start = Math.floor(d.getTime() / (3600 * 24 * 1000)); //days as integer from..
        var end = Math.floor(new Date().getTime() / (3600 * 24 * 1000)); //days as integer from..
        var daysDiff = end - start; // exact dates start = Math.floor( date1.getTime() / (3600*24*1000)); //days as integer from..

        if (statusID == 3)
        {
            return 20-daysDiff;
        }
    }

    self.ViewPage = function(activity,caseinfo)
    {
        if (activity.Activity.ActivityID == 1) {
            rapFactory.GetPetitionViewInfo(caseinfo.C_ID).then(function (response) {
                if (!alert.checkResponse(response)) {
                    return;
                }
                self.caseinfo = response.data;
                rapGlobalFactory.CaseDetails = self.caseinfo;
                $location.path("/ViewPetition");
            });
        }
        else if (activity.Activity.ActivityID == 26) {
            rapFactory.GetAppealInfoForView(caseinfo.C_ID).then(function (response) {
                if (!alert.checkResponse(response)) {
                    return;
                }
                self.caseinfo = response.data;
                rapGlobalFactory.CaseDetails = self.caseinfo;
                $location.path("/ViewAppeal");
            });
        }
        else if (activity.Activity.ActivityID == 27) {
            rapFactory.GetTenantResponseViewInfo(caseinfo.C_ID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
                }
                self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
            $location.path("/ViewTenantResponse");
            });
        }
        else if (activity.Activity.ActivityID == 35) {
            rapFactory.GetOResponseViewByCaseID(caseinfo.C_ID).then(function (response) {
                if (!alert.checkResponse(response)) {
                    return;
                }
                self.caseinfo = response.data;
                rapGlobalFactory.CaseDetails = self.caseinfo;
                rapGlobalFactory.FromSelectedCase = false;
                $location.path("/ViewOwnerResponse");
            });
        }
        else if (activity.Status.StatusID == 6) {
            if (activity.NotificationType == 1) {
                rapFactory.GetCustomEmailNotification(caseinfo.C_ID, activity.Activity.ActivityID, activity.NotificationID).then(function (response) {
                    if (!alert.checkForResponse(response)) {
                        return;
                    }
                        rapGlobalFactory.Notification = response.data;
                        $location.path("/emailnotificationsent");
                        });
                        }
                        else if(activity.NotificationType == 2) {
                    rapFactory.GetMailNotification(activity.NotificationID).then(function (response) {
                        if(!alert.checkForResponse(response)) {
                            return;
                    }
                    rapGlobalFactory.MailNotification = response.data;
                    $location.path("/usmailnotificationsent");
                });
            }
        }
       
    }

    self.OpenSelectedCase = function (caseinfo)
    {
        rapGlobalFactory.SelectedCase = caseinfo;
        $location.path("/selectedcase");
    }
    self.UploadDocumentation = function (caseinfo) {
        rapGlobalFactory.SelectedCase = caseinfo;
        $location.path("/additionaldocuments");
    }
    

}];
var rapdashboardController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //    $scope.model = response.data;
        //    //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}