'use strict';
var rapappealtypeFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _GetCaseInfo = function (model) {
        blockUI.start();

        var url = _routePrefix + '/getcaseinfo';

        return ajax.Post(model,url)
        .finally(function () {
            blockUI.stop();
        });
      }
    
     
    factory.GetCaseInfo = _GetCaseInfo;
    
    return factory;
}];