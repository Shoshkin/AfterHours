(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$state", "ApiComm"];
    app.controller("Add", function ($scope, $state, ApiComm) {
        $scope.postEvent = function() {
            ApiComm.post("api/events", $scope.event)
                .then(function (){
                    $state.go("root.home");
                });

        };
    });
})();
