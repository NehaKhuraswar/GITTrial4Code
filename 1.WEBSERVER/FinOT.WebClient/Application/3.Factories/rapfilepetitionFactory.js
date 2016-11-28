'use strict';
var rapfilepetitionFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _GetCaseInfo = function (model, UserID) {
        blockUI.start();
        var url = _routePrefix + '/getcaseinfo/'+ UserID;

        return ajax.Post(model,url)
        .finally(function () {
            blockUI.stop();
        });
      }
    
     
    factory.GetCaseInfo = _GetCaseInfo;
    
    return factory;
}];