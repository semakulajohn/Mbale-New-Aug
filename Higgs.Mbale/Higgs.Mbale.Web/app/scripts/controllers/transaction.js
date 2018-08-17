
angular
    .module('homer').controller('TransactionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/TransactionApi/GetAllTransactions');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    
                        name: 'TransactionId', field:'TransactionId',
                        sort: {
                            direction: uiGridConstants.DESC,
                            priority: 1
                        }
                    },
                
                { name: 'Amount ', field: 'Amount' },
                { name: 'Transaction SubType', field: 'TransactionSubTypeName' },               
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Sector Name', field: 'SectorName' },
                {name:'Created On',field:'CreatedOn'}
                
              


            ];




        }]);



angular
    .module('homer').controller('TransactionTypeTransactionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {

            $scope.loadingSpinner = true;
            var transactionTypeId = $scope.transactionTypeId;
            var promise = $http.get('/webapi/TransactionApi/GetAllTransactionsForAParticularTransactionType?transactionTypeId=' + transactionTypeId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {

                    name: 'TransactionId', field: 'TransactionId',
                    sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    }
                },
               
                { name: 'Amount ', field: 'Amount' },
                { name: 'Transaction SubType', field: 'TransactionSubTypeName' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Sector Name', field: 'SectorName' },
                { name: 'Created On', field: 'CreatedOn' }




            ];




        }]);

angular
    .module('homer')
    .controller('TransactionEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var transactionId = $scope.transactionId;
        var action = $scope.action;

      

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
            machineRepairId = 0;
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
            var promise = $http.get('/webapi/TransactionApi/GetTransaction?transactionId=' + transactionId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.transaction = {
                        TransactionId: b.TransactionId,
                        Amount: b.Amount,
                        BranchId: b.BranchId,
                        SectorId: b.SectorId,
                        TransactionSubTypeId: b.TransactionSubTypeId,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };
                });
        }

        $scope.Save = function (transaction) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/TransactionApi/Save', {
                    TransactionId: transactionId,
                    Amount: transaction.Amount,
                    BranchId: transaction.BranchId,
                    TransactionSubTypeId: transaction.TransactionSubTypeId,
                    SectorId: transaction.SectorId,
                    CreatedBy: transaction.CreatedBy,
                    CreatedOn: transaction.CreatedOn,
                    Deleted: transaction.Deleted
                });

                promise.then(
                    function (payload) {
                        transactionId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('transaction-edit', { 'action': 'edit', 'transactionId': transactionId });
                            }

                        }, 1500);
                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('transactions.list');
        };

        $scope.Delete = function (transactionId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/TransactionApi/Delete?transactionId=' + transactionId, {});
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
    .module('homer').controller('BranchTransactionController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var branchId = $scope.branchId;
            var promise = $http.get('/webapi/TransactionApi/GetAllTransactionsForAParticularBranch?branchId=' + branchId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {

                    name: 'TransactionId', field: 'TransactionId',
                    sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    }
                },

                { name: 'Amount ', field: 'Amount' },
                { name: 'Transaction SubType', field: 'TransactionSubTypeName' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Sector Name', field: 'SectorName' },
                { name: 'Created On', field: 'CreatedOn' }




            ];




        }]);

