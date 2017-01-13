var rapEmailNotificationSentController = ['$scope', 'alertService', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, alert, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CityUser;
    self.c_id = rapGlobalFactory.SelectedCase.C_ID;
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.model = rapGlobalFactory.Notification;
   
    var _date = function () {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today = mm + '-' + dd + '-' + yyyy;
        return today;
    }
    self.Date = _date();
    self.SentBy = rapGlobalFactory.CityUser.FirstName + ' ' + rapGlobalFactory.CityUser.LastName;
    self.BackToCase = function () {
        rapGlobalFactory.Notification = null;
        $location.path("/selectedcase");
    }

}];

var rapEmailNotificationSentController_resolve = {
    model: ['$route', 'alertService', function ($route, alert, rapFactory) {

    }]
}