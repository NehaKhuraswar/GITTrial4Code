'use strict';
var rapTRreviewFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';
    
    var _GetTenantResponseReviewInfo = function (CaseNumber,custId) {
        blockUI.start();

        var url = _routePrefix + '/gettenantresponsereviewinfo/' + CaseNumber;
        if (!(custId == null || custId == undefined)) { url = url + '/' + custId; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.GetTenantResponseReviewInfo = _GetTenantResponseReviewInfo;

    return factory;
}];