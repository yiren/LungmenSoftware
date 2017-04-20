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
                //console.log(vm.changeRequestData);
            }, function (err) {

            });

            drsDataService.getDrsPanelList().then(function (res) {
                
                $scope.drsPanels = res.data;
                //console.log($scope.numacPanels);

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
            if ($scope.system !== undefined) {
                //console.log($scope.system);
                drsDataService.getFidsById(panelId).then(
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
                     
            module.Panel = $scope.system.Panel;
            module.ChassisName = $scope.subsystem.ChassisName;
            //module.SocketLocation=$scope..SocketLocation;
            $scope.modList.push(module);
            $scope.modules.splice(indexForRemoval, 1);
            //console.log($scope.modList);
        }
        
        function ResetSelectData() {
            $scope.subsystem = undefined;
            $scope.modules = undefined;
        }

        vm.deleteModItem = function (item) {
            var indexForRemoval = $scope.modList.indexOf(item);
            $scope.modules.push(item);
            $scope.modList.splice(indexForRemoval, 1);

        }        

        vm.postModList = function () {
            vm.changeRequestData.NumacChangeDeltas = $scope.modList;
            //console.log(vm.changeRequestData);
            drsDataService.postChangeRequestData(vm.changeRequestData);
            $state.go('drsConfirmForm');
                   
        }
    }
})();
