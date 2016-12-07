'use strict';
var rapfilepetitionFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _GetCaseInfo = function () {
          blockUI.start();

          var url = _routePrefix + '/getcaseinfo';

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }


      
      var _getPetitionCategory = function()
      {
          blockUI.start();
          var url = _routePrefix + '/GetPetitioncategory/';
          var caseInfo = null;
          return ajax.Get(url,caseInfo)
          .finally(function () {
              blockUI.stop();
          });
      }

      factory.GetPetitionCategory = _getPetitionCategory;
      factory.GetCaseInfo = _GetCaseInfo;
    
    return factory;
}];