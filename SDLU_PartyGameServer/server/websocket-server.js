const { WebSocketServer } = require("ws");
const fs = require("fs");
const Logger = require('../util/logger');
var path = require('path');

let prefix = '[WebSocketServer] ';
const globalObj = {};

const maxRoomUser = 8;

function GenerateRoomManager() {
    globalObj.room = class {
        constructor(id, roomName) {
            this.id = id;
            this.roomName = roomName;
            this.roomUsers = [];
        }

        addUser(socket) {
            if (this.roomUsers.length >= maxRoomUser) {
                Logger.Debug(`${prefix} room ${this.id} is full`);
                return false;
            }
            this.roomUsers.push(socket);
        }

        removeUser(socket) {
            this.roomUsers.splice(this.roomUsers.indexOf(socket), 1);
        }

        getUsers() {
            return this.roomUsers;
        }

        getRoomName() {
            return this.roomName;
        }

        getRoomId() {
            return this.id;
        }
    }
    globalObj.roomManager = new class {
        constructor() {
            this.roomList = [];
        }
        createRoom(vo) {
            var room = new globalObj.room(this.roomList.length + 1, vo.title, vo.password);
            this.roomList.push(room);
            return room;
        }
        joinRoom(id, socket) {
            let room = this.getRoomById(id);
            if (room == null) {
                Logger.error(prefix + 'No room with id ' + id + ' found');
                return false;
            }
            room.addUser(socket);
        }
        leaveRoom(socket) {
            let room = this.getRoomBySocket(socket);
            if (room == null) {
                Logger.error(prefix + socket + ' is not in any room');
                return false;
            }
            room.removeUser(socket);
        }
        getRoomById(id) {
            for (let i = 0; i < this.roomList.length; i++) {
                if (this.roomList[i].getRoomId() == id) {
                    return this.roomList[i];
                }
            }
            return null;
        }
        getRoomBySocket(socket) {
            for (let i = 0; i < this.roomList.length; i++) {
                if (this.roomList[i].getUsers().indexOf(socket) != -1) {
                    return this.roomList[i];
                }
            }
            return null;
        }
        matchMaking() {
            this.roomList.forEach(room => {
                if (room.getUsers().length < maxRoomUser) {
                    return room;
                }
            });
            return this.createRoom();
        }
        getRoomData(id) {
            let room = this.getRoomById(id);
            if (room == null) {
                Logger.error(prefix + 'No room with id ' + id + ' found');
                return null;
            }
            return {
                id: room.getRoomId(),
                name: room.getRoomName(),
                userList: room.getUsers().map(socket => socket.user.getUserName())
            };
        }
    };
}

function GenerateUserManager() {
    globalObj.user = class {
        constructor(socket, name) {
            this.socket = socket;
            this.name = name;
        }
        getSocket() {
            return this.socket;
        }
        getName() {
            return this.name;
        }
    }
    globalObj.userManager = new class {
        constructor() {
            this.userList = [];
        }
        addUser(socket, vo) {
            var user = new globalObj.user(socket, vo.name, vo.password);
            socket.user = user;
            this.userList.push(user);
            return user;
        }
        getUserBySocket(socket) {
            for (let i = 0; i < this.userList.length; i++) {
                if (this.userList[i].getSocket() == socket) {
                    return this.userList[i];
                }
            }
            return null;
        }
        getUserByName(name) {
            for (let i = 0; i < this.userList.length; i++) {
                if (this.userList[i].getName() == name) {
                    return this.userList[i];
                }
            }
            return null;
        }
    };
}

class WebsocketServer {
    constructor(port) {
        GenerateRoomManager();
        GenerateUserManager();

        this.handlers = {};
        const handlerFiles = fs.readdirSync('handler').filter(file => file.endsWith('.js'));
        handlerFiles.forEach(file => {
            const handler = require(path.join('../handler', file));
            this.handlers[handler.type] = handler;
            Logger.Debug(prefix + 'Handler loaded: ' + handler.type + '.js');
        });

        this.port = port;
        this.server = new WebSocketServer({ port }, () => {
            Logger.Info(prefix + 'Server started on port ' + port);
        });

        this.server.on('connection', (socket) => {
            socket.globalObj = globalObj;
            Logger.Debug(`${prefix} connection ( ${socket.url} )- ${socket.upgradeReq.connection.remoteAddress}`);

            socket.on('message', (message) => {
                Logger.Debug(`${prefix} message ( ${socket.url} ) - ${message}`);
                this.handleMessage(socket, message);
            });

            socket.on('close', (code, reason) => {
                Logger.Debug(`${prefix} close ( ${socket.url} ) - ${code} - ${reason}`);
            });
        });
    }

    handleMessage(socket, message) {
        const json = JSON.parse(message);
        this.handlers[json.type].handle(socket, json.payload);
    }
}

exports.WebsocketServer = WebsocketServer;