'use strict';
var rapstaffdashboardController = ['$scope', '$modal', 'alertService', 'rapstaffdashboardFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', '$anchorScroll', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory, $anchorScroll) {
    var self = this;
    self.caseinfo = rapGlobalFactory.CaseDetails;
    self.model = rapGlobalFactory.CityUser;
    self.Error = "";
    self.CaseSearchModel = [];
    self.CaseSearchResult = [];
    self.CasewithNoAnalystResult = [];
    self.SearchModel = {
        TotalCount: 0,
        PageSize: null,
        CurrentPage: 1,
        SortBy: 'CaseID',
        SortReverse: true
    };
    self.pageNumberList = [];
    self.pagesizeOptions = [5, 10, 20, 50];
    self.CaseStatusList =[{ "StatusID": "1", "StatusName": "All" }, { "StatusID": "2", "StatusName": "Open"}, { "StatusID": "3", "StatusName": "Closed"}]
    self.pagingsortingModel = [];
    self.CaseList = [];
    self.Analysts = [];
    self.FromRecord = 1;
    self.ToRecord = 5;
    self.FromRecordNoAnalyst = 1;
    self.ToRecordNoAnalyst = 10;
    self.HearingOfficers = [];
    self.InviteThirdPartyUser = function () {
        $location.path("/invitethirdparty");
    }
    self.FilePetition = function () {
        $location.path("/filePetition");
    }
    self.NewCaseStatus = function () {
        $location.path("/newCaseStatus");
    }
    self.AccountSearch = function () {
        $location.path("/accountSearch");
    }
    self.ManageAccounts = function () {
        $location.path("/admindashboard");
    }

    $anchorScroll();
   
    self.FileAppeal = function (model) {
        //self.caseinfo.CaseID = 
        rapFactory.GetCaseInfoWithModel(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }

            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
        });
        $location.path("/fileappeal");
    }

    self.GetCaseActivityStatus = function (model) {
        rapFactory.GetCaseActivityStatus(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.caseinfo.ActivityStatus = response.data;
        });
    }
    var _GetCaseswithNoAnalyst = function (pagingsortingModel, userID) {
        pagingsortingModel.SortBy = 'CaseID';
        rapFactory.GetCaseswithNoAnalyst(pagingsortingModel, userID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.CasewithNoAnalystResult = response.data;
            self.pagingsortingModel.TotalCount = response.data.TotalCount;
            self.pagingsortingModel.CurrentPage = response.data.CurrentPage;
            self.FromRecordNoAnalyst = self.CasewithNoAnalystResult.List[0].RankNo;
            self.ToRecordNoAnalyst = self.CasewithNoAnalystResult.List[(self.CasewithNoAnalystResult.List.length - 1)].RankNo;
            self.GeneratePageNumberListNoAnalyst();
        });
    }
    

    var _GetAnalysts = function () {
        masterFactory.GetAnalysts().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.Analysts = response.data;
        });
    }
    _GetAnalysts();

    var _GetHearingOfficers = function () {
        masterFactory.GetHearingOfficers().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.HearingOfficers = response.data;
        });
    }
    _GetHearingOfficers();
    var _GetCaseInfo = function () {

        rapFactory.GetCaseInfo().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }

            self.caseinfo = response.data;
            rapGlobalFactory.CaseDetails = self.caseinfo;
        });
    }
    var _getEmptyCaseSearchModel = function () {
        rapFactory.GetEmptyCaseSearchModel().then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.CaseSearchModel = response.data;
            self.CaseSearchModel.PageSize = 5;
            self.pagingsortingModel = angular.copy(response.data);
            self.pagingsortingModel.PageSize = 10;
            self.pagingsortingModel.CurrentPage = 1;
            self.pagingsortingModel.SortBy = 'CaseID';
            _GetCaseswithNoAnalyst(self.pagingsortingModel, self.model.UserID);
        });
    }
    _getEmptyCaseSearchModel();

    if (self.caseinfo == null) {
        _GetCaseInfo();
    }

    self.GeneratePageNumberListNoAnalyst = function()
    {
        self.pageNumberListNoAnalyst =[];
        var TotalPages = Math.ceil(self.pagingsortingModel.TotalCount / self.pagingsortingModel.PageSize);
        for (var i = 1; i <= TotalPages; i++)
        {
            self.pageNumberListNoAnalyst.push({ text: i, active: true });
        }
         
    }
    
    self.isLastPageNoAnalyst = function () {
        return (self.pagingsortingModel.TotalCount - (self.pagingsortingModel.CurrentPage * self.pagingsortingModel.PageSize) <= 0);
    };
    self.isFirstPageNoAnalyst = function () {
        return (self.pagingsortingModel.CurrentPage == 1);
    };
    self.GetPageNoAnalyst = function (newPage, model) {

        //if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.CaseSearchModel.CurrentPage)) {
        self.pagingsortingModel.CurrentPage = newPage;
        rapFactory.GetCaseswithNoAnalyst(self.pagingsortingModel, self.model.UserID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.CasewithNoAnalystResult = response.data;
            self.pagingsortingModel.CurrentPage = response.data.CurrentPage;
            self.FromRecordNoAnalyst = self.CasewithNoAnalystResult.List[0].RankNo;
            self.ToRecordNoAnalyst = self.CasewithNoAnalystResult.List[(self.CasewithNoAnalystResult.List.length - 1)].RankNo;
        });
        // }
    }
    self.GetNextPageNoAnalyst = function (model) {
        var newPage = self.pagingsortingModel.CurrentPage + 1;
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.CaseSearchModel.CurrentPage)) {
            self.pagingsortingModel.CurrentPage = self.pagingsortingModel.CurrentPage+1;
            rapFactory.GetCaseswithNoAnalyst(self.pagingsortingModel, self.model.UserID).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.CasewithNoAnalystResult = response.data;
                self.pagingsortingModel.CurrentPage = response.data.CurrentPage;
                self.FromRecordNoAnalyst = self.CasewithNoAnalystResult.List[0].RankNo;
                self.ToRecordNoAnalyst = self.CasewithNoAnalystResult.List[(self.CasewithNoAnalystResult.List.length - 1)].RankNo;
            });
        }
    }
    self.GetPreviousPageNoAnalyst = function (model) {
        var newPage = self.pagingsortingModel.CurrentPage - 1;
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.pagingsortingModel.CurrentPage)) {
            self.pagingsortingModel.CurrentPage = self.pagingsortingModel.CurrentPage - 1;
            rapFactory.GetCaseswithNoAnalyst(self.pagingsortingModel, self.model.UserID).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.CasewithNoAnalystResult = response.data;
                self.pagingsortingModel.CurrentPage = response.data.CurrentPage;
                self.FromRecordNoAnalyst = self.CasewithNoAnalystResult.List[0].RankNo;
                self.ToRecordNoAnalyst = self.CasewithNoAnalystResult.List[(self.CasewithNoAnalystResult.List.length - 1)].RankNo;
            });
        }
    }
    self.OnPageSizeChangeNoAnalyst = function () {
        self.pagingsortingModel.CurrentPage = 1;

        rapFactory.GetCaseswithNoAnalyst(self.pagingsortingModel, self.model.UserID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.CasewithNoAnalystResult = response.data;
            self.pagingsortingModel.TotalCount = response.data.TotalCount;
            self.pagingsortingModel.CurrentPage = response.data.CurrentPage;
            self.FromRecordNoAnalyst = self.CasewithNoAnalystResult.List[0].RankNo;
            self.ToRecordNoAnalyst = self.CasewithNoAnalystResult.List[(self.CasewithNoAnalystResult.List.length - 1)].RankNo;
        });
    }

    self.onSortNoAnalyst = function (_sortBy, model) {
       
        if (model.SortBy == _sortBy) {
            model.SortReverse = !model.SortReverse;
        } else {
            model.SortBy = _sortBy;
            model.SortReverse = false;
        }
        rapFactory.GetCaseswithNoAnalyst(model, self.model.UserID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.CasewithNoAnalystResult = response.data;
            self.pagingsortingModel.TotalCount = response.data.TotalCount;
            self.pagingsortingModel.CurrentPage = response.data.CurrentPage;
            self.pagingsortingModel.SortBy = model.SortBy;
            self.pagingsortingModel.SortReverse = model.SortReverse;


        });
        }
    self.AssignAnalyst = function (C_ID, Analyst) {

        masterFactory.AssignAnalyst(C_ID, Analyst.UserID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            _GetCaseswithNoAnalyst(self.pagingsortingModel, self.model.UserID);
            //_GetCasesNoAnalyst(self.model.UserID);
        });
    }
    self.AssignAnalystSearch = function (C_ID, Analyst) {

        masterFactory.AssignAnalyst(C_ID, Analyst.UserID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            _GetCaseSearch();
            
        });
    }
    var _GetCaseSearch = function()
    {
        rapFactory.GetCaseSearch(self.CaseSearchModel).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.CaseSearchResult = response.data.List;
        })
    }
    self.AssignHearingOfficer = function (C_ID, HearingOfficer) {

        masterFactory.AssignHearingOfficer(C_ID, HearingOfficer.UserID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            _GetCaseswithNoAnalyst(self.pagingsortingModel, self.model.UserID);
           // _GetCasesNoAnalyst(self.model.UserID);
        });
    }
    self.AssignHearingOfficerSearch = function (C_ID, HearingOfficer) {

        masterFactory.AssignHearingOfficer(C_ID, HearingOfficer.UserID).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            _GetCaseSearch();
        });
    }
    self.OpenSelectedCase = function (caseinfo)
    {
        rapGlobalFactory.SelectedCase = caseinfo;
        $location.path("/selectedcase");
    }

    self.OnClearFilter = function () {
        _getEmptyCaseSearchModel();
        self.CaseSearchResult = [];
         self.pageNumberList =[];
    }

    

    self.CaseSearch = function (model) {

        model.CurrentPage = 1;
        model.SortBy = "CaseID";
        model.SortReverse = 0;
        
        rapFactory.GetCaseSearch(model).then(function (response) {
            if (!alert.checkForResponse(response)) {
                self.Error = rapGlobalFactory.Error;
                $anchorScroll();
                return;
            }
            self.CaseSearchResult = response.data.List;
            self.CaseSearchModel.TotalCount = response.data.TotalCount;
            self.CaseSearchModel.CurrentPage = response.data.CurrentPage;
            self.FromRecord = self.CaseSearchResult[0].RankNo;
            self.ToRecord = self.CaseSearchResult[(self.CaseSearchResult.length-1)].RankNo;
            self.GeneratePageNumberList();
        });
    }
    self.GeneratePageNumberList = function()
    {
        self.pageNumberList =[];
        var TotalPages = Math.ceil(self.CaseSearchModel.TotalCount / self.CaseSearchModel.PageSize);
        for (var i = 1; i <= TotalPages; i++)
        {
            self.pageNumberList.push({ text: i, active: true });
        }
         
    }
    
    self.isLastPage = function () {
        return (self.CaseSearchModel.TotalCount - (self.CaseSearchModel.CurrentPage * self.CaseSearchModel.PageSize) <= 0);
    };
    self.isFirstPage = function () {
        return (self.CaseSearchModel.CurrentPage == 1);
    };
    self.GetPage = function (newPage, model) {

        //if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.CaseSearchModel.CurrentPage)) {
            self.CaseSearchModel.CurrentPage = newPage;
            rapFactory.GetCaseSearch(self.CaseSearchModel).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.CaseSearchResult = response.data.List;
                self.CaseSearchModel.CurrentPage = response.data.CurrentPage;
                self.FromRecord = self.CaseSearchResult[0].RankNo;
                self.ToRecord = self.CaseSearchResult[(self.CaseSearchResult.length - 1)].RankNo;
            });
       // }
    }
    self.GetNextPage = function (model) {
        var newPage = self.CaseSearchModel.CurrentPage + 1;
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.CaseSearchModel.CurrentPage)) {
            self.CaseSearchModel.CurrentPage = self.CaseSearchModel.CurrentPage+1;
            rapFactory.GetCaseSearch(self.CaseSearchModel).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.CaseSearchResult = response.data.List;
                self.CaseSearchModel.CurrentPage = response.data.CurrentPage;
                self.FromRecord = self.CaseSearchResult[0].RankNo;
                self.ToRecord = self.CaseSearchResult[(self.CaseSearchResult.length - 1)].RankNo;
            });
        }
    }
    self.GetPreviousPage = function (model) {
        var newPage = self.CaseSearchModel.CurrentPage - 1;
        if ((newPage > 0 && !self.isLastPage()) || (newPage > 0 && newPage < self.CaseSearchModel.CurrentPage)) {
            self.CaseSearchModel.CurrentPage = self.CaseSearchModel.CurrentPage - 1;
            rapFactory.GetCaseSearch(self.CaseSearchModel).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                self.CaseSearchResult = response.data.List;
                self.CaseSearchModel.CurrentPage = response.data.CurrentPage;
                self.FromRecord = self.CaseSearchResult[0].RankNo;
                self.ToRecord = self.CaseSearchResult[(self.CaseSearchResult.length - 1)].RankNo;
            });
        }
    }
    self.OnPageSizeChange = function () {
        self.CaseSearchModel.CurrentPage = 1;

        rapFactory.GetCaseSearch(self.CaseSearchModel).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.CaseSearchResult = response.data.List;
            self.CaseSearchModel.TotalCount = response.data.TotalCount;
            self.CaseSearchModel.CurrentPage = response.data.CurrentPage;
            self.FromRecord = self.CaseSearchResult[0].RankNo;
            self.ToRecord = self.CaseSearchResult[(self.CaseSearchResult.length - 1)].RankNo;
        });
    }

    self.onSort = function (_sortBy, model) {
       
        if (model.SortBy == _sortBy) {
            model.SortReverse = !model.SortReverse;
        } else {
            model.SortBy = _sortBy;
            model.SortReverse = false;
            }
        rapFactory.GetCaseSearch(model).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.CaseSearchResult = response.data.List;
            self.CaseSearchModel.TotalCount = response.data.TotalCount;
            self.CaseSearchModel.CurrentPage = response.data.CurrentPage;
            self.CaseSearchModel.SortBy = model.SortBy;
            self.CaseSearchModel.SortReverse = model.SortReverse;


        });
}

}];
var rapstaffdashboardController_resolve = {
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