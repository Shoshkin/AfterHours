(function () {
    'use strict';

    var app = angular.module("AfterHours", [
        'ui.router',
        'ui.bootstrap'
    ]);
    app.config.$inject = ["$locationProvider", "$stateProvider", "$urlRouterProvider"];
    app.config(function ($locationProvider, $stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("root", {
                abstract: true,
                controller: "Root",
                templateUrl: "components/root/partial.html"
            })
            .state("root.login", {
                url: "/login",
                controller: "Login",
                templateUrl: "components/login/partial.html"
            })
            .state("root.home", {
                url: "/",
                controller: "Home",
                templateUrl: "components/home/partial.html"
            })
            .state("event", {
                url "event"
                parent: "root",
                controller: "Event",
                templateUrl: "components/event/partial.html"
            });


        $locationProvider.html5Mode(true);
        // $urlRouterProvider.otherwise("/template1");
        $urlRouterProvider.otherwise('/');
    });
})();
