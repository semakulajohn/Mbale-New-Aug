angular
    .module('homer')
    .controller('BranchEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var branchId = $scope.branchId;
        var action = $scope.action;

       


        if (action == 'create') {
            branchId = 0;

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



            var promise = $http.get('/webapi/BranchApi/GetBranch?branchId=' + branchId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.branch = {
                        BranchId: b.BranchId,
                        Name: b.Name,
                        PhoneNumber : b.PhoneNumber,
                        Location: b.Location,
                        TimeStamp: b.TimeStamp,
                        MillingChargeRate : b.MillingChargeRate,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted : b.Deleted,


                    };

                });


        }

        $scope.Save = function (branch) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/BranchApi/Save', {
                    BranchId: branchId,
                    Name: branch.Name,
                    PhoneNumber: branch.PhoneNumber,
                    MillingChargeRate : branch.MillingChargeRate,
                    CreatedBy: branch.CreatedBy,
                    CreatedOn: branch.CreatedOn,
                    Location: branch.Location,
                    Deleted : branch.Deleted,

                });

                promise.then(
                    function (payload) {

                        branchId = payload.data;

                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('branch-edit', { 'action': 'edit', 'branchId': branchId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('branches.list');

        };

        $scope.Delete = function (branchId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/BranchApi/Delete?branchId=' + branchId, {});
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
    .module('homer').controller('BranchController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/BranchApi/GetAllBranches');
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
                    name: 'Name',field:'Name',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Phone Number', field: 'PhoneNumber' },

                 { name: 'Location', field: 'Location' },
                 {name: 'Milling Charge Rate',field : 'MillingChargeRate'},

  { name: 'View', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/branches/{{row.entity.BranchId}}">Dashboard</a></div>' },

                 {
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/branches/edit/{{row.entity.BranchId}}">Edit</a> </div>',
                    
                 },


            ];




        }]);

angular
    .module('homer')
    .controller('BranchDashBoardController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var branchId = $scope.branchId;
        //var branchId = $scope.retrievedBranchId;
        //$scope.branchId = 0;

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
                    var promise = $http.get('/webapi/UserApi/GetLoggedInUserBranch?userBranchId=' + $scope.user.Id, {});
                    promise.then(
                        function (payload) {
                            var c = payload.data;

                            $scope.userBranch = {
                                UserId: c.UserId,
                                BranchId: c.BranchId,
                            };

                            //$scope.branchId = c.BranchId;
                            //var branches = branchDataService.getAllBranches();

                            //var branch = $filter('filter')(branches, { BranchId: $scope.branchId })[0];

                            //$scope.retrievedBranchId = $scope.userBranch.BranchId;
                            if ($scope.userBranch != null) {
                                branchId = $scope.userBranch.BranchId

                            }
                        });

                }

            );

           
            //$scope.init = function (retrievedBranchId) {
            //    //This function is sort of private constructor for controller
            //    $scope.branchId = retreivedBranchId;
               
            //    //Based on passed argument you can make a call to resource
            //    //and initialize more objects
            //    //$resource.getMeBond(007)
           

            var promise = $http.get('/webapi/BranchApi/GetBranch?branchId=' + branchId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.branch = {
                        BranchId: b.BranchId,
                        Name: b.Name,
                        PhoneNumber: b.PhoneNumber,
                        Location: b.Location,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,


                    };

                });


            var promise = $http.get('/webapi/SupplyApi/GetAllSuppliesForAParticularBranch?branchId=' + branchId, {});
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


            ];

            var promise = $http.get('/webapi/CasualWorkerApi/GetAllCasualWorkersForAParticularBranch?branchId=' + branchId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [
                { name: 'Id Number', field: 'UniqueNumber' },
                {
                    name: 'FirstName', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/casualWorkers/edit/{{row.entity.CasualWorkerId}}">{{row.entity.FirstName}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },
                {
                    name: 'LastName', field: 'LastName'
                },
                {
                    name: 'Phone', field: 'PhoneNumber'
                },
               {
                   name: 'Address', field: 'Address'
               },
               { name: 'Nin Number', field: 'NINNumber' },
               { name: 'Next Of keen', field: 'NextOfKeen' },
               { name: 'Email', field: 'EmailAddress' },
               {
                   name: 'Branch', field: 'BranchName'
               },
               { name: 'Casual Activities', field: 'Id', width: '10%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/casualActivities/{{row.entity.CasualWorkerId}}">Manage Casual Activities</a></div>' },
            { name: 'Transaction Activities', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/accounttransactionactivities/{{row.entity.CasualWorkerId}}">Manage Transaction Activities</a></div>' },

            ];



            var promise = $http.get('/webapi/OrderApi/GetAllOrdersForAparticularBranch?branchId=' + branchId, {});
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
                    name: 'Product Name', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/orders/edit/{{row.entity.OrderId}}">{{row.entity.ProductName}}</a> </div>'
                },
                { name: 'Customer Name', field: 'CustomerName' },
                { name: 'Order Number', field: 'Name' },
                { name: 'Status', field: 'StatusName' },
                { name: 'Quantity', field: 'Amount' },
                { name: 'Branch Name', field: 'BranchName' },
                { name: 'Order Details', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/order/detail/{{row.entity.OrderId}}"> Order Detail</a> </div>' },
            { name: 'Delivery', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/deliveries/{{row.entity.OrderId}}">Delivery</a></div>' },

            ];

              
    }

    //}
        
    ]);



