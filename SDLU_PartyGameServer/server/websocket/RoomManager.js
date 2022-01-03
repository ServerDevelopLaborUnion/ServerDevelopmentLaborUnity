const Logger = require("../../util/logger").Get("RoomManager");

const fs = require("fs");
const path = require("path");

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

const maxRoomUser = 8;

class Room {
    constructor(id, roomManager, Logger) {
        this.id = id;
        this.roomUsers = [];
        this.status = 'waiting';
        this.voter = [];
        this.vote = [];
        this.game = null;
        this.roomManager = roomManager;
        this.Logger = Logger;
    }
    broadcast(data) {
        for (let i = 0; i < this.roomUsers.length; i++) {
            this.roomUsers[i].send(data);
        }
    }
    addUser(socket) {
        if (this.roomUsers.length >= maxRoomUser) {
            this.Logger.Debug(`room ${this.id} is full`);
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
            this.roomManager.removeRoom(this.id);
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
            this.Logger.Debug(`room ${this.id} is all ready`);
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
        this.Logger.Debug(`room ${this.id} start vote`);
    }
    voteGame(socket, vote) {
        this.Logger.Debug(`room ${this.id} vote ${vote}, voter: ${socket.user.id}, total: ${this.voter.length}`);
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
        console.log(game);
        this.game = new game.class(this);
        this.game.start();
        this.Logger.Debug(`room ${this.id} start game`);
    }
}

class RoomManager {
    constructor(Logger) {
        this.roomList = [];
        this.roomCount = 0;
        this.Logger = Logger;
    }
    createRoom() {
        var room = new Room(this.roomCount, this, this.Logger);
        this.roomList.push(room);
        this.roomCount++;
        return room;
    }
    removeRoom(id) {
        this.roomList.splice(this.roomList.indexOf(id), 1);
    }
    joinRoom(id, socket) {
        let room = this.getRoomById(id);
        if (room == null) {
            this.Logger.error('No room with id ' + id + ' found');
            return false;
        }
        room.addUser(socket);
        socket.user.room = room;
    }
    leaveRoom(socket) {
        const room = socket.user.room;
        if (room == null) {
            this.Logger.Error('User ' + socket.user.id + ' is not in any room');
            return false;
        }
        room.removeUser(socket);
        this.Logger.Debug(socket.user.id + ' leave room ' + room.id);
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
        this.Logger.Debug('Room Count: ' + this.roomList.length);
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
            this.Logger.Debug(`Room ${room.id} has ${room.getUsers().length} users`);
        });

        return matchedRoom;
    }
    getRoomData(id) {
        let room = this.getRoomById(id);
        if (room == null) {
            this.Logger.error('No room with id ' + id + ' found');
            return null;
        }
        return {
            id: room.getRoomId(),
            name: room.getRoomName(),
            userList: room.getUsers().map(socket => socket.user.getUserName())
        };
    }
}

exports.Room = Room;
exports.RoomManager = RoomManager;