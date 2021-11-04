const { User } = require("../Types/Type");

class UserUtil
{
    constructor()
    {
        /** @type {User} */
        this.userArr = [];
    }

    addUser(uuid, user)
    {
        this.userArr.push(user);
    }

    removeUser(socket) {
        var index = this.userArr.indexOf(socket.user);
        if (index > -1) {
            this.userArr.splice(index, 1);
        }
    }

    /**
     * 접속한 모든 유저 불러오기
     * @returns {Array<User>}
     */
    getUsers()
    {
        return this.userArr;
    }

    /**
     * 소켓에서 유저 불러오기
     * @param {WebSocket} socket
     * @returns {User}
     */
    getUserBySocket(socket)
    {
        return socket.user;
    }

    /**
     * UUID로 유저 불러오기
     * @param {string} uuid
     * @returns {User}
     */
    getUserByUUID(uuid)
    {
        for (var i = 0; i < this.userArr.length; i++) {
            if (this.userArr[i].uuid === uuid) {
                return this.userArr[i];
            }
        }
        return null;
    }
    getUserByNickname(nickname)
    {
        for (var i = 0; i < this.userArr.length; i++) {
            if (this.userArr[i].nickname === nickname) {
                console.log("findUser!: " + this.userArr[i]);
                return this.userArr[i];
            }
        }
        console.log("userNotFinded...: " + this.userArr[i]);
        return null;
    }
    getUserBySessionId(sessionId)
    {
        for (var i = 0; i < this.userArr.length; i++) {
            if (this.userArr[i].id === sessionId) {
                return this.userArr[i];
            }
        }
        return null;
    }
}

exports.UserUtil = new UserUtil();