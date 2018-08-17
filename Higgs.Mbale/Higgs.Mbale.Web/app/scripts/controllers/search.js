angular
    .module('homer')
    .controller('SearchController', ['$scope', '$stateParams', 'ngTableParams', '$http', '$filter', '$location', '$log', '$sce', '$modal', 
    function ($scope, $stateParams, ngTableParams, $http, $filter, $location, $log, $sce, $modal) {
        $scope.loadingSpinner = false;
        $scope.q = $stateParams.q;
        $scope.results = {};

        $scope.showLoadMore = false;
        $scope.loading = false;
        $scope.pageIndex = 1;

        if ($scope.q.length > 0) {
            $scope.loadingSpinner = true;
            $scope.showLoadMore = true;
            var promise = $http.get('/webapi/SearchApi/index/?q=' + $scope.q + "&pageIndex=" + $scope.pageIndex, {});
            promise.then(
                function (payload) {
                    $scope.results = payload.data;
                    if ($scope.results.SearchResults.length == 0) {
                        $scope.showLoadMore = false;
                    }
                    $scope.loadingSpinner = false;
                }
            );
        }

        $scope.search = function () {
            $scope.pageIndex = 1;
            $scope.showLoadMore = false;

            if ($scope.q.length > 0) {

                $scope.loadingSpinner = true;

                var promise = $http.get('/webapi/SearchApi/index/?q=' + $scope.q + "&pageIndex=" + $scope.pageIndex, {});
                promise.then(
                    function (payload) {
                        $scope.results = payload.data;
                        if ($scope.results.SearchResults.length == 0) {
                            $scope.showLoadMore = false;
                        }
                        else {
                            $scope.showLoadMore = true;
                        }
                        $scope.loadingSpinner = false;
                    }
                );
            }
        }

        $scope.LoadMore = function () {

            $scope.loading = true;
            $scope.pageIndex = $scope.pageIndex + 1;
            var promise = $http.get('/webapi/SearchApi/index/?q=' + $scope.q + "&CountryId=" + $scope.selectedCountry.CountryId + "&pageIndex=" + $scope.pageIndex, {});
            promise.then(
              function (payload) {
                  var data = payload.data;
                  setTimeout(function () {
                      $scope.$apply(function () {
                          $scope.loading = false;

                          if (data.SearchResults.length > 0) {
                              angular.forEach(data.SearchResults, function (obj) {
                                  $scope.results.SearchResults.push(obj);
                              });
                          }
                          else {
                              $scope.showLoadMore = false;
                          }
                      });
                  }, 500);
              },
              function (reason) {
                  console.log(reason);
              }
            );
        }

        $scope.renderHtml = function (html_code) {
            return $sce.trustAsHtml(html_code);
        };

    }]);