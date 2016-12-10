'use strict';
var rapOwnerRentalHistoryFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOwnerRentIncreaseAndPropertyInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOwnerRentIncreaseAndPropertyInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveOwnerRentIncreaseAndUpdatePropertyInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOwnerRentIncreaseAndUpdatePropertyInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOwnerRentIncreaseAndPropertyInfo = _getOwnerRentIncreaseAndPropertyInfo;
    factory.SaveOwnerRentIncreaseAndUpdatePropertyInfo = _saveOwnerRentIncreaseAndUpdatePropertyInfo;

    return factory;
}];