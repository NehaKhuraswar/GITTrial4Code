'use strict';
var rapreviewFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';
    
    var _GetTenantReviewInfo = function (custId) {
        blockUI.start();

        var url = _routePrefix + '/gettenantreview';
        if (!(custId == null || custId == undefined)) { url = url + '/' + custId; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.GetTenantReviewInfo = _GetTenantReviewInfo;

    return factory;
}];