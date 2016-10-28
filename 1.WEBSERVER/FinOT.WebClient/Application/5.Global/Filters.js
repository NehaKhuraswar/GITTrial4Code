var inArray = ['$filter', function ($filter) {
    return function (list, arrayFilter, element) {
        if (arrayFilter) {
            return $filter("filter")(list, function (listItem) {
                return arrayFilter.indexOf(listItem[element]) != -1;
            });
        }
    };
}];

var datetime = ['$filter', function ($filter) {
    return function (input) {
        if (input == null) { return ""; }
        var _date = $filter('date')(new Date(input).toLocaleString("en-US"));
        return _date.toUpperCase();
    };
}];

var allocationFilter = [function () {
    return function (input, type) {
        var _total;
        angular.forEach(input, function (item) {
            if (item.FundingType.ID == type) {
                if (_total == undefined) { _total = 0.0; }
                _total = parseFloat(_total) + parseFloat(item.Allocation);
            }
        });
        return _total;
    }
}];

function convertUTCToLocalTime(UTCDateString) {
    var cvtrLocalTime = new Date(UTCDateString);
    var hourOffset = cvtrLocalTime.getTimezoneOffset() / 60;
    cvtrLocalTime.setHours(cvtrLocalTime.getHours() + hourOffset);
    return cvtrLocalTime;
}

function handleFile(data, status, headers) {
    var octetStreamMime = 'application/octet-stream';
    var success = false;
    var filename = headers['x-filename'] || 'download.bin';
    var contentType = headers['content-type'] || octetStreamMime;

    try {
        var blob = new Blob([data], { type: contentType });
        if (navigator.msSaveBlob)
            navigator.msSaveBlob(blob, filename);
        else {
            var saveBlob = navigator.webkitSaveBlob || navigator.mozSaveBlob || navigator.saveBlob;
            if (saveBlob === undefined) throw "Not supported";
            saveBlob(blob, filename);
        }
        success = true;
    } catch (ex) { }

    if (!success) {
        var urlCreator = window.URL || window.webkitURL || window.mozURL || window.msURL;
        if (urlCreator) {
            var link = document.createElement('a');
            if ('download' in link) {
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = urlCreator.createObjectURL(blob);
                    link.setAttribute('href', url);

                    link.setAttribute("download", filename);

                    var event = document.createEvent('MouseEvents');
                    event.initMouseEvent('click', true, true, window, 1, 0, 0, 0, 0, false, false, false, false, 0, null);
                    link.dispatchEvent(event);
                    success = true;
                } catch (ex) { }
            }

            if (!success) {
                try {
                    var blob = new Blob([data], { type: octetStreamMime });
                    var url = urlCreator.createObjectURL(blob);
                    window.location = url;
                    success = true;
                } catch (ex) { }
            }

        }
    }

    if (!success) {
        window.open(httpPath, '_blank', '');
    }
}

//sort array numerically. e.g. array.sort(sortNumber)
function sortNumber(a, b) { return a - b; }

/*Array Prototype*/
Array.prototype.contains = function (obj) {
    var i = this.length;
    while (i--) {
        if (this[i] === obj) {
            return true;
        }
    }
    return false;
}

Array.prototype.max = function () {
    return Math.max.apply(null, this);
};

Array.prototype.min = function () {
    return Math.min.apply(null, this);
};