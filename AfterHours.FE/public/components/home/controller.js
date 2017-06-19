(function () {
    'use strict';

    angular.module("AfterHours")
        .controller("Home", function ($scope) {
            $scope.msg = "I love London";
            console.log($scope);
        });
})();