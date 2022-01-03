const { WebSocket, WebSocketServer } = require("ws");
const fs = require("fs");
const Logger = require('../util/logger').Get('WebSocketServer');
const request = require('request');

const { Room, RoomManager } = require('./websocket/RoomManager');
const { User, UserManager } = require('./websocket/UserManager');

var path = require('path');

const globalObj = {
    room: Room,
    roomManager: new RoomManager(Logger),
    user: User,
    userManager: null,
    Logger: Logger,
    server: null,
    sockets: [],
};

globalObj.userManager = new UserManager(Logger, globalObj.roomManager),

WebSocket.prototype.globalObj = globalObj;

class WebsocketServer {
    constructor(port) {
        this.handlers = {};
        const handlerFiles = fs.readdirSync('handler').filter(file => file.endsWith('.js'));
        handlerFiles.forEach(file => {
            if (file.startsWith('_')) return;
            const handler = require(path.join('../handler', file));
            this.handlers[handler.type] = handler;
            Logger.Debug('Handler loaded: ' + handler.type + '.js');
        });

        this.port = port;
        this.server = new WebSocketServer({ port }, () => {
            Logger.Info('Server started on port ' + port);
        });

        globalObj.server = this.server;

        this.server.on('connection', (socket) => {

            globalObj.userManager.addUser(socket, { name: 'null' });
            globalObj.sockets.push(socket);

            this.getRandomName().then(username => {
                socket.user.name = username;
                socket.send(JSON.stringify({
                    type: 'UserName',
                    payload: {
                        name: username,
                        id: socket.user.id
                    }
                }));
                Logger.Debug(`Client connected: \x1b[32m${socket.user.id} + ${socket.user.name}`);
            });

            socket.on('message', (message) => {
                Logger.Debug(`message ( ${socket.user.id} ) - ${message}`);
                this.handleMessage(socket, message);
            });

            socket.on('close', (code) => {
                globalObj.userManager.removeUser(socket);
                globalObj.sockets.splice(globalObj.sockets.indexOf(socket), 1);
                Logger.Debug(`Client disconnected: \x1b[31m${socket.user.id} - ${socket.user.name} (${code})`);
            });
        });
    }

    handleMessage(socket, message) {
        const json = JSON.parse(message);
        if (this.handlers[json.type] == null) {
            Logger.Error('No handler for type ' + json.type);
            return;
        }
        this.handlers[json.type].handle(socket, json.payload);
    }

    getRandomName() {
        return new Promise((resolve, reject) => {
            request.get('https://nickname.hwanmoo.kr/?format=json&count=1&max_length=1', (error, response, body) => {
                if (error) {
                    reject(error);
                }
                const data = JSON.parse(body);
                resolve(data.words[0]);
            });
        });
    }
}

exports.WebsocketServer = WebsocketServer;