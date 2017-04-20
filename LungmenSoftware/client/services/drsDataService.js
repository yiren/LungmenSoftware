(function () {
    'use strict';

    angular
        .module('drsApp')
        .service('drsDataService', drsDataService);

    drsDataService.$inject = ['$http'];

    function drsDataService($http) {
        
        var drsChangeRequestPromise;

        this.getDrsData = function() {
            return $http.get('/drsData');
        };

        this.getChangeRequestRecord = function() {
            return $http.get('/changerequest/InitChangeRequest');
        };

        this.getDrsPanelList = function() {
            return $http.get("/drsData/getdrspanellist");
        };

        this.getFidByPanelId = function(panelId) {
            return $http.get('/drsData/GetFidByPanelId/?panelId=' + panelId);
        };


        this.postChangeRequestData = function(changeRequestData) {
            drsChangeRequestPromise = $http.post('/changerequest/AddDrsChangeRequestRecord', changeRequestData);
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