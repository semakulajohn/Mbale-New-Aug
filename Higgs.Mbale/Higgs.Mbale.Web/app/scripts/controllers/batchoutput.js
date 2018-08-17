angular
    .module('homer')
    .controller('BatchOutPutEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }
        $scope.selectedGrades = [];
        var batchId = $scope.batchId;
        var batchOutPutId = $scope.batchOutPutId;
        var action = $scope.action;
        $scope.showMessageFlourOutPut = false;


        $http.get('webapi/GradeApi/GetAllGrades').success(function (data, status) {
            $scope.grades = data;
        });
        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });

        $http.get('/webapi/SectorApi/GetAllSectors').success(function (data, status) {
            $scope.sectors = data;
        });

        $http.get('/webapi/StoreApi/GetAllStores').success(function (data, status) {
            $scope.stores = data;
        });

        if (action == 'create') {
            batchOutPutId = 0;
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

            var promise = $http.get('/webapi/BatchOutPutApi/GetBatchOutPut?batchOutPutId=' + batchOutPutId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.batchOutPut = {
                        BatchId: b.BatchId,
                        Loss: b.Loss,
                        BrandPercentage: b.BrandPercentage,
                        FlourPercentage: b.FlourPercentage,
                        LossPercentage: b.LossPercentage,
                        BrandOutPut: b.BrandOutPut,
                        FlourOutPut: b.FlourOutPut,
                        BranchId: b.BranchId,
                        StoreId : b.StoreId,
                        SectorId : b.SectorId,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,
                        Grades: b.Grades

                    };

                });


        }

      

        $scope.Save = function (batchOutPut) {
               
                $scope.TotalGradeKgs = 0;
                $scope.DenominationKgs = 0;
                $scope.showMessageSave = false;
                
                angular.forEach($scope.selectedGrades, function (value, key) {
                    var denominations = value.Denominations;
                    angular.forEach(denominations, function (denominations) {
                        $scope.DenominationKgs = (denominations.Value * denominations.Quantity) + $scope.DenominationKgs;
                    });
                    $scope.TotalGradeKgs = $scope.DenominationKgs;
                    //+$scope.TotalGradeKgs;
                });
                if ($scope.TotalGradeKgs == batchOutPut.FlourOutPut) {
                    $scope.showMessageFlourOutPut = false;

                    if ($scope.form.$valid) {
                        var promise = $http.post('/webapi/BatchOutPutApi/Save', {
                            BatchId: batchId,
                            BatchOutPutId: batchOutPutId,
                            BranchId: batchOutPut.BranchId,
                            SectorId : batchOutPut.SectorId,
                            CreatedBy: batchOutPut.CreatedBy,
                            CreatedOn: batchOutPut.CreatedOn,
                            Deleted: batchOutPut.Deleted,
                            BrandOutPut: batchOutPut.BrandOutPut,
                            FlourOutPut: batchOutPut.FlourOutPut,
                            StoreId : batchOutPut.StoreId,
                            
                            Grades: batchOutPutId == 0 ? $scope.selectedGrades : batchOutPut.Grades
                        });

                        promise.then(
                            function (payload) {

                                batchOutPutId = payload.data;

                                $scope.showMessageSave = true;
                                

                                $timeout(function () {
                                    $scope.showMessageSave = false;
                                  
                                    if (action == "create") {
                                        $state.go('batchoutput-batch-edit', { 'action': 'edit', 'batchOutPutId': batchOutPutId, 'batchId': batchId });
                                    }

                                }, 1500);


                            });
                    }
                }
                else {
                    $scope.showMessageFlourOutPut = true;
                   $scope.TotalGradeKgs = 0;
                    $timeout(function () {
                        $scope.showMessageFlourOutPut = false;

                    }, 3000);
                

            }

        }




        $scope.Cancel = function () {
            $state.go('batchoutput-batch', { 'batchId': batchId });
        };

        $scope.Delete = function (batchOutPutId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/BatchOutPutApi/Delete?batchOutPutId=' + batchOutPutId, {});
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
    .module('homer').controller('BatchOutPutController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;

            var batchId = $scope.batchId;

            var promise = $http.get('/webapi/BatchOutPutApi/GetAllBatchOutPutsForAParticularBatch?batchId=' + batchId, {});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );
            $scope.retrievedBatchId = $scope.batchId;

            $scope.gridData = {
                enableColumnResizing: true,
                enableFiltering: false,
                columnDefs: $scope.columns,
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'Id', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/batchoutputs/edit/{{row.entity.BatchOutPutId}}/' + $scope.batchId + '">{{row.entity.BatchOutPutId}}</a> </div>',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    },
                    //width: '5%'
                },
                { name: 'Loss(kgs)', field: 'Loss' },
                { name: 'Loss(%)', field: 'LossPercentage' },
                { name: 'Flour(kgs)', field: 'FlourOutPut' },
                { name: 'Flour(%)', field: 'FlourPercentage' },
                { name: 'Brand(kgs)', field: 'BrandOutPut' },
                { name: 'Brand(%)', field: 'BrandPercentage' },
                { name: 'Branch', field: 'BranchName' },
                { name: 'Department', field: 'SectorName' },
                  {
                      name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/batchoutputs/edit/{{row.entity.BatchOutPutId}}/' + $scope.batchId + '">Edit</a> </div>',
                     
                  },

            ];




        }]);

