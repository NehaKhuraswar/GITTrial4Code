'use strict';
var rapreviewFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';
    
    //var _GetTenantReviewInfo = function (petitionID) {
    //    blockUI.start();

    //    var url = _routePrefix + '/gettenantreview';
    //    if (!(petitionID == null || petitionID == undefined)) { url = url + '/' + petitionID; }

    //    return ajax.Get(url)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}
    //factory.GetReviewInfo = _GetReviewInfo;

    return factory;
}];