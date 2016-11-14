'use strict';
var rapinvitethirdpartyController = ['$scope', '$modal', 'alertService', 'rapinvitethirdpartyFactory', '$location', 'rapGlobalFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory) {
    var self = this;
    self.model = rapGlobalFactory.CustomerDetails;
    self.showEmailNotFound = false;
    self.showEmailFound = false;
    self.IsConsent = false;
    
    
   // self.AuthorizedUsers = [];
    var _getauthorizedusers = function () {
        return rapFactory.GetAuthorizedUsers(self.model.custID).then(function (response) {            
            self.authorizedusers = response.data;
        });
    }
    _getauthorizedusers();
 //   $q.all([_getAuthorizedUsers()]).then(function () {

        //if (self.model.RequestType.ID == 1) {
        //    angular.forEach(self.model.FYDetails, function (item) {
        //        if (item.DivisionList.length == 0) {
        //            _getDivision(item.FY, item);
        //        }
        //        if (item.Division != null && item.Division.Code != null) {
        //            if (item.BureauList.length == 0) {
        //                _getBureau(item.FY, item.Division.Code, item);
        //            }
        //        }
        //        if (item.Bureau != null && item.Bureau.Code != null) {
        //            if (item.ProgramList.length == 0) {
        //                _getProgram(item.FY, item.Bureau.Code, item);
        //            }
        //        }
        //    });
        //}

 //   })
    self.SearchInviteThirdPartyUser = function (email) {
        self.showEmailFound = false;
        self.showEmailNotFound = false;
        rapFactory.SearchInviteThirdPartyUser(email).then(function (response) {
            if (!alert.checkResponse(response)) {
                self.showEmailNotFound = true;
                return;
            }
            self.showEmailFound = true;
            self.model = response.data;

        });
    }
    self.Authorize = function (model) {
        if (self.IsConsent == true) {
            rapFactory.Authorize(rapGlobalFactory.CustomerDetails.custID, model).then(function (response) {


            });
        }
        else {
            alert.Error("Please consent to Authorize the third party")
        }
    }
    self.Invite = function (model) {
       rapFactory.Invite(model).then(function (response) {


            });
    }

    self.RemoveThirdParty = function (authorizedusers) {
        rapFactory.RemoveThirdParty(rapGlobalFactory.CustomerDetails.custID, authorizedusers[0]).then(function (response) {


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



