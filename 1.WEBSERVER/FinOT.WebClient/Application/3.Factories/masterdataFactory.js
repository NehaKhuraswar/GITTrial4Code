'use strict';
var masterdataFactory = ['blockUI', 'ajaxService', '$timeout', function (blockUI, ajax, $timeout) {
    var factory = {};
    var _routePrefix = 'api/masterdata';
    var _GetStateList = function () {
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
        var url = 'api/dashboard' + '/assignanalyst/' + C_ID + '/' + AnalystUserID;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _AssignHearingOfficer = function (C_ID, HearingOfficerUserID) {
        blockUI.start();
        var url = 'api/dashboard' + '/assignhearingofficer/' + C_ID + '/' + HearingOfficerUserID;

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

    var _GetCasesForCustomer = function (custID) {
        blockUI.start();
        var url = 'api/applicationprocessing' + '/getcasesforcustomer/' + custID;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }


    var _GetStatusList = function (reqid) {
        return ajax.Get(_routePrefix + '/statuslist/get');
    }


    var _Years = function () {
        var range = 5;
        var currentYear = new Date().getFullYear();
        var _years = [];
        for (var i = range; i > 0 ; i--) {

            _years.push(currentYear - i);
        }
        for (var i = 0; i < range + 1; i++) {
            _years.push(currentYear + i);
        }
        return _years;
    }

    var _Months = function () {
        return ([
       { id: 1, value: 'January' },
       { id: 2, value: 'February' },
       { id: 3, value: 'March' },
       { id: 4, value: 'April' },
       { id: 5, value: 'May' },
       { id: 6, value: 'June' },
       { id: 7, value: 'July' },
       { id: 8, value: 'August' },
       { id: 9, value: 'September' },
       { id: 10, value: 'October' },
       { id: 11, value: 'November' },
       { id: 12, value: 'December' }])
    };

    var _Days = function () {
        return ([1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
       11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
       21, 22, 23, 24, 25, 26, 27, 29, 29, 30, 31])
    };

    var _Calender = {
        Months: _Months(),
        Days: _Days(),
        Years: _Years()
    }

    var _getDocument = function (model) {
        blockUI.start();
        var url = 'api/applicationprocessing' + '/GetDocument';

        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
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
    factory.Calender = _Calender;
    factory.GetDocument = _getDocument;

    return factory;
}];