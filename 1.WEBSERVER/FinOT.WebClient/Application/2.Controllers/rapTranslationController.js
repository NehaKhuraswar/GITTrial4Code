'use strict';
var rapTranslationController = ['$scope', '$modal', 'alertService', 'rapTranslationFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = [];
    self.custDetails = rapGlobalFactory.CustomerDetails;    
    self.Cases = null;
    self.Hide = false;
    self.Error = "";
   
   
    var _GetTranslationServiceInfo = function () {
        rapFactory.GetTranslationServiceInfo(self.custDetails.User.UserID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            self.TranslationService = response.data;
        });
    }
    _GetTranslationServiceInfo();

    //var _UpdateTranslationServiceInfo = function (cases) {
    //    return masterFactory.UpdateThirdPartyAccessPrivilege(cases, self.custDetails.custID).then(function (response) {
    //        if (!alert.checkForResponse(response)) {
    //            self.Error = rapGlobalFactory.Error;
    //            return;
    //        }
    //        self.Cases = response.data;
    //        $location.path("/YourRepresentative");
    //    });
    //}
    self.SaveTranslationServiceInfo = function (model) {        
        return rapFactory.SaveTranslationServiceInfo(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                return;
            }
            $location.path("/publicdashboard");
        });        
    }

    self.Cancel = function()
    {
        $location.path("/publicdashboard");
    }
}];
var rapTranslationController_resolve = {
    model: ['$route', 'alertService', 'rapTranslationFactory', 'rapGlobalFactory', function ($route, alert, rapFactory, rapGlobalFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //       if (!alert.checkResponse(response)) { return; }
        //       rapGlobalFactory.CustomerDetails= response.data;
            
        //});
    }]
}