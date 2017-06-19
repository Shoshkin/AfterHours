(function () {
    'use strict';

    var app = angular.module("AfterHours", [
        'ui.router'
    ]);
    app.config.$inject = ["$locationProvider", "$stateProvider"];
    app.config(function ($locationProvider, $stateProvider) {
        $locationProvider.html5Mode(true);

        $stateProvider
            .state("root", {
                controller: "Root",
                templateUrl: "components/root/partial.html"
            })
            .state("home", {
                url: "/",
                parent: "root",
                controller: "Home",
                templateUrl: "components/home/partial.html"
            });
    });
})();