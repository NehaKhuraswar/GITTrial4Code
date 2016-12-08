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

    var _GetFiscalYear = function (reqtypeid) {
        blockUI.start();
        var url = _routePrefix + '/fiscalyear/get';
        if (!(reqtypeid == null || reqtypeid == undefined)) { url += '/' + reqtypeid; }
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetPendingRestructure = function () {
        blockUI.start();
        return ajax.Post({}, _routePrefix + '/restructure/pending')
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetDivision = function (fy, restructureid) {
        blockUI.start();
        return ajax.Post({ FY: fy, RestructureID: restructureid }, _routePrefix + '/division')
        .finally(function () {
            blockUI.stop();
        });

        //var deferred = $q.defer();
        //deferred.resolve(response);
        //deferred.reject(response);
        //return deferred.promise;
    }

    var _GetBureau = function (fy, division, restructureid) {
        blockUI.start();
        return ajax.Post({ FY: fy, Division: division, RestructureID: restructureid }, _routePrefix + '/bureau')
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetProgram = function (fy, bureau, restructureid) {
        blockUI.start();
        return ajax.Post({ FY: fy, Bureau: bureau, RestructureID: restructureid }, _routePrefix + '/program')
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetFundingType = function () {
        return ajax.Get(_routePrefix + '/fundingtype/get');
    }

    var _GetExpenseType = function () {
        return ajax.Get(_routePrefix + '/expensetype/get');
    }

    var _GetFundingClass = function () {
        return ajax.Get(_routePrefix + '/fundingclass/get');
    }

    var _GetFundingSource = function () {
        return ajax.Get(_routePrefix + '/fundingsource/get');
    }

    var _GetA6ReimbursementRates = function (reqid) {
        return ajax.Get(_routePrefix + '/fundingsource/get');
    }

    var _GetGrants = function (code, fundingclassid) {
        return ajax.Post({ ID: fundingclassid, Code: code}, _routePrefix + '/grants');
    }
    
    var _GetBCode = function (fy, code) {
        return ajax.Post({ FY: fy, Code: code }, _routePrefix + '/bcode');
    }

    var _GetRSCode = function (fy, code) {
        return ajax.Post({ FY: fy, Code: code }, _routePrefix + '/rscode');
    }

    var _GetMHYSector = function (fy, code) {
        return ajax.Post({ FY: fy, Code: code }, _routePrefix + '/mhysector');
    }

    var _GetFundingStream = function (fy, code) {
        return ajax.Post({ FY: fy, Code: code }, _routePrefix + '/fundingstream');
    }

    var _GetA6ReimbursementRates = function (fy) {
        return ajax.Post(fy, _routePrefix + '/a6reimbursementrates');
    }

    var _GetDocumentCategory = function () {
        return ajax.Post({}, _routePrefix + '/documentcategory');
    }

    var _GetDocumentExtensions = function () {
        return ajax.Post({}, _routePrefix + '/documentextensions');
    }

    var _GetFourthCharList = function (reqid) {
        return ajax.Post(appendQuotes(reqid), _routePrefix + '/fourthcharacterlist');
    }

    var _GetStatusList = function (reqid) {
        return ajax.Get(_routePrefix + '/statuslist/get');
    }

    var appendQuotes = function (value) {
        return '"' + value + '"'
    }

    factory.GetFiscalYear = _GetFiscalYear,
    factory.GetPendingRestructure = _GetPendingRestructure,
    factory.GetDivision = _GetDivision;
    factory.GetBureau = _GetBureau;
    factory.GetProgram = _GetProgram;
    factory.GetFundingType = _GetFundingType;
    factory.GetExpenseType = _GetExpenseType;
    factory.GetFundingClass = _GetFundingClass;
    factory.GetFundingSource = _GetFundingSource;
    factory.GetGrants = _GetGrants;
    factory.GetBCode = _GetBCode;
    factory.GetRSCode = _GetRSCode;
    factory.GetMHYSector = _GetMHYSector;
    factory.GetFundingStream = _GetFundingStream;
    factory.GetA6ReimbursementRates = _GetA6ReimbursementRates;
    factory.GetDocumentCategory = _GetDocumentCategory;
    factory.GetDocumentExtensions = _GetDocumentExtensions;
    factory.GetFourthCharList = _GetFourthCharList;
    factory.GetStatusList = _GetStatusList;
    factory.GetStateList = _GetStateList;
    factory.GetAccountTypes = _GetAccountTypes;

    return factory;
}];