'use strict';
var ajaxService = ['$http', '$location', '$log', function ($http, $location, $log) {
    var _base;
    if (_base == null || _base == undefined) {
        _base = sessionStorage.getItem('apibaseurl');
    }

    this.getToken = function (username,password) {
        return $http({
            method: 'POST',
            url: _base + "token",
            data: "grant_type=password&username=" + username + "&password=" + password,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
    }

    this.Get = function (route, data) {
        //var _url = _base + route;
        var _url = '/' + route;
        return $http({
            method: 'GET',
            url: _url,
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('token') }
        }).then(function (response) {
            $log.info(response.data);
            return response.data;
        },
        function(reason)
        {
            $log.info(reason.status + ' | ' + reason.statusTex);
        }).catch(function (response) {
            $log.info(response.status + ' | ' + reason.statusTex);
            return response.data;
        })
    }
    
    this.Post = function (data, route) {
        //var _url = _base + route;
        var _url = '/' + route;
        return $http({
            method: 'POST',
            url: _url,
            data: data,
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('token'), 'enctype' : 'multipart/form-data' }
          
        }).then(function (response) {
            $log.info(response.data);
            return response.data;
        },
        function (reason)
        {
         $log.info(reason.status + ' | ' + reason.statusTex);
        }).catch(function (response) {
            if (response.status == 401) { $location.path('/noaccess'); }
            return response.data;
        });
    }

    this.GetResponse = function (route) {
        var _url = _base + route;
        return $http({
            method: 'GET',
            url: _url,
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('token') },
            responseType:'arraybuffer',
        });
    }

    this.PostResponse = function (data, route) {
        var _url = _base + route;
        return $http({
            method: 'POST',
            url: _url,
            data: data,
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('token') },
            responseType: 'arraybuffer',
        });
    }
}]