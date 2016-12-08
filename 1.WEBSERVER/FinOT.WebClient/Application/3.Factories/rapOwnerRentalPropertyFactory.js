'use strict';
var rapOwnerRentalPropertyFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOwnerPropertyAndTenantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOwnerPropertyAndTenantInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveOwnerPropertyAndTenantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOwnerPropertyAndTenantInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOwnerPropertyAndTenantInfo = _getOwnerPropertyAndTenantInfo;
    factory.SaveOwnerPropertyAndTenantInfo = _saveOwnerPropertyAndTenantInfo;

    return factory;
}];