(function () {
    'use strict';

    var app = angular.module("AfterHours");
    app.$inject = ["$http", "locker"];
    app.service("ApiComm", function ($http, locker) {
        this.get = function (url) {
            if (locker.has('session')){
                var user = locker.get('session');
                return $http.get(url, {headers: {afterHoursAuth: user.username + "," + user.password}});
            }
            return $http.get(url);
        };

        this.post = function (url, body) {
            if (locker.has('session')) {
                var user = locker.get('session');
                return $http.post(url, body, {headers: {afterHoursAuth: user.username + "," + user.password}});
            }
            return $http.post(url);
        };
    })
})();
