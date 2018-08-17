
angular
    .module('homer').controller('CreditorController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/CreditorApi/GetAllCreditors');
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
                    name: 'CreditorId', field:'CreditorId',
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
                {name:'Department',field:'SectorName'},
               {
                   name: 'CreatedOn', field: 'CreatedOn'
               },
               {
                   name: 'Branch', field: 'BranchName'
               },
               //{
               //    name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/creditors/edit/{{row.entity.CreditorId}}">Edit</a> </div>',
                  
               //},
            
            ];




        }]);



angular
    .module('homer').controller('BranchCreditorController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var branchId = $scope.branchId;
            var promise = $http.get('/webapi/CreditorApi/GetAllBranchCreditors?branchId=' + branchId, {});
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
                    name: 'CreditorId', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/creditors/edit/{{row.entity.CreditorId}}">{{row.entity.CreditorId}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.DESC,
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
               

            ];




        }]);