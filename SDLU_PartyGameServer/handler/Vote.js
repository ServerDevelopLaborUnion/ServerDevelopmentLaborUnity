const { DataVO } = require('../type/VO');
const prefix = '[GetRoomData] ';

class VoteVO {
    constructor(item) {
        this.item = item;
    }
}

module.exports = {
    type: "Vote",
    async handle(socket, payload) {
        const voteVO = new VoteVO(payload.item);
        socket.user.room.voteGame(socket, voteVO.item);
    }
}
