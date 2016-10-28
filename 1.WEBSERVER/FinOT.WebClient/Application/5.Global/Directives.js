var otsMaxinput = function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            var maxlength = Number(attrs.rmsMaxinput);
            function fromUser(text) {
                if (text.length > maxlength) {
                    var transformedInput = text.substring(0, maxlength);
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                    return transformedInput;
                }
                return text;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
};

var numeric = function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                var transformedInput = text.replace(/[^0-9]/g, '');
                if (transformedInput !== text) {
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                }
                return transformedInput;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
};

var alphaNumeric = function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                var transformedInput = text.replace(/[^a-zA-Z0-9]/g, '');
                if (transformedInput !== text) {
                    ngModelCtrl.$setViewValue(transformedInput);
                    ngModelCtrl.$render();
                }
                return transformedInput;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
};

var inputPercent = function () {
    return {
        restrict: 'EA',
        template: '<div class="input-group input-group-sm"><input class="form-control" name="{{inputName}}" ng-model="inputValue" ng-disabled="inputDisable" ng-required="inputRequired" /><span class="input-group-addon">%</span></div>',
        scope: {
            inputValue: '=',
            inputName: '=',
            inputDisable: '=',
            inputRequired: '='
        },
        link: function (scope) {
            scope.$watch('inputValue', function (newValue, oldValue) {
                if (newValue == undefined) { return; }
                var re = new RegExp(/[\-,\s]+/g); //regex: '-' --> \- : ',' --> , : ' ' --> \s : g --> all occurances
                if (re.test(String(newValue))) {
                    scope.inputValue = String(newValue).replace(/[\-,\s]+/g, '');
                }
                else {
                    var index_dot,
                        arr = String(newValue).split("");
                    if (arr.length === 0) return;
                    if (arr.length === 1 && (arr[0] == '-' || arr[0] === '.')) return;
                    if (arr.length === 2 && newValue === '-.') return;
                    if (isNaN(newValue) || ((index_dot = String(newValue).indexOf('.')) != -1 && String(newValue).length - index_dot > 3) || (newValue > 100)) {
                        scope.inputValue = oldValue;
                    }
                }
            });
        }
    };
};

var ngRedirectTo = ['$window', '$location', function ($window, $location) {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {
            element.bind('click', function (event) {
                //$window.location.href = attributes.ngRedirectTo;
                $location.path(attributes.ngRedirectTo);
                scope.$apply()
            });
        }
    };
}];

var dynamicHtml = ['$compile', '$parse', function ($compile, $parse) {
    return {
        restrict: 'EA',
        link: function (scope, element, attr) {
            scope.$watch(attr.content, function () {
                element.html($parse(attr.content)(scope));
                $compile(element.contents())(scope);
            }, true);
        }
    };
}];