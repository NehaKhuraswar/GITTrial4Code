'use strict';
var rapcustFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/accountmanagement';

    var _GetOTRequest = function (reqid, fy) {
        blockUI.start();

        var url = _routePrefix + '/get';
        if (!(reqid == null || reqid == undefined)) { url += '/' + reqid; }
        if (!(fy == null || fy == undefined)) { url += '/' + fy; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetCustomer = function (custid) {
        blockUI.start();
        var reqid = 1;
        var url = _routePrefix + '/get';
        if (!(reqid == null || reqid == undefined)) { url = url + '/' + reqid; }

        return ajax.Get(url)        
        .finally(function () {
            blockUI.stop();
        });
    }  
    
   
    factory.SaveCustomer = _SaveCustomer;
    factory.GetCustomer = _GetCustomer
    
    return factory;
}];