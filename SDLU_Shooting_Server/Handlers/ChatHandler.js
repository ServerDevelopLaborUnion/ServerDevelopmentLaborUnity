const { DataVO } = require("../VO/DataVO.js");
const { broadcast } = require("../Utils/Broadcast.js");

class ChatHandler {
    Chat(socket, payload) {
        /** @type {string} */
        var message = payload;

        if (message.trim() != "" && message != null) {
            message = socket.user.nickname + ": " + message;
            var dataVO = new DataVO("chat", message);
            broadcast(JSON.stringify(dataVO));
        }
    }
}

exports.ChatHandler = new ChatHandler();