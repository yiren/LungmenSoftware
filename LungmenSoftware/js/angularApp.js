(function() {
    angular.module('simpleApp', [])
    .controller('MainCtrl', ['$http','$log', '$scope', MainCtrl]
        
     );

    function UpdateWorkStationList($http, vm) {
        $http.get('/foxworkstations/getworkstations')
            .success(function (data) {
                vm.allWorkstations = data;
                //$log.info("All Workstations " + data);
                var processData = [];


            })
            .error(function () {
                vm.message = 'Unexpected Error While Getting All Workstations';
            });
    }

    function MainCtrl($http, $log, $scope) {
        var vm = this;
        vm.message = 'Before Getting Data';
        vm.newWorkstation = {};
        UpdateWorkStationList($http, vm);
        $http.get('/foxsoftwares/getsystemsoftwares')
               .success(function (data) {
                   vm.message = 'Data Retrieves Successfully';
                   vm.softwareList = data;
                   //$log.info(data);
               })
               .error(function () {
                   vm.message = 'Unexpected Error When Getting All Softwares';
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


        //For Test $http Only
        vm.addNewWorkStation = function (wk) {
            //$log.info(vm.newWorkstation);
            $log.info("Add " + vm.newWorkstation);
            $http.post('/foxworkstations/addnewworkstation', vm.newWorkstation)
                .then(function(response) {
                    console.log(response);
                    UpdateWorkStationList($http,vm);
                    vm.message = 'Add New Workstation Successfully';
                });

        }

        vm.getSelectedWorkstationList = function (selected) {
            //$log.info("Before Resetting");
            //$log.info(vm.allWorkstations);
            angular.forEach(vm.allWorkstations, function(value, key) {
                value.isChecked = false;
            });
            //$log.info("After Resetting");
            //$log.info(vm.allWorkstations);
            var tempAllWks = vm.allWorkstations;
            
            $log.info(tempAllWks);

            $http.get('/foxsoftwares/GetWorkStationsBySoftId/' + selected.FoxSoftwareId)
                .then(function(response) {
                    /* Legacy Code
                    //var rev = response.data[0].Rev;
                    //$log.info(rev);
                    //var tempRev='';
                    
                    var revs = [rev];
                    //$log.info(revs.indexOf("x"));
                    
                    angular.forEach(response.data, function(value, key) {
                        var responseRev = value.Rev;
                        //$log.info(responseRev);
                        if (revs.indexOf(responseRev) === -1) {
                            revs.push(responseRev);
                        }
                    });
                    
                    $log.info(revs);
                    

                    //$log.info(response.data[0].Rev);
                    //console.log("Inside Rev Info:" + rev);
                    */
                    vm.revStations = [];
                    vm.data = response.data;
                    var data=vm.data;
                    console.log(vm.data);
                    for (var c = 0; c < data.length; c++) {
                        vm.revStations.push(vm.allWorkstations);
                    }
                    $log.info(vm.revStations);
                    for (var i = 0; i < data.length; i++) {
                        //$log.info(data[i].JoinTableData);
                        for (var j = 1; j < data[i].JoinTableData.length; j++) {
                            //$log.info(data[i].Rev);
                            //$log.info(data[i].JoinTableData[j].FoxWorkStationId);
                            var wkId = data[i].JoinTableData[j].FoxWorkStationId;
                            //$log.info(wkId);
                            var wkObject = tempAllWks;
                            //$log.info(wkObject);
                            angular.forEach(wkObject, function(value, key) {
                                //$log.info(value);
                                var id = value.WorkStationId;
                                //$log.info("Loop WKs:" + id);
                                //$log.info("The Key is " + key);
                                //$log.info("Original WK:" + wkId);
                                if (wkId === id) {
                                    //$log.info(vm.allWorkstations[key]);
                                    //$log.info(value.WorkStationId + " Before: " + tempAllWks[key].isChecked);
                                    tempAllWks[key].isChecked = true;
                                    //$log.info(value.WorkStationId + " After: " + tempAllWks[key].isChecked);
                                }
                            });

                        }
                    }

                    //vm.selectedSoftwareRev = rev;

                    //console.log("Inside SetRev Info: " + vm.selectedSoftwareRev);
                    //$log.info(rev);

                    vm.revCategory = response.data;
                    /*Legacy Code
                    angular.forEach(response.data, function (value, key) {
                        
                        //$log.info(value);

                        //response.data 是 WKAndFoxJoinTable物件

                        //var wkId = value.FoxWorkStationId;
                        
                        

                        //$log.info(wkId);
                        //$log.info("Is reponse.data an Object? " + angular.isObject(value));
                        //var wkObject = angular.fromJson(tempAllWks);
                        var wkObject = tempAllWks;
                        //$log.info(wkObject);
                        angular.forEach(wkObject, function (value, key) {
                            //$log.info(value);
                            var id = value.WorkStationId;
                            //$log.info("Loop WKs:" + id);
                            //$log.info("The Key is " + key);
                            //$log.info("Original WK:" + wkId);
                            if (wkId === id) {
                                //$log.info(vm.allWorkstations[key]);
                                //$log.info(value.WorkStationId + " Before: " + tempAllWks[key].isChecked);
                                tempAllWks[key].isChecked = true;
                               // $log.info(value.WorkStationId + " After: " + tempAllWks[key].isChecked);
                            }
                        });
                    });
                    //$log.info(tempAllWks);
                    
                    vm.allWorkstations = tempAllWks;
                    
                    //$log.info(tempAllWks);

                    //$log.info(vm.allWorkstations);
                }, function (errResponse) {
                    $log.error(errResponse);
                    vm.message = 'Unexpected Error While Getting Workstations By SoftId';
                });
                */
                    //$log.info(selected);
                    //console.log("Outside Rev Info:" + rev);

                    //console.log("Final Rev Info: "+vm.selectedSoftwareRev);
                    vm.allWorkstations = tempAllWks;

                });
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

        vm.modifyRev=function(record, rev) {
            //vm.selectedSoftwareInfo;
            var softRevInfo = {
                FoxSoftwareId: record.FoxSoftwareId,
                Rev:rev
            };
            $log.info(softRevInfo);
            $http.post('/foxsoftwares/UpdateSoftwareRev', softRevInfo)
                .then(function(response) {
                    $log.info(response);
                    vm.selectedSoftwareRev = response.data.Rev;
                });
        }
        }    
} )();
