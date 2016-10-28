var dropdownMultiselect = ['$document', function ($document) {
    return {
        restrict: 'E',
        scope: {
            model: '=',
            options: '=',
            display: '@',
            events: '=',
            showSelect: '=',
            pullRight: '='
        },
        templateUrl: 'Templates/dirDropdownMultiselect.html',
        link: function ($scope, $element, $attrs) {
            var $dropdownTrigger = $element.children()[0];
            if ($scope.showSelect == null || $scope.showSelect == undefined)
                $scope.showSelect = false;

            $scope.extEvents = {
                onCheck: angular.noop,
                onUncheck: angular.noop
            };
            angular.extend($scope.extEvents, $scope.events || []);
            
            $scope.openDropdown = function () {
                $scope.open = !$scope.open;
            };

            $document.on('click', function (e) {
                var target = e.target.parentElement;
                var parentFound = false;

                while (angular.isDefined(target) && target !== null && !parentFound) {
                    if (target.className.split(' ').contains('multiselect-dropdown-parent') && !parentFound) {
                        if (target === $dropdownTrigger) {
                            parentFound = true;
                        }
                    }
                    target = target.parentElement;
                }

                if (!parentFound) {
                    $scope.$apply(function () {
                        $scope.open = false;
                    });
                }
            });

            $scope.selectAll = function () {
                $scope.model = [];
                angular.forEach($scope.options, function (item, index) {
                    $scope.model.push(item);
                });
            };

            $scope.deselectAll = function () {
                $scope.model = [];
            };

            $scope.selectItem = function (option) {
                var intIndex = -1;
                angular.forEach($scope.model, function (item, index) {
                    if (item == option) {
                        intIndex = index;
                    }
                });

                if (intIndex >= 0) {
                    $scope.model.splice(intIndex, 1);
                    $scope.extEvents.onUncheck(option);
                }
                else {
                    $scope.model.push(option);
                    $scope.extEvents.onCheck(option);
                }
            };

            $scope.getUlClassName = function () {
                var varClassName = 'dropdown-menu';
                if ($scope.pullRight == true) { varClassName += ' pull-right'; }
                return (varClassName);
            };

            $scope.getLiClassName = function (option) {
                var varClassName = 'glyphicon glyphicon-remove';
                angular.forEach($scope.model, function (item, index) {
                    if (item == option) {
                        varClassName = 'glyphicon glyphicon-ok';
                    }
                });
                return (varClassName);
            };

            $scope.formatSelectedModel = function () {
                if ($scope.model.length == 0) { return "Select"; }
                else {
                    if ($scope.display == undefined) {
                        return $scope.model.sort().join();
                    }
                    else {
                        return "Selected " + $scope.model.length + " of " + $scope.options.length;
                    }
                }
            }
        }
    };
}];