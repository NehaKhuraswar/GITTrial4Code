'use strict';
var rapappellantsinfoFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
      var _SaveTenantAppealInfo = function (model,custID) {
          blockUI.start();

          var url = _routePrefix + '/savetenantappealinfo/' +custID;

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }

      var _GetCaseInfoWithModel = function (caseid, custID) {
          blockUI.start();

          var url = 'api/applicationprocessing' + '/getcaseinfo';

          if (!(caseid == null || caseid == undefined)) { url = url + '/' + caseid; }
          if (!(custID == null || custID == undefined)) { url = url + '/' + custID; }

          return ajax.Get(url)
         .finally(function () {
             blockUI.stop();
         });
      }

      factory.SaveTenantAppealInfo = _SaveTenantAppealInfo;
      factory.GetCaseInfoWithModel = _GetCaseInfoWithModel;
    
    return factory;
}];