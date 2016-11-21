'use strict';
var rapappellantsinfoFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
      var _SaveTenantAppealInfo = function (model) {
          blockUI.start();

          var url = _routePrefix + '/savetenantappealinfo';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveTenantAppealInfo = _SaveTenantAppealInfo;
    
    return factory;
}];