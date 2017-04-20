(function () {
    'use strict';

    angular
        .module('drsApp')
        .controller('drsConfirmFormCtrl', numacConfirmFormCtrl);

    drsConfirmFormCtrl.$inject = ['$scope','drsDataService']; 

    function drsConfirmFormCtrl($scope,drsDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.message="等待伺服器回應中";

        drsDataService.getDrsChangeRequestPromise().then(function(res){
            vm.message="新增案件成功";
            vm.data=res.data;
            console.log(vm);
        },function(err){

        });
    }
})();
