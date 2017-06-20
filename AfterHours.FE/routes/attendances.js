var express = require('express');
var rp = require('request-promise');
var url = require("url");
var ulrJoin = require("url-join");
var router = express.Router();

var baseUrl = "Attendances";

var makeRequest = function (options) {
    return rp(options)
        .then(function (response) {
            return response;
        })
        .catch(function (err) {
            // console.log(err);
        });
};
module.exports = function (apiUrl) {

    router.post('/:id', function (req, res, next) {
        makeRequest(
            {
                method: 'POST',
                uri: ulrJoin(apiUrl, baseUrl, "?eventId=" + req.params.id),
                body: req.body,
                headers: {
                    'afterHoursAuth': req.headers.afterhoursauth
                },
                json: true
            }
        ).then(function (response) {
            res.send(response);
        });
    });

    router.delete('/:id', function (req, res, next) {
        console.log(req.headers)
        makeRequest(
            {
                method: 'DELETE',
                uri: ulrJoin(apiUrl, baseUrl, "?eventId=" + req.params.id),
                headers: {
                    'afterHoursAuth': req.headers.afterhoursauth
                },
                json: true
            }
        ).then(function (response) {
            res.send(response);
        });
    });

    return router;
};
