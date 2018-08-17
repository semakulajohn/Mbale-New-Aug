angular
    .module('homer')
    .controller('CasualWorkerEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });
        var casualWorkerId = $scope.casualWorkerId;
        var action = $scope.action;




        if (action == 'create') {
            casualWorkerId = 0;

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



            var promise = $http.get('/webapi/CasualWorkerApi/GetCasualWorker?casualWorkerId=' + casualWorkerId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.casualWorker = {
                        CasualWorkerId: b.CasualWorkerId,
                        FirstName: b.FirstName,
                        LastName: b.LastName,
                        BranchId : b.BranchId,
                        PhoneNumber: b.PhoneNumber,
                        Address: b.Address,
                        EmailAddress: b.EmailAddress,
                         NINNumber :b.NINNumber,
                        NextOfKeen :b.NextOfKeen,
                        UniqueNumber : b.UniqueNumber,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        Deleted: b.Deleted,


                    };

                });


        }

        $scope.Save = function (casualWorker) {

            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/CasualWorkerApi/Save', {
                    CasualWorkerId: casualWorkerId,
                    FirstName: casualWorker.FirstName,
                    LastName: casualWorker.LastName,
                    BranchId : casualWorker.BranchId,
                    PhoneNumber: casualWorker.PhoneNumber,
                    NextOfKeen: casualWorker.NextOfKeen,
                    UniqueNumber: casualWorker.UniqueNumber,
                    EmailAddress: casualWorker.EmailAddress,
                    NINNumber : casualWorker.NINNumber,
                    Address : casualWorker.Address,
                    CreatedBy: casualWorker.CreatedBy,
                    CreatedOn: casualWorker.CreatedOn,
                    Deleted: casualWorker.Deleted,

                });

                promise.then(
                    function (payload) {

                        CasualWorkerId = payload.data;

                        $scope.showMessageSave = true;

                        $timeout(function () {
                            $scope.showMessageSave = false;


                            if (action == "create") {
                                $state.go('casualWorker-edit', { 'action': 'edit', 'casualWorkerId': CasualWorkerId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('casualWorkers.list');

        };

        $scope.Delete = function (casualWorkerId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/CasualWorkerApi/Delete?casualWorkerId=' + casualWorkerId, {});
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
    .module('homer').controller('CasualWorkerController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/CasualWorkerApi/GetAllCasualWorkers');
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
                {name: 'Id Number',field:'UniqueNumber',width: '10%'},
                {
                    name: 'FirstName', field:'FirstName',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                    ,width:'15%'
                },
                {
                    name : 'LastName', field:'LastName'
                },
             
               {
                   name: 'Branch', field:'BranchName'
               },
               { name: 'View Details', field: 'Id', width: '10%', cellTemplate: '<div class="ui-grid-cell-contents"><a  href="#/casualworker/detail/{{row.entity.CasualWorkerId}}">Details</a></div>' },

               {name:'Casual Activities',field:'Id' ,width:'15%',cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/casualActivities/{{row.entity.CasualWorkerId}}">Manage Casual Activities</a></div>'},
            { name: 'Transaction Activities', field: 'Id', width: '20%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/accounttransactionactivities/{{row.entity.CasualWorkerId}}">Manage Transaction Activities</a></div>' },

             {
                 name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/casualWorkers/edit/{{row.entity.CasualWorkerId}}">Edit</a> </div>',
                 width: '10%'
             },
            ];




        }]);



angular
    .module('homer')
    .controller('CasualWorkerDetailController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        $http.get('/webapi/BranchApi/GetAllBranches').success(function (data, status) {
            $scope.branches = data;
        });
        var casualWorkerId = $scope.casualWorkerId;
       

            var promise = $http.get('/webapi/CasualWorkerApi/GetCasualWorker?casualWorkerId=' + casualWorkerId, {});
            promise.then(
                function (payload) {
                    var b = payload.data;

                    $scope.casualWorker = {
                        CasualWorkerId: b.CasualWorkerId,
                        FirstName: b.FirstName,
                        LastName: b.LastName,
                        BranchId: b.BranchId,
                        PhoneNumber: b.PhoneNumber,
                        Address: b.Address,
                        EmailAddress: b.EmailAddress,
                        NINNumber: b.NINNumber,
                        NextOfKeen: b.NextOfKeen,
                        UniqueNumber: b.UniqueNumber,
                        TimeStamp: b.TimeStamp,
                        CreatedOn: b.CreatedOn,
                        CreatedBy: b.CreatedBy,
                        UpdatedBy: b.UpdatedBy,
                        BranchName : b.BranchName,
                        Deleted: b.Deleted,


                    };

                });


        

     

        $scope.Cancel = function () {
            $state.go('casualWorkers.list');

        };

   

    }
    ]);



angular
    .module('homer').controller('BranchCasualWorkerController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var branchId = $scope.branchId;
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
                enableRowSelection: false
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [
                { name: 'Id Number', field: 'UniqueNumber', width: '10%' },
                {
                    name: 'FirstName', field: 'FirstName',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                    , width: '15%'
                },
                {
                    name: 'LastName', field: 'LastName'
                },

               {
                   name: 'Branch', field: 'BranchName'
               },
               { name: 'View Details', field: 'Id', width: '10%', cellTemplate: '<div class="ui-grid-cell-contents"><a  href="#/casualworker/detail/{{row.entity.CasualWorkerId}}">Details</a></div>' },

               { name: 'Casual Activities', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/casualActivities/{{row.entity.CasualWorkerId}}">Manage Casual Activities</a></div>' },
            { name: 'Transaction Activities', field: 'Id', width: '20%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/accounttransactionactivities/{{row.entity.CasualWorkerId}}">Manage Transaction Activities</a></div>' },

             {
                 name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/casualWorkers/edit/{{row.entity.CasualWorkerId}}">Edit</a> </div>',
                 width: '10%'
             },
            ];




        }]);

