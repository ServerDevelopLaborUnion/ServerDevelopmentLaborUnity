const { DataVO } = require('../type/VO');
const prefix = '[GetRoomData] ';

class ReadyVO {
    constructor(state) {
        this.state = state;
    }
}

module.exports = {
    type: "Ready",
    async handle(socket, payload) {
        const readyVO = new ReadyVO(payload.state);
        socket.roomManager.getRoomBySocket(socket).setReadyState(socket.user.getUserId, readyVO.state);
    }
}