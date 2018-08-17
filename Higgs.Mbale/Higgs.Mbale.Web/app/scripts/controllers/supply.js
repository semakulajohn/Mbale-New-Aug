angular
    .module('homer')
    .controller('SupplyEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        var supplierId = $scope.supplierId;
        var supplyId = $scope.supplyId;
        var action = $scope.action;

       
        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });

        $http.get('/webapi/StoreApi/GetAllStores').success(function (data, status) {
            $scope.stores = data;
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
            var promise = $http.get('/webapi/SupplyApi/GetSupply?supplyId=' + supplyId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.supply = {
                        SupplyId : b.SupplyId,
                        Quantity : b.Quantity,
                        SupplyDate: b.SupplyDate != null ? moment(b.SupplyDate).format('YYYY-MM-DD HH:mm:ss') : null,
                        SupplyNumber : b.SupplyNumber,
                        BranchId : b.BranchId,
                        SupplierId :b.SupplierId,
                        Amount: b.Amount,
                        Used: b.Used,
                        StoreId : b.StoreId,
                        MoistureContent: b.MoistureContent,
                        WeightNoteNumber: b.WeightNoteNumber,
                        NormalBags: b.NormalBags,
                        BagsOfStones: b.BagsOfStones,
                        TruckNumber : b.TruckNumber,
                        Price : b.Price,
                        CreatedOn :b.CreatedOn,
                        TimeStamp : b.TimeStamp,
                        CreatedBy : b.CreatedBy,
                        Deleted  : b.Deleted, 
                        UpdatedBy: b.UpdatedBy,

                    };
                });


        }

        $scope.Save = function (supply) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/SupplyApi/Save', {

                    SupplyId  : supplyId,
                    Quantity : supply.Quantity,
                    SupplyDate : supply.SupplyDate,
                    SupplyNumber : supply.SupplyNumber,
                    BranchId : supply.BranchId,
                    SupplierId : supplierId,
                    Amount: supply.Amount,
                    StoreId : supply.StoreId,
                    MoistureContent: supply.MoistureContent,
                    WeightNoteNumber: supply.WeightNoteNumber,
                    NormalBags: supply.NormalBags,
                    BagsOfStones: supply.BagsOfStones,
                    TruckNumber : supply.TruckNumber,
                    Price : supply.Price,
                    CreatedOn : supply.CreatedOn,
                    TimeStamp : supply.TimeStamp,
                    CreatedBy : supply.CreatedBy,
                    Deleted: supply.Deleted,
                   
                });

                promise.then(
                    function (payload) {

                        supplyId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('supplier-supply-edit', { 'action': 'edit', 'supplierId':supplierId,'supplyId': supplyId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('supplier-supply-list', { 'supplierId': supplierId});
           // $state.go('supplies.list');
        };

        $scope.Delete = function (supplyId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/SupplyApi/Delete?supplyId=' + supplyId, {});
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
    .module('homer').controller('SupplyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/SupplyApi/GetAllSupplies');
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
                    name: 'SupplyDate', 
                    sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    }
                },
                { name: 'Supply Number', field: 'SupplyNumber' },
                { name: 'Truck', field: 'TruckNumber' },
                { name: 'Supplier Name',field:'SupplierName'},
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Quantity(kgs)', field: 'Quantity' },
                { name: 'Price', field: 'Price' },
                { name: 'Amount', field: 'Amount' },
                { name: 'Used', field: 'Used' },
            { name: 'WeightNoteNumber', field: 'WeightNoteNumber' },
                 { name: 'MoistureContent', field: 'MoistureContent' },
                 { name: 'Normal Bags', field: 'NormalBags' },
                 { name: 'Stone Bags', field: 'BagsOfStones' },
                 { name: 'Status', field: 'StatusName' },
                
                 { name: 'Action',  cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/supplies/edit/{{row.entity.SupplyId}}">Edit</a> </div>' },

            ];




        }]);



angular
    .module('homer').controller('SupplierSupplyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {

            var supplierId = $scope.supplierId;
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/SupplyApi/GetAllSuppliesForAParticularSupplier?supplierId='+ supplierId,{});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedSupplierId = $scope.supplierId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'SupplyDate',
                    sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    }
                },
                { name: 'Supply Number', field: 'SupplyNumber' },
                { name: 'Truck', field: 'TruckNumber' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Quantity(kgs)', field: 'Quantity' },
                { name: 'Price', field: 'Price' },
                { name: 'Amount', field: 'Amount' },
                { name: 'Used', field: 'Used' },
                {name:'IsPaid',field:'IsPaid'},
                  { name: 'WeightNoteNumber', field: 'WeightNoteNumber' },
                 { name: 'MoistureContent', field: 'MoistureContent' },
                 { name: 'Normal Bags', field: 'NormalBags' },
                 { name: 'Stone Bags', field: 'BagsOfStones' },
                 { name: 'Status', field: 'StatusName' },

                  { name: 'Action',  cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/supplies/edit/' + supplierId + '/{{row.entity.SupplyId}}">Edit</a> </div>' },

            ];




        }]);


angular
    .module('homer').controller('SupplierUnPaidSupplyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', '$timeout', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, $timeout, Utils, uiGridConstants) {

            var supplierId = $scope.supplierId;
            $scope.supplyAmount = 0;
            $scope.selectedSupplies = null;
            var transactionSubTypeId = 30009;
            var sectorId = 2;
            var action = "-";
            $scope.loadingSpinner = true;
            $scope.isDisabled = false;

            var promise = $http.get('/webapi/SupplyApi/GetAllUnPaidSuppliesForAParticularSupplier?supplierId=' + supplierId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedSupplierId = $scope.supplierId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                {
                    name: 'SupplyDate',
                    sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    }
                },
                { name: 'Supply Number', field: 'SupplyNumber' },
                { name: 'Truck', field: 'TruckNumber' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Quantity(kgs)', field: 'Quantity' },
                { name: 'Price', field: 'Price', width:"5%" },
                { name: 'Amount', field: 'Amount' },
                { name: 'Used', field: 'Used',width:"5%"},
                {name:'IsPaid',field:'IsPaid'},
                  { name: 'WeightNoteNumber', field: 'WeightNoteNumber' },
                 { name: 'MoistureContent', field: 'MoistureContent' },
                 { name: 'Normal Bags', field: 'NormalBags' },
                 { name: 'Stone Bags', field: 'BagsOfStones' },
                 { name: 'Status', field: 'StatusName' },
                  { name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/supplies/edit/' + supplierId + '/{{row.entity.SupplyId}}">Edit</a> </div>' },



            ];


            
            $http.get('/webapi/AccountTransactionActivityApi/GetAllPaymentModes').success(function (data, status) {
                $scope.paymentModes = data;
            });

            $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
                $scope.branches = data;
            });

            $scope.currentFocused = "";

           
            $scope.UpdateSupplyAmount = function (value) {
                $scope.supplyAmount += value;
                $scope.supply.PaymentAmount = $scope.supplyAmount;
                //console.log($scope.supply.PaymentAmount);

            };
            $scope.DecreaseSupplyAmount = function (value) {
                $scope.supplyAmount -= value;
                $scope.supply.PaymentAmount = $scope.supplyAmount;
               // console.log($scope.supply.PaymentAmount);

            };
            $scope.supply = {
               
                PaymentAmount:"",
                

            };
           
            $scope.gridData.onRegisterApi = function (gridApi) {
                $scope.gridApi = gridApi;

                gridApi.selection.on.rowSelectionChanged($scope, function (row) {
                    if (row.isSelected) {
                        $scope.UpdateSupplyAmount(row.entity.Amount);
                    }
                    else {
                        $scope.DecreaseSupplyAmount(row.entity.Amount);
                    }
                    var msg = 'row selected ' + row.isSelected;
                  
                });

                   
            };


                 
            
            $scope.PaySupply = function () {
                $scope.isDisabled = true;
                $scope.selectedSupplies = $scope.gridApi.selection.getSelectedRows($scope.gridData);
                if ($scope.selectedSupplies != null) {

                    var accountActivity = {

                        Amount: $scope.supply.PaymentAmount,
                        SectorId: sectorId,
                        Notes : $scope.supply.Notes,
                        BranchId : $scope.supply.BranchId,
                        TransactionSubTypeId : transactionSubTypeId,
                        PaymentModeId: $scope.supply.PaymentModeId,
                        Deleted: false,
                        AspNetUserId: supplierId,
                        Action : action,
                    };

                    var promise = $http.post('/webapi/SupplyApi/PayMultipleSupplies', { Supplies: $scope.selectedSupplies, AccountActivity: accountActivity });
                    promise.then(
                        function (payload) {
                            var Id = payload.data;
                            $scope.showMessagePaymentMade = true;

                            $timeout(function () {
                                $scope.showMessagePaymentMade = false;
                                $state.go('supplier-supply-list', { 'supplierId': supplierId });
                                 }, 1500);
                            
                        });
                }
            }



        }]);

angular
    .module('homer').controller('SupplierPaidSupplyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {

            var supplierId = $scope.supplierId;
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/SupplyApi/GetAllPaidSuppliesForAParticularSupplier?supplierId=' + supplierId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedSupplierId = $scope.supplierId;
            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'SupplyDate',
                    sort: {
                        direction: uiGridConstants.DESC,
                        priority: 1
                    }
                },
                { name: 'Supply Number', field: 'SupplyNumber' },
                { name: 'Truck', field: 'TruckNumber' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Quantity(kgs)', field: 'Quantity' },
                { name: 'Price', field: 'Price' },
                { name: 'Amount', field: 'Amount' },
                { name: 'Used', field: 'Used' },
                { name: 'IsPaid', field: 'IsPaid' },
                  { name: 'WeightNoteNumber', field: 'WeightNoteNumber' },
                 { name: 'MoistureContent', field: 'MoistureContent' },
                 { name: 'Normal Bags', field: 'NormalBags' },
                 { name: 'Stone Bags', field: 'BagsOfStones' },
                 { name: 'Status', field: 'StatusName' },
                  { name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/supplies/edit/' + supplierId + '/{{row.entity.SupplyId}}">Edit</a> </div>' },


            ];




        }]);

angular
    .module('homer').controller('StoreMaizeStockController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var storeId = $scope.storeId;

            var promise = $http.get('/webapi/SupplyApi/GetAllMaizeStocksForAparticularStore?storeId=' + storeId, {});
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
             name: 'Supply Number', field: 'SupplyNumber'
         },
         
         { name: 'Start Stock', field: 'StartStock' },
          
         { name: 'Quantity', field: 'Quantity' },
         { name: 'End Stock', field: 'StockBalance' },

        {
            name: 'CreatedOn', field: 'TimeStamp',
            sort: {
                direction: uiGridConstants.DESC,
                priority: 1
            }

        }
            ];




        }]);
