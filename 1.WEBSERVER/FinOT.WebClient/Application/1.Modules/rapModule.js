'use strict';
var rapModule = angular.module('rapModule', ['ngFileUpload'])
    
    .factory('otrequestFactory', otrequestFactory)
    .factory('rapcustFactory', rapcustFactory)
    .factory('rapGlobalFactory', function () {

          // public
         var CustID = 0;
         var CustomerDetails;
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
    .controller('otrequestController', otrequestController)
    .controller('otnotesController', otnotesController)
    .controller('raploginController', raploginController)
    .controller('rapregisterController', rapregisterController)
    .controller('rapdashboardController', rapdashboardController)
    .controller('rapinvitethirdpartyController', rapinvitethirdpartyController)
    .controller('rapFilePetitionController', rapFilePetitionController)
    .controller('otheaderController', otheaderController)
    .controller('otstaffController', otstaffController)
    .controller('otdocumentsController', otdocumentsController)
    .directive('otRequest', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model'
            },
            templateUrl: 'Views/Request/_Request.html',
            controller: 'otheaderController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('otHeader', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/Request/_Header.html',
            controller: 'otheaderController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('otRegister', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '=',
                model: '=model',
            },
            templateUrl: 'Views/Account/_CreateUser.html',
            controller: 'otregisterController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('otStaff', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '='
            },
            templateUrl: 'Views/Request/_Staff.html',
            controller: 'otstaffController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('otFunding', function () {
        return {
            restrict: 'E',
            scope: {
               reqid: '='
            },
            templateUrl: 'Views/Request/_Funding.html',
            controller: 'otfundingController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('otApproval', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '='
            },
            templateUrl: 'Views/Request/_Approval.html',
            controller: 'otapprovalController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('otNotes', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '='
            },
            templateUrl: 'Views/Request/_Notes.html',
            controller: 'otnotesController',
            controllerAs: 'Ctrl'
        };
    })
    .directive('otDocuments', function () {
        return {
            restrict: 'E',
            scope: {
                reqid: '='
            },
            templateUrl: 'Views/Request/_Documents.html',
            controller: 'otdocumentsController',
            controllerAs: 'Ctrl'
        }
    })
