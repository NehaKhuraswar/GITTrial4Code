'use strict';
var rapOwnerApplicantInfoFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

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


    //factory.GetCaseInfo = _GetCaseInfo;
    //factory.SaveApplicationInfo = _SaveApplicationInfo;

    return factory;
}];