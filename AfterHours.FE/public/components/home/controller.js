(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$state", "ApiComm"];
    app.controller("Home", function ($scope, $state, ApiComm) {
        ApiComm.get("api/events")
            .then(function successCallback(response) {
                $scope.previewEvents = $scope.filterEventsByTags(response.data, $scope.tags);
            }, function fail(response) {
                console.log(response);
            });

        ApiComm.clickEvent = function (eventId) {
            $state.go("root.event", {"id": eventId});
        };

        $scope.filterEventsByTags = function (events, tags) {
            if (tags && tags.length > 0) {
                return _.filter(events, function (e) {
                    if (e.Tags) {
                        return _.intersection(e.Tags.split(","), _.map(tags, function (m) {
                                return m.text;
                            })).length > 0;
                    }

                    return false;
                });
            }

            return events
            // _.filter(events, function (e) {return tags.split()})
        };

        $scope.updateEvents = function (tag) {
            if (!$scope.tags) {
                $scope.tags = [tag];
            }

            return $scope.previewEvents = $scope.filterEventsByTags($scope.previewEvents, $scope.tags);
        };
    });
})();
