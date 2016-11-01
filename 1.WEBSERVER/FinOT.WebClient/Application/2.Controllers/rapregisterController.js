'use strict';
var rapregisterController = ['$scope', '$modal', 'alertService',  function ($scope, $modal, alert) {
    var self = this;
    self.model = [];
    self.Register = function () {
        var plainBodyText = "";
    }

}];
var rapregisterController_resolve = {
    model: ['$route', 'authFactory', 'alertService',  function ($route, auth, alert) {
        //return auth.fetchToken().then(function (response) {
        //    return otFactory.GetOTRequest($route.current.params.reqid).then(function (response) {
        //        if (!alert.checkResponse(response)) { return; }
        //        return response.data;
        //    });
        //});
    }]
}