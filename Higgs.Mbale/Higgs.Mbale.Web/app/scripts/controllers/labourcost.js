angular
    .module('homer')
    .controller('LabourCostEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        
        var labourCostId = $scope.labourCostId;
        var action = $scope.action;
        var batchId = $scope.batchId;


        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });
        $http.get('/webapi/SectorApi/GetAllSectors').success(function (data, status) {
            $scope.sectors = data;
        });


        $http.get('/webapi/ActivityApi/GetAllActivities').success(function (data, status) {
            $scope.activities = data;
        });

        if (action == 'create') {
            labourCostId = 0;
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
            var promise = $http.get('/webapi/LabourCostApi/GetLabourCost?labourCostId=' + labourCostId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.labourCost = {
                        LabourCostId: b.LabourCostId,
                        Quantity : b.Quantity,
                        Amount: b.Amount,
                        ActivityId: b.ActivityId,
                        BranchId: b.BranchId,
                        SectorId: b.SectorId,
                        BatchId: b.BatchId,
                        Rate: b.Rate,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };
                });
        }

        $scope.Save = function (labourCost) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/LabourCostApi/Save', {
                    LabourCostId: labourCostId,
                    Quantity: labourCost.Quantity,
                    ActivityId : labourCost.ActivityId,
                    BranchId: labourCost.BranchId,
                    BatchId: batchId,
                    SectorId: labourCost.SectorId,
                    CreatedBy: labourCost.CreatedBy,
                    CreatedOn: labourCost.CreatedOn,
                    Deleted: labourCost.Deleted
                });

                promise.then(
                    function (payload) {
                        labourCostId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                            if (action == "create") {
                                $state.go('labourCost-batch-edit', { 'action': 'edit', 'labourCostId': labourCostId, 'batchId': batchId });
                            }

                        }, 1500);
                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('labourCost-batch', { 'batchId': $scope.batchId });
        };

        $scope.Delete = function (labourCostId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/LabourCostApi/Delete?labourCostId=' + labourCostId, {});
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
    .module('homer').controller('BatchLabourCostController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var batchId = $scope.batchId;

            var promise = $http.get('/webapi/LabourCostApi/GetAllLabourCostsForAParticularBatch?batchId=' + batchId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedBatchId = $scope.batchId;

            $scope.gridData = {
                enableFiltering: false,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

          
            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'Activity Name', field: 'ActivityName',
                    
                },

                  { name: 'Quantity', field: 'Quantity' },
                { name: 'Rate', field: 'Rate' },
                { name: 'Amount ', field: 'Amount' },
              
                {name: 'Department',field:'SectorName'},
                { name: 'Branch Name', field: 'BranchName' },
                 {
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/labourCosts/edit/{{row.entity.LabourCostId}}/' + $scope.batchId + '">Edit</a> </div>',
                    
                 },
            ];
        }]);

