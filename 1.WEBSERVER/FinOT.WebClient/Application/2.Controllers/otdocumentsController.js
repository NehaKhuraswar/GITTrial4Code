'use strict';
var otdocumentsController = ['$scope', '$q', '$http', 'Upload', 'alertService', 'masterdataFactory', 'otrequestFactory', function ($scope, $q, $http, Upload, alert, dataFactory, otFactory) {
    var _base = sessionStorage.getItem('apibaseurl');
    var self = this;
    self.editMode = false;
    self.model = [];
    self.editmodel = [];
    self.Extensions = '';

    $scope.newFiles = [];
    $scope.newFilesContent = [];

    self.onEdit = function (event) {
        if (event && event.stopPropagation) { event.stopPropagation(); } else if (window.event) { window.event.cancelBubble = true; }

        $q.all([_getDocumentExtensions()]).then(function () {
            self.editMode = true;
            angular.copy(self.model, self.editmodel);
        })
    }

    self.onCancel = function (event) {
        if (event && event.stopPropagation) { event.stopPropagation(); } else if (window.event) { window.event.cancelBubble = true; }

        self.editMode = false;
        angular.copy(self.model, self.editmodel);
    }

    /*Lookup data*/
    var _getDocuments = function () {
        if ($scope.reqid == null || $scope.reqid == undefined) { return; }
        return otFactory.GetDocuments($scope.reqid).then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.model = response.data;
            angular.copy(self.model, self.editmodel);
        })
    }

    var _getDocumentExtensions = function () {
        if (self.Extensions != '') { return; }
        return dataFactory.GetDocumentExtensions().then(function (response) {
            if (!alert.checkResponse(response)) { return; }
            self.Extensions = response.data;
        })
    }

    //inital load
    $q.all([_getDocuments()]).then(function () { })
    /*End Lookup data*/

    self.onGetDocumentContent = function (docid) {
        if (docid != undefined || docid != null) {
            otFactory.GetDocumentContent(docid).then(function (response) {
                handleFile(response.data, response.status, response.headers());
            });
        }
    };

    self.onSave = function (event) {
        if (event && event.stopPropagation) { event.stopPropagation(); } else if (window.event) { window.event.cancelBubble = true; }
        $scope.upload = Upload.upload({
            url: _base + 'api/rcrequest/document/upload',
            method: 'POST',
            data: self.editmodel,
            file: $scope.newFilesContent,
            fileName: $scope.newFiles,
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('token') }
        }).progress(function (evt) {
            //console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
        }).success(function (response, status, headers, config) {
            //if no errors
            if (!alert.checkResponse(response)) { return; }
            //show success message
            alert.Success('Document(s) saved successfully!');
            //refresh section
            _getDocuments();
            //change to readonly mode
            self.editMode = false;
        }).error(function (response) {
            alert.checkResponse(response);
        });
    }

    self.onDocumentDelete = function (doc) {
        angular.forEach(self.editmodel, function (item) {
            if (item.DocID == doc.DocID && item.FileName == doc.FileName) {
                if (item.DocID == null) {
                    var i = self.editmodel.indexOf(doc)
                    self.editmodel.splice(i, 1);
                }
                else {
                    item.Active = false;
                }
            }
        });
    }

    $scope.createNewDocument = function () {
        return {
            DocID: null,
            KeyID: $scope.reqid,
            Section: { ID: 15, Description: "Documents" },
            Category: { ID: 0, Description: "None" },
            FileName: null,
            Content: null,
            Description: null,
            Audit: null,
            Version: null,
            Rank: 1,
            showVersion: false
        }
    }

    $scope.onFileSelect = function ($files) {
        if ($files && $files.length) {
            if ($files[0].size > 4000000) {
                alert.Error('File size exceeds maximum permissible limit (4 MB).');
                return;
            }

            var newFileName = $files[0].name;
            var file = $scope.createNewDocument();
            file.FileName = newFileName;
            self.editmodel.push(file);
            $scope.newFiles.push($files[0].name);
            $scope.newFilesContent.push($files[0]);
        }
    }

    self.openConsultEmail = function (username) {
        $scope.openConsultEmail({ 'email': username + '@health.nyc.gov' });
    }
}];
