(function () {
    'use strict';

    angular
        .module("numacApp")
        
        .config(
                function ($stateProvider, $urlRouterProvider) {

                    
                    $urlRouterProvider.otherwise('/');
                    
                    $stateProvider
                        //Get Numac Change Form
                        .state('form', {
                            url: '/',
                            templateUrl: '/../client/templates/numac/drsChangeForm.html',
                            controller: 'numacFormCtrl',
                            controllerAs:'f'
                        })
                        //
                        .state('drsConfirmForm', {
                            url: '/drsConfirmForm',
                            templateUrl: '/../client/templates/numac/confirmresponse.html',
                            controller:'drsConfirmFormCtrl',
                            controllerAs:'d',
                            // resolve: {
                            //     numacFormData:['numacDataService', function(numacDataService){
                            //         return numacDataService.getNumacChangeRequestPromise();
                            //     } ]
                            //     }
                            resovle:{
                                numacDataForm:function(){
                                    // return numacDataService.getNumacChangeRequestPromise().then(
                                    //     function(res){
                                    //         return res.data;
                                    //     },function(err){

                                    //     }
                                    // );
                                   return {value:"Hello"};
                                }
                            }
                            }
                        )
                        
                    ;
                })

})();