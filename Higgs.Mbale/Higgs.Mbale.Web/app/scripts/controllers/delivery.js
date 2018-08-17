angular
    .module('homer')
    .controller('DeliveryEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        var transactionSubTypeId = 2;
        var deliveryId = $scope.deliveryId;
        var orderId = $scope.orderId;
        var customerId = $scope.customerId;
        var action = $scope.action;
               
        $http.get('webapi/ProductApi/GetAllproducts').success(function (data, status) {
            $scope.products = data;
        });

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
            var promise = $http.get('/webapi/DeliveryApi/GetDelivery?deliveryId=' + deliveryId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.delivery = {
                        DeliveryId: b.DeliveryId,
                        CustomerName: b.CustomerName,
                        DeliveryCost: b.DeliveryCost,
                        OrderId: b.OrderId,
                        VehicleNumber: b.VehicleNumber,
                        BranchId: b.BranchId,
                        Location: b.Location,
                        TransactionSubTypeId : b.TransactionSubTypeId,
                        SectorId: b.SectorId,
                        DriverNIN: b.DriverNIN,
                        DriverName: b.DriverName,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };
                });


        }

        $scope.Save = function (delivery) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/DeliveryApi/Save', {
                    DeliveryId: deliveryId,
                    CustomerId :customerId,
                    DeliveryCost: delivery.DeliveryCost,
                    OrderId : orderId,
                    VehicleNumber :delivery.VehicleNumber,
                    BranchId: delivery.BranchId,
                    Location : delivery.Location,
                    SectorId: delivery.SectorId,
                    TransactionSubTypeId : transactionSubTypeId,
                    DriverName: delivery.DriverName,
                    DriverNIN: delivery.DriverNIN,
                    CreatedBy: delivery.CreatedBy,
                    CreatedOn: delivery.CreatedOn,
                    Deleted: delivery.Deleted
                });

                promise.then(
                    function (payload) {

                        deliveryId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('delivery-edit', { 'action': 'edit', 'deliveryId': deliveryId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('delivery-order-list', { 'orderId': orderId });
        };

        $scope.Delete = function (deliveryId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/DeliveryApi/Delete?deliveryId=' + deliveryId, {});
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
    .module('homer').controller('DeliveryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/DeliveryApi/GetAllDeliveries');
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
                    name: 'Destination', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/deliveries/edit/{{row.entity.DeliveryId}}">{{row.entity.Location}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                { name: 'Customer Name', field: 'CustomerName' },
                { name: 'Driver Name', field: 'DriverName' },
                {name: 'Driver NIN',field: 'DriverNIN'},
                { name: 'Delivery Charge', field: 'DeliveryCost' },
                { name: 'Vehicle Number', field: 'VehicleNumber' },
                { name: 'Branch Name', field: 'BranchName' },
              
            ];




        }]);


angular
    .module('homer').controller('OrderDeliveryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var orderId = $scope.orderId;
            var promise = $http.get('/webapi/DeliveryApi/GetAllDeliveriesForAParticularOrder?orderId=' + orderId, {});
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
                    name: 'Destination', field:'Location',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                { name: 'Customer Name', field: 'CustomerName' },
                { name: 'Driver Name', field: 'DriverName' },
                { name: 'Driver NIN', field: 'DriverNIN' },
                { name: 'Delivery Charge', field: 'DeliveryCost' },
                { name: 'Vehicle Number', field: 'VehicleNumber' },
                { name: 'Branch Name', field: 'BranchName' },

            ];




        }]);



angular
    .module('homer').controller('BranchDeliveryController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var branchId = $scope.branchId;
            var promise = $http.get('/webapi/DeliveryApi/GetAllBranchDeliveries?branchId=' + branchId, {});
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
                    name: 'Destination', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/deliveries/edit/{{row.entity.DeliveryId}}">{{row.entity.Location}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                { name: 'Customer Name', field: 'CustomerName' },
                { name: 'Driver Name', field: 'DriverName' },
                { name: 'Driver NIN', field: 'DriverNIN' },
                { name: 'Delivery Charge', field: 'DeliveryCost' },
                { name: 'Vehicle Number', field: 'VehicleNumber' },
                { name: 'Branch Name', field: 'BranchName' },

            ];




        }]);
