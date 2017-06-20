var express = require('express');
var rp = require('request-promise');
var url = require("url");
var ulrJoin = require("url-join");
var router = express.Router();

var baseUrl = "Tags";

var makeRequest = function (options) {
    return rp(options)
        .then(function (response) {
            return response;
        })
        .catch(function (err) {
            console.log(err);
        });
};
module.exports = function (apiUrl) {
    router.get('/', function (req, res, next) {
        makeRequest(
            {
                method: 'GET',
                uri: ulrJoin(apiUrl, baseUrl),
                headers: {
                    'afterHoursAuth': req.headers.afterHoursAuth
                },
                json: true
            }
        ).then(function (response) {
            res.send(response);
        });
    });

    return router;
};
