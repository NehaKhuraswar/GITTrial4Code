'use strict';
var otheaderController = ['$scope', '$modal', '$location', 'alertService', 'otrequestFactory', function ($scope, $modal, $location, alert, otFactory) {
    var self = this;
    self.model = [];

    self.openAddPopup = function (event) {
        if (event && event.stopPropagation) { event.stopPropagation(); } else if (window.event) { window.event.cancelBubble = true; }

        var modalInstance = $modal.open({
            animation: true,
            windowClass: 'large-modal-window',
            backdrop: 'static',
            templateUrl: 'Views/Request/_HeaderNew.html',
            controller: ['$scope', '$q', '$filter', '$modalInstance', 'alertService', 'masterdataFactory', 'otrequestFactory', 'model', 'reqid', function ($scope, $q, $filter, $modalInstance, alert, dataFactory, otFactory, model, reqid) {
                var self = this;
                self.model = angular.copy(model);

                

                self.validateModel = function () {
                    var arrayMsg = [];
                    if (self.model.RequestType.ID == 0)
                    {
                        arrayMsg.push("Please select the Request type")
                    }
                    if (self.model.CashOrComp == "" || self.model.CashOrComp == null)
                    {
                        arrayMsg.push("Please select the Cash or Comp")
                    }
                    if (arrayMsg.length > 0)
                    {
                        alert.Error(arrayMsg);
                        return false;
                    }
                    return true;
                }

               self.RequestSelectionChanged = function () {
                    //if (self.model.RequestType.ID == "1")
                    //    alert.Error("change!");
                }

                $scope.submit = function () {

                    if (self.validateModel()) {
                        otFactory.SaveOTRequest(reqid, self.model).then(function (response) {
                            if (!alert.checkResponse(response)) {
                                return;
                            }
                            $modalInstance.close(response.data);
                        });
                    }
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };
            }],
            controllerAs: 'Ctrl',
            resolve: {
                model: $scope.model,
                reqid: $scope.reqid
            }
        });

       

        modalInstance.result.then(function (data) {
            if ($scope.reqid == null) {
                $location.path('/request/' + data.ReqID);
            }
            else {
                angular.copy(data.Header, $scope.model);
                _bindSelectedFY();
                $scope.refreshAccess();
            }
        }, function () {
            if ($scope.reqid == null || $scope.reqid == undefined) { $location.path('/'); }
        });

}   


    if ($scope.reqid == null) {
        self.openAddPopup();
    }

}];