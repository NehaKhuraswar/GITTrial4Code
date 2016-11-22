'use strict';
var rapServingAppealController = ['$scope', '$modal', 'alertService', 'rapservingappealFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.AddAnotherOpposingParty = function (model) {
        rapFactory.AddAnotherOpposingParty(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }            
        });
        // rapGlobalFactory.CaseDetails = self.caseinfo;

    }
    
    self.ContinueToReview = function () {
        rapFactory.SaveTenantServingAppeal(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
        });
        $location.path("/reviewappeal");
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