(function () {
    'use strict';

    angular
        .module('numacApp')
        .controller('numacFormCtrl', numacFormCtrl);

    numacFormCtrl.$inject = ['$scope', '$state', 'numacDataService']; 

    function numacFormCtrl($scope, $state, numacDataService) {
        /* jshint validthis:true */
        var vm = this;
        
        function init() {
            
            numacDataService.getChangeRequestRecord().then(function (res) {
                vm.changeRequestData = res.data;
                //console.log(vm.changeRequestData);
            }, function (err) {
            });

            numacDataService.getSystemPanelList().then(function (res) {
                
                $scope.numacPanels = res.data;
                //console.log($scope.numacPanels);

            }, function (err) {

            });
            $scope.subSystems = [];
            $scope.modList = [];
            $scope.tempModules = [];
        }

        init();
        
        vm.getSubsystemById = function (systemId) {
            //console.log($scope.system);
            //console.log(systemId);
            if ($scope.system != undefined) {
                //console.log($scope.system);
                numacDataService.getSubSystemBySytemId(systemId).then(
                function (res) {
                    //console.log(res.data);
                    $scope.subSystems = res.data;
                }, function (err) {

                }
                );
            } else {
                ResetSelectData();
            }
        }
        
        vm.getModulesById = function (subsystemId) {
            //console.log(subsystemId);
            if ($scope.subsystem != undefined) {
                numacDataService.getModulesById(subsystemId).then(
                function (res) {
                    console.log(res.data)
                    $scope.modules = res.data;
                }, function (err) {

                }
                )
            } else {
                ResetSelectData();
            }
            
        }

        $scope.addToModList = function (module) {
            var indexForRemoval = $scope.modules.indexOf(module);
                     
            module.Panel = $scope.system.Panel;
            module.ChassisName = $scope.subsystem.ChassisName;
            //module.SocketLocation=$scope..SocketLocation;
            $scope.modList.push(module);
            $scope.modules.splice(indexForRemoval, 1);
            console.log($scope.modList);
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
            numacDataService.postChangeRequestData(vm.changeRequestData)
            $state.go('numacConfirmForm');
                   
        }



    }
})();
