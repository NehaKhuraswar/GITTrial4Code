'use strict';
var Config = ['$routeProvider', '$locationProvider', '$httpProvider', 'uiSelectConfig', 'blockUIConfig', 'informProvider', 'paginationTemplateProvider', function ($routeProvider, $locationProvider, $httpProvider, uiSelectConfig, blockUIConfig, informProvider, paginationTemplateProvider) {
    //config routes
    $routeProvider
        
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
        .when('/filePetition', {
            templateUrl: 'views/filepetition/Index.html',
            controller: rapPetitionMainController,
            controllerAs: 'Ctrl',
            resolve: rapPetitionMainController_resolve
        })
        .when('/applicationinfo', {
            templateUrl: 'views/filepetition/ApplicationInfo.html',
            controller: rapApplicationInfoController,
            controllerAs: 'Ctrl',
            resolve: rapApplicationInfoController_resolve
        })
        .when('/groundsforpetition', {
            templateUrl: 'views/filepetition/GroundsForPetition.html',
            controller: rapGroundsOfPetitionController,
            controllerAs: 'Ctrl',
            resolve: rapGroundsOfPetitionController_resolve
        })
        .when('/rentalhistory', {
            templateUrl: 'views/filepetition/RentalHistory.html',
            controller: rapRentalHistoryController,
            controllerAs: 'Ctrl',
            resolve: rapRentalHistoryController_resolve
        })
        .when('/lostservices', {
            templateUrl: 'views/filepetition/LostServices.html',
            controller: rapLostServicesController,
            controllerAs: 'Ctrl',
            resolve: rapLostServicesController_resolve
        })
       .when('/document', {
              templateUrl: 'views/filepetition/Document.html',
              controller: rapDocumentController,
              controllerAs: 'Ctrl',
              resolve: rapDocumentController_resolve
          })
        .when('/review', {
            templateUrl: 'views/filepetition/Review.html',
            controller: rapReviewController,
            controllerAs: 'Ctrl',
            resolve: rapReviewController_resolve
        })
        .when('/verification', {
            templateUrl: 'views/filepetition/Verification.html',
            controller: rapVerificationController,
            controllerAs: 'Ctrl',
            resolve: rapVerificationController_resolve
        })
        .when('/fileappeal', {
            templateUrl: 'views/fileAppeal/FileAppeal.html',
            controller: rapFileAppealController,
            controllerAs: 'Ctrl',
            resolve: rapFileAppealController_resolve
        })
        .when('/appellantsinfo', {
            templateUrl: 'views/fileAppeal/AppellantsInfo.html',
            controller: rapAppellantsInfoController,
            controllerAs: 'Ctrl',
            resolve: rapAppellantsInfoController_resolve
        })
        .when('/groundsforappeal', {
            templateUrl: 'views/fileAppeal/GroundsForAppeal.html',
            controller: rapGroundsOfAppealController,
            controllerAs: 'Ctrl',
            resolve: rapGroundsOfAppealController_resolve
        })
        .when('/reviewappeal', {
            templateUrl: 'views/fileAppeal/Review.html',
            controller: rapReviewAppealController,
            controllerAs: 'Ctrl',
            resolve: rapReviewAppealController_resolve
        })
        .when('/servingappeal', {
            templateUrl: 'views/fileAppeal/ServingAppeal.html',
            controller: rapServingAppealController,
            controllerAs: 'Ctrl',
            resolve: rapServingAppealController_resolve
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