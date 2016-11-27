'use strict';
var raprentalhistoryFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _SaveTenantRentalIncrementInfo = function (model) {
          blockUI.start();

          var url = _routePrefix + '/saverentalincrementinfo';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveTenantRentalIncrementInfo = _SaveTenantRentalIncrementInfo;
    return factory;
}];