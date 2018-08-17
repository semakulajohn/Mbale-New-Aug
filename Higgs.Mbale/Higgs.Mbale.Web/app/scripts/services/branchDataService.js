angular.module('homer')
  .service('branchDataService', ['$http', '$q', '$localStorage',
      function ($http, $q, $localStorage) {
          var deferObject;

          this.getAllBranches = function () {
              var branches = $localStorage.branches;
              if (!angular.isDefined(branches) || branches === null) {

                  var promise = $http.get('/webapi/BranchApi/GetAllBranches', {
                      cache: true
                  }), deferObject = deferObject || $q.defer();
                  promise.then(
                    function (data) {
                        deferObject.resolve(data);
                    },
                    function (reason) {
                        deferObject.reject(reason);
                    });

                  var deferObjectPromise = deferObject.promise;
                  var promise = deferObjectPromise;
                  promise.then(function (payload) {
                      branches = payload.data;
                      $localStorage.branches = branches;

                  }, function (reason) {
                      console.log('Failed: ' + reason);
                  });
                  return deferObjectPromise;
              }
              return branches;
          };

          this.getBranch = function (branchId) {
              var promise = $http.get('/webapi/BranchApi/GetBranch?branchId=' + branchId, { cache: true }),
                  deferObject = deferObject || $q.defer();

              promise.then(function (data) {
                  deferObject.resolve(data);
              }, function (reason) {
                  deferObject.reject(reason);
              });
              return deferObject.promise;
          };

       
      }]);