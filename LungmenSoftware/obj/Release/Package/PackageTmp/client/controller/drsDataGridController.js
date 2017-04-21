(function () {
    'use strict';

    angular
        .module('drsApp')
        .controller('drsDataGridCtrl', drsDataGridController);

    drsDataGridController.$inject = ['$scope', '$log', 'uiGridConstants', 'drsDataService'];
    
    function drsDataGridController($scope, $log, uiGridConstants, drsDataService) {
        /* jshint validthis:true */
        var vm = this;
        

        function init() {
            
            drsDataService.getDrsData().then(suc, err);
            function suc(res) {
                vm.gridOptions.data = res.data;
                
            }
            function err(err) {

            }
            //ui-grid欄位設定
            var colDef = [
                    
                    { name: "System",width:75, field: "SystemName", enableColumnMenu: true },
                    { name: "Panel", width: 130, field: "DRSPanelName", enableColumnMenu: true },
                    { name: "FID", width: 150, field: "FIDDiagramNo", enableColumnMenu: true },
                    { name: "DIV", width: 50, field: "Division", enableColumnMenu: false },
                    { name: "Description", field: "Description", enableColumnMenu: false },
                    { name: "Rev", width: 50, field: "EPROMRev", enableColumnMenu: false },
                    { name: "Checksum", width: 100, field: "Checksum", enableColumnMenu: false },
                    {
                        name: "Note", width: 100, enableColumnMenu: false, cellTemplate: '<div>' +
                       '<a class="btn btn-default" href="/drs/drsChangeRequestById?fidId={{row.entity.FidId}}" target="_blank">修改歷史紀錄</a>' +
                    //    '<p class="center"><a href="/meetings/download/?p={{row.entity.MeetingFiles[0].FileId}}{{row.entity.MeetingFiles[0].Extension}}&d={{row.entity.MeetingFiles[0].FileName}}"> <span class="fa fa-download"></span></a></p>' +
                       '</div>'
                    }

                     //ui-grid的相關設定
                   
            ];

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
            
        }

        init();

        
        $scope.getHistory=function(row){
            drsDataService.getDrsChangeRequestRecordById(row.entity.FidId).then(
                function(res){
                    console.log(res.data);
                    vm.changeRequestRecord=res.data;
                },function(err){

                }
            );

            
        }

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

        vm.toggleFiltering = function () {
            //過濾狀態判斷
            vm.gridOptions.enableFiltering = !vm.gridOptions.enableFiltering;
            if (vm.gridOptions.enableFiltering === true) {
                vm.searchTextToggle = "關閉";
            } else {
                vm.searchTextToggle = "啟用";
            }
            //當輸入過濾條件後，自動render新資料
            vm.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
        };
    }
})();
