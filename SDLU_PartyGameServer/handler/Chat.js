const { DataVO } = require('../type/VO');

class ChatVO {
    constructor(data) {
        this.message = data.message;
    }
}

module.exports = {
    type: "Chat",
    async handle(socket, payload) {
        const data = new ChatVO(payload);
        socket.user.room.chat(socket, data.message);
    }
}