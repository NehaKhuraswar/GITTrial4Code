var mainController = ['$scope', '$location', 'Page', 'authFactory', 'alertService', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $location, Page, auth, alertService, rapGlobalFactory, masterFactory) {
    $scope.Page = Page;
  
    var self = this;
    self.UserName = '';
    
    self.Home =function()
    {
        if (rapGlobalFactory.CustomerDetails != null) {
            $location.path("/publicdashboard");
        }
        else if (rapGlobalFactory.CityUser != null)
        {
            $location.path("/staffdashboard");
        }
    }   
    self.GetUserName = function ()
    {
        if (rapGlobalFactory.CustomerDetails != null)
        {
            self.UserName =  rapGlobalFactory.CustomerDetails.User.FirstName;
        }
        else if (rapGlobalFactory.CityUser != null)
        {
            self.UserName = rapGlobalFactory.CityUser.FirstName;
        }
        return self.UserName;
    }

    //TBD clear the cache
    self.LogOut = function ()
    { 
        var publiclogin = false;
        var stafflogin = false;
        
        if (rapGlobalFactory.CustomerDetails != null) {
            publiclogin = true;            
        }
        else if (rapGlobalFactory.CityUser != null)
        {
            stafflogin = true;
        }
                self.UserName = '';
        rapGlobalFactory.CustomerDetails = null;
        rapGlobalFactory.CustID = 0;
        rapGlobalFactory.CaseDetails = null;
        rapGlobalFactory.CityUser = null;
        if (publiclogin == true)
        {
           $location.path("/Login");
        }
        else if (stafflogin == true)
        {
            $location.path("/CityLogin");
        }
    }
  //  $location.path("/loginURL");
}];