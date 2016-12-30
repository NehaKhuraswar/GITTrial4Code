'use strict';
var rapOResponseReviewFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOResponseReview = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOResponseReview';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
   

    factory.GetOResponseReview = _getOResponseReview;
    return factory;
}];