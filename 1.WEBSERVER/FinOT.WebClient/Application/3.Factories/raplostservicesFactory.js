'use strict';
var raplostservicesFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
   
      var _SaveTenantLostServiceInfo = function (model) {
          blockUI.start();

          var url = _routePrefix + '/savetenantlostserviceinfo';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveTenantLostServiceInfo = _SaveTenantLostServiceInfo;
    
    return factory;
}];