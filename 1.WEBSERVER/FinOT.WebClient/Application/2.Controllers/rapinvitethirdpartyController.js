'use strict';
var rapinvitethirdpartyController = ['$scope', '$modal', 'alertService', 'rapinvitethirdpartyFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = [];
    var emailSearch = "";
    var thirdpartyDetails;
    // rapFactory.param.set("temp");
    self.SearchInviteThirdPartyUser = function (email) {
        
        rapFactory.Login(email).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
 
            //  angular.copy(response.data, MyService.value);
            //rapGlobalFactory.CustomerDetails = response.data;
            //$scope.model = response.data;
            //$location.path("/dashboard");

        });
    }

}];
var rapinvitethirdpartyController_resolve = {
    model: ['$route', 'alertService', 'raploginFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        return rapFactory.GetCustomer(null).then(function (response) {
            //   if (!alert.checkResponse(response)) { return; }
            //    return response.data;
            //});
        });
    }]
}

   // var emailSearch = "";
   // var thirdpartyDetails;
   //// self.model = rapGlobalFactory.CustomerDetails;
   // self.SearchInviteThirdPartyUser = function (email) {
   //     rapFactory.SearchInviteThirdPartyUser(email).then(function (response) {
   //         if (!alert.checkResponse(response)) {
   //                             return;
   //         }
   //         thirdpartyDetails = response.data;
   //     });
   // }

