const { DataVO } = require("../VO/DataVO.js");
const { broadcast } = require("../Utils/Broadcast.js");

class ChatHandler
{
    Chat(socket, payload)
    {
        var message = payload;
        message = socket.user.nickname + ": " + message;

        if(message)
        {
            var dataVO = new DataVO();
            dataVO.type = "chat";
            dataVO.data = message;
            broadcast(JSON.stringify(dataVO));
        }
    }
}

exports.ChatHandler = new ChatHandler();