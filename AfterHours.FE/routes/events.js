var express = require('express');
var rp = require('request-promise');
var url = require("url");
var ulrJoin = require("url-join");
var router = express.Router();

var baseUrl = "Events";

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
                uri: ulrJoin(apiUrl, baseUrl)
            }
        ).then(function (response) {
            res.send(response);
        });
    });

    router.get('/:id', function (req, res, next) {
        makeRequest(
            {
                uri: ulrJoin(apiUrl, baseUrl, req.params.id),
                headers: {
                    'afterHoursAuth': req.headers.afterhoursauth
                }
            }
        ).then(function (response) {
            res.send(response);
        });
    });

    router.post('/', function (req, res, next) {
        makeRequest(
            {
                method: 'POST',
                uri: ulrJoin(apiUrl, baseUrl),
                headers: {
                    'afterHoursAuth': req.headers.afterhoursauth
                },
                body: req.body,
                json: true
            }
        ).then(function (response) {
            res.send(response);
        });
    });
    return router;
};
