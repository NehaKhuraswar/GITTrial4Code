﻿'use strict';
var rapImpInfoController = ['$scope', '$modal', 'alertService', 'rapfilepetitionFactory', '$location', 'rapGlobalFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, $anchorScroll) {
    var self = this;
    $scope.model.stepNo = 2;
    $anchorScroll();
    self.Continue = function () {
        $scope.model.bImpInfo = false;
        $scope.model.bAppInfo = true;
        $scope.model.tPetionActiveStatus.ImportantInformation = true;
       
    }
}];
var rapImpInfoController_resolve = {
    model: ['$route', 'alertService', 'rapfilepetitionFactory', function ($route, alert, rapFactory) {
        ////return auth.fetchToken().then(function (response) {
        //return rapFactory.GetTenantPetetionFormInfo().then(function (response) {
        //  if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
    }]
}