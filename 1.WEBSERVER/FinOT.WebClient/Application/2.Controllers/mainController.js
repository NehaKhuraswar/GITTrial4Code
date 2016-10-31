var mainController = ['$scope', '$location', 'Page', 'authFactory', function ($scope, $location, Page, auth) {
    $scope.Page = Page;

    $scope.Register = { Text: '' };
    $scope.Register = function () {
        var text = 'Staff.html';//$scope.quickSearch.Text;
        if (angular.isDefined(text) && text.trim() != '') {
            //$scope.quickSearch.Text = '';
            //$location.path("/register/" + text.trim());
            $location.path("/register");
        }
    }
    //$scope.Register = function () {
    //    var text = 'Register';//$scope.quickSearch.Text;
    //    //if (angular.isDefined(text) && text.trim() != '') {
    //    //    $scope.quickSearch.Text = '';
    //    //    $location.path("/request/" + text.trim());
    //    //}
    //}
}];