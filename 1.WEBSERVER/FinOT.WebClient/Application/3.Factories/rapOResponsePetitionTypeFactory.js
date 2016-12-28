'use strict';
var rapOResponsePetitionTypeFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';

    
    var _getPetitionCategory = function () {
        blockUI.start();
        var url = _routePrefix + '/GetPetitioncategory/';
        var caseInfo = null;
        return ajax.Get(url, caseInfo)
        .finally(function () {
            blockUI.stop();
        });
    }

    factory.GetPetitionCategory = _getPetitionCategory;

    return factory;
}];