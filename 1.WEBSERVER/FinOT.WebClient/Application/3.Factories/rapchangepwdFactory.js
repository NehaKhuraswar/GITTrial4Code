'use strict';
var rapchangepwdFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
    var _ChangePassword = function (model) {
        blockUI.start();

        return ajax.Post(model, _routePrefix + '/changepwd')
        .finally(function () {
            blockUI.stop();
        });
    }
   
    factory.ChangePassword = _ChangePassword;
    return factory;
}];