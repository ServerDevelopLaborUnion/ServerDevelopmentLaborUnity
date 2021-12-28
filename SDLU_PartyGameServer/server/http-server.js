var express = require('express')
const Logger = require('../util/logger').Get('HttpServer');
var path = require('path');

class HttpServer {
    constructor(port) {
        this.app = express();
        this.port = port;

        this.app.listen(this.port, () => {
            Logger.Info(`started at ${this.port}`);
        });

        this.app.get('/', (req, res) => {
            res.sendFile(path.resolve('public/index.html'));
            Logger.Debug(`get ( ${req.url} )- ${req.ip}`);
        });
    }
}

exports.HttpServer = HttpServer;