var mainController = ['$scope', '$location', 'Page', 'authFactory', 'alertService', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $location, Page, auth, alertService, rapGlobalFactory, masterFactory) {
    $scope.Page = Page;
  
    var self = this;
    self.UserName = '';
    
    
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
        self.UserName = '';
        rapGlobalFactory.CustomerDetails = null;
        rapGlobalFactory.CustID = 0;
        rapGlobalFactory.CaseDetails = null;
        rapGlobalFactory.CityUser = null;
        $location.path("/loginURL");
    }
  //  $location.path("/loginURL");
}];