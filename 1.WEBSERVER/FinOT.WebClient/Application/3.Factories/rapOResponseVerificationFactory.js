'use strict';
var rapOResponseVerificationFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';


    var _submitOwnerResponse = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SubmitOwnerResponse';
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.SubmitOwnerResponse = _submitOwnerResponse;
    return factory;
}];