'use strict';
var rapverificationFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
    
      var _SaveCaseInfo = function (model) {
          blockUI.start();

          var url = _routePrefix + '/savecaseinfo';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
    factory.SaveCaseInfo = _SaveCaseInfo;
    
    return factory;
}];