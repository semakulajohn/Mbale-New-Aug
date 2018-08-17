

angular
    .module('homer')
    .controller('UserEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', 'insertItemService', 
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, insertItemService) {
        var Id = $scope.Id;
        $scope.roles = [];
        $scope.checkedRoles = [];
        console.log($scope.checkedRoles);
        var action = $scope.action;
            
        if (action == 'create') {
            Id = 0;
            $scope.user = {
                UserRoles: []
            };
        }
        var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
        promise.then(
            function (payload) {
                var c = payload.data;

                $scope.user = {
                    Id: c.Id,
                    FirstName:c.FirstName,
                    LastName :c.LastName,
                    Email :c.Email,
                    UserName :c.UserName ,
                    MiddleName :c.MiddleName,
                    Address :c.Address,
                    PhoneNumber :c.PhoneNumber,
                    Mobile :c.Mobile,
                    Town :c.Town,
                    CityId :c.CityId,
                    CountryId :c.CountryId,
                    GenderId :c.GenderId,
                    CreatedBy:c.CreatedBy,
                    UpdatedBy :c.UpdatedBy,
                    TimeStamp :c.TimeStamp,
                    DateOfBirth :c.DateOfBirth,
                    CreatedOn  :c.CreatedOn,
                    MediaId: c.MediaId,
                    UserRoles: c.UserRoles,
                    UserPhoto: c.UserPhoto,
                };
            }
        );

        
       
        $scope.Save = function (user) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                console.log("selected roles"+ user.UserRoles);
                var promise = $http.post('/webapi/UserApi/SaveUser', {
                    Id: user.Id,
                    FirstName: user.FirstName,
                    LastName: user.LastName,
                    Email: user.Email,
                    UserName: user.UserName,
                    MiddleName: user.MiddleName,
                    Address: user.Address,
                    PhoneNumber: user.PhoneNumber,
                    UserRoles: user.UserRoles,
                    MediaId: user.UserPhotoId
                });

                promise.then(
                    function (payload) {
                        user = payload.data;
                      
                        if (user.EmailExists == true) {
                            $scope.showMessageEmailExists = true;

                            $timeout(function () {
                                $scope.showMessageEmailExists = false;
                            }, 4000);
                        }

                        else {
                            $scope.showMessageSave = true;

                            $timeout(function () {
                                $scope.showMessageSave = false;
                            }, 1500);
                        }
                        

                    });
            }
        }

        $scope.DeletePhoto = function () {
            $scope.user.UserPhoto = {};
            $scope.user.UserPhotoId = null
        };

        $scope.Cancel = function () {
            //$state.go('dashboard');
            $location.url('/dashboard');

        };

        $scope.ManagePhoto = function (user) {
            $scope.modalInstance = $modal.open({
                templateUrl: '/app/views/modal/modalContent.html?rnd=' + new Date().getTime(),
                size: 'large',
                controller: 'ModalInstanceCtrl',
                resolve: {
                    items: function () {

                        $scope.dataItems = {
                            showLinkPickerBtn2: true,
                            showLinkPicker2: true,
                            inPageMediaLinkingType: EnumInPageMediaLinkingType.Type.UserPhoto,
                            mediaTypes: EnumMediaType.Type.Image
                        }
                        return $scope.dataItems;
                    },
                },
                windowClass: 'contentModalBox'
            });
        };

        $scope.$on('InsertItemBroadcastHandler', function () {
            if (insertItemService.inPageMediaLinkingType == EnumInPageMediaLinkingType.Type.UserPhoto) {
                $scope.user.UserPhotoId = insertItemService.Item.MediaId
                $scope.user.UserPhoto = insertItemService.Item;
            }
            $scope.modalInstance.close();
        });

        $scope.checkPass = function () {
            var pass1 = document.getElementById('pass1');
            var pass2 = document.getElementById('pass2');
    
            var message = document.getElementById('confirmMessage');

            var goodColor = "#66cc66";
            var badColor = "#ff6666";

            if (pass1.value == pass2.value) {           
                message.style.color = goodColor;
                $scope.showMessagePasswordMatch = true;

                $scope.showMessagePasswordDoNotMatchMatch = false;
                $timeout(function () {
                    $scope.showMessagePasswordMatch = false;
                }, 1500);
            } else {
                message.style.color = badColor;
                $scope.showMessagePasswordDoNotMatchMatch = true;
            }
        }
  
        var promise = $http.get('/webapi/UserApi/GetAllRoles', {});
        promise.then(
            function (payload) {
                $scope.roles = payload.data;
            });
    }
    ]);

angular
    .module('homer').controller('UserController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', '$sce',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, $sce) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/UserApi/GetAllAspNetUsers');
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
                {name: 'Id',field:'Id',widht:'15%',},
                { name: 'First Name', field: 'FirstName', width: '10%', },
                { name: 'Last Name', field: 'LastName', width: '10%', },
                { name: 'User Name', field: 'UserName', width: '10%' },
                { name: 'PhoneNumber', field: 'PhoneNumber', width: '10%' },
                { name: 'Edit', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/admins/edit/{{row.entity.Id}}">edit</a></div>' },
            ];


            $scope.gridData.onRegisterApi = function (gridApi) {
                $scope.gridApi = gridApi;
            };

        }]);


angular
    .module('homer')
    .controller('AspNetUserEditController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$modal', 'insertItemService', 'EnumInPageMediaLinkingType', 'EnumMediaType',
    function ($scope, $http, $filter, $location, $log, $timeout, $modal, insertItemService, EnumInPageMediaLinkingType, EnumMediaType) {
        var Id = $scope.adminId;
        $scope.roles = [];
        $scope.checkedRoles = [];
        console.log($scope.checkedRoles);
        var action = $scope.action;

        if (action == 'create') {
            Id = 0;
            $scope.aspnetUser = {
                UserRoles: []
            };
        }
        
        if (action == 'edit') {
            var promise = $http.get('/webapi/UserApi/GetUser?userId=' + Id, {});
            promise.then(
                function (payload) {
                    var c = payload.data;

                    $scope.aspnetUser = {
                        Id: c.Id,
                        FirstName: c.FirstName,
                        LastName: c.LastName,
                        Email: c.Email,
                        UserName: c.UserName,
                        MiddleName: c.MiddleName,
                        Address: c.Address,
                        PhoneNumber: c.PhoneNumber,
                        Mobile: c.Mobile,
                        Town: c.Town,
                        Password: c.PasswordHash,
                        CityId: c.CityId,
                        CountryId: c.CountryId,
                        GenderId: c.GenderId,
                        CreatedBy: c.CreatedBy,
                        UpdatedBy: c.UpdatedBy,
                        TimeStamp: c.TimeStamp,
                        DateOfBirth: c.DateOfBirth,
                        CreatedOn: c.CreatedOn,
                        MediaId: c.MediaId,
                        UserRoles: c.UserRoles,
                        UserPhoto: c.UserPhoto,
                    };
                }
            );
        }

        $scope.Save = function (aspnetUser) {
            $scope.showMessageSave = false;
            if ($scope.form.$valid) {
                console.log("selected roles" + aspnetUser.UserRoles);
                var promise = $http.post('/webapi/UserApi/SaveUser', {
                    Id: aspnetUser.Id,
                    FirstName: aspnetUser.FirstName,
                    LastName: aspnetUser.LastName,
                    Email: aspnetUser.Email,
                    UserName: aspnetUser.UserName,
                    MiddleName: aspnetUser.MiddleName,
                    Address: aspnetUser.Address,
                    PhoneNumber: aspnetUser.PhoneNumber,
                    UserRoles: aspnetUser.UserRoles,
                    MediaId: aspnetUser.UserPhotoId,
                    PasswordHash: aspnetUser.Password
                });

                promise.then(
                    function (payload) {
                        aspnetUser = payload.data;

                        if (aspnetUser.EmailExists == true) {
                            $scope.showMessageEmailExists = true;

                            $timeout(function () {
                                $scope.showMessageEmailExists = false;
                            }, 4000);
                        }

                        else {
                            $scope.showMessageSave = true;

                            $timeout(function () {
                                $scope.showMessageSave = false;
                            }, 1500);
                        }


                    });
            }
        }

        $scope.DeletePhoto = function () {
            $scope.aspnetUser.UserPhoto = {};
            $scope.aspnetUser.UserPhotoId = null
        };

        $scope.ManagePhoto = function (aspnetUser) {
            $scope.modalInstance = $modal.open({
                templateUrl: '/app/views/modal/modalContent.html?rnd=' + new Date().getTime(),
                size: 'large',
                controller: 'ModalInstanceCtrl',
                resolve: {
                    items: function () {

                        $scope.dataItems = {
                            showLinkPickerBtn2: true,
                            showLinkPicker2: true,
                            inPageMediaLinkingType: EnumInPageMediaLinkingType.Type.UserPhoto,
                            mediaTypes: EnumMediaType.Type.Image
                        }
                        return $scope.dataItems;
                    },
                },
                windowClass: 'contentModalBox'
            });
        };

        $scope.$on('InsertItemBroadcastHandler', function () {
            if (insertItemService.inPageMediaLinkingType == EnumInPageMediaLinkingType.Type.UserPhoto) {
                $scope.aspnetUser.UserPhotoId = insertItemService.Item.MediaId
                $scope.aspnetUser.UserPhoto = insertItemService.Item;
            }
            $scope.modalInstance.close();
        });

        $scope.checkPass = function () {
            var pass1 = document.getElementById('pass1');
            var pass2 = document.getElementById('pass2');

            var message = document.getElementById('confirmMessage');

            var goodColor = "#66cc66";
            var badColor = "#ff6666";

            if (pass1.value == pass2.value) {
                message.style.color = goodColor;
                $scope.showMessagePasswordMatch = true;

                $scope.showMessagePasswordDoNotMatchMatch = false;
                $timeout(function () {
                    $scope.showMessagePasswordMatch = false;
                }, 1500);
            } else {
                message.style.color = badColor;
                $scope.showMessagePasswordDoNotMatchMatch = true;
            }
        }

        var promise = $http.get('/webapi/UserApi/GetAllRoles', {});
        promise.then(
            function (payload) {
                $scope.roles = payload.data;
            });
    }
    ]);