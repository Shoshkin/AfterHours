(function () {
    'use strict';

    var app = angular.module("AfterHours", [
        'ui.router',
        // 'ui.bootstrap',
        'ngTagsInput',
        'angular-locker',
        'angular-timeline'
    ]);
    app.config.$inject = ["$locationProvider", "$stateProvider", "$urlRouterProvider", "lockerProvider"];
    app.config(function ($locationProvider, $stateProvider, $urlRouterProvider, lockerProvider) {
        $stateProvider
            .state("root", {
                abstract: true,
                controller: "Root",
                templateUrl: "components/root/partial.html"
            })
            .state("root.home", {
                url: "/",
                controller: "Home",
                templateUrl: "components/home/partial.html"
            })
            .state("root.signin", {
                url: "/signin",
                controller: "Signin",
                templateUrl: "components/signin/partial.html"
            })
            .state("root.signup", {
                url: "/signup",
                controller: "Signup",
                templateUrl: "components/signup/partial.html"
            })
            .state("root.add", {
                url: "/add",
                controller: "Add",
                templateUrl: "components/add/partial.html"
            })
            .state("root.event", {
                url: "/event/:id",
                params: {
                  id: null
                },
                controller: "Event",
                templateUrl: "components/event/partial.html"
            });


        $locationProvider.html5Mode(true);
        // $urlRouterProvider.otherwise("/template1");
        $urlRouterProvider.otherwise('/');

        lockerProvider.defaults({
            driver: 'session',
            namespace: 'after-hours',
            separator: '.'
        });
    });
})();
