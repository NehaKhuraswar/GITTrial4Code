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
        .when('/editcustomerinformation', {
            templateUrl: 'views/account/CreateUser.html',
            controller: rapregisterController,
            controllerAs: 'Ctrl',
            resolve: rapregisterController_resolve
        })
        .when('/collaborator', {
            templateUrl: 'views/account/Collaborator.html',
            controller: rapcollaboratorController,
            controllerAs: 'Ctrl',
            resolve: rapcollaboratorController_resolve
        })
        .when('/forgotpassword', {
            templateUrl: 'views/account/ForgetPassword.html',
            controller: rapForgetPwdController,
            controllerAs: 'Ctrl',
            resolve: rapForgetPwdController_resolve
        })
        .when('/changepassword', {
            templateUrl: 'views/account/ChangePassword.html',
            controller: rapChangePasswordController,
            controllerAs: 'Ctrl',
            resolve: rapChangePasswordController_resolve
        })
        .when('/resendpin', {
            templateUrl: 'views/account/ResendPIN.html',
            controller: rapResendPinController,
            controllerAs: 'Ctrl',
            resolve: rapResendPinController_resolve
        })
        .when('/loginURL', {
            templateUrl: 'views/account/LoginURL.html',
            controller: raploginURLController,
            controllerAs: 'Ctrl',
            resolve: raploginURLController_resolve
        })
        .when('/loginPublic', {
            templateUrl: 'views/account/Login.html',
            controller: raploginController,
            controllerAs: 'Ctrl',
            resolve: raploginController_resolve
        })
        .when('/loginCity', {
            templateUrl: 'views/account/Login.html',
            controller: raploginCityUserController,
            controllerAs: 'Ctrl',
            resolve: raploginCityUserController_resolve
        })
        .when('/publicdashboard', {
                templateUrl: 'views/account/PublicDashboard.html',
                controller: rapdashboardController,
                controllerAs: 'Ctrl',
                resolve: rapdashboardController_resolve
        })
        .when('/staffdashboard', {
                    templateUrl: 'views/account/StaffDashboard.html',
                    controller: rapstaffdashboardController,
                    controllerAs: 'Ctrl',
                    resolve: rapstaffdashboardController_resolve
        })
        .when('/selectedcase', {
            templateUrl: 'views/account/SelectedCase.html',
            controller: rapSelectedCaseController,
            controllerAs: 'Ctrl',
            resolve: rapSelectedCaseController_resolve
        })
        .when('/additionaldocuments', {
            templateUrl: 'views/account/AdditionalCaseDocumentation.html',
            controller: rapAdditionalCaseDocumentController,
            controllerAs: 'Ctrl',
            resolve: rapAdditionalCaseDocumentController_resolve
        })
         .when('/sendemailnotification', {
             templateUrl: 'views/account/SendEmailNotification.html',
             controller: rapCustomEmailController,
             controllerAs: 'Ctrl',
             resolve: rapCustomEmailController_resolve
         })
        .when('/admindashboard', {
                    templateUrl: 'views/account/AdminDashboard.html',
                    controller: rapadmindashboardController,
                    controllerAs: 'Ctrl',
                    resolve: rapadmindashboardController_resolve
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
        .when('/newCaseStatus', {
            templateUrl: 'views/account/NewCaseStatus.html',
            controller: rapNewCaseStatusController,
            controllerAs: 'Ctrl',
            resolve: rapNewCaseStatusController_resolve
        })
        .when('/YourRepresentative', {
            templateUrl: 'views/account/YourRepresentative.html',
            controller: rapRepresentativeController,
            controllerAs: 'Ctrl',
            resolve: rapRepresentativeController_resolve
        })
        .when('/AddNewRepresentative', {
            templateUrl: 'views/account/AddNewRepresentative.html',
            controller: rapNewRepresentativeController,
            controllerAs: 'Ctrl',
            resolve: rapNewRepresentativeController_resolve
        })
        .when('/accountSearch', {
            templateUrl: 'views/account/AccountSearch.html',
            controller: rapSearchAccountController,
            controllerAs: 'Ctrl',
            resolve: rapSearchAccountController_resolve
        })
        .when('/createCityUserAccount', {
            templateUrl: 'views/account/CreateCityAccount.html',
            controller: rapCityUserAcctController,
            controllerAs: 'Ctrl',
            resolve: rapCityUserAcctController_resolve
        })
        .when('/ViewPetition', {
            templateUrl: 'views/account/ViewPetition.html',
            controller: rapViewPetitionController,
            controllerAs: 'Ctrl',
            resolve: rapViewPetitionController_resolve
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
        //.when('/review', {
        //    templateUrl: 'views/filepetition/Review.html',
        //    controller: rapReviewController,
        //    controllerAs: 'Ctrl',
        //    resolve: rapReviewController_resolve
        //})
        //.when('/verification', {
        //    templateUrl: 'views/filepetition/Verification.html',
        //    controller: rapVerificationController,
        //    controllerAs: 'Ctrl',
        //    resolve: rapVerificationController_resolve
        //})
        .when('/fileappeal', {
            templateUrl: 'views/fileAppeal/Index.html',
            controller: rapAppealMainController,
            controllerAs: 'Ctrl',
            resolve: rapAppealMainController_resolve
        })
        .when('/filetenantresponse', {
            templateUrl: 'views/TenantResponse/Index.html',
            controller: rapTRMainController,
            controllerAs: 'Ctrl',
            resolve: rapTRMainController_resolve
        })
        .when('/appellantsinfo', {
            templateUrl: 'views/fileAppeal/AppellantsInfo.html',
            controller: rapAppellantsInfoController,
            controllerAs: 'Ctrl',
            resolve: rapAppellantsInfoController_resolve
        })
        //.when('/groundsforappeal', {
        //    templateUrl: 'views/fileAppeal/GroundsForAppeal.html',
        //    controller: rapGroundsOfAppealController,
        //    controllerAs: 'Ctrl',
        //    resolve: rapGroundsOfAppealController_resolve
        //})
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
        .when('/ownerresponse', {
              templateUrl: 'views/OwnerResponse/Index.html',
              controller: rapOResponseMainController,
              controllerAs: 'Ctrl',
              resolve: rapOResponseMainController_resolve
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

   // $locationProvider.html5Mode(true);
    //Enable cross domain calls
    $httpProvider.defaults.useXDomain = true;
    //Remove the header used to identify ajax call  that would prevent CORS from working
    delete $httpProvider.defaults.headers.common['X-Requested-With'];

    //config blockUI
    blockUIConfig.message = '';
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