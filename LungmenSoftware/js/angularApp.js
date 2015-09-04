(function() {
    angular.module('simpleApp', [])
    .controller('MainCtrl', ['$http','$log', '$scope', MainCtrl]
        
     );

    function MainCtrl($http, $log, $scope) {
        var vm = this;
        vm.message = 'Before Getting Data';
        
        $http.get('/foxsoftwares/getsystemsoftwares')
               .success(function (data) {
                   vm.message = 'Data Retrieves Successfully';
                   vm.softwareList = data;
                   //$log.info(data);
               })
               .error(function () {
                   vm.message = 'Unexpected Error When Getting All Softwares';
               });
        $http.get('/foxworkstations/getworkstations')
            .success(function(data) {
                vm.allWorkstations = data;
                //$log.info("All Workstations " + data);
                var processData = [];
               
                
            })
            .error(function() {
                vm.message = 'Unexpected Error While Getting All Workstations';
            });
        vm.requestWorkStation=function() {
            $http.get('/foxsoftwares/getsystemsoftwarelist')
                .success(function(data) {
                    vm.message = 'Data Retrieves Successfully';
                    vm.softwareList = data;
                    //$log.info(data);
                })
                .error(function() {
                    vm.message = 'Unexpected Error While Getting Software List';
                });
        }
        vm.getSelectedSoft = function (selected) {
            vm.allWorkstations.isChecked = false;
            var tempAllWks = vm.allWorkstations;
            $http.get('/foxsoftwares/GetWorkStationsBySoftId/' + selected.FoxSoftwareId)
                .then(function(response) {
                    $log.info(response.data.length);
                    angular.forEach(response.data, function (value, key) {
                        
                        var wk = value;
                        var wkId = wk.WorkstationId;
                        $log.info("Is reponse.data an Object? " + angular.isObject(value));
                        var wkObject = angular.fromJson(tempAllWks);
                        $log.info(wkObject);
                        angular.forEach(wkObject, function (value, key) {
                            var id = value.WorkStationId;
                            $log.info("One of WKs:" + id);
                            if (wkId === id) {
                                $log.info(vm.allWorkstations[key]);
                                tempAllWks[key].isChecked = true;
                            }
                        });
                    });
                    vm.allWorkstations = tempAllWks;
                }, function (errResponse) {
                    $log.error(errResponse);
                    vm.message = 'Unexpected Error While Getting Workstations By SoftId';
                });
            $log.info(selected);
        }
        vm.showChecked = function (selected) {

            $log.info(selected);
        }
        vm.isDividedByFive=function(index) {
            if ((index + 1) % 5 == 0) {
                return true;
            } else {
                return false;
            }
        }


        

        }    
} )();
