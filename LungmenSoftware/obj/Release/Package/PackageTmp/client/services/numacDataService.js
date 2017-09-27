(function () {
    'use strict';

    angular
        .module('numacApp')
        .service('numacDataService', numacDataService);

    numacDataService.$inject = ['$http'];

    function numacDataService($http) {
        
        var numacChangeRequestPromise;
        var routePrefix = "lungscm";
        this.getNumacData = function () {
            return $http.get(routePrefix+'/numac/getnumacdata');
        }

        this.getChangeRequestRecord = function () {
            return $http.get(routePrefix +'/changerequest/InitChangeRequest');
        }

        this.getSystemPanelList = function () {
            return $http.get(routePrefix +"/numac/getsystempanellist");
        }

        this.getNumacData = function () {
            return $http.get(routePrefix +"/numac/getnumacdata");
        }

        this.getSubSystemBySytemId = function (systemId) {
            return $http.get(routePrefix +'/numac/GetSubSystemById/?systemId=' + systemId);
        }
        this.getModulesById=function(chassisId) {
            return $http.get(routePrefix +'/numac/GetModulesById/?chassisId=' + chassisId);
        }

        this.postChangeRequestData = function (changeRequestData) {
            numacChangeRequestPromise = $http.post(routePrefix +'/changerequest/AddNumacChangeRequestRecord', changeRequestData);
            return numacChangeRequestPromise;
        }

        this.getNumacChangeRequestPromise=function(){
            return numacChangeRequestPromise;
        }

        this.getNumacChangeRequestRecordById=function(ChassisBoardId){
            return $http.get(routePrefix +'/numac/GetNumacChangeRequestRecordById/?ChassisBoardId='+ChassisBoardId);
        }

    }
})();