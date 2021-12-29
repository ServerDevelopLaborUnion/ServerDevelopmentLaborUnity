const { WebSocketServer } = require("ws");
const fs = require("fs");
const Logger = require('../util/logger').Get('WebSocketServer');
const request = require('request');

var path = require('path');

const globalObj = {};

let TotalConnections = 0;
let TotalRooms = 0;

const maxRoomUser = 8;

function GenerateRoomManager() {
    globalObj.room = class {
        constructor(id) {
            this.id = id;
            this.roomUsers = [];
            this.status = 'waiting';
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
                Logger.Debug(`room ${this.id} is full`);
                return false;
            }
            this.roomUsers.push(socket);
            this.broadcast(JSON.stringify({
                type: 'RoomUserJoin',
                payload: {
                    user: socket.user.id
                }
            }));
            return true;
        }
        removeUser(socket) {
            this.roomUsers.splice(this.roomUsers.indexOf(socket), 1);
            this.broadcast(JSON.stringify({
                type: 'RoomUserLeave',
                payload: {
                    user: socket.user.id
                }
            }));

            if (this.roomUsers.length <= 0) {
                globalObj.roomManager.removeRoom(this.id);
            }
        }
        getUsers() {
            let users = [];
            for (let i = 0; i < this.roomUsers.length; i++) {
                users.push(this.roomUsers[i].user);
            }
            return users;
        }
        setReadyState(socket, state) {
            if (state == socket.ready) return;

            if (state) {
                socket.ready = true;
            } else {
                socket.ready = false;
            }

            this.broadcast(JSON.stringify({
                type: 'RoomUserReady',
                payload: {
                    user: socket.user.id,
                    ready: state
                }
            }));

            if (this.isAllReady()) {
                Logger.Debug(`room ${this.id} is all ready`);
                this.startGameVote();
            }
        }
        isAllReady() {
            if (this.roomUsers.length <= 2) return false;
            for (let i = 0; i < this.roomUsers.length; i++) {
                if (!this.roomUsers[i].ready) {
                    return false;
                }
            }
            return true;
        }
        startGameVote() {
            this.voter = [];
            this.status = 'voting';
            for (let i = 0; i < this.roomUsers.length; i++) {
                this.voter.push(this.roomUsers[i].user.id);
            }
            this.broadcast(JSON.stringify({
                type: 'RoomStartVote',
                payload: {
                    count: this.voter.length
                }
            }));
        }
        voteGame(socket, vote) {
            Logger.Debug(`room ${this.id} vote ${vote}, voter: ${socket.user.id}, total: ${this.voter.length}`);
            if (this.voter.indexOf(socket.user.id) < 0) return;
            this.voter.splice(this.voter.indexOf(socket.user.id), 1);

            this.vote.push(vote);

            this.broadcast(JSON.stringify({
                type: 'RoomUserVote',
                payload: {
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
                payload: {
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
            var room = new globalObj.room(TotalRooms);
            this.roomList.push(room);
            TotalRooms++;
            return room;
        }
        removeRoom(id) {
            this.roomList.splice(this.roomList.indexOf(id), 1);
        }
        joinRoom(id, socket) {
            let room = this.getRoomById(id);
            if (room == null) {
                Logger.error('No room with id ' + id + ' found');
                return false;
            }
            room.addUser(socket);
            socket.user.room = room;
        }
        leaveRoom(socket) {
            const room = socket.user.room;
            if (room == null) {
                Logger.Error('User ' + socket.user.id + ' is not in any room');
                return false;
            }
            room.removeUser(socket);
            Logger.Debug(socket.user.id + ' leave room ' + room.id);
        }
        getRoomById(id) {
            for (let i = 0; i < this.roomList.length; i++) {
                if (this.roomList[i].id == id) {
                    return this.roomList[i];
                }
            }
            return null;
        }
        matchMaking(socket) {
            Logger.Debug('Room Count: ' + this.roomList.length);
            let matchedRoom = null;
            this.roomList.forEach(room => {
                if (room.getUsers().length < maxRoomUser) {
                    if (room.status == 'waiting') {
                        matchedRoom = room;
                    }
                }
            });
            if (matchedRoom == null) {
                matchedRoom = this.createRoom();
            }
            this.joinRoom(matchedRoom.id, socket);

            this.roomList.forEach(room => {
                Logger.Debug(`Room ${room.id} has ${room.getUsers().length} users`);
            });

            return matchedRoom;
        }
        getRoomData(id) {
            let room = this.getRoomById(id);
            if (room == null) {
                Logger.error('No room with id ' + id + ' found');
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
        constructor(socket, id, name) {
            this.socket = socket;
            this.id = id;
            this.name = name;
            this.room = null;
        }
        getSocket() {
            globalObj.sockets.forEach(socket => {
                if (socket.user.getUserName() == this.name) {
                    return socket;
                }
            });
        }
    }
    globalObj.userManager = new class {
        constructor() {
            this.userList = [];
        }
        addUser(socket, name) {
            var user = new globalObj.user(socket, TotalConnections, name);
            socket.user = user;
            this.userList.push(user);
            Logger.Debug(`User ${user.id} added, Total: ${this.userList.length}`);
            TotalConnections++;
            return user;
        }
        removeUser(socket) {
            globalObj.roomManager.leaveRoom(socket);
            this.userList.splice(this.userList.indexOf(socket.user), 1);
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
            socket.globalObj = globalObj;

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
                Logger.Debug(`Client disconnected: \x1b[31m${socket.user.id} - ${socket.user.name}`);
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