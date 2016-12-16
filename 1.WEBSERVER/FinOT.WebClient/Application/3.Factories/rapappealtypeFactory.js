'use strict';
var rapappealtypeFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/applicationprocessing';
    
      var _GetCaseInfo = function (caseID) {
        blockUI.start();

        var url = _routePrefix + '/getcaseinfo/' + caseID;

        return ajax.Get(url)
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

      factory.GetPetitionCategory = _GetPetitionCategory;
     
    factory.GetCaseInfo = _GetCaseInfo;
    
    return factory;
}];