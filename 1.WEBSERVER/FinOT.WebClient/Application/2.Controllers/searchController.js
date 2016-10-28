var searchController = ['$scope', '$q', '$filter', 'Page', 'alertService', 'masterdataFactory', 'searchFactory', 'searchType', function ($scope, $q, $filter, Page, alert, dataFactory, searchFactory, searchType) {
    if (searchType == 1) { Page.setTitle('RC Search'); } else { Page.setTitle('Request Search'); }
    $scope.toggleResults = true;
    $scope.pagesizeOptions = [10, 20, 50];
    var self = this;
    self.model = {
        List: [],
        Filter: null,
        TotalCount: 0,
        PageSize: null,
        CurrentPage: 1,
        SortBy: ((searchType == 1) ? 'RC' : 'ReqID'),
        SortReverse: false
    };
    self.model.PageSize = 10;
    self.FYList = [];
    self.PendingRestructureList = [];
    self.SourceCriteriaList = [];
    self.TargetCriteriaList = [];
    self.StatusList = [];
    self.ExpenseTypeList = [];
    self.FundingClassList = [];
    self.A6ConfigLines = [];
    self.ListHierarchy = [];
    self.A6SelectDefaultText = "";

    self.isLastPage = function () {
        return (self.model.TotalCount - (self.model.CurrentPage * self.model.PageSize) <= 0);
    };
    self.totalPage = function () {
        if (self.model == null || self.model == undefined) { return 0; }
        return (Math.floor(self.model.TotalCount / self.model.PageSize) + (((self.model.TotalCount % self.model.PageSize) != 0) ? 1 : 0))
    };

    var _getFYList = function () {
        return dataFactory.GetFiscalYear().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.FYList = response.data;
        });
    }

    var _restructureid = function () {
        return ((self.Filter.Restructure == null) ? null : self.Filter.Restructure.ID);
    }

    var _getPendingRestructure = function () {
        return dataFactory.GetPendingRestructure().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.PendingRestructureList = response.data;
        });
    }

    var _getDivision = function () {
        return dataFactory.GetDivision(null, _restructureid()).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.TargetCriteriaList = response.data;
            if (searchType == 2) {
                self.SourceCriteriaList = response.data;
            }
        });
    }

    var _getBureau = function () {
        return dataFactory.GetBureau(null, null, _restructureid()).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.SourceCriteriaList = response.data;
            self.TargetCriteriaList = response.data;
        });
    }

    var _getProgram = function () {
        return dataFactory.GetProgram(null, null, _restructureid()).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.SourceCriteriaList = response.data;
            self.TargetCriteriaList = response.data;
        });
    }

    var _getStatusList = function () {
        return dataFactory.GetStatusList().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.StatusList = response.data;
        });
    }

    var _getExpenseType = function () {
        return dataFactory.GetExpenseType().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.ExpenseTypeList = response.data;
        });
    }

    var _getFundingClass = function () {
        return dataFactory.GetFundingClass().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.FundingClassList = response.data;
        });
    }

    //Functions: select Article-6 line control
    var _getA6ConfigLines = function (lineid) {
        return a6Factory.GetLinesByFY(0, lineid).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.A6ConfigLines = response.data;
            if (self.A6ConfigLines.length) { self.A6SelectDefaultText = "Select " + self.A6ConfigLines[0].Level.Description; }
        });
    }

    self.onSelectA6ConfigLine = function (line) {
        if (self.Filter.A6ConfigLine != null && !self.Filter.A6ConfigLine.LowestLevel) {
            _getA6ConfigLines(self.Filter.A6ConfigLine.Item.ID);
            var Content = (self.ListHierarchy.length) ? '&nbsp;<i class=\"glyphicon glyphicon-forward\"></i>' : '';
            Content += '<button type=\"button\" class=\"btn btn-default btn-xs\" ng-click=\"Ctrl.goBackToLine(' + self.Filter.A6ConfigLine.Item.ID + ')\">' + self.Filter.A6ConfigLine.Item.Description + '</button>';
            self.ListHierarchy.push({ 'ID': self.Filter.A6ConfigLine.Item.ID, 'Content': Content });
        }
    }

    self.goBackToLine = function (ItemID) {
        if (ItemID != null) {
            _getA6ConfigLines(ItemID);
            var i = self.ListHierarchy.indexOf($filter('filter')(self.ListHierarchy, { ID: ItemID }, true)[0]);
            self.ListHierarchy.splice(i + 1, self.ListHierarchy.length);
        }
    }
    //Functions: select Article-6 line control

    //Functions: select GCode(s) control
    self.searchFundingClassCodeResults = [];
    self.refreshFundingClassCode = function (search) {
        if (typeof (search) == "undefined" || search == null) return [];
        if (search.length > 2) {
            dataFactory.GetGrants(search, null).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.searchFundingClassCodeResults = response.data.filter(function (i) {
                    return (self.Filter.FundingClassCode.map(function (e) { return e.Code; }).indexOf(i.Code) < 0);
                });
            });
        }
    }
    //Functions: select GCode(s) control
    if (searchType == 1) {

    }
    $q.all([_getFYList(), _getPendingRestructure(), _getStatusList(), _getExpenseType(), _getFundingClass(), _getA6ConfigLines(null)]).then(function () {

    })

    self.onRestructueChange = function () {
        self.onLevelChange();
    }

    self.onLevelChange = function () {
        self.SourceCriteriaList = [];
        self.TargetCriteriaList = [];

        if (self.Filter.Level == "D") { _getDivision(); }
        else if (self.Filter.Level == "B") { _getBureau(); }
        else if (self.Filter.Level == "P") { _getProgram(); }
    }

    self.onFundingClassCodeSelect = function ($item, $model, $label) {
        self.Filter.FundingClassCode = $item;
    };

    var _getRequestSearchResults = function (isExport) {
        isExport = ((isExport == null || isExport == undefined) ? false : isExport);
        if (searchType == 1) {
            searchFactory.SearchRC(_getFilter(isExport)).then(function (response) {
                if (isExport) {
                    handleFile(response.data, response.status, response.headers());
                } else {
                    if (!alert.checkResponse(response)) { return; }
                    self.model.List = response.data.List;
                    self.model.TotalCount = response.data.TotalCount;
                }
            });
        }
        else if (searchType == 2) {
            searchFactory.SearchRequest(_getFilter(isExport)).then(function (response) {
                if (isExport) {
                    handleFile(response.data, response.status, response.headers());
                } else {
                    if (!alert.checkResponse(response)) { return; }
                    self.model.List = response.data.List;
                    self.model.TotalCount = response.data.TotalCount;
                }
            });
        }
    }

    var _getFilter = function (isExport) {
        return {
            Filter: self.Filter,
            PageSize: self.model.PageSize,
            CurrentPage: ((isExport == true) ? 0 : self.model.CurrentPage),
            SortBy: self.model.SortBy,
            SortReverse: self.model.SortReverse,
            Export: isExport
        };
    }

    $scope.onSort = function (_sortBy) {
        if (self.model.SortBy == _sortBy) {
            self.model.SortReverse = !self.model.SortReverse;
        } else {
            self.model.SortBy = _sortBy;
            self.model.SortReverse = false;
        }
        _getRequestSearchResults();
    }

    $scope.onFilter = function () {
        self.model.CurrentPage = 1;
        _getRequestSearchResults();
        $scope.toggleResults = false
    }

    $scope.getPage = function (newPage) {
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.model.CurrentPage)) {
            self.model.CurrentPage = newPage;
            _getRequestSearchResults();
        }
    }

    $scope.onPageSizeChange = function () {
        self.model.CurrentPage = 1;
        _getRequestSearchResults();
    }

    $scope.onExportResults = function (event) {
        if (event && event.stopPropagation) { event.stopPropagation(); } else if (window.event) { window.event.cancelBubble = true; }
        _getRequestSearchResults(true);
    }

    $scope.onClearFilter = function () {
        self.Filter = {
            Restructure: null,
            Level: null,
            SourceCriteria: [],
            TargetCriteria: [],
            ProposedFY: [],
            RequestTypeID: null,
            ReqID: null,
            RC: null,
            Status: [],
            ExpenseType: [],
            FundingClass: [],
            FundingClassCode: [],
            A6ConfigLine: null,
        };
        self.ListHierarchy = [];
        _getA6ConfigLines(null);
    }

    $scope.onClearFilter();
}];

var searchOTCController_resolve = {
    searchType: ['authFactory', function (auth) {
        return new auth.fetchToken().then(function (response) {
            return 1;
        });
    }]
}

var searchRequestController_resolve = {
    searchType: ['authFactory', function (auth) {
        return new auth.fetchToken().then(function (response) {
            return 2;
        });
    }]
}