'use strict';
var rapMailFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/dashboard';

    var _getMail = function (cid) {
        blockUI.start();
        var url = _routePrefix + '/GetMail';
        var model = null;
        return ajax.Get(url, model)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetMailNotification = function (NotificationID) {
        blockUI.start();
        var url = _routePrefix + '/GetMailNotification';
        if (!(NotificationID == null || NotificationID == undefined)) { url = url + '/' + NotificationID; }
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
        
    var _submitMail = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SubmitMail';
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetMail = _getMail;
    factory.SubmitMail = _submitMail;
    factory.GetMailNotification = _GetMailNotification;

    return factory;
}];