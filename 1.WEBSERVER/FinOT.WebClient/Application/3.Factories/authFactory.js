'use strict';
var authFactory = ['$q', '$location', 'ajaxService', function ($q, $location, ajax) {
    var authFactory = {};
    
   

    var _fetchToken = function () {
        var defer = $q.defer();
        if (_getToken() != null && _getToken() != "undefined") {
            defer.resolve();
            return defer.promise;
        }

        ajax.getToken(sessionStorage.getItem('username')).then(function (response) {
            if (response.data !== "") {
                sessionStorage.setItem('token', response.data.access_token);
                sessionStorage.setItem('username', response.data.Username);
                sessionStorage.setItem('roles', response.data.Roles);
                sessionStorage.setItem('expire', new Date(Date.now() + response.data.expires_in * 1000));
                defer.resolve();
            } else { defer.reject(); $location.path("/notoken") }
        }, function (response) {
            if (response.data.error != undefined && response.data.error == "NOACCESS") {
                $location.path("/noaccess");
            }
            defer.reject(response);
        });

        return defer.promise;
    }

    var _getToken = function () {
        return sessionStorage.getItem('token');
    }

    var _getTokenExpire = function () {
        return sessionStorage.getItem('expire');
    }

    var _getUsername = function () {
        return sessionStorage.getItem('username');
    }

    var _getUserDisplayName = function () {
        return sessionStorage.getItem('UserDisplayName');
    }

    var _getRoles = function () {
        return sessionStorage.getItem('roles');
    }

    var _isViewer = function () {
        var roles = _getRoles().split(',');
        return (roles.length == 1 && roles[0] == "2400")
    }

    var _isA6ConfigManager = function () {
        var roles = _getRoles().split(',');
        return (roles.indexOf("2405") > -1);
    }

    var _getDatetime = function () {
        return new Date().toLocaleString("en-US");
    }

    authFactory.fetchToken = _fetchToken;
    authFactory.getToken = _getToken;
    authFactory.getTokenExpire = _getTokenExpire;
    authFactory.getUsername = _getUsername;
    authFactory.getUserDisplayName = _getUserDisplayName;
    authFactory.getRoles = _getRoles;
    authFactory.isViewer = _isViewer;
    authFactory.isA6ConfigManager = _isA6ConfigManager;
    authFactory.getDatetime = _getDatetime;
   // authFactory.login = _Login;

    return authFactory;
}];