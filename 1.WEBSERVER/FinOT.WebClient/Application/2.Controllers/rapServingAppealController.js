'use strict';
var rapServingAppealController = ['$scope', '$q', '$modal', 'alertService', 'rapservingappealFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $q, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
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
            $anchorScroll();
            return;
         }
            self.StateList = response.data;
            $anchorScroll();
        });
     }
    //_GetStateList();
    self.OpposingParty;
    var _GetOpposingParty = function() {
    rapFactory.GetOpposingParty ().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.OpposingParty = response.data;
                $anchorScroll();
            });
    }


    var _GetAppealServe = function (appealID) {
        rapFactory.GetAppealServe(appealID, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            // self.caseinfo = response.data;
            self.caseinfo.TenantAppealInfo.serveAppeal = response.data.TenantAppealInfo.serveAppeal;
            self.serveAppeal = self.caseinfo.TenantAppealInfo.serveAppeal;
            self.serveAppeal.AppealID = appealID;
            $anchorScroll();
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
        self.Error = "";
        if (model.FirstName==null || model.FirstName == "")
        {
            self.Error = self.Error + 'First name is a required field \n';
        }
        if (model.LastName == null || model.LastName == "") {
            self.Error = self.Error + 'Last name is a required field \n';
        }
        if (model.AddressLine1 == null || model.AddressLine1 == "") {
            self.Error = self.Error + 'Address line 1 is a required field \n';
        }
        if (model.City == null || model.City == "") {
            self.Error = self.Error + 'City is a required field \n';
        }
        if (model.State == null) {
            self.Error = self.Error + 'State is a required field \n';
        }
        if (model.Zip == null) {
            self.Error = self.Error + 'Zip is a required field \n';
        }
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
            
            $anchorScroll();
        }
   }

   
    self.ResendPin = function () {
        masterFactory.ResendPin(self.custDetails).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.Error = "Your pin has been sent to your email address";
            $anchorScroll();
        });
    }
    
    self.ContinueToReview = function (serveAppeal) {
        var OppositePartyExists = false;
        angular.forEach(self.serveAppeal.OpposingParty, function (item) {
            if (item.IsDeleted == false) {
                OppositePartyExists = true;
            }
        });

        if (OppositePartyExists == false && self.OpposingParty.FirstName != "" && self.OpposingParty.LastName != ""
            && self.OpposingParty.AddressLine1 != ""
            && self.OpposingParty.City != ""
            && self.OpposingParty.State != null && self.OpposingParty.Zip != null) 
        {
            self.serveAppeal.OpposingParty.push(self.OpposingParty);
        }
        OppositePartyExists = false;
        angular.forEach(self.serveAppeal.OpposingParty, function (item) {
            if (item.IsDeleted == false) {
                OppositePartyExists = true;
            }
        });
        if (OppositePartyExists == false)
        {
            self.Error = 'Add at least one opposing party';
            $anchorScroll();
            return;
        }
        rapFactory.SaveTenantServingAppeal(self.caseinfo.TenantAppealInfo, self.custDetails.custID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                 $anchorScroll();
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