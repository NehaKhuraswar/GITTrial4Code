'use strict';
var rapServingAppealController = ['$scope', '$q', '$modal', 'alertService', 'rapservingappealFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $q, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Calender = masterFactory.Calender;
    self.StateList = [];
    self.serveAppeal;
    var _GetStateList = function () {
    masterFactory.GetStateList().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.StateList = response.data;
        });
     }
    //_GetStateList();
    self.OpposingParty;
    var _GetOpposingParty = function() {
    rapFactory.GetOpposingParty ().then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
                }
            self.OpposingParty = response.data;
            });
    }


    var _GetAppealServe = function (appealID) {
        rapFactory.GetAppealServe(appealID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.serveAppeal = response.data;
            self.serveAppeal.AppealID = appealID;
        });
    }
   // _GetAppealServe(self.caseinfo.TenantAppealInfo.AppealID);
   // _GetOpposingParty();
    $q.all([_GetStateList(), _GetOpposingParty(), _GetAppealServe(self.caseinfo.TenantAppealInfo.AppealID)]).then(function () {
                    
    })
    //self.OpposingParty = angular.copy(self.caseinfo.TenantAppealInfo.AppealOpposingPartyInfo);

    self.AddAnotherOpposingParty = function (model) {
        var _opposingParty= angular.copy(model);
        self.serveAppeal.OpposingParty.Push(_opposingParty);
        model.FirstName = "";
        model.LastName = "";
        model.AddressLine1 = "";
        model.AddressLine2 = "";
        model.City = "";
        model.State = null;
        model.Zip = 0;

        }

   

    
    self.ContinueToReview = function (serveAppeal) {
        //if (serveAppeal.OpposingParty.length == 0)
        //{
        //   serveAppeal.OpposingParty.push(self.OpposingParty);
        //}
        //rapFactory.SaveTenantServingAppeal(serveAppeal).then(function (response) {
        //    if (!alert.checkResponse(response)) {
        //        return;
        //    }
            $scope.model.bServingAppeal = false;
            $scope.model.bReview = true;
       // });
       // $location.path("/reviewappeal");
    }
   
    //self.AddAnotherOpposingParty = function (opposingParty) {     
    //    self.caseinfo.TenantAppealInfo.AppealOpposingPartyInfo.push(opposingParty);
    //    var a;
    //    //rapFactory.AddAnotherOpposingParty(opposingParty).then(function (response) {
    //    //    if (!alert.checkResponse(response)) {
    //    //        return;
    //    //    }
    //    //    $modalInstance.close(response.data);
    //    //});
    //}
}];
var rapServingAppealController_resolve = {
    model: ['$route', 'alertService', 'rapservingappealFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}