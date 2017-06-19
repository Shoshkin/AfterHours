(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "locker", "$http", "$state", "ApiComm"];
    app.controller("Signin", function ($scope, locker, $http, $state, ApiComm) {
        $scope.signin = function () {
            $http.post('api/signin', {}, {headers: {afterHoursAuth: $scope.username + "," + $scope.password}})
                .then(function successCallback(response) {
                    $http.defaults.headers.common["afterHoursAuth"] = $scope.username + "," + $scope.password;
                    locker.put("session", {username: $scope.username, password: $scope.password});
                    $state.go("root.home");

                }, function errorCallback(response) {

                    console.log(response);

                });

        }
    });
})();