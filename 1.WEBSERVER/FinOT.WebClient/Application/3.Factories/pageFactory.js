'use strict';
var pageFactory = [function () {
    var _default = 'Rent Adjustment Program';
    var title = '';
    return {
        title: function () { return title + ((title != '') ? ' - ' : '') + _default; },
        setTitle: function (newTitle) { title = newTitle; }
    };
}];