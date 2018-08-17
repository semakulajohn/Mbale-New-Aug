angular
    .module('homer')
    .controller('InventoryEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var transactionSubTypeId = 10005;
        var inventoryId = $scope.inventoryId;
        var action = $scope.action;
        var storeId = $scope.storeId;

      

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
            var promise = $http.get('/webapi/InventoryApi/GetInventory?inventoryId=' + inventoryId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.inventory = {
                        InventoryId: b.InventoryId,
                        ItemName: b.ItemName,
                        Amount: b.Amount,
                        StoreId: b.StoreId,
                        Description: b.Description,
                        BranchId: b.BranchId,
                        PurchaseDate: b.PurchaseDate != null ? moment(b.PurchaseDate).format('YYYY-MM-DD HH:mm:ss') : null,
                        TransactionSubTypeId: b.TransactionSubTypeId,
                        SectorId: b.SectorId,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };
                });


        }

        $scope.Save = function (inventory) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/InventoryApi/Save', {
                    InventoryId: inventoryId,
                    ItemName: inventory.ItemName,
                    Amount: inventory.Amount,
                    PurchaseDate: inventory.PurchaseDate,
                    Description: inventory.Description,
                    BranchId: inventory.BranchId,
                    StoreId: storeId,
                    SectorId: inventory.SectorId,
                    TransactionSubTypeId: transactionSubTypeId,
                    CreatedBy: inventory.CreatedBy,
                    CreatedOn: inventory.CreatedOn,
                    Deleted: inventory.Deleted
                });

                promise.then(
                    function (payload) {

                        inventoryId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('inventory-store-edit', { 'action': 'edit', 'inventoryId': inventoryId ,'storeId':storeId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('inventory-store', { 'storeId': storeId });
        };

        $scope.Delete = function (inventoryId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/InventoryApi/Delete?inventoryId=' + inventoryId, {});
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
    .module('homer').controller('InventoryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/InventoryApi/GetAllInventories');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: false,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'Id', field:'InventoryId',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    },
                    width:'5%'
                },
                { name: 'Item Name', field: 'ItemName',width:'10%' },
                { name: 'Description', field: 'Description' },
               
                { name: 'Amount', field: 'Amount' },
                { name: 'PurchaseDate', field: 'PurchaseDate' },
                { name: 'Branch Name', field: 'BranchName' },
                 { name: 'Sector Name', field: 'SectorName' },
                 { name: 'Store Name', field: 'StoreName' },
                   {
                       name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/inventories/edit/{{row.entity.InventoryId}}">Edit</a> </div>',
                     
                   },

            ];




        }]);



angular
    .module('homer').controller('StoreInventoryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var storeId = $scope.storeId;
            
            var promise = $http.get('/webapi/InventoryApi/GetAllInventoriesForParticularStore?storeId=' + storeId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedStoreId = $scope.storeId;

            $scope.gridData = {
                enableFiltering: false,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'Id', field:'InventoryId',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    },
                    width: '5%'
                },
                { name: 'Item Name', field: 'ItemName', width: '10%' },
                { name: 'Description', field: 'Description' },

                { name: 'Amount', field: 'Amount' },
                { name: 'PurchaseDate', field: 'PurchaseDate' },
                { name: 'Branch Name', field: 'BranchName' },
                 { name: 'Sector Name', field: 'SectorName' },
                  {
                      name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/inventories/edit/{{row.entity.InventoryId}}">Edit</a> </div>',

                  },

            ];




        }]);

