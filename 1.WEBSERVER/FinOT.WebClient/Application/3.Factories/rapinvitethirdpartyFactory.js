'use strict';
var rapinvitethirdpartyFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
  

    var _SearchInviteThirdPartyUser = function (model) {
        blockUI.start();

        var url = _routePrefix + '/searchinvite'
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _Authorize = function (custid, model) {
        blockUI.start();

        var url = _routePrefix + '/authorize'
        if (!(custid == null || custid == undefined)) {
            url += '?custid=' + custid;
        }
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.SearchInviteThirdPartyUser = _SearchInviteThirdPartyUser;
    factory.Authorize = _Authorize;
    return factory;
}];