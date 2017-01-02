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
   
    var _saveOResponseReviewPageSubmission = function (custId) {
        blockUI.start();
        var url = _routePrefix + '/SaveOResponseReviewPageSubmission';
        if (!(custId == null || custId == undefined)) { url = url + '/' + custId; }
        return ajax.Post(null, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.GetOResponseReview = _getOResponseReview;
    factory.SaveOResponseReviewPageSubmission = _saveOResponseReviewPageSubmission;
    return factory;
}];