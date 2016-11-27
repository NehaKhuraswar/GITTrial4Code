'use strict';
var rapverificationFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
    
      var _SubmitTenantPetition = function (model) {
          blockUI.start();

          var url = _routePrefix + '/submittenantpetition';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
    factory.SaveCaseInfo = _SaveCaseInfo;
    
    return factory;
}];