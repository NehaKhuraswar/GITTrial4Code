'use strict';
var rapAppealDocumentFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _GetAppealDocuments = function (CustomerID, DocumentTitle) {
        blockUI.start();

        var url = _routePrefix + '/GetUploadedDocuments/' + CustomerID + '/' + DocumentTitle;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _SaveAppeallDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveAppeallDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetAppealDocuments = _GetAppealDocuments;
    factory.SaveAppeallDocuments = _SaveAppeallDocuments;

    return factory;
}];