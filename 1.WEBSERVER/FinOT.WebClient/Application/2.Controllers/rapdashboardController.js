'use strict';
var rapdashboardController = ['$scope', '$modal', 'alertService', 'rapdashboardFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = rapGlobalFactory.CustomerDetails;
    self.btoggle = false;
    self.ThirdPartyRepresentative = function () {
        $location.path("/YourRepresentative");
    }
    self.FilePetition = function () {
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
        return masterFactory.GetCasesForCustomer(self.model.custID).then(function (response) {
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