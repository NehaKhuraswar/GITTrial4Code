'use strict';
var rapOResponseApplicantInfoFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getApplicantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOResponseApplicantInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveApplicantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOResponseApplicantInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetApplicationInfo = _getApplicantInfo;
    factory.SaveApplicationInfo = _saveApplicantInfo;

    return factory;
}];