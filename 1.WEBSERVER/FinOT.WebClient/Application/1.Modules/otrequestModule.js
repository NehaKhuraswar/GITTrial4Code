'use strict';
var otrequestModule = angular.module('otrequestModule', ['ngFileUpload'])
    .factory('otrequestFactory', otrequestFactory)
    .factory('rapcustFactory', rapcustFactory)
    .factory('raploginFactory', raploginFactory)
    .controller('otrequestController', otrequestController)
    .controller('otnotesController', otnotesController)
    .controller('raploginController', raploginController)
    .controller('rapregisterController', rapregisterController)
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
