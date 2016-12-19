'use strict';
var rapreviewappealFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
    
      var _SubmitAppeal = function (model) {
          blockUI.start();

          var url = _routePrefix + '/submitappeal';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SubmitAppeal = _SubmitAppeal;
    
    return factory;
}];