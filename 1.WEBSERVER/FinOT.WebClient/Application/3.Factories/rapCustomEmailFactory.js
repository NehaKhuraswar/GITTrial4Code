'use strict';
var rapCustomEmailFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/dashboard';

    var _getCustomEmail = function (cid) {
        blockUI.start();
        var url = _routePrefix + '/GetCustomEmail';
        if (!(cid == null || cid == undefined)) { url = url + '/' + cid; }
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _submitCustomEmail = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SubmitCustomEmail';
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetCustomEmail = _getCustomEmail;
    factory.SubmitCustomEmail = _submitCustomEmail;

    return factory;
}];