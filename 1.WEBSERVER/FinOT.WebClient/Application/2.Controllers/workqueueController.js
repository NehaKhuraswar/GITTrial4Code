var workqueueController = ['$scope', '$q', 'Page', 'alertService', 'masterdataFactory', 'workqueueFactory', 'model', function ($scope, $q, Page, alert, dataFactory, wqFactory, model) {
    Page.setTitle('Workqueue');
    $scope.toggleSearch = true;
    $scope.pagesizeOptions = [10, 20, 50];
    var self = this;
    self.model = model;
    self.model.SortBy = "LastModifiedDate";
    self.PendingRestructureList = [];
    self.SourceCriteriaList = [];
    self.TargetCriteriaList = [];
    self.ExpenseTypeList = [];
    self.FundingClassList = [];
    self.isLastPage = function () { return (self.model.TotalCount - (self.model.CurrentPage * self.model.PageSize) < 0); };
    self.totalPage = function () { return (Math.floor(self.model.TotalCount / self.model.PageSize) + (((self.model.TotalCount % self.model.PageSize) != 0) ? 1 : 0)) };
    //self.FundingClassCodeList = model.List.filter(function (item) { return (item.FundingClassCode != null); }).map(function (e) { return e.FundingClassCode; });

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
            self.SourceCriteriaList = response.data;
            self.TargetCriteriaList = response.data;
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

    $q.all([_getPendingRestructure(), _getExpenseType(), _getFundingClass()]).then(function () {

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

    var _getWorkQueue = function (isExport) {
        isExport = ((isExport == null || isExport == undefined) ? false : isExport);
        wqFactory.Filter(_getFilter(isExport)).then(function (response) {
            if (isExport) {
                handleFile(response.data, response.status, response.headers());
            }
            else {
                if (!alert.checkResponse(response)) { return; }
                self.model.List = response.data.List;
                self.model.TotalCount = response.data.TotalCount;
            }
        });
    }

    var _getFilter = function (isExport) {
        return {
            Filter: self.Filter,
            PageSize: self.model.PageSize,
            CurrentPage: ((isExport == true) ? 0 : self.model.CurrentPage),
            SortBy: self.model.SortBy,
            SortReverse: self.model.SortReverse,
            Export: isExport
        }
    }

    $scope.onSort = function (_sortBy) {
        if (self.model.SortBy == _sortBy) {
            self.model.SortReverse = !self.model.SortReverse;
        } else {
            self.model.SortBy = _sortBy;
            self.model.SortReverse = false;
        }
        _getWorkQueue();
    }

    $scope.onFilter = function () {
        self.model.CurrentPage = 1;
        _getWorkQueue();
    }

    $scope.getPage = function (newPage) {
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.model.CurrentPage)) {
            self.model.CurrentPage = newPage;
            _getWorkQueue();
        }
    }

    $scope.onPageSizeChange = function () {
        self.model.CurrentPage = 1;
        _getWorkQueue();
    }

    $scope.onExportResults = function (event) {
        if (event && event.stopPropagation) { event.stopPropagation(); } else if (window.event) { window.event.cancelBubble = true; }
        _getWorkQueue(true);
    }

    $scope.onClearFilter = function () {
        self.Filter = {
            Restructure: null,
            Level: null,
            SourceCriteria: [],
            TargetCriteria: [],
            RequestTypeID: null,
            ReqID: null,
            RC: null,
            Status: [],
            ExpenseType: [],
            FundingClass: [],
            FundingClassCode: []
        };
    }

    $scope.onClearFilter();
}];

var workqueueController_resolve = {
    model: ['$location', 'authFactory', 'alertService', 'workqueueFactory', function ($location, auth, alert, wqFactory) {
        return new auth.fetchToken().then(function (response) {
            if (auth.isViewer()) {
                $location.path('/rcsearch');
            }
            return wqFactory.GetWorkQueue().then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                return response.data;
            });
        });
    }]
}