const { DataVO } = require('../type/VO');

module.exports = {
    type: "LeaveRoom",
    async handle(socket, payload) {
        socket.globalObj.roomManager.leaveRoom(socket);
    }
}