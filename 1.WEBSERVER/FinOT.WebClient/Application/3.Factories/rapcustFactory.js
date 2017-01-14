'use strict';
var rapcustFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
      var _routePrefix = 'api/accountmanagement';
      var _SaveCustomer = function (custID, model) {
          blockUI.start();
          return ajax.Post(model, _routePrefix + '/saveCust')//?custid=' + custID)
          .finally(function () {
              blockUI.stop();
          });
      }

    
    var _GetCustomer = function (custid) {
        blockUI.start();
        var url = _routePrefix + '/get';

        return ajax.Get(url)        
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetCustomerFromID = function (custid) {
        blockUI.start();
        var url = _routePrefix + '/getCustomer/' + custid;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }


    var _DeleteCustomer = function (model) {
        blockUI.start();
        return ajax.Post(model, _routePrefix + '/DeleteCustomer')
        .finally(function () {
            blockUI.stop();
        });
    }
    
   
    factory.SaveCustomer = _SaveCustomer;
    factory.GetCustomer = _GetCustomer;
    factory.GetCustomerFromID = _GetCustomerFromID;
    factory.DeleteCustomer = _DeleteCustomer;
    
    return factory;
}];