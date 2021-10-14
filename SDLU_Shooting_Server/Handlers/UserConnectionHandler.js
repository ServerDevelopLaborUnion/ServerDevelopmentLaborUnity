const { broadcast } = require("../Utils/Broadcast.js");
const { DataVO } = require("../VO/DataVO.js");

function userConnectedHandler(wsServer, socket)
{
    broadcast(wsServer, socket, JSON.stringify(new DataVO("connect", JSON.stringify({ id: socket.sessionId }))));
}

exports.userConnectedHandler = userConnectedHandler;