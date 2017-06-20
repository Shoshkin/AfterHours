(function() {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$http"];
    app.controller("Home", function($scope, $http) {
        $http.get("api/events").then(function successCallback(response) {
            $scope.previewEvents = response.data;
        }, function fail(response) {
            console.log(response);
        });

    });
})();
