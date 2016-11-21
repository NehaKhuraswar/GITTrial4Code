'use strict';
var rapservingappealFactory = ['blockUI', 'ajaxService', function (blockUI, ajax) {
    var factory = {};
    var _routePrefix = 'api/applicationprocessing';
    
    //var _AddAnotherOpposingParty = function (model) {
    //    blockUI.start();

    //    var url = _routePrefix + '/addopposingparty'
    //    return ajax.Post(model, url)
    //    .finally(function () {
    //        blockUI.stop();
    //    });           
    //}

    //factory.AddAnotherOpposingParty = _AddAnotherOpposingParty;
    return factory;
}];