﻿'use strict';
var rapVerificationController = ['$scope', '$modal', 'alertService', 'rapverificationFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = $scope.model;
    self.custDetails = rapGlobalFactory.CustomerDetails;
    self.caseinfo = rapGlobalFactory.CaseDetails;
        self.SubmitPetition = function (model) {
     
            rapGlobalFactory.CaseDetails = self.caseinfo;
            rapFactory.SubmitTenantPetition(rapGlobalFactory.CaseDetails).then(function (response) {
                if (!alert.checkResponse(response)) {
                    return;
                }
                $modalInstance.close(response.data);
            });
        }
}];
var rapVerificationController_resolve = {
    model: ['$route', 'alertService', 'rapverificationFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}