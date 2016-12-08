'use strict';
var rapSearchAccountController = ['$scope', '$modal', 'alertService', 'rapSearchAccountFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.AccountTypesList = [];
    self.AccountSearchModel = [];
    self.AccountSearchResult = [];
    self.model = {        
        TotalCount: 0,
        PageSize: null,
        CurrentPage: 1,
        SortBy: 'Name',
        SortReverse: true
    };
    self.pagesizeOptions = [5, 10, 20, 50];
    
    //self.model.PageSize = 10;
    //self.isLastPage = function () {
    //    return (self.model.TotalCount - (self.model.CurrentPage * self.model.PageSize) <= 0);
    //};
    //self.totalPage = function () {
    //    if (self.model == null || self.model == undefined) { return 0; }
    //    return (Math.floor(self.model.TotalCount / self.model.PageSize) + (((self.model.TotalCount % self.model.PageSize) != 0) ? 1 : 0))
    //};
    var _getAccountTypes = function () {        
        masterFactory.GetAccountTypes().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountTypesList = response.data;
        });
    }
    var _getEmptyAccountSearchModel = function () {
        rapFactory.GetEmptyAccountSearchModel().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountSearchModel = response.data;
            self.AccountSearchModel.PageSize = 5;
        });
    }
    _getAccountTypes();
    _getEmptyAccountSearchModel();
    
    self.AccountSearch = function (model) {
        //model.PageSize = 10;
       
        model.CurrentPage = 1;
        model.SortBy = "Name";
        model.SortReverse = 0;
        rapFactory.GetAccountSearch(model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountSearchResult = response.data.List;
            self.AccountSearchModel.TotalCount = response.data.TotalCount;
            self.AccountSearchModel.CurrentPage = response.data.CurrentPage;
        });
    }
    self.OnClearFilter = function () {
        _getEmptyAccountSearchModel();
        self.AccountSearchResult = [];
    }
    self.isLastPage = function () {
        return (self.AccountSearchModel.TotalCount - (self.AccountSearchModel.CurrentPage * self.AccountSearchModel.PageSize) <= 0);
    };
    self.GetPage = function (newPage, model) {
        
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.AccountSearchModel.CurrentPage)) {
            self.AccountSearchModel.CurrentPage = newPage;            
            rapFactory.GetAccountSearch(model).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.AccountSearchResult = response.data.List;
                self.AccountSearchModel.CurrentPage = response.data.CurrentPage;
            });
        }
    }
    self.TotalPage = function () {
        if (self.AccountSearchModel == null || self.AccountSearchModel == undefined) { return 0; }
        return (Math.floor(self.AccountSearchModel.TotalCount / self.AccountSearchModel.PageSize) + (((self.AccountSearchModel.TotalCount % self.AccountSearchModel.PageSize) != 0) ? 1 : 0))
    };
    self.OnPageSizeChange = function () {
        self.AccountSearchModel.CurrentPage = 1;
        
        rapFactory.GetAccountSearch(self.AccountSearchModel).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountSearchResult = response.data.List;
            self.AccountSearchModel.TotalCount = response.data.TotalCount;
            self.AccountSearchModel.CurrentPage = response.data.CurrentPage;
        });
    }
    
}];
var rapSearchAccountController_resolve = {
    model: ['$route', 'alertService', 'rapSearchAccountFactory', function ($route, alert, rapFactory) {
        
    }]
}