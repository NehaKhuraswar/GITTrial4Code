'use strict';
var rapcustFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/cust';

    var _GetOTRequest = function (reqid, fy) {
        blockUI.start();

        var url = _routePrefix + '/get';
        if (!(reqid == null || reqid == undefined)) { url += '/' + reqid; }
        if (!(fy == null || fy == undefined)) { url += '/' + fy; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetNotes = function (reqid) {
        blockUI.start();

        var url = _routePrefix + '/notes/get'
        if (!(reqid == null || reqid == undefined)) { url = url + '/' + reqid; }

        return ajax.Get(url).finally(function () {
            blockUI.stop();
        });
    }
    
    var _SaveOTRequest = function (reqid, model) {
        blockUI.start();

        var url = _routePrefix + '/save';
        if (!(reqid == null || reqid == undefined))
        {
            url += '?reqid=' + reqid;
        }

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _SaveNotes = function (reqid, model) {
        blockUI.start();
        return ajax.Post(model, _routePrefix + '/notes/save?reqid=' + reqid)
        .finally(function () {
            blockUI.stop();
        });
    }

    var appendQuotes = function (value) {
        return '"' + value + '"'
    }

    factory.GetOTRequest = _GetOTRequest;
    factory.SaveOTRequest = _SaveOTRequest;
    factory.GetNotes = _GetNotes;
    factory.SaveNotes = _SaveNotes;
    
    return factory;
}];