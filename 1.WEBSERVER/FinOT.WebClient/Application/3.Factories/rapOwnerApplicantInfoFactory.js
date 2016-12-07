'use strict';
var rapOwnerApplicantInfoFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getApplicantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOwnerApplicantInfo';

            return ajax.Post(model, url)
            .finally(function () {
                blockUI.stop();
            });
    }
    var _saveApplicantInfo = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOwnerApplicantInfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    //var _SaveApplicationInfo = function (model) {
    //    blockUI.start();

    //    var url = _routePrefix + '/saveapplicationinfo';

    //    return ajax.Post(model, url)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}
    //var _GetCaseInfo = function () {
    //    blockUI.start();

    //    var url = _routePrefix + '/getcaseinfo';

    //    return ajax.Get(url)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}


    factory.GetApplicationInfo = _getApplicantInfo;
    factory.SaveApplicationInfo = _saveApplicantInfo;

    return factory;
}];