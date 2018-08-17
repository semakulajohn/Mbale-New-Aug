angular
    .module('homer')
    .controller('FactoryExpenseEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

       // var transactionSubTypeId = 10006;
        var factoryExpenseId = $scope.factoryExpenseId;
        var action = $scope.action;
        var batchId = $scope.batchId;

       

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });

        $http.get('/webapi/SectorApi/GetAllSectors').success(function (data, status) {
            $scope.sectors = data;
        });

       
        if (action == 'create') {
            inventoryId = 0;
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
            var promise = $http.get('/webapi/FactoryExpenseApi/GetFactoryExpense?factoryExpenseId=' + factoryExpenseId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.factoryExpense = {
                        FactoryExpenseId: b.FactoryExpenseId,
                        BatchId : b.BatchId,
                        Amount: b.Amount,
                        BatchId: b.BatchId,
                        Description: b.Description,
                        BranchId: b.BranchId,
                        SectorId: b.SectorId,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };
                });


        }

        $scope.Save = function (factoryExpense) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/FactoryExpenseApi/Save', {
                    FactoryExpenseId: factoryExpenseId,
                    Amount: factoryExpense.Amount,
                    
                    Description: factoryExpense.Description,
                    BranchId: factoryExpense.BranchId,
                    BatchId: batchId,
                    SectorId: factoryExpense.SectorId,
                    CreatedBy: factoryExpense.CreatedBy,
                    CreatedOn: factoryExpense.CreatedOn,
                    Deleted: factoryExpense.Deleted
                });

                promise.then(
                    function (payload) {

                        factoryExpenseId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('factoryExpense-batch-edit', { 'action': 'edit', 'factoryExpenseId': factoryExpenseId, 'batchId': batchId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('factoryExpense-batch', { 'batchId': batchId });
        };

        $scope.Delete = function (factoryExpenseId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/FactoryExpenseApi/Delete?factoryExpenseId=' + factoryExpenseId, {});
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
    .module('homer').controller('BatchFactoryExpenseController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var batchId = $scope.batchId;

            var promise = $http.get('/webapi/FactoryExpenseApi/GetAllFactoryExpensesForAParticularBatch?batchId=' + batchId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedBatchId = $scope.batchId;

            $scope.gridData = {
                enableColumnResizing: true,
                enableFiltering: false,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;
           

            $scope.gridData.columnDefs = [

                {
                    name: 'Id', field:'FactoryExpenseId',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    },
                   // width: '5%'
                },
                { name: 'Description', field: 'Description'},

                { name: 'Amount', field: 'Amount' },
                { name: 'CreatedOn', field: 'CreatedOn' },
                { name: 'Branch Name', field: 'BranchName' },
                 { name: 'Department', field: 'SectorName' },
                   {
                       name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/factoryExpenses/edit/{{row.entity.FactoryExpenseId}}/' + $scope.batchId + '">Edit</a> </div>',
                                          },

            ];




        }]);

