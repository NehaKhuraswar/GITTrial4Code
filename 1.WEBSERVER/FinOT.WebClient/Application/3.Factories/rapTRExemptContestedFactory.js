'use strict';
var rapTRExemptContestedFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
      var _SaveTenantResponseExemptContestedInfo = function (model, custID) {
          blockUI.start();

          var url = _routePrefix + '/savetenantresponseexemptcontested/' + custID;

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }

      var _GetTenantResponseExemptContestedInfo = function (TenantResponseID) {
          blockUI.start();

          var url = _routePrefix + '/gettenantresponseexemptcontestedinfo';
          if (!(TenantResponseID == null || TenantResponseID == undefined)) { url = url + '/' + TenantResponseID; }

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.SaveTenantResponseExemptContestedInfo = _SaveTenantResponseExemptContestedInfo;
      factory.GetTenantResponseExemptContestedInfo = _GetTenantResponseExemptContestedInfo;
    return factory;
}];