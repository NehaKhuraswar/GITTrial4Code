'use strict';
var alertService = ['inform', 'rapGlobalFactory', function (inform, rapGlobalFactory) {
    var self = this;
    // self.isAuthenticated = false;
    self.checkResponse = function (res) {
        if (res != null && res != undefined) {
            if (res.exceptions != null && res.exceptions.length) { self.Error(res.exceptions); }
            if (res.warnings != null && res.warnings.length) { self.Warning(res.warnings); }
            if (res.errors != null) { self.ModelError(res.errors); }
            return res.status;
        }
        return false;
    }
    self.checkForResponse = function (res) {
        if (res != null && res != undefined) {
            if (res.exceptions != null && res.exceptions.length) { self.GetError(res.exceptions); }
            if (res.warnings != null && res.warnings.length) { self.Warning(res.warnings); }
            if (res.errors != null) { self.ModelError(res.errors); }
            return res.status;
        }
        return false;
    }

    var showMessage = function (messages, style) {
        if (angular.isArray(messages)) {
            var message = '<ul>';
            angular.forEach(messages, function (msg) {
                message += '<li>' + msg + '</li>';
            });
            self.Error(message + '</ul>');
        }
        else {
            inform.add(messages, { type: style });
        }
    }

    var changeElementStyle = function (obj) {
        console.log(obj);
    }

    self.ModelError = function (errors) {
        var message = '<ul>';
        angular.forEach(Object.keys(errors), function (key) {
            changeElementStyle(key);
            for (var i = 0; i < errors[key].length; i++) {
                message += '<li>' + errors[key][i] + '</li>';
            }
        });
        self.Error(message + '</ul>');
    }

    self.Success = function (messages) {
        showMessage(messages, 'success');
    }
    self.Error = function (messages) {
        showMessage(messages, 'danger');
    }
    self.GetError = function (messages) {
        //showMessage(messages, 'danger');
        var message = '';
        angular.forEach(messages, function (msg) {
            message += msg;
        });
        rapGlobalFactory.Error = message;
    }

    self.Warning = function (messages) {
        showMessage(messages, 'default');
    }

    self.Info = function (messages) {
        showMessage(messages, 'info');
    }
}];