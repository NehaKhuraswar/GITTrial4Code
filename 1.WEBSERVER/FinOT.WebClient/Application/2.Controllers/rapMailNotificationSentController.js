var rapMailNotificationSentController = ['$scope', 'alertService', '$location', 'rapGlobalFactory', 'masterdataFactory', function ($scope, alert, $location, rapGlobalFactory, masterFactory) {
    var self = this;
    self.custDetails = rapGlobalFactory.CityUser;
    if (rapGlobalFactory.SelectedCase == null || rapGlobalFactory.SelectedCase == undefined)
    {
        $location.path("/staffdashboard");
    }
    self.c_id = rapGlobalFactory.SelectedCase.C_ID;
    self.caseinfo = rapGlobalFactory.SelectedCase;
    self.model = rapGlobalFactory.Notification;
    //self.Recipient = null;
    //for (var i = 0 ; i < self.model.Message.RecipientAddress.length; i++)
    //{
    //    if(i == 0)
    //    {
    //        self.Recipient = self.model.Message.RecipientAddress[i];
    //        self.Recipient = self.Recipient + ',' + self.model.Message.RecipientAddress[i];
    //    }
    //}

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
    if (!(self.model.CreatedDate == null || self.model.CreatedDate == undefined)) {
        self.Date = self.model.CreatedDate;
    }
   //    self.SentBy = rapGlobalFactory.CityUser.FirstName + ' ' + rapGlobalFactory.CityUser.LastName;
    self.BackToCase = function () {
        rapGlobalFactory.Notification = null;
        $location.path("/selectedcase");
    }

}];

var rapMailNotificationSentController_resolve = {
    model: ['$route', 'alertService', function ($route, alert) {

    }]
}