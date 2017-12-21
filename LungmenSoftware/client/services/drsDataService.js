(function () {
    'use strict';

    angular
        .module('drsApp')
        .service('drsDataService', drsDataService);

    drsDataService.$inject = ['$http'];

    function drsDataService($http) {
        
        var drsChangeRequestPromise;
        var routePrefix = "lungscm";
        this.getDrsData = function () {
            return $http.get('drsData');
        };

        this.getChangeRequestRecord = function () {
            return $http.get('/changerequest/InitChangeRequest');
        };

        this.getDrsSystemPanelList = function () {
            return $http.get("/drsData/GetDrsSystemPanelList");
        };

        this.getFidsByPanelId = function (panelId) {

            return $http.get('/drsData/GetFidsByPanelId/' + panelId);
        };


        this.postChangeRequestData = function (changeRequestData) {
            drsChangeRequestPromise = $http.post('/changerequest/AddDrsChangeRequestRecord', changeRequestData);
            return drsChangeRequestPromise;
        };

        this.getDrsChangeRequestPromise = function() {
            return drsChangeRequestPromise;
        };

        this.getDrsChangeRequestRecordById = function(ChassisBoardId) {
            return $http.get('drsData/GetDrsChangeRequestRecordById/?ChassisBoardId=' + ChassisBoardId);
        };

    }
})();