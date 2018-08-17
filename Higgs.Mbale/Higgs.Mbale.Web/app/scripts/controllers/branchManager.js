angular
    .module('homer').controller('BranchManagerController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/BranchManagerApi/GetAllBranchManagers');
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

                   { name: 'Attach Branch', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/branchmanagers/{{row.entity.Id}}">Attach Branch</a></div>' },

                   
            ];




        }]);

angular
    .module('homer')
    .controller('AttachBranchEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {
        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var action = $scope.action;
        var userId = $scope.userId;


        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });

        if (action == 'create') {
            inventoryId = 0;
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
            var promise = $http.get('/webapi/BranchManagerApi/GetBranchManager?branchManagerId=' + userId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;
                    $scope.branchManager = {
                        UserId: b.UserId,
                        BranchId: b.BranchId,
                        TimeStamp: b.TimeStamp,
                      
                    };
                });


        }

        $scope.Save = function (branchManager) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/BranchManagerApi/Save', {
                    UserId: userId,
                    
                    BranchId: branchManager.BranchId,
                   
                });

                promise.then(
                    function (payload) {

                        userId = payload.data;
                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;

                           

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('branchmanagers.list');
        };

     


    }
    ]);
