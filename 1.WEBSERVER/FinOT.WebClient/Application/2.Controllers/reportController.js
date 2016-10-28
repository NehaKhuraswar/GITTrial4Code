var reportController = ['$scope', 'Page', 'model', function ($scope, Page, model) {
    Page.setTitle('Report - ' + model.PageTitle);
    var self = this;
    self.model = model;
}];


var reportController_resolve = {
    model: ['$route', 'authFactory', 'alertService', 'reportFactory', function ($route, auth, alert, reportData) {
        return auth.fetchToken().then(function (response) {
            //var pairs = str.split('&');
            //var result = {};
            //pairs.forEach(function (pair) {
            //    pair = pair.split('=');
            //    var name = pair[0]
            //    var value = pair[1]
            //    if (name.length)
            //        if (result[name] !== undefined) {
            //            if (!result[name].push) {
            //                result[name] = [result[name]];
            //            }
            //            result[name].push(value || '');
            //        } else {
            //            result[name] = value || '';
            //        }
            //});
            var rptParams = {};
            angular.forEach(Object.keys($route.current.params), function (key) {
                if (key != "reportid")
                    rptParams[key] = $route.current.params[key];
            });
            console.log(rptParams);
            console.log($route.current.params);

            return reportData.GetSource($route.current.params.reportid, rptParams).then(function (response) {
                if (!alert.checkResponse(response)) { return; }
                return response.data;
            });
        });
    }]
}