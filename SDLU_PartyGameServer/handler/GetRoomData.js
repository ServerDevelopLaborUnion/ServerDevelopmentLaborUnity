const { DataVO } = require('../type/VO');

class GetRoomDataVO {
    constructor(id) {
        this.id = id;
    }
}

module.exports = {
    type: "GetRoomData",
    async handle(socket, payload) {
        const data = new GetRoomDataVO(payload.id);
        const vo = new DataVO("RoomData", JSON.stringify(socket.globalObj.roomManager.getRoomData(data.id)));
    }
}