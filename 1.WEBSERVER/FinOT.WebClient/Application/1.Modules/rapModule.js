﻿'use strict';
var rapModule = angular.module('rapModule', ['ngFileUpload'])
    
    
    .factory('rapcustFactory', rapcustFactory)
    .factory('rapGlobalFactory', function () {

          // public
         var CustID = 0;
         var CustomerDetails;
         var CaseDetails;
          // public
          return {

              get: function () {
                  return CustomerDetails;
              },

              set: function (val) {
                  CustomerDetails = val;
              }

          };
      })

    .factory('raploginFactory', raploginFactory)
    .factory('rapdashboardFactory', rapdashboardFactory)
    .factory('rapinvitethirdpartyFactory', rapinvitethirdpartyFactory)
    .factory('rapfilepetitionFactory', rapfilepetitionFactory)
    .factory('rapapplicationinfoFactory', rapapplicationinfoFactory)
    .factory('rapgroundsofpetitionFactory', rapgroundsofpetitionFactory)
    .factory('raplostservicesFactory', raplostservicesFactory)
    .factory('raprentalhistoryFactory', raprentalhistoryFactory)
    .factory('rapreviewFactory', rapreviewFactory)
    .factory('rapverificationFactory', rapverificationFactory)
    .factory('rapappellantsinfoFactory', rapappellantsinfoFactory)
    .factory('rapfileappealFactory', rapfileappealFactory)
    .factory('rapgroundsofappealFactory', rapgroundsofappealFactory)
    .factory('rapservingappealFactory', rapservingappealFactory)
    .factory('rapreviewappealFactory', rapreviewappealFactory)
    .controller('raploginController', raploginController)
    .controller('rapregisterController', rapregisterController)
    .controller('rapdashboardController', rapdashboardController)
    .controller('rapinvitethirdpartyController', rapinvitethirdpartyController)
    .controller('rapFilePetitionController', rapFilePetitionController)
    .controller('rapApplicationInfoController', rapApplicationInfoController)
    .controller('rapGroundsOfPetitionController', rapGroundsOfPetitionController)
    .controller('rapRentalHistoryController', rapRentalHistoryController)
    .controller('rapLostServicesController', rapLostServicesController)
    .controller('rapDocumentController', rapDocumentController)
    .controller('rapReviewController', rapReviewController)
    .controller('rapVerificationController', rapVerificationController)
    .controller('rapAppellantsInfoController', rapAppellantsInfoController)
    .controller('rapFileAppealController', rapFileAppealController)
    .controller('rapGroundsOfAppealController', rapGroundsOfAppealController)
    .controller('rapServingAppealController', rapServingAppealController)
    .controller('rapReviewAppealController', rapReviewAppealController)

   
    .directive('rapPetitiontype', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/FilePetition.html',
            controller: 'rapFilePetitionController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapApplicationinfo', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',

            },
            templateUrl: 'Views/FilePetition/ApplicationInfo.html',
            controller: 'rapApplicationInfoController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapGroundsofpetition', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/GroundsForPetition.html',
            controller: 'rapGroundsOfPetitionController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapRentalhistory', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/RentalHistory.html',
            controller: 'rapRentalHistoryController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapLostservices', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/LostServices.html',
            controller: 'rapLostServicesController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapDocument', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/Document.html',
            controller: 'rapDocumentController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapReview', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/Review.html',
            controller: 'rapReviewController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapVerification', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/Verification.html',
            controller: 'rapVerificationController',
            controllerAs: 'Ctrl'
        };
    })
    
   
