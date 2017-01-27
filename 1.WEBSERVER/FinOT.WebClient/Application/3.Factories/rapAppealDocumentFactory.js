'use strict';
var rapAppealDocumentFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';
    //Get Document based on Customer ID in Appeal Form
    var _GetAppealDocuments = function (CustomerID, DocumentTitle) {
        blockUI.start();

        var url = _routePrefix + '/GetUploadedDocuments/' + CustomerID + '/' + DocumentTitle;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    //Save Document and Customer ID in Appeal Form
    var _SaveAppealDocuments = function (model,custID) {
        blockUI.start();
        var url = _routePrefix + '/SaveAppealDocuments/' + custID;

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetAppealDocuments = _GetAppealDocuments;
    factory.SaveAppealDocuments = _SaveAppealDocuments;

    return factory;
}];