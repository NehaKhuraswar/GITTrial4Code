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
    var _GetAnalysts = function () {
        blockUI.start();
        var url = 'api/dashboard' + '/getanalysts'

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

    var _GetCustomer = function (custid) {
        blockUI.start();
        
        var url = 'api/accountmanagement' + '/get';
        if (!(custid == null || custid == undefined)) { url = url + '/' + custid; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    
    var _GetCasesForCustomer = function (C_ID) {
        blockUI.start();
        var url = 'api/applicationprocessing' + '/getcasesforcustomer/' + C_ID;

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
    factory.AssignHearingOfficer = _AssignHearingOfficer;
    factory.GetCasesForCustomer = _GetCasesForCustomer;
    factory.GetCustomer = _GetCustomer;

    return factory;
}];