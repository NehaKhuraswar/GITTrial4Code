'use strict';
var searchFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/search';

    var _SearchRC = function (model) {
        return _Search(model, _routePrefix + '/rc')
    }

    var _SearchRequest = function (model) {
        return _Search(model, _routePrefix + '/request')
    }

    var _Search = function (model, url) {
        blockUI.start();
        if (model.Export) {
            return ajax.PostResponse(model, url)
            .finally(function () {
                blockUI.stop();
            });
        }
        else {
            return ajax.Post(model, url)
            .finally(function () {
                blockUI.stop();
            });
        }
    }

    var appendQuotes = function (value) {
        return '"' + value + '"'
    }

    factory.SearchRC = _SearchRC;
    factory.SearchRequest = _SearchRequest;
    return factory;
}];