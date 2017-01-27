'use strict';
var rapadmindashboardFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
  
    //Get Case Information
    var _GetCaseInfoWithModel = function (model) {
        blockUI.start();

        var url = 'api/applicationprocessing' + '/getcaseinfo';

         return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    //Get Case Activity Status
    var _GetCaseActivityStatus = function(model) {
        blockUI.start();

        var url = 'api/dashboard' + '/getcaseactivitystatus';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
         });
    }
    //Get Case Account Types
    var _GetAccountTypes = function () {
        blockUI.start();
        var url = _routePrefix + '/getaccounttypes/'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    //Get Empty Account Search Model
    var _GetEmptyAccountSearchModel = function () {
        blockUI.start();
        var url = _routePrefix + '/getemptyaccountsearchmodel/'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    //Get Account Search
    var _GetAccountSearch = function (model) {
        blockUI.start();
        var url = _routePrefix + '/getaccountsearch/'

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetCaseInfoWithModel = _GetCaseInfoWithModel;
    factory.GetCaseActivityStatus = _GetCaseActivityStatus;
    factory.GetAccountTypes = _GetAccountTypes;
    factory.GetEmptyAccountSearchModel = _GetEmptyAccountSearchModel;
    factory.GetAccountSearch = _GetAccountSearch;


    return factory;
}];