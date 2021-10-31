const { DataVO } = require("../VO/DataVO.js");
const { broadcast } = require("../Utils/Broadcast.js");

class ChatHandler
{
    Chat(socket, data)
    {
        var data = JSON.fromString(data);
        var message = data.payload;
        message = socket.user.nickname + ": " + message;

        if(data.message)
        {
            var dataVO = new DataVO();
            dataVO.type = "chat";
            dataVO.data = message;
            broadcast(dataVO);
        }
    }
}

exports.ChatHandler = ChatHandler;