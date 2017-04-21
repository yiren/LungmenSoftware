(function () {
    'use strict';

    angular
        .module('numacApp')
        .controller('numacConfirmFormCtrl', numacConfirmFormCtrl);

    numacConfirmFormCtrl.$inject = ['$scope','numacDataService']; 

    function numacConfirmFormCtrl($scope,numacDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.message="等待伺服器回應中";

        numacDataService.getNumacChangeRequestPromise().then(function(res){
            vm.message="新增案件成功";
            vm.data=res.data;
            console.log(vm);
        },function(err){

        });
    }
})();
