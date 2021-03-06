﻿'use strict';
var raprepresentativeFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
    //Save or Update Third Party Info
    var _SaveOrUpdateThirdPartyInfo = function (model) {
        blockUI.start();

        return ajax.Post(model, _routePrefix + '/SaveOrUpdateThirdPartyInfo')
        .finally(function () {
            blockUI.stop();
        });
    }
    //Remove Third Party Information
    var _RemoveThirdPartyInfo = function (model) {
        blockUI.start();

        return ajax.Post(model, _routePrefix + '/RemoveThirdPartyInfo')
        .finally(function () {
            blockUI.stop();
        });
    }

   
    //Get Third Party Information based on Customer ID
    var _GetThirdPartyInfo = function (custID) {
        blockUI.start();

        return ajax.Get(_routePrefix + '/GetThirdPartyInfo/' + custID)
        .finally(function () {
            blockUI.stop();
        });
    }

    
   
    factory.SaveOrUpdateThirdPartyInfo = _SaveOrUpdateThirdPartyInfo;
    factory.GetThirdPartyInfo = _GetThirdPartyInfo;
    factory.RemoveThirdPartyInfo = _RemoveThirdPartyInfo;
    
    return factory;
}];