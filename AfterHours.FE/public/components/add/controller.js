(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$state", "ApiComm"];
    app.controller("Add", function ($scope, $state, ApiComm) {
        $scope.postEvent = function() {
            $scope.event.tags = _.map($scope.event.tags, function(t){return t.text}).join(',');
            ApiComm.post("api/events", $scope.event)
                .then(function (){
                    $state.go("root.home");
                });

        };
    });
})();
