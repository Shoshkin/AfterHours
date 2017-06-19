(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$http", "locker"];
    app.service("ApiComm", function ($http, locker) {
        this.get = function (url) {
            // locker.
            $http.get(url, {headers: {afterHoursAuth: $scope.username + "," + $scope.password}});
        };

        this.post = function (url, body) {
            $http.post(url, {headers: {afterHoursAuth: $scope.username + "," + $scope.password}});
        };
    })
})();
