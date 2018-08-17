angular
    .module('homer')
    .controller('TransactionSubTypeEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var transactionSubTypeId = $scope.transactionSubTypeId;
        var action = $scope.action;

        $http.get('/webapi/TransactionSubTypeApi/GetAllTransactionTypes').success(function (data, status) {
            $scope.transactionTypes = data;
        });


        if (action == 'create') {
            transactionSubTypeId = 0;

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



            var promise = $http.get('/webapi/TransactionSubTypeApi/GetTransactionSubType?transactionSubTypeId=' + transactionSubTypeId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.transactionSubType = {
                        TransactionSubTypeId: b.TransactionSubTypeId,
                        Name: b.Name,
                        TransactionTypeId: b.TransactionTypeId,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,


                    };

                });


        }

        $scope.Save = function (transactionSubType) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/TransactionSubTypeApi/Save', {
                    TransactionSubTypeId: transactionSubTypeId,
                    Name: transactionSubType.Name,
                    TransactionTypeId: transactionSubType.TransactionTypeId,
                    CreatedBy: transactionSubType.CreatedBy,
                    CreatedOn: transactionSubType.CreatedOn,
                    Deleted: transactionSubType.Deleted,

                });

                promise.then(
                    function (payload) {

                        transactionSubTypeId = payload.data;

                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('transactionSubType-edit', { 'action': 'edit', 'transactionSubTypeId': transactionSubTypeId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('transactionSubTypes.list');

        };

        $scope.Delete = function (transactionSubTypeId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/TransactionSubTypeApi/Delete?transactionSubTypeId=' + transactionSubTypeId, {});
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
    .module('homer').controller('TransactionSubTypeController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/TransactionSubTypeApi/GetAllTransactionSubTypes');
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

                { name: 'Transaction Type', field: 'TransactionTypeName' },

                 {
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/transactionSubTypes/edit/{{row.entity.TransactionSubTypeId}}">Edit</a> </div>',
                    
                 },
            ];




        }]);

