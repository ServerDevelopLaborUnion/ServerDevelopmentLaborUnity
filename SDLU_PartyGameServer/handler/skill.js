const { DataVO } = require('../type/VO');


module.exports = {
    type: "skill",
    async handle(socket, payload) {
        try {
            socket.user.room.broadcast(JSON.stringify({ type: "skill", payload: payload }));
        } catch (err) {
            console.log("SkillHandler > " + err);
        }
    }
}