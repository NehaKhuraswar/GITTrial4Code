'use strict';
var rapinvitethirdpartyFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
  

    var _SearchInviteThirdPartyUser = function (model) {
        blockUI.start();

        var url = _routePrefix + '/searchinvite'
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
    //var _GetOTRequest = function (reqid, fy) {
    //    blockUI.start();

    //    var url = _routePrefix + '/get';
    //    if (!(reqid == null || reqid == undefined)) { url += '/' + reqid; }
    //    if (!(fy == null || fy == undefined)) { url += '/' + fy; }

    //    return ajax.Get(url)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}
    //var _GetCustomer = function (custid) {
    //    blockUI.start();
    //    var reqid = 1;
    //    var url = _routePrefix + '/get';
    //    //if (!(custid == null || custid == undefined)) { url += '/' + custid; }
    //    //var url = _routePrefix + '/notes/get';
    //    if (!(reqid == null || reqid == undefined)) { url = url + '/' + reqid; }
    //    //try{
    //    return ajax.Get(url)
    ////}
    ////catch(err){
    ////   console.log(err.name + ': "' + err.message +  '" occurred when assigning x.');
    ////}
    //.finally(function () {
    //    blockUI.stop();
    //});
    //}

    //var _GetNotes = function (reqid) {
    //    blockUI.start();

    //    var url = _routePrefix + '/notes/get'
    //    if (!(reqid == null || reqid == undefined)) { url = url + '/' + reqid; }

    //    return ajax.Get(url).finally(function () {
    //        blockUI.stop();
    //    });
    //}

    //var _SaveOTRequest = function (reqid, model) {
    //    blockUI.start();

    //    var url = _routePrefix + '/save';
    //    if (!(reqid == null || reqid == undefined)) {
    //        url += '?reqid=' + reqid;
    //    }

    //    return ajax.Post(model, url)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}

    //var _SaveNotes = function (reqid, model) {
    //    blockUI.start();
    //    return ajax.Post(model, _routePrefix + '/notes/save?reqid=' + reqid)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}
    //var _SaveCustomer = function (custID, model) {
    //    blockUI.start();
    //    return ajax.Post(model, _routePrefix + '/saveCust')//?custid=' + custID)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}

    //var appendQuotes = function (value) {
    //    return '"' + value + '"'
    //}

    //factory.GetOTRequest = _GetOTRequest;
    //factory.SaveOTRequest = _SaveOTRequest;
    //factory.GetNotes = _GetNotes;
    //factory.SaveNotes = _SaveNotes;
    //factory.SaveCustomer = _SaveCustomer;
    //factory.GetCustomer = _GetCustomer;
    factory.SearchInviteThirdPartyUser = _SearchInviteThirdPartyUser;

    return factory;
}];