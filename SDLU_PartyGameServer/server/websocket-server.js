const { WebSocketServer } = require("ws");
const fs = require("fs");
const Logger = require('../util/logger').Get('WebSocketServer');
const request = require('request');

var path = require('path');

const globalObj = {};

let TotalConnections = 0;
let TotalRooms = 0;

const maxRoomUser = 8;

const games = {};

const dirname = path.resolve('./');
const gameFolders = fs.readdirSync(path.join('game'));
gameFolders.forEach(folder => {
    const game = require(path.join(dirname, './game', folder, `${folder}.js`));
    const temp = new game();
    games[temp.id] = {
        class: game,
        handlers: {}
    }
    const handlers = fs.readdirSync(path.join(dirname, './game', folder, 'handler'));
    handlers.forEach(handler => {
        const handlerFile = require(path.join(dirname, './game', folder, 'handler', handler));
        games[temp.id].handlers[handlerFile.type] = handlerFile;
        Logger.Debug(`Game ${temp.id} handler ${handlerFile.type} added`);
    });

    Logger.Debug(`Game loaded: ${temp.id} - ${temp.name}`);
});

globalObj.games = games;

// 접어두는 것을 추천합니다
function GenerateRoomManager() {
    globalObj.room = class {
        constructor(id) {
            this.id = id;
            this.roomUsers = [];
            this.status = 'waiting';
            this.voter = [];
            this.vote = [];
            this.game = null;
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
            Logger.Debug(`room ${this.id} start vote`);
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
            // 개별 항목의 투표 결과를 구합니다
            let result = {};
            for (let i = 0; i < this.vote.length; i++) {
                if (!result[this.vote[i]]) {
                    result[this.vote[i]] = 0;
                }
                result[this.vote[i]]++;
            }

            // 가장 많은 투표 결과를 구하고 만약 같은 결과가 있다면 랜덤으로 결정합니다
            let max = 0;
            let maxKey = null;
            for (let key in result) {
                if (result[key] > max) {
                    max = result[key];
                    maxKey = key;
                }
            }
            if (maxKey == null) {
                maxKey = Math.floor(Math.random() * this.vote.length);
            }
            return maxKey;
        }
        startGame() {
            this.broadcast(JSON.stringify({
                type: 'RoomGameStart',
                payload: {
                    game: this.getVoteResult()
                }
            }));
            this.status = 'playing';
            const game = games[this.getVoteResult()];
            this.game = new game.class(this);
            this.game.start();
            Logger.Debug(`room ${this.id} start game`);
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
// 접어두는 것을 추천합니다
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