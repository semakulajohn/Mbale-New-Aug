angular
    .module('homer').controller('NavController', ['$scope', '$http',
    function ($scope, $http) {
        $scope.isAdmin = false;
        $scope.isSupplier = false;
        $scope.isCustomer = false;
        $scope.isBranchManager = false;
        var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
        promise.then(
            function (payload) {
                var c = payload.data;

                $scope.user = {
                    UserName: c.UserName,
                    MediaId: c.MediaId,
                    UserPhoto: c.UserPhoto,
                    FirstName: c.FirstName,
                    LastName: c.LastName,
                    UserRoles: c.UserRoles,
                    Id: c.Id,
                    RoleName: c.RoleName,
                };
                for (var i = 0; i < $scope.user.UserRoles.length; i++) {
                    if ($scope.user.UserRoles[i] == "admin") {
                        $scope.isAdmin = true;
                        break;
                    }
                    else if ($scope.user.UserRoles[i] == "supplier") {
                        $scope.isSupplier = true;
                        break;
                    }
                    else if ($scope.user.UserRoles[i] == "customer") {
                        $scope.isCustomer = true;
                        break;
                    }
                    else if($scope.user.UserRoles[i]=="branchManager") {
                        $scope.isBranchManager = true;
                        //var promise = $http.get('/webapi/UserApi/GetLoggedInUserBranch?userBranchId='+$scope.user.Id, {});
                        //promise.then(
                        //    function (payload) {
                        //        var c = payload.data;

                        //        $scope.userBranch = {
                        //            UserId: c.UserId,
                        //           BranchId : c.BranchId,
                        //        };
                        //        $scope.retrievedBranchId = $scope.userBranch.BranchId;
                        //    });
                        break
                    }
                }
            }
        );
    }
]);

