
angular
    .module('homer').controller('DebtorController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/DebtorApi/GetAllDebtors');
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
                    name: 'DebtorId', field:'DebtorId',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                {
                    name: 'AccountName', field: 'AccountName'
                },
                {
                    name: 'Amount', field: 'Amount'
                },
                { name: 'Department', field: 'SectorName' },
               {
                   name: 'CreatedOn', field: 'CreatedOn'
               },
               
               {
                   name: 'Branch', field: 'BranchName'
               },
               {
                   name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/debtors/edit/{{row.entity.DebtorId}}">Edit</a> </div>',
                 
               },
            ];




        }]);


angular
    .module('homer').controller('BranchDebtorController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var branchId = $scope.branchId;
            var promise = $http.get('/webapi/DebtorApi/GetAllBranchDebtors?branchId=' + branchId, {});
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
                    name: 'DebtorId', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/debtors/edit/{{row.entity.DebtorId}}">{{row.entity.DebtorId}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                {
                    name: 'AccountName', field: 'AccountName'
                },
                {
                    name: 'Amount', field: 'Amount'
                },
                { name: 'Department', field: 'SectorName' },
               {
                   name: 'CreatedOn', field: 'CreatedOn'
               },

               {
                   name: 'Branch', field: 'BranchName'
               },
               //{ name: 'Action', field: 'Action' },

            ];




        }]);

