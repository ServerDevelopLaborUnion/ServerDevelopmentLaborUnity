const { WebSocketServer } = require("ws");
const fs = require("fs");
const Logger = require('../util/logger');
const request = require('request');

var path = require('path');

let prefix = '[WebSocketServer] ';
const globalObj = {};

let randomSeed = Math.random();

const maxRoomUser = 8;

function GenerateRoomManager() {
    globalObj.room = class {
        constructor(id, roomName) {
            this.id = id;
            this.roomName = roomName;
            this.roomUsers = [];
            this.voter = [];
            this.vote = [];
        }
        broadcast(data) {
            for (let i = 0; i < this.roomUsers.length; i++) {
                this.roomUsers[i].send(data);
            }
        }
        addUser(socket) {
            if (this.roomUsers.length >= maxRoomUser) {
                Logger.Debug(`${prefix} room ${this.id} is full`);
                return false;
            }
            this.roomUsers.push(socket);
            this.broadcast(JSON.stringify({
                type: 'RoomUserJoin',
                data: {
                    user: socket.user.id
                }
            }));
            return true;
        }
        removeUser(socket) {
            this.roomUsers.splice(this.roomUsers.indexOf(socket), 1);
            this.broadcast(JSON.stringify({
                type: 'RoomUserLeave',
                data: {
                    user: socket.user.id
                }
            }));
        }
        getUsers() {
            let users = [];
            for (let i = 0; i < this.roomUsers.length; i++) {
                users.push(this.roomUsers[i].user);
            }
            return users;
        }
        getRoomName() {
            return this.roomName;
        }
        getRoomId() {
            return this.id;
        }
        setReadyState(socket, state) {
            if (state) {
                socket.ready = true;
            } else {
                socket.ready = false;
            }

            this.broadcast(JSON.stringify({
                type: 'RoomUserReady',
                data: {
                    user: socket.user.id,
                    ready: state
                }
            }));

            if (this.isAllReady()) {
                this.startGameVote();
            }
        }
        isAllReady() {
            for (let i = 0; i < this.roomUsers.length; i++) {
                if (!this.roomUsers[i].ready) {
                    return false;
                }
            }
            return true;
        }
        startGameVote() {
            this.voter = [];
            for (let i = 0; i < this.roomUsers.length; i++) {
                this.voter.push(this.roomUsers[i].user.id);
            }
            this.broadcast(JSON.stringify({
                type: 'RoomStartVote',
                data: {
                    count: this.voter.length
                }
            }));
        }
        vote(socket, vote) {
            if (this.voter.indexOf(socket.user.id) < 0) return;
            this.voter.splice(this.voter.indexOf(socket.user.id), 1);

            this.vote.push(vote);

            this.broadcast(JSON.stringify({
                type: 'RoomUserVote',
                data: {
                    user: socket.user.id
                }
            }));

            if (this.voter.length <= 0) {
                this.startGame();
            }
        }
        getVoteResult() {
            // return most voted
            // if vote count is equal, return random
            let max = 0;
            let maxIndex = 0;
            for (let i = 0; i < this.vote.length; i++) {
                if (this.vote[i] > max) {
                    max = this.vote[i];
                    maxIndex = i;
                }
            }
            if (this.vote.length == max) {
                return this.roomUsers[Math.floor(Math.random() * this.roomUsers.length)].user.id;
            }
            return this.roomUsers[maxIndex].user.id;
        }
        startGame() {
            this.broadcast(JSON.stringify({
                type: 'RoomGameStart',
                data: {
                    game: this.getVoteResult()
                }
            }));
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
        constructor(id, name) {
            this.id = id;
            this.name = name;
        }
        getSocket() {
            globalObj.sockets.forEach(socket => {
                if (socket.user.getUserName() == this.name) {
                    return socket;
                }
            });
        }
        getUserName() {
            return this.name;
        }
        getUserId() {
            return this.id;
        }
    }
    globalObj.userManager = new class {
        constructor() {
            this.userList = [];
        }
        addUser(socket, vo) {
            var user = new globalObj.user(socket, vo.name);
            socket.user = user;
            this.userList.push(user);
            return user;
        }
        leaveRoom(socket) {
            let user = this.getUserBySocket(socket);
            if (user == null) {
                Logger.Error(prefix + socket + ' is not in any room');
                return false;
            }
            this.userList.splice(this.userList.indexOf(user), 1);
            if (globalObj.roomManager.getRoomBySocket(socket) != null) {
                globalObj.roomManager.leaveRoom(socket);
            }
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
                if (this.userList[i].getUserName() == name) {
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
        globalObj.sockets = [];

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

        globalObj.server = this.server;

        this.server.on('connection', async (socket) => {
            globalObj.userManager.addUser(socket, { name: await this.getRandomName() });
            globalObj.sockets.push(socket);
            socket.globalObj = globalObj;
            Logger.Debug(`${prefix} connection ( ${socket.url} )`);

            socket.on('message', (message) => {
                Logger.Debug(`${prefix} message ( ${socket.url} ) - ${message}`);
                this.handleMessage(socket, message);
            });

            socket.on('close', (code) => {
                Logger.Debug(`${prefix} close ( ${socket.url} ) - ${code}`);
                globalObj.userManager.leaveRoom(socket);
                globalObj.sockets.splice(globalObj.sockets.indexOf(socket), 1);
            });
        });
    }

    handleMessage(socket, message) {
        const json = JSON.parse(message);
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