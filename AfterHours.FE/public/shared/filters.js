(function () {
    var app = angular.module("AfterHours");

    var dictionary = {
        "sport": "ספורט"
    };


    app.filter("toHebrew", function () {
        return function (word) {
            if (word in dictionary){
                return dictionary[word];
            }

            return word;
        };
    })
})();