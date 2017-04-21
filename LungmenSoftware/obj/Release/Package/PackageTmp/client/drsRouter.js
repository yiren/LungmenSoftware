(function () {
    'use strict';

    angular
        .module("drsApp")
        .config(
            function($stateProvider, $urlRouterProvider) {


                $urlRouterProvider.otherwise('/');

                $stateProvider
                    //Get Numac Change Form
                    .state('form',
                    {
                        url: '/',
                        templateUrl: '/../client/templates/drs/drsChangeForm.html',
                        controller: 'drsFormCtrl',
                        controllerAs: 'f'
                    })
                    //
                    .state('drsConfirmForm',
                        {
                            url: '/drsConfirmForm',
                            templateUrl: '/../client/templates/drs/confirmresponse.html',
                            controller: 'drsConfirmFormCtrl',
                            controllerAs: 'c',
                            // resolve: {
                            //     numacFormData:['numacDataService', function(numacDataService){
                            //         return numacDataService.getNumacChangeRequestPromise();
                            //     } ]
                            //     }
                            resovle: {
                                numacDataForm: function() {

                                    return { value: "Hello" };
                                }
                            }
                        }
                    );
            });

})();