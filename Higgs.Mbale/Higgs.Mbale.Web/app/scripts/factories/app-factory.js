angular
    .module('homer')
    .factory('appSettingsService', ['$rootScope', function($rootScope) {

        return {
            getAppSettingsAsync: function (callback) {
                $http.get('/webapi/AppApi/GetAppSettings').success(callback);
            }
        };
    }]);

angular
    .module('homer')
    .factory('Utils', ['$rootScope', function($rootScope) {

    var service = {
        isUndefinedOrNull: function (obj) {
            return !angular.isDefined(obj) || obj === null;
        }
    }
    return service;
}]);

angular
    .module('homer')
    .factory('openDialogService', ['$rootScope', function($rootScope) {

        var service = {};
        service.prepForBroadcast = function (selectedText, relatedContentType) {

            this.selectedText = selectedText;
            this.relatedContentType = relatedContentType;
            this.broadcastItem();
        };

        service.broadcastItem = function () {
            $rootScope.$broadcast('OpenDialogBroadcastHandler');
        };
        return service;
}]);


angular
    .module('homer')
    .factory('FileExtentions', ['$http', function($http) {


        return {
            GetAllExtentionTypes: function (callback) {
                $http.get('/webapi/MediaApi/GetAllExtentionTypes').success(callback);
            }
        };

}]);

angular
    .module('homer')
    .factory('selectSupplyService', ['$rootScope', function ($rootScope) {

        var service = {};
        service.prepForBroadcast = function (item) {
            this.item = item;
            this.broadcastItem();
        };

        service.broadcastItem = function () {
            $rootScope.$broadcast('selectSupplyServiceBroadcastHandler');
        };
        return service;
    }]);


angular
    .module('homer')
    .factory('selectBatchService', ['$rootScope', function ($rootScope) {

        var service = {};
        service.prepForBroadcast = function (item) {
            this.item = item;
            this.broadcastItem();
        };

        service.broadcastItem = function () {
            $rootScope.$broadcast('selectBatchServiceBroadcastHandler');
        };
        return service;
    }]);


angular
    .module('homer')
    .factory('mediaService', ['$http', function($http) {

        return {
            getMediaAsync: function (id, callback) {
                $http.get('/webapi/MediaApi/GetMedia?mediaId=' + id + '&rnd=' + new Date().getTime()).success(callback);
            }
        };
    }]);


angular
    .module('homer')
    .factory('insertItemService', ['$rootScope', function($rootScope) {


        var service = {};

        service.prepForBroadcast = function (inPageMediaLinkingType, item) {
            this.Item = item;
            this.inPageMediaLinkingType = inPageMediaLinkingType;
            this.broadcastItem();
        };

        service.broadcastItem = function () {
            $rootScope.$broadcast('InsertItemBroadcastHandler');
        };
        return service;
    }]);

angular
    .module('homer')
    .factory('insertItemInEditorService', ['$rootScope', function($rootScope) {

        var service = {};
        service.prepForBroadcast = function (linkPickerType, selectedNode, selectedGlossaryTermGroup, urlLink, selectedLegalInstrument, selectedFormGroup, selectedMedia) {
            this.linkPickerType = linkPickerType;

            this.selectedNode = selectedNode;
            this.selectedGlossaryTermGroup = selectedGlossaryTermGroup;
            this.urlLink = urlLink;
            this.selectedLegalInstrument = selectedLegalInstrument;
            this.selectedFormGroup = selectedFormGroup;
            this.selectedMedia = selectedMedia;

            this.broadcastItem();
        };

        service.broadcastItem = function () {
            $rootScope.$broadcast('insertItemInEditorBroadcastHandler');
        };
        return service;
    }]);




angular
    .module('homer')
    .factory('EnumStatus', [function () {

        return {
            Status: {

                InProgress : 1,
                Completed : 2,
                Initial : 3,
               
            }
        }
    }]);

