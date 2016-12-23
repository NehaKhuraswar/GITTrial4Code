﻿'use strict';
var rapModule = angular.module('rapModule', ['ngFileUpload'])
    
    
    .factory('rapcustFactory', rapcustFactory)
    .factory('rapcollaboratorFactory', rapcollaboratorFactory)
    .factory('rapGlobalFactory', function () {

          // public
         var CustID = 0;
         var CustomerDetails;
         var CaseDetails;
         var CityUser;
         var IsEdit = false;

         
        
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

    .factory('raploginCityUserFactory', raploginCityUserFactory)
    .factory('raploginFactory', raploginFactory)
    .factory('rapchangepwdFactory', rapchangepwdFactory)
    .factory('rapresendpinFactory', rapresendpinFactory)
    .factory('rapforgetPasswordFactory', rapforgetPasswordFactory)
    .factory('rapdashboardFactory', rapdashboardFactory)
    .factory('rapstaffdashboardFactory', rapstaffdashboardFactory)
    .factory('rapadmindashboardFactory', rapadmindashboardFactory)
    .factory('rapnewcasestatusFactory', rapnewcasestatusFactory)
    .factory('rapSearchAccountFactory', rapSearchAccountFactory)
    .factory('rapinvitethirdpartyFactory', rapinvitethirdpartyFactory)
    .factory('rapfilepetitionFactory', rapfilepetitionFactory)
    .factory('rapapplicationinfoFactory', rapapplicationinfoFactory)
    .factory('rapgroundsofpetitionFactory', rapgroundsofpetitionFactory)
    .factory('raplostservicesFactory', raplostservicesFactory)
    .factory('raprentalhistoryFactory', raprentalhistoryFactory)
    .factory('rapreviewFactory', rapreviewFactory)
    .factory('rapverificationFactory', rapverificationFactory)
    .factory('rapappealtypeFactory', rapappealtypeFactory)
    .factory('rapappellantsinfoFactory', rapappellantsinfoFactory)
    .factory('rapfileappealFactory', rapfileappealFactory)
    .factory('rapgroundsofappealFactory', rapgroundsofappealFactory)
    .factory('rapservingappealFactory', rapservingappealFactory)
    .factory('rapreviewappealFactory', rapreviewappealFactory)
    .factory('rapcityuserregisterFactory', rapcityuserregisterFactory)
    .factory('rapOwnerApplicantInfoFactory', rapOwnerApplicantInfoFactory)
    .factory('rapOwnerJustificationFactory', rapOwnerJustificationFactory)
    .factory('rapOwnerRentalPropertyFactory', rapOwnerRentalPropertyFactory)
    .factory('rapOwnerRentalHistoryFactory', rapOwnerRentalHistoryFactory)
    .factory('rapOwnerVerificationFactory', rapOwnerVerificationFactory)
    .controller('raploginURLController', raploginURLController)
    .controller('raploginController', raploginController)
    .controller('raploginCityUserController', raploginCityUserController)
    .controller('rapregisterController', rapregisterController)
    .controller('rapcollaboratorController', rapcollaboratorController)
    .controller('rapChangePasswordController', rapChangePasswordController)
    .controller('rapResendPinController', rapResendPinController)
    .controller('rapForgetPwdController', rapForgetPwdController)
    .controller('rapdashboardController', rapdashboardController)
    .controller('rapstaffdashboardController', rapstaffdashboardController)
    .controller('rapadmindashboardController', rapadmindashboardController)
    .controller('rapNewCaseStatusController', rapNewCaseStatusController)
    .controller('rapAppealConfirmationController', rapAppealConfirmationController)
    .controller('rapSearchAccountController', rapSearchAccountController)
    .controller('rapCityUserAcctController', rapCityUserAcctController)
    .controller('rapinvitethirdpartyController', rapinvitethirdpartyController)
    .controller('rapFilePetitionController', rapFilePetitionController)
    .controller('rapApplicationInfoController', rapApplicationInfoController)
    .controller('rapGroundsOfPetitionController', rapGroundsOfPetitionController)
    .controller('rapRentalHistoryController', rapRentalHistoryController)
    .controller('rapLostServicesController', rapLostServicesController)
    .controller('rapDocumentController', rapDocumentController)
    .controller('rapReviewController', rapReviewController)
    .controller('rapVerificationController', rapVerificationController)
    .controller('rapAppealTypeController', rapAppealTypeController)
    .controller('rapAppealImpInfoController', rapAppealImpInfoController)
    .controller('rapAppellantsInfoController', rapAppellantsInfoController)
    .controller('rapFileAppealController', rapFileAppealController)
    .controller('rapAppealDocumentController', rapAppealDocumentController)
    .controller('rapGroundsOfAppealController', rapGroundsOfAppealController)
    .controller('rapServingAppealController', rapServingAppealController)
    .controller('rapReviewAppealController', rapReviewAppealController)
    .controller('rapAppealMainController', rapAppealMainController)
    .controller('rapImpInfoController', rapImpInfoController)
    .controller('rapOwnerImpInfoController', rapOwnerImpInfoController)
    .controller('rapOwnerApplicantInfoController', rapOwnerApplicantInfoController)
    .controller('rapOwnerJustificationController', rapOwnerJustificationController)
    .controller('rapOwnerRentalPropertyController', rapOwnerRentalPropertyController)
    .controller('rapOwnerRentalHistoryController', rapOwnerRentalHistoryController)
    .controller('rapOwnerDocumentsController', rapOwnerDocumentsController)
    .controller('rapOwnerReviewController', rapOwnerReviewController)
    .controller('rapOwnerVerificationController', rapOwnerVerificationController)
    .directive('yearDrop',function(){
        function getYears(offset, range) {
            var range = range / 2;
            var currentYear = new Date().getFullYear();
            var years = [];
            for (var i = range; i > 0 ; i--) {
                
                years.push(currentYear + offset - i);
            }
            for (var i = 0; i < range + 1; i++){
                years.push(currentYear + offset + i);
            }
            return years;
        }
        return {
            link: function (scope, element, attrs) {
                scope.years = getYears(+attrs.offset, +attrs.range);
                scope.selected = scope.years[0];
            },
            template: '<select class="class="custom-select" ng-model="Ctrl.selected" ng-options="y for y in years"></select>'
        };
    })
   
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
    .directive('rapImpinfo', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FilePetition/ImportantInformation.html',
            controller: 'rapImpInfoController',
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
    .directive('rapAppealtype', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/AppealType.html',
            controller: 'rapAppealTypeController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapAppellantinfo', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/AppellantsInfo.html',
            controller: 'rapAppellantsInfoController',
            controllerAs: 'Ctrl'
        };
    })

    .directive('rapImpinfoappeal', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/ImportantInfo.html',
            controller: 'rapAppealImpInfoController',
            controllerAs: 'Ctrl'
        };
    })

    .directive('rapGrounds', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/GroundsForAppeal.html',
            controller: 'rapGroundsOfAppealController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapAdddocsappeal', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/Document.html',
            controller: 'rapAppealDocumentController',
            controllerAs: 'Ctrl'
        };
    })

    .directive('rapServingappeal', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/ServingAppeal.html',
            controller: 'rapServingAppealController',
            controllerAs: 'Ctrl'
        };
    })  
   
    .directive('rapReviewappeal', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/Review.html',
            controller: 'rapReviewAppealController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('rapConfirmationappeal', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/FileAppeal/ConfirmationPage.html',
            controller: 'rapAppealConfirmationController',
            controllerAs: 'Ctrl'
        };
    })
  .directive('rapOwnerimpinfo', function () {
      return {
          restrict: 'E',
          scope: {
              reqid: '=',
              model: '=model',
          },
          templateUrl: 'Views/FilePetition/Owner/ImportantInformation.html',
          controller: 'rapOwnerImpInfoController',
          controllerAs: 'Ctrl'
      };
  })
  .directive('rapOwnerapplicantinfo', function () {
      return {
          restrict: 'E',
          scope: {
              reqid: '=',
              model: '=model',
          },
          templateUrl: 'Views/FilePetition/Owner/ApplicantInformation.html',
          controller: 'rapOwnerApplicantInfoController',
          controllerAs: 'Ctrl'
      };
  })
  .directive('rapOwnerjustification', function () {
      return {
          restrict: 'E',
          scope: {
              reqid: '=',
              model: '=model',
          },
          templateUrl: 'Views/FilePetition/Owner/Justification.html',
          controller: 'rapOwnerJustificationController',
          controllerAs: 'Ctrl'
      };
  })
 .directive('rapOwnerrentalproperty', function () {
     return {
         restrict: 'E',
         scope: {
             reqid: '=',
             model: '=model',
         },
         templateUrl: 'Views/FilePetition/Owner/RentalProperty.html',
         controller: 'rapOwnerRentalPropertyController',
         controllerAs: 'Ctrl'
     };
 })
.directive('rapOwnerrentalhistory', function () {
    return {
        restrict: 'E',
        scope: {
            reqid: '=',
            model: '=model',
        },
        templateUrl: 'Views/FilePetition/Owner/RentalHistory.html',
        controller: 'rapOwnerRentalHistoryController',
        controllerAs: 'Ctrl'
    };
})
.directive('rapOwneradditionaldocuments', function () {
    return {
        restrict: 'E',
        scope: {
            reqid: '=',
            model: '=model',
        },
        templateUrl: 'Views/FilePetition/Owner/AdditionalDocuments.html',
        controller: 'rapOwnerDocumentsController',
        controllerAs: 'Ctrl'
    };
})
.directive('rapOwnerreview', function () {
    return {
        restrict: 'E',
        scope: {
            reqid: '=',
            model: '=model',
        },
        templateUrl: 'Views/FilePetition/Owner/Review.html',
        controller: 'rapOwnerReviewController',
        controllerAs: 'Ctrl'
    };
})
.directive('rapOwnerverification', function () {
    return {
        restrict: 'E',
        scope: {
            reqid: '=',
            model: '=model',
        },
        templateUrl: 'Views/FilePetition/Owner/Verification.html',
        controller: 'rapOwnerVerificationController',
        controllerAs: 'Ctrl'
    };
})