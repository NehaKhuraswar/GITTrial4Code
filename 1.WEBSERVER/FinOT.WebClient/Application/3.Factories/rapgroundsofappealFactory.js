'use strict';
var rapgroundsofappealFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _SaveAppealGroundInfo = function (model) {
          blockUI.start();

          var url = _routePrefix + '/saveappealgroundinfo';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveAppealGroundInfo = _SaveAppealGroundInfo;
    return factory;
}];