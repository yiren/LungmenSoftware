(function () {
    'use strict';

    angular
        .module('numacApp')
        .controller('numacDataGridCtrl', numacDataGridController);

    numacDataGridController.$inject = ['$scope', '$http', '$log', 'uiGridConstants', ];

    function numacDataGridController($scope, $http, $log, uiGridConstants) {
        /* jshint validthis:true */
        var vm = this;
        

        //ui-grid欄位設定
        var colDef = [
                { name: "System", field: "ChassisName", enableColumnMenu: false },

                { name: "Panel", field: "Panel", enableColumnMenu: false },
                { name: "SubSystem", field: "SubSystem", enableColumnMenu: false },
                { name: "Module", field: "ChassisBoardName", enableColumnMenu: false },
                { name: "Socket Location", field: "SocketLocation", enableColumnMenu: false },
                { name: "Assembly", field: "Assembly", enableColumnMenu: false },
                { name: "Serial Number", field: "SerialNumber", enableColumnMenu: false },
                { name: "Program", field: "Program", enableColumnMenu: false },
                { name: "Rev", field: "Rev", enableColumnMenu: false },


                //{
                //    name: "檔案下載", enableColumnMenu: false, cellTemplate: '<div>' +
                //    '<p class="center"><a href="/meetings/download/?p={{row.entity.MeetingFiles[0].FileId}}{{row.entity.MeetingFiles[0].Extension}}&d={{row.entity.MeetingFiles[0].FileName}}"> <span class="fa fa-download"></span></a></p>' +
                //    '</div>'
                //}
        ];

        ////多個檔案
        //function attachmentFileLoop(row) {
        //    var cellTemplate = '';
        //    for (var i = 0; i < row.entity.MeetingFiles.length; i++) {

        //        cellTemplate.concat('<div>' +
        //        '<p class="center"><a href="/meetings/download/?p={{row.entity.MeetingFiles[i].FileId}}{{row.entity.MeetingFiles[i].Extension}}&d={{row.entity.MeetingFiles[i].FileName}}"> <span class="fa fa-download"></span></a></p>' +
        //        '</div>');
        //    }
        //    return cellTemplate;
        //}

        vm.searchTextToggle = "啟用";

        //ui-grid的相關設定
        vm.gridOptions = {
            enableFiltering: false,
            enableColumnResizing: true,
            enableSorting: true,
            paginationPageSize: 25,
            paginationPageSizes: [25, 50, 75],
            enableRowSelection: true,
            enableRowHeaderSelection: false,
            multiSelect: false,
            onRegisterApi: function (gridApi) {
                //console.log(gridApi);
                vm.gridApi = gridApi;

            },
            columnDefs: colDef



        }


        //取得Database中佈告資料
        $http.get('/chassis/getnumacdata').then(suc, err);
        function suc(res) {
            vm.gridOptions.data = res.data;

        }
        function err(err) {

        }


        vm.toggleFiltering = function () {
            //過濾狀態判斷
            vm.gridOptions.enableFiltering = !vm.gridOptions.enableFiltering;
            if (vm.gridOptions.enableFiltering == true) {
                vm.searchTextToggle = "關閉";
            } else {
                vm.searchTextToggle = "啟用";
            }
            //當輸入過濾條件後，自動render新資料
            vm.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
        };
    }
})();
