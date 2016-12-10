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
     var _GetTenantLostServiceInfo = function (petitionID) {
          blockUI.start();

          var url = _routePrefix + '/gettenantlostservice';
          if (!(petitionID == null || petitionID == undefined)) { url = url + '/' + petitionID; }

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
     }

    var _GetEmptyProblemsInfo = function (petitionID) {
          blockUI.start();

          var url = _routePrefix + '/getemptyproblemsinfo';

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
    }

    var _GetEmptyLostServicesInfo = function (petitionID) {
          blockUI.start();

          var url = _routePrefix + '/getemptylostservicesinfo';

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }

     factory.GetTenantLostServiceInfo = _GetTenantLostServiceInfo;
     factory.SaveTenantLostServiceInfo = _SaveTenantLostServiceInfo;
     factory.GetEmptyProblemsInfo = _GetEmptyProblemsInfo;
     factory.GetEmptyLostServicesInfo = _GetEmptyLostServicesInfo;


    return factory;
}];