(function() {
    angular.module('simpleApp', [])
    .controller('MainCtrl', ['$http','$log', MainCtrl]
        
     );

    function MainCtrl($http, $log) {
        var vm = this;
        vm.message = 'Before Getting Data';
        $http.get('/foxsoftwares/getsystemsoftwarelist')
               .success(function (data) {
                   vm.message = 'Data Retrieves Successfully';
                   vm.softwareList = data;
                   $log.info(data);
               })
               .error(function () {
                   vm.message = 'Unexpected Error When Getting All Softwares';
               });
        $http.get('/foxsoftwares/getwkbysoftid')
            .success(function(data) {
                vm.relatedWorkstations = data;
                $log.info("Installed Workstations Data " + data);
            })
            .error(function() {
                vm.message = 'Unexpected Error While Getting Installed Workstations';
            });
        vm.requestWorkStation=function() {
            $http.get('/foxsoftwares/getsystemsoftwarelist')
                .success(function(data) {
                    vm.message = 'Data Retrieves Successfully';
                    vm.softwareList = data;
                    $log.info(data);
                })
                .error(function() {
                    vm.message = 'Unexpected Error';
                });
        }


        }    
} )();
