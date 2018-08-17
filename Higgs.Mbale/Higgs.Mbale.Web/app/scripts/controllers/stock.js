

angular
    .module('homer').controller('StockController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/StockApi/GetAllStocks');
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
                    name: 'Product Name', field:'ProductName'

                },
                { name: 'Batch Number', field: 'BatchNumber' },
                { name: 'In/Out', field: 'StockInOrOut' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Stock Details', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/stock/detail/{{row.entity.StockId}}">Stock Details</a> </div>' },
            

            ];




        }]);


angular
    .module('homer')
    .controller('StockDetailController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$state',
    function ($scope, $http, $filter, $location, $log, $timeout, $state) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var stockId = $scope.stockId;

        var promise = $http.get('/webapi/StockApi/GetStock?stockId=' + stockId, {});
        promise.then(
            function (payload) {
                var b = payload.data;
                $scope.stock = {
                    StockId: b.StockId,
                    BatchNumber: b.BatchNumber,
                    ProductId: b.ProductId,
                    BranchId: b.BranchId,
                    SectorId : b.SectorId,
                    SectorName : b.SectorName,
                    TimeStamp: b.TimeStamp,
                    CreatedOn: b.CreatedOn,
                    CreatedBy: b.CreatedBy,
                    UpdatedBy: b.UpdatedBy,
                    Deleted: b.Deleted,
                    Grades: b.Grades,
                    InOrOut: b.InOrOut,
                    BranchName: b.BranchName,
                    ProductName: b.ProductName,
                    ProductOutPut : b.ProductOutPut
                };
            });

    }]);


angular
    .module('homer').controller('StoreStockController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var storeId = $scope.storeId;
            
            var promise = $http.get('/webapi/StockApi/GetAllStocksForAparticularStore?storeId=' + storeId, {});
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
             name: 'Product Name', field: 'ProductName'
         },
         { name: 'Batch Number', field: 'BatchNumber' },
       
         { name: 'Start Stock', field: 'StartStock' },
           { name: 'In/Out', field: 'StockInOrOut' },

         {name:'Quantity',field:'Quantity'},
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
