const { WebSocketServer } = require("ws");
const Logger = require('../util/logger');
var path = require('path');

let prefix = '[WebSocketServer] ';

class WebsocketServer {
    constructor(port) {
        this.handlers = {};
        const handlerFiles = fs.readdirSync('./handler').filter(file => file.endsWith('.js'));
        handlerFiles.forEach(file => {
            const handler = require(path.join('./handler', file));
            this.handlers[handler.type] = handler;
        });

        this.port = port;
        this.server = new WebSocketServer({ port }, () => {
            Logger.Log.Info(prefix + 'Server started on port ' + port);
        });

        this.server.on('connection', (socket) => {
            Logger.Log.Debug(`${prefix} connection ( ${socket.url} )- ${socket.upgradeReq.connection.remoteAddress}`);

            socket.on('message', (message) => {
                Logger.Log.Debug(`${prefix} message ( ${socket.url} ) - ${message}`);
                this.handleMessage(socket, message);
            });

            socket.on('close', (code, reason) => {
                Logger.Log.Debug(`${prefix} close ( ${socket.url} ) - ${code} - ${reason}`);
            });
        });
    }

    handleMessage(socket, message) {
        const json = JSON.parse(message);
        this.handlers[json.type].handle(socket, json.payload);
    }
}

exports.WebsocketServer = WebsocketServer;