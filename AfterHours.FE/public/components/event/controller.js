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

        $scope.isAttending = function(){

            if($scope.event) {
                var user = locker.get("session");
                var all_usernames = _.map($scope.event.Users, function(u){return u.Username});
                if(all_usernames.indexOf(user.username) != -1){
                    return true;
                }
            }
            
            return false;
        };

        $scope.attend = function()
        {
            ApiComm.post("api/Attendances/" + $state.params.id, {})
                    .then(function(){
                        return ApiComm.get("api/events/" + $state.params.id);
                    })
                    .then(function (response) {
                        $scope.event = response.data;
                    });
        };
        
        $scope.cancelAttend = function()
        {
            ApiComm.delete("api/Attendances/" + $state.params.id, {})
                    .then(function(){
                        return ApiComm.get("api/events/" + $state.params.id);
                    })
                    .then(function (response) {
                        $scope.event = response.data;
                    });
        };
    });
})();
