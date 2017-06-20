(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$http", "$state"];
    app.controller("Event", function ($scope, $http, $state) {
        $http.get("api/events/" + $state.params.id)
            .then(function(response){
                $scope.event = response;
            });
    });
})();
