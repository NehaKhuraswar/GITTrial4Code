var mainController = ['$scope', '$location', 'Page', 'authFactory', 'alertService', function ($scope, $location, Page, auth, alertService) {
    $scope.Page = Page;

    //$scope.Register = { Text: '' };
    //$scope.Register = function () {
       
       ///   $location.path("/register");
  
    //}
    //$scope.Login = function () {
        
           
    $location.path("/login");

  //  }
    //$scope.Register = function () {
    //    var text = 'Register';//$scope.quickSearch.Text;
    //    //if (angular.isDefined(text) && text.trim() != '') {
    //    //    $scope.quickSearch.Text = '';
    //    //    $location.path("/request/" + text.trim());
    //    //}
    //}
}];