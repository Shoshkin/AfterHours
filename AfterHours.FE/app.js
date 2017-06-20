var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var app = express();

// view engine setup
// app.set('views', path.join(__dirname, 'views'));
// app.set('view engine', 'jade');

// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: false}));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

var events = require('./routes/events');
var comments = require('./routes/comments');
var signin = require('./routes/signin');
var tags = require('./routes/tags');
var signup = require('./routes/signup');
var attendances = require('./routes/attendances');

app.use('/api/events', events("http://localhost:55049/api/"));
app.use('/api/comments', comments("http://localhost:55049/api/"));
app.use('/api/signin', signin("http://localhost:55049/api/"));
app.use('/api/tags', tags("http://localhost:55049/api/"));
app.use('/api/signup', signup("http://localhost:55049/api/"));
app.use('/api/attendances', attendances("http://localhost:55049/api/"));
app.use("/", function (req, res) {
    // Use res.sendfile, as it streams instead of reading the file into memory.
    res.sendFile(__dirname + '/public/index.html');
});
// app.use('/api/events', index);


// catch 404 and forward to error handler
app.use(function (req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

// error handler
app.use(function (err, req, res, next) {
    // set locals, only providing error in development
    res.locals.message = err.message;
    res.locals.error = req.app.get('env') === 'development' ? err : {};

    // render the error page
    res.status(err.status || 500);
    console.log(err);
    // res.render('error');
});

module.exports = app;
