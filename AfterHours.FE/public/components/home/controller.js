(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$scope", "$state", "ApiComm", "$filter"];
    app.controller("Home", function ($scope, $state, ApiComm, $filter) {
        ApiComm.get("api/events")
            .then(function successCallback(response) {
                $scope.previewEvents = response.data;
                $scope.eventsToShow = $scope.filterEventsByTags($scope.previewEvents, $scope.tags);
                $scope.eventsToTimeline($scope.eventsToShow);
            }, function fail(response) {
                console.log(response);
            });

        $scope.clickEvent = function (eventId) {
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
            return events;
        };

        $scope.tagRemoved = function (tag) {
            if (tag in $scope.tags) {
                $scope.tags.remove(tag);
            }

            $scope.eventsToTimeline($scope.filterEventsByTags($scope.previewEvents, $scope.tags));
            $scope.eventsToShow = $scope.filterEventsByTags($scope.previewEvents, $scope.tags);
        };

        $scope.tagAdded = function (tag) {
            if (!$scope.tags) {
                $scope.tags = [tag];
            }

            $scope.eventsToTimeline($scope.filterEventsByTags($scope.previewEvents, $scope.tags));
            $scope.eventsToShow = $scope.filterEventsByTags($scope.previewEvents, $scope.tags);
        };

        $scope.eventsToTimeline = function (events) {
            $scope.timeLineEvents = [];
            var sides = ["left", "right"];
            var sidesIndex = 0;
            for (var event in _.sortBy(events, ["StartTime"])) {
                sidesIndex++;
                console.log(events[event]);
                $scope.timeLineEvents.push({
                    badgeClass: 'danger',
                    eventId: events[event].EventId,
                    side: sides[sidesIndex % 2],
                    // attendees: events[event]
                    title: events[event].Name,
                    currentAttandance: events[event].CurrentAttandance,
                    maxAttandence: events[event].MaxAttandence,
                    place: events[event].Place,
                    startTime: events[event].StartTime,
                    tags: events[event].Tags
                });
            }
        };
    });
})();
