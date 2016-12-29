'use strict';
var rapfileappealFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _GetCaseInfo = function (model) {
        blockUI.start();

        var url = _routePrefix + '/getcaseinfo';

        return ajax.Post(model,url)
        .finally(function () {
            blockUI.stop();
        });
      }

      var _GetPetitionCategory = function () {
          blockUI.start();
          var url = _routePrefix + '/GetPetitioncategory/';
          var caseInfo = null;
          return ajax.Get(url, caseInfo)
          .finally(function () {
              blockUI.stop();
          });
      }
    
      var _GetAppealPageSubmissionStatus = function (custId) {
          blockUI.start();
          var url = _routePrefix + '/GetAppealPageSubmissionStatus/';
          if (!(custId == null || custId == undefined)) { url = url + '/' + custId; }
          return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
      }
     
      factory.GetCaseInfo = _GetCaseInfo;
      factory.GetPetitionCategory = _GetPetitionCategory;
      factory.GetAppealPageSubmissionStatus = _GetAppealPageSubmissionStatus;

    return factory;
}];