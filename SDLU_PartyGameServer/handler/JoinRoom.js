const { DataVO } = require('../type/VO');

class JoinRoomDataVO {
    constructor(id) {
        this.id = id;
    }
}

module.exports = {
    type: "JoinRoom",
    async handle(socket, payload) {
        const data = new JoinRoomDataVO(payload.id);
        const room = socket.globalObj.roomManager.joinRoom(data.id);
        if (room == null) {
            return;
        }
    }
}