'use strict';
var app = angular.module('OTS', ['ngRoute', 'blockUI', 'inform', 'ui.select', 'ngSanitize', 'ngAnimate', 'ui.bootstrap', 'angularUtils.directives.dirPagination', 'angularjs-dropdown-multiselect', 'angular.filter',  'rapModule'])
.config(Config)
.service('ajaxService', ajaxService)
.service('alertService', alertService)
.factory('authFactory', authFactory)
.factory('Page', pageFactory)
.factory('masterdataFactory', masterdataFactory)

//binding controllers to app module
.controller('mainController', mainController)

//binding global directives and filters
.directive('otsMaxinput', otsMaxinput)
.directive('numeric', numeric)
.directive('alphaNumeric', alphaNumeric)
.directive('inputPercent', inputPercent)
.directive('ngRedirectTo', ngRedirectTo)
.directive('dynamicHtml', dynamicHtml)
.directive('dropdownMultiselect', dropdownMultiselect)
.filter('datetime', datetime)
.filter('inArray', inArray)
.filter("allocationFilter", allocationFilter)

//avoid template cache
app.run(['$rootScope', '$templateCache', function ($rootScope, $templateCache) {
    //$rootScope.$on('$viewContentLoaded', function () { $templateCache.removeAll(); }); //doesn't work with UI Bootstrap
    $rootScope.$on('$routeChangeStart', function (event, next, current) {
        if (typeof (current) !== 'undefined') {
            $templateCache.remove(current.templateUrl);
        }
    });
}]);

//bootstrap angular
angular.bootstrap(document, ['OTS']);
