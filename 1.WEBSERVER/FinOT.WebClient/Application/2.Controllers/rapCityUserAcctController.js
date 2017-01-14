'use strict';
var rapCityUserAcctController = ['$scope', '$modal', 'alertService', 'rapcityuserregisterFactory', 'rapGlobalFactory', 'masterdataFactory', '$location', function ($scope, $modal, alert, rapFactory,rapGlobalFactory, masterFactory, $location) {
    var self = this;
    // self.CityUserAccount = [];
    self.AccountTypesList = [];
    self.confirmPwd = "";
    self.bEdit = rapGlobalFactory.IsEdit;
    self.Title = "Create a City of Oakland Account";
    self.SubmitText = "Create account";

    var _GetCityUserFromID = function (CityUserID) {
        return rapFactory.GetCityUserFromID(CityUserID).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.CityUserAccount = response.data;
        });
    }
    if (rapGlobalFactory.IsEdit == true) {
        self.Title = "Edit a City of Oakland Account";
        self.SubmitText = "Update account";
        _GetCityUserFromID(rapGlobalFactory.SelectedForEdit.custID);
    }


    self.CreateAccount = function (model) {
        if (model.Password != self.confirmPwd) {
            alert.Error("Please enter same password in password fields.");
            return;
        }
        rapFactory.CreateCityUserAccount(model).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            if (rapGlobalFactory.IsEdit == true) {
                rapGlobalFactory.SelectedForEdit = null;
                rapGlobalFactory.IsEdit = false;
                $location.path("/admindashboard");
            }
            else if(rapGlobalFactory.IsAdmin == true)
            {
                rapGlobalFactory.IsAdmin = false;
                $location.path("/admindashboard");
            }
            else {
                $location.path("/CityLogin");
            }
            
            
        });
    }
    self.DeleteCityUser = function (UserID) {
        rapFactory.DeleteCityUser(UserID).then(function (response) {
            if (!alert.checkResponse(response)) {
                return;
            }
            if (rapGlobalFactory.IsEdit == true) {
                rapGlobalFactory.SelectedForEdit = null;
                rapGlobalFactory.IsEdit = false;
                $location.path("/admindashboard");
            }
            else {
                $location.path("/CityLogin");
            }

        });
    }
    self.Cancel = function () {
        if (rapGlobalFactory.IsEdit == true) {
            rapGlobalFactory.SelectedForEdit = null;
            rapGlobalFactory.IsEdit = false;
            $location.path("/admindashboard");
        }
        else if (rapGlobalFactory.IsAdmin == true) {
            rapGlobalFactory.IsAdmin = false;
            $location.path("/admindashboard");
        }
        else {
            $location.path("/CityLogin");
        }
    }
    var _getAccountTypes = function () {
        masterFactory.GetAccountTypes().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.AccountTypesList = response.data;
        });
    }
    _getAccountTypes();

}];
var rapCityUserAcctController_resolve = {
    model: ['$route', 'alertService', 'rapcityuserregisterFactory', function ($route, alert, rapFactory) {
        //return auth.fetchToken().then(function (response) {
        //return rapFactory.GetCustomer(null).then(function (response) {
        //     //   if (!alert.checkResponse(response)) { return; }
        //    //    return response.data;
        //    //});
        //});
    }]
}