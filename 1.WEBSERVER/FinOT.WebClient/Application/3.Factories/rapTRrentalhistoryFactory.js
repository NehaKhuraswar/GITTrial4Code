'use strict';
var rapTRrentalhistoryFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _SaveTenantRentalHistoryInfo = function (model, custID) {
          blockUI.start();

          var url = _routePrefix + '/saverentalhistoryinfo/' + custID;

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      var _GetRentalHistoryInfo = function (petitionID) {
          blockUI.start();

          var url = _routePrefix + '/getrentalhistoryinfo';
          if (!(petitionID == null || petitionID == undefined)) { url = url + '/' + petitionID; }

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      var _GetEmptyTenantRentalIncrementInfo = function () {
          blockUI.start();

          var url = _routePrefix + '/getemptyrentalhistoryinfo';

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveTenantRentalHistoryInfo = _SaveTenantRentalHistoryInfo;
      factory.GetRentalHistoryInfo = _GetRentalHistoryInfo;
      factory.GetEmptyTenantRentalIncrementInfo = _GetEmptyTenantRentalIncrementInfo;
    return factory;
}];