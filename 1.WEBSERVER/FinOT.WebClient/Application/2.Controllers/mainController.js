var mainController = ['$scope', '$location', '$cookies', 'Page', 'authFactory', 'alertService', 'rapGlobalFactory', 'masterdataFactory', function ($scope, $location, $cookies, Page, auth, alertService, rapGlobalFactory, masterFactory) {
    $scope.Page = Page;
  
    var self = this;
    self.UserName = '';
    var _getCustomer = function (custid) {
        var url = '/api/accountmanagement' + '/getCustomer';
        if (!(custid == null || custid == undefined)) { url = url + '/' + custid; }
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: url,
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('token') }
        }).then(function (response) {
            deferred.resolve(response.data);           
        },
        function(reason)
        {
            deferred.reject(reason);
        })
         return deferred.promise;
    }


    //    masterFactory.GetCustomer(custID).then(function (response) {
    //        if (!alert.checkResponse(response)) {
    //            deferred.reject(response);
    //        }
    //        deferred.resolve(response.data);
    //    });
    //    return deferred.promise;
    //}
    //if (rapGlobalFactory.CustomerDetails == null || rapGlobalFactory.CustomerDetails == undefined) {
    //    var custID = rapGlobalFactory.GetCustomer();
    //    if (custID != null)
    //        rapGlobalFactory.CustomerDetails = _getCustomer(custID);
    //}
    var userType = rapGlobalFactory.GetUserType();
    if (userType == 'PublicUser')
    {
        if (rapGlobalFactory.CustomerDetails == null || rapGlobalFactory.CustomerDetails == undefined) {
            rapGlobalFactory.CustomerDetails = rapGlobalFactory.GetCustomer();
        }
    }
    else if (userType == 'CityUser')
    {
        if (rapGlobalFactory.CityUser == null || rapGlobalFactory.CityUser == undefined) {
            rapGlobalFactory.CityUser = rapGlobalFactory.GetCityUser();
        }
    }
        //var custID = rapGlobalFactory.GetCustomer();
        //masterFactory.GetCustomer(custID).then(function (response) {
        //    if (!alert.checkResponse(response)) {
        //        return;
        //    }
        //    self.model = response.data;
        //    rapGlobalFactory.CustomerDetails = response.data;
            
        //});
    
    
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
        sessionStorage.clear();
        $cookies.remove("userInfo");
        if (publiclogin == true)
        {
           $location.path("/Login");
        }
        else if (stafflogin == true)
        {
            $location.path("/CityLogin");
        }
    }
    if ($location.$$path == "") {
        $location.path("/Login");
    }
}];