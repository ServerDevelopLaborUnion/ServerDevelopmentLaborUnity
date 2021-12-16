var express = require('express')
const Logger = require('../util/logger');
var path = require('path');

let prefix = '[HttpServer] ';

class HttpServer {
    constructor(port) {
        this.app = express();
        this.port = port;

        this.app.listen(this.port, () => {
            Logger.Info(`${prefix} started at ${this.port}`);
        });

        this.app.get('/', (req, res) => {
            res.sendFile(path.resolve('public/index.html'));
            Logger.Debug(`${prefix} get ( ${req.url} )- ${req.ip}`);
        });
    }
}

exports.HttpServer = HttpServer;