'use strict';
var rapOResponseExemptionFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOResponseExemption = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOResponseExemption';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveOResponseExemption = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOResponseExemption';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOResponseExemption = _getOResponseExemption;
    factory.SaveOResponseExemption = _saveOResponseExemption;

    return factory;
}];