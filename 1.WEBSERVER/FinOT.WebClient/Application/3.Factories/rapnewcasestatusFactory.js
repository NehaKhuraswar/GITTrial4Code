'use strict';
var rapnewcasestatusFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/dashboard';
      var _GetActivity = function () {
          blockUI.start();

          var url = _routePrefix + '/getactivity';

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      var _GetStatus = function (activityID) {
          blockUI.start();

          var url = _routePrefix + '/getstatus/' + activityID;

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      var _GetEmptyActivityStatus = function () {
          blockUI.start();

          var url = _routePrefix + '/getemptyactivitystatus';

          return ajax.Get(url)
          .finally(function () {
              blockUI.stop();
          });
      }
      factory.GetEmptyActivityStatus = _GetEmptyActivityStatus;
      factory.GetActivity = _GetActivity;
      factory.GetStatus = _GetStatus;
    return factory;
}];