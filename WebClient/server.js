'use strict';
var port = process.env.PORT || 1337;
var connect = require('connect');
var serveStatic = require('serve-static');

connect()
    .use(serveStatic("public"))
    .listen(port, () => console.log("Server listening on port: " + port));