const { DataVO } = require('../type/VO');

class ReadyVO {
    constructor(state) {
        this.state = state;
    }
}

module.exports = {
    type: "Ready",
    async handle(socket, payload) {
        const readyVO = new ReadyVO(payload.state);
        socket.user.room.setReadyState(socket, readyVO.state);
    }
}