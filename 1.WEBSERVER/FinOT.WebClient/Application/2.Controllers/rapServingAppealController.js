'use strict';
var rapServingAppealController = ['$scope', '$q', '$modal', 'alertService', 'rapservingappealFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $q, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.Calender = masterFactory.Calender;
    self.StateList = [];
    self.serveAppeal;
    self.Error = "";
    $scope.model.stepNo = 6;
    var _GetStateList = function () {
    masterFactory.GetStateList().then(function (response) {
        if (!alert.checkForResponse(response)) {
            self.Error = rapGlobalFactory.Error;
            return;
         }
            self.StateList = response.data;
        });
     }
    //_GetStateList();
    self.OpposingParty;
    var _GetOpposingParty = function() {
    rapFactory.GetOpposingParty ().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.OpposingParty = response.data;
            });
    }


    var _GetAppealServe = function (appealID) {
        rapFactory.GetAppealServe(appealID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            // self.caseinfo = response.data;
            self.caseinfo.TenantAppealInfo.serveAppeal = response.data.TenantAppealInfo.serveAppeal;
            self.serveAppeal = self.caseinfo.TenantAppealInfo.serveAppeal;
            self.serveAppeal.AppealID = appealID;
        });
    }
   // _GetAppealServe(self.caseinfo.TenantAppealInfo.AppealID);
   // _GetOpposingParty();
    $q.all([_GetStateList(), _GetOpposingParty(), _GetAppealServe(self.caseinfo.TenantAppealInfo.AppealID)]).then(function () {
                    
    })
    //$q.all([ _GetAppealServe(self.caseinfo.TenantAppealInfo.AppealID)]).then(function () {
                    
    //})
    //self.OpposingParty = angular.copy(self.caseinfo.TenantAppealInfo.AppealOpposingPartyInfo);

    self.AddAnotherOpposingParty = function (model) {
        if (model.FirstName != "" && model.LastName != "" && model.AddressLine1 != ""
            && model.City != ""
            && model.State != null && model.Zip != null) {
            var _opposingParty = angular.copy(model);
            self.serveAppeal.OpposingParty.push(_opposingParty);

            model.FirstName = "";
            model.LastName = "";
            model.AddressLine1 = "";
            model.AddressLine2 = "";
            model.City = "";
            model.State = null;
            model.Zip = null;
        }
        else
        {
            self.Error = 'Opposing parties First Name, Address 1, City , State , Zip fields required';
        }
        }

   
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            alert.Error("Pin is sent to your email");
        });
    }
    
    self.ContinueToReview = function (serveAppeal) {
        if (self.serveAppeal.OpposingParty.length == 0 && self.OpposingParty.FirstName!=null)
        {
            self.serveAppeal.OpposingParty.push(self.OpposingParty);
        }
        rapFactory.SaveTenantServingAppeal(self.caseinfo.TenantAppealInfo, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            $scope.model.bServingAppeal = false;
            $scope.model.bReview = true;
            $scope.model.AppealSubmissionStatus.ServingAppeal = true;
        });
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