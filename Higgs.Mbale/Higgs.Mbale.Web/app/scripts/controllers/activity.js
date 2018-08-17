angular
    .module('homer')
    .controller('ActivityEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var activityId = $scope.activityId;
        var action = $scope.action;




        if (action == 'create') {
            activityId = 0;

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
            var promise = $http.get('/webapi/ActivityApi/GetActivity?activityId=' + activityId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.activity = {
                        ActivityId: b.ActivityId,
                        Name: b.Name,
                        Charge : b.Charge,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted
                    };

                });


        }

        $scope.Save = function (activity) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/ActivityApi/Save', {
                    ActivityId: activityId,
                    Name: activity.Name,
                    Charge: activity.Charge,
                    CreatedBy: activity.CreatedBy,
                    CreatedOn: activity.CreatedOn,
                    Deleted: activity.Deleted,

                });

                promise.then(
                    function (payload) {

                        activityId = payload.data;
                        $scope.showMessageSave = true;
                        $timeout(function () {
                            $scope.showMessageSave = false;
                            if (action == "create") {
                                $state.go('activity-edit', { 'action': 'edit', 'activityId': activityId });
                            }

                        }, 1500);


                    });
            }

        }

        $scope.Cancel = function () {
            $state.go('activities.list');
        };

        $scope.Delete = function (activityId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/ActivityApi/Delete?activityId=' + activityId, {});
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
    .module('homer').controller('ActivityController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/ActivityApi/GetAllActivities');
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

             { name: 'Charge', field: 'Charge' },
               {
                   name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/activities/edit/{{row.entity.ActivityId}}">Edit</a> </div>',
                  
               },

            ];




        }]);

