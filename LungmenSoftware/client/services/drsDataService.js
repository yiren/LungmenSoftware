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
            return $http.get(routePrefix+'/drsData');
        };

        this.getChangeRequestRecord = function () {
            return $http.get(routePrefix+'/changerequest/InitChangeRequest');
        };

        this.getDrsSystemPanelList = function () {
            return $http.get(routePrefix+"/drsData/GetDrsSystemPanelList");
        };

        this.getFidsByPanelId = function (panelId) {

            return $http.get(routePrefix+'/drsData/GetFidsByPanelId/' + panelId);
        };


        this.postChangeRequestData = function (changeRequestData) {
            drsChangeRequestPromise = $http.post(routePrefix+'/changerequest/AddDrsChangeRequestRecord', changeRequestData);
            return drsChangeRequestPromise;
        };

        this.getDrsChangeRequestPromise = function() {
            return drsChangeRequestPromise;
        };

        this.getDrsChangeRequestRecordById = function(ChassisBoardId) {
            return $http.get('/drsData/GetDrsChangeRequestRecordById/?ChassisBoardId=' + ChassisBoardId);
        };

    }
})();