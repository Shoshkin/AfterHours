(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$http", "locker", "$state"];
    app.controller("Signup", function ($scope, $http, locker, $state) {

        $scope.signup = function () {
            $http.post('api/signup', $scope.user)
                .then(function successCallback(response) {
                    // $http.defaults.headers.common["afterHoursAuth"] = $scope.username + "," + $scope.password;
                    locker.put("session", {username: $scope.username, password: $scope.password});
                    $state.go("root.home");

                }, function errorCallback(response) {

                    console.log(response);

                });
        }
    });
})();
