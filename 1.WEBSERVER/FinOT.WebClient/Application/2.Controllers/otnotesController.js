'use strict';
var otnotesController = ['$scope', '$modal', '$location', 'alertService', 'otrequestFactory', function ($scope, $modal, $location, alert, otFactory) {
    var self = this;
    self.model = [];

    self.AddNote = function (event) {
        if (event && event.stopPropagation) { event.stopPropagation(); } else if (window.event) { window.event.cancelBubble = true; }

        var modalInstance = $modal.open({
            animation: true,
            windowClass: 'large-modal-window',
            backdrop: 'static',
            templateUrl: 'Views/Request/_NotesAdd.html',
            controller: ['$scope', '$q', '$filter', '$modalInstance', 'alertService', 'masterdataFactory', 'otrequestFactory', 'model', 'reqid', function ($scope, $q, $filter, $modalInstance, alert, dataFactory, otFactory, model, reqid) {
                var self = this;
                self.model = angular.copy(model);


                $scope.submit = function () {

                    otFactory.SaveNotes(reqid, self.model).then(function (response) {
                            if (!alert.checkResponse(response)) {
                                return;
                            }
                            $modalInstance.close(response.data);
                        });
                    
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


        modalInstance.result.then(function () {
            _getNotes() //refresh notes section
        });
    }

    if (($scope.reqid != null || $scope.reqid != undefined) && !self.model.length) {
        _getNotes(); //get notes for the ReqID
    }

}];