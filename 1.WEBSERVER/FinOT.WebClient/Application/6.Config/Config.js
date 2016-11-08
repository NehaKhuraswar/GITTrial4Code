﻿'use strict';
var Config = ['$routeProvider', '$locationProvider', '$httpProvider', 'uiSelectConfig', 'blockUIConfig', 'informProvider', 'paginationTemplateProvider', function ($routeProvider, $locationProvider, $httpProvider, uiSelectConfig, blockUIConfig, informProvider, paginationTemplateProvider) {
    //config routes
    $routeProvider
        .when('/', {
            templateUrl: 'views/workqueue/index.html',
            controller: 'workqueueController',
            controllerAs: 'Ctrl',
            resolve: workqueueController_resolve
        })
        .when('/workqueue', {
            templateUrl: 'views/workqueue/index.html',
            controller: 'workqueueController',
            controllerAs: 'Ctrl',
            resolve: workqueueController_resolve
        })
        .when('/search/otcode', {
            templateUrl: 'views/search/otc/index.html',
            controller: 'searchController',
            controllerAs: 'Ctrl',
            resolve: searchOTCController_resolve
        })
        .when('/search/request', {
            templateUrl: 'views/search/request/index.html',
            controller: 'searchController',
            controllerAs: 'Ctrl',
            resolve: searchRequestController_resolve
        })
        .when('/request', {
            templateUrl: 'views/request/index.html',
            controller: otrequestController,
            controllerAs: 'Ctrl',
            resolve: otrequestController_resolve
        })
        .when('/request/:reqid', {
            templateUrl: 'views/request/index.html',
            controller: otrequestController,
            controllerAs: 'Ctrl',
            resolve: otrequestController_resolve
        })
        .when('/register', {
            templateUrl: 'views/account/CreateUser.html',
            controller: rapregisterController,
            controllerAs: 'Ctrl',
            resolve: rapregisterController_resolve
        })
        .when('/login', {
            templateUrl: 'views/account/Login.html',
            controller: raploginController,
            controllerAs: 'Ctrl',
            resolve: raploginController_resolve
        })
        .when('/dashboard', {
                templateUrl: 'views/account/Dashboard.html',
                controller: rapdashboardController,
                controllerAs: 'Ctrl',
                resolve: rapdashboardController_resolve
        })
        .when('/invitethirdparty', {
                templateUrl: 'views/account/InviteThirdParty.html',
                controller: rapinvitethirdpartyController,
                controllerAs: 'Ctrl',
                resolve: rapinvitethirdpartyController_resolve
        })
        .when('/report/:reportid', {
            templateUrl: 'views/report/index.html',
            controller: reportController,
            controllerAs: 'Ctrl',
            resolve: reportController_resolve
        })
        .when('/error', {
            templateUrl: 'views/shared/error.html',
        })
        .when('/noaccess', {
            templateUrl: 'views/shared/noaccess.html',
        })
        .when('/notoken', {
            templateUrl: 'views/shared/notoken.html',
        })
        .when('/noresource', {
            templateUrl: 'views/shared/404.html',
        })
        .otherwise({ redirectTo: '/' });

    //Enable cross domain calls
    $httpProvider.defaults.useXDomain = true;
    //Remove the header used to identify ajax call  that would prevent CORS from working
    delete $httpProvider.defaults.headers.common['X-Requested-With'];

    //config blockUI
    blockUIConfig.message = 'Processing...';
    blockUIConfig.delay = 100;
    blockUIConfig.autoBlock = false;

    //config inform popup message
    informProvider.defaults({ ttl: 5000, type: 'info', html: true });

    paginationTemplateProvider.setPath('Templates/dirPaginationNoPageNumber.tpl.html');

    uiSelectConfig.theme = 'bootstrap';
    uiSelectConfig.resetSearchInput = true;
    uiSelectConfig.appendToBody = true;
    uiSelectConfig.refreshDelay = 0;

}];