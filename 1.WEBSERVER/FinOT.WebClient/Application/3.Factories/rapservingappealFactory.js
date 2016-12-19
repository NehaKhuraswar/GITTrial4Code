'use strict';
var rapservingappealFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';
    var _AddAnotherOpposingParty = function (model) {
          blockUI.start();

          var url = _routePrefix + '/addopposingparty';

          return ajax.Post(model, url)
          .finally(function () {
              blockUI.stop();
        });
    }
    var _GetOpposingParty = function () {
        blockUI.start();

        var url = _routePrefix + '/getopposingparty';

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetAppealServe = function (AppealID) {
        blockUI.start();

        var url = _routePrefix + '/getappealserve/' + AppealID;

        return ajax.Get( url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _SaveTenantServingAppeal = function(model, CustID){
        blockUI.start();

        var url = _routePrefix + '/savetenantservingappeal/' + CustID;

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.AddAnotherOpposingParty = _AddAnotherOpposingParty;
    factory.GetAppealServe = _GetAppealServe;
    factory.SaveTenantServingAppeal = _SaveTenantServingAppeal;
    factory.GetOpposingParty = _GetOpposingParty;
    return factory;
}];