'use strict';
var rapTranslationFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
    var _SaveTranslationServiceInfo = function (model) {
        blockUI.start();

        return ajax.Post(model, _routePrefix + '/SaveTranslationServiceInfo')
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetTranslationServiceInfo = function (custID) {
        blockUI.start();

        return ajax.Get(_routePrefix + '/GetTranslationServiceInfo/' + custID)
        .finally(function () {
            blockUI.stop();
        });
    }

    
   
    factory.SaveTranslationServiceInfo = _SaveTranslationServiceInfo;
    factory.GetTranslationServiceInfo = _GetTranslationServiceInfo;
    return factory;
}];