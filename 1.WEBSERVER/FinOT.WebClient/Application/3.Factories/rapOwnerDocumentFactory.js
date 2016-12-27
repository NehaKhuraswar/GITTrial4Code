'use strict';
var rapOwnerDocumentFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    var _getOwnerAdditionalDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/GetOwnerAdditionalDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _saveOwnerAdditionalDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveOwnerAdditionalDocuments';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetOwnerAdditionalDocuments = _getOwnerAdditionalDocuments;
    factory.SaveOwnerAdditionalDocuments = _saveOwnerAdditionalDocuments;

    return factory;
}];