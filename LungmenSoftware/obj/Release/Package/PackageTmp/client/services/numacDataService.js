(function () {
    'use strict';

    angular
        .module('numacApp')
        .service('numacDataService', numacDataService);

    numacDataService.$inject = ['$http'];

    function numacDataService($http) {
        
        var numacChangeRequestPromise;

        this.getNumacData = function () {
            return $http.get('/numac/getnumacdata');
        }

        this.getChangeRequestRecord = function () {
            return $http.get('/changerequest/InitChangeRequest');
        }

        this.getSystemPanelList = function () {
            return $http.get("/numac/getsystempanellist");
        }

        this.getNumacData = function () {
            return $http.get("/numac/getnumacdata");
        }

        this.getSubSystemBySytemId = function (systemId) {
            return $http.get('/numac/GetSubSystemById/?systemId=' + systemId);
        }
        this.getModulesById=function(chassisId) {
            return $http.get('/numac/GetModulesById/?chassisId=' + chassisId);
        }

        this.postChangeRequestData = function (changeRequestData) {
            numacChangeRequestPromise=$http.post('/changerequest/AddNumacChangeRequestRecord', changeRequestData);
            return numacChangeRequestPromise;
        }

        this.getNumacChangeRequestPromise=function(){
            return numacChangeRequestPromise;
        }

        this.getNumacChangeRequestRecordById=function(ChassisBoardId){
            return $http.get('/numac/GetNumacChangeRequestRecordById/?ChassisBoardId='+ChassisBoardId);
        }

    }
})();