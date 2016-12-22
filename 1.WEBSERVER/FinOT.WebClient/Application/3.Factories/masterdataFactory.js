'use strict';
var masterdataFactory = ['blockUI', 'ajaxService', '$timeout', '$http', function (blockUI, ajax, $timeout, $http) {
    var factory = {};
    var _routePrefix = 'api/masterdata';
    var _GetStateList = function () {
        blockUI.start();
        var url = 'api/accountmanagement' + '/getstatelist';
        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _ResendPin = function (model) {
        blockUI.start();

        return ajax.Post(model, 'api/accountmanagement' + '/resendpin')
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetAccountTypes = function () {
        blockUI.start();
        var url = 'api/accountmanagement' + '/getaccounttypes'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetHearingOfficers = function () {
        blockUI.start();
        var url = 'api/dashboard' + '/gethearingofficers'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }
    var _GetAnalysts = function () {
        blockUI.start();
        var url = 'api/dashboard' + '/getanalysts'

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _AssignAnalyst = function (C_ID, AnalystUserID) {
        blockUI.start();
        var url = 'api/dashboard' + '/assignanalyst/' + C_ID + '/' + AnalystUserID;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _AssignHearingOfficer = function (C_ID, HearingOfficerUserID) {
        blockUI.start();
        var url = 'api/dashboard' + '/assignhearingofficer/' + C_ID + '/' + HearingOfficerUserID;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetCustomer = function (custid) {
        blockUI.start();

        var url = 'api/accountmanagement' + '/get';
        if (!(custid == null || custid == undefined)) { url = url + '/' + custid; }

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }

    var _GetCasesForCustomer = function (custID) {
        blockUI.start();
        var url = 'api/applicationprocessing' + '/getcasesforcustomer/' + custID;

        return ajax.Get(url)
        .finally(function () {
            blockUI.stop();
        });
    }


    var _GetStatusList = function (reqid) {
        return ajax.Get(_routePrefix + '/statuslist/get');
    }

    var _fileExtensons = ['.PDF', '.DOC', '.DOCX', '.XLS', '.JPEG', '.TIFF', '.PNG'];
    var _fileSize = 25 ;


  var _getDocument = function (data) {        
      var _url = 'api/applicationprocessing' + '/GetDocument';
        return $http({
            method: 'POST',
            url: _url,
            data: data,
            headers: { 'Authorization': 'Bearer ' + sessionStorage.getItem('token'), 'enctype': 'multipart/form-data' }

        }).then(function (response) {            
            _Download(response.data)
        },
        function (reason) {
            $log.info(reason.status + ' | ' + reason.statusTex);
        }).catch(function (response) {
            if (response.status == 401) { $location.path('/noaccess'); }
            return response.data;
        });
    }

    var _Download = function (doc) {
        var blob = b64toBlob(doc.data.Base64Content, doc.data.MimeType);
        var blobUrl = URL.createObjectURL(blob);
        if (window.navigator && window.navigator.msSaveOrOpenBlob) {
            window.navigator.msSaveOrOpenBlob(blob, doc.data.DocName);
        }
        else {

            window.open(blobUrl);
        }
    }

    function b64toBlob(b64Data, contentType, sliceSize) {
        contentType = contentType || '';
        sliceSize = sliceSize || 512;

        var byteCharacters = atob(b64Data);
        var byteArrays = [];

        for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
            var slice = byteCharacters.slice(offset, offset + sliceSize);

            var byteNumbers = new Array(slice.length);
            for (var i = 0; i < slice.length; i++) {
                byteNumbers[i] = slice.charCodeAt(i);
            }

            var byteArray = new Uint8Array(byteNumbers);

            byteArrays.push(byteArray);
        }

        var blob = new Blob(byteArrays, { type: contentType });
        return blob;
    }


    var _Years = function () {
        var range = 5;
        var currentYear = new Date().getFullYear();
        var _years = [];
        for (var i = range; i > 0 ; i--) {

            _years.push(currentYear - i);
        }
        for (var i = 0; i < range + 1; i++) {
            _years.push(currentYear + i);
        }
        return _years;
    }

    var _Months = function () {
        return ([
       { id: 1, value: 'January' },
       { id: 2, value: 'February' },
       { id: 3, value: 'March' },
       { id: 4, value: 'April' },
       { id: 5, value: 'May' },
       { id: 6, value: 'June' },
       { id: 7, value: 'July' },
       { id: 8, value: 'August' },
       { id: 9, value: 'September' },
       { id: 10, value: 'October' },
       { id: 11, value: 'November' },
       { id: 12, value: 'December' }])
    };

    var _Days = function () {
        return ([1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
       11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
       21, 22, 23, 24, 25, 26, 27, 29, 29, 30, 31])
    };

    var _Calender = {
        Months: _Months(),
        Days: _Days(),
        Years: _Years()
    }

    //var _getDocument = function (model) {
    //    blockUI.start();
    //    var url = 'api/applicationprocessing' + '/GetDocument';

    //    return ajax.Post(model, url)
    //    .finally(function () {
    //        blockUI.stop();
    //    });
    //}
    
   

    factory.GetStatusList = _GetStatusList;
    factory.GetStateList = _GetStateList;
    factory.GetAccountTypes = _GetAccountTypes;
    factory.GetHearingOfficers = _GetHearingOfficers;
    factory.GetAnalysts = _GetAnalysts;
    factory.AssignAnalyst = _AssignAnalyst;
    factory.AssignHearingOfficer = _AssignHearingOfficer;
    factory.GetCasesForCustomer = _GetCasesForCustomer;
    factory.GetCustomer = _GetCustomer;
    factory.Calender = _Calender;
    factory.GetDocument = _getDocument;
    factory.ResendPin = _ResendPin;
    factory.FileExtensons = _fileExtensons;
    factory.FileSize = _fileSize;   
    return factory;
}];