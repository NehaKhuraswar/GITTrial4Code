'use strict';
var rapSearchAccountController = ['$scope', '$modal', 'alertService', 'rapSearchAccountFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.AccountTypesList = [];
    self.AccountSearchModel = [];
    self.AccountSearchResult = [];
    var _getAccountTypes = function () {        
        rapFactory.GetAccountTypes().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountTypesList = response.data;
        });
    }
    var _getEmptyAccountSearchModel = function () {
        rapFactory.GetEmptyAccountSearchModel().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountSearchModel = response.data;
        });
    }
    _getAccountTypes();
    _getEmptyAccountSearchModel();
    
    self.AccountSearch = function (model) {
        rapFactory.GetAccountSearch(model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountSearchResult = response.data;
        });
    }
    
}];
var rapSearchAccountController_resolve = {
    model: ['$route', 'alertService', 'rapSearchAccountFactory', function ($route, alert, rapFactory) {
        
    }]
}