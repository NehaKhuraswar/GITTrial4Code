'use strict';
var reportModule = angular.module('reportModule', [])
    .factory('reportFactory', reportFactory)
    .controller('reportController', reportController)