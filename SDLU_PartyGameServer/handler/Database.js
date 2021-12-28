// const { DataVO } = require('../type/VO');
const { sendPost } = require('../util/database.js');

module.exports = {
    type: "DB",
    async handle(socket, payload) {
        sendPost(payload._type, payload._params);
    }
}