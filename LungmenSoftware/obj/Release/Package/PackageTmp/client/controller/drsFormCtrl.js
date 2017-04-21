(function () {
    'use strict';

    angular
        .module('drsApp')
        .controller('drsFormCtrl', drsFormCtrl);

    drsFormCtrl.$inject = ['$scope', '$state', 'drsDataService']; 

    function drsFormCtrl($scope, $state, drsDataService) {
        /* jshint validthis:true */
        var vm = this;
        
        function init() {
            
            drsDataService.getChangeRequestRecord().then(function (res) {
                vm.changeRequestData = res.data;
                console.log(vm.changeRequestData);
            }, function (err) {

            });

            drsDataService.getDrsSystemPanelList().then(function (res) {
                
                $scope.drsPanels = res.data;
                //console.log($scope.drsPanels);

            }, function (err) {

            });
            $scope.fids = [];
            $scope.modList = [];
            $scope.tempFids = [];
        }

        init();
        
        vm.getFidsById = function (panelId) {
            //console.log($scope.system);
            //console.log(systemId);
            if ($scope.panel !== undefined && $scope.panel !=='' && panelId !==undefined) {
                //console.log($scope.system);
                drsDataService.getFidsByPanelId(panelId).then(
                function (res) {
                    //console.log(res.data);
                    $scope.fids = res.data;
                }, function (err) {

                }
                );
            } else {
                ResetSelectData();
            }
        }
        

        $scope.addToModList = function (fid) {
            var indexForRemoval = $scope.fids.indexOf(fid);
            console.log(fid);
            fid.DrsPanelName = $scope.panel.DRSPanelName;
            $scope.modList.push(fid);
            $scope.fids.splice(indexForRemoval, 1);
            
        }
        

        function ResetSelectData() {
            
            $scope.fids = undefined;
        }

        vm.deleteModItem = function (item) {
            var indexForRemoval = $scope.modList.indexOf(item);
            $scope.fids.push(item);
            $scope.modList.splice(indexForRemoval, 1);

        }        

        vm.postModList = function () {
            vm.changeRequestData.DrsChangeDeltas = $scope.modList;
            //console.log(vm.changeRequestData);
            drsDataService.postChangeRequestData(vm.changeRequestData);
            $state.go('drsConfirmForm');
                   
        }
    }
})();
