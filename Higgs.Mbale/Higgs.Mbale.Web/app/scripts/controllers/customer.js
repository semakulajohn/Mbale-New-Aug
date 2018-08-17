angular
    .module('homer').controller('CustomerController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/CustomerApi/GetAllCustomers');
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
                    name: 'First Name', field: 'FirstName',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'LastName', field: 'LastName', width: '15%', },

                { name: 'Email', field: 'Email' },
                 { name: 'PhoneNumber', field: 'PhoneNumber', width: '15%', },
                   { name: 'Action', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/orders/{{row.entity.Id}}">Manage Orders</a></div>' },
                      { name: 'Account', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/accounttransactionactivities/{{row.entity.Id}}">Manage Account</a></div>' },


            ];




        }]);