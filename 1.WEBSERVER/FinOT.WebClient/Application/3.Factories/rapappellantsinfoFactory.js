'use strict';
var rapappellantsinfoFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
      var _SaveTenantAppealInfo = function (custID) {
          blockUI.start();

          var url = _routePrefix + '/savetenantappealinfo/' +custID;

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveTenantAppealInfo = _SaveTenantAppealInfo;
    
    return factory;
}];