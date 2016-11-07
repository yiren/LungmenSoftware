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
                            templateUrl: '/../client/templates/numac/numacChangeForm.html',
                            controller: 'numacFormCtrl',
                            controllerAs:'f'
                        })
                        //
                        .state('numacConfirmForm', {
                            url: '/numacConfirmForm',
                            templateUrl: '/../client/templates/numac/confirmresponse.html',
                            controller:'numacConfirmFormCtrl',
                            controllerAs:'c',
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