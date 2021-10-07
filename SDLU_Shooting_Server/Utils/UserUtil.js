const { User } = require("../Types/Type");

class UserUtil
{
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
    getUserByID(wsServer, uuid) {
        var user = null;
        wsServer.clients.forEach(socket => {
            if (socket.user.uuid == uuid)
            {
                user = socket.user;
                return user;
            }
        });
        return user;
    }
}

exports.UserUtil = new UserUtil();