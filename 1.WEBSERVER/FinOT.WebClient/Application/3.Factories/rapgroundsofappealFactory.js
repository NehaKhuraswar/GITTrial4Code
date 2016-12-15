'use strict';
var rapgroundsofappealFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';
    
    var _GetAppealGroundInfo = function (CaseNumber, appealFiledBy) {
        blockUI.start();

        var url = _routePrefix + '/getappealgroundinfo/' + CaseNumber + '/' + appealFiledBy;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _SaveAppealGroundInfo = function (model) {
        blockUI.start();

        var url = _routePrefix + '/saveappealgroundinfo';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.SaveAppealGroundInfo = _SaveAppealGroundInfo;
    factory.GetAppealGroundInfo = _GetAppealGroundInfo;
    return factory;
}];