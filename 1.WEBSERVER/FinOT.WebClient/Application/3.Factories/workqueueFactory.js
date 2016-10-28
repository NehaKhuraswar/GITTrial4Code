'use strict';
var workqueueFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/workqueue';

    var _GetWorkQueue = function () {
        blockUI.start();
        var url = _routePrefix + '/get';
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _Filter = function (model) {
        blockUI.start();
        if (model.Export) {
            return ajax.PostResponse(model, _routePrefix + '/filter')
            .finally(function () {
                blockUI.stop();
            });
        }
        else {
            return ajax.Post(model, _routePrefix + '/filter')
            .finally(function () {
                blockUI.stop();
            });
        }
    }

    var appendQuotes = function (value) {
        return '"' + value + '"'
    }

    factory.GetWorkQueue = _GetWorkQueue;
    factory.Filter = _Filter;
    return factory;
}];