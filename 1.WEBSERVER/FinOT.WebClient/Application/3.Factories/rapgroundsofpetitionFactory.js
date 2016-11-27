'use strict';
var rapgroundsofpetitionFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
      var _SavePetitionGroundInfo = function (model) {
          blockUI.start();

          var url = _routePrefix + '/savepetitiongroundinfo';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SavePetitionGroundInfo = _SavePetitionGroundInfo;
    return factory;
}];