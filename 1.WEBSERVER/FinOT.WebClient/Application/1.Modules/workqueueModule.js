'use strict';
var workqueueModule = angular.module('workqueueModule', [])
    .factory('workqueueFactory', workqueueFactory)
    .controller('workqueueController', workqueueController)