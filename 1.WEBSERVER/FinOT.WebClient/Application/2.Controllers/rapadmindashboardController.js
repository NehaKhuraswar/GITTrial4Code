'use strict';
var rapadmindashboardController = ['$scope', '$modal', 'alertService', 'rapadmindashboardFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = rapGlobalFactory.CustomerDetails;
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
    self.CreatePublicUserAccount = function () {
        $location.path("/register");
    }
    self.EditPublicUserAccount = function () {
        $location.path("/editpublicuser");
    }
    self.FileAppeal = function (model) {
        //self.caseinfo.CaseID = 
        rapFactory.GetCaseInfoWithModel(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }

            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
        });
        $location.path("/fileappeal");
    }

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

    var _GetCaseInfo = function () {

        rapFactory.GetCaseInfo().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }

            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
        });
    }
    // _getrent();
    if (self.caseinfo == null) {
        _GetCaseInfo();
    }

}];
var rapadmindashboardController_resolve = {
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