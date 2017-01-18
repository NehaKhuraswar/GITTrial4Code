'use strict';
var rapTenantlDocumentFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _GetTenantDocuments = function (CustomerID, DocumentTitle) {
        blockUI.start();

        var url = _routePrefix + '/GetUploadedDocuments/' + CustomerID + '/' + DocumentTitle;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _SaveTenantDocuments = function (model,customerID) {
        blockUI.start();
        var url = _routePrefix + '/SaveTenantDocuments/' + customerID;

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetTenantDocuments = _GetTenantDocuments;
    factory.SaveTenantDocuments = _SaveTenantDocuments;

    return factory;
}];