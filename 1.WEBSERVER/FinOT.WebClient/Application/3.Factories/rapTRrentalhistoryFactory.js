'use strict';
var rapTRrentalhistoryFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _SaveTenantResponseRentalHistoryInfo = function (model, custID) {
          blockUI.start();

          var url = _routePrefix + '/savetenantresponserentalhistoryinfo/' + custID;

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      var _GetTenantResponseRentalHistoryInfo = function (TenantResponseID, custID) {
          blockUI.start();

          var url = _routePrefix + '/gettenantresponserentalhistoryinfo';
          if (!(TenantResponseID == null || TenantResponseID == undefined)) { url = url + '/' + TenantResponseID; }
          if (!(custID == null || custID == undefined)) { url = url + '/' + custID; }

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      var _GetEmptyTenantResponseRentalIncrementInfo = function () {
          blockUI.start();

          var url = _routePrefix + '/getemptytrrentalhistoryinfo';

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveTenantResponseRentalHistoryInfo = _SaveTenantResponseRentalHistoryInfo;
      factory.GetTenantResponseRentalHistoryInfo = _GetTenantResponseRentalHistoryInfo;
      factory.GetEmptyTenantResponseRentalIncrementInfo = _GetEmptyTenantResponseRentalIncrementInfo;
    return factory;
}];