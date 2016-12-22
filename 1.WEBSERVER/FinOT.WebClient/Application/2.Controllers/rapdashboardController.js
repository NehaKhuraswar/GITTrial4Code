'use strict';
var rapdashboardController = ['$scope', '$modal', 'alertService', 'rapdashboardFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = rapGlobalFactory.CustomerDetails;
    self.btoggle = false;
    self.InviteThirdPartyUser = function () {
        $location.path("/invitethirdparty");
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

           // self.caseinfo = response.data;
            //rapGlobalFactory.CaseDetails = self.caseinfo;
        });
       // $location.path("/fileappeal");
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