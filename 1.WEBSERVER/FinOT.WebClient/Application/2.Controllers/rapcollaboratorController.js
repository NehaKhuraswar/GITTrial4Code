'use strict';
var rapcollaboratorController = ['$scope', '$modal', 'alertService', 'rapcollaboratorFactory', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $modal, alert, rapFactory, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.model = angular.copy(rapGlobalFactory.CustomerDetails);
    self.showEmailNotFound = false;
    self.showEmailFound = false;
    self.IsConsent = false;
    self.authorizedusers;
    self.Email;
    self.Cases;
    
    self.model.User.FirstName = '';
    self.model.User.LastName = '';
    self.model.email = '';
    //var _GetCustomerModel = function () {
    //    return masterFactory.GetCustomer(null).then(function (response) {
    //        if (!alert.checkResponse(response)) { return; }
    //        self.model = response.data;
    //    });
        
    //}
    //_GetCustomerModel();
    
   // self.AuthorizedUsers = [];
    var _getauthorizedusers = function () {
        return rapFactory.GetAuthorizedUsers(self.model.custID).then(function (response) {            
            self.authorizedusers = response.data;
        });
    }
    _getauthorizedusers();

    var __GetCasesForCustomer = function () {
        return masterFactory.GetCasesForCustomer(self.model.custID).then(function (response) {
            self.Cases = response.data;
        });
    }
    __GetCasesForCustomer();

    
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
    self.SearchInviteCollaborator = function (email) {
        self.showEmailFound = false;
        self.showEmailNotFound = false;
        self.model.email = email;
        rapFactory.SearchInviteCollaborator(self.model).then(function (response) {
            if (!alert.checkResponse(response)) {
                self.showEmailNotFound = true;
                return;
            }
            self.showEmailFound = true;
            self.model = response.data;

        });
    }
    self.Authorize = function () {
        if (self.IsConsent == true) {
            var i;
            var caseItem;
            for( i = 0; i < self.Cases.length; i++)
            {
                caseItem = self.Cases[i];
                if (caseItem.Selected == true) {
                    self.authorizedusers.C_ID.push(caseItem.C_ID);
                }
            }
            self.authorizedusers.custID = rapGlobalFactory.CustomerDetails.custID;
            self.authorizedusers.collaboratorCustID = self.model.custID;

            //self.Cases.foreach(function (caseItem) {
            //    if (caseItem.Selected == true) {
            //        self.authorizedusers.CaseID.push(self.Cases.CaseID);
            //    }
            //});
           
           // self.authorizedusers = Ctrl
            rapFactory.Authorize(self.authorizedusers).then(function (response) {


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
var rapcollaboratorController_resolve = {
    model: ['$route', 'alertService', 'rapcollaboratorFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //    //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}



