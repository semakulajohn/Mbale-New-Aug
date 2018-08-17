angular
    .module('homer')
    .controller('AccountCasualActivityEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval','selectBatchService',
function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval,selectBatchService) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        var accountId = $scope.accountId;
        var casualActivityId = $scope.casualActivityId;
        var action = $scope.action;
        var transactionSubTypeId = 10006;
        $scope.showMessageBatchSelected = false;

        var selectedAction = "+";

        $http.get('/webapi/ActivityApi/GetAllActivities').success(function (data, status) {
            $scope.activities = data;
        });

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });


        $http.get('/webapi/SectorApi/GetAllSectors').success(function (data, status) {
            $scope.sectors = data;
        });

        if (action == 'create') {
            deliveryId = 0;
            var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
            promise.then(
                function (payload) {
                    var c = payload.data;
                    $scope.user = {
                        UserName: c.UserName,
                        Id: c.Id,
                        FirstName: c.FirstName,
                        LastName: c.LastName,
                        UserRoles: c.UserRoles,
                    };
                }

            );
        }

        if (action == 'edit') {
            var promise = $http.get('/webapi/CasualActivityApi/GetCasualActivity?casualActivityId=' + casualActivityId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.casual = {
                        CasualActivityId: b.CasualActivityId,
                        Notes: b.Notes,
                        ActivityId : b.ActivityId,
                        TransactionSubTypeId: b.TransactionSubTypeId,
                        BranchId: b.BranchId,
                        Quantity: b.Quantity,
                        SectorId: b.SectorId,
                        CasualWorkerId: b.CasualWorkerId,
                        AspNetUserId: b.AspNetUserId,
                        Amount: b.Amount,
                        CreatedOn: b.CreatedOn,
                        TimeStamp: b.TimeStamp,
                        CreatedBy: b.CreatedBy,
                        Deleted: b.Deleted,
                        UpdatedBy: b.UpdatedBy,

                    };
                });


        }

        $scope.selectBatch = function () {
            $scope.modalInstance = $modal.open({
                templateUrl: '/app/views/modal/modalBatch.html' + CACHE_BUST_SUFFIX,
                size: 'large',
                controller: 'ModalInstanceCtrl',
                resolve: {
                    items: function () {
                        $scope.dataItems = {

                        }
                        return $scope.dataItems;
                    },
                },
                windowClass: 'contentModalBox'
            });
        };

        $scope.$on('selectBatchServiceBroadcastHandler', function () {


            $scope.selectedBatch = [];
            $scope.filteredBatch = [];
            var currentDate = new Date();

            selectBatchService.item.forEach(function (batch) {
                var batchObject = {
                    BatchId: batch.BatchId,
                   
                };

                $scope.selectedBatch.push(batchObject);


            });
            $scope.showMessageBatchSelected = true;
            $timeout(function () {
                $scope.showMessageSave = false;
                //$scope.showMessageSupplySelected = false;
                if ($scope.modalInstance != null)
                    $scope.modalInstance.close();
            }, 1500);

        });

        $scope.Save = function (casual) {
            if (($scope.selectedBatch == null || $scope.selectedBatch == undefined)) {
                $scope.showMessageBatchNotSelected = true;
                $timeout(function () {
                    $scope.showMessageBatchNotSelected = false;

                }, 2000);

            }
            else {
                $scope.showMessageSave = false;
                if ($scope.form.$valid) {
                    var promise = $http.post('/webapi/CasualActivityApi/Save', {

                        CasualActivityId: casualActivityId,
                        Amount: casual.Amount,
                        ActivityId: casual.ActivityId,
                        Quantity: casual.Quantity,
                        BranchId: casual.BranchId,
                        CasualWorkerId: accountId,
                        BatchId: $scope.selectedBatch[0].BatchId,
                        SectorId: casual.SectorId,
                        TransactionSubTypeId: transactionSubTypeId,
                        CreatedOn: casual.CreatedOn,
                        TimeStamp: casual.TimeStamp,
                        Action: selectedAction,
                        Notes: casual.Notes,
                        CreatedBy: casual.CreatedBy,
                        Deleted: casual.Deleted,

                    });

                    promise.then(
                        function (payload) {

                            casualActivityId = payload.data;
                            $scope.showMessageSave = true;

                            $timeout(function () {
                                $scope.showMessageSave = false;

                                if (action == "create") {
                                    $state.go('casualactivity-edit', { 'action': 'edit', 'accountId': accountId, 'casualActivityId': casualActivityId });
                                }

                            }, 1500);


                        });
            }
           
            }

        }



        $scope.Cancel = function () {

            $state.go('casualActivities-list', { 'accountId': accountId });
        };

        $scope.Delete = function (casualActivityId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/CasualActivityApi/Delete?casualActivityId=' + casualActivityId, {});
            promise.then(
                function (payload) {
                    $scope.showMessageDeleted = true;
                    $timeout(function () {
                        $scope.showMessageDeleted = false;
                        $scope.Cancel();
                    }, 2500);
                    $scope.showMessageDeleteFailed = false;
                },
                function (errorPayload) {
                    $scope.showMessageDeleteFailed = true;
                    $timeout(function () {
                        $scope.showMessageDeleteFailed = false;
                        $scope.Cancel();
                    }, 1500);
                });
        }


    }
    ]);


angular
    .module('homer').controller('AccountCasualActivityController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {

            var accountId = $scope.accountId;
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/CasualActivityApi/GetAllCasualActivitiesForAParticularCasualWorker?casualWorkerId=' + accountId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                   
                }
            );
            $scope.retrievedAccountId = $scope.accountId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;
              
            $scope.gridData.columnDefs = [
               
                { name: 'Activity', field: 'ActivityName' },
                {name:'Quantity',field:'Quantity'},
               {name:'Batch',field:'BatchNumber'},
               
                { name: 'Amount', field: 'Amount' },
                {name:'Branch',field:'BranchName'},
                 { name: 'Department', field: 'SectorName' },
                 {
                     name: 'CreatedOn', field: 'CreatedOn',
                     sort: {
                         direction: uiGridConstants.DESC,
                         priority: 1
                     }
                 },


            ];




        }]);


angular
    .module('homer')
    .controller("BatchesPickerController", ['$scope', '$modal', '$http', '$timeout', '$filter', 'Utils', '$window', 'selectBatchService', '$sessionStorage', 'uiGridConstants',
function ($scope, $modal, $http, $timeout, $filter, Utils, $window, selectBatchService, $sessionStorage, uiGridConstants) {


    $scope.gridData = {
        enableFiltering: true,
        columnDefs: $scope.columns,
        enableRowSelection: false
    };

    $scope.gridData.multiSelect = false;

    $scope.gridData.columnDefs = [

        {
            name: 'Batch Number', field:'Name',
            sort: {
                direction: uiGridConstants.ASC,
                priority: 1
            }
        },
    

    ];
    $scope.gridData.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
    };




    $scope.loadingSpinner = true;
    var promise = $http.get('/webapi/BatchApi/GetAllBatches');
    promise.then(
        function (payload) {
            $scope.gridData.data = payload.data;
            $scope.loadingSpinner = false;
        }
    );

    $scope.selectBatch = function () {
        $scope.selectedBatch = $scope.gridApi.selection.getSelectedRows($scope.gridData);
        if ($scope.selectedBatch != null) {
            selectBatchService.prepForBroadcast($scope.selectedBatch);

        }
    }


}]);
