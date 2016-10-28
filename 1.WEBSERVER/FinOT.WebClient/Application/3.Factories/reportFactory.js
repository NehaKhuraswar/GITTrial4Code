'use strict';
var reportFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/application';

    var _GetSource = function (reportid, model) {
        blockUI.start();

        var url = _routePrefix + '/reportsource?ReportID=' + reportid
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetSource = _GetSource;

    return factory;
}];