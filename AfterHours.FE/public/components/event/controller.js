(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$state", "ApiComm"];
    app.controller("Event", function ($scope, $state, ApiComm) {
        ApiComm.get("api/events/" + $state.params.id)
            .then(function(response){
                $scope.event = response;
            });
    });
})();
