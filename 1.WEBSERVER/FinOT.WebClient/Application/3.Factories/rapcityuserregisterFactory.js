'use strict';
var rapcityuserregisterFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';

    
    var _CreateCityUserAccount = function (model) {
        blockUI.start();
        var url = _routePrefix + '/createcityuseraccount';
        return ajax.Post(model, url)
        .finally(function () {
            blockUI.stop();
        });
    }
     var _GetCityUserAcctEmpty = function () {
        blockUI.start();
        var url = _routePrefix + '/getcityuseracctempty';

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

   
  
    factory.CreateCityUserAccount = _CreateCityUserAccount;
    factory.GetCityUserAcctEmpty = _GetCityUserAcctEmpty;
    //factory.GetAccountTypes = _GetAccountTypes;
    
    return factory;
}];