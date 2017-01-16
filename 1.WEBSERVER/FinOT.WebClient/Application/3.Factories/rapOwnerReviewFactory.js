'use strict';
var rapOwnerReviewFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOwnerReview = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOwnerReview';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _getOwnerReviewByCaseID = function (cid) {
        blockUI.start();
        var url = _routePrefix + '/GetOwnerReviewByCaseID';
        if (!(cid == null || cid == undefined)) { url = url + '/' + cid; }
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    
    var _saveOwnerReviewPageSubmission = function (custId) {
        blockUI.start();
        var url = _routePrefix + '/SaveOwnerReviewPageSubmission';
        if (!(custId == null || custId == undefined)) { url = url + '/' + custId; }
        return ajax.Post(null, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.GetOwnerReview = _getOwnerReview;
    factory.SaveOwnerReviewPageSubmission = _saveOwnerReviewPageSubmission;
    factory.GetOwnerReviewByCaseID = _getOwnerReviewByCaseID;
    return factory;
}];