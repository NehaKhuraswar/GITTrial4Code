'use strict';
var rapresendpinFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
    var _ResendPin = function (model) {
        blockUI.start();

        return ajax.Post(model, _routePrefix + '/resendpin')
        .finally(function () {
            blockUI.stop();
        });
    }
   
    factory.ResendPin = _ResendPin;
    return factory;
}];