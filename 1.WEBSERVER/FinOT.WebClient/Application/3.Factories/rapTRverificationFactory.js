'use strict';
var rapTRverificationFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
    
      var _SubmitTenantResponse = function (model) {
          blockUI.start();

          var url = _routePrefix + '/submittenantresponse';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SubmitTenantResponse = _SubmitTenantResponse;
    
    return factory;
}];