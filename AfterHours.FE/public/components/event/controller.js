(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$state", "ApiComm", "locker"];
    app.controller("Event", function ($scope, $state, ApiComm, locker) {
        ApiComm.get("api/events/" + $state.params.id)
            .then(function (response) {
                $scope.event = response.data;
            });

        $scope.sendComment = function () {
            if ($scope.comment) {
                var user = locker.get("session");
                ApiComm.post("api/comments/" + $state.params.id , {"Username":user.username, "Content": $scope.comment })
                    .then(function(){
                        return ApiComm.get("api/events/" + $state.params.id);
                    })
                    .then(function (response) {
                        $scope.comment = "";
                        $scope.event = response.data;
                    });
            }
        };
    });
})();
