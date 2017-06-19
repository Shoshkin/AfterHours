var express = require('express');
var rp = require('request-promise');
var url = require("url");
var ulrJoin = require("url-join");
var router = express.Router();

var baseUrl = "Organizers";

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

    router.post('/', function (req, res, next) {
        makeRequest(
            {
                method: 'POST',
                uri: ulrJoin(apiUrl, baseUrl),
                body: req.body
            }
        ).then(function (response) {
            res.send(response);
        });
    });
    return router;
};
