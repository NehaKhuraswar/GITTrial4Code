'use strict';
var rapOResponseRentalPropertyFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOwnerPropertyAndTenantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOResponsePropertyAndTenantInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveOwnerPropertyAndTenantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOResponsePropertyAndTenantInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOwnerPropertyAndTenantInfo = _getOwnerPropertyAndTenantInfo;
    factory.SaveOwnerPropertyAndTenantInfo = _saveOwnerPropertyAndTenantInfo;

    return factory;
}];