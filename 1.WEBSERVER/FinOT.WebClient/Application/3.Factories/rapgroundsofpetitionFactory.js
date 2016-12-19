'use strict';
var rapgroundsofpetitionFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
      var _SavePetitionGroundInfo = function (model, custID) {
          blockUI.start();

          var url = _routePrefix + '/savepetitiongroundinfo/' + custID;

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }

      var _GetPetitionGroundInfo = function (petitionID) {
          blockUI.start();

          var url = _routePrefix + '/getgroundsinfo';
          if (!(petitionID == null || petitionID == undefined)) { url = url + '/' + petitionID; }

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SavePetitionGroundInfo = _SavePetitionGroundInfo;
      factory.GetPetitionGroundInfo = _GetPetitionGroundInfo;
    return factory;
}];