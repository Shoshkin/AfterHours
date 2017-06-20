(function () {

    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "locker", "ApiComm"];
    app.controller("Root", function ($scope, locker, ApiComm) {
        $scope.isLoggedIn = function () {
            return locker.has("session");
        };

        $scope.loadTags = function (query) {
            return ApiComm.get("api/tags")
                .then(function (response) {
                    return _.filter(response.data, function(o) { return _.startsWith(o, query); });
                });
        };
    });
})();