(function() {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$http", "$state"];
    app.controller("Home", function($scope, $http, $state) {
        $http.get("api/events").then(function successCallback(response) {
            $scope.previewEvents = response.data;
        }, function fail(response) {
            console.log(response);
        });

        $scope.clickEvent = function(eventId){
            $state.go("root.event", {"id":eventId});
        }

    });
})();
