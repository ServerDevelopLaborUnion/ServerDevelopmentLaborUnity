class User {
    constructor(socket, id, name) {
        this.socket = socket;
        this.id = id;
        this.name = name;
        this.room = null;
    }
    getSocket() {
        return this.socket;
    }
}

class UserManager {
    constructor(Logger, roomManager) {
        this.userList = [];
        this.Logger = Logger;
        this.roomManager = roomManager;
        this.TotalConnections = 0;
    }
    addUser(socket, name) {
        var user = new User(socket, this.TotalConnections, name);
        socket.user = user;
        this.userList.push(user);
        this.Logger.Debug(`User ${user.id} added, Total: ${this.userList.length}`);
        this.TotalConnections++;
        return user;
    }
    removeUser(socket) {
        this.roomManager.leaveRoom(socket);
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
}

exports.User = User;
exports.UserManager = UserManager;