'use strict';
var rapOResponseDecreasedHousingFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOResponseDecreasedHousing = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOResponseDecreasedHousing';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveOResponseDecreasedHousing = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOResponseAdditionalDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOResponseDecreasedHousing = _getOResponseDecreasedHousing;
    factory.SaveOResponseDecreasedHousing = _saveOResponseDecreasedHousing;

    return factory;
}];