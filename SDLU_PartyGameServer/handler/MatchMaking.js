const { DataVO } = require('../type/VO');
const Logger = require('../util/logger');

const prefix = '[MatchMaking] ';

module.exports = {
    type: "MatchMaking",
    async handle(socket) {
        const room = socket.globalObj.roomManager.matchMaking();
        if (room == null) {
            Logger.Debug(prefix + 'No room found');
            return;
        }
        const vo = new DataVO("JoinRoom", { "roomId" : room.getRoomId() });
        socket.send(vo);
    }
}