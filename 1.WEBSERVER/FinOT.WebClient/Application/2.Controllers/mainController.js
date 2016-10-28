var mainController = ['$scope', '$location', 'Page', 'authFactory', function ($scope, $location, Page, auth) {
    $scope.Page = Page;

    $scope.quickSearch = { Text: '' };
    $scope.quickSearch = function () {
        var text = $scope.quickSearch.Text;
        if (angular.isDefined(text) && text.trim() != '') {
            $scope.quickSearch.Text = '';
            $location.path("/request/" + text.trim());
        }
    }
}];