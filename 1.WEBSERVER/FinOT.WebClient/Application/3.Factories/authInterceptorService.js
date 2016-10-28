'use strict';
var authInterceptorServiceFactory = ['$q', '$location', function ($q, $location) {
    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};
        //console.log(sessionStorage.getItem('token'));
        if (sessionStorage.getItem('token') != null) {
            config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('token');
        }
        console.log(config.headers);
        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/error');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}];

var BearerAuthInterceptor = ['$window', '$q', function ($window, $q) {
    return {
        request: function (config) {
            config.headers = config.headers || {};
            if (sessionStorage.getItem('token')) {
                // may also use sessionStorage
                config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('token');
                //config.headers.common['X-Requested-With'] = 'XMLHttpRequest';
                //console.log(sessionStorage.getItem('token'));
            }
            //console.log('asd');
            return config || $q.when(config);
        },
        response: function (response) {
            if (response.status === 401) {
                console.log(response);
                //  Redirect user to login page / signup Page.
            }
            return response || $q.when(response);
        }
    };
}];


var BearerAuthInterceptor = ['$q', function ($q) {
    return {
        request: function (config) {
            config.headers = config.headers || {};
            if (sessionStorage.getItem('token')) {
                // you may use sessionStorage or $window.localStorage
                config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('token');
            }
            return config || $q.when(config);
        },
        response: function (response) {
            if (response.status === 401) {
                //  Redirect user to login page / signup Page.
            }
            return response || $q.when(response);
        }
    };
}];