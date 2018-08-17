angular
    .module('homer')
    .controller('SectorEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var sectorId = $scope.sectorId;
        var action = $scope.action;
                
        if (action == 'create') {
            sectorId = 0;

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



            var promise = $http.get('/webapi/SectorApi/GetSector?sectorId=' + sectorId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.sector = {
                        SectorId: b.SectorId,
                        Name: b.Name,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,


                    };

                });


        }

        $scope.Save = function (sector) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/SectorApi/Save', {
                    SectorId: sectorId,
                    Name: sector.Name,
                    CreatedBy: sector.CreatedBy,
                    CreatedOn: sector.CreatedOn,
                    Deleted: sector.Deleted,

                });

                promise.then(
                    function (payload) {

                        SectorId = payload.data;

                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('sector-edit', { 'action': 'edit', 'sectorId': SectorId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('sectors.list');

        };

        $scope.Delete = function (sectorId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/SectorApi/Delete?sectorId=' + sectorId, {});
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
    .module('homer').controller('SectorController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/SectorApi/GetAllSectors');
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
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/sectors/edit/{{row.entity.SectorId}}">Edit</a> </div>',
                    
                 },
               

            ];




        }]);

