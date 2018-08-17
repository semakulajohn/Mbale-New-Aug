angular
    .module('homer')
    .controller('AccountTransactionActivityEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        var accountId = $scope.accountId;
        var transactionActivityId = $scope.transactionActivityId;
        var action = $scope.action;

        $scope.actions = ["+", "-"];
        
         $http.get('/webapi/TransactionSubTypeApi/GetAllTransactionSubTypes').success(function (data, status) {
            $scope.transactionSubTypes = data;
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
            var promise = $http.get('/webapi/AccountTransactionActivityApi/GetAccountTransactionActivity?transactionActivityId=' + transactionActivityId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.transactionActivity = {
                        AccountTransactionActivityId: b.AccountTransactionActivityId,
                        Notes: b.Notes,
                        Balance: b.Balance,
                        TransactionSubTypeId: b.TransactionSubTypeId,
                        BranchId: b.BranchId,
                        SectorId:b.SectorId,
                        CasualWorkerId: b.CasualWorkerId,
                        AspNetUserId : b.AspNetUserId,
                        Amount: b.Amount,
                        StartAmount: b.StartAmount,
                        Action: b.Action,
                        CreatedOn: b.CreatedOn,
                        TimeStamp: b.TimeStamp,
                        CreatedBy: b.CreatedBy,
                        Deleted: b.Deleted,
                        UpdatedBy: b.UpdatedBy,

                    };
                });


        }

        $scope.Save = function (transactionActivity) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/AccountTransactionActivityApi/Save', {

                    AccountTransactionActivityId: transactionActivityId,
                    Amount: transactionActivity.Amount,
                    StartAmount: transactionActivity.StartAmount,
                    Balance: transactionActivity.Balance,
                    PaymentModeId : transactionActivity.PaymentModeId,
                    BranchId: transactionActivity.BranchId,
                    AspNetUserId: accountId,
                    CasualWorkerId: accountId,
                    SectorId : transactionActivity.SectorId,
                    Notes: transactionActivity.Notes,
                    Action: transactionActivity.Action,
                    TransactionSubTypeId: transactionActivity.TransactionSubTypeId,
                    CreatedOn: transactionActivity.CreatedOn,
                    TimeStamp: transactionActivity.TimeStamp,
                    CreatedBy: transactionActivity.CreatedBy,
                    Deleted: transactionActivity.Deleted,

                });

                promise.then(
                    function (payload) {

                        transactionActivityId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('account-accounttransactionactivities-edit', { 'action': 'edit', 'accountId': accountId, 'transactionActivityId': transactionActivityId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
           
            $state.go('account-accounttransactionactivities-list', { 'accountId': accountId });
        };

        $scope.Delete = function (transactionActivityId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/AccountTransactionActivityApi/Delete?transactionActivityId=' + transactionActivityId, {});
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
    .module('homer').controller('AccountTransactionActivityController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/AccountTransactionActivityApi/GetAllAccountTransactionActivities');
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
                    name: 'AccountTransactionActivityId',
                    sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    }
                },
                { name: 'Account', field: 'AccountName'},
                { name: 'TransactionSubTypeName', field: 'TransactionSubTypeName' },
                { name: 'Notes', field: 'Notes', field:'Notes'},
                { name: 'Start Amount', field: 'StartAmount' },
                { name: 'Action', field: 'Action' },
               
                { name: 'Amount', field: 'Amount' },
                 { name: 'Balance', field: 'Balance' },
                 { name: 'Branch', field: 'BranchName' },
                 { name: 'Department', field: 'SectorName' },

            ];




        }]);



angular
    .module('homer').controller('AccountAccountTransactionActivityController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {

            var accountId = $scope.accountId;
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/AccountTransactionActivityApi/GetAllAccountTransactionActivitiesForAParticularAccount?accountId=' + accountId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                    $scope.Length = payload.data.length;
                    if ($scope.Length > 0) {

                        var lastIndex = $scope.Length - 1;
                        $scope.accountBalance = payload.data[lastIndex].Balance;
                    }
                    else {
                        $scope.accountBalance = 0;
                    }
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

            
                 
                { name: 'Account', field: 'AccountName' },
                { name: 'TransactionSubTypeName', field: 'TransactionSubTypeName' },
                { name: 'Notes', field: 'Notes'},
                { name: 'Start Amount', field: 'StartAmount' },
                { name: 'Action', field: 'Action' },

                { name: 'Amount', field: 'Amount' },
                 { name: 'Balance', field: 'Balance' },
                 {
                     name: 'CreatedOn', field: 'CreatedOn',
                     sort: {
                         direction: uiGridConstants.DESC,
                         priority: 1
                     }
                 },


            ];




        }]);

