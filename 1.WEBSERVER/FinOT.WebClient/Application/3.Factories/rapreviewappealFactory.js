'use strict';
var rapreviewappealFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
    
      var _SubmitAppeal = function (model) {
          blockUI.start();

          var url = _routePrefix + '/submitappeal';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
          });
      }
      var _GetTenantAppealInfoForReview = function (AppealID, custID) {
          blockUI.start();

          var url = 'api/applicationprocessing' + '/GetTenantAppealInfoForReview';
          if (!(AppealID == null || AppealID == undefined)) { url = url + '/' + AppealID; }
          if (!(custID == null || custID == undefined)) { url = url + '/' + custID; }

          return ajax.Get(url)
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
      factory.SubmitAppeal = _SubmitAppeal;
      factory.GetCaseInfoWithModel = _GetCaseInfoWithModel;
      factory.GetTenantAppealInfoForReview = _GetTenantAppealInfoForReview;
    
    return factory;
}];