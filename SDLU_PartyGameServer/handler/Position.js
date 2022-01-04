const DataVO = require('../type/VO');

module.exports = {
    type: "move",
    async handle(socket, payload) {
        try {
            socket.user.room.broadcast(JSON.stringify({ type: "move", payload: payload }));
        } catch (err) {
            console.log("MoveHandler > " + err);
        }
    }
}