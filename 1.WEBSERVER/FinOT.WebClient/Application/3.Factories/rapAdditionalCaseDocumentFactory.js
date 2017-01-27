'use strict';
var rapAdditionalCaseDocumentFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/dashboard';
    //Get the Case Document
    var _getCaseDocuments = function (cid) {
        blockUI.start();
        var url = _routePrefix + '/GetCaseDocuments';
        if (!(cid == null || cid == undefined)) { url = url + '/' + cid; }
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    //Save the Case Document
    var _saveCaseDocuments = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SaveCaseDocuments';
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetCaseDocuments = _getCaseDocuments;
    factory.SaveCaseDocuments = _saveCaseDocuments;

    return factory;
}];