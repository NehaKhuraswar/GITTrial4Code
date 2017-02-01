﻿'use strict';
var rapdashboardFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
  

    var _GetCaseInfoWithModel = function (caseid) {
        blockUI.start();

        var url = 'api/applicationprocessing' + '/getcaseinfo';

        if (!(caseid == null || caseid == undefined)) { url = url + '/' + caseid; }

         return ajax.Get( url)
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
    
    var _GetPetitionViewInfo = function (CID) {
        blockUI.start();

        var url = 'api/applicationprocessing' + '/GetPetitionViewInfo';
        if (!(CID == null || CID == undefined)) { url = url + '/' + CID; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetTenantAppealInfoForView = function (CID) {
        blockUI.start();

        var url = 'api/applicationprocessing' + '/GetTenantAppealInfoForView';
        if (!(CID == null || CID == undefined)) { url = url + '/' + CID; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetTenantResponseViewInfo = function (CID) {
        blockUI.start();

        var url = 'api/applicationprocessing' + '/GetTenantResponseViewInfo';
        if (!(CID == null || CID == undefined)) { url = url + '/' + CID; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetOResponseViewByCaseID = function (CID) {
        blockUI.start();
        var url = 'api/applicationprocessing' + '/GetOResponseViewByCaseID';
        if (!(CID == null || CID == undefined)) {
            url = url + '/' + CID;
        }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    factory.GetPetitionViewInfo = _GetPetitionViewInfo;
    factory.GetCaseInfo = _GetCaseInfo;
    factory.GetCaseInfoWithModel = _GetCaseInfoWithModel;
    factory.GetCaseActivityStatus = _GetCaseActivityStatus;
    factory.GetAppealInfoForView = _GetTenantAppealInfoForView;
    factory.GetTenantResponseViewInfo = _GetTenantResponseViewInfo;
    factory.GetOResponseViewByCaseID = _GetOResponseViewByCaseID;
    return factory;
}];