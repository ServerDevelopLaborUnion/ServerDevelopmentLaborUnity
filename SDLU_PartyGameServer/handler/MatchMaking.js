const { DataVO } = require('../type/VO');
const Logger = require('../util/logger').Get("MatchMaking");

module.exports = {
    type: "MatchMaking",
    async handle(socket) {
        const room = socket.globalObj.roomManager.matchMaking(socket);
        const vo = new DataVO("JoinRoom", { "id" : room.id });
        socket.send(JSON.stringify(vo));
    }
}