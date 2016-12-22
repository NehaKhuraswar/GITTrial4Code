'use strict';
var rapadmindashboardFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
  

    var _GetCaseInfoWithModel = function (model) {
        blockUI.start();

        var url = 'api/applicationprocessing' + '/getcaseinfo';

         return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetCaseActivityStatus = function(model) {
        blockUI.start();

        var url = 'api/dashboard' + '/getcaseactivitystatus';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
         });
    }

    var _GetAccountTypes = function () {
        blockUI.start();
        var url = _routePrefix + '/getaccounttypes/'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetEmptyAccountSearchModel = function () {
        blockUI.start();
        var url = _routePrefix + '/getemptyaccountsearchmodel/'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

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