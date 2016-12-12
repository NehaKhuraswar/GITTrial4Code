'use strict';
var masterdataFactory = ['blockUI', 'ajaxService', '$timeout', function (blockUI, ajax, $timeout) {
    var factory = {};
    var _routePrefix = 'api/masterdata';
    var _GetStateList  = function () {
        blockUI.start();
        var url = 'api/accountmanagement' + '/getstatelist';
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetAccountTypes = function () {
        blockUI.start();
        var url = 'api/accountmanagement' + '/getaccounttypes'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetHearingOfficers = function () {
        blockUI.start();
        var url = 'api/dashboard' + '/gethearingofficers'

        return ajax.Get(url)
        .finally(function () { 
            blockUI.stop();
        });
    }

    var _AssignAnalyst = function (C_ID, AnalystUserID) {
        blockUI.start();
        var url = 'api/dashboard' + '/assignanalyst/' +C_ID +'/'+AnalystUserID;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
        }

    var _AssignHearingOfficer = function (C_ID, HearingOfficerUserID) {
        blockUI.start();
        var url = 'api/dashboard' + '/assignhearingofficer/' +C_ID +'/'+HearingOfficerUserID;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    
    

    var _GetStatusList = function (reqid) {
        return ajax.Get(_routePrefix + '/statuslist/get');
    }
    
    factory.GetStatusList = _GetStatusList;
    factory.GetStateList = _GetStateList;
    factory.GetAccountTypes = _GetAccountTypes;
    factory.GetHearingOfficers = _GetHearingOfficers;
    factory.GetAnalysts = _GetAnalysts;
    factory.AssignAnalyst = _AssignAnalyst;
    factory.AssignHearingOfficer =_AssignHearingOfficer;

    return factory;
}];