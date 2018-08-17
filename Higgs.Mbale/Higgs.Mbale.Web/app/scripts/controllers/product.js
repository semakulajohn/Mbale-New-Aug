angular
    .module('homer')
    .controller('ProductEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var productId = $scope.productId;
        var action = $scope.action;




        if (action == 'create') {
            productId = 0;

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
            var promise = $http.get('/webapi/ProductApi/Getproduct?productId=' + productId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.product = {
                        ProductId: b.productId,
                        Name: b.Name,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };

                });


        }

        $scope.Save = function (product) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/ProductApi/Save', {
                    ProductId: productId,
                    Name: product.Name,
                    CreatedBy: product.CreatedBy,
                    CreatedOn: product.CreatedOn,
                    Deleted: product.Deleted,

                });

                promise.then(
                    function (payload) {

                        productId = payload.data;
                        $scope.showMessageSave = true;
                        $timeout(function () {
                            $scope.showMessageSave = false;
                            if (action == "create") {
                                $state.go('product-edit', { 'action': 'edit', 'productId': productId });
                            }

                        }, 1500);


                    });
            }

        }

        $scope.Cancel = function () {
            $state.go('products.list');
        };

        $scope.Delete = function (productId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/ProductApi/Delete?productId=' + productId, {});
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
    .module('homer').controller('ProductController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/ProductApi/GetAllproducts');
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
                    name: 'Name', field:'Name',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                  {
                      name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/products/edit/{{row.entity.ProductId}}">Edit</a> </div>',
                     
                  },


            ];




        }]);

