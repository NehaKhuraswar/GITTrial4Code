'use strict';
var rapSearchAccountFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
    
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

    var _GetAccountSearch = function () {
        blockUI.start();
        var url = _routePrefix + '/getaccountsearch/'

        return ajax.Post(model,url)
        .finally(function () {
            blockUI.stop();
        });
    }
    


    factory.GetAccountTypes = _GetAccountTypes;
    factory.GetEmptyAccountSearchModel = _GetEmptyAccountSearchModel;
    factory.GetAccountSearch = _GetAccountSearch;
    
    return factory;
}];