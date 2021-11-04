const { User } = require("../Types/Type");

class UserUtil
{
    constructor()
    {
        this.userDict = {}
    }

    addUser(uuid, user)
    {
        this.userDict[uuid] = user;
    }

    removeUser(socket) {
        for (var key in this.userDict) {
            if (this.userDict[key].socket == socket) 
            {
                delete this.userDict[key];
            }
        }
    }

    /**
     * 접속한 모든 유저 불러오기
     * @returns {Array<User>}
     */
    getUsers()
    {
        let users = [];

        for (var key in this.userDict) {
            console.log("key:" + key);
            users.push(key);
        }

        return users;
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
        return this.userDict[uuid];
    }
}

exports.UserUtil = new UserUtil();