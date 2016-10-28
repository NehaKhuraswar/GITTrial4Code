'use strict';
var searchModule = angular.module('searchModule', [])
    .factory('searchFactory', searchFactory)
    .controller('searchController', searchController)