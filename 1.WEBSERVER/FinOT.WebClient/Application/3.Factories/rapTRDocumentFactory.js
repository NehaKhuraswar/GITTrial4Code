'use strict';
var rapTRDocumentFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getTRAdditionalDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetTRAdditionalDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveTRAdditionalDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveTRAdditionalDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetTRAdditionalDocuments = _getTRAdditionalDocuments;
    factory.SaveTRAdditionalDocuments = _saveTRAdditionalDocuments;

    return factory;
}];