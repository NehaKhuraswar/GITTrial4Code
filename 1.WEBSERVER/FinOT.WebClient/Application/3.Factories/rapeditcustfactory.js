'use strict';
var rapeditcustFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/accountmanagement';
    var _EditCustomer = function (model) {
        blockUI.start();
        return ajax.Post(model, _routePrefix + '/editCust')
        .finally(function () {
            blockUI.stop();
        });
    }
    
   
      factory.EditCustomer = _EditCustomer;
    
    
    return factory;
}];