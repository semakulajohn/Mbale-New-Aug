angular
    .module('homer')
    .controller('MachineRepairEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        var transactionSubTypeId = 3;
        var machinerepairId = $scope.machinerepairId;
        var action = $scope.action;
        var batchId = $scope.batchId;

      
        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });
        $http.get('/webapi/SectorApi/GetAllSectors').success(function (data, status) {
            $scope.sectors = data;
        });


        $http.get('/webapi/TransactionSubTypeApi/GetAllTransactionSubTypes').success(function (data, status) {
            $scope.transactionSubTypes = data;
        });

        if (action == 'create') {
            machinerepairId = 0;
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
            var promise = $http.get('/webapi/MachineRepairApi/GetMachineRepair?machinerepairId=' + machinerepairId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.machineRepair = {                       
                        MachineRepairId: b.MachineRepairId,
                      
                        Amount: b.Amount,
                        NameOfRepair: b.NameOfRepair,
                        DateRepaired: b.DateRepaired != null ? moment(b.DateRepaired).format('YYYY-MM-DD HH:mm:ss') : null,
                        BranchId: b.BranchId,
                        Description : b.Description,
                        SectorId: b.SectorId,
                        BatchId : b.BatchId,
                        TransactionSubTypeId : b.TransactionSubTypeId,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };
                });
        }

        $scope.Save = function (machineRepair) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/MachineRepairApi/Save', {
                    MachineRepairId: machinerepairId,
                   
                    Amount: machineRepair.Amount,
                    NameOfRepair: machineRepair.NameOfRepair,
                    DateRepaired: machineRepair.DateRepaired,
                    BranchId: machineRepair.BranchId,
                    BatchId: batchId,
                    Description : machineRepair.Description,
                    TransactionSubTypeId : transactionSubTypeId,
                    SectorId : machineRepair.SectorId,
                    CreatedBy: machineRepair.CreatedBy,
                    CreatedOn: machineRepair.CreatedOn,
                    Deleted: machineRepair.Deleted
                });

                promise.then(
                    function (payload) {
                        machinerepairId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('machineRepair-batch-edit', { 'action': 'edit', 'machinerepairId': machinerepairId ,'batchId':batchId});
                            }

                        }, 1500);
                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('machineRepair-batch', { 'batchId': $scope.batchId });
        };

        $scope.Delete = function (machinerepairId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/MachineRepairApi/Delete?machinerepairId=' + machinerepairId, {});
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
    .module('homer').controller('BatchMachineRepairController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var batchId = $scope.batchId;

            var promise = $http.get('/webapi/MachineRepairApi/GetAllMachineRepairsForAParticularBatch?batchId=' + batchId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedBatchId = $scope.batchId;

            $scope.gridData = {
                enableFiltering: false,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

           

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'Repairer Name',field:'NameOfRepair',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Amount ', field: 'Amount' },
                { name: 'Description', field: 'Description' },
                { name: 'Date Repair Was  Made', field: 'DateRepaired' },
                { name: 'Branch Name', field: 'BranchName' },
                 {
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/machineRepairs/edit/{{row.entity.MachineRepairId}}/' + $scope.batchId + '">Edit</a> </div>',
                     
                 },
            ];
        }]);


angular
    .module('homer').controller('MachineRepairController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/MachineRepairApi/GetAllMachineRepairs');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };
            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                {
                    name: 'Repairer Name', field:'NameOfRepair',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
               
                { name: 'Amount ', field: 'Amount' },
                { name : 'Description',field: 'Description'},
                { name: 'Date Repair Was  Made', field: 'DateRepaired' },               
                { name: 'Branch Name', field: 'BranchName' },
                 {
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/machinerepairs/edit/{{row.entity.MachineRepairId}}">Edit</a> </div>',
                   
                 },

            ];
        }]);

