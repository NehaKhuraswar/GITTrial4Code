var mainController = ['$scope', '$location', 'Page', 'authFactory', 'alertService', function ($scope, $location, Page, auth, alertService) {
    $scope.Page = Page;
  
           
    $location.path("/loginURL");
 
}];