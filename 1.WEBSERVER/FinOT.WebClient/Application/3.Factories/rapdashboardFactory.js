'use strict';
var rapdashboardFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
  

    var _GetCaseInfoWithModel = function (model) {
        blockUI.start();

        var url = 'api/applicationprocessing' + '/getcaseinfo';

        //if (!(caseid == null || caseid == undefined)) { url = url + '/' + caseid; }

         return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetCaseActivityStatus = function(model) {
        blockUI.start();

        var url = 'api/dashboard' + '/getcaseactivitystatus';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
         });
    }

    var _GetCaseInfo = function () {
        blockUI.start();

        var url = _routePrefix + '/getcaseinfo';

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
      }
    
     
    factory.GetCaseInfo = _GetCaseInfo;

    factory.GetCaseInfoWithModel = _GetCaseInfoWithModel;
    factory.GetCaseActivityStatus = _GetCaseActivityStatus;


    return factory;
}];