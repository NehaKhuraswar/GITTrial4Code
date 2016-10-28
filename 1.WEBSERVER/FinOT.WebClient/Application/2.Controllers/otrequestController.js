'use strict';
var otrequestController = ['$scope', '$modal', 'Page', 'alertService', 'otrequestFactory', 'model', function ($scope, $modal, Page, alert, otFactory, model) {
    Page.setTitle('OT Request');
    var self = this;
    self.model = model;

    $scope.ControlElementAccess = function () {
        angular.forEach(self.model.Access, function (item) {
            var element = document.querySelectorAll(item.ElementName);
            if (!item.Editable && item.Viewable) {
                angular.element(element).attr("disabled", "disabled");
                angular.element(element).removeClass("hide").addClass("show");
            }
            else if (!item.Editable && !item.Viewable) {
                angular.element(element).attr("disabled", "disabled");
                angular.element(element).removeClass("show").addClass("hide");
            }
            else if (item.Editable && item.Viewable) {
                angular.element(element).removeAttr("disabled");
                angular.element(element).removeClass("hide").addClass("show");
            }
        });
    }

    $scope.GetAccessConfig = function () {
        if (self.model.ReqID == null || self.model.ReqID == undefined) { return; }
        otFactory.GetAccessConfig(self.model.ReqID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.model.Access = response.data;
            $scope.ControlElementAccess();
        });
    }

    $scope.ChangeFY = function (fy) {
        if (fy == null || fy == undefined) { return; }
        otFactory.GetOTRequest(self.model.ReqID, fy).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.model = response.data;
        });
    }

    $scope.GetDocuments = function (reqid) {
        if (reqid == null || reqid == undefined) { return; }
        return otFactory.GetDocuments(reqid).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.model.Documents = response.data;
        })
    }

    $scope.refreshNotes = function (reqid) {
        if (reqid == null || reqid == undefined) { return; }
        return otFactory.GetNotes(reqid).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.model.Notes = response.data;
        })
    }

    $scope.openWorkflowPopup = function (action) {
        var modalInstance = $modal.open({
            animation: true,
            size: 'md',
            templateUrl: 'Views/Request/_Workflow.html',
            controller: ['$scope', '$modalInstance', 'alertService', 'rcrequestFactory', 'ReqID', function ($scope, $modalInstance, alert, otFactory, ReqID) {
                var self = this;
                //scope variables
                self.Action = action;
                self.ActionName = '';
                self.CommentStatus = '';
                self.Comments = '';
                self.PriorStatusList = [];

                var _getPriorStatus = function () {
                    return otFactory.GetReturnStatus(ReqID).then(function (response) {
                        if (!alert.checkResponse(response)) { return; }
                        self.PriorStatusList = response.data;
                    });
                }

                //change display variables based on action
                if (action == 'A') {
                    self.ActionName = 'Approve';
                    self.CommentStatus = 'optional';
                }
                else if (action == 'R') {
                    self.ActionName = 'Return';
                    self.CommentStatus = 'required';
                    _getPriorStatus();
                }
                else if (action == 'W') {
                    self.ActionName = 'Withdraw';
                    self.CommentStatus = 'required';
                }

                $scope.submit = function () {
                    var newstatus = null;
                    if (angular.isDefined(self.NewStatus) && self.NewStatus != null) { newstatus = { ID: self.NewStatus.ID, Description: self.NewStatus.Description }; }
                    var model = { ReqID: ReqID, Action: self.Action, NewStatus: newstatus, Comments: self.Comments };
                    otFactory.WorkflowAction(model).then(function (response) {
                        if (!alert.checkResponse(response)) { return; }
                        $modalInstance.close(response.data);
                    });
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
            }],
            controllerAs: 'Ctrl',
            resolve: {
                ReqID: function () { return self.model.ReqID }
            }
        });

        modalInstance.result.then(function (data) {
            angular.copy(data, self.model.Header.Status);
            $scope.GetAccessConfig();
            $scope.refreshNotes(self.model.ReqID);
        });
    }

    $scope.openConsultEmail = function (email) {
        var tlink = window.location;
        var plainBodyText = "";
        var link = encodeURI(tlink);
        link = link.replace('#', '%23');
        plainBodyText += "Request%23 : " + self.model.ReqID + " - " + link + "%0D%0A";
        plainBodyText += "Request Type : " + self.model.Header.RequestType.Description + "%0D%0A";
        plainBodyText += "Proposed FYs : " + self.model.Header.ProposedFY.join(",") + "%0D%0A";
        plainBodyText += "Funding Class : " + self.model.Header.FundingClass.Description + "%0D%0A";
        plainBodyText += "Program : " + self.model.Header.FYDetails[0].Program.Code + " - " + self.model.Header.FYDetails[0].Program.Description + "%0D%0A";
        window.location = "mailto:" + ((email == null || email == undefined) ? '?' : email) + "&subject=RC%20Request%23%20" + self.model.ReqID + "&body=" + plainBodyText;
    }

    $scope.openStatusReport = function () {
        var tlink = '#/report/15';
        var link = tlink; //+ '&NoLayout=true&ReqID=' + self.model.ReqID + '&Parameters=ReqID';
        var Rand = (1 + Math.ceil(Math.random() * 100)).toString();
        var report = window.open(link, Rand, "title=WorkflowHistory,location=0,status=1,scrollbars=1,resizable=1,width=900,height=600");
        report.document.title = "Workflow History";
        return false;
    }

}];


var otrequestController_resolve = {
    model: ['$route', 'authFactory', 'alertService', 'otrequestFactory', function ($route, auth, alert, otFactory) {
        return auth.fetchToken().then(function (response) {
            return otFactory.GetOTRequest($route.current.params.reqid).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                return response.data;
            });
        });
    }]
}