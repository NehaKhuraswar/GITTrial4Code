'use strict';
var rapOResponseRentalHistoryFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _GetOResponseRentIncreaseAndPropertyInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOResponseRentIncreaseAndPropertyInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _SaveOResponseRentIncreaseAndUpdatePropertyInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOResponseRentIncreaseAndUpdatePropertyInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOResponseRentIncreaseAndPropertyInfo = _GetOResponseRentIncreaseAndPropertyInfo;
    factory.SaveOResponseRentIncreaseAndUpdatePropertyInfo = _SaveOResponseRentIncreaseAndUpdatePropertyInfo;

    return factory;
}];