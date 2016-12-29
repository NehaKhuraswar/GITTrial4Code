'use strict';
var rapOResponseDocumentFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOResponseAdditionalDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOResponseAdditionalDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveOResponseAdditionalDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOResponseAdditionalDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOResponseAdditionalDocuments = _getOResponseAdditionalDocuments;
    factory.SaveOResponseAdditionalDocuments = _saveOResponseAdditionalDocuments;

    return factory;
}];