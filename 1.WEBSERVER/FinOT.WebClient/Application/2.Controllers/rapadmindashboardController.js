'use strict';
var rapadmindashboardController = ['$scope', '$modal', 'alertService', 'rapadmindashboardFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = rapGlobalFactory.CustomerDetails;
    self.pageNumberList = [];
    self.CreateCityUserAccount = function () {
        $location.path("/createCityUserAccount");
    }
    self.CreatePublicUserAccount = function () {
        $location.path("/register");
    }
    self.EditPublicUserAccount = function () {
        $location.path("/editpublicuser");
    }

    self.AccountTypesList = [];
    self.AccountSearchModel = [];
    self.AccountSearchResult = [];
    self.model = {
        List:[],
        TotalCount: 0,
        PageSize: null,
        CurrentPage: 1,
        SortBy: 'Name',
        SortReverse: true
    };
    self.pageNumberList = [];
    self.pagesizeOptions = [5, 10, 20, 50];
    self.FromRecord = 1;
    self.ToRecord = 5;
    
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
    self.GeneratePageNumberList = function () {
        var TotalPages = Math.ceil(self.AccountSearchModel.TotalCount / self.AccountSearchModel.PageSize);
        for (var i = 1; i <= TotalPages; i++) {
            self.pageNumberList.push({ text: i, active: true });
        }

    }
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
            self.AccountSearchModel.CurrentPage = 1;
            self.AccountSearchModel.SortBy = "Name";
            self.AccountSearchModel.SortReverse = 0;
            self.FromRecord = self.AccountSearchResult[0].RankNo;
            self.ToRecord = self.AccountSearchResult[(self.AccountSearchResult.length - 1)].RankNo;
            self.GeneratePageNumberList();
        });
    }
    self.OnClearFilter = function () {
        _getEmptyAccountSearchModel();
        self.AccountSearchResult = [];
        self.pageNumberList = [];
    }
    self.isLastPage = function () {
        return (self.AccountSearchModel.TotalCount - (self.AccountSearchModel.CurrentPage * self.AccountSearchModel.PageSize) <= 0);
    };
    self.isFirstPage = function () {
        return (self.AccountSearchModel.CurrentPage == 1);
    };
    self.GetPage = function (newPage, model) {
        
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.AccountSearchModel.CurrentPage)) {
            self.AccountSearchModel.CurrentPage = newPage;            
            rapFactory.GetAccountSearch(model).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.AccountSearchResult = response.data.List;
                self.AccountSearchModel.CurrentPage = response.data.CurrentPage;
                self.FromRecord = self.AccountSearchResult[0].RankNo;
                self.ToRecord = self.AccountSearchResult[(self.AccountSearchResult.length - 1)].RankNo;
            });
        }
    }
    self.GetNextPage = function (model) {
        var newPage = self.AccountSearchModel.CurrentPage + 1;
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.AccountSearchModel.CurrentPage)) {
            self.AccountSearchModel.CurrentPage = self.AccountSearchModel.CurrentPage + 1;
            rapFactory.GetAccountSearch(self.AccountSearchModel).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.AccountSearchResult = response.data.List;
                self.AccountSearchModel.CurrentPage = response.data.CurrentPage;
                self.FromRecord = self.AccountSearchResult[0].RankNo;
                self.ToRecord = self.AccountSearchResult[(self.AccountSearchResult.length - 1)].RankNo;
            });
        }
    }
    self.GetPreviousPage = function (model) {
        var newPage = self.AccountSearchModel.CurrentPage - 1;
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.AccountSearchModel.CurrentPage)) {
            self.AccountSearchModel.CurrentPage = self.AccountSearchModel.CurrentPage - 1;
            rapFactory.GetAccountSearch(self.AccountSearchModel).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.AccountSearchResult = response.data.List;
                self.AccountSearchModel.CurrentPage = response.data.CurrentPage;
                self.FromRecord = self.AccountSearchResult[0].RankNo;
                self.ToRecord = self.AccountSearchResult[(self.AccountSearchResult.length - 1)].RankNo;
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
    self.onSort = function (_sortBy, model) {
        if (model.SortBy == _sortBy) {
            model.SortReverse = !model.SortReverse;
        } else {
            model.SortBy = _sortBy;
            model.SortReverse = false;
        }
        rapFactory.GetAccountSearch(model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountSearchResult = response.data.List;
            self.AccountSearchModel.TotalCount = response.data.TotalCount;
            self.AccountSearchModel.CurrentPage = response.data.CurrentPage;
            self.AccountSearchModel.SortBy = model.SortBy;
            self.AccountSearchModel.SortReverse = model.SortReverse;

        });
    }
    self.GetCaseActivityStatus = function (model) {
        //self.caseinfo.CaseID = 
        rapFactory.GetCaseActivityStatus(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            self.caseinfo.ActivityStatus = response.data;

           // self.caseinfo = response.data;
            //rapGlobalFactory.CaseDetails = self.caseinfo;
        });
       // $location.path("/fileappeal");
    }
    

}];
var rapadmindashboardController_resolve = {
    model: ['$route', 'alertService',  function ($route, alert) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //    $scope.model = response.data;
        //    //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}