'use strict';
var rapOwnerVerificationFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

 
    var _submitOwnerPetition = function (model) {
        blockUI.start();
        var url = _routePrefix + '/SubmitOwnerPetition';
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }  
    factory.SubmitOwnerPetition = _submitOwnerPetition;
    return factory;
}];