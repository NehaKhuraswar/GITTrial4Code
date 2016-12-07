'use strict';
var rapregisterController = ['$scope', '$modal', 'alertService', 'rapcustFactory', function ($scope, $modal, alert, rapFactory) {
    var self = this;
    self.model = [];
    //var checkPassword = function (str) {
    //    var re = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$/;
    //    return re.test(str);
    //}
    self.Register = function (model) {
        //if (!checkPassword(model.Password))
        //{
        //    return;
        //}
        rapFactory.SaveCustomer(null, model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
        });
    }    

}];
var rapregisterController_resolve = {
    model: ['$route', 'alertService', 'rapcustFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //     //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}