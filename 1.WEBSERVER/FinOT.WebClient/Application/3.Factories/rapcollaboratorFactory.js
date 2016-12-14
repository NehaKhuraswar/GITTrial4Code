'use strict';
var rapcollaboratorFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
  

    var _SearchInviteCollaborator = function (model) {
        blockUI.start();

        var url = _routePrefix + '/searchinvite'
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _Authorize = function (model) {
        blockUI.start();

        var url = _routePrefix + '/authorizecollaborator';
        //if (!(custid == null || custid == undefined)) {
        //    url += '?custid=' + custid;
        //}
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetAuthorizedUsers = function (custid) {
        blockUI.start();

        var url = _routePrefix + '/authorizedusers'
        if (!(custid == null || custid == undefined)) {
            url += '?custid=' + custid;
        }
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _Invite = function ( model) {
        blockUI.start();

        var url = _routePrefix + '/invite'
        
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _RemoveThirdParty = function (custid, model) {
        blockUI.start();

        var url = _routePrefix + '/removethirdparty'
        if (!(custid == null || custid == undefined)) {
            url += '?custid=' + custid;
        }

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.SearchInviteCollaborator = _SearchInviteCollaborator;
    factory.Authorize = _Authorize;
    factory.Invite = _Invite;
    factory.GetAuthorizedUsers = _GetAuthorizedUsers;
    factory.RemoveThirdParty = _RemoveThirdParty;
    return factory;
}];