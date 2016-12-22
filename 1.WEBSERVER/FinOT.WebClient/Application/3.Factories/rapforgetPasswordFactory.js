'use strict';
var rapforgetPasswordFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
    var _ForgetPwd = function (model) {
        blockUI.start();

        return ajax.Post(model,_routePrefix + '/forgetpwd')
        .finally(function () {
            blockUI.stop();
        });
    }
   
    factory.ForgetPwd = _ForgetPwd;
    return factory;
}];