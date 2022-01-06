const { DataVO } = require('../type/VO');
const Logger = require('../util/logger').Get("MatchMaking");

module.exports = {
    type: "MatchMaking",
    async handle(socket) {
        const room = socket.globalObj.roomManager.matchMaking(socket);
        socket.send(JSON.stringify({
            type: "JoinRoom",
            payload: {
                "id": room.id
            }
        }));
    }
}