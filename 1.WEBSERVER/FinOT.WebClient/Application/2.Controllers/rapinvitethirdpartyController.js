'use strict';
var rapinvitethirdpartyController = ['$scope', '$modal', 'alertService', 'rapinvitethirdpartyFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = [];
    self.showEmailNotFound = false;
    self.showEmailFound = false;
    self.IsConsent = false;
    self.SearchInviteThirdPartyUser = function (email) {
        
        rapFactory.SearchInviteThirdPartyUser(email).then(function (response) {
            if (!alert.checkResponse(response)) {
                showEmailNotFound = true;
                return;
            }
            self.showEmailFound = true;
            self.model = response.data;

        });
    }
    self.Authorize = function (model) {
        if (self.IsConsent == true) {
            rapFactory.Authorize(model).then(function (response) {


            });
        }
        else {
            alert.Error("Please consent to Authorize the third party")
        }
    }

}];
var rapinvitethirdpartyController_resolve = {
    model: ['$route', 'alertService', 'raploginFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        return rapFactory.GetCustomer(null).then(function (response) {
            //   if (!alert.checkResponse(response)) { return; }
            //    return response.data;
            //});
        });
    }]
}



