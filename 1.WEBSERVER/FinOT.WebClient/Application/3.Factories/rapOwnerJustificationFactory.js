'use strict';
var rapOwnerJustificationFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getRentIncreaseReasonInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetRentIncreaseReasonInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveRentIncreaseReasonInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveRentIncreaseReasonInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetRentIncreaseReasonInfo = _getRentIncreaseReasonInfo;
    factory.SaveRentIncreaseReasonInfo = _saveRentIncreaseReasonInfo;

    return factory;
}];